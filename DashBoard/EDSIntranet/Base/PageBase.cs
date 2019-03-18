using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using EDS.Intranet.Common;
using EDS.Intranet.ServerControls.Configuration;

namespace EDS.Intranet.Base
{
    /// <summary>
    ///  Page Base Class.  All .aspx pages in an application should ultimately inherit
    ///  from this base class.  This class should be customized for each unique
    ///  application.
    /// </summary>
    public abstract class PageBase : System.Web.UI.Page
    {
        private string _LoginUserID = String.Empty;
        private string _LoginUserName = String.Empty;
        private string _LoginUserEmail = String.Empty;

        /// <summary>
        ///  This method returns the Master Page for the current page.
        /// </summary>
        public new EDS.Intranet.IntranetCore Master
        {
            get { return (EDS.Intranet.IntranetCore)base.Master; }
        }

        /// <summary>
        /// This property returns the Login User ID
        /// </summary>
        public string LoginUserID
        {
            get
            {
                if (_LoginUserID == String.Empty)
                {
                    _LoginUserID = this.GetLoginUserID();
                }
                return _LoginUserID;
            }
        }

        /// <summary>
        /// This property returns the Login User Name.
        /// </summary>
        public string LoginUserName
        {
            get
            {
                if (_LoginUserName == String.Empty)
                {
                    _LoginUserName = this.GetLoginUserName();
                }
                return _LoginUserName;
            }
        }

        /// <summary>
        /// This property returns the Login User Email.
        /// </summary>
        public string LoginUserEmail
        {
            get
            {
                if (_LoginUserEmail == String.Empty)
                {
                    _LoginUserEmail = this.GetLoginUserEmail();
                }
                return _LoginUserEmail;
            }
        }

        /// <summary>
        /// The OnPreInit method is called at the beginning of the page initialization stage. 
        /// After the OnPreInit method is called, personalization information is loaded and the page theme, 
        /// if any, is initialized. This is also the preferred stage to dynamically define a PageTheme 
        /// or MasterPage for the Page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {

            String currentLanguage = String.Empty;

            //Set the CurrentCulture property to the culture associated with the saved cookie or 
            //the web browser's current language setting.
            if (Page.Request.Cookies.Get("language") != null)
            {
                currentLanguage = Page.Request.Cookies.Get("language").Value;
            }

            if (currentLanguage == String.Empty)
            {
                if (Page.Request.UserLanguages != null)
                {
                    currentLanguage = Page.Request.UserLanguages[0];

                    //Set the browser's default language into a cookie
                    this.SetCookie("language", currentLanguage);
                }
                else
                {
                    currentLanguage = "en";
                }
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentLanguage);

            //It is good practice to explicitly set the CurrentUICulture property.
            //Initialize the CurrentUICulture property with the CurrentCulture property.
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.OnPreInit(e);
        }

        /// <summary>
        /// The OnInit method performs the initialization and setup steps required to create a Page instance. 
        /// In this stage of the page's life cycle, declared server controls on the page are initialized 
        /// to their default state; however, the view state of each control is not yet populated. 
        /// A control on the page cannot access other server controls on the page during the Page_Init phase, 
        /// regardless of whether the other controls are child or parent controls. Other server controls 
        /// are not guaranteed to be created and ready for access.
        /// The OnInit method is called after the OnPreInit method and before the OnInitComplete method.
        /// </summary>
        /// <param name="e">The event data</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// The OnPreLoad method is called after all postback data returned from the user is loaded. 
        /// At this stage in the page's life cycle, view-state information and postback data for 
        /// declared controls and controls created during the initialization stage are loaded into 
        /// the page's controls.
        /// Controls created in the OnPreLoad method will also be loaded with view-state and postback data.
        /// </summary>
        /// <param name="e">The event data</param>
        /// <remarks></remarks>
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            // add onfocus and onblur Javascripts to all input controls on the form
            // so that the active control has a different appearance.
            //Utilities.SetInputControlsHighlight(this, "highlight", false);

            base.OnLoad(e);

            Master.SupportedLanguages = AppConfigSettings.SupportedLanguageCodes;
        }

        /// <summary>
        ///  This method returns the Global Net ID of the current user.
        /// </summary>
        /// <returns></returns>
        private string GetLoginUserID()
        {
            string currentUser = String.Empty;
            string siteMinderAuth = ConfigurationManager.AppSettings["ENABLE_SITE_MINDER"];

            if (siteMinderAuth != null && siteMinderAuth.Trim().ToLower().Equals("true"))
            {
                currentUser = ""; // Get the siteminder spefic user value from the header.
            }
            else
            {
                //Get authenticated login ID from ClearTrust
                currentUser = this.GetRequestServerVariable("HTTP_CT_REMOTE_USER");

                //Get authenticated login ID from User Identity
                if (currentUser.Length == 0)
                {
                    if (User.Identity.Name != null && User.Identity.Name.Trim().Length > 0)
                    {
                        currentUser = User.Identity.Name;
                        //Logger.writeDebug("Setting preference using User Identity " + currentUser);
                    }
                    else
                    {
                        //Logger.writeDebug("Pref using cookie option ");
                        //Check if the Cookie is setup for user.
                        if (Request.Cookies != null)
                        {
                            //Logger.writeDebug("pref cookie is not null");
                            HttpCookie cookie = Request.Cookies[Globals.DashboardCookieName];

                            if (cookie != null)
                            {
                                currentUser = cookie.Value;
                                //Logger.writeDebug("pref cookie user " + currentUser);
                            }
                        }
                    }

                }
            }


            if (currentUser.Length > 0)
            {
                //Strip the Domain Name from the Global Net ID.
                Int32 slashPosition = currentUser.IndexOf('\\');
                if (slashPosition > -1)
                {
                    currentUser = currentUser.Substring(slashPosition + 1);
                }
                currentUser = currentUser.ToUpper();
            }

            return currentUser;
        }

        /// <summary>
        /// This method returns the name of the current user.
        /// </summary>
        /// <returns></returns>
        private string GetLoginUserName()
        {
            EDS.Intranet.Common.ContactLookup contact;
            bool contactFound;
            string globalNetID;
            string userName;

            try
            {
                //Retrieve names from global net ID
                globalNetID = this.LoginUserID;
                globalNetID = globalNetID.Trim();
                contact = new EDS.Intranet.Common.ContactLookup();
                contactFound = contact.LookupContact(globalNetID);

                if (contactFound)
                {
                    userName = contact.FirstName + " " + contact.LastName;
                }
                else
                {
                    userName = "UNKNOWN USER";
                }

                return userName;
            }

            catch (System.Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// This method returns the email address of the current user.
        /// </summary>
        /// <returns></returns>
        private string GetLoginUserEmail()
        {
            EDS.Intranet.Common.ContactLookup contact;
            bool contactFound;
            string globalNetID;
            string userEmail;

            try
            {
                //Retrieve names from global net ID
                globalNetID = this.LoginUserID;
                globalNetID = globalNetID.Trim();
                contact = new EDS.Intranet.Common.ContactLookup();
                contactFound = contact.LookupContact(globalNetID);

                if (contactFound)
                {
                    userEmail = contact.Email;
                }
                else
                {
                    userEmail = String.Empty;
                }

                return userEmail;
            }

            catch (System.Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// This method checks to see if the current user is authorized for the given role.
        /// </summary>
        /// <param name="rolename"></param>
        protected void CheckAuthorization(string rolename)
        {
            try
            {
                if (!Roles.IsUserInRole(this.LoginUserID, rolename))
                {
                    Server.Transfer("~/EDSIntranet/Error/AccessDenied.aspx");
                }
            }
            catch (System.Exception sysException)
            {
                sysException.Data["CheckAuthorization: LoginUserID"] = this.LoginUserID;
                sysException.Data["CheckAuthorization: roleName"] = rolename;
                throw;
            }

        }

        /// <summary>
        /// This method initializes the culture.
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
        }

        protected string GetParmString(string parmName)
        {
            string parmValue = String.Empty;

            parmValue = Request.QueryString[parmName];
            if (parmValue != null)
            {
                Session[parmName] = parmValue;
            }
            else
            {
                parmValue = (string)Session[parmName];
            }
            return parmValue;
        }

        protected string GetSessionString(string keyName)
        {
            string keyValue = String.Empty;

            keyValue = (string)Session[keyName];

            return keyValue;
        }

        /// <summary>
        /// This method gets the integer value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public int GetSessionInteger(string keyName)
        {
            int keyValue;

            if (Session == null || Session[keyName] == null)
            {
                keyValue = Int32.MinValue;
            }
            else
            {
                keyValue = Convert.ToInt32(Session[keyName]);
            }

            return keyValue;
        }

        /// <summary>
        /// This method gets the boolean value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool GetSessionBoolean(string keyName)
        {
            bool keyValue;

            if (Session == null || Session[keyName] == null)
            {
                keyValue = false;
            }
            else
            {
                keyValue = Convert.ToBoolean(Session[keyName]);
            }

            return keyValue;
        }

        protected int GetRowsPerPage()
        {
            int rowsPerPage = 0;

            rowsPerPage = this.GetSessionInteger("RowsPerPage");

            if (rowsPerPage == Int32.MinValue)
            {
                rowsPerPage = Convert.ToInt32(AppConfigSettings.DefaultRowsPerPage);
            }
            return rowsPerPage;
        }

        protected void SetSessionString(string keyName, string keyValue)
        {
            SetSessionString(keyName, keyValue, false);
        }

        protected void SetSessionString(string keyName, string keyValue, bool locked)
        {
            if (locked)
            {
                lock (Session.SyncRoot)
                {
                    Session[keyName] = keyValue;
                }
            }
            else
            {
                Session[keyName] = keyValue;
            }
        }

        /// <summary>
        /// This method sets the integer value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        public void SetSessionInteger(string keyName, int keyValue)
        {
            SetSessionInteger(keyName, keyValue, false);
        }

        /// <summary>
        /// This method sets the integer value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        /// <param name="locked"></param>
        public void SetSessionInteger(string keyName, int keyValue, bool locked)
        {
            if (locked)
            {
                lock (Session.SyncRoot)
                {
                    Session[keyName] = keyValue;
                }
            }
            else
            {
                Session[keyName] = keyValue;
            }
        }

        /// <summary>
        /// This method sets the boolean value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        public void SetSessionBoolean(string keyName, bool keyValue)
        {
            SetSessionBoolean(keyName, keyValue, false);
        }

        /// <summary>
        /// This method sets the boolean value of the specified Session variable.
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        /// <param name="locked"></param>
        public void SetSessionBoolean(string keyName, bool keyValue, bool locked)
        {
            if (locked)
            {
                lock (Session.SyncRoot)
                {
                    Session[keyName] = keyValue;
                }
            }
            else
            {
                Session[keyName] = keyValue;
            }
        }

        /// <summary>
        /// Finds a Control recursively. Note finds the first match and exists
        /// </summary>
        /// <param name="ContainerCtl"></param>
        /// <param name="IdToFind"></param>
        /// <returns></returns>
        public Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;
            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);

                if (FoundCtl != null)

                    return FoundCtl;
            }
            return null;
        }

        public String GetRequestServerVariable(string name)
        {
            string returnValue;

            if (System.Web.HttpContext.Current == null)
            {
                return String.Empty;
            }

            if (System.Web.HttpContext.Current.Request == null)
            {
                return String.Empty;
            }

            returnValue = System.Web.HttpContext.Current.Request.ServerVariables[name];
            if (returnValue == null)
            {
                return String.Empty;
            }

            return returnValue;
        }

        public string GetWebServerName()
        {
            return this.GetRequestServerVariable("HTTP_HOST");
        }

        protected void SetCookie(string cookieName, string cookieValue)
        {
            HttpCookie aCookie = new HttpCookie(cookieName);
            aCookie.Value = cookieValue;
            aCookie.Expires = DateTime.Now.AddYears(1);
            aCookie.Path = "/";

            Response.Cookies.Add(aCookie);
        }

        protected override void OnError(EventArgs e)
        {
            try
            {
                //Retrieve the last error
                HttpContext ctx = HttpContext.Current;
                Exception exception = ctx.Server.GetLastError();

                //Unpack the exception into an instance of the ErrorLog class
                EDS.Intranet.Error.ErrorInfo lastError = new EDS.Intranet.Error.ErrorInfo();

                if (exception.InnerException == null)
                {
                    lastError.Message = exception.Message;
                    lastError.Source = exception.Source;
                    lastError.StackTrace = exception.StackTrace;
                    lastError.ExceptionType = exception.GetType().ToString();

                    if (exception.Data != null)
                    {
                        StringBuilder additionalInfo = new StringBuilder();
                        foreach (DictionaryEntry de in exception.Data)
                        {
                            additionalInfo.AppendLine(de.Key + " = " + de.Value + "<br/>");
                        }
                        lastError.Data = additionalInfo.ToString();
                    }

                }
                else
                {
                    lastError.Message = exception.InnerException.Message;
                    lastError.Source = exception.InnerException.Source;
                    lastError.StackTrace = exception.InnerException.StackTrace;
                    lastError.ExceptionType = exception.InnerException.GetType().ToString();

                    if (exception.InnerException.Data != null)
                    {
                        StringBuilder additionalInfo = new StringBuilder();
                        foreach (DictionaryEntry de in exception.InnerException.Data)
                        {
                            additionalInfo.AppendLine(de.Key + " = " + de.Value + "<br/>");
                        }
                        lastError.Data = additionalInfo.ToString();
                    }
                }

                if (exception.InnerException != null)
                {
                    lastError.InnerException = Server.HtmlEncode(exception.InnerException.Message);
                }

                lastError.Exception = exception.Message;

                if (exception.GetBaseException() != null)
                {
                    lastError.BaseException = exception.GetBaseException().Message;
                }

                lastError.BrowserType = ctx.Request.ServerVariables["HTTP_USER_AGENT"];

                lastError.ComputerName = Utilities.GetComputerName();

                lastError.NetVersion = Utilities.GetNetVersion();

                lastError.OSVersion = Utilities.GetOSVersion();

                if (exception.TargetSite != null)
                {
                    lastError.Target = exception.TargetSite.Name;
                }

                lastError.Url = ctx.Request.Url.ToString();

                lastError.UserID = this.LoginUserID;

                lastError.UserName = this.LoginUserName;

                //Encode any potentially problem text
                lastError.Message = Server.HtmlEncode(lastError.Message);

                //Save the error information into a static variable.
                Utilities.LastError = lastError;

                //Transfer to the GenericError page
                Response.Redirect("~/EDSIntranet/Error/GenericError.aspx", true);
            }
            catch
            {
            }
        }
    }
}
