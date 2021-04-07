using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Models;

namespace DAL
{

    public class UsuarioDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregareBD"].ConnectionString;

        public Usuario VerificarUsuarios(string usr, string mail)
        {
            Usuario usuario = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Users WHERE Username LIKE @usr OR Email LIKE @mail";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usr", usr);
            cmd.Parameters.AddWithValue("@mail", mail);

            SqlDataReader dr = cmd.ExecuteReader();

            if(dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.Username = dr["Username"].ToString();
                usuario.City= dr["City"].ToString();
                usuario.NascDate = Convert.ToDateTime(dr["NascDate"].ToString()).Date;
                usuario.Password = dr["Password"].ToString();
                usuario.Email = dr["Email"].ToString();            
            }
            conn.Close();
            return usuario;
        }
        public void InserirUsuario(Usuario objUsuario)
        {
            int userId = 0;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "Insert_User";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", objUsuario.Username);
            cmd.Parameters.AddWithValue("@City", objUsuario.City);
            cmd.Parameters.AddWithValue("@NascDate", objUsuario.NascDate);
            cmd.Parameters.AddWithValue("@Password", objUsuario.Password);
            cmd.Parameters.AddWithValue("@Email", objUsuario.Email);

            userId = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            switch (userId)
            {
                case -1:
                    break;
                case -2:
                    break;
                default:
                    SendActivationEmail(userId, objUsuario.Email, objUsuario.Username);
                    break;
            }
        }
        private void SendActivationEmail(int userId, string mail, string usr)
        {
            string activationCode = Guid.NewGuid().ToString();
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)");

            SqlDataAdapter sda = new SqlDataAdapter();

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MailMessage mm = new MailMessage("sender@gmail.com", mail);

            mm.Subject = "Atiavação de conta";
            string body = "Olá " + usr + ",";
            body += "<br /><br />Clique no link a seguir para ativar sua conta";
            body += "<br /><a href = '" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("Home.aspx", "Activation.aspx?ActivationCode=" + activationCode) + "'>Clique aqui para ativar sua conta.</a>";
            body += "<br /><br />Obrigado";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            NetworkCredential NetworkCred = new NetworkCredential("sender.email-validation@gmail.com", "P@ssw0rd2021");            
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
      
    }
}
