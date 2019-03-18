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

namespace EDSIntranet.Admin
{
    public partial class SQLQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack)
            {
                
                ltrMessage.Visible = true;
                if (this.txtQuery.Text.ToLower().Trim().StartsWith("select"))
                {
                    this.ltrMessage.Text = "Message is good";
                }
                else
                {
                    this.ltrMessage.Text = "Only Select query is supported at this time.";
                }

            }
        }
    }
}
