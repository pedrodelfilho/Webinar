using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class NotificarDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;

        public void CadastrarEmailNotificar(Notificar objNotificar)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "INSERT INTO NotificarEvento(NotificarEmail) VALUES(@mail)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mail", objNotificar.NotificarEmail);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
