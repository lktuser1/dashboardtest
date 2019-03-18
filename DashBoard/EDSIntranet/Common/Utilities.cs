using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EDS.Intranet.Common
{
    /// <summary>
    /// This class contains various utility functions.
    /// </summary>
    public class Utilities
    {
        private static EDS.Intranet.Error.ErrorInfo _lastError;

        /// <summary>
        /// This static property holds the last error.
        /// </summary>
        public static EDS.Intranet.Error.ErrorInfo LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }

        /// <summary>
        /// This method returns an array with the names of all local Themes.
        /// </summary>
        /// <returns></returns>
        public static string[] GetThemes()
        {
            if ((HttpContext.Current.Cache["SiteThemes"] != null))
            {
                return (string[])HttpContext.Current.Cache["SiteThemes"];
            }
            else
            {
                string themesDirPath = HttpContext.Current.Server.MapPath("~/App_Themes");
                // get the array of themes folders under /App_Themes
                string[] themes = Directory.GetDirectories(themesDirPath);
                for (int i = 0; i <= themes.Length - 1; i++)
                {
                    themes[i] = Path.GetFileName(themes[i]);
                }
                CacheDependency dep = new CacheDependency(themesDirPath);
                HttpContext.Current.Cache.Insert("SiteThemes", themes, dep);
                return themes;
            }

        }

        /// <summary>
        /// This method adds the onfocus and onblur attributes to all input controls found in the specified parent,
        //  to change their appearance when the control has the focus
        /// </summary>
        /// <param name="container"></param>
        /// <param name="className"></param>
        /// <param name="onlyTextBoxes"></param>
        public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
        {
            foreach (Control ctl in container.Controls)
            {
                if ((onlyTextBoxes && (ctl) is TextBox) || (ctl) is TextBox || (ctl) is DropDownList || (ctl) is ListBox || (ctl) is CheckBox || (ctl) is RadioButton || (ctl) is RadioButtonList || (ctl) is CheckBoxList)
                {
                    WebControl wctl;
                    wctl = (WebControl)ctl;
                    wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
                    wctl.Attributes.Add("onblur", "this.className = '';");
                }
                else
                {
                    if ((ctl.Controls.Count > 0)) SetInputControlsHighlight(ctl, className, onlyTextBoxes);
                }
            }
        }

        /// <summary>
        ///  This method converts the input plain-text to HTML version, replacing carriage returns
        //   and spaces with <br /> and &nbsp;
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ConvertToHtml(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;").Replace("\\t", "&nbsp;&nbsp;&nbsp;").Replace("\\n", "<br>");
            return content;
        }

        /// <summary>
        ///  This method formats the specified date in the EDS standard format.
        /// </summary>
        /// <param name="edsDate"></param>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string edsDateTime(System.DateTime edsDate, bool showTime)
        {
            string myDate;

            if (edsDate == DateTime.MinValue)
            {
                return string.Empty;
            }

            myDate = edsDate.ToString("dd MMM yyyy");

            if (showTime == true)
            {
                myDate += ",&nbsp;" + edsDate.ToShortTimeString();
            }

            return myDate;
        }

        /// <summary>
        /// This method looks up an employee record in Corporate Directory given a global net ID.
        /// </summary>
        /// <param name="globalNetID"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EmployeeLookup(string globalNetID, ref string firstName, ref string lastName, ref string phone, ref string email)
        {
            EDS.Intranet.Common.ContactLookup contact;
            bool contactFound;

            try
            {
                //Retrieve names from global net ID
                globalNetID = globalNetID.Trim();
                contact = new EDS.Intranet.Common.ContactLookup();
                contactFound = contact.LookupContact(globalNetID);

                if (contactFound)
                {
                    firstName = contact.FirstName;
                    lastName = contact.LastName;
                    phone = contact.Phone;
                    email = contact.Email;
                }
                else
                {
                    firstName = "UNKNOWN";
                    lastName = "UNKNOWN";
                    phone = "UNKNOWN";
                    email = "UNKNOWN";
                }

                return contactFound;
            }

            catch (System.Exception)
            {
                return false;
            }

            finally
            {
                // put clean up code here
            }
        }

        /// <summary>
        ///  This method generates a unique name/id that can be used 
        ///  for client script ids.  The generated name
        ///  will be the character "x" followed by a GUID
        ///  with all of the hyphens removed.
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueStringID()
        {
            return "x" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        ///  This method duplicates/concatenates a provided string the specified number of times.
        ///  For example this could be used to create a string that consists of
        ///  3 non-blank spaces by specifying the non-blank space character as the 
        ///  "stringToReplicate" and 3 as the "times".
        /// </summary>
        /// <param name="textToReplicate"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string ReplicateString(string textToReplicate, Int32 times)
        {
            if (string.IsNullOrEmpty(textToReplicate))
            {
                return string.Empty;
            }
            else
            {
                System.Text.StringBuilder buffer = new System.Text.StringBuilder(textToReplicate.Length * times);

                for (Int32 i = 1; i <= times; i++)
                {
                    buffer.Append(textToReplicate);
                }

                return buffer.ToString();
            }
        }

        /// <summary>
        /// This method gets the specified key from Cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCache(string key)
        {
            return (string)HttpContext.Current.Cache[key];
        }

        /// <summary>
        /// This method adds the specified key to Cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCache(string key, object value)
        {
            HttpContext.Current.Cache.Insert(key, value);
        }

        /// <summary>
        /// This method returns the current machine name.
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            try
            {
                return System.Environment.MachineName.ToString();
            }
            catch
            {
                return "Unknown";
            }
        }


        /// <summary>
        /// This method returns the current machine .NET version.
        /// </summary>
        /// <returns></returns>
        public static string GetNetVersion()
        {
            try
            {
                return System.Environment.Version.ToString();
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// This method returns the current machine operating system version.
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            try
            {
                return System.Environment.OSVersion.ToString();
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// This method gets the elapsed time in a nice format.
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        /// <returns></returns>
        public static string GetElapsedTime(int elapsedSeconds)
        {
            string timeString = String.Empty;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            seconds = elapsedSeconds;

            minutes = seconds / 60;
            hours = minutes / 60;
            minutes = minutes % 60;
            seconds = seconds % 60;

            if (hours > 0)
            {
                if (hours == 1)
                {
                    timeString = timeString + hours + " hour, ";
                }
                else
                {
                    timeString = timeString + hours + " hours, ";
                }
            }

            if (minutes > 0)
            {
                if (minutes == 1)
                {
                    timeString = timeString + minutes + " minute, ";
                }
                else
                {
                    timeString = timeString + minutes + " minutes, ";
                }
            }

            timeString = timeString + seconds + " seconds";

            return timeString;
        }

    }
}
