using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class TransactionDetailDAL
    {
        // Create Connection String
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(TransactionDetailBLL td)
        {
            // Create a boolean value and set its default value to false
            bool isSuccess = false;

            // Create a database connection here
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    // SQL Query to Insert Transaction details
                    string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                    // Passing the value to the SQL Query
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Passing the values using cmd
                        cmd.Parameters.AddWithValue("@product_id", td.product_id);
                        cmd.Parameters.AddWithValue("@rate", td.rate);
                        cmd.Parameters.AddWithValue("@qty", td.qty);
                        cmd.Parameters.AddWithValue("@total", td.total);
                        cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                        cmd.Parameters.AddWithValue("@added_date", td.added_date);
                        cmd.Parameters.AddWithValue("@added_by", td.added_by);

                        // Open Database connection
                        conn.Open();

                        // Declare the int variable and execute the query
                        int rows = cmd.ExecuteNonQuery();

                        isSuccess = rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

