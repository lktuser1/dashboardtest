using System;       
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using EDS.Intranet.Common;
using EDS.Intranet.Base;

namespace EDSIntranet
{
    public partial class DashboardAlerts : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "Alerts";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
            this.Title = "test";
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
                return "getLatestAlertData() - Error -  " + ex.Message;
            }
        }
    }
}