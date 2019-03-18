using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EDS.Intranet.Base;

namespace EDSIntranet.EDSIntranet.Utility
{
    /// <summary>
    ///  Comments Class.  This class displays the Contact Us page.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class Comments : UtilityPageBase
    {
        //TODO: Review and revise the following constants to determine how and where comment feedback is to be sent.
        private const string SmtpServer = "forwarder.eds.com";
        private const string Recipient = "lalit.taneja@hpe.com;lalit.taneja@hpe.com";
        private const string Subject = "Website Comment";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Master.PageTitle = SiteMap.CurrentNode.ParentNode.Description;
            Master.PageSubtitle = SiteMap.CurrentNode.Description;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(EmailTextBox.Text);
                msg.To.Add(Recipient);
                msg.Subject = Subject;

                string commentsBody = "<b>Name:</b> " + Server.HtmlEncode(NameTextBox.Text) + "<br/>"
                    + "<b>Location:</b> " + Server.HtmlEncode(LocationTextBox.Text) + "<br/>"
                    + "<b>E-mail:</b> " + Server.HtmlEncode(EmailTextBox.Text) + "<br/>"
                    + "<b>Phone:</b> " + Server.HtmlEncode(PhoneTextBox.Text) + "<br/>"
                    + "<b>Comments:</b> <br/>" + Server.HtmlEncode(CommentsTextBox.Text).Replace("\n", "<br/>");

                msg.Body = commentsBody;
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(SmtpServer);
                smtp.Send(msg);

                Response.Redirect("CommentsConfirm.aspx", true);
            }
        }
    }
}
