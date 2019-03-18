using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace EDS.Intranet.ServerControls.Configuration
{
    /// <summary>
    ///  This class allows access to all application-specific appsSettings in the web.config file.
    /// </summary>
    /// <change date="2007/09/04" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public class AppConfigSettings: ConfigurationSection
    {
        public static string ApplicationAcronym
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["applicationAcronym"]; }
        }

        public static string DefaultLanguageCode
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["defaultLanguageCode"]; }
        }

        public static string SupportedLanguageCodes
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["supportedLanguageCodes"]; }
        }

        public static string DefaultRowsPerPage
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["defaultRowsPerPage"]; }
        }

        public static string ldapCredentials
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ldapConnectionString"].ConnectionString; }
        }

    } 
}
