using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class DeaCustDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT Method for Dealer and Customer
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_dea_cust";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        connection.Open();
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dataTable;
        }
        #endregion

        #region INSERT Method to Add details for Dealer or Customer
        public bool Insert(DeaCustBLL dc)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@type", dc.type);
                        command.Parameters.AddWithValue("@name", dc.name);
                        command.Parameters.AddWithValue("@email", dc.email);
                        command.Parameters.AddWithValue("@contact", dc.contact);
                        command.Parameters.AddWithValue("@address", dc.address);
                        command.Parameters.AddWithValue("@added_date", dc.added_date);
                        command.Parameters.AddWithValue("@added_by", dc.added_by);

                        connection.Open();
                        int rows = command.ExecuteNonQuery();

                        isSuccess = (rows > 0);
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

        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@type", dc.type);
                        command.Parameters.AddWithValue("@name", dc.name);
                        command.Parameters.AddWithValue("@email", dc.email);
                        command.Parameters.AddWithValue("@contact", dc.contact);
                        command.Parameters.AddWithValue("@address", dc.address);
                        command.Parameters.AddWithValue("@added_date", dc.added_date);
                        command.Parameters.AddWithValue("@added_by", dc.added_by);
                        command.Parameters.AddWithValue("@id", dc.id);

                        connection.Open();
                        int rows = command.ExecuteNonQuery();

                        isSuccess = (rows > 0);
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

        #region DELETE Method for Dealer and Customer Module
        public bool Delete(DeaCustBLL dc)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", dc.id);

                        connection.Open();
                        int rows = command.ExecuteNonQuery();

                        isSuccess = (rows > 0);
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

        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR type LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        connection.Open();
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dataTable;
        }
        #endregion

        #region METHOD TO SEARCH DEALER OR CUSTOMER FOR TRANSACTION MODULE
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
            DeaCustBLL dc = new DeaCustBLL();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable dataTable = new DataTable();

                try
                {
                    string sql = "SELECT name, email, contact, address FROM tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        connection.Open();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dc.name = dataTable.Rows[0]["name"].ToString();
                            dc.email = dataTable.Rows[0]["email"].ToString();
                            dc.contact = dataTable.Rows[0]["contact"].ToString();
                            dc.address = dataTable.Rows[0]["address"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dc;
        }
        #endregion

        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public DeaCustBLL GetDeaCustIDFromName(string name)
        {
            DeaCustBLL dc = new DeaCustBLL();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable dataTable = new DataTable();

                try
                {
                    string sql = "SELECT id FROM tbl_dea_cust WHERE name = '" + name + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        connection.Open();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dc.id = int.Parse(dataTable.Rows[0]["id"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dc;
        }
        #endregion
    }
}
