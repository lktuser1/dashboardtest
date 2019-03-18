using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EDS.Intranet.Base;
using EDS.Intranet.Utility;

namespace EDSIntranet
{
    public partial class DashboardForWindow : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "Dashboard For Windows";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
            this.Title = "Dashboard For Windows";
        }
    }
}
