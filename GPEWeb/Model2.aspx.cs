using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

namespace GPEWeb
{
    public partial class Model2 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["GPEDB"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateModelGridview();
                
                //InputTab.CssClass = "Clicked";
                //MainView.ActiveViewIndex = 0;

            }
            //else { gvModel.Visible = false; }

            //Page.MaintainScrollPositionOnPostBack  = true;

            //GridViewHelper helper = new GridViewHelper(this.gvModelItem);
            //helper.RegisterGroup("Group", true, true);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.Master.UpdateStatus("");
        }


        void PopulateModelTabViews(string ModelID)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {

                sqlCon.Open();
                //SqlParameter ModelIDParam = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                //ModelIDParam.Value = ModelID;


                SqlCommand cmdSel = new SqlCommand();
                cmdSel.Connection = sqlCon;
                cmdSel.CommandType = CommandType.StoredProcedure;
                //cmdSel.Parameters.Add(ModelIDParam);
                cmdSel.Parameters.AddWithValue("@ModelID", ModelID);

                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlDa.SelectCommand = cmdSel;

                //cmdSel.CommandText = "spGetModelInputs";
                //sqlDa.Fill(dtbl);
                //gvModelInput.DataSource = dtbl;
                //gvModelInput.DataBind();

                //dtbl.Clear();
                //cmdSel.CommandText = "spGetModelRates";
                //sqlDa.Fill(dtbl);
                //gvModelRate.DataSource = dtbl;
                //gvModelRate.DataBind();

                //dtbl.Clear();
                //cmdSel.CommandText = "spGetModelGroups";
                //sqlDa.Fill(dtbl);
                //gvModelGroup.DataSource = dtbl;
                //gvModelGroup.DataBind();

                SqlParameter pAccountingType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);
                cmdSel.Parameters.Add(pAccountingType);

                //dtbl.Clear();
                //cmdSel.CommandText = "spGetModelItems";
                //pAccountingType.Value = "R";
                //sqlDa.Fill(dtbl);
                //gvModelRevenueItem.DataSource = dtbl;
                //gvModelRevenueItem.DataBind();

                dtbl.Clear();
                cmdSel.CommandText = "spGetModelItems";
                pAccountingType.Value = "E";
                sqlDa.Fill(dtbl);
                gvModelExpenseItem.DataSource = dtbl;
                gvModelExpenseItem.DataBind();

                

                //GroupGridView(gvModelItem, "lblGroupName", 0);
                //GroupGridView(gvModelRate, "lblGroupName", 0);
            }

        }

       

        void PopulateModelGridview()
        {

            try
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
                    gvModel.DataSource = dtbl;
                    gvModel.DataBind();


                }
                if (dtbl.Rows.Count == 0)
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    gvModel.DataSource = dtbl;
                    gvModel.DataBind();
                    gvModel.Rows[0].Cells.Clear();
                    gvModel.Rows[0].Cells.Add(new TableCell());
                    gvModel.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    gvModel.Rows[0].Cells[0].Text = "No Data Found ..!";
                    gvModel.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }



        }

        protected void gvModel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        //string query = "INSERT INTO Model (FirstName,LastName,Contact,Email) VALUES (@FirstName,@LastName,@Contact,@Email)";
                        //SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        //sqlCmd.Parameters.AddWithValue("@FirstName", (gvModel.FooterRow.FindControl("txtFirstNameFooter") as TextBox).Text.Trim());
                        //sqlCmd.Parameters.AddWithValue("@LastName", (gvModel.FooterRow.FindControl("txtLastNameFooter") as TextBox).Text.Trim());
                        //sqlCmd.Parameters.AddWithValue("@Contact", (gvModel.FooterRow.FindControl("txtContactFooter") as TextBox).Text.Trim());
                        //sqlCmd.Parameters.AddWithValue("@Email", (gvModel.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
                        //sqlCmd.ExecuteNonQuery();

                        SqlParameter pModelName = new SqlParameter("@ModelName", SqlDbType.VarChar, 100);
                        SqlParameter pModelID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        //ModelIDParam.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();
                        pModelName.Value = (gvModel.FooterRow.FindControl("txtModelName") as TextBox).Text.Trim();
                        pModelID.Value = (gvModel.FooterRow.FindControl("ddlModel") as DropDownList).SelectedValue;


                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spUpdateModel";
                        sqlCmd.Parameters.Add(pModelName);
                        sqlCmd.Parameters.Add(pModelID);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelGridview();

                        this.Master.UpdateStatus("New Model Added Successfully");
                    }
                }

                if (e.CommandName.Equals("Duplicate"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {

                        sqlCon.Open();

                        SqlParameter ModelIDParam = new SqlParameter("@FromModelID", SqlDbType.Int, 4);
                        ModelIDParam.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();


                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spCopyModel";
                        sqlCmd.Parameters.Add(ModelIDParam);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelGridview();
                        PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                        this.Master.UpdateStatus("Model Duplicated Successfully");

                    }
                }

                if (e.CommandName.Equals("Calculate"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {

                        sqlCon.Open();

                        SqlParameter QuoteIDParam = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        QuoteIDParam.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();


                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.CommandText = "spCalculateModel";
                        sqlCmd.Parameters.Add(QuoteIDParam);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelGridview();
                        PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                        this.Master.UpdateStatus("Model Calculation Completed Successfully");

                    }
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }
        
        
        

        protected void gvModel_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModel.EditIndex = e.NewEditIndex;
            PopulateModelGridview();
            ((TextBox)gvModel.Rows[e.NewEditIndex].FindControl("txtModelName")).Focus();
            //((TextBox)gvModel.Rows[e.NewEditIndex].FindControl("txtModelDesc")).Style.Add("width", "400px");
            

        }

        protected void gvModel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModel.EditIndex = -1;
            PopulateModelGridview();
        }

        protected void gvModel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand();
                    
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spUpdateModel";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ModelName", (gvModel.Rows[e.RowIndex].FindControl("txtModelName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ModelId", Convert.ToInt32(gvModel.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvModel.EditIndex = -1;
                    PopulateModelGridview();
                    
                    this.Master.UpdateStatus("Model Record Updated");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message,"Error");
            }
        }

        protected void gvModel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
               
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModel";
                    sqlCmd.CommandType = CommandType.StoredProcedure; 
                    sqlCmd.Parameters.AddWithValue("@ModelID", Convert.ToInt32(gvModel.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvModel.SelectedIndex  = -1;
                    PopulateModelGridview();
                    PopulateModelTabViews("0");
                    this.Master.UpdateStatus("Selected Record Deleted");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }


        protected void Tab1_Click(object sender, EventArgs e)
        {

            //InputTab.CssClass = "Clicked";
            //RevenueItemsTab.CssClass = "Initial";
            //ExpenseItemsTab.CssClass = "Initial";
            //GroupsTab.CssClass = "Initial";
            //MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            //InputTab.CssClass = "Initial";
            //RevenueItemsTab.CssClass = "Clicked";
            //ExpenseItemsTab.CssClass = "Initial";
            //RatesTab.CssClass = "Initial";
            //GroupsTab.CssClass = "Initial";
            //MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            //InputTab.CssClass = "Initial";
            //RevenueItemsTab.CssClass = "Initial";
            //ExpenseItemsTab.CssClass = "Clicked";
            //RatesTab.CssClass = "Initial";
            //GroupsTab.CssClass = "Initial";
            //MainView.ActiveViewIndex = 2;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            //InputTab.CssClass = "Initial";
            //RevenueItemsTab.CssClass = "Initial";
            //ExpenseItemsTab.CssClass = "Initial";
            //RatesTab.CssClass = "Clicked";
            //GroupsTab.CssClass = "Initial";
            //MainView.ActiveViewIndex = 3;
        }
        protected void Tab5_Click(object sender, EventArgs e)
        {
            //InputTab.CssClass = "Initial";
            //RevenueItemsTab.CssClass = "Initial";
            //ExpenseItemsTab.CssClass = "Initial";
            //RatesTab.CssClass = "Initial";
            //GroupsTab.CssClass = "Clicked";
            //MainView.ActiveViewIndex = 4;
        }

        protected void gvModel_SelectedIndexChanged(object sender, EventArgs e)
        {


            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

        }

        protected void gvModelInput_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModelInput.EditIndex = e.NewEditIndex;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

            //((TextBox)gvModelInput.Rows[e.NewEditIndex].FindControl("txtInputValue")).Focus();
            TextBox tb = ((TextBox)gvModelInput.Rows[e.NewEditIndex].FindControl("txtInputValue"));
            tb.Focus();
            tb.Attributes.Add("onfocus", "this.select();");
        }

        protected void gvModelRevenueItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModelRevenueItem.EditIndex = e.NewEditIndex;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

            //((TextBox)gvModelItem.Rows[e.NewEditIndex].FindControl("txtItemName")).Focus();

            //TextBox tb = ((TextBox)gvModelRevenueItem.Rows[e.NewEditIndex].FindControl("txtRate"));
            //if (tb != null)
            //{
            //    tb.Focus();
            //    tb.Attributes.Add("onfocus", "this.select();");
            //}
        }

        protected void gvModelExpenseItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModelExpenseItem.EditIndex = e.NewEditIndex;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
            
            //((TextBox)gvModelItem.Rows[e.NewEditIndex].FindControl("txtItemName")).Focus();

            //TextBox tb = ((TextBox)gvModelExpenseItem.Rows[e.NewEditIndex].FindControl("txtRate"));
            //if (tb != null)
            //{
            //    tb.Focus();
            //    tb.Attributes.Add("onfocus", "this.select();");
            //}
        }

        protected void gvModelRate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModelRate.EditIndex = e.NewEditIndex;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
            ((DropDownList)gvModelRate.Rows[e.NewEditIndex].FindControl("ddlGroupName")).Focus();
        }

        protected void gvModelInput_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModelInput.EditIndex = -1;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
        }

        protected void gvModelInput_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlParameter pInputID = new SqlParameter("@InputID", SqlDbType.Int, 4);
                    SqlParameter pInputName = new SqlParameter("@InputName", SqlDbType.VarChar, 100);
                    SqlParameter pInputKey = new SqlParameter("@InputKey", SqlDbType.VarChar, 100);
                    SqlParameter pValue = new SqlParameter("@Value", SqlDbType.Int, 4);

                    pInputID.Value = (gvModelInput.DataKeys[e.RowIndex].Value.ToString());
                    pInputName.Value = (gvModelInput.Rows[e.RowIndex].FindControl("txtInputName") as TextBox).Text.Trim();
                    pInputKey.Value = (gvModelInput.Rows[e.RowIndex].FindControl("txtInputKey") as TextBox).Text.Trim();

                    string sValue = (gvModelInput.Rows[e.RowIndex].FindControl("txtInputValue") as TextBox).Text.Trim();
                    pValue.Value = sValue == "" ? "1": sValue;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "spUpdateModelInput";
                    sqlCmd.Parameters.Add(pInputID);
                    sqlCmd.Parameters.Add(pInputName);
                    sqlCmd.Parameters.Add(pInputKey);
                    sqlCmd.Parameters.Add(pValue);
                    sqlCmd.ExecuteNonQuery();

                    gvModelInput.EditIndex = -1;
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Input Updated Successfully");
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
        
        
        protected void gvModelRevenueItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (gvModelRevenueItem.EditIndex == e.Row.RowIndex && (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow))
            {
                DropDownList ddlGroup = (DropDownList)e.Row.FindControl("ddlGroupName");
                if (ddlGroup != null)
                {
                    SqlCommand cmdSel = new SqlCommand();
                    DataTable dtbl = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pModelID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pAccountingType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);
                        pModelID.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();

                        cmdSel.Connection = sqlCon;
                        cmdSel.CommandType = CommandType.StoredProcedure;
                        cmdSel.Parameters.Add(pModelID);
                        cmdSel.Parameters.Add(pAccountingType);

                        SqlDataAdapter sqlDa = new SqlDataAdapter();
                        sqlDa.SelectCommand = cmdSel;

                        dtbl.Clear();
                        pAccountingType.Value = "R";
                        cmdSel.CommandText = "spGetModelGroups";
                        sqlDa.Fill(dtbl);

                       
                        ddlGroup.DataTextField = "Name";
                        ddlGroup.DataValueField = "GroupID";
                        ddlGroup.DataSource = dtbl;
                        ddlGroup.DataBind();

                        ddlGroup.Items.Add(new ListItem("", "0"));

                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        if (dr!=null)
                            //ddlGroup.SelectedItem.Text = dr["GroupName"].ToString();
                            ddlGroup.SelectedValue = dr["GroupID"].ToString() == "" ? "0" : dr["GroupID"].ToString();
                            

                    }
                }
            }
            
        }

        protected void gvModelExpenseItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (gvModelExpenseItem.EditIndex == e.Row.RowIndex && (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow))
            {
                DropDownList ddlGroup = (DropDownList)e.Row.FindControl("ddlGroupName");
                if (ddlGroup != null)
                {
                    SqlCommand cmdSel = new SqlCommand();
                    DataTable dtbl = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pModelID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pAccountingType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);
                        pModelID.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();

                        cmdSel.Connection = sqlCon;
                        cmdSel.CommandType = CommandType.StoredProcedure;
                        cmdSel.Parameters.Add(pModelID);
                        cmdSel.Parameters.Add(pAccountingType);

                        SqlDataAdapter sqlDa = new SqlDataAdapter();
                        sqlDa.SelectCommand = cmdSel;

                        dtbl.Clear();                        
                        pAccountingType.Value = "E";
                        cmdSel.CommandText = "spGetModelGroups";
                        sqlDa.Fill(dtbl);

                        ddlGroup.DataTextField = "Name";
                        ddlGroup.DataValueField = "GroupID";
                        ddlGroup.DataSource = dtbl;
                        ddlGroup.DataBind();

                        ddlGroup.Items.Add(new ListItem("", "0"));

                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        if (dr != null)
                            //ddlGroup.SelectedItem.Text = dr["GroupName"].ToString();
                            ddlGroup.SelectedValue = dr["GroupID"].ToString() == "" ? "0" : dr["GroupID"].ToString();


                    }
                }
            }

        }

        protected void gvModelRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (gvModelRate.EditIndex == e.Row.RowIndex && (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow))
            {
                DropDownList ddlGroup = (DropDownList)e.Row.FindControl("ddlGroupName");
                if (ddlGroup != null)
                {
                    SqlCommand cmdSel = new SqlCommand();
                    DataTable dtbl = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pModelID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pAccountingType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);
                        pModelID.Value = gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString();
                        

                        cmdSel.Connection = sqlCon;
                        cmdSel.CommandType = CommandType.StoredProcedure;
                        cmdSel.Parameters.Add(pModelID);
                        cmdSel.Parameters.Add(pAccountingType);

                        SqlDataAdapter sqlDa = new SqlDataAdapter();
                        sqlDa.SelectCommand = cmdSel;

                        dtbl.Clear();
                        pAccountingType.Value = "X";
                        cmdSel.CommandText = "spGetModelGroups";
                        sqlDa.Fill(dtbl);

                        ddlGroup.DataTextField = "Name";
                        ddlGroup.DataValueField = "GroupID";
                        ddlGroup.DataSource = dtbl;
                        ddlGroup.DataBind();

                        ddlGroup.Items.Add(new ListItem("", "0"));

                        DataRowView dr = (DataRowView)e.Row.DataItem;
                        if (dr != null)
                            //ddlGroup.SelectedItem.Text = dr["GroupName"].ToString();
                            ddlGroup.SelectedValue = dr["GroupID"].ToString()==""?"0": dr["GroupID"].ToString();

                    }
                }
            }

        }

        protected void gvModelRate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            { 
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                                        
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateModelRate";
                    sqlCmd.Parameters.AddWithValue("@RateID", Convert.ToInt32(gvModelRate.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@GroupID", (gvModelRate.Rows[e.RowIndex].FindControl("ddlGroupName") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Name", (gvModelRate.Rows[e.RowIndex].FindControl("txtRateName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Key", (gvModelRate.Rows[e.RowIndex].FindControl("txtRateKey") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvModelRate.Rows[e.RowIndex].FindControl("txtRateValue") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    gvModelRate.EditIndex = -1;
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Model Rate Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelRate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModelRate.EditIndex = -1;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
        }

        protected void gvModel_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    DropDownList ddlModel = (DropDownList)e.Row.FindControl("ddlModel");

            //    SqlCommand cmdSel = new SqlCommand();
            //    DataTable dtbl = new DataTable();
            //    using (SqlConnection sqlCon = new SqlConnection(connectionString))
            //    {
            //        sqlCon.Open();

            //        cmdSel.Connection = sqlCon;
            //        cmdSel.CommandType = CommandType.StoredProcedure;
            //        SqlDataAdapter sqlDa = new SqlDataAdapter();
            //        sqlDa.SelectCommand = cmdSel;
            //        dtbl.Clear();
            //        cmdSel.CommandText = "spGetModel";
            //        sqlDa.Fill(dtbl);

            //        ddlModel.DataTextField = "Name";
            //        ddlModel.DataValueField = "ModelID";
            //        ddlModel.DataSource = dtbl;
            //        ddlModel.DataBind();

            //    }
            //}
        }

        protected void gvModelGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlParameter pID = new SqlParameter("@GroupID", SqlDbType.Int, 4);
                    SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                    SqlParameter pKey = new SqlParameter("@Key", SqlDbType.VarChar, 100);
                    SqlParameter pType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);

                    pID.Value = (Convert.ToInt32(gvModelGroup.DataKeys[e.RowIndex].Value.ToString()));
                    pName.Value = (gvModelGroup.Rows[e.RowIndex].FindControl("txtGroupName") as TextBox).Text.Trim();
                    pKey.Value = (gvModelGroup.Rows[e.RowIndex].FindControl("txtGroupKey") as TextBox).Text.Trim();
                    pType.Value = (gvModelGroup.Rows[e.RowIndex].FindControl("ddlAccountingType") as DropDownList).SelectedValue;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "spUpdateModelGroup";
                    sqlCmd.Parameters.Add(pID);
                    sqlCmd.Parameters.Add(pName);
                    sqlCmd.Parameters.Add(pKey);
                    sqlCmd.Parameters.Add(pType);
                    sqlCmd.ExecuteNonQuery();

                    gvModelGroup.EditIndex  = -1;
                    PopulateModelTabViews(pID.Value.ToString());

                    this.Master.UpdateStatus("New Group Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModelGroup.EditIndex = e.NewEditIndex;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

            ((TextBox)gvModelGroup.Rows[e.NewEditIndex].FindControl("txtGroupName")).Focus();
        }

        protected void gvModelGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModelGroup.EditIndex = -1;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
        }

        
        protected void gvModelExpenseItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModelExpenseItem.EditIndex = -1;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
        }

        protected void gvModelRevenueItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModelRevenueItem.EditIndex = -1;
            PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
        }

        protected void gvModelRevenueItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateModelItem";
                    sqlCmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(gvModelRevenueItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@GroupID", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("ddlGroupName") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Name", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("txtItemName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Key", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("txtItemKey") as TextBox).Text.Trim());
                    //sqlCmd.Parameters.AddWithValue("@Type", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("ddlAccountingType") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Type", "R");
                    sqlCmd.Parameters.AddWithValue("@Formula", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("txtFormula") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvModelRevenueItem.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    gvModelRevenueItem.EditIndex = -1;
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Model Item Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelExpenseItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateModelItem";
                    sqlCmd.Parameters.AddWithValue("@ModelID", Convert.ToInt32(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(gvModelExpenseItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@GroupID", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("ddlGroupName") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Name", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("txtItemName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Key", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("txtItemKey") as TextBox).Text.Trim());
                    //sqlCmd.Parameters.AddWithValue("@Type", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("ddlAccountingType") as DropDownList).SelectedValue);
                    sqlCmd.Parameters.AddWithValue("@Type", "E");
                    sqlCmd.Parameters.AddWithValue("@Formula", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("txtFormula") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Rate", (gvModelExpenseItem.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();

                    gvModelExpenseItem.EditIndex = -1;
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());

                    this.Master.UpdateStatus("Model Expense Item Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelInput_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();


                        SqlParameter pModelID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pInputName = new SqlParameter("@InputName", SqlDbType.VarChar, 100);
                        SqlParameter pInputKey = new SqlParameter("@InputKey", SqlDbType.VarChar, 100);
                        SqlParameter pValue = new SqlParameter("@Value", SqlDbType.Int, 4);

                        pModelID.Value = (gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                        pInputName.Value = (gvModelInput.FooterRow.FindControl("txtInputName") as TextBox).Text.Trim();
                        pInputKey.Value = (gvModelInput.FooterRow.FindControl("txtInputKey") as TextBox).Text.Trim();

                        string sValue = (gvModelInput.FooterRow.FindControl("txtInputValue") as TextBox).Text.Trim();
                        pValue.Value = sValue == "" ? "1" : sValue;

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "spUpdateModelInput";
                        sqlCmd.Parameters.Add(pModelID);
                        sqlCmd.Parameters.Add(pInputName);
                        sqlCmd.Parameters.Add(pInputKey);
                        sqlCmd.Parameters.Add(pValue);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelTabViews(pModelID.Value.ToString());

                        this.Master.UpdateStatus("New Input Added Successfully");
                    }
                }

                
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelInput_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModelInput";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@InputID", Convert.ToInt32(gvModelInput.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Model Input Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelRate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModelRate";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@RateID", Convert.ToInt32(gvModelRate.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Model Rate Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelExpenseItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModelItem";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(gvModelExpenseItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Model Expense Item Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelRevenueItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModelItem";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ItemID", Convert.ToInt32(gvModelRevenueItem.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Model Revenue Item Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }


        protected void gvModelGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        SqlParameter pKey = new SqlParameter("@Key", SqlDbType.VarChar, 100);
                        SqlParameter pType = new SqlParameter("@AccountingType", SqlDbType.VarChar, 1);

                        pID.Value = (gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                        pName.Value = (gvModelGroup.FooterRow.FindControl("txtGroupName") as TextBox).Text.Trim();
                        pKey.Value = (gvModelGroup.FooterRow.FindControl("txtGroupKey") as TextBox).Text.Trim();
                        pType.Value = (gvModelGroup.FooterRow.FindControl("ddlAccountingType") as DropDownList).SelectedValue;

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "spUpdateModelGroup";
                        sqlCmd.Parameters.Add(pID);
                        sqlCmd.Parameters.Add(pName);
                        sqlCmd.Parameters.Add(pKey);
                        sqlCmd.Parameters.Add(pType);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelTabViews(pID.Value.ToString());

                        this.Master.UpdateStatus("New Group Added Successfully");
                    }
                }


            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelRevenueItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pGrpID = new SqlParameter("@GroupID", SqlDbType.Int, 4);
                        SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        SqlParameter pKey = new SqlParameter("@Key", SqlDbType.VarChar, 100);
                        SqlParameter pType = new SqlParameter("@Type", SqlDbType.VarChar, 1);
                        SqlParameter pFormula = new SqlParameter("@Formula", SqlDbType.VarChar, 500);
                        SqlParameter pRate = new SqlParameter("@Rate", SqlDbType.VarChar, 100);

                            

                        pID.Value = (gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                        pName.Value = (gvModelRevenueItem.FooterRow.FindControl("txtItemName") as TextBox).Text.Trim();
                        pKey.Value = (gvModelRevenueItem.FooterRow.FindControl("txtItemKey") as TextBox).Text.Trim();
                        pGrpID.Value = (gvModelRevenueItem.FooterRow.FindControl("ddlGroupName") as DropDownList).SelectedValue;
                        pType.Value = "R"; //(gvModelRevenueItem.FooterRow.FindControl("ddlAccountingType") as DropDownList).SelectedValue;
                        pFormula.Value = (gvModelRevenueItem.FooterRow.FindControl("txtFormula") as TextBox).Text.Trim();
                        pRate.Value = (gvModelRevenueItem.FooterRow.FindControl("txtRate") as TextBox).Text.Trim();

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "spUpdateModelItem";
                        sqlCmd.Parameters.Add(pID);
                        sqlCmd.Parameters.Add(pGrpID);
                        sqlCmd.Parameters.Add(pName);
                        sqlCmd.Parameters.Add(pType);
                        sqlCmd.Parameters.Add(pKey);
                        sqlCmd.Parameters.Add(pFormula);
                        sqlCmd.Parameters.Add(pRate);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelTabViews(pID.Value.ToString());

                        this.Master.UpdateStatus("New Item Added Successfully");
                    }
                }


            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelExpenseItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pGrpID = new SqlParameter("@GroupID", SqlDbType.Int, 4);
                        SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        SqlParameter pKey = new SqlParameter("@Key", SqlDbType.VarChar, 100);
                        SqlParameter pType = new SqlParameter("@Type", SqlDbType.VarChar, 1);
                        SqlParameter pFormula = new SqlParameter("@Formula", SqlDbType.VarChar, 500);
                        SqlParameter pRate = new SqlParameter("@Rate", SqlDbType.VarChar, 100);



                        pID.Value = (gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                        pName.Value = (gvModelExpenseItem.FooterRow.FindControl("txtItemName") as TextBox).Text.Trim();
                        pKey.Value = (gvModelExpenseItem.FooterRow.FindControl("txtItemKey") as TextBox).Text.Trim();
                        pGrpID.Value = (gvModelExpenseItem.FooterRow.FindControl("ddlGroupName") as DropDownList).SelectedValue;
                        pType.Value = "E"; //(gvModelExpenseItem.FooterRow.FindControl("ddlAccountingType") as DropDownList).SelectedValue;
                        pFormula.Value = (gvModelExpenseItem.FooterRow.FindControl("txtFormula") as TextBox).Text.Trim();
                        pRate.Value = (gvModelExpenseItem.FooterRow.FindControl("txtRate") as TextBox).Text.Trim();

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "spUpdateModelItem";
                        sqlCmd.Parameters.Add(pID);
                        sqlCmd.Parameters.Add(pGrpID);
                        sqlCmd.Parameters.Add(pName);
                        sqlCmd.Parameters.Add(pType);
                        sqlCmd.Parameters.Add(pKey);
                        sqlCmd.Parameters.Add(pFormula);
                        sqlCmd.Parameters.Add(pRate);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelTabViews(pID.Value.ToString());

                        this.Master.UpdateStatus("New Expense Item Added Successfully");
                    }
                }


            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelRate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();

                        SqlParameter pID = new SqlParameter("@ModelID", SqlDbType.Int, 4);
                        SqlParameter pGrpID = new SqlParameter("@GroupID", SqlDbType.Int, 4);
                        SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        SqlParameter pKey = new SqlParameter("@Key", SqlDbType.VarChar, 100);
                        SqlParameter pValue = new SqlParameter("@Rate", SqlDbType.Decimal);

                        pID.Value = (gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                        pName.Value = (gvModelRate.FooterRow.FindControl("txtRateName") as TextBox).Text.Trim();
                        pKey.Value = (gvModelRate.FooterRow.FindControl("txtRateKey") as TextBox).Text.Trim();
                        pGrpID.Value = (gvModelRate.FooterRow.FindControl("ddlGroupName") as DropDownList).SelectedValue;
                        pValue.Value = (gvModelRate.FooterRow.FindControl("txtRateValue") as TextBox).Text.Trim();

                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.CommandText = "spUpdateModelRate";
                        sqlCmd.Parameters.Add(pID);
                        sqlCmd.Parameters.Add(pName);
                        sqlCmd.Parameters.Add(pGrpID);
                        sqlCmd.Parameters.Add(pKey);
                        sqlCmd.Parameters.Add(pValue);
                        sqlCmd.ExecuteNonQuery();

                        PopulateModelTabViews(pID.Value.ToString());

                        this.Master.UpdateStatus("New Group Added Successfully");
                    }
                }


            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

        protected void gvModelGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteModelGroup";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@GroupID", Convert.ToInt32(gvModelGroup.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateModelTabViews(gvModel.DataKeys[gvModel.SelectedIndex].Value.ToString());
                    this.Master.UpdateStatus("Selected Group Deleted");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }

    }
}