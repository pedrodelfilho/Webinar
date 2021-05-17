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

        public Notificar VerificarEmail(string mail)
        {
            Notificar email = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM NotificarEvento WHERE NotificarEmail = @mail";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mail", mail);

            SqlDataReader dr = cmd.ExecuteReader();

            
            email = new Notificar();
            try { email.NotificarEmail = dr["NotificarEmail"].ToString(); } catch { email.NotificarEmail = null; }

            conn.Close();
            return email;

        }
        public void CadastrarEmailNotificar(string mail)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "INSERT INTO NotificarEvento(NotificarEmail) VALUES(@mail)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mail", mail);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
