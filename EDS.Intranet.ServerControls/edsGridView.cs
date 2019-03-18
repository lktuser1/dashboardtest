using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDS.Intranet.ServerControls
{
    /// <summary>
    /// edsGridView Class.  This class extends the Microsoft GridView control to provide the
    /// functionality and look-and-feel needed for EDS Web Standards.
    /// </summary>
    /// <change date="2007/07/30" author="Todd McIntosh">
    ///  First revision
    /// </change>
    [ToolboxData("<{0}:edsGridView runat=server></{0}:edsGridView>")]
    [Themeable(true)]
    public class edsGridView : System.Web.UI.WebControls.GridView
    {
        const string ClearImageUrl = "~/Images/dot_clear.gif";
        const string SortAscImageUrl = "~/Images/SortAscending3D.gif";
        const string SortDescImageUrl = "~/Images/SortDescending3D.gif";
        const string FirstPageImageUrl = "~/Images/FirstPage3D.gif";
        const string PrevPageImageUrl = "~/Images/PrevPage3D.gif";
        const string NextPageImageUrl = "~/Images/NextPage3D.gif";
        const string LastPageImageUrl = "~/Images/LastPage3D.gif";

        private int rowID = 0;


        #region Properties
        /// <summary>
        /// This property allows you to enable/disable MultiColumn Sorting.
        /// </summary>
        [Description("Whether sorting on more than one column is enabled")]
        [Category("Behavior")]
        [DefaultValue("false")]
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["EnableMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                AllowSorting = true;
                ViewState["EnableMultiColumnSorting"] = value;
            }
        }
        /// <summary>
        /// This property allows you to enable/disable validation when paging.
        /// </summary>
        [Description("Whether validation when paging is enabled")]
        [Category("Behavior")]
        [DefaultValue("true")]
        public bool PagingCausesValidation
        {
            get
            {
                object o = ViewState["PagingCausesValidation"];
                return (o != null ? (bool)o : true);
            }
            set
            {
                ViewState["PagingCausesValidation"] = value;
            }
        }

        #endregion

        #region Life Cycle
        /// <summary>
        ///  This method is called in response to the edsGridView OnSorting event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (AllowMultiColumnSorting)
            {
                e.SortExpression = GetSortExpression(e);
            }

            base.OnSorting(e);
        }

        /// <summary>
        ///  This method is called in response to the edsGridView OnRowCreated event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (!DesignMode)
                {
                    this.DisplaySortOrderImages(SortExpression, e.Row);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("id", "row" + rowID.ToString());
                rowID++;
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                this.DisplayPagerControls(e.Row);
            }

            base.OnRowCreated(e);
        }

        /// <summary>
        ///  This method is called in response to the RowSize DDL SelectedIndexChanged event.
        /// </summary>
        /// <param name="e"></param>
        protected void RowSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDown = (DropDownList)sender;
            this.PageSize = int.Parse(dropDown.SelectedValue);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        ///  This method returns the Sort Expression by looking up the 
        ///  existing Grid View Sort Expression.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected string GetSortExpression(GridViewSortEventArgs e)
        {
            string[] sortColumns = null;
            string sortAttribute = SortExpression;

            //Check to See if we have an existing Sort Order already in the Grid View.	
            //If so get the Sort Columns into an array
            if (sortAttribute != String.Empty)
            {
                sortColumns = sortAttribute.Split(",".ToCharArray());
            }

            //if User clicked on the columns in the existing sort sequence.
            //Toggle the sort order or remove the column from sort appropriately

            if (sortAttribute.IndexOf(e.SortExpression) > 0 || sortAttribute.StartsWith(e.SortExpression))
                sortAttribute = ModifySortExpression(sortColumns, e.SortExpression);
            else
                sortAttribute += String.Concat(",", e.SortExpression, " ASC ");
            return sortAttribute.TrimStart(",".ToCharArray()).TrimEnd(",".ToCharArray());
        }

        /// <summary>
        ///  This method toggles the sort order or removes the column from the sort appropriately.
        /// </summary>
        /// <param name="sortColumns"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        protected string ModifySortExpression(string[] sortColumns, string sortExpression)
        {

            string ascSortExpression = String.Concat(sortExpression, " ASC ");
            string descSortExpression = String.Concat(sortExpression, " DESC ");

            for (int i = 0; i < sortColumns.Length; i++)
            {

                if (ascSortExpression.Equals(sortColumns[i]))
                {
                    sortColumns[i] = descSortExpression;
                }

                else if (descSortExpression.Equals(sortColumns[i]))
                {
                    Array.Clear(sortColumns, i, 1);
                }
            }

            return String.Join(",", sortColumns).Replace(",,", ",").TrimStart(",".ToCharArray());
        }

        /// <summary>
        ///  This method looks up the Current Sort Expression to determine the order of a specific item.
        /// </summary>
        /// <param name="sortColumns"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortOrderNo"></param>
        protected void SearchSortExpression(string[] sortColumns, string sortColumn, out string sortOrder, out int sortOrderNo)
        {
            sortOrder = "";
            sortOrderNo = -1;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                //if (sortColumns[i].StartsWith(sortColumn))
                if (sortColumns[i].Equals(sortColumn))
                {
                    sortOrderNo = i + 1;
                    if (AllowMultiColumnSorting)
                        sortOrder = sortColumns[i].Substring(sortColumn.Length).Trim();
                    else
                        sortOrder = ((SortDirection == SortDirection.Ascending) ? "ASC" : "DESC");
                }
            }
        }
        /// <summary>
        ///  This method displays a graphic image for the Sort Order.  If multicolumn sorting is
        ///  allowed, the sort sequence number is also displayed.
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="dgItem"></param>
        protected void DisplaySortOrderImages(string sortExpression, GridViewRow dgItem)
        {

            string[] sortColumns = sortExpression.Split(",".ToCharArray());

            for (int i = 0; i < dgItem.Cells.Count; i++)
            {
                if (dgItem.Cells[i].Controls.Count > 0 && dgItem.Cells[i].Controls[0] is LinkButton)
                {
                    string sortOrder;
                    int sortOrderNo;
                    string column = ((LinkButton)dgItem.Cells[i].Controls[0]).CommandArgument;
                    SearchSortExpression(sortColumns, column, out sortOrder, out sortOrderNo);

                    dgItem.Cells[i].CssClass = "noWrap";

                    Image imgSortDirection = new Image();
                    imgSortDirection.ImageAlign = ImageAlign.Middle;

                    if ((sortOrderNo > 0) && (sortOrder == "ASC"))
                    {
                        imgSortDirection.ImageUrl = edsGridView.SortAscImageUrl;
                        imgSortDirection.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.AscendingToolTip;
                    }
                    else if ((sortOrderNo > 0) && (sortOrder == "DESC"))
                    {
                        imgSortDirection.ImageUrl = edsGridView.SortDescImageUrl;
                        imgSortDirection.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.DescendingToolTip;
                    }
                    else
                    {
                        imgSortDirection.ImageUrl = edsGridView.ClearImageUrl;
                        imgSortDirection.ToolTip = String.Empty;
                    }

                    dgItem.Cells[i].Controls.Add(imgSortDirection);

                    if ((sortOrderNo > 0) && AllowMultiColumnSorting)
                    {
                        Label lblSortOrder = new Label();
                        lblSortOrder.Font.Size = FontUnit.Small;
                        lblSortOrder.Text = sortOrderNo.ToString();
                        dgItem.Cells[i].Controls.Add(lblSortOrder);
                    }
                }
            }
        }

        /// <summary>
        ///  This method displays the custom pager controls for the edsGridView.
        /// </summary>
        /// <param name="pagerRow"></param>
        protected void DisplayPagerControls(GridViewRow pagerRow)
        {
            // Create First Page button
            ImageButton btnFirst = new ImageButton();
            btnFirst.CausesValidation = this.PagingCausesValidation;
            btnFirst.CommandName = "Page";
            btnFirst.CommandArgument = "First";
            btnFirst.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.FirstPageToolTip;
            btnFirst.CssClass = "firstPage";
            btnFirst.ImageUrl = edsGridView.FirstPageImageUrl;
            btnFirst.ImageAlign = ImageAlign.AbsBottom;

            // Create Previous Page button
            ImageButton btnPrevious = new ImageButton();
            btnPrevious.CausesValidation = this.PagingCausesValidation;
            btnPrevious.CommandName = "Page";
            btnPrevious.CommandArgument = "Prev";
            btnPrevious.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.PreviousPageToolTip;
            btnPrevious.CssClass = "previousPage";
            btnPrevious.ImageUrl = edsGridView.PrevPageImageUrl;
            btnPrevious.ImageAlign = ImageAlign.AbsBottom;

            // Create page number label
            Label lblPageNum = new Label();
            int currentPage = this.PageIndex + 1;
            string pageText = EDS.Intranet.ServerControls.Properties.Resources.PageXofY;
            lblPageNum.Text = String.Format(pageText, currentPage.ToString(), this.PageCount.ToString());

            // Create Next Page button
            ImageButton btnNext = new ImageButton();
            btnNext.CausesValidation = this.PagingCausesValidation;
            btnNext.CommandName = "Page";
            btnNext.CommandArgument = "Next";
            btnNext.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.NextPageToolTip;
            btnNext.CssClass = "nextPage";
            btnNext.ImageUrl = edsGridView.NextPageImageUrl;
            btnNext.ImageAlign = ImageAlign.AbsBottom;

            // Create Last Page button
            ImageButton btnLast = new ImageButton();
            btnLast.CausesValidation = this.PagingCausesValidation;
            btnLast.CommandName = "Page";
            btnLast.CommandArgument = "Last";
            btnLast.ToolTip = EDS.Intranet.ServerControls.Properties.Resources.LastPageToolTip;
            btnLast.CssClass = "lastPage";
            btnLast.ImageUrl = edsGridView.LastPageImageUrl;
            btnLast.ImageAlign = ImageAlign.AbsBottom;

            // Create page row label
            Label lblRowNum = new Label();
            lblRowNum.Text = EDS.Intranet.ServerControls.Properties.Resources.RowsPerPage;

            // Create page row drop down list
            DropDownList ddlRowNum = new DropDownList();
            for (int i = 5; i <= 25; i = i + 5)
            {
                ListItem item = new ListItem(i.ToString());
                if (i == this.PageSize)
                {
                    item.Selected = true;
                }
                ddlRowNum.Items.Add(item);
            }

            ddlRowNum.SelectedIndexChanged += new System.EventHandler(this.RowSize_SelectedIndexChanged);
            ddlRowNum.AutoPostBack = true;

            // Add the created controls to the pager row
            pagerRow.Cells[0].Controls.Add(btnFirst);
            pagerRow.Cells[0].Controls.Add(btnPrevious);
            pagerRow.Cells[0].Controls.Add(lblPageNum);
            pagerRow.Cells[0].Controls.Add(btnNext);
            pagerRow.Cells[0].Controls.Add(btnLast);

            pagerRow.Cells[0].Controls.Add(lblRowNum);
            pagerRow.Cells[0].Controls.Add(ddlRowNum);
        }
        #endregion
    }
}
