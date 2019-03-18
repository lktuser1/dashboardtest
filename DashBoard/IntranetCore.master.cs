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
using EDS.Intranet.Common;
using EDS.Intranet.UserControls;

namespace EDS.Intranet
{
    public partial class IntranetCore : System.Web.UI.MasterPage
    {
        private bool _ShowPageBreadCrumbs;
        private bool _ShowPageLastUpdated;
        private bool _ShowPageMenu;
        private bool _ShowPageSubTitle;
        private bool _ShowPageTitle;
        private bool _ShowPageUtils;
        private string _SupportedLanguages;

        /// <summary>
        ///  This property gets/sets the current date.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CurrentDate
        {
            get { return CurrentDateCtl.Text; }
            set { CurrentDateCtl.Text = value; }
        }

        /// <summary>
        ///  This property gets the Footer Control.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public PageFooter Footer
        {
            get { return this.PageFooter; }

        }

        /// <summary>
        ///  This property gets/sets the Body class the current page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PageBodyClass
        {
            get { return this.pageBody.Attributes["class"]; }
            set { this.pageBody.Attributes["class"] = value; }
        }

        /// <summary>
        ///  This property gets/sets the Last Updated date for the current page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PageLastUpdated
        {
            get { return PageLastUpdatedCtl.Text; }
            set { PageLastUpdatedCtl.Text = value; }
        }

        /// <summary>
        ///  This property gets/sets the subtitle of the current page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PageSubtitle
        {
            get { return PageSubtitleTag.Text; }
            set { PageSubtitleTag.Text = value; }
        }

        /// <summary>
        ///  This property gets/sets the name of the current page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PageTitle
        {
            get { return PageTitleTag.Text; }
            set { PageTitleTag.Text = value; }
        }
       
        
        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the breadcrumbs control on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageBreadCrumbs
        {
            get { return _ShowPageBreadCrumbs; }
            set
            {
                _ShowPageBreadCrumbs = value;
                this.PageUtils.ShowBreadcrumb = value;
            }
        }

        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the Last Updated control on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageLastUpdated
        {
            get { return _ShowPageLastUpdated; }
            set
            {
                _ShowPageLastUpdated = value;
                this.lastupdated.Visible = value;
            }
        }

        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the Page Menu on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageMenu
        {
            get { return _ShowPageMenu; }
            set
            {
                _ShowPageMenu = value;
                this.PageMenu.Visible = value;
                if (value == false)
                {
                    this.pageBody.Attributes["class"] = "full";
                }
            }
        }

        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the subtitle on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageSubTitle
        {
            get { return _ShowPageSubTitle; }
            set
            {
                _ShowPageSubTitle = value;
                this.pageSubTitle.Visible = value;
            }
        }

        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the Page Title on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageTitle
        {
            get { return _ShowPageTitle; }
            set
            {
                _ShowPageTitle = value;
                this.PageTitleTag.Visible = value;
            }
        }

        /// <summary>
        ///  This property gets/sets a flag indicating whether to display the Page Utilities on the page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowPageUtils
        {
            get { return _ShowPageUtils; }
            set
            {
                _ShowPageUtils = value;
                this.PageUtils.Visible = value;
            }
        }

        /// <summary>
        ///  This property gets/sets the name of the web site.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SiteName
        {
            get { return SiteNameTag.Text; }
            set { SiteNameTag.Text = value; }
        }

        /// <summary>
        ///  This property gets/sets the tag line for the web site.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SiteTagLine
        {
            get { return SiteTagLineTag.Text; }
            set { SiteTagLineTag.Text = value; }
        }

        /// <summary>
        ///  This property gets/sets the list of supported languages.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SupportedLanguages
        {
            get { return _SupportedLanguages; }
            set { _SupportedLanguages = value; }

        }

        /// <summary>
        ///  This property gets/sets the window title.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string WindowTitle
        {
            get { return this.Page.Header.Title; }
            set { this.Page.Header.Title = value; }
        }

        /// <summary>
        ///  This property gets/sets the name of the current page.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string RightPageTitle
        {
            get { return RightPageTitleTag.Text; }
            set { RightPageTitleTag.Text = value; }
        }

        /// <summary>
        ///  This method is called by the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {

            RegisteredClientScriptBlocks.SetRenderMethodDelegate(
            delegate(HtmlTextWriter w, Control c)
            {
                w.WriteLine();
                w.WriteLine(@"<script src=""{0}"" type=""text/javascript""></script>"
                    , ResolveClientUrl("EDSIntranet/Common/com/js/utils.js"));
            });

            if (this.CurrentDate.Length == 0)
            {
                this.CurrentDate = Utilities.edsDateTime(DateTime.Now, true);
            }

            if (this.PageTitle.Length == 0)
            {
                this.PageTitle = "Default Page Name";
            }

            if (this.SiteName.Length == 0)
            {
                this.SiteName = "Default Site Name";
            }

            if (this.SiteTagLine.Length == 0)
            {
                this.SiteTagLine = "Default Site Tag Line";
            }

            if (this.WindowTitle.Length == 0)
            {
                this.WindowTitle = "Default Window Title";
            }
        }


    }
}
