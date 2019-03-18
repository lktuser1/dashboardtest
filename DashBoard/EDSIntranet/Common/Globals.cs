using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EDS.Intranet.Common
{
    /// <summary>
    ///  This class contains any global strings or constants used throughout the web application.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public sealed class Globals
    {
        public const string EDSInfoHandlingLink = "<a href='http://www.security.eds.com/edssec/awareness/info_handling/information_handling_classifications.aspx'>";
        public const string EDSTemplatesLink = "<a href='http://web.standards.eds.com/templates/'>";
        public const string AdministratorRole = "Administrator";
        public const string SecurityAdministratorRole = "Security Administrator";
        public const string DashboardCookieName = "DW_SRA_DASHBOARD";

    }
}
