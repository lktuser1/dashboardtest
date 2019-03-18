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
using EDS.Intranet.Common;

namespace Dashboard
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
        //private const string SmtpServer = "forwarder.eds.com";
        private const string SmtpServer = "smtp3.hpe.com";
        //private const string Recipient = "sradashboardoperations@hpe.com";
        private const string Subject = "Dashboard Website Comments";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Master.PageTitle = SiteMap.CurrentNode.ParentNode.Description;
            //Master.PageSubtitle = SiteMap.CurrentNode.Description;
            Master.PageTitle = "Contact us";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(EmailTextBox.Text);
                //msg.To.Add(Recipient);
                string email = System.Configuration.ConfigurationManager.AppSettings["CONTACT_US_EMAIL"];
                if (email == null || email.Trim().Equals(""))
                {
                    msg.To.Add("lalit.taneja@dxc.com");
                }
                else
                {
                    string[] emailList = email.Split(';');
                    foreach (string data in emailList)
                    {
                        msg.To.Add(data);
                    }
                }

                msg.Subject = Subject;

                string commentsBody = "<b>Name:</b> " + Server.HtmlEncode(NameTextBox.Text) + "<br/>"
                    + "<b>Location:</b> " + Server.HtmlEncode(LocationTextBox.Text) + "<br/>"
                    + "<b>E-mail:</b> " + Server.HtmlEncode(EmailTextBox.Text) + "<br/>"
                    + "<b>Phone:</b> " + Server.HtmlEncode(PhoneTextBox.Text) + "<br/>"
                    + "<b>Comments:</b> <br/>" + Server.HtmlEncode(CommentsTextBox.Text).Replace("\n", "<br/>");

                msg.Body = commentsBody;
                msg.IsBodyHtml = true;
                Logger.writeDebug("Comments() - Email To - " + msg.To.ToString());
                SmtpClient smtp = new SmtpClient(SmtpServer);
                //smtp.Port = 25;
                smtp.Send(msg);

                Response.Redirect("CommentsConfirm.aspx", true);
            }
        }





        protected MailAddressCollection getReceipientList()
        {
            MailAddressCollection mac = new MailAddressCollection();

            string email = System.Configuration.ConfigurationManager.AppSettings["CONTACT_US_EMAIL"];
            if (email == null || email.Trim().Equals(""))
            {
                mac.Add(new MailAddress("lalit.taneja@dxc.com"));
            }
            else
            {
                string[] emailList = email.Split(';');
                foreach (string data in emailList)
                {
                    mac.Add(new MailAddress(data));
                }
            }

            return mac;
        }
    }
}