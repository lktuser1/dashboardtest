using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using EDS.Intranet.Base;
using EDS.Intranet.Common;
using Dashboard.Reports;
using Dashboard.Monitors;


namespace Dashboard.Monitors
{
    public partial class MonitorDetailsList :  PageBase
    {
        
        private string application = "SM7 Prod";
        private string instance = "AMS";
        private string priority = "1" ;
        private string monitor = "";
        //string localInstance = "";
        SQLHelper sqlHelper = new SQLHelper();

        private string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Master.PageTitle = "Monitor Details List";
                Master.ShowPageSubTitle = true;
                Master.PageSubtitle = "Monitor Details List";
                Master.ShowPageLastUpdated = false;
                this.Title = "Monitors Details List";

                //string localInstance = "";
                 if (Request.QueryString["app"] != null)
                {
                    application = Request.QueryString["app"];
                }
                if (Request.QueryString["inst"] != null)
                {
                    instance = Request.QueryString["inst"];
                    //localInstance = sqlHelper.getInstanceFullName(instance);
                }
                Master.PageSubtitle = "For Application " + application + " and Instance " + instance;
                MonitorDataAccess monitorData = new MonitorDataAccess();
                DataSet ds = new DataSet();

                ds = monitorData.getMonitorDetailsList("id", dbConnection, application, instance, priority, monitor);
                this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                this.GridView1.DataBind();

              //GridView1.DataSource =  MonitorDataAccessLayer.GetMonitors("id", dbConnection, application, instance, "", "");
              //  GridView1.DataBind();
            }
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Response.Write("Sort Expression = " + e.SortExpression);
            //Response.Write("<br/>");
            //Response.Write("Sort Direction = " + e.SortDirection.ToString());

            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            MonitorDataAccess monitorData = new MonitorDataAccess();
            DataSet ds = new DataSet();
            

            ds = monitorData.getMonitorDetailsList(e.SortExpression + " " + strSortDirection, dbConnection, application, instance, priority, monitor);
            this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
            this.GridView1.DataBind();


            //GridView1.DataSource = MonitorDataAccessLayer.GetMonitors(e.SortExpression + " " + strSortDirection, dbConnection, application, instance, "", "");
           // GridView1.DataBind();
        }


        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null && gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] = (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
            }
        }

        protected void Export_Click(object sender, EventArgs e)
        {

            if (GridView1.Rows.Count != 0)
            {
                // Clear all content output from the buffer stream
                Response.ClearContent();

                // Specify the default file name using "content-disposition" RESPONSE header
                Response.AppendHeader("content-disposition", "attachment; filename=MonitorDetailsList.xls");
                //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Backlog.xlsx"));

                // Set excel as the HTTP MIME type
                Response.ContentType = "application/vnd.xls";

                // Create an instance of stringWriter for writing information to a string
                System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                // Create an instance of HtmlTextWriter class for writing markup 
                // characters and text to an ASP.NET server control output stream
                HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

                // Set white color as the background color for gridview header row
                GridView1.HeaderRow.Style.Add("background-color", "White");

                // Set background color of each cell of GridView1 header row
                foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
                {
                    tableCell.Style["background-color"] = "White";
                }

                // Set background color of each cell of each data row of GridView1
                foreach (GridViewRow gridViewRow in GridView1.Rows)
                {
                    gridViewRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "White";
                    }
                }

                GridView1.RenderControl(htw);
                Response.Write(stringWriter.ToString());
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}