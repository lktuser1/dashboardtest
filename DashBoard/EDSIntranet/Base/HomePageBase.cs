using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EDS.Intranet.ServerControls.Configuration;

namespace EDS.Intranet.Base
{
    /// <summary>
    ///  Core Page Base Class.  All core .aspx pages in an application should inherit
    ///  from this base class.  This class should be customized for each unique
    ///  application.
    /// </summary>
    public class HomePageBase : PageBase
    {
        /// <summary>
        /// The OnPreInit method is called at the beginning of the page initialization stage. 
        /// After the OnPreInit method is called, personalization information is loaded and the page theme, 
        /// if any, is initialized. This is also the preferred stage to dynamically define a PageTheme 
        /// or MasterPage for the Page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        /// <summary>
        /// The OnInit method performs the initialization and setup steps required to create a Page instance. 
        /// In this stage of the page's life cycle, declared server controls on the page are initialized 
        /// to their default state; however, the view state of each control is not yet populated. 
        /// A control on the page cannot access other server controls on the page during the Page_Init phase, 
        /// regardless of whether the other controls are child or parent controls. Other server controls 
        /// are not guaranteed to be created and ready for access.
        /// The OnInit method is called after the OnPreInit method and before the OnInitComplete method.
        /// </summary>
        /// <param name="e">The event data</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// The OnPreLoad method is called after all postback data returned from the user is loaded. 
        /// At this stage in the page's life cycle, view-state information and postback data for 
        /// declared controls and controls created during the initialization stage are loaded into 
        /// the page's controls.
        /// Controls created in the OnPreLoad method will also be loaded with view-state and postback data.
        /// </summary>
        /// <param name="e">The event data</param>
        /// <remarks></remarks>
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
        }

        /// <summary>
        ///  The OnLoad method is called immediately after the browser loads the page.
        /// </summary>
        /// <param name="e">The event data</param>
        /// <remarks></remarks>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Do not show a page title
            Master.ShowPageTitle = false;

            //Do not show a page subtitle
            Master.ShowPageSubTitle = false;

            //Indicate whether the Last Updated control should appear on the page.
            Master.ShowPageLastUpdated = false;

            //Indicate whether the page utilities (breadcrumbs, print, email, bookmark) should appear on the page.
            Master.ShowPageUtils = false;

            //Indicate whether to show the page menu
            Master.ShowPageMenu = false;

            //Set the default value for the Window Title.
            Master.WindowTitle = Master.SiteName;
        }
    }
}
