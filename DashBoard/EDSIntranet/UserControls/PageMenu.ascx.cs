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
    ///  PageMenu Class.  This control displays the page menu.
    /// </summary>
    /// <change date="2007/07/01" author="Todd McIntosh">
    ///  First revision
    /// </change>
    public partial class PageMenu : System.Web.UI.UserControl
    {
        /// <summary>
        ///  This function determines which class to use to highlight the active page menu.
        /// </summary>
        /// <param name="nodeBucket"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected string HighlightActivePageMenu(SiteMapNode nodeBucket)
        {

            //Pages that don't contain an item in Web.sitemap are not highlighted
            if ((SiteMap.CurrentNode == null))
            {
                return "pagemenunormal";
            }

            if ((SiteMap.CurrentNode.Equals(nodeBucket) | SiteMap.CurrentNode.IsDescendantOf(nodeBucket)))
            {
                return "pagemenuselected";
            }
            else
            {
                return "pagemenunormal";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string createURL(SiteMapNode node)
        {
            if (node.Roles.Contains("IGNORE"))
            {
                return "";
            }
            else
            {
                return node.Url;
            }
        }
    }
}