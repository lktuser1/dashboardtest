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

namespace Dashboard.Monitors
{
    public class MonitorDataAccess
    {
        public DataSet getMonitorListByInstance(string dbConnection, string application, string instance, string priority, string monitor, string fromDate, string toDate)
        {

            string sql = "dbo.sp_rptMonitorListByInstance";


            Logger.writeDebug("getMonitorListByInstance() - " + sql);
            Logger.writeDebug("getBacklogListByInstance() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);



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
                //Logger.writeDebug("getMonitorListByInstance() - sql execute complete ");
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getMonitorListByInstance() - ", ex);
                return ds;
            }

        }
        public DataSet getMonitorDetailsList(string sortColumn, string dbConnection, string application, string instance, string priority, string monitor)
        {
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

               string sql = "dbo.sp_rptMonitorDetailsList";

                //if (!string.IsNullOrEmpty(sortColumn))
                //{
               //     sql += " order by " + sortColumn;
               // }

                Logger.writeDebug("getMonitorDetailsList() - " + sql);
                Logger.writeDebug("getMonitorDetailsList() - " + sql + ", " + application + ", " + instance + ", " + priority + ", " + monitor);

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                        cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                        //cmd.Parameters.Add("@p_priority", SqlDbType.Int).Value = priority;
                        //cmd.Parameters.Add("@p_monitor", SqlDbType.VarChar).Value = monitor;



                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.SelectCommand.CommandTimeout = 180;
                            sda.Fill(ds, "DATA");
                            //Logger.writeDebug("getMonitorDetailsList() -  sql execute success ");
                            return ds;

                        } //using adapter
                    }//end try
                    catch (Exception ex)
                    {
                        Logger.writeError("getMonitorDetailsList() - ", ex);
                        return ds;
                    }

                }//using command
            } //using connection
        }

        public SqlDataReader getMonitorDetails(string sortColumn, string dbConnection, string application, string instance, int priority, string monitor)
        {

            string sql = "dbo.sp_rptMonitorDetails";

            if (!string.IsNullOrEmpty(sortColumn))
            {
                sql += " order by " + sortColumn;
            }

            Logger.writeDebug("getMonitorDetails() - " + sql);
            Logger.writeDebug("getMonitorDetails() - " + sql + ", " + application + ", " + instance + ", " + priority + ", " + monitor);

            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlDataReader sdr ;
                
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                    cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                    cmd.Parameters.Add("@p_monitor", SqlDbType.VarChar).Value = monitor;

                    sdr = cmd.ExecuteReader();
                    return sdr;
              
                   
                    

            } //using connection
            
        }


    }

}
