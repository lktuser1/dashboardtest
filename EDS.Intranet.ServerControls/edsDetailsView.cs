using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDS.Intranet.ServerControls
{
    [ToolboxData("<{0}:edsDetailsView runat=server></{0}:edsDetailsView>")]
    public class edsDetailsView : System.Web.UI.WebControls.DetailsView
    {

        protected override void OnItemCreated(EventArgs e)
        {
            // Test FooterRow to make sure all rows have been created 
            if (this.FooterRow != null)
            {
                // The command bar is the last element in the Rows collection
                int commandRowIndex = this.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = this.Rows[commandRowIndex];

                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    foreach (Control ctl in cell.Controls)
                    {
                        LinkButton link = ctl as LinkButton;
                        if (link != null)
                        {
                            switch (link.CommandName)
                            {
                                case "Cancel":
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.CancelToolTip;
                                    break;
                                case "Delete":
                                    string confirmMessage = EDS.Intranet.ServerControls.Properties.Resources.DeleteConfirmMessage;
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.DeleteToolTip;
                                    link.OnClientClick = "return confirm('" + confirmMessage + "');";
                                    break;
                                case "Edit":
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.EditToolTip;
                                    break;
                                case "Insert":
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.InsertToolTip;
                                    break;
                                case "New":
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.NewToolTip;
                                    break;
                                case "Update":
                                    link.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.UpdateToolTip;
                                    break;
                            }

                        }
                    }
                }
            }
        }
    }
}
