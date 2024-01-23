using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class CategoriesDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_categories";
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

        #region Insert New Category
        public bool Insert(CategoriesBLL category)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO ####### (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", category.title);
                        command.Parameters.AddWithValue("@description", category.description);
                        command.Parameters.AddWithValue("@added_date", category.added_date);
                        command.Parameters.AddWithValue("@added_by", category.added_by);

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

        #region Update Method
        public bool Update(CategoriesBLL category)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "UPDATE ##### SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", category.title);
                        command.Parameters.AddWithValue("@description", category.description);
                        command.Parameters.AddWithValue("@added_date", category.added_date);
                        command.Parameters.AddWithValue("@added_by", category.added_by);
                        command.Parameters.AddWithValue("@id", category.id);

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

        #region Delete Category Method
        public bool Delete(CategoriesBLL category)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "DELETE FROM ######## WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", category.id);

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

        #region Method for Search Functionality
        public DataTable Search(string keywords)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM ############ WHERE id LIKE '%" + keywords + "%' OR title LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";
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
    }
}
