using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class TransactionDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method
        public bool InsertTransaction(TransactionBLL t, out int transactionID)
        {
            bool isSuccess = false;
            transactionID = -1;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", t.type);
                        cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                        cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                        cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                        cmd.Parameters.AddWithValue("@tax", t.tax);
                        cmd.Parameters.AddWithValue("@discount", t.discount);
                        cmd.Parameters.AddWithValue("@added_by", t.added_by);

                        conn.Open();

                        object o = cmd.ExecuteScalar();

                        if (o != null)
                        {
                            transactionID = Convert.ToInt32(o);
                            isSuccess = true;
                        }
                        else
                        {
                            isSuccess = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return isSuccess;
        }
        #endregion

        #region Method to Display All Transactions
        public DataTable DisplayAllTransactions()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_transactions";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        conn.Open();
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dt;
        }
        #endregion

        #region Method to Display Transactions Based on Transaction Type
        public DataTable DisplayTransactionByType(string type)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_transactions WHERE type=@type";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", type);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        conn.Open();
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dt;
        }
        #endregion
    }
}
