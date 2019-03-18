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
    public partial class PageUtilities : System.Web.UI.UserControl
    {
        private Boolean _ShowPrintEmailBookmark;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.ShowPrintEmailBookmark)
            {
                this.RenderPrintEmailBookmark();
            }
        }

        private void RenderPrintEmailBookmark()
        {
            this.toolsPlaceholder.SetRenderMethodDelegate(
            delegate(HtmlTextWriter w, Control c)
            {
                w.WriteLine(@"
                        <ul id=""pagetools"">
                        <li><a href=""#"" onclick=""print();"" onmouseover=""self.status='{1}';return true;"" onfocus=""self.status='{1}';return true;"" onmouseout=""self.status='';return true;"" onblur=""self.status='';return true;"" title=""{1}"" id=""printthis"">{0}</a></li>
                        <li><script type=""text/javascript"">pageToolsEmail();</script></li>
                        <li><a href=""#"" onclick=""pageToolsBookmark();"" onmouseover=""self.status='{3}';return true;"" onfocus=""self.status='{3}';return true;"" onmouseout=""self.status='';return true;"" onblur=""self.status='';return true;"" title=""{3}"" id=""bookmarkthis"">{2}</a></li>
                        </ul>
                "
                    , GetLocalResourceObject("Print")
                    , GetLocalResourceObject("PrintTooltip")
                    , GetLocalResourceObject("Bookmark")
                    , GetLocalResourceObject("BookmarkTooltip")
                    );
            });
        }

        public bool ShowBreadcrumb
        {
            set { this.smPath.Visible = value; }
        }

        public bool ShowPrintEmailBookmark
        {
            get { return _ShowPrintEmailBookmark; }
            set { _ShowPrintEmailBookmark = value; }
        }
    }
}