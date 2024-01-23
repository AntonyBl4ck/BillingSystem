using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class ProductsDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_products";

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

        #region Method to Insert Product in database
        public bool Insert(ProductsBLL p)
        {
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", p.name);
                        cmd.Parameters.AddWithValue("@category", p.category);
                        cmd.Parameters.AddWithValue("@description", p.description);
                        cmd.Parameters.AddWithValue("@rate", p.rate);
                        cmd.Parameters.AddWithValue("@qty", p.qty);
                        cmd.Parameters.AddWithValue("@added_date", p.added_date);
                        cmd.Parameters.AddWithValue("@added_by", p.added_by);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

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

        #region Method to Update Product in Database
        public bool Update(ProductsBLL p)
        {
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, qty=@qty, added_date=@added_date, added_by=@added_by WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", p.name);
                        cmd.Parameters.AddWithValue("@category", p.category);
                        cmd.Parameters.AddWithValue("@description", p.description);
                        cmd.Parameters.AddWithValue("@rate", p.rate);
                        cmd.Parameters.AddWithValue("@qty", p.qty);
                        cmd.Parameters.AddWithValue("@added_date", p.added_date);
                        cmd.Parameters.AddWithValue("@added_by", p.added_by);
                        cmd.Parameters.AddWithValue("@id", p.id);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

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

        #region Method to Delete Product from Database
        public bool Delete(ProductsBLL p)
        {
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "DELETE FROM tbl_products WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", p.id);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

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

        #region SEARCH Method for Product Module
        public DataTable Search(string keywords)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";

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
    }
}
