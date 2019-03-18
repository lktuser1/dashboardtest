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

namespace EDS.Intranet.UserControls
{
    /// <summary>
    ///  PageContact Class.  This control displays the page contact information.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class PageContact : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ContactName
        {
            set
            {
                ContactNameText.Text = value;
            }
        }

        public string ContactEmail
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ContactEmailLink.Visible = true;
                    ContactEmailText.Text = value;
                    ContactEmailLink.NavigateUrl = "mailto:" + value;
                }
                else
                {
                    ContactEmailLink.Visible = false;
                }
            }
        }

        public string ContactNetid
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ViewContactDetailsLink.Visible = true;
                    this.ViewContactDetailsLink.NavigateUrl = string.Format(
                        @"http://peoplesearch.iweb.eds.com/Search.aspx?queryText={0}"
                        , value);
                }
                else
                {
                    this.ViewContactSeparator.Visible = false;
                    this.ViewContactDetailsLink.Visible = false;
                }
            }
        }
    }
}