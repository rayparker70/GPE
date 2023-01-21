using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace GPEWeb
{
    public partial class SiteMaster : MasterPage
    {
        string connectionString = ConfigurationManager.ConnectionStrings["GPEDB"].ConnectionString;

        public void UpdateStatus(string Message, string Status = "Success")
        {

            
            if (Message == "")
                lblStatus.CssClass = "";
            else if (Status == "Error")
                lblStatus.CssClass = "alert alert-danger";
            else if (Status == "Success")
                lblStatus.CssClass = "alert alert-success";


            lblStatus.Text = Message;

        }
        public void UpdateMainPanel()
        {
            MainUP.Update();
        }

        public void ShowComment(string aID, string aType, string aItem)
        {
            

            CommentID.Value = aID.ToString();
            CommentType.Value = aType.ToString();

            if (aType == "Quote")
                CommentDesc.Text = "Estimate : ";

            if (aType == "Input")
                CommentDesc.Text = "Customer Data : ";

            if (aType == "Revenue")
            {
                CommentDesc.Text = "Revenue : ";
            }
            if (aType == "Expense")
            {
                CommentDesc.Text = "Expense : ";
            }
            if (aType == "Rate")
            {
                CommentDesc.Text = "Constant : ";
            }

            CommentDesc.Text += aItem;

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandText = "spGetUserComment";
                sqlCmd.Parameters.AddWithValue("@aID", CommentID.Value);
                sqlCmd.Parameters.AddWithValue("@aType", CommentType.Value);
                sqlCmd.Parameters.Add("@aComment", SqlDbType.VarChar,5000).Direction = ParameterDirection.Output;
                sqlCmd.ExecuteNonQuery();

                txtComment.Text = sqlCmd.Parameters["@aComment"].Value.ToString();

            }

            
            //txtComment.Text = aComment;
            CommentModal.Show();
        }

        protected void btnSaveComment_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandText = "spUpdateUserComment";
                    sqlCmd.Parameters.AddWithValue("@aID", CommentID.Value);
                    sqlCmd.Parameters.AddWithValue("@aType", CommentType.Value);
                    sqlCmd.Parameters.AddWithValue("@aComment", txtComment.Text);
                    sqlCmd.ExecuteNonQuery();


                    UpdateStatus("Comment Updated Successfully");

                }
            }
            catch (Exception ex)
            {
                UpdateStatus(ex.Message, "Error");
            }
        }
    
    

    protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}