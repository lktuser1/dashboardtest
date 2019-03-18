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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Dashboard
{
    /// <summary>
    ///  Comments Class.  This class mails the Contact Us messages via SMTP, and 
    ///  displays a thank you message.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class CommentsConfirm : UtilityPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Master.PageTitle = SiteMap.CurrentNode.ParentNode.ParentNode.Description;
            //Master.PageSubtitle = SiteMap.CurrentNode.Description;
            Master.PageTitle = "Contact us - Thank you";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
        }
    }
}
