using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;
using System.Net;
using EDS.Intranet.ServerControls.Configuration;

namespace EDS.Intranet.Common
{
    public class ContactLookup
    {
        private string firstNameValue = string.Empty;
        private string lastNameValue = string.Empty;
        private string phoneValue = string.Empty;
        private string emailValue = string.Empty;
        private string employeeOrVendorValue = string.Empty;
        private string globalIDRegexPattern = "^[b-df-hj-np-tv-z]z[b-df-hj-np-tv-z0-9]{4}$";

        public string Email
        {
            get { return emailValue; }
        }

        public string EmployeeOrVendor
        {
            get { return employeeOrVendorValue; }
        }

        public string FirstName
        {
            get { return firstNameValue; }
        }

        public string LastName
        {
            get { return lastNameValue; }
        }

        public string Phone
        {
            get { return phoneValue; }
        }

        public bool LookupContact(string globalID)
        {

            bool entryWasFound;
            string[] creds;
            const string ID = "cn=Rebar,AedsRsrcType=Applications,o=eds";
            string[] personAttributes = {"aedsfirstname", "aedsgoesbyname", "aedslastname", "telephonenumber", "mail", "aedsuidcstring"};
            
            if (globalID == null)
            {
                throw new ArgumentNullException("Global ID", "The given Global ID is a null reference.");
            }

            //retrieve the common corporate directory credentials to be used
            //to validate the globalID argument.
            {
                creds = AppConfigSettings.ldapCredentials.Split(';');
            }

            //verify given ID is in a valid format.
            //globalIDRegexPattern:  six characters long -- first character must be a consonant, second character must be a z,
            //the other four characters must be either consonants or digits; not case sensitive.
            if (Regex.IsMatch(globalID, globalIDRegexPattern, RegexOptions.IgnoreCase) == false)
            {
                throw new System.ArgumentException("The given Global ID is not in a valid format.", globalID);
            }

            using (LdapConnection corpDir = new LdapConnection(creds[0]))
            {
                {
                    corpDir.Credential = new NetworkCredential(ID, creds[1]);
                    corpDir.AuthType = AuthType.Basic;
                    corpDir.SessionOptions.ProtocolVersion = 3;
                    corpDir.Bind();
                }

                SearchRequest request = new SearchRequest("ou=People,o=EDS", "AedsNetID=" + globalID, SearchScope.Subtree, personAttributes);
                DirectoryResponse dirResponse;
               
                try
                {
                    dirResponse = corpDir.SendRequest(request);
                }

                catch (DirectoryOperationException)
                {
                    //global ID not found.
                    return false;

                }

                SearchResponse response = (SearchResponse)dirResponse;

                entryWasFound = response.Entries.Count > 0;

                if (entryWasFound)
                {
                    SearchResultAttributeCollection attr = response.Entries[0].Attributes;
                    DirectoryAttribute goesbynameAttr = attr["aedsgoesbyname"];
                    
                    if (goesbynameAttr == null)
                    {
                        DirectoryAttribute firstnameAttr = attr["aedsfirstname"];
                        if (firstnameAttr != null && firstnameAttr.Count > 0)
                        {
                            firstNameValue = TitleCase(firstnameAttr[0].ToString());
                        }
                    }
                    else
                    {
                        if (goesbynameAttr.Count > 0)
                        {
                            firstNameValue = TitleCase(goesbynameAttr[0].ToString());
                        }
                    }

                    DirectoryAttribute lastnameAttr = attr["aedslastname"];
                    
                    if (lastnameAttr != null && lastnameAttr.Count > 0)
                    {
                        lastNameValue = TitleCase(lastnameAttr[0].ToString());
                    }

                    DirectoryAttribute phoneAttr = attr["telephonenumber"];
                    
                    if (phoneAttr != null && phoneAttr.Count > 0)
                    {
                        if (phoneAttr.Count > 1)
                        {
                            string phone0 = phoneAttr[0].ToString();
                            if (phone0.StartsWith("8 "))
                            {
                                phoneValue = string.Format("{0} / {1}", phoneAttr[1], phone0);
                            }
                            else
                            {
                                phoneValue = string.Format("{0} / {1}", phone0, phoneAttr[1]);
                            }
                        }
                        else
                        {
                            phoneValue = phoneAttr[0].ToString();
                        }
                    }

                    DirectoryAttribute emailAttr = attr["mail"];
                    
                    if (emailAttr != null && emailAttr.Count > 0)
                    {
                        emailValue = FormatEmail(emailAttr[0].ToString());
                    }

                    DirectoryAttribute uidcAttr = attr["aedsuidcstring"];
                    
                    if (uidcAttr != null && uidcAttr.Count > 0)
                    {
                        employeeOrVendorValue = uidcAttr[0].ToString();
                    }
                }
            }
            return entryWasFound;
        }

        private string TitleCase(string name)
        {
            if (name == name.ToUpper())
            {
                //this came in all uppercase - convert to title case.
                name = name.Substring(0,1).ToUpper() + name.Substring(1).ToLower();
            }
            if (name.StartsWith("Mc") || name.StartsWith("O'"))
            {
                name = name.Substring(0, 2) + name.Substring(2,1).ToUpper() + name.Substring(3);
            }
            return name;
        }

        private string FormatEmail(string email)
        {

            int atPosition = email.IndexOf('@');

            if (email.ToLower().Contains("eds.com"))
            {
                if (atPosition > 0)
                {
                    return email.Substring(0, atPosition + 1) + "eds.com";
                }
            }

            return email;

        }
    }
}
