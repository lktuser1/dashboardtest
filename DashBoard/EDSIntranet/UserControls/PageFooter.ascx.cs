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
using EDS.Intranet.Common;

namespace EDS.Intranet.UserControls
{
    /// <summary>
    ///  AppFooter Class.  This control displays the page footer.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class PageFooter : System.Web.UI.UserControl
    {
        /// <summary>
        /// This method is called during the OnLoad event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            string myString;

            base.OnLoad(e);

            //Set the language-specific values for the Footer Disclaimer
            myString = this.FooterDisclaimerTag.Text;
            this.FooterDisclaimerTag.Text = string.Format(myString, "<strong>" + Globals.EDSInfoHandlingLink, "</a></strong>");

            //Set the language-specific values for the Footer Templates text
            myString = this.FooterTemplateTag.Text;
            this.FooterTemplateTag.Text = myString = string.Format(myString, Globals.EDSTemplatesLink, "</a>");
        }
    }
}