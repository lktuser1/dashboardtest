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
    ///  NavMenu Class.  This control displays the main navigation menu.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class NavMenu : System.Web.UI.UserControl
    {
        /// <summary>
        ///  This function determines which class to use to highlight the active navigation menu.
        /// </summary>
        /// <param name="nodeBucket"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected string HighlightActiveNavMenu(SiteMapNode nodeBucket)
        {

            //Pages that don't contain an item in Web.sitemap are not highlighted
            if ((SiteMap.CurrentNode == null))
            {
                return "navmenunormal";
            }

            //Do not show sitemapnodes with visible set to false
            if ((nodeBucket["visible"] != null))
            {
                if (nodeBucket["visible"].Equals("false"))
                {
                    return "navmenuhide";
                }
            }

            if ((SiteMap.CurrentNode.Equals(nodeBucket) | SiteMap.CurrentNode.IsDescendantOf(nodeBucket)))
            {
                return "navmenuselected";
            }
            else
            {
                return "navmenunormal";
            }
        }
    }
}