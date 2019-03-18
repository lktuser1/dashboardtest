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

namespace EDS.Intranet.Utility
{
    /// <summary>
    ///  Privacy Class.  This class displays the Privacy page.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class Privacy : UtilityPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Master.PageTitle = SiteMap.CurrentNode.ParentNode.Description;
            Master.PageSubtitle = SiteMap.CurrentNode.Description;
        }
    }
}
