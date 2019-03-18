using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EDS.Intranet.Common;

namespace Dashboard.Monitors
{
     public class Monitors
    {
        public int Id { get; set; }
        public string Instance { get; set; }
        public string Application { get; set; }
        public string Monitor { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Frequency { get; set; }

              
    }

     public class MonitorAll
     {
         public string Monitor { get; set; }
         public string AMS { get; set; }
         public string EMEA { get; set; }
         public string EMEA2 { get; set; }
         public string ALU { get; set; }
         public string APJ1 { get; set; }
         


     }
     public class MonitorDesc
     {
         public int Id { get; set; }
         public string detaildesc { get; set; }
         public string technicaldesc { get; set; }
         


     }

     public class MonitorType
     {

         public int Id { get; set; }
         public string Name { get; set; }

     }
     public class MonitorDataAccessLayer
     {
         public static List<Monitors> GetMonitorsList(string sortColumn, string dbConnection, string application, string instance, string priority, string monitor)
         {
             List<Monitors> listMonitors = new List<Monitors>();

             string CS = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
             using (SqlConnection con = new SqlConnection(CS))
             {
                 string sql = "dbo.sp_rptMonitorList";
                           // " Order by priority ";
                           //  " and priority = " + priority ; 
                 


                 //if (!string.IsNullOrEmpty(sortColumn))
                // {
                //     sql += " order by " + sortColumn;
               //  }

                 Logger.writeDebug("GetMonitorsList() - " + sql);
                 Logger.writeDebug("getMonitorList() - " + sql + ", " + application + ", " + instance + ", " + priority + ", " + monitor);

                 SqlCommand cmd = new SqlCommand(sql, con);
                 cmd.CommandType = CommandType.StoredProcedure;

                 cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                 cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;

                 con.Open();
                 SqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     Monitors monitorrow = new Monitors();
                     monitorrow.Id = Convert.ToInt32(rdr["Id"]);
                     monitorrow.Instance = rdr["Instance"].ToString();
                     monitorrow.Application = rdr["Application"].ToString();
                     monitorrow.Monitor = rdr["Monitor"].ToString();
                     monitorrow.Description = rdr["Description"].ToString();
                     monitorrow.Priority = rdr["Priority"].ToString();
                     monitorrow.Frequency = rdr["Frequency"].ToString();

                     listMonitors.Add(monitorrow);
                 }
             }

             return listMonitors;
         }
         public static List<MonitorAll> GetAllMonitors(string sortColumn, string dbConnection, string application, string instance, string priority, string monitor)
         {
             List<MonitorAll> listMonitorAll = new List<MonitorAll>();
             string sp_name = "sp_getAllMonitors";
             string CS = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
             Logger.writeDebug("GetAllMonitors - " + sp_name);
             using (SqlConnection con = new SqlConnection(CS))
             {
                 con.Open();

                 SqlCommand cmd = new SqlCommand(sp_name, con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandTimeout = 120;
                 SqlDataReader rdr = cmd.ExecuteReader();

                 while (rdr.Read())
                 {
                     MonitorAll monitorrow = new MonitorAll();
                     monitorrow.Monitor = rdr["Monitor"].ToString();
                     monitorrow.AMS = rdr["AMS"].ToString();
                     monitorrow.EMEA = rdr["EMEA"].ToString();
                     monitorrow.EMEA2 = rdr["EMEA-2"].ToString();
                     monitorrow.ALU = rdr["ALU"].ToString();
                     monitorrow.APJ1 = rdr["APJ-1"].ToString();
                    

                     listMonitorAll.Add(monitorrow);
                 }
             }

             return listMonitorAll;
         }
         public static List<MonitorDesc> GetMonitorDesc(string sortColumn, string dbConnection, string application, string instance, string priority, string monitor)
         {
             List<MonitorDesc> listMonitorDesc = new List<MonitorDesc>();

             string CS = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
             using (SqlConnection con = new SqlConnection(CS))
             {
                 string sql = "Select  Id,  detaildesc, technicaldesc from DB_monitordesc ";
                         
                 // " Order by priority ";
                 //  " and priority = " + priority ; 



                 if (!string.IsNullOrEmpty(sortColumn))
                 {
                     sql += " order by " + sortColumn;
                 }

                 Logger.writeDebug("GetMonitorsDesc() - " + sql);

                 SqlCommand cmd = new SqlCommand(sql, con);

                 con.Open();
                 SqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     MonitorDesc monitorrow = new MonitorDesc();
                     monitorrow.Id = Convert.ToInt32(rdr["Id"]);
                     monitorrow.detaildesc = rdr["detaildesc"].ToString();
                     monitorrow.technicaldesc = rdr["technicaldesc"].ToString();
                  

                     listMonitorDesc.Add(monitorrow);
                 }
             }

             return listMonitorDesc;
         }
     }
}