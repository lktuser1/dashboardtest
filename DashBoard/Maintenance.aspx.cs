using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDS.Intranet;
using EDS.Intranet.Base;
using EDS.Intranet.Common;
using System.Drawing;


namespace EDSIntranet
{
    public partial class Maintenance : System.Web.UI.Page
    {
       


        protected void Page_Load(object sender, EventArgs e)
        {
            IntranetCore masterpage = (IntranetCore)(Page.Master);

            masterpage.PageTitle = "Maintain Database Tables";
            masterpage.ShowPageLastUpdated = false;
            Master.Page.Title = "test";
            this.Title = "Maintain Database Tables";
            
           
        }


        protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTable.SelectedValue == "Select")
            {
                ListItem li = new ListItem();
                li.Text = "Select";
                li.Value = "Select";
            }

            if (ddlTable.SelectedValue == "DB_Monitor")
            {
                ListItem li = new ListItem();
                li.Text = "DB_Monitor";
            }
            if (ddlTable.SelectedValue == "DB_MonitorDetails")
            {
                ListItem li = new ListItem();
                li.Text = "DB_MonitorDetails";

            }
        }

       protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            if (ddlTable.SelectedValue == "DB_Monitor")
            {
                GridView2.Visible = true;
                GridView3.Visible = false;
                BindGridView2();
            }
            else
            {
                GridView2.Visible = false;
                GridView3.Visible = true;
                BindGridView3();
            }
        }

       protected void ddlMonitorDetailsID_SelectedIndexChanged(object sender, EventArgs e)
           {
            
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT ID, name from db_monitordetails;"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            
                        }
                    }
                }
            }
        }
       
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGridView2();
        }
        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            BindGridView3();
        }

        protected void BindGridView2()
        {
            
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"select * from " + ddlTable.SelectedValue + ";"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            
                        }
                    }
                }
            }
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            EnableEditMode();
            BindGridView2();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ddlTable.SelectedValue == "DB_Monitor")
            {

                bool blActive = false;

                TextBox txtInstance = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtInstance");
                TextBox txtApplication = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtApplication");
                TextBox txtName1 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtName1");
                TextBox txtDescription = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtDescription");
                TextBox txtPriority = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtPriority");
                TextBox txtFreq = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtFreq");
                CheckBox txtActive = (CheckBox)GridView2.Rows[e.RowIndex].FindControl("txtActive");
                TextBox txtMonitorDetailsID = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txtMonitorDetailsID");
                if (txtActive.Checked)

                    blActive = true;

                else

                    blActive = false;
                
                {
                    string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"Update " + ddlTable.SelectedValue + " SET instance ='" + txtInstance.Text + "' , application='" + txtApplication.Text + "' , name='" + txtName1.Text + "', description=  '" + txtDescription.Text + "', priority= '" + txtPriority.Text + "' , freq= '" + txtFreq.Text + "' , active= '" + blActive + "' , monitordetailsID= '" + txtMonitorDetailsID.Text + "' WHERE ID='" + GridView2.DataKeys[e.RowIndex].Values[0].ToString() + "';"))
                            
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    GridView2.EditIndex = -1;
                                    sda.Fill(dt);
                                    
                                    GridView2.DataSource = dt;
                                    GridView2.DataBind();
                                    
                                    lblmsg.Text = "Record updated successfully!!";
                                    DisableEditMode();
                                    if (ddlTable.SelectedValue == "DB_Monitor")
                                    {
                                        BindGridView2();
                                    }
                                    else
                                    {
                                        BindGridView3();
                                    }
                                }
                            }                         
                        }
                    }
                }
            }
            
        }
        
    
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"DELETE FROM " + ddlTable.SelectedValue + " WHERE ID='" + GridView2.DataKeys[e.RowIndex].Values[0].ToString() + "';"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            GridView2.EditIndex = -1;
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            lblmsg.Text = "Record deleted successfully!!";
                        }
                    }
                }
            }
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            DisableEditMode();
            BindGridView2();

        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {

            TextBox txtInstance = (TextBox)GridView2.FooterRow.FindControl("txtInstance");
            TextBox txtApplication = (TextBox)GridView2.FooterRow.FindControl("txtApplication");
            TextBox txtName1 = (TextBox)GridView2.FooterRow.FindControl("txtName1");
            TextBox txtDescription = (TextBox)GridView2.FooterRow.FindControl("txtDescription");
            TextBox txtPriority = (TextBox)GridView2.FooterRow.FindControl("txtPriority");
            TextBox txtFreq = (TextBox)GridView2.FooterRow.FindControl("txtFreq");
            CheckBoxList txtActive = (CheckBoxList)GridView2.FooterRow.FindControl("txtActive");
            TextBox txtMonitorDetailsID = (TextBox)GridView2.FooterRow.FindControl("txtMonitorDetailsID");

            {
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO " + ddlTable.SelectedValue + " (instance, application, name, description, priority, freq, active, monitordetailsID) values ( '" + txtInstance.Text + "' , '" + txtApplication.Text + "' , '" + txtName1.Text + "' , '" + txtDescription.Text + "' , '" + txtPriority.Text + "' , '" + txtFreq.Text + "' , '" + txtFreq.Text + "' , '" + txtMonitorDetailsID.Text +"' );"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            GridView2.EditIndex = -1;
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            lblmsg.Text = "Record inserted successfully!!";
                        }
                    }
                }
            }
        }
        }
        }

        protected void BindGridView3()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"select * from " + ddlTable.SelectedValue + ";"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView3.DataSource = dt;
                            GridView3.DataBind();
                           
                        }
                    }
                }
            }
        }

        protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView3.EditIndex = e.NewEditIndex;
            EnableEditMode();
            BindGridView3();
        }

        protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if(ddlTable.SelectedValue == "DB_MonitorDetails")
            {
                TextBox txtName = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtName");
                TextBox txtDetaildesc = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtDetaildesc");
                TextBox txtTechnicaldesc = (TextBox)GridView3.Rows[e.RowIndex].FindControl("txtTechnicaldesc");
                {
                    string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"Update " + ddlTable.SelectedValue + " SET name ='" + txtName.Text + "' , detaildesc='" + txtDetaildesc.Text + "' , technicaldesc='" + txtTechnicaldesc.Text + "' WHERE ID='" + GridView3.DataKeys[e.RowIndex].Values[0].ToString() + "';"))
                        {


                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    GridView3.EditIndex = -1;
                                    sda.Fill(dt);
                                    GridView3.DataSource = dt;
                                    GridView3.DataBind();
                                    lblmsg.Text = "Record updated successfully!!";
                                    DisableEditMode();
                                    if (ddlTable.SelectedValue == "DB_Monitor")
                                    {
                                        BindGridView2();
                                    }
                                    else
                                    {
                                        BindGridView3();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            
        }


        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"DELETE FROM " + ddlTable.SelectedValue + " WHERE ID='" + GridView3.DataKeys[e.RowIndex].Values[0].ToString() + "';"))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            GridView3.EditIndex = -1;
                            sda.Fill(dt);
                            GridView3.DataSource = dt;
                            GridView3.DataBind();
                            lblmsg.Text = "Record deleted successfully!!";
                        }
                    }
                }
            }
        }

        protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView3.EditIndex = -1;
            
            DisableEditMode();
            BindGridView3();

        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox txtName = (TextBox)GridView2.FooterRow.FindControl("txtName");
                TextBox txtDetaildesc = (TextBox)GridView2.FooterRow.FindControl("txtDetaildesc");
                TextBox txtTechnicaldesc = (TextBox)GridView2.FooterRow.FindControl("txtTechnicaldesc");

                {
                    string constr = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"INSERT INTO " + ddlTable.SelectedValue + " (name, detaildescdescription, technicaldesc) values ( '" + txtName.Text + "' , '" + txtDetaildesc.Text + "' , '" + txtTechnicaldesc.Text + "' );"))
                        {


                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = con;
                                sda.SelectCommand = cmd;
                                using (DataTable dt = new DataTable())
                                {
                                    GridView3.EditIndex = -1;
                                    sda.Fill(dt);
                                    GridView3.DataSource = dt;
                                    GridView3.DataBind();
                                    lblmsg.Text = "Record inserted successfully!!";
                                }
                            }
                        }
                    }
                }
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void EnableEditMode()
        {
            ddlTable.Enabled = false;
            btnSubmit1.Enabled = false;
        }

        public void DisableEditMode()
        {
            ddlTable.Enabled = true;
            btnSubmit1.Enabled = true;
        }


    }
}