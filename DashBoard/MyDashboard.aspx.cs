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

namespace Dashboard
{
    public partial class MyDashboard : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "My Dashboard";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;
            this.Title = "My Dashboard";
            Master.RightPageTitle = "BackLog Alerts";
            Response.AppendHeader("Refresh", "30");
        }

        public string getLatestData()
        {

            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string userID = this.LoginUserID;

            try
            {
                //SQLHelper xx = new SQLHelper();
                SQLHelper sqlHelper = new SQLHelper();
                return sqlHelper.getLatestData(userID, dbConnection,true);

            }
            catch (Exception ex)
            {
                return "Error : While generating the data. Message : " + ex.Message;
            }
        }
        public string getLatestAlertData()
        {

            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string userID = this.LoginUserID; 

            try
            {

                SQLHelper sqlHelper = new SQLHelper();
                return sqlHelper.getLatestAlertData(userID, dbConnection, true);

            }
            catch (Exception ex)
            {
                return "Error : While generating the alert data. Message : " + ex.Message;
            }
        }
    }
}
