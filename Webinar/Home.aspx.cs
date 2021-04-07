using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class Home : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
        }       

        protected void btnCadastrarUsuario_Click(object sender, EventArgs e)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["AggregareBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_User"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", txtCadastrarNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", ddlCidade.SelectedValue);
                        cmd.Parameters.AddWithValue("@NascDate", txtDtNasc.Text);
                        cmd.Parameters.AddWithValue("@Password", Criptografia.GetMD5Hash(txtCadastrarSenha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Email", txtCadastrarEmail.Text.Trim());
                        cmd.Connection = con;
                        con.Open();
                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                }
                string message = string.Empty;
                switch (userId)
                {
                    case -1:
                        message = "Nome de usuário já existe.\\nPor favor, escolha um nome de usuário diferente.";
                        break;
                    case -2:
                        message = "O endereço de e-mail fornecido já foi usado.";
                        break;
                    default:
                        message = "Registro bem sucedido. O e-mail de ativação foi enviado. Verifique em sua caixa de entrada.";
                        SendActivationEmail(userId);
                        break;
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
        }

        private void SendActivationEmail(int userId)
        {
            string constr = ConfigurationManager.ConnectionStrings["AggregareBD"].ConnectionString;
            string activationCode = Guid.NewGuid().ToString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            using (MailMessage mm = new MailMessage("sender@gmail.com", txtCadastrarEmail.Text))
            {
                mm.Subject = "Atiavação de conta";
                string body = "Olá " + txtCadastrarNome.Text + ",";
                body += "<br /><br />Clique no link a seguir para ativar sua conta";
                body += "<br /><a href = 'http://localhost:54970/Activation.aspx?ActivationCode=" + activationCode + "'>Clique aqui para ativar sua conta.</a>";
                body += "<br /><br />Obrigado";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                NetworkCredential NetworkCred = new NetworkCredential("sender.email.validation@gmail.com", "P@ssw0rd2021");                
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        protected void btnEntrarLogin_Click(object sender, EventArgs e)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["AggregareBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", Login1.UserName);
                    cmd.Parameters.AddWithValue("@Password", Criptografia.GetMD5Hash(Login1.Password));
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                string message = string.Empty;
                switch (userId)
                {
                    case -1:
                        message = "Credencial incorreta, tente novamente.";
                        break;
                    case -2:
                        message = "Credencial pendente de validação.";
                        break;
                    default:
                        message = "Login realizado, bem vindo de volta.";
                        //FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                        break;
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
        }

        protected void btnRecuperarSenha_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["AggregareBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Username, [Password] FROM Users WHERE Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmailRecuperar.Text.Trim());
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            username = sdr["Username"].ToString();
                            password = sdr["Password"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            string message = string.Empty;
            if (!string.IsNullOrEmpty(password))
            {
                MailMessage mm = new MailMessage("sender@gmail.com", txtEmailRecuperar.Text);
                mm.Subject = "Recuperação de senha";
                string body = "Olá " + username + ",";
                body += "<br /><br />Sua senha é " + Criptografia.SetMD5Hash(password);
                body += "<br /><br />Obrigado!";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                NetworkCredential NetworkCred = new NetworkCredential("sender.email.validation@gmail.com", "P@ssw0rd2021");                
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                message = "A recuperação de senha foi enviada para o seu endereço de e-mail.";
            }
            else
            {
                message = "O endereço de e-mail fornecido não consta cadastrado em nosso banco de dados.";
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}