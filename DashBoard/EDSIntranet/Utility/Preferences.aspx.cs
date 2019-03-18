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
using EDS.Intranet.ServerControls.Configuration;

namespace EDSIntranet.EDSIntranet.Utility
{
    /// <summary>
    ///  Comments Class.  This class displays the Preferences page.
    /// </summary>
    /// <change date="2008/02/26" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class Preferences : UtilityPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Master.PageTitle = SiteMap.CurrentNode.ParentNode.Description;
            Master.PageSubtitle = SiteMap.CurrentNode.Description;

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string languageCode = "en";

            languageCode = this.ddlLanguage.SelectedValue;
            this.ChangeLanguage(languageCode);

            Response.Redirect("Preferences.aspx",true);
        }

        protected void Language_OnLoad(object sender, EventArgs e)
        {
            string currentLanguage = String.Empty;
            string[] localeCodes;
            bool isSelected = false;

            if (!IsPostBack)
            {
                localeCodes = this.Master.SupportedLanguages.Split(',');

                if (HttpContext.Current.Request.Cookies["language"] != null)
                {
                    currentLanguage = HttpContext.Current.Request.Cookies["language"].Value;
                }

                foreach (string localeCode in localeCodes)
                {
                    if (localeCode == currentLanguage)
                    {
                        isSelected = true;
                    }
                    else
                    {
                        isSelected = false;
                    }

                    string nativeName = "Unknown";
                    string displayName = "&lt;" + localeCode + "&gt;";

                    System.Globalization.CultureInfo ci = null;
                    try
                    {
                        ci = new System.Globalization.CultureInfo(localeCode);
                        nativeName = ci.NativeName;
                        displayName = ci.DisplayName;
                    }
                    catch
                    {
                    }

                    ListItem item = new ListItem();
                    item.Text = displayName + " / " + nativeName;
                    item.Value = localeCode;
                    item.Selected = isSelected;

                    this.ddlLanguage.Items.Add(item);
                }
            }
        }

        protected void ChangeLanguage(string languageCode)
        {
            HttpCookie aCookie = new HttpCookie("language");
            aCookie.Value = languageCode;
            aCookie.Expires = DateTime.Now.AddYears(1);
            aCookie.Path = "/";

            Response.Cookies.Add(aCookie);

        }
    }
}
