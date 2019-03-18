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
using System.Drawing;
using System.Globalization;
using System.Data.SqlClient;

using EDS.Intranet.Base;
using EDS.Intranet.Common;
using Dashboard.Reports;
using Dashboard.Monitors;


namespace Dashboard
{
    public partial class DashboardReports : PageBase
    {

       // public static int action ;
        private string instance = "";
        private string application = "";
        private string priority = "";
        private string monitor = "";
        private string monitortype = "";
        

        private string fromDate = DateTime.Now.AddDays(-180).ToString("yyyy-MM-dd HH:mm:ss");
        private string toDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

      
        public string SortDireaction
        {
            get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        private string _sortDirection;

        protected void Page_Init(object sender, EventArgs e)
        {
            
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLHelper sqlHelper = new SQLHelper();
            
            Master.PageTitle = "Historical Reporting";
            Master.ShowPageSubTitle = false;
            //Master.PageSubtitle = "Reports";
            Master.ShowPageLastUpdated = false;
            this.Title = "Dashboard Reporting";

           string sqlApp = " select distinct application from DB_monitor where active = 1 ";
    
            if (!IsPostBack)
            {

                string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(dbConnection))
                {

                    con.Open();
                    SqlCommand cmdApp = new SqlCommand(sqlApp, con);
                    SqlDataReader rdrApp = cmdApp.ExecuteReader();
                    ddlApp.DataSource = rdrApp;
                    ddlApp.DataBind();
                    con.Close();
  
                    ListItem liAll = new ListItem("-Select-", "-1");
                    ddlApp.Items.Insert(0, liAll);
                    ddlInstance.Items.Insert(0, liAll);
                    ddlPriority.Items.Insert(0, liAll);
                    ddlMonitorType.Items.Insert(0, liAll);
                    ddlMonitor.Items.Insert(0, liAll);

                  //  MonitorType monitorType = new MonitorType();
                  //  List<MonitorType> MonitorTypeList = new List<MonitorType>();
                  //  MonitorTypeList.Add(new MonitorType() { Id = 1, Name = "John" });
                  //  ddlMonitorType.DataSource = MonitorTypeList;
                  //  ddlMonitorType.DataTextField = "Name";
                  //  ddlMonitorType.DataValueField = "Id";
                  //  ddlMonitorType.DataBind();

                  

                    this.LabelReqApp.Visible = true;
                    this.LabelReqInstance.Visible = true;
                    this.LabelReqPriority.Visible = true;
                    this.LabelReqMonitorType.Visible = true;
                    this.LabelReqMonitor.Visible = false;

                    this.ddlApp.Enabled = true;
                    this.ddlInstance.Enabled = true;
                    this.ddlPriority.Enabled = true;
                    this.ddlMonitorType.Enabled = true;
                    this.ddlMonitor.Enabled = false;


                    Calendar1.Visible = false;
                    Calendar2.Visible = false;

                    TextBox1.Text = fromDate;
                    TextBox2.Text = toDate;

                   
                

                    //searchData((int)ViewState["rptaction"], "");

                   // ViewState["rptaction"] = 3;
                   // searchData((int)ViewState["rptaction"], "");
                        //AddLinkButton();
                    /*
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            AddLinkButtonRow(row);
                        }
                    */
                }
            }

        }
       

        protected void ddlApp_SelectedIndexChanged(object sender, EventArgs e)
        {



            string sql = " SELECT distinct instance FROM DB_monitor where active = 1 ";
            if (ddlApp.SelectedIndex > 0)
            {
                string sqlWhere = " and application = " + "'" + ddlApp.SelectedValue + "'";
                sql = sql + sqlWhere;
            }

            //Logger.writeDebug("ddlApp_SelectedIndexChanged() - " + sql);

            ddlInstance.DataSource = getData(sql);
            ddlInstance.DataBind();

            ListItem liAll = new ListItem("-Select-", "-1");
            ddlInstance.Items.Insert(0, liAll);


            ddlPriority.SelectedIndex = 0;
            ddlMonitor.SelectedIndex = 0;

        }

        protected void ddlInstance_SelectedIndexChanged(object sender, EventArgs e)
        {


            string sql = " select distinct priority from DB_monitor where active = 1 ";
            if (ddlInstance.SelectedIndex > 0)
            {
                string sqlWhere = " and application = " + "'" + ddlApp.SelectedValue + "'" + " and instance = " + "'" + ddlInstance.SelectedValue + "'";
                sql = sql + sqlWhere;
            }
            //Logger.writeDebug("ddlInstance_SelectedIndexChanged() - " + sql);

            ddlPriority.DataSource = getData(sql);
            ddlPriority.DataBind();

            ListItem liAll = new ListItem("-Select", "-1");
            ddlPriority.Items.Insert(0, liAll);

            ddlPriority.SelectedIndex = 0;
            ddlMonitor.SelectedIndex = 0;

        }
        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = " SELECT id, instance, application, name,description, priority, freq FROM DB_monitor where active = 1 ";
            if (ddlPriority.SelectedIndex > 0)
            {
                string sqlWhere = " and application = " + "'" + ddlApp.SelectedValue + "'" + " and instance = " + "'" + ddlInstance.SelectedValue + "'" + " and priority = " + "'" + ddlPriority.SelectedValue + "'";
                sql = sql + sqlWhere;
            }
            //Logger.writeDebug("ddlPriority_SelectedIndexChanged() - " + sql);

            ddlMonitor.DataSource = getData(sql);
            ddlMonitor.DataBind();

            ListItem liAll = new ListItem("-Select-", "-1");
            ddlMonitor.Items.Insert(0, liAll);



            string monitortypeother = " and name in ('HPSX', 'MS Autoclose - OCMQ', 'MSarchiveeventoutandin', 'MSEvent_CM_RM') ";
            string monitortypescheduleother = " and name in ('Schedule agent', 'Schedule anonymise', 'Schedule change', 'Schedule event', 'Schedule inactive', 'Schedule lister', 'Schedule marquee', 'Schedule ocm', 'Schedule report', 'Schedule suspendhold') ";
                
            ddlMonitorType.Items.Clear();

            if ( ((ddlReports.SelectedValue == "1") || (ddlReports.SelectedValue == "5")) )
            {
                switch (ddlPriority.SelectedValue)
                {

                    case "1":
                        // P1 Monitors
                        ddlMonitorType.Items.Add(new ListItem("Counter", "Counter"));
                        ddlMonitorType.Items.Add(new ListItem("eNote", "eNote"));
                        ddlMonitorType.Items.Add(new ListItem("MSEvent IM", "MSEvent_IM"));
                        ddlMonitorType.Items.Add(new ListItem("MSEvent SD", "MSEvent_SD"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule alert", "Schedule alert"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule linker", "Schedule linker"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule message", "Schedule message"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule problem", "Schedule problem"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule sla", "Schedule sla"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule All", "Schedule"));
                        ddlMonitorType.Items.Add(new ListItem("Work Table RWS", "Work"));
                        break;

                    case "2":
                        //P2 Monitors
                        ddlMonitorType.Items.Add(new ListItem("Counter", "Counter"));
                        ddlMonitorType.Items.Add(new ListItem("MSCDSSync", "MSCDS"));
                        ddlMonitorType.Items.Add(new ListItem("MSEvent LDSS", "MSEvent_LDSS"));
                        ddlMonitorType.Items.Add(new ListItem("MSEvent ESL", "MSEvent_ESL"));
                        ddlMonitorType.Items.Add(new ListItem("MS.Purge", "MS."));
                        ddlMonitorType.Items.Add(new ListItem("Schedule MSbgmonitor", "Schedule MSbg"));
                        ddlMonitorType.Items.Add(new ListItem("Schedule All", "Schedule"));
                        break;
                    case "3":
                        //P3 Monitors
                        ddlMonitorType.Items.Add(new ListItem("SERVLET", " and name like 'SERVLET%'"));

                        break;
                    default:

                        //ddlMonitorType.Items.Add(new ListItem("Other Monitors", monitortypeother));
                        break;

                }


            }
            else
            {
                liAll = new ListItem("-Select-", "-1");
                ddlMonitorType.Items.Insert(0, liAll);
                // Set default selected monitor type
                ddlMonitorType.SelectedIndex = 0;


            }
            liAll = new ListItem("-Select-", "-1");
            ddlMonitorType.Items.Insert(0, liAll);
            // Set default selected monitor type
            ddlMonitorType.SelectedIndex = 0;
                
        }

        public DataSet getData(string sql)
        {

            DataSet ds = new DataSet();
            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            try
            {

                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);
                sqlda.Fill(ds, "DATA");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getData() - ", ex);
                return ds;
            }

        }
       

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
            Calendar1.Visible = false;
        }
       
        
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            
            TextBox2.Text = Calendar2.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
            Calendar2.Visible = false;
        }
      
        protected void Export_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count != 0)
            {
                // Clear all content output from the buffer stream
                Response.ClearContent();

                // Specify the default file name using "content-disposition" RESPONSE header
                Response.AppendHeader("content-disposition", "attachment; filename=Backlog.xls");
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

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            int action ;
            action = Convert.ToInt32(ddlReports.SelectedValue);
            Logger.writeDebug("ddlReports_SelectedIndexChanged() - Report Type - " + action);

            try{


                    switch (action)
                    {
                        // Backlog Counts for selected Monitor Types
                        case 1:
                           
                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.LabelReqMonitorType.Visible = true;
                            this.LabelReqMonitor.Visible = false;
                           

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = true;
                            this.ddlMonitor.Enabled = false;
                        
                            break;
                        // BackLog Counts for all Monitors
                        case 2: 
                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.LabelReqMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = false;
                           

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = false;

                            break;

                        // BackLog Detail and Group By
                        case 3:
                        case 4:
                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.LabelReqMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = true;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = true;

                            break;

                        //BackLog Summary for selected Monitor Type

                        case 5:

                             this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.LabelReqMonitorType.Visible = true;
                            this.LabelReqMonitor.Visible = false;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = true;
                            this.ddlMonitor.Enabled = false;

                            break;

                     // Backlog Summary for all Monitors
                        case 6:
                       

                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.LabelReqMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = false;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = false;

                            break;

                            //Monitors List By Instance
                        case 21:
                             this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = false;
                            this.LabelReqPriority.Visible = false;
                            this.LabelReqMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = false;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = false;
                            this.ddlPriority.Enabled = false;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = false;
                            break;

                            //Monitor Details and Descriptions
                        case 22:
                        case 23:
                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = false;
                            this.LabelReqMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = false;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = false;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = false;

                            break;

                            // Default
                         default:
                            this.LabelReqApp.Visible = true;
                            this.LabelReqInstance.Visible = true;
                            this.LabelReqPriority.Visible = true;
                            this.ddlMonitorType.Visible = false;
                            this.LabelReqMonitor.Visible = false;

                            this.ddlApp.Enabled = true;
                            this.ddlInstance.Enabled = true;
                            this.ddlPriority.Enabled = true;
                            this.ddlMonitorType.Enabled = false;
                            this.ddlMonitor.Enabled = false;

                            break;

                
                        }
          
                       
                    }
            catch (Exception ex)
            {
                Logger.writeDebug("ddlReports_SelectedIndexChanged() - " + ex.Message);
               
            }

            

        }
       
        protected void txtSearch_Click(object sender, EventArgs e)
        {
            

            searchData(-1, "");
        }

      
        
        public string searchData(int raction, string sort)
        {
            double headerheight;
            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            ReportsDataAccess reportsData = new ReportsDataAccess();
            MonitorDataAccess monitorData = new MonitorDataAccess();
            ReportsHelper reportsHelper = new ReportsHelper(); //old code, not used
            DataSet ds = new DataSet();
            DataTable dataTable;
           
            try
            {

                headerheight = 30;
                application = ddlApp.SelectedValue;
                instance = ddlInstance.SelectedValue;
                priority = ddlPriority.SelectedValue;
                monitor = ddlMonitor.SelectedValue;
                monitortype = ddlMonitorType.SelectedValue;
                fromDate = TextBox1.Text;
                toDate = TextBox2.Text;

                if (application.Trim().Equals(""))
                {
                    return "Please select application ";
                }
                if (instance.Trim().Equals(""))
                {
                    return "Please select instance. ";
                }
                //Logger.writeDebug("Report-Index - " + ddlReports.SelectedIndex);
               // Logger.writeDebug("Report-Value - " + ddlReports.SelectedValue);
               // Logger.writeDebug("Report-Item - " + ddlReports.SelectedItem);

                //Logger.writeDebug("SearchData() - Report Type initial - " + raction);

                if (raction == -1)
                {
                    raction = Convert.ToInt32(ddlReports.SelectedValue);
                    ViewState["rptAction"] = raction;
                }
                else
                {
                    raction = (int)ViewState["rptAction"];

                }

                // Logger.writeDebug("SearchData() - Report Type view - " + ViewState["rptAction"]);

                Logger.writeDebug("SearchData() - Report Type - " + raction);
                Logger.writeDebug("SearchData() - Parameters - " + application + ", " + instance + ", " + priority + ", " + monitortype + ", " +  monitor + ", " + fromDate + ", " + toDate);

                // Clear all rows of each table in dataset and Initialize Grid before getting any data
                ds.Clear();
                this.GridView1.DataSource = null;
                this.GridView1.DataBind();

                //Check Date Time format
                DateTime Test;
                if (DateTime.TryParseExact(fromDate, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out Test) != true)
                {
                    this.Label1.Text = "From Date format should be yyyy-MM-dd HH:mm:ss";
                    return "";
                }
                else if (DateTime.TryParseExact(toDate, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out Test) != true)
                {
                    this.Label1.Text = "To Date format should be yyyy-MM-dd HH:mm:ss";
                    return "";
                }
                
                switch (raction)
                {
                    // Backlog Counts for selected Monitor Type
                    case 1:
                        if ((application == "-1") || (instance == "-1") || (priority == "-1") || (monitortype == "-1"))

                            this.Label1.Text = "Please select all required information - Application, Instance, Priority and Monitor Type.";
                        else
                        {
                            this.Label1.Text = "";
                            ds = reportsData.getBackLogCounts(dbConnection, application, instance, priority, monitortype, fromDate, toDate);
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                           // this.GridView1.DataBind();

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;

                           // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 100 ,false); </script>", false);
                        }
                        break;

                    // BackLog Counts for all Monitors
                    case 2:

                        if ((application == "-1") || (instance == "-1") || (priority == "-1") )

                            this.Label1.Text = "Please select all required information - Application, Instance, and Priority.";
                        else
                        {

                            this.Label1.Text = "";
                            ds = reportsData.getBackLogCounts(dbConnection, application, instance, priority, "", fromDate, toDate);

                            
                            //this.GridView1.DataSource = null;
                            //this.GridView1.DataBind();
                            //this.GridView1.AllowPaging = false;
                            //this.GridView1.PageSize = 4;

                           

                            
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            //this.GridView1.DataBind();

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;

                            
                    
                               
                           // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 40 ,false); </script>", false);
                        }
                        break;

                    // BackLog Detail as created in DB
                    case 3:

                        if ((application == "-1") || (instance == "-1") || (priority == "-1") || (monitor == "-1"))
                        {
                            this.Label1.Text = "Please select all required information - Application, Instance, Priority and Monitor.";
                           
                        }
                        else
                        {
                            this.Label1.Text = "";
                            ds = reportsData.getBacklogDetail(dbConnection, application, instance, priority, monitor, fromDate, toDate);

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;
                        }

                        break;
                    
                    // BackLog Detail Grouped By Hour
                    case 4:

                        if ((application == "-1") || (instance == "-1") || (priority == "-1") || (monitor == "-1"))

                            this.Label1.Text = "Please select all required information - Application, Instance, Priority and Monitor.";
                        else
                        {
                            this.Label1.Text = "";
                            ds = reportsData.getBacklogGroupByHr(dbConnection, application, instance, priority, monitor, fromDate, toDate);
                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;
                        }
                        break;

                    // BackLog Summary for selected Monitor Types

                    case 5:
                        if ((application == "-1") || (instance == "-1") || (priority == "-1") || (monitortype == "-1"))

                            this.Label1.Text = "Please select all required information - Application, Instance, Priority and MonitorType.";
                        else
                        {
                            this.Label1.Text = "";
                            ds = reportsData.getBackLogCrosstab(dbConnection, application, instance, priority, monitortype, fromDate, toDate);
                            //this.GridView1.DataSource = MergeRows(ds.Tables["DATA"].DefaultView.ToTable());
                            //this.GridView1.DataBind();

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = MergeRows(dataTable); 
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;

                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 40 ,false); </script>", false);
                     
                        }


                        break;

                    // BackLog Summary for All Monitors
                    case 6:
                        if ((application == "-1") || (instance == "-1") || (priority == "-1"))

                            this.Label1.Text = "Please select all required information - Application, Instance and Priority.";
                        else
                        {
                            this.Label1.Text = "";
                            ds = reportsData.getBackLogCrosstab(dbConnection, application, instance, priority, "", fromDate, toDate);
                            //this.GridView1.AllowPaging = true;
                            //this.GridView1.DataSource = MergeRows(ds.Tables["DATA"].DefaultView.ToTable());
                            //this.GridView1.DataBind();

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = MergeRows(dataTable); 
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            //headerheight = this.GridView1.HeaderRow.Height.Value;
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;
                           
                            //Logger.writeDebug("headerheight=" + headerheight);

                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 100 ,false); </script>", false);
                        }
                        break;

                    

                    // Monitor List By Instance

                    case 21:
                        if ((application == "-1") )

                            this.Label1.Text = "Please select all required information - Application and Instance.";
                        else
                        {
                            ds = monitorData.getMonitorListByInstance(dbConnection, application, instance, priority, monitor, fromDate, toDate);
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            //this.GridView1.DataBind();

                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;

                        }
                        break;
                    // Monitor List
                    case 22:
                        if ((application == "-1") || (instance == "-1"))

                            this.Label1.Text = "Please select all required information - Application and Instance.";
                        else
                        {
                            
                            
                            GridView1.DataSource = MonitorDataAccessLayer.GetMonitorsList("priority", dbConnection, application, instance, priority, monitor);
                            GridView1.DataBind();

                           // ds.Tables["DATA"].DefaultView.Sort = sort;
                           // dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                           // this.GridView1.DataSource = dataTable;
                            //this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                            //this.GridView1.DataBind();
                           // this.GridView1.AutoGenerateColumns = true;
                        }
                        break;
                    // Monitor Details List
                    case 23:
                        if ((application == "-1") || (instance == "-1"))

                            this.Label1.Text = "Please select all required information - Application and Instance.";
                        else
                        {
                             
                            ds = monitorData.getMonitorDetailsList("id", dbConnection, application, instance, priority, monitor);
                          
                           
                            ds.Tables["DATA"].DefaultView.Sort = sort;
                            dataTable = ds.Tables["DATA"].DefaultView.ToTable();
                            this.GridView1.DataSource = dataTable;
                            this.GridView1.DataBind();
                            this.GridView1.AutoGenerateColumns = true;


                             

                            
                            // Add hyperlink to gridview
                            // Adds to all grids for all reports. 
                            // So we should not do it here, but in row data bound event and select particular report type
                            //HyperLinkField HLF = new HyperLinkField();
                            //HLF.Text =  "[View Report]";
                           // HLF.HeaderText = "[View Report]";
                            //HLF.Visible = true;
                           // this.GridView1.Columns.Add(HLF);


                            this.GridView1.DataBind();


                            //PlaceHolder1.Controls.Add(gv);
                        }
                        break;
                       
                     //====================== Old Code Not used =========================================================================================

                    case 31:
                        ds = reportsHelper.getBacklogHistoryCounts(dbConnection, application, instance, priority, monitor, fromDate, toDate);
                        //DataTable dt = ds.Tables["DATA"].DefaultView.ToTable();
                        this.GridView1.DataSource = ds.Tables["DATA"].DefaultView.ToTable();
                        this.GridView1.DataBind();
                        break;

                    // Alerts - Pivot Data
                    case 32:
                        GridView1.DataSource = SAAlertDataAccessLayer.GetAllAlerts("creationdate");
                        GridView1.DataBind();
                        break;
                    // Alerts - Pivot Data
                    case 33:
                        GridView1.DataSource = SAAlertDataAccessLayer.GetAllAlertsSM("creationdate");
                        GridView1.DataBind();
                        break;
                    //Transpose data
                    case 34:
                        ds = reportsHelper.getBacklogHistory(dbConnection, application, instance, priority, monitor, fromDate, toDate);
                        DataTable dt = ds.Tables["DATA"];
                        this.GridView1.DataSource = dt;
                        this.GridView1.DataBind();

                        //code for converting rows to columns and columns to rows - transpose

                        //Create a new data table to store transpose results
                        DataTable dtpivot = new DataTable();

                        //Create columns for the new datatable dm
                        //equal to the number of rows in the original table

                        for (int i = 0; i <= dt.Rows.Count; i++)
                        {
                            dtpivot.Columns.Add(i.ToString());
                            Logger.writeDebug("Add columns - " + i.ToString());
                        }

                        //For each column in original table, create a new row
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {

                            //create a new row in new datatable 
                            DataRow dr = dtpivot.NewRow();

                            //first column is header. take from first row of original data table
                            dr[0] = dt.Columns[i].ToString();
                            //Logger.writeDebug("dr[0] - " + dr[0]);


                            //for each row in original table
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                //add it as a column
                                dr[j + 1] = dt.Rows[j][i];
                                //Logger.writeDebug("dr[j + 1] - " + i + "," + j + dt.Rows[j][i]);
                            }
                            dtpivot.Rows.Add(dr);
                        }

                        this.GridView1.DataSource = dtpivot;
                        this.GridView1.ShowHeader = true;
                        this.GridView1.DataBind();
                        break;

                    case 35:
                        GridView1.DataSource = MonitorDataAccessLayer.GetAllMonitors("id", dbConnection, application, instance, priority, monitor);
                        GridView1.DataBind();
                        break;

                    default:
                     
                        break;

                
                }

                switch (raction)
                {
                    case 2:
                        if ((application.Trim().Equals("SM9 Prod")) & (priority.Trim().Equals("2")))
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 46 ,false); </script>", false);
                        else
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 30 ,false); </script>", false);
                        break;
                    case 6:
                        if ((application.Trim().Equals("SM9 Prod")) || (application.Trim().Equals("SA")))
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 43 ,false); </script>", false);
                        else
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 30 ,false); </script>", false);
                 
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + this.GridView1.ClientID + "', 1300, 450 , 30 ,false); </script>", false);
                        break;

                }
               
                    
                    
                    return "";
            }
            catch (Exception ex)
            {
                Logger.writeDebug("searchData () - " + ex.Message);
                return "Error : While searching data. Message : " + ex.Message;
            }
        }

        
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
           

            //SortDirection sortDirection = SortDirection.Ascending;
            //string sortField = string.Empty;

            //SortGridview((GridView)sender, e, out sortDirection, out sortField);
            //string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            //GridView1.DataSource = MonitorDataAccessLayer.GetAllMonitors(e.SortExpression + " " + strSortDirection);
            //GridView1.DataBind();

            SetSortDirection(SortDireaction);
  

            searchData(-1, e.SortExpression + " " +_sortDirection);

            SortDireaction = _sortDirection;
        }

        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
            }
            else
            {
                _sortDirection = "ASC";
            }
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            int raction = 0;
              
                   // action = Convert.ToInt32(ddlReports.SelectedValue);

            raction = (int)ViewState["rptAction"];
                 //Logger.writeDebug("Action-Row - " + raction);
              
                switch (raction)
                {
                    case 3:
              
                        int status = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Status"));
                        if (status == 1)
                        {
                            e.Row.Cells[4].BackColor = Color.Red;
                            e.Row.Cells[4].ForeColor = Color.White;
                        }
                        if (status == 2)
                        {
                            e.Row.Cells[4].BackColor = Color.Yellow;
                            e.Row.Cells[4].ForeColor = Color.Black;
                        }
                        if (status == 3)
                        {
                            e.Row.Cells[4].BackColor = Color.White;
                            e.Row.Cells[4].ForeColor = Color.Black;

                        }
                        break;
                    case 4:
                        string message = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Backlog"));
                        string msg  = ParseMessage(message);
                         
                          //Logger.writeDebug("message - " + message);
                          //Logger.writeDebug("msg - " + msg);
                          //int Backlogmin = Convert.ToInt32(msg.Trim());
                          //Logger.writeDebug("Backlogmin - " + Backlogmin);
                          break;
                    case 1:
                    case 2:
                          //AddLinkButton();
                          if (e.Row.RowType == DataControlRowType.DataRow)
                          {
                              //Hyperlink
                              //HyperLink link = new HyperLink();
                              //link.Text = "hyperlink";
                              
                              //e.Row.Cells[0].Controls.Add(link);
                              ////link.NavigateUrl = "Navigate somewhere based on data: " + e.Row.DataItem;
                              //link.NavigateUrl = "~/DashboardDetails.aspx";
                              //e.Row.Cells[0].Controls.Add(link);


                              ////HyperLink DetailsLink = e.Row.FindControl("GridView1") as HyperLink;
                             //// DetailsLink.NavigateUrl = "Main.aspx";

                              //Button
                             // Button b= new Button();
                             // b.ID = "b1";
                             // b.Text = e.Row.Cells[1].Text;
                             // b.Click += new EventHandler(b_Click);
                             // e.Row.Cells[1].Controls.Add(b);
                             // //b.Attributes.Add("OnClick", "b_Click()");


                              //Link Button
                             // correct AddLinkButtonRow(e.Row);
                            
                              //if (e.Row.RowType == DataControlRowType.DataRow)
                              //{
                                  // CREATE A LinkButton AND IT TO EACH ROW.
                                 // LinkButton lb = new LinkButton();
                                  //lb.ID = "lb1";
                                 // lb.Text = "link";  //e.Row.Cells[0].Text;
                                 //lb.CommandName = "link";
                                // lb.Command += LinkButton_Command; 
                                //  e.Row.Cells[0].Controls.Add(lb);

                                  
                            //  }


                              // button Field
                             // ButtonField btnfld = new ButtonField();
                             // btnfld.ButtonType = ButtonType.Button;
                             // btnfld.Text = e.Row.Cells[0].Text;
                             // btnfld.CommandName = e.Row.Cells[0].Text;
                              //btnfld.CausesValidation = false;
                             //GridView1.Columns.Add(btnfld);

                                /*if (e.Row.DataItem != null)
                                {
                                    LinkButton lb = new LinkButton();
                                    lb.CommandArgument = e.Row.Cells[1].Text;
                                    lb.CommandName = "NumClick";
                                    lb.Text = e.Row.Cells[0].Text;
                                    e.Row.Cells[1].Controls.Add((Control)lb);
                                } 
                                */

                             

                              
                              
                          }
                          break;
                    default:
                        //Logger.writeDebug("Action-Default - " +  action );
                        break;

                    }


        }
        private void AddLinkButtonRow(GridViewRow row)
        {
            TableCell cellWithLink = new TableCell();
            LinkButton lnk = new LinkButton();
            lnk.Command += new CommandEventHandler(Detail);

            if (row.RowType == DataControlRowType.DataRow)
            {
                //String.Empty Then 
                //if (row.Cells[0].Text != "5")
                //{
                    lnk.Text = "detail";
                    lnk.CommandName = "detail";
                    cellWithLink.Controls.Add(lnk);
                    row.Cells.Add(cellWithLink);
               // }
            }
        }
        protected void Detail(object sender, CommandEventArgs e)
        {

            ViewState["rptAction"] = 3;
            Logger.writeDebug("Detail() - Report Type view - " + ViewState["rptAction"]);
            //searchData(3, "");
             if (e.CommandName == "detail")
             {

                 ViewState["rptAction"] = 3;
            LinkButton lb = (LinkButton)sender;
            lb.Text = "OK";
             }
            //Response.Redirect("NetChart.aspx");
        }
        /// <summary>
        /// Add a LinkButton To GridView Row.
        /// </summary>
        private void AddLinkButton()
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lb = new LinkButton();
                    lb.Text = "link";
                    lb.CommandName = "link";
                    lb.Command += LinkButton_Command;
                    row.Cells[0].Controls.Add(lb);
                }
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            AddLinkButton();
        }
        protected void GridView1_RowCommand(object sender, CommandEventArgs e)
        {

            searchData(3,"");
            switch (e.CommandName.ToLower())
            {
                case "link":
                    //string url = GetUrl(e.CommandArgument.ToString());
                    //Response.Redirect(url);
                    break;
                default:
                    break;
            }
        }

       protected void b_Click(object sender, EventArgs e) 
       {
           searchData(3, "");
       }
       protected void LinkButton_Command(object sender, CommandEventArgs e)
       {
           ViewState["rptaction"] = 3;
           searchData((int)ViewState["rptaction"], "");
          // if (e.CommandName == "link")
          // {
         //correct      LinkButton lb = (LinkButton)sender;
         //correct      lb.Text = "OK";
          // }
       }
       

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            searchData(1, "");
        }
        public string ParseMessage(string message)
        {
            //string message = dataReader["message"].ToString();
            string backlog = "";
            string backlogmin = "";
            string records = "";
            int startsearch = 0;

            if (string.IsNullOrEmpty(message) || string.Equals(message, "OK"))
            {
                backlogmin = message;
                records = "";
                backlog = backlogmin;
            }
            else
            {
                backlogmin = message.Substring(0, message.IndexOf(" "));
                if (message.IndexOf(",") >= 0)
                {
                    startsearch = message.IndexOf(",");
                    records = message.Substring(startsearch + 1, message.IndexOf(" ", startsearch + 2) - startsearch);
                    backlog = backlogmin + " : " + records;
                }
                else
                {
                    backlog = backlogmin;
                }

            }
            //Logger.writeDebug("Parse message - " + message);
            return backlogmin;
        }


        public DataTable MergeRows(DataTable dTable)
        {

            
                //Initialize all variables
                string backlogdate = "";
                string cell = string.Empty;
                string match = "";
                string from = "";
                string to = "";
                TimeSpan span;
                string spanstr;
                int r = 0;
                int c = 0;
                DateTime frdate;
                DateTime todate;
                int comparedates;
                int pair = 0;

                //Create a new data table to store merged results
                DataTable dtmerge = new DataTable();

                try
                {

                // Add header row with same column names as dataset returned from query.
                foreach (DataColumn col in dTable.Columns)
                {
                    dtmerge.Columns.Add(col.ColumnName);
                }


                // For each row in data table
                foreach (DataRow row in dTable.Rows)
                {


                    //if backlog date of current row matches that of previous row
                    if (row["BackLogDate"].ToString() == backlogdate)
                    {
                        pair = 2;
                        // Initialize column no.
                        c = 0;

                        //create a new row in new datatable 
                        DataRow dr = dtmerge.NewRow();

                        //Logger.writeDebug("Loop through all columns of this row " + r);
                        foreach (DataColumn col in dTable.Columns)
                        {

                            object cellData = row[col];
                            //Logger.writeDebug("row - " + r.ToString() + ", col - " + col + " Value - [" + cellData.ToString() + "]");
                            //Logger.writeDebug("prev - " + backlogdate + "next - " + row["BackLogDate"].ToString());

                            dr[c] = row[col];

                            if (c == 2)
                            {
                                dr[c] = Convert.ToDateTime(row[col]).ToString("ddd, MMM dd, yyyy");
                            }

                            // Ignore application, instance and backlogdate columns
                            if (c >= 3)
                            {
                                //If Backlog alert time exists for this monitor/column
                                if (!string.IsNullOrEmpty(row[col].ToString()))
                                {
                                    //Merge the Min and max times and calculate time difference between the two
                                    span = (DateTime.Parse(row[col].ToString()) - DateTime.Parse(dTable.Rows[r - 1][col].ToString()));
                                    spanstr = span.ToString();
                                    spanstr = spanstr.Substring(0, 5);

                                    from = Convert.ToDateTime(dTable.Rows[r - 1][col]).ToString("HH:mm", CultureInfo.CurrentCulture);
                                    to = Convert.ToDateTime(row[col].ToString()).ToString("HH:mm", CultureInfo.CurrentCulture);

                                    frdate = Convert.ToDateTime(dTable.Rows[r - 1][col]);
                                    todate = Convert.ToDateTime(row[col].ToString());
                                    comparedates = DateTime.Compare(frdate, todate);

                                    match = "Bl " + spanstr +  " Fr " + from  + " To " + to;

                                    //match = comparedates.ToString() + " Bl " + spanstr + Environment.NewLine + " Fr " + from + System.Environment.NewLine + " To " + to;
                                    //match = " Backlog " + span.ToString() + " From - " + dTable.Rows[r - 1][col] + " To - " + row[col].ToString();
                                    //Logger.writeDebug("Match - " + match);
                                    dr[c] = match;


                                }
                                //add it as a column
                                //dr[j + 1] = dt.Rows[j][i];
                                //Logger.writeDebug("dr[j + 1] - " + i + "," + j + dt.Rows[j][i]);
                            }
                            //Logger.writeDebug("dr[" + r + "," + c + "]" + dr[c]);

                            //Increment column number and check next column
                            c++;
                        }
                        //All columns have been checked, Add merged row to new datatable dtmerge
                        dtmerge.Rows.Add(dr);
                    }
                    else
                    {

                        if (pair == 1)
                        {
                            //Logger.writeDebug("BackLogDate-" + backlogdate);

                            // Initialize column no.
                            c = 0;

                            //create a new row in new datatable 
                            DataRow dr = dtmerge.NewRow();

                            //Logger.writeDebug("Loop through all columns of this row " + r);
                            foreach (DataColumn col in dTable.Columns)
                            {

                                object cellData = row[col];
                            

                                dr[c] = dTable.Rows[r - 1][col].ToString();

                                if (c == 2)
                                {
                                    dr[c] = DateTime.Parse(dTable.Rows[r - 1][col].ToString()).ToString("ddd, MMM dd, yyyy");
                                    
                                }

                                // Ignore application, instance and backlogdate columns)
                                if (c >= 3)
                                {
                                    //If Backlog alert time exists for this monitor/column
                                    
                                    if (!string.IsNullOrEmpty(dTable.Rows[r - 1][col].ToString()))
                                    {
                                       //No merge. Just print the row

                                        from = Convert.ToDateTime(dTable.Rows[r - 1][col]).ToString("HH:mm", CultureInfo.CurrentCulture);
                                        to = Convert.ToDateTime(dTable.Rows[r - 1][col]).ToString("HH:mm", CultureInfo.CurrentCulture);
                                        
                                        match = "Bl 00:00 " + "Fr " + from + " To " + to ;

                                         dr[c] = match;


                                    }
                                    
                                }
                               

                                //Increment column number and check next column
                                c++;
                            }
                            //All columns have been checked, Add merged row to new datatable dtmerge
                            dtmerge.Rows.Add(dr);

                        }

                        pair = 1;
                        //No match of backlog date with previous row. Save backlog date for this row to check with next row.
                        backlogdate = row["BackLogDate"].ToString();

                        
                        //backlogtime = row[col].ToString();
                        //Logger.writeDebug("NoMatch " );
                    }

                    //dm.Rows.Add(dr);
                    //Increment row number and check next row.
                    r++;
                }
                //Return datatable with merged rows.
                return dtmerge;
            }
            catch (Exception ex)
            {
                Logger.writeError("MergeRows() - " + "Error ", ex);
                return dtmerge;
            }
        }       
                       
    }
}