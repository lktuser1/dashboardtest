using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Dashboard.Monitors
{
    public partial class MonitorListTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTreeViewItems();
            }
        }

        private void GetTreeViewItems()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("Select  distinct id, name, type from DB_monitor where instance = 'AMS'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ds.Relations.Add("ChildRows", ds.Tables[0].Columns["id"],
                ds.Tables[0].Columns["type"]);

            foreach (DataRow level1DataRow in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(level1DataRow["type"].ToString()))
                {
                    TreeNode parentTreeNode = new TreeNode();
                    parentTreeNode.Text = level1DataRow["name"].ToString();
                    parentTreeNode.Value = level1DataRow["ID"].ToString();
                    GetChildRows(level1DataRow, parentTreeNode);
                    TreeView1.Nodes.Add(parentTreeNode);
                }
            }
        }

        private void GetChildRows(DataRow dataRow, TreeNode treeNode)
        {
            DataRow[] childRows = dataRow.GetChildRows("ChildRows");
            foreach (DataRow row in childRows)
            {
                TreeNode childTreeNode = new TreeNode();
                childTreeNode.Text = row["Name"].ToString();
                childTreeNode.Value = row["ID"].ToString();
                treeNode.ChildNodes.Add(childTreeNode);

                if (row.GetChildRows("ChildRows").Length > 0)
                {
                    GetChildRows(row, childTreeNode);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            GetSelectedTreeNodes(TreeView1.Nodes[0]);
        }

        private void GetSelectedTreeNodes(TreeNode parentTreeNode)
        {
            if (parentTreeNode.Checked)
            {
                ListBox1.Items.Add(parentTreeNode.Text + " - " + parentTreeNode.Value);
            }
            if (parentTreeNode.ChildNodes.Count > 0)
            {
                foreach (TreeNode childTreeNode in parentTreeNode.ChildNodes)
                {
                    GetSelectedTreeNodes(childTreeNode);
                }
            }
        }
    }
}