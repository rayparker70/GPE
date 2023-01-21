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
    public partial class Customer : System.Web.UI.Page
    {
        //string connectionString = @"Data Source=rparker-pc;Integrated Security=true;Initial Catalog=GPE";
        string connectionString = ConfigurationManager.ConnectionStrings["GPEDB"].ConnectionString;

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            this.PopulateCustomerGridview(e.SortExpression);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCustomerGridview();
                
            }

            

            //GridViewHelper helper = new GridViewHelper(this.gvModelItem);
            //helper.RegisterGroup("Group", true, true);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.Master.UpdateStatus("");
        }


        
       

        void PopulateCustomerGridview(string sortExpression = null)
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

                cmdSel.CommandText = "spGetCustomer";
                sqlDa.Fill(dtbl);
                //gvCustomer.DataSource = dtbl;

                if (sortExpression != null)
                {
                    DataView dv = dtbl.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvCustomer.DataSource = dv;
                }
                else
                {
                    gvCustomer.DataSource = dtbl;
                }

                gvCustomer.DataBind();

  
            }
            if (dtbl.Rows.Count == 0)
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvCustomer.DataSource = dtbl;
                gvCustomer.DataBind();
                gvCustomer.Rows[0].Cells.Clear();
                gvCustomer.Rows[0].Cells.Add(new TableCell());
                gvCustomer.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvCustomer.Rows[0].Cells[0].Text = "No Data Found ..!";
                gvCustomer.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

            

        }

        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
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


                        sqlCmd.CommandText = "spUpdateCustomer";
                        sqlCmd.Parameters.AddWithValue("@CustomerName", (gvCustomer.FooterRow.FindControl("txtCustomerName") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ContactName", (gvCustomer.FooterRow.FindControl("txtContactName") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ContactPhone", (gvCustomer.FooterRow.FindControl("txtContactPhone") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ContactEmail", (gvCustomer.FooterRow.FindControl("txtContactEmail") as TextBox).Text.Trim());

                        sqlCmd.ExecuteNonQuery();

                        PopulateCustomerGridview();

                        this.Master.UpdateStatus("New Model Added Successfully");
                    }
                }

                
                
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message,"Error");
            }
        }
        
        
        

        protected void gvCustomer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomer.EditIndex = e.NewEditIndex;
            PopulateCustomerGridview();
            ((TextBox)gvCustomer.Rows[e.NewEditIndex].FindControl("txtCustomerName")).Focus();
            //((TextBox)gvCustomer.Rows[e.NewEditIndex].FindControl("txtModelDesc")).Style.Add("width", "400px");
            

        }

        protected void gvCustomer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomer.EditIndex = -1;
            PopulateCustomerGridview();
        }

        protected void gvCustomer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand();

                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "spUpdateCustomer";
                    sqlCmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(gvCustomer.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@CustomerName", (gvCustomer.Rows[e.RowIndex].FindControl("txtCustomerName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ContactName", (gvCustomer.Rows[e.RowIndex].FindControl("txtContactName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ContactPhone", (gvCustomer.Rows[e.RowIndex].FindControl("txtContactPhone") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ContactEmail", (gvCustomer.Rows[e.RowIndex].FindControl("txtContactEmail") as TextBox).Text.Trim());

                    sqlCmd.ExecuteNonQuery();

                    
                    gvCustomer.EditIndex = -1;
                    PopulateCustomerGridview();
                    
                    this.Master.UpdateStatus("Customer Record Updated");

                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message,"Error");
            }
        }

        protected void gvCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spDeleteCustomer";
                    sqlCmd.CommandType = CommandType.StoredProcedure; 
                    sqlCmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(gvCustomer.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateCustomerGridview();
                    this.Master.UpdateStatus("Customer Record Deleted");
                }
            }
            catch (Exception ex)
            {
                this.Master.UpdateStatus(ex.Message, "Error");
            }
        }


        protected void gvCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {


            //PopulateModelTabViews(gvCustomer.DataKeys[gvCustomer.SelectedIndex].Value.ToString());

        }

        
    }
}