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
using System.Collections.Specialized;

namespace Dashboard
{
    public partial class Preferences : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = "Preferences";
            Master.PageSubtitle = String.Empty;
            Master.ShowPageLastUpdated = false;

            this.MessagePanel.Visible = false;
            this.DataPanel.Visible = true;

            if (!IsPostBack)
            {
                if (this.LoginUserID.Trim().Equals(""))
                {
                    this.MessagePanel.Visible = true;
                    this.MessageLine.Text = "Unable to determine User Identity. Please contact support team.";
                    this.DataPanel.Visible = false;
                }
                else
                {
                    SQLHelper sqlHelper = new SQLHelper();
                    string returnValue = sqlHelper.loadPreferencesData(this.LoginUserID, ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
                    if (returnValue.Equals("SUCCESS"))
                    {
                        populateListBox(this.ListBoxSourceInstance, this.ListBoxTargetInstance,
                            (StringCollection)Session["INSTANCE"], (StringCollection)Session["USER_INSTANCE"]);
                        populateListBox(this.ListBoxSourceApplication, this.ListBoxTargetApplication,
                            (StringCollection)Session["APPLICATION"], (StringCollection)Session["USER_APPLICATION"]);
                    }
                    else
                    {
                        this.DataPanel.Visible = false;
                        this.MessageLine.Text = returnValue;
                        this.MessagePanel.Visible = true;
                    }
                }
            }

        }

        private void populateListBox(ListBox mainListBox, ListBox userListBox, StringCollection mainData, StringCollection userData)
        {
            foreach (string data in mainData)
            {
                if (userData.Contains(data))
                {
                    userListBox.Items.Add(new ListItem(data));
                }
                else
                {
                    mainListBox.Items.Add(new ListItem(data));
                }
            }
        }

        private void moveDataBetweenListBox(ListBox srcListBox, ListBox targetListBox)
        {
            while (srcListBox.Items.Count > 0 && srcListBox.SelectedItem != null)
            {
                ListItem selectedItem = srcListBox.SelectedItem;
                targetListBox.Items.Add(selectedItem);
                srcListBox.Items.Remove(selectedItem);
                selectedItem.Selected = false;
            }
        }

        protected void ButtonRightInstance_Click(object sender, EventArgs e)
        {
            moveDataBetweenListBox(this.ListBoxSourceInstance, this.ListBoxTargetInstance);
        }

        protected void ButtonLeftInstance_Click(object sender, EventArgs e)
        {
            moveDataBetweenListBox(this.ListBoxTargetInstance, this.ListBoxSourceInstance);
        }

        protected void ButtonRightApplication_Click(object sender, EventArgs e)
        {
            moveDataBetweenListBox(this.ListBoxSourceApplication, this.ListBoxTargetApplication);
        }

        protected void ButtonLeftApplication_Click(object sender, EventArgs e)
        {
            moveDataBetweenListBox(this.ListBoxTargetApplication, this.ListBoxSourceApplication);
        }

        protected void ApplyChanges_Click(object sender, EventArgs e)
        {
            string instanceList = "";
            string applicationList = "";
            int counter = 0;

            foreach (ListItem li in this.ListBoxTargetInstance.Items)
            {
                instanceList += li.Value;
                if (counter != ListBoxTargetInstance.Items.Count)
                {
                    instanceList += ";";
                }
                counter++;
            }

            counter = 0;

            foreach (ListItem li in this.ListBoxTargetApplication.Items)
            {
                applicationList += li.Value;
                if (counter != ListBoxTargetApplication.Items.Count)
                {
                    applicationList += ";";
                }
                counter++;
            }

            //Now we have values lets update it.
            SQLHelper sqlHelper = new SQLHelper();

            string returnValue = sqlHelper.updateUserPreferences(this.LoginUserID, instanceList, applicationList);

            this.MessagePanel.Visible = true;

            if (!returnValue.Equals("SUCCESS"))
            {
                this.MessageLine.Text = returnValue;
            }
            else
            {
                this.MessageLine.Text = "Preferences setting is successfully updated.";
            }
        }



    }
}
