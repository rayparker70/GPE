using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Configuration;
//using System.Reflection.Emit;

namespace GPEWeb
{
    public partial class Quote : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["GPEDB"].ConnectionString;
        
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            this.PopulateQuoteGridview(e.SortExpression);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateQuoteGridview();
                
                InputTab.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;

            }

            

            //GridViewHelper helper = new GridViewHelper(this.gvQuoteItem);
            //helper.RegisterGroup("Group", true, true);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.Master.UpdateStatus("");
        }


        void PopulateQuoteTabViews(string QuoteID)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                SqlParameter QuoteIDParam = new SqlParameter("@QuoteID", SqlDbType.Int, 4);
                QuoteIDParam.Value = QuoteID;


                SqlCommand cmdSel = new SqlCommand();
                cmdSel.Connection = sqlCon;
                cmdSel.CommandType = CommandType.StoredProcedure;
                cmdSel.Parameters.Add(QuoteIDParam);

                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlDa.SelectCommand = cmdSel;

                cmdSel.CommandText = "spGetQuoteInputs";
                sqlDa.Fill(dtbl);
                gvQuoteInput.DataSource = dtbl;
                gvQuoteInput.DataBind();

                dtbl.Clear();
                cmdSel.CommandText = "spGetQuoteRates";
                sqlDa.Fill(dtbl);
                gvQuoteRate.DataSource = dtbl;
                gvQuoteRate.DataBind();

                SqlParameter pAccountingType = new SqlParameter("@AccountingType",SqlDbType.VarChar,1);
                cmdSel.Parameters.Add(pAccountingType);

                dtbl.Clear();
                cmdSel.CommandText = "spGetQuoteItems";
                pAccountingType.Value = "E";
                sqlDa.Fill(dtbl);
                gvQuoteExpenseItem.DataSource = dtbl;
                gvQuoteExpenseItem.DataBind();

                dtbl.Clear();
                cmdSel.CommandText = "spGetQuoteItems";
                pAccountingType.Value = "R";
                sqlDa.Fill(dtbl);
                gvQuoteRevenueItem.DataSource = dtbl;
                gvQuoteRevenueItem.DataBind();


                GroupGridView(gvQuoteRevenueItem, "lblGroupName", 0);
                GroupGridView(gvQuoteExpenseItem, "lblGroupName", 0);
                GroupGridView(gvQuoteRate, "lblGroupName", 0);
            }

        }

       

        void PopulateQuoteGridview(string sortExpression = null)
        {

            try
            {
                //Store Previously selected Quote ID
                string QuoteId = "";
                if (gvQuote.SelectedIndex >= 0) 
                { 
                    QuoteId = gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString();  
                }

                

                SqlCommand cmdSel = new SqlCommand();
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    cmdSel.Connection = sqlCon;
                    cmdSel.CommandType = CommandType.StoredProcedure;


                    SqlDataAdapter sqlDa = new SqlDataAdapter();
                    sqlDa.SelectCommand = cmdSel;

                    cmdSel.CommandText = "spGetQuote";
                    sqlDa.Fill(dtbl);
                    //gvQuote.DataSource = dtbl;

                    if (sortExpression != null)
                    {
                        DataView dv = dtbl.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvQuote.DataSource = dv;

                    }
                    else
                    {
                        gvQuote.DataSource = dtbl;
                    }

                    gvQuote.DataBind();

                    //Select Previously selected QuoteId after sort
                    gvQuote.SelectedIndex = GetKeyIndexByQuoteId(QuoteId);


                }
                if (dtbl.Rows.Count == 0)
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    gvQuote.DataSource = dtbl;
                    gvQuote.DataBind();
                    gvQuote.Rows[0].Cells.Clear();
                    gvQuote.Rows[0].Cells.Add(new TableCell());
                    gvQuote.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    gvQuote.Rows[0].Cells[0].Text = "No Data Found ..!";
                    gvQuote.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }



        }

        private int GetKeyIndexByQuoteId(string QuoteId)
        {
            int i = 0;
            foreach (DataKey key in gvQuote.DataKeys)
            {
                if (key.Value.ToString() == QuoteId)
                {
                    return i;
                }
                i++;
            }

            return -1;
            //DataKeyArray theKeys = CustomerGridView.DataKeys;
            //DataKey[] myNewArray = new DataKey[theKeys.Count];
            //theKeys.CopyTo(myNewArray, 0);
            //Label2.Visible = true;

            //// Display first page key values from the new array.
            //for (int i = 0; i < myNewArray.Length; i++)
            //{
            //    Label2.Text += "<br />" + myNewArray[i].Value;
            //}
        }

        protected void gvQuoteInput_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                

                if (e.CommandName.Equals("Comment"))
                {
                    GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    string aID = (gvQuoteInput.DataKeys[RowIndex].Value.ToString());
                    string aItem = (gvQuoteInput.Rows[RowIndex].FindControl("lblInputName") as Label).Text.Trim();

                    this.Master.ShowComment(aID, "Input", aItem);
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteRevenueItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName.Equals("Comment"))
                {

                    GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    string aID = (gvQuoteRevenueItem.DataKeys[RowIndex].Value.ToString());
                    string aItem = (gvQuoteRevenueItem.Rows[RowIndex].FindControl("lblItemName") as Label).Text.Trim();

                    this.Master.ShowComment(aID, "Revenue", aItem);
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteExpenseItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
            
                if (e.CommandName.Equals("Comment"))
                {

                    GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    string aID = (gvQuoteExpenseItem.DataKeys[RowIndex].Value.ToString());
                    string aItem = (gvQuoteExpenseItem.Rows[RowIndex].FindControl("lblItemName") as Label).Text.Trim();
                    
                    this.Master.ShowComment(aID, "Expense", aItem);
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteRate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName.Equals("Comment"))
                {

                    GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    string aID = (gvQuoteRate.DataKeys[RowIndex].Value.ToString());
                    string aItem = (gvQuoteRate.Rows[RowIndex].FindControl("lblRateName") as Label).Text.Trim();

                    this.Master.ShowComment(aID, "Rate", aItem);
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuote_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spUpdateQuote";
                        sqlCmd.Parameters.AddWithValue("@QuoteDesc", (gvQuote.FooterRow.FindControl("txtQuoteDesc") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ModelID", (gvQuote.FooterRow.FindControl("ddlModel") as DropDownList).SelectedValue);
                        sqlCmd.Parameters.AddWithValue("@CustomerID", (gvQuote.FooterRow.FindControl("ddlCustomer") as DropDownList).SelectedValue);
                        sqlCmd.ExecuteNonQuery();

                        PopulateQuoteGridview();

                        this.Master.UpdateStatus("New Quote Added Successfully");
                    }
                }

                if (e.CommandName.Equals("Duplicate"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {

                        sqlCon.Open();

                        SqlParameter QuoteIDParam = new SqlParameter("@FromQuoteID", SqlDbType.Int, 4);
                        QuoteIDParam.Value = gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString();


                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spCopyQuote";
                        sqlCmd.Parameters.Add(QuoteIDParam);
                        sqlCmd.ExecuteNonQuery();

                        PopulateQuoteGridview();
                        PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

                        this.Master.UpdateStatus("Quote Duplicated Successfully");

                    }
                }

                if (e.CommandName.Equals("Calculate"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {

                        sqlCon.Open();

                        SqlParameter QuoteIDParam = new SqlParameter("@QuoteID", SqlDbType.Int, 4);
                        QuoteIDParam.Value = gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString();


                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spCalculateQuote";
                        sqlCmd.Parameters.Add(QuoteIDParam);
                        sqlCmd.ExecuteNonQuery();

                        PopulateQuoteGridview();
                        PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

                        this.Master.UpdateStatus("Quote Calculation Completed Successfully");

                    }

                }
                if (e.CommandName.Equals("Comment"))
                {

                    GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    string aID = (gvQuote.DataKeys[RowIndex].Value.ToString());
                    string aItem = (gvQuote.Rows[RowIndex].FindControl("txtQuoteDesc") as Label).Text.Trim();

                    this.Master.ShowComment(aID, "Quote", aItem);
                }
                if (e.CommandName.Equals("Report"))
                {

                    Response.Write("<script type='text/javascript'>window.open('http://rparker-pc:8888/ReportServer/Pages/ReportViewer.aspx?/QuoteDetails&QuoteID=" + gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString() + "','_blank');</script>");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }
        
               

        protected void gvQuote_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuote.EditIndex = e.NewEditIndex;
            PopulateQuoteGridview();
            ((TextBox)gvQuote.Rows[e.NewEditIndex].FindControl("txtQuoteDesc")).Focus();
            //((TextBox)gvQuote.Rows[e.NewEditIndex].FindControl("txtQuoteDesc")).Style.Add("width", "400px");
            

        }

        protected void gvQuote_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuote.EditIndex = -1;
            PopulateQuoteGridview();
        }

        protected void gvQuote_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    //SqlParameter pQuoteDesc = new SqlParameter("@QuoteDesc", SqlDbType.VarChar, 100);
                    //SqlParameter pQuoteID = new SqlParameter("@QuoteID", SqlDbType.Int, 4);
                    //pQuoteDesc.Value = (gvQuote.Rows[e.RowIndex].FindControl("txtQuoteDesc") as TextBox).Text.Trim();
                    //pQuoteID.Value = Convert.ToInt32(gvQuote.DataKeys[e.RowIndex].Value.ToString());
                    //sqlCmd.Parameters.Add(pQuoteDesc);
                    //sqlCmd.Parameters.Add(pQuoteID);

                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spUpdateQuote";                    
                    sqlCmd.Parameters.AddWithValue("@QuoteDesc", (gvQuote.Rows[e.RowIndex].FindControl("txtQuoteDesc") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CustomerID", (gvQuote.Rows[e.RowIndex].FindControl("ddlCustomer") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Status", (gvQuote.Rows[e.RowIndex].FindControl("ddlStatus") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@QuoteID",Convert.ToInt32(gvQuote.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();

                    gvQuote.EditIndex = -1;
                    PopulateQuoteGridview();
                    
                    this.Master.UpdateStatus("Estimate Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message,"Error");
            }
        }

        protected void gvQuote_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteQuote";
                    sqlCmd.CommandType = CommandType.StoredProcedure; 
                    sqlCmd.Parameters.AddWithValue("@QuoteID", Convert.ToInt32(gvQuote.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvQuote.SelectedIndex  = -1;
                    PopulateQuoteGridview();
                    PopulateQuoteTabViews("0");
                    this.Master.UpdateStatus("Estimate Record Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void Invoice_Click(object sender, EventArgs e)
        {

             Response.Write("<script type='text/javascript'>window.open('http://rparker-pc:8888/ReportServer/Pages/ReportViewer.aspx?/QuoteInvoice&QuoteID=" + gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString() + "','_blank');</script>");

        }

            protected void Tab1_Click(object sender, EventArgs e)
        {

            InputTab.CssClass = "Clicked";
            RevenueItemsTab.CssClass = "Initial";
            ExpenseItemsTab.CssClass = "Initial";
            RatesTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            InputTab.CssClass = "Initial";
            RevenueItemsTab.CssClass = "Clicked";
            ExpenseItemsTab.CssClass = "Initial";
            RatesTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            InputTab.CssClass = "Initial";
            RevenueItemsTab.CssClass = "Initial";
            ExpenseItemsTab.CssClass = "Clicked";
            RatesTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            InputTab.CssClass = "Initial";
            RevenueItemsTab.CssClass = "Initial";
            ExpenseItemsTab.CssClass = "Initial";
            RatesTab.CssClass = "Clicked";
            MainView.ActiveViewIndex = 3;
        }

        protected void gvQuote_SelectedIndexChanged(object sender, EventArgs e)
        {
            var QuoteId = gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString();
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

            
            var CustomerName = (gvQuote.Rows[gvQuote.SelectedIndex].FindControl("lblCustomer") as Label).Text;
            var QuoteDesc = (gvQuote.Rows[gvQuote.SelectedIndex].FindControl("txtQuoteDesc") as Label).Text;
            lblQuoteTitle.Text = QuoteId + " - " + CustomerName + " - " + QuoteDesc;

        }

        protected void gvQuoteInput_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuoteInput.EditIndex = e.NewEditIndex;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

            //((TextBox)gvQuoteInput.Rows[e.NewEditIndex].FindControl("txtInputValue")).Focus();
        }

        protected void gvQuoteItem_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvQuoteRate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuoteRate.EditIndex = e.NewEditIndex;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
            //((TextBox)gvQuoteRate.Rows[e.NewEditIndex].FindControl("txtRate")).Focus();
        }

        protected void gvQuoteInput_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuoteInput.EditIndex = -1;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
        }

        protected void gvQuoteInput_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE QuoteInput SET Value=@Value WHERE QInputID = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Value", (gvQuoteInput.Rows[e.RowIndex].FindControl("txtInputValue") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvQuoteInput.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvQuoteInput.EditIndex = -1;
                    PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Quote Input Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message,"Error");
            }
        }

        void GroupGridView(GridView gv, string GroupName, int GroupColIndex)
        {
            for (int i = gv.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gv.Rows[i];
                GridViewRow previousRow = gv.Rows[i - 1];

                Label GroupLabel = (Label)row.FindControl(GroupName);
                Label PrevGroupLabel = (Label)previousRow.FindControl(GroupName);

                if (GroupLabel.Text == PrevGroupLabel.Text)
                {

                    if (previousRow.Cells[GroupColIndex].RowSpan == 0)
                    {
                        if (row.Cells[GroupColIndex].RowSpan == 0)
                        {
                            previousRow.Cells[GroupColIndex].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[GroupColIndex].RowSpan = row.Cells[GroupColIndex].RowSpan + 1;
                        }
                        row.Cells[GroupColIndex].Visible = false;
                    }

                    GroupLabel.Visible = false;
                    GroupLabel.Text = string.Empty;
                }
            }
        }

        protected void gvQuoteRevenueItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuoteRevenueItem.EditIndex = e.NewEditIndex;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
            //((TextBox)gvQuoteRevenueItem.Rows[e.NewEditIndex].FindControl("txtRate")).Focus();
        }


        protected void gvQuoteRevenueItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuoteRevenueItem.EditIndex = -1;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
        }

        protected void gvQuoteRevenueItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateQuoteItem";
                    sqlCmd.Parameters.AddWithValue("@QuoteID", Convert.ToInt32(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@QItemID", Convert.ToInt32(gvQuoteRevenueItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvQuoteRevenueItem.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    gvQuoteRevenueItem.EditIndex = -1;
                    PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Revenue Item Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteExpenseItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuoteExpenseItem.EditIndex = e.NewEditIndex;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
            //((TextBox)gvQuoteExpenseItem.Rows[e.NewEditIndex].FindControl("txtRate")).Focus();
        }


        protected void gvQuoteExpenseItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuoteExpenseItem.EditIndex = -1;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
        }

        protected void gvQuoteExpenseItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateQuoteItem";
                    sqlCmd.Parameters.AddWithValue("@QuoteID", Convert.ToInt32(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@QItemID", Convert.ToInt32(gvQuoteExpenseItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvQuoteExpenseItem.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    gvQuoteExpenseItem.EditIndex = -1;
                    PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Expense Item Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label titleLabel = (Label)e.Row.FindControl("lblGroupName");
            //    string strval = ((Label)(titleLabel)).Text;
            //    string title = (string)ViewState["title"];
            //    if (title == strval)
            //    {
            //        titleLabel.Visible = false;
            //        titleLabel.Text = string.Empty;
            //    }
            //    else
            //    {
            //        title = strval;
            //        ViewState["title"] = title;
            //        titleLabel.Visible = true;
            //        titleLabel.Text = "<br><b>" + title + "</b><br>";
            //    }
            //}
        }

        protected void gvQuoteRate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            { 
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateQuoteRate";
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvQuoteRate.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@QRateID", Convert.ToInt32(gvQuoteRate.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();

                    gvQuoteRate.EditIndex = -1;
                    PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Quote Rate Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvQuoteRate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuoteRate.EditIndex = -1;
            PopulateQuoteTabViews(gvQuote.DataKeys[gvQuote.SelectedIndex].Value.ToString());
        }

        protected void gvQuote_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (gvQuote.EditIndex == e.Row.RowIndex && (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow))
            {
                DropDownList ddlModel = (DropDownList)e.Row.FindControl("ddlModel");
                if (ddlModel != null)
                {
                    SqlCommand cmdSel = new SqlCommand();
                    DataTable dtbl = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        cmdSel.Connection = sqlCon;
                        cmdSel.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDa = new SqlDataAdapter();
                        sqlDa.SelectCommand = cmdSel;
                        cmdSel.CommandText = "spGetModel";
                        sqlDa.Fill(dtbl);

                        ddlModel.DataTextField = "Name";
                        ddlModel.DataValueField = "ModelID";
                        ddlModel.DataSource = dtbl;
                        ddlModel.DataBind();

                    }
                }
                DropDownList ddlCustomer = (DropDownList)e.Row.FindControl("ddlCustomer");
                if (ddlCustomer != null)
                {
                    SqlCommand cmdSel = new SqlCommand();
                    DataTable dtbl = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        cmdSel.Connection = sqlCon;
                        cmdSel.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDa = new SqlDataAdapter();
                        sqlDa.SelectCommand = cmdSel;
                        dtbl.Clear();
                        cmdSel.CommandText = "spGetCustomer";
                        sqlDa.Fill(dtbl);

                        ddlCustomer.DataTextField = "Name";
                        ddlCustomer.DataValueField = "CustomerID";
                        ddlCustomer.DataSource = dtbl;
                        ddlCustomer.DataBind();

                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        if (dr != null)
                            //ddlCustomer.SelectedItem.Text = dr["CustomerName"].ToString();
                            ddlCustomer.SelectedValue = dr["CustomerID"].ToString();

                    }
                }

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("Status");
                if (ddlStatus != null)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;
                    if (dr != null)
                        //ddlGroup.SelectedItem.Text = dr["GroupName"].ToString();
                        ddlStatus.SelectedValue = dr["Status"].ToString();
                }

            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvQuoteInput.Rows)
            {
                if (row.RowIndex == gvQuoteInput.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }


    }
}