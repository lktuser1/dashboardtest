using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using EDSIntranet.Reports;
using EDS.Intranet.Base;
using EDS.Intranet.Common;
using System.Web.Script.Services;
using System.Web.Services.Protocols;

namespace EDSIntranet.Reports
{
     [WebService(Namespace = "http://msrasv01.tor.omc.hp.com/EDSIntranet.Reports")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class DataService : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetApplications()
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                List<Application> applications = new List<Application>();
                using (SqlConnection con = new SqlConnection(cs))
                {

                    string sql = " select distinct application from DB_monitor where active = 1 ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Application application = new Application();
                        //application.Id = Convert.ToInt32(rdr["Id"]);
                        application.application = rdr["application"].ToString();
                        applications.Add(application);
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(applications));
            }
            catch (Exception ex)
            {
                Logger.writeError("Error occured while retrieving applications ", ex);
               
            }
            
        }

        [WebMethod]
        public void GetInstances(string application)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                List<Instance> instances = new List<Instance>();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string sql = " SELECT distinct instance, application FROM DB_monitor where active = 1 and application =  @ApplicationId ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter()
                    {
                        ParameterName = "@ApplicationId",
                        Value = application
                    };
                    cmd.Parameters.Add(param);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Instance instance = new Instance();
                       // instance.Id = Convert.ToInt32(rdr["Id"]);
                        instance.instance = rdr["instance"].ToString();
                        instance.application = rdr["application"].ToString();
                        instances.Add(instance);
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(instances));
             }
            
            catch (Exception ex)
            {
                Logger.writeError("Error occured while retrieving instances ", ex);
               
            }
        }

        [WebMethod]
        public void GetPriorities(string application, string instance)
        {
             try
            {
            string cs = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            List<Priority> priorities = new List<Priority>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql = " select distinct priority, instance from DB_monitor where active = 1 and application =  @ApplicationId and instance = @InstanceId";
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter()
                {
                    ParameterName = "@ApplicationId",
                    Value = application
                };
                cmd.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter()
                {
                    ParameterName = "@InstanceId",
                    Value = instance
                };
                cmd.Parameters.Add(param2);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Priority priority = new Priority();
                    //priority.Id = Convert.ToInt32(rdr["Id"]);
                    priority.priority = rdr["priority"].ToString();
                    priority.instance = rdr["instance"].ToString();
                    priorities.Add(priority);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(priorities));
            }

             catch (Exception ex)
             {
                 Logger.writeError("Error occured while retrieving priorities ", ex);

             }
        }
        [WebMethod]
        public void GetMonitors(string application, string instance, string priority)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                List<MonitorName> monitors = new List<MonitorName>();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string sql = " SELECT distinct name, priority FROM DB_monitor where active = 1 and application =  @ApplicationId and instance = @InstanceId and priority =  @PriorityId ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter()
                    {
                        ParameterName = "@ApplicationId",
                        Value = application
                    };
                    cmd.Parameters.Add(param1);
                    SqlParameter param2 = new SqlParameter()
                    {
                        ParameterName = "@InstanceId",
                        Value = instance
                    };
                    cmd.Parameters.Add(param2);
                    SqlParameter param3 = new SqlParameter()
                    {
                        ParameterName = "@PriorityId",
                        Value = priority
                    };
                    cmd.Parameters.Add(param3);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MonitorName monitor = new MonitorName();
                        // instance.Id = Convert.ToInt32(rdr["Id"]);
                        monitor.name = rdr["name"].ToString();
                        monitor.priority = Convert.ToInt32(rdr["Priority"]);
                        monitors.Add(monitor);
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(monitors));
            }

            catch (Exception ex)
            {
                Logger.writeError("Error occured while retrieving monitors ", ex);

            }
        }

         [WebMethod]
        public void GetBacklogMonitors(String application, String instance, String priority)
        {

           

            Logger.writeDebug("GetBacklogMonitors() - Start " );

           // string application = "SA";
           // string instance = "SA-Prod";
           // string priority = "1";

            List<Monitor> listMonitors = new List<Monitor>();

            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                string sql = "sp_rptBacklogMonitorsExist";

                Logger.writeDebug("GetBacklogMonitors() - " + sql);
                Logger.writeDebug("GetBacklogMonitors() - " + application + ", " + instance + ", " + priority );
                
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    DataSet ds = new DataSet();

                    try
                    {
                        cmd.CommandTimeout = 240;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                        cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                        cmd.Parameters.Add("@p_priority", SqlDbType.VarChar).Value = priority;
                        // cmd.Parameters.Add("@p_monitor", SqlDbType.VarChar).Value = monitor;
                       // cmd.Parameters.Add("@p_fromDate", SqlDbType.DateTime).Value = fromDate;
                        //cmd.Parameters.Add("@p_toDate", SqlDbType.DateTime).Value = toDate;

                        
                            con.Open();
                            SqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                Monitor mon = new Monitor();
                                mon.Id = Convert.ToInt32(rdr["Id"]);
                                mon.Name = rdr["Name"].ToString();
                                mon.Application = rdr["Application"].ToString();
                                mon.Instance = rdr["Instance"].ToString();
                                mon.Priority = rdr["Priority"].ToString();
                                listMonitors.Add(mon);
                            }
                        
                        


                        
                    }//end try
                    catch (Exception ex)
                    {
                        Logger.writeError("GetBacklogMonitors() - Error - ", ex);
                        //return null;
                    }

                }//using command
            } //using connection

        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(listMonitors));

       
                        

        }
            
        
            
        
    }
}

