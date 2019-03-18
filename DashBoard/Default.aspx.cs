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

namespace Dashboard
{
    public partial class Default : HomePageBase
    {
         protected override void OnLoad(EventArgs e)
        {
            string myString;

            base.OnLoad(e);

            //Get localized text to display for this page
            myString = this.GetLocalResourceObject("Welcome").ToString();
            myString = String.Format(myString, "<a href='Category1/Category1Main.aspx'>", "</a>");
            this.Welcome.Text = myString;
            this.Master.PageBodyClass = "home";
           
            
        }
    }
}
