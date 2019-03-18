using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EDS.Intranet.Base;
using EDS.Intranet.Common;
using Dashboard.Reports;
using Dashboard.Monitors;

namespace Dashboard.Monitors
{
    public partial class MonitorDetails : PageBase
    {
            private string application = "SM7 Prod";
            private string instance = "AMS";
            private int priority = 1;
            private string monitor = "35";
            private int messagedetailsID = 0;

            //string localInstance = "";
            SQLHelper sqlHelper = new SQLHelper();

            private string dbConnection = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {

                    Master.PageTitle = "Monitor Details";
                    Master.ShowPageSubTitle = true;
                    Master.PageSubtitle = "Monitor Details";
                    Master.ShowPageLastUpdated = false;
                    this.Title = "Monitors Details";


                    if (Request.QueryString["app"] != null)
                    {
                        application = Request.QueryString["app"];
                    }
                    if (Request.QueryString["inst"] != null)
                    {
                        instance = Request.QueryString["inst"];
                       
                    }
                    if (Request.QueryString["monid"] != null)
                    {
                        monitor = Request.QueryString["monid"];
                        
                    }
                    if (Request.QueryString["messagedetailsID"] != null )  
                    {
                        if (string.IsNullOrEmpty(Request.QueryString["messagedetailsID"].Trim()))
                        {
                            messagedetailsID = 0;
                        }
                        else
                        {
                            messagedetailsID = Convert.ToInt32(Request.QueryString["messagedetailsID"]);
                        }
                    }
                   
                    Master.PageSubtitle = "For Application " + application + " and Instance " + instance;


                    string sql = @"select db_monitor.id, application, instance, priority, db_monitor.name, description,  detaildesc, technicaldesc ,  freq
                                from DB_monitor 
                                left join DB_monitordetails on db_monitor.monitordetailsID = db_monitordetails.ID
                                where 
                                application =   @p_application  
                                and instance =    @p_instance  
                                and db_monitor.id =    @p_monitor "; 

                                //and name =    @p_monitor " ; 





                    //Logger.writeDebug("getMonitorDesc() - " + sql );
                    Logger.writeDebug("getMonitorDesc() - " + application + ", " + instance + ", " + priority + ", " + monitor + ", " + messagedetailsID);

                    using (SqlConnection con = new SqlConnection(dbConnection))
                    {
                        SqlDataReader sdr;

                        SqlCommand cmd = new SqlCommand(sql, con);
                        con.Open();

                        cmd.Parameters.Add("@p_application", SqlDbType.VarChar).Value = application;
                        cmd.Parameters.Add("@p_instance", SqlDbType.VarChar).Value = instance;
                        cmd.Parameters.Add("@p_monitor", SqlDbType.Int).Value = monitor;

                        sdr = cmd.ExecuteReader();

                        // Call Read before accessing data.
                        while (sdr.Read())
                        {
                            //Logger.writeDebug(sdr["name"].ToString());
                            //Logger.writeDebug(sdr["detaildesc"].ToString());
                            //Logger.writeDebug(sdr["technicaldesc"].ToString());

                            this.Labelname.Text = sdr["name"].ToString();
                            this.Labelname.DataBind();

                            this.Labeldescription.Text = sdr["description"].ToString();
                            this.Labeldescription.DataBind();

                            this.Labelid.Text = sdr["id"].ToString();
                            this.Labelid.DataBind();

                            this.Labelpriority.Text = sdr["priority"].ToString();
                            this.Labelpriority.DataBind();

                            this.Labelfreq.Text = sdr["freq"].ToString();
                            this.Labelfreq.DataBind();

                            this.Label1.Text = sdr["detaildesc"].ToString();
                            this.Label1.DataBind();

                            this.Label2.Text = sdr["technicaldesc"].ToString();
                            this.Label2.DataBind();
                        }

                        // Call Close when done reading.
                        sdr.Close();


                    } //using connection

                    sql = @"SELECT m.[ID] , messagedetailsid,messagedetails
                            FROM 
                              [DB_message] m
                              inner join  [DB_messagedetails] md
                              on m.messagedetailsID = md.id
                                where 
                                messagedetailsID =   @p_messagedetailsID  ";

                  
                    using (SqlConnection con = new SqlConnection(dbConnection))
                    {
                        SqlDataReader sdr;

                        SqlCommand cmd = new SqlCommand(sql, con);
                        con.Open();

                        cmd.Parameters.Add("@p_messagedetailsID", SqlDbType.Int).Value = messagedetailsID;
                       

                        sdr = cmd.ExecuteReader();

                        // Call Read before accessing data.
                        while (sdr.Read())
                        {
                            
                            this.Label3.Text = sdr["messagedetails"].ToString();
                            this.Label3.DataBind();

                            
                        }

                        // Call Close when done reading.
                        sdr.Close();


                    } //using connection
             
                }
            }
    }
}