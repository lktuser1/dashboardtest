using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDS.Intranet.ServerControls
{
    [ToolboxData("<{0}:ControlBase runat=server></{0}:ControlBase>")]
    public class ControlBase : WebControl
    {
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete("Use not allowed.  Use a style sheet instead.", true)]
        public override System.Drawing.Color BackColor
        {
            set { }
        } 

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete("Use not allowed.  Use a style sheet instead.", true)]
        public override System.Drawing.Color BorderColor
        {
            set { }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete("Use not allowed.  Use a style sheet instead.", true)]
        public override System.Web.UI.WebControls.Unit BorderWidth
        {
            set { }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Obsolete("Use not allowed.  Use a style sheet instead.", true)]
        public override System.Drawing.Color ForeColor
        {
            set { }
        }

        public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
        {
            // Don't let Microsoft stuff generate. 
        }

        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
        {
            // Don't let Microsoft stuff generate. 
        } 

    }
}
