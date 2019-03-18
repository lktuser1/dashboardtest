using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDS.Intranet;
using EDS.Intranet.Base;
using EDS.Intranet.Common;
using System.Drawing;


namespace EDSIntranet
{
    public partial class HistoricalReporting : System.Web.UI.Page
    {
        public string strApplication { get; set; }
        public string strInstances { get; set; }
        public int Priority { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            IntranetCore masterpage = (IntranetCore)(Page.Master);

            masterpage.PageTitle = "Historical Reporting";
            masterpage.ShowPageLastUpdated = false;
            Master.Page.Title = "test";
            this.Title = "Historical Reporting";
            //Master.PageTitle = "Dashboard";
            //Master.PageSubtitle = String.Empty;
            // Master.ShowPageLastUpdated = false;
            // this.Title = "Dashboard";
            /* GridView1.DataSource = getGridData(); 
             GridView1.DataBind();*/


        }

        protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApplication.SelectedValue == "Select")
            {
                ddlInstances.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Select";
                li.Value = "Select";
                ddlInstances.Items.Add(li);
            }
            if (ddlApplication.SelectedValue == "SA")
            {
                ddlInstances.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "SA Prod";
                ddlInstances.Items.Add(li);
            }
            if (ddlApplication.SelectedValue == "SM7 Prod")
            {
                // ListItem liInstances = new ListItem();
                // liInstances.Text = "ALU";
                ddlInstances.Items.Clear();
                ListItemCollection liInstances = new ListItemCollection();
                liInstances.Add(new ListItem("--Select--", "--Select--"));
                liInstances.Add(new ListItem("ALU", "ALU"));
                liInstances.Add(new ListItem("AMS", "AMS"));
                liInstances.Add(new ListItem("APJ-1", "APJ-1"));
                liInstances.Add(new ListItem("EMEA", "EMEA"));
                liInstances.Add(new ListItem("EMEA-2", "EMEA-2"));
                ddlInstances.DataSource = liInstances;
                ddlInstances.DataBind();
            }
        }

        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPriority.SelectedValue == "Select")
            {
                ddlPriority.Items.Clear();
                ListItemCollection liPriority = new ListItemCollection();
                liPriority.Add(new ListItem("1", "1"));
                liPriority.Add(new ListItem("2", "2"));
                liPriority.Add(new ListItem("3", "3"));
                liPriority.Add(new ListItem("4", "4"));

            }
        }

        protected void ddlMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar2.Visible = true;
        }

        protected void DateTimeChanged(object sender, EventArgs e)
        {
            txtFromDate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }
        protected void DateTimeToChanged(object sender, EventArgs e)
        {
            txtToDate.Text = Calendar2.SelectedDate.ToShortDateString();
            Calendar2.Visible = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGridView();
            ImageButton2.Visible = true;

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        protected void BindGridView()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"select DB_monitor.name, 
                DB_monitor.priority, DB_alert.creationtime, DB_message.message from DB_alert
                INNER JOIN DB_monitor on DB_alert.monitor = DB_monitor.ID
                INNER JOIN DB_message on DB_alert.errmess = DB_message.ID
                where DB_monitor.application = @strApplication and DB_monitor.instance = 
                @strInstances and DB_monitor.priority = @Priority and
                DB_alert.creationtime between @FromDate and @ToDate and DB_alert.errmess != '1'"))
                {
                    cmd.Parameters.Add("@strApplication", System.Data.SqlDbType.VarChar);// Set SqlDbType based on your DB column Data-Type
                    cmd.Parameters.Add("@strInstances", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@Priority", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@FromDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@ToDate", System.Data.SqlDbType.DateTime);
                    cmd.Parameters["@strApplication"].Value = ddlApplication.SelectedValue;
                    cmd.Parameters["@strInstances"].Value = ddlInstances.SelectedValue;
                    cmd.Parameters["@Priority"].Value = ddlPriority.SelectedValue;
                    //2017-04-01 00:00:00.000
                    cmd.Parameters["@FromDate"].Value = Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    cmd.Parameters["@ToDate"].Value = Calendar2.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/xls";
            Response.AddHeader("content-disposition", "attachment;filename=HistoricalReporting.xls");
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                this.BindGridView();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}