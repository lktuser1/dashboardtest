using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EDS.Intranet.Common;

namespace EDSIntranet
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {


            string currentUser = String.Empty;
            string siteMinderAuth = ConfigurationManager.AppSettings["ENABLE_SITE_MINDER"];
            string siteMinderURL = ConfigurationManager.AppSettings["SITE_MINDER_URL"];
            string siteMinderKey = ConfigurationManager.AppSettings["SITE_MINDER_KEY"];
            string siteMinderKeyValue = "";
            

            if (siteMinderAuth != null && siteMinderAuth.Trim().ToUpper().Equals("TRUE"))
            {
                try
                {
                    //Make sure that siteMinderKey is defined
                    if (siteMinderKey == null || siteMinderKey.Trim().Length == 0)
                    {
                        throw new Exception("Invalid site minder validation. SiteMinderKey must be defined in configuration file.");
                    }
                    else
                    {
                        //get the site minder key value
                        siteMinderKeyValue = Request.Headers[siteMinderKey];
                        if (siteMinderKeyValue == null || siteMinderKeyValue.Trim().Length == 0)
                        {
                            Logger.writeDebug("site Minder key value is not there in header. redirecting to Site minder site for validation.");
                            Response.Redirect(siteMinderURL);
                        }
                        else
                        {
                            currentUser = siteMinderKeyValue;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new Exception("You are not authorized to access this application." + ex.Message);
                }
            }
            else
            {
                try
                {
                    //Get authenticated login ID from ClearTrust
                    currentUser = this.GetRequestServerVariable("HTTP_CT_REMOTE_USER");

                    //Get authenticated login ID from User Identity
                    if (currentUser.Length == 0)
                    {
                        if (User.Identity.Name != null && User.Identity.Name.Trim().Length > 0)
                        {
                            currentUser = User.Identity.Name;
                            Logger.writeDebug("Setting identity based on User name : " + currentUser);
                        }
                        else
                        {
                            Logger.writeDebug("User.Identify is null trying cookie");

                            //Looks like server is not setup with Window authentication. Try tracking user with Cookies
                            if (Request.Cookies != null)
                            {
                                HttpCookie cookie = Request.Cookies[Globals.DashboardCookieName];

                                if (cookie != null)
                                {
                                    currentUser = cookie.Value;
                                    Logger.writeDebug("tracking cookie found " + currentUser);
                                }
                                else
                                {
                                    Logger.writeDebug("Creating new cookie ");
                                    //set the value of cookie
                                    cookie = new HttpCookie(Globals.DashboardCookieName);
                                    cookie.Expires = DateTime.Now.AddYears(5);
                                    cookie.Value = Guid.NewGuid().ToString();

                                    currentUser = cookie.Value;

                                    Response.Cookies.Add(cookie);

                                }
                            }
                            else
                            {
                                Logger.writeDebug("Cookie is null");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logger.writeError("Error occured while identifying user", ex);
                }

                try
                {
                    if (currentUser.Length > 0)
                    {
                        Logger.writeLog("USER TRACKER : Starting session for user : " + currentUser);

                        //Strip the Domain Name from the Global Net ID.
                        Int32 slashPosition = currentUser.IndexOf('\\');
                        if (slashPosition > -1)
                        {
                            currentUser = currentUser.Substring(slashPosition + 1);
                        }
                        currentUser = currentUser.ToUpper();

                        //If we have the user lets add it to database for tracking purpose.
                        SQLHelper sqlHelper = new SQLHelper();
                        sqlHelper.checkForUser(currentUser);
                    }
                    else
                    {
                        Logger.writeLog("USER TRACKER : Unable to determine the user identity");
                    }
                }
                catch (Exception ex)
                {
                    Logger.writeError("Error encountered while processing Session start function.", ex);
                }

            }

            Logger.writeLog("Request was initiated from " + Request.UserHostName + " and ip_address " + Request.UserHostAddress);

        }

        private String GetRequestServerVariable(string name)
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
    }
}