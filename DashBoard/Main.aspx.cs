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
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Dashboard
{
    public partial class Main : PageBase
    {
        public string datastring = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "Dashboard";
           
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
            this.Title = "Dashboard";
            Master.RightPageTitle = "BackLog Alerts";
           
            Response.AppendHeader("Refresh","30");
            
        }


        public string getLatestData()
        {

            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string userID = ""; //This is a main page. All data will be retrieved.
            
            try
            {
                
                SQLHelper sqlHelper = new SQLHelper();
                return sqlHelper.getLatestData(userID, dbConnection,false);
                
            }
            catch (Exception ex)
            {
                return "Error : While generating the data. Message : " + ex.Message;
            }
        }

        public string getLatestAlertData()
        {

            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string userID = ""; 

            try
            {
               
                SQLHelper sqlHelper = new SQLHelper();
                return sqlHelper.getLatestAlertData(userID, dbConnection, false);

            }
            catch (Exception ex)
            {
                return "Error : While generating the alert data. Message : " + ex.Message;
            }
        }
        protected void txtAlerts_Click(object sender, EventArgs e)
        {
        

          
        }

    }
}
