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

namespace EDSIntranet
{
    public partial class DashboardHelp : EDS.Intranet.Base.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "Dashboard Help";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
            this.Title = "Dashboard Help";
        }
    }
}
