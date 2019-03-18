using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace EDS.Intranet.Common
{
    public class SQLHelper
    {

        private string getColorForStatus(int status)
        {
            switch (status)
            {
                case 1:
                    //red
                    return " bgcolor='#FF0000'";

                case 2:
                    //yellow
                    return " bgcolor='#FFFF00'";

                case 3:
                    //Green
                    return "bgcolor='#00FF00'";
        
                case 4:
                    //Green
                    return "bgcolor='#00FF00'";
            }
            return "";
        }
        private string getColorForStatusDetail(int status)
        {
            switch (status)
            {
                case 1:
                    //red
                    return " style='background-color:#FF0000;color:white;' ";

                case 2:
                    //yellow
                    return " bgcolor='#FFFF00'";

                case 3:
                    //Green
                    //return "bgcolor='#00FF00'";
                    //white
                    return "bgcolor='White'";

                case 4:
                    //Green
                    return "bgcolor='#00FF00'";
            }
            return "";
        }

        private string getColorForOld(int status)
        {
            if (status == 1)
            {
                return "bgcolor='FF00FF'";
               // return "";
            }
            else
            {
                return "";
            }
        }

        private string generateRowOnClickURL(string instance, string application)
        {
            //NavToUrl
            return " onclick=\"NavToUrl('DashboardDetails.aspx?inst=" + instance + "&app=" + application + "')\"";
        }


        public string getLatestData(string userID, string dbConnection, bool isCalledFromMyDashboard)
        {

            SqlConnection sqlConnection;
            Dictionary<string, string> rowList = new Dictionary<string, string>();
            string dataToReturn = "";

            //try to connect to database now
            sqlConnection = new SqlConnection(dbConnection);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return "Error : Unable to connect to Database Server. Message : " + ex.Message;
            }

            try
            {
                
                string sp_name = "sp_get_web_data";
                SqlCommand cmd = new SqlCommand(sp_name, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_user_id", SqlDbType.NVarChar, 100));
                cmd.Parameters["@p_user_id"].Value = userID;
                cmd.CommandTimeout = 120;

                SqlDataReader dataReader = cmd.ExecuteReader();

                string appName = "";
                string newAppName = "";
                bool firstTime = true;
                bool pendingAppend = false;

                System.Text.StringBuilder rowbuilder = new System.Text.StringBuilder();

                while (dataReader.Read())
                {

                    newAppName = dataReader["app"].ToString();
                    //newAppName = newAppName.Replace("SM7", "SM9");
                    if (firstTime)
                    {
                        appName = newAppName;
                        firstTime = false;
                    }

                    if (!(newAppName.Equals(appName)))
                    {
                        rowList.Add(appName, rowbuilder.ToString());
                        appName = newAppName;
                        rowbuilder = new System.Text.StringBuilder();
                        pendingAppend = false;
                    }

                    pendingAppend = true;
                    rowbuilder.Append("<tr " + this.generateRowOnClickURL(dataReader["instance"].ToString(),newAppName) + ">" 
                        + "<td width='50px'>" + dataReader["fullname"].ToString() 
                        + "</td><td width='25px' " + getColorForStatus((int)dataReader["priority_1"]) + ">&nbsp;"
                        + "</td><td width='25px' " + getColorForStatus((int)dataReader["priority_2"]) + ">&nbsp;"
                        + "</td><td width='25px' " + getColorForStatus((int)dataReader["priority_3"]) + ">&nbsp;"
                        + "</td><td width='25px' " + getColorForOld((int)dataReader["old"]) + ">&nbsp;" + "</td></tr>");

              

                }

                //If there is pending append then append it.
                if (pendingAppend)
                    rowList.Add(newAppName, rowbuilder.ToString());

                System.Text.StringBuilder tablebuilder = new System.Text.StringBuilder();

                //Time to build the table
                foreach (KeyValuePair<string, string> kvp in rowList)
                {
                    //Add Header
                    tablebuilder.Append("<table width='250' class='tablecontainer'");
                    tablebuilder.Append("<tr><th class='center'  colspan='5'>" + kvp.Key + "</th></tr>");
                    tablebuilder.Append("<tr bgcolor='#808080'><th width='50px'>Instance</th><th width='25px'>P1"
                        + "</th><th width='25px'>P2"
                        + "</th><th width='25px'>P3"
                        + "</th><th width='25px'>Remark" + "</th></tr>");

                    tablebuilder.Append(kvp.Value);
                    tablebuilder.Append("</table>");
                }

                //end the table

                sqlConnection.Close();
                
                dataToReturn = tablebuilder.ToString();
                if(dataToReturn.Trim().Equals(""))
                {
                    if(isCalledFromMyDashboard)
                    {
                        return "No Monitor data found. Please check your setting on preferences page.";
                    }
                    else
                    {
                        return "No Monitor data found";
                    }
                }

                return dataToReturn;
            }
            catch (Exception ex)
            {
                return "Error : While generating the data. Message : " + ex.Message;
            }
        }
        public string getLatestAlertData(string userID, string dbConnection, bool isCalledFromMyDashboard)
        {

            SqlConnection sqlConnection;
            Dictionary<string, string> rowList = new Dictionary<string, string>();
            string dataToReturn = "";

            //Logger.writeDebug("getLatestAlertData() - " + "Param - " + userID + ", " + isCalledFromMyDashboard); 

            //try to connect to database now
            sqlConnection = new SqlConnection(dbConnection);

            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                return "getLatestAlertData() - Error - " + ex.Message;
            }
            
            try
            {

                string sp_name = "sp_get_alert_data";
                SqlCommand cmd = new SqlCommand(sp_name, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_user_id", SqlDbType.NVarChar, 100));
                cmd.Parameters["@p_user_id"].Value = userID;
                cmd.CommandTimeout = 120;

                SqlDataReader dataReader = cmd.ExecuteReader();
                

                string appName = "";
                string newAppName = "";
                bool firstTime = true;
                bool pendingAppend = false;

                System.Text.StringBuilder rowbuilder = new System.Text.StringBuilder();

                while (dataReader.Read())
                {

                    newAppName = dataReader["application"].ToString();
                   // newAppName = newAppName.Replace("SM7", "SM9");
                    if (firstTime)
                    {
                        appName = newAppName;
                        firstTime = false;
                    }

                    if (!(newAppName.Equals(appName)))
                    {
                        rowList.Add(appName, rowbuilder.ToString());
                        appName = newAppName;
                        rowbuilder = new System.Text.StringBuilder();
                        pendingAppend = false;
                    }
                     
                    pendingAppend = true;

                    string message = dataReader["message"].ToString();
                    string backlog = "";
                    string backlogmin = "";
                    string records = "";
                    int startsearch = 0;
                    //int endsearch = 0;

                    if (string.IsNullOrEmpty(message) || string.Equals(message, "OK") )
                    {
                        backlogmin = message;
                        records = "";
                        backlog = backlogmin;
                    }
                    else
                    {
                        backlogmin = message.Substring(0, message.IndexOf(" ") );
                        if (message.IndexOf(",") >= 0)
                        {
                            startsearch = message.IndexOf(",");
                            if( message.IndexOf(" ",startsearch + 2) >= 0)
                                 records = message.Substring(startsearch + 1, message.IndexOf(" ", startsearch + 2) - startsearch);
                            else
                                records = "0";
                            backlog = backlogmin + " : " + records;
                        }
                        else
                        {
                            backlog = backlogmin;
                        }
                    }
                    
                 
                   // rowbuilder.Append("<tr bgcolor ='white' " + this.generateRowOnClickURL(dataReader["instance"].ToString(), newAppName) + ">"
                  //      + "<td width='25px' " + getColorForStatusDetail((int)dataReader["Status"]) + ">&nbsp;</td>"
                  //      + "<td width='25px'>" + dataReader["instance"].ToString() + "</td>"
                  //      + "<td width='100px' nowrap >P" + dataReader["Priority"] + " : "  +  dataReader["name"].ToString() + "</td>"
                  //      + "<td width='25px' >" +  backlogmin  +  "</td>"
                  //      + "<td width='25px' >" +  records +  "</td>"
                  //      + "</tr>");
                    //+ ">P" + dataReader["Priority"]

                    rowbuilder.Append("<tr bgcolor ='white' " + this.generateRowOnClickURL(dataReader["instance"].ToString(), newAppName) + ">"
                        + "<td width='20px' " + getColorForStatusDetail((int)dataReader["Status"])  + "</td>"
                        + "<td width='20px'>P" + dataReader["priority"].ToString() + "</td>"
                        + "<td width='30px'>" + dataReader["instance"].ToString() + "</td>"
                        + "<td width='120px' nowrap >" + dataReader["name"].ToString() + "</td>"
                        + "<td width='20px' >" + backlogmin + "</td>"
                        + "<td width='20px' >" + records + "</td>"
                        + "</tr>");

                }

                //If there is pending append then append it.
                if (pendingAppend)
                    rowList.Add(newAppName, rowbuilder.ToString());

                

                System.Text.StringBuilder tablebuilder = new System.Text.StringBuilder();

               
               
                //Time to build the table
                foreach (KeyValuePair<string, string> kvp in rowList)
                {
                    //Start new table for new application
                    tablebuilder.Append("<div style='max-height:220px; width:400px;overflow-x:auto; overflow-y:auto;'>");
                    tablebuilder.Append("<table width='300px' class='tablecontainer' bgcolor='White'");
                    tablebuilder.Append("<tr><th class='center'  colspan='4'>" + kvp.Key + "</th></tr>");
                    
                    //table heading row
                     tablebuilder.Append("<tr bgcolor='#808080'>"
                        + "<th width='20px'>Status</th>"
                        + "<th width='20px'>Priority</th>"
                        + "<th width='30px'>Instance</th>"
                        + "<th width='120px' nowrap>Monitor</th>"
                        + "<th width='20px'>Backlog</th>"
                        + "<th width='20px'>Records</th>"
                        + "</tr>");
                     

                    //All table rows
                    tablebuilder.Append(kvp.Value);

                    //end of table tag
                    tablebuilder.Append("</table>");
                    tablebuilder.Append("</div><BR/>");
                }

                //end the table
                

                sqlConnection.Close();

                dataToReturn = tablebuilder.ToString();
                //Logger.writeDebug("getLatestAlertData() - " + "Data - " + dataToReturn);

                if (dataToReturn.Trim().Equals(""))
                {
                   // if (isCalledFromMyDashboard)
                   // {

                     
                   // }
                  //  else
                  //  {
                        
                        tablebuilder.Append("<div style=width:400px;>");
                        tablebuilder.Append("<table width='300px'  class='tablecontainer' bgcolor='White'>");
                        tablebuilder.Append("<tr><th class='center'  colspan='4'>&nbsp;</th></tr>");
                        tablebuilder.Append("<tr><th>&nbsp;</th><th>No Backlog Alerts at this time&nbsp;</th></tr>");
                       // tablebuilder.Append("<tr><td width='20px' bgcolor='#00FF00'>&nbsp;</td><td></td></tr>");
                        tablebuilder.Append("</table>");
                        tablebuilder.Append("</div>");
                        dataToReturn = tablebuilder.ToString();
                       // return "No Backlog Alerts at this time";
                   // }
                }

                return dataToReturn;
            }
            catch (Exception ex)
            {
                return "getLatestAlertData() - Error - " + ex.Message;
            }
        }

        public DataSet getDashboardDetails(string dbConnection, string instance, string application)
        {
            string sql = "select monID, Name, Description, Status, Priority, Message, Creationtime, Isold, messagedetailsID from view_latest " +
                         "where instance = '" + instance + "' and application = '" + application +                          
                         "' order by  status, isold DESC, priority";

            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);
                sqlda.SelectCommand.CommandTimeout = 120;
                sqlda.Fill(ds, "DATA");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getDashboardDetails() - Error - ", ex);
                return ds;
            }

        }
        public DataSet getDashboardalerts(string dbConnection, string instance, string application)
        {
            string sql = "select application, instance, Name, Status, Priority, Message from view_latest " +
                            "where status <> 4 order by  application, instance,  priority, status ";
                             
            Logger.writeDebug("getDashboardAlerts() - " + sql);

            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);
                sqlda.Fill(ds, "DATA");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getDashboardalerts() - Error - ", ex);
                return ds;
            }

        }
        public DataSet getDashboardHistory(string dbConnection, string instance, string application, string priority, string monitor, string fromDate, string toDate)
        {
            string sql =    "select Name, Status, Priority, Creationtime, Message " +
                            " from DB_alert INNER JOIN DB_monitor on DB_alert.monitor = DB_monitor.ID " +
                            " INNER JOIN DB_message on DB_alert.errmess = DB_message.ID " +
                            " where DB_alert.errmess != '1' " +
                            " and instance = '" + instance + "' and application = '" + application + "'" +
                            " and priority = '" + priority + "' and monitor = '" + monitor + "'" +
                            " and creationtime >= '" + fromDate + "' and creationtime <= '" + toDate + "'" +
                            " order by name, status, priority , creationtime";

         
            Logger.writeDebug("getDashboardHistory() - " + sql);
          

            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                //dbConn.ConnectionTimeout = 120;
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);
                sqlda.SelectCommand.CommandTimeout = 120;
                sqlda.Fill(ds, "DATA");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getDashboardHistory() - Error - ", ex);
                return ds;
            }

        }
        public void checkForUser(string UserIdentity)
        {
            string sqlStmt = "select isNull(instance_pref,'') as instance_pref, isNull(application_pref,'') " +
                             " as application_pref from DB_Preferences where user_id = '" + UserIdentity + "'";

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlStmt, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    StringCollection dataHolder = new StringCollection();
                    dataHolder.AddRange(dataReader["instance_pref"].ToString().Split(';'));
                    HttpContext.Current.Session.Add("INSTANCE_PREF", dataHolder);
                    dataHolder = new StringCollection();
                    dataHolder.AddRange(dataReader["application_pref"].ToString().Split(';'));
                    HttpContext.Current.Session.Add("APPLICATION_PREF", dataHolder);

                    //Close the datareader
                    dataReader.Close();

                    //Update the last accessed time
                    sqlStmt = " Update DB_preferences set last_accessed = GETDATE() where user_id = '" + UserIdentity + "'";
                    cmd = new SqlCommand(sqlStmt, conn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    if (!dataReader.IsClosed) dataReader.Close();

                    //User does not exists. Lets add a new row.
                    sqlStmt = " Insert into DB_preferences values ( '" + UserIdentity + "' , '' , '', 0, GETDATE(),GETDATE())";
                    cmd = new SqlCommand(sqlStmt, conn);
                    cmd.ExecuteNonQuery();

                    HttpContext.Current.Session.Add("INSTANCE_PREF", new StringCollection());
                    HttpContext.Current.Session.Add("APPLICATION_PREF", new StringCollection());
                }

            }
            catch (Exception ex)
            {
                Logger.writeError("Error encounterd for method checkForUser() ", ex);
            }
        }

        public string updateUserPreferences(string UserIdentity, string instancePref, string applicationPref)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                string sqlStmt = " UPDATE DB_preferences Set instance_pref = '" + instancePref + "' , " +
                                 " application_pref = '" + applicationPref + "' " +
                                 " where user_id = '" + UserIdentity + "'";

                SqlCommand cmd = new SqlCommand(sqlStmt, conn);
                int rowCount = cmd.ExecuteNonQuery();

                //No record was updated.. Need to insert the record.
                if (rowCount == 0)
                {
                    sqlStmt = " Insert into DB_preferences (user_id, instance_pref, application_pref) values  ( '" + UserIdentity + "' , '" +
                              instancePref + "' , '" + applicationPref + "')";
                    cmd = new SqlCommand(sqlStmt, conn);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "Error : While updating user preferences. Message : " + ex.Message;
            }
        }

        public string getInstanceFullName(string instanceName)
        {
            if (instanceName == null || instanceName.Equals(""))
            {
                return "";
            }

            try
            {
                string sqlStmt = " select fullname from DB_regions where instance = '" + instanceName + "'";
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlStmt, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    return dataReader["fullname"].ToString();
                }
                else
                {
                    Logger.writeDebug("No instance name mapping found for " + instanceName);
                    return instanceName;
                }

            }
            catch (Exception ex)
            {
                Logger.writeError("Error occured while getting the instance full name.", ex);
                return instanceName;
            }
        }

        public string loadPreferencesData(string userID, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    StringCollection stringValues = new StringCollection();

                    string queryString = "select distinct Instance from view_latest order by instance";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        stringValues.Add(reader[0].ToString());
                    }

                    HttpContext.Current.Session["INSTANCE"] = stringValues;
                    reader.Close();

                    stringValues = new StringCollection();

                    queryString = "select distinct application from view_latest order by application";
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        stringValues.Add(reader[0].ToString());
                    }
                    reader.Close();

                    HttpContext.Current.Session["APPLICATION"] = stringValues;

                    //Time to get user specific data
                    if (userID != null && userID.Length > 0)
                    {
                        queryString = "select instance_pref, application_pref from DB_Preferences where user_id = '"
                            + userID + "'";
                        command = new SqlCommand(queryString, connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string instanceList = reader[0].ToString();
                            string appList = reader[1].ToString();

                            stringValues = new StringCollection();
                            stringValues.AddRange(instanceList.Split(';'));
                            HttpContext.Current.Session["USER_INSTANCE"] = stringValues;

                            stringValues = new StringCollection();
                            stringValues.AddRange(appList.Split(';'));
                            HttpContext.Current.Session["USER_APPLICATION"] = stringValues;
                        }
                        else
                        {
                            HttpContext.Current.Session["USER_INSTANCE"] = new StringCollection();
                            HttpContext.Current.Session["USER_APPLICATION"] = new StringCollection();

                        }
                    }
                    else
                    {
                        HttpContext.Current.Session["USER_INSTANCE"] = new StringCollection();
                        HttpContext.Current.Session["USER_APPLICATION"] = new StringCollection();
                    }

                    connection.Close();

                    return "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                //Response.Write("Error : " + ex.Message + "<br>" + ex.StackTrace);
                return "Error : Following error is encountered while building up preferences list : " + ex.Message;
            }

        }

       
    }
}
