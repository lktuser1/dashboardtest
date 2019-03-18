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
using System.Drawing;


namespace Dashboard
{
    public partial class DashboardDetails : PageBase
    {
        protected string application = "SM7 Prod";
        protected string instance = "AMS";

        protected void Page_Load(object sender, EventArgs e)
        {
            SQLHelper sqlHelper = new SQLHelper();
            Master.PageTitle = "Dashboard Details";
            Master.ShowPageSubTitle = true;
            Master.PageSubtitle = "HI THERE";
            Master.ShowPageLastUpdated = false;
            this.Title = "Dashboard Details";

            //string localInstance = "";

            if (Request.QueryString["inst"] != null)
            {
                instance = Request.QueryString["inst"];
                //localInstance = sqlHelper.getInstanceFullName(instance);
            }

            if (Request.QueryString["app"] != null)
            {
                application = Request.QueryString["app"];
            }

            Master.PageSubtitle = "For Application " + application + " and Instance " + instance;
            Response.AppendHeader("Refresh", "30");

            //Response.Write("Page subtitle : " + Master.ShowPageSubTitle + ":" + Master.PageSubtitle);
            //Load data

            
        }

        public string loadData()
        {
            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SQLHelper sqlHelper = new SQLHelper();
            DataSet ds = new DataSet();
            int priorityCount = 0;

            try
            {
                if (instance.Trim().Equals(""))
                {
                    return "Incorrect configuration. Please contact support team.";
                }

                if (application.Trim().Equals(""))
                {
                    return "Incorrect configuration. Please contact support team.";
                }

                ds = sqlHelper.getDashboardDetails(dbConnection, instance, application);

                if (!(ds.Tables.Contains("DATA")) || ds.Tables["DATA"].Rows.Count <= 0)
                {
                    return "No monitor data found.";
                }

                //lets do processing for priority 1 data
                ds.Tables["DATA"].DefaultView.Sort = "Status, Name";
                ds.Tables["DATA"].DefaultView.RowFilter = "Priority = 1";
                DataTable dtPriority1 = ds.Tables["DATA"].DefaultView.ToTable();
                if (dtPriority1.Rows.Count == 0)
                {
                    PanelPriority1.Visible = false;
                }
                else
                {
                    //Lets bind the data now.
                    priorityCount++;
                    this.GridView1.DataSource = dtPriority1; //ds.Tables["DATA"];
                    this.GridView1.DataBind();
                }

                //Processing for priority 2 data
                ds.Tables["DATA"].DefaultView.RowFilter = "Priority = 2";
                DataTable dtPriority2 = ds.Tables["DATA"].DefaultView.ToTable();
                if (dtPriority2.Rows.Count == 0)
                {
                    PanelPriority2.Visible = false;
                }
                else
                {
                    //Lets bind the data now.
                    priorityCount++;
                    this.GridView2.DataSource = dtPriority2; //ds.Tables["DATA"];
                    this.GridView2.DataBind();
                }

                //Processing for priority 3 data
                ds.Tables["DATA"].DefaultView.RowFilter = "Priority = 3";
                DataTable dtPriority3 = ds.Tables["DATA"].DefaultView.ToTable();
                if (dtPriority3.Rows.Count == 0)
                {
                    PanelPriority3.Visible = false;
                }
                else
                {
                    //Lets bind the data now.
                    priorityCount++;
                    this.GridView3.DataSource = dtPriority3; //ds.Tables["DATA"];
                    this.GridView3.DataBind();
                }

                //If priority count is 1 then get rid of scroll bar and show all monitor.
                if (priorityCount == 1)
                {
                    if (dtPriority1.Rows.Count > 0)
                    {
                        PanelPriority1.ScrollBars = ScrollBars.None;
                        PanelPriority1.Height = Unit.Percentage(100);
                    }
                    else if(dtPriority2.Rows.Count > 0)
                    {
                        PanelPriority2.ScrollBars = ScrollBars.None;
                        PanelPriority2.Height = Unit.Percentage(100);
                    }
                    else if (dtPriority3.Rows.Count > 0)
                    {
                        PanelPriority3.ScrollBars = ScrollBars.None;
                        PanelPriority3.Height = Unit.Percentage(100);
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "Error : While retrieving data. Message : " + ex.Message;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[2].Text.Equals("1"))
            {
                //e.Row.BackColor = Color.Red;
                e.Row.BackColor = Color.Black;
                e.Row.ForeColor = Color.Red;
                e.Row.Font.Bold = true;
            }
            if (e.Row.Cells[2].Text.Equals("2"))
            {
                //e.Row.BackColor = Color.Yellow;
                e.Row.BackColor = Color.Black;
                e.Row.ForeColor = Color.Yellow;
                e.Row.Font.Bold = true;
            }

            //Lets check for yellow
            if (e.Row.Cells[6].Text.Equals("1"))
            {
                e.Row.Cells[5].BackColor = Color.Violet;
                e.Row.Cells[5].ForeColor = Color.Black;
                e.Row.Cells[5].Font.Bold = false;

            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Logger.writeDebug("command " + e.CommandName);

                // Add code here to add the item to the shopping cart.
            

        }
        protected void GridView2_RowCommand(object sender,  GridViewCommandEventArgs e)
        {
            
            
            Logger.writeDebug("command " + e.CommandName);

            if (e.CommandName == "Page") return;

            if (e.CommandName == "Select")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.

               
                int index = Convert.ToInt32(e.CommandArgument);

                

                Logger.writeDebug("index " + index);

                

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView2.Rows[index];
                

                // Get the last name of the selected author from the appropriate
                // cell in the GridView control.
                GridViewRow selectedRow = GridView2.Rows[index];
                TableCell contactName = selectedRow.Cells[1];
                string contact = contactName.Text;

                // Display the selected value.
                //Label1.Text = "detail " + contact + ".";


                // Add code here to add the item to the shopping cart.
            }

        }
        public string GetUrl(object monid, object messagedetailsID)
        {
            //here you can do validation e.g. if companyname is not null or something 
            //Also you can do some customization based on your logged-in user 
            //You can get the Page location dynamically from say web.config

            string url =
           "~/Monitors/MonitorDetails.aspx?monid=" + monid.ToString() +  "&messagedetailsID=" + messagedetailsID.ToString() +  "&app=" + Server.UrlEncode(application) +  "&inst=" +  Server.UrlEncode(instance); 

            return url;
        }

       
        
    }
}
