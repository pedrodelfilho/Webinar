using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
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

        public void VerificarUsuario()
        {
            string Usr = txtCadastrarNome.Text;
            string Mail = txtCadastrarEmail.Text;

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.VerificarUsuarios(Usr, Mail);

            if ( usuario == null)
            {
                Usuario objUsuario = new Usuario();
                objUsuario.Username = txtCadastrarNome.Text;
                objUsuario.City = ddlCidade.SelectedValue;
                objUsuario.NascDate = Convert.ToDateTime(txtDtNasc.Text).Date;
                objUsuario.Password = txtCadastrarSenha.Text;
                objUsuario.Email = txtCadastrarEmail.Text;

                uDAL.InserirUsuario(objUsuario);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Nome de usuário e/ou E-mail já possuí cadastro.');", true);
            }
        }

        protected void btnCadastrarUsuario_Click(object sender, EventArgs e)
        {
            //VerificarUsuario();
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
                        cmd.Parameters.AddWithValue("@Password", txtCadastrarSenha.Text.Trim());
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
                        message = "Username already exists.\\nPlease choose a different username.";
                        break;
                    case -2:
                        message = "Supplied email address has already been used.";
                        break;
                    default:
                        message = "Registration successful. Activation email has been sent.";
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
                lbl.Text = activationCode;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("sender.email.validation@gmail.com", "P@ssw0rd2021");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

    }
}