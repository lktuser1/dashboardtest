using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EDS.Intranet.Common;

namespace Dashboard.Reports
{
    public class SAAlert
    {
        //public int id { get; set; }
        public string BacklogDate { get; set; }
        public string AMS { get; set; }
        public string EMEA1 { get; set; }
        public string EMEA2 { get; set; }
        public string ALU { get; set; }
        public string APJ { get; set; }
        public string DWNG_AMS { get; set; }
        

    }
    public class SMAlert
    {
        //public int id { get; set; }
        public string Application { get; set; }
        public string Instance { get; set; }
        public string BacklogDate { get; set; }
        public string eNote_Monitor { get; set; }
        public string MSEvent_IM { get; set; }
        public string MSEvent_SD { get; set; }
        public string Schedule_agent { get; set; }
        public string Schedule_ocm { get; set; }
         public string Schedule_sla3 { get; set; }
        

    }

    public class SAAlertDataAccessLayer
    {
        public static List<SAAlert> GetAllAlerts(string sortColumn)
        {

            
            List<SAAlert> listSAAlerts = new List<SAAlert>();
            string sp_name = "sp_viewalerts";
            try
            {
            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                
                con.Open();

                SqlCommand cmd = new SqlCommand(sp_name, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                String firstalert = "";
                string ams = "";
                string emea1 = "";
                string emea2 = "";
                string alu = "";
                string apj = "";
                string dwng_ams = "";

                while (rdr.Read())
                {
                    SAAlert alert = new SAAlert();
                    //alert.id = Convert.ToInt32(rdr["id"]);

                    if (rdr["BacklogDate"].ToString() == firstalert)
                    {
                        alert.BacklogDate = firstalert;
                        if (!string.IsNullOrEmpty(ams))
                            alert.AMS = " Backlog " + (DateTime.Parse(ams) - DateTime.Parse(rdr["SM AMS-1"].ToString())) + " From - " + ams + " To - " + rdr["SM AMS-1"].ToString() ;
                        if (!string.IsNullOrEmpty(emea1))
                            alert.EMEA1 = " Backlog " + (DateTime.Parse(emea1) - DateTime.Parse(rdr["SM EMEA-1"].ToString())) + " From - " + emea1 + " To - " + rdr["SM EMEA-1"].ToString();
                        if (!string.IsNullOrEmpty(emea2))
                            alert.EMEA2 = " Backlog " + (DateTime.Parse(emea2) - DateTime.Parse(rdr["SM EMEA-2"].ToString())) + " From - " + emea2 + " To - " + rdr["SM EMEA-2"].ToString();
                        if (!string.IsNullOrEmpty(alu))
                            alert.ALU = " Backlog " + (DateTime.Parse(alu) - DateTime.Parse(rdr["SM ALU"].ToString())) + " From - " + alu + " To - " + rdr["SM ALU"].ToString();
                        if (!string.IsNullOrEmpty(apj))
                            alert.APJ = " Backlog " + (DateTime.Parse(apj) - DateTime.Parse(rdr["SM APJ-1"].ToString())) + " From - " + apj + " To - " + rdr["SM APJ-1"].ToString();
                        if (!string.IsNullOrEmpty(dwng_ams))
                            alert.DWNG_AMS = " Backlog " + (DateTime.Parse(dwng_ams) - DateTime.Parse(rdr["DWNG AMS"].ToString())) + " From - " + dwng_ams + " To - " + rdr["DWNG AMS"].ToString();

                        listSAAlerts.Add(alert);
                    }
                    else
                    {
                        firstalert = rdr["BacklogDate"].ToString();
                        ams = rdr["SM AMS-1"].ToString();
                        emea1 = rdr["SM EMEA-1"].ToString();
                        emea2 = rdr["SM EMEA-2"].ToString();
                        alu = rdr["SM ALU"].ToString();
                        apj = rdr["SM APJ-1"].ToString();
                        dwng_ams = rdr["DWNG AMS"].ToString();
                    }
                 
                }
            }

            return listSAAlerts;
            }
            catch (Exception ex)
            {
                Logger.writeError("Error encounterd for method SAGetAllAlerts() ", ex);
                return listSAAlerts;
            }
        }
        public static List<SMAlert> GetAllAlertsSM(string sortColumn)
        {


            List<SMAlert> listSMAlerts = new List<SMAlert>();
            string sp_name = "sp_viewalerts_SM";
            string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                con.Open();

                SqlCommand cmd = new SqlCommand(sp_name, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
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
            }

            return listSMAlerts;
        }
    }
}