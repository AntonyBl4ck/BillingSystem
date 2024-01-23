using AnyStore.BLL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class LoginDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool LoginCheck(LoginBLL l)
        {
            bool isSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", l.username);
                        command.Parameters.AddWithValue("@password", l.password);
                        command.Parameters.AddWithValue("@user_type", l.user_type);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        isSuccess = (dataTable.Rows.Count > 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return isSuccess;
        }
    }
}
