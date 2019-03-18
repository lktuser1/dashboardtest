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
    public class ReportsDataAccess
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

                string sql = "sp_rptBackLogDetail";

                Logger.writeDebug("getBacklogDetail() - " + sql);
                Logger.writeDebug("getBacklogDetail() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        cmd.CommandTimeout = 90;
                        cmd.CommandType = CommandType.StoredProcedure;
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
                            sda.SelectCommand.CommandTimeout = 90;
                            sda.Fill(ds, "DATA");
                            //Logger.writeDebug("getBacklogDetail() -  sql execute success ");
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

                string sql = "sp_rptBackLogDetailGroupByHr";

                Logger.writeDebug("getBacklogGroupByHr() - " + sql);
                Logger.writeDebug("getBacklogGroupByHr() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        cmd.CommandTimeout = 90;
                        cmd.CommandType = CommandType.StoredProcedure;
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
                            sda.SelectCommand.CommandTimeout = 90;
                            sda.Fill(ds, "DATA");
                            //Logger.writeDebug("getBacklogGroupByHr -  sql execute success ");
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


            //Logger.writeDebug("getBackLogCounts() - " + sql);
            //Logger.writeDebug("getBacklogCounts() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));
                cmd.Parameters.Add(new SqlParameter("@p_monitor", monitor));
                cmd.Parameters.Add(new SqlParameter("@p_fromdate", Convert.ToDateTime(fromDate)));
                cmd.Parameters.Add(new SqlParameter("@p_todate", Convert.ToDateTime(toDate)));

                sqlda.SelectCommand.CommandTimeout = 90;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");
                //Logger.writeDebug("getBackLogCounts() -  sql execute complete ");
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


            //Logger.writeDebug("getBackLogCrosstab() - " + sql);
            //Logger.writeDebug("getBacklogCrosstab() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + fromDate + ", " + toDate);


            DataSet ds = new DataSet();

            try
            {
                SqlConnection dbConn = new SqlConnection(dbConnection);
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlda = new SqlDataAdapter(sql, dbConn);

                //DataTable dt = new DataTable();

                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_application", application));
                cmd.Parameters.Add(new SqlParameter("@p_instance", instance));
                cmd.Parameters.Add(new SqlParameter("@p_priority", Convert.ToInt32(priority)));
                cmd.Parameters.Add(new SqlParameter("@p_monitor", monitor));
                cmd.Parameters.Add(new SqlParameter("@p_fromdate", Convert.ToDateTime(fromDate)));
                cmd.Parameters.Add(new SqlParameter("@p_todate", Convert.ToDateTime(toDate)));

                sqlda.SelectCommand.CommandTimeout = 90;
                sqlda.SelectCommand = cmd;
                sqlda.Fill(ds, "DATA");

               // Logger.writeDebug("getBackLogCrosstab() -  sql execute complete - Is ds null- " + (ds == null).ToString() + " - ds tables count - " + ds.Tables.Count);
                return ds;
            }
            catch (Exception ex)
            {
                Logger.writeError("getBackLogCrosstab() - " + " Error - ", ex);
                return ds;
            }

        }

        

    

    }
}