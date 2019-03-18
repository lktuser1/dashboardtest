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
using EDS.Intranet.Common;

namespace Dashboard.Reports
{

    public class ReportsHelper
    {
        public string p_application { get; set; }
        public string p_instance { get; set; }
        public int p_priority { get; set; }
        public string p_monitor { get; set; }
        public DateTime p_fromDate { get; set; }
        public DateTime p_toDate { get; set; }

        public DataSet getBacklogDetail(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                
                    string sql = @"select Application, Instance, Priority, Name as Monitor, Status, Creationtime, Message  
                    from DB_alert INNER JOIN DB_monitor on DB_alert.monitor = DB_monitor.ID  INNER JOIN DB_message on DB_alert.errmess = DB_message.ID  
                    where DB_alert.status <= 3  
                    and application =   @p_application  
                    and instance =    @p_instance  
                    and priority =   @p_priority  
                    and monitor =  @p_monitor  
                    and creationtime >= @p_fromDate  
                    and creationtime <= @p_toDate  
                    order by name,  priority , creationtime";

                    Logger.writeDebug("getBacklogDetail() - " + sql);
                    Logger.writeDebug("getBacklogDetail() - " +  application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        DataSet ds = new DataSet();
                       
                        try
                        { 

                        cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                        cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                        cmd.Parameters.Add("@p_priority", SqlDbType.Int).Value = priority;
                        cmd.Parameters.Add("@p_monitor", SqlDbType.VarChar).Value = monitor;
                        cmd.Parameters.Add("@p_fromDate", SqlDbType.DateTime).Value = fromDate;
                        cmd.Parameters.Add("@p_toDate", SqlDbType.DateTime).Value = toDate;

                                         
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                sda.SelectCommand.CommandTimeout = 180;
                                sda.Fill(ds, "DATA");
                                Logger.writeDebug("getBacklogDetail() -  sql execute success ");
                                return ds;
                                
                            } //using adapter
                        }//end try
                       catch (Exception ex)
                        {
                            Logger.writeError("getBacklogDetail() - Error - ", ex);
                            return ds;
                         }
  
                    }//using command
                } //using connection
        }
        public DataSet getBacklogGroupByHr(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                string sql = @"
                SELECT 
                    Application, Instance, Priority,
                    dashboard.dbo.DB_monitor.name as 'Monitor/Process Name', 
	                convert(varchar(19), dashboard.dbo.DB_alert.creationtime, 107)  as 'Date' ,
	                convert(varchar(13), dashboard.dbo.DB_alert.creationtime, 120) AS 'Hour',
	                convert(varchar(19), min(dashboard.dbo.DB_alert.creationtime)) as 'First Alert in Hour', 
	                convert(varchar(19), max(dashboard.dbo.DB_alert.creationtime)) as 'Last Alert in Hour',
                    --datediff(minute,min(dashboard.dbo.DB_alert.creationtime) , max(dashboard.dbo.DB_alert.creationtime) ) + 5 as 'Backlog (minutes)'
                    max(Message) as Backlog
                FROM 
                dashboard.dbo.DB_alert 
                INNER JOIN dashboard.dbo.DB_message ON dashboard.dbo.DB_alert.errmess = dashboard.dbo.DB_message.ID 
                INNER JOIN dashboard.dbo.DB_monitor ON dashboard.dbo.DB_alert.monitor = dashboard.dbo.DB_monitor.ID";

                string sqlwhere = @" where 
                        application =   @p_application  
                    and instance =    @p_instance  
                    and priority =   @p_priority  
                    and monitor =  @p_monitor  
                    and creationtime >= @p_fromDate  
                    and creationtime <= @p_toDate";

                string sqlgroupby = @"
                    AND (dashboard.dbo.DB_alert.status <= 3 )
                    and name != 'DWNG AMS'
                    group by Application, Instance, Priority, name, 
                    convert(varchar(19), dashboard.dbo.DB_alert.creationtime, 107) ,
                    CONVERT(VARCHAR(13), dashboard.dbo.DB_alert.creationtime, 120)
                    --having datediff(minute,min(dashboard.dbo.DB_alert.creationtime) , max(dashboard.dbo.DB_alert.creationtime))  > 0
                    order by   'Hour'";

                sql = sql + sqlwhere + sqlgroupby;

               // Logger.writeDebug("getBacklogGroupByHr() - " + sql);
                Logger.writeDebug("getBacklogGroupByHr() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DataSet ds = new DataSet();

                    try
                    {

                        cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                        cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                        cmd.Parameters.Add("@p_priority", SqlDbType.Int).Value = priority;
                        cmd.Parameters.Add("@p_monitor", SqlDbType.VarChar).Value = monitor;
                        cmd.Parameters.Add("@p_fromDate", SqlDbType.DateTime).Value = fromDate;
                        cmd.Parameters.Add("@p_toDate", SqlDbType.DateTime).Value = toDate;


                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.SelectCommand.CommandTimeout = 180;
                            sda.Fill(ds, "DATA");
                            Logger.writeDebug("getBacklogGroupByHr -  sql execute success ");
                            return ds;

                        } //using adapter
                    }//end try
                    catch (Exception ex)
                    {
                        Logger.writeError("getBacklogGroupByHr() - Error - ", ex);
                        return ds;
                    }

                }//using command
            } //using connection
        }
        public DataSet getBackLogCounts(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {

            string sql = "dbo.sp_rptBackLogCounts";


            Logger.writeDebug("getBackLogCounts() - " + sql);
            Logger.writeDebug("getBacklogCounts() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));
                cmd.Parameters.Add(new SqlParameter("@p_fromdate", Convert.ToDateTime(fromDate)));
                cmd.Parameters.Add(new SqlParameter("@p_todate", Convert.ToDateTime(toDate)));

                sqlda.SelectCommand.CommandTimeout = 240;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");
                Logger.writeDebug("getBackLogCounts() -  sql execute complete ");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getBackLogCounts() - ", ex);
                return ds;
            }

        }

        public DataSet getBackLogCrosstab(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {

            string sql = "dbo.sp_rptBackLogCrosstab";


            Logger.writeDebug("getBackLogCrosstab() - " + sql);
            Logger.writeDebug("getBacklogCrosstab() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

                //DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));
                cmd.Parameters.Add(new SqlParameter("@p_monitor", monitor));
                cmd.Parameters.Add(new SqlParameter("@p_fromdate", Convert.ToDateTime(fromDate)));
                cmd.Parameters.Add(new SqlParameter("@p_todate", Convert.ToDateTime(toDate)));

                sqlda.SelectCommand.CommandTimeout = 240;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");

                Logger.writeDebug("getBackLogCrosstab() -  sql execute complete - Is ds null- " + (ds == null).ToString() + " - ds tables count - " + ds.Tables.Count );
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getBackLogCrosstab() - " + " Error - ", ex);
                return ds;
            }

        }

        public DataSet getMonitorListByInstance(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {

            string sql = "dbo.sp_rptMonitorList";


            Logger.writeDebug("getMonitorListByInstance() - " + sql);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                //cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                //cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));

                sqlda.SelectCommand.CommandTimeout = 240;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");
                Logger.writeDebug("getMonitorListByInstance() - sql execute complete ");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getMonitorListByInstance() - ", ex);
                return ds;
            }

        }
       
        public DataSet getBacklogHistoryCounts(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {
             //====================== Old Code Not used =======================================================
            string sql = "dbo.sp_viewBacklogCounts";


            Logger.writeDebug("getBacklogHistoryCounts - " + sql);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

                //DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));
                
                sqlda.SelectCommand.CommandTimeout = 240;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");
                Logger.writeDebug("getBacklogHistoryCounts -  sql execute complete ");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("Error encounterd for method getDashboardBacklogCounts() ", ex);
                return ds;
            }

        }
        public DataSet getBacklogHistory(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {



            //====================== Old Code Not used =======================================================

            string sql = @"
            SELECT 
	            application, instance,
	            convert(nvarchar(10), convert(date, dashboard.dbo.DB_alert.creationtime), 121)  as 'BackLogDate' ,  
                dashboard.dbo.DB_monitor.name, 
        	    min(dashboard.dbo.DB_alert.creationtime) as BackLogFrom, 
	            max(dashboard.dbo.DB_alert.creationtime) as BackLogTo
      
            FROM 

                dashboard.dbo.DB_alert 
                INNER JOIN dashboard.dbo.DB_message ON dashboard.dbo.DB_alert.errmess = dashboard.dbo.DB_message.ID 
                INNER JOIN dashboard.dbo.DB_monitor ON dashboard.dbo.DB_alert.monitor = dashboard.dbo.DB_monitor.ID";

               string sqlwhere = " WHERE dashboard.dbo.DB_monitor.application = '" + application + "'"
                + " and dashboard.dbo.DB_monitor.instance = '" + instance + "'"
                + " and dashboard.dbo.DB_monitor.priority = " + priority
                + " and creationtime >= '" + fromDate + "'"
                + " and creationtime <= '" + toDate + "'"
                + " AND (dashboard.dbo.DB_alert.status <= 3 ) "
                + " and name != 'DWNG AMS' ";

            string sqlgroupby = @"

                GROUP BY application, instance,  convert(nvarchar(10), convert(date, dashboard.dbo.DB_alert.creationtime), 121), name ";

            sql = sql + sqlwhere + sqlgroupby;

            
            Logger.writeDebug("getBacklogHistory - " + sql);
            // + " and dashboard.dbo.DB_monitor.id = '" + monitor + "' "

            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);
                sqlda.SelectCommand.CommandTimeout = 180;
                sqlda.Fill(ds, "DATA");
                Logger.writeDebug("getBacklogHistory -  sql execute success ");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("Error encounterd for method getBacklogHistory() ", ex);
                return ds;
            }

        }
        public DataSet getDashboardBacklogCrosstab(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {
            //====================== Old Code Not used =======================================================

            string sql = "sp_viewalerts_SM";


            Logger.writeDebug("getDashboardBacklogCrosstab - " + sql);

            List<SMAlert> listSMAlerts = new List<SMAlert>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
           

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

               
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));

                sqlda.SelectCommand.CommandTimeout = 240;
                sqlda.SelectCommand = cmd;
                SqlDataReader rdr = cmd.ExecuteReader();
                Logger.writeDebug("getDashboardBacklogCrosstab -  sql execute complete ");
                sqlda.Fill(ds);

                //foreach (DataTable t in ds.Tables) 
                //{
                // Console.WriteLine("Table " + t.TableName + " is in dataset");
                // Console.WriteLine("Row 0, column 1: " + t.Rows[0][1]); 
                // Console.WriteLine("Row 1, column 1: " + t.Rows[1][1]); 
                // Console.WriteLine("Row 2, column 1: " + t.Rows[2][1]); 
                //}



                String firstalert = "";
                string Application = "";
                string Instance = "";
                string eNote_Monitor = "";
                string MSEvent_IM = "";
                string MSEvent_SD = "";
                string Schedule_agent = "";
                string Schedule_ocm = "";
                string Schedule_sla3 = "";
         
                while (rdr.Read())
                {
                    SMAlert alert = new SMAlert();
                    //alert.id = Convert.ToInt32(rdr["id"]);
                    
                  
                    if (rdr["BacklogDate"].ToString() == firstalert)
                    {
                        
                        alert.Application = Application;
                        alert.Instance = Instance;
                        alert.BacklogDate = firstalert;
                        if (!string.IsNullOrEmpty(eNote_Monitor))
                            alert.eNote_Monitor = " Backlog " + (DateTime.Parse(eNote_Monitor) - DateTime.Parse(rdr["eNote Monitor"].ToString())) + " From - " + eNote_Monitor + " To - " + rdr["eNote Monitor"].ToString();
                        if (!string.IsNullOrEmpty(MSEvent_IM))
                            alert.MSEvent_IM = " Backlog " + (DateTime.Parse(MSEvent_IM) - DateTime.Parse(rdr["MSEvent_IM"].ToString())) + " From - " + MSEvent_IM + " To - " + rdr["MSEvent_IM"].ToString();
                        if (!string.IsNullOrEmpty(MSEvent_SD))
                            alert.MSEvent_SD = " Backlog " + (DateTime.Parse(MSEvent_SD) - DateTime.Parse(rdr["MSEvent_SD"].ToString())) + " From - " + MSEvent_SD + " To - " + rdr["MSEvent_SD"].ToString();
                        if (!string.IsNullOrEmpty(Schedule_agent))
                            alert.Schedule_agent = " Backlog " + (DateTime.Parse(Schedule_agent) - DateTime.Parse(rdr["Schedule agent"].ToString())) + " From - " + Schedule_agent + " To - " + rdr["Schedule agent"].ToString();
                        if (!string.IsNullOrEmpty(Schedule_ocm))
                            alert.Schedule_ocm = " Backlog " + (DateTime.Parse(Schedule_ocm) - DateTime.Parse(rdr["Schedule ocm"].ToString())) + " From - " + Schedule_ocm + " To - " + rdr["Schedule ocm"].ToString();
                        if (!string.IsNullOrEmpty(Schedule_sla3))
                            alert.Schedule_sla3 = " Backlog " + (DateTime.Parse(Schedule_sla3) - DateTime.Parse(rdr["Schedule sla3"].ToString())) + " From - " + Schedule_sla3 + " To - " + rdr["Schedule sla3"].ToString();

                        listSMAlerts.Add(alert);
                    }
                    else
                    {

                        Application = rdr["Application"].ToString();
                        Instance = rdr["Instance"].ToString();
                        firstalert = rdr["BacklogDate"].ToString();
                        eNote_Monitor = rdr["eNote Monitor"].ToString();
                        MSEvent_IM = rdr["MSEvent_IM"].ToString();
                        MSEvent_SD = rdr["MSEvent_SD"].ToString();
                        Schedule_agent = rdr["Schedule agent"].ToString();
                        Schedule_ocm = rdr["Schedule ocm"].ToString();
                        Schedule_sla3 = rdr["Schedule sla3"].ToString();

                    }
   
                }
           

            //return listSMAlerts;
              
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("Error encounterd for method getDashboardBacklogCrosstab() ", ex);
                return ds;
            }

        }

       
        
    }
}