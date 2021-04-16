using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Microsoft.AspNet.Identity;
using Models;

namespace Webinar
{
    public partial class Default : System.Web.UI.Page
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }       

        protected void btnCadastrarUsuario_Click(object sender, EventArgs e)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_User"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        string tipo = "Convidado";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", txtCadastrarNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", Criptografia.GetMD5Hash(txtCadastrarSenha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Email", txtCadastrarEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tipo", tipo);
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
                        CadastrarConvidado(userId);
                        break;
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
        }

        protected void CadastrarConvidado(int id)
        {
            Convidado objConvidado = new Convidado();
            objConvidado.UserId = id;

            UsuarioDAL uDAL = new UsuarioDAL();
            uDAL.InserirConvidado(objConvidado);
        }

        private void SendActivationEmail(int userId)
        {
            string constr = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
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
                try { 
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
                catch { ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ocorreu um erro ao eniar o e-mail para validação da conta. Tente realizar o Login com o e-mail e senha que realizou o cadastro.');", true); }
            }
        }

        private void ReSendActivationEmail(int userId, string mail)
        {
            string constr = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
            string activationCode = Guid.NewGuid().ToString();

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarCODE(userId);


            using (MailMessage mm = new MailMessage("sender@gmail.com", mail))
            {
                mm.Subject = "Atiavação de conta";
                string body = "Olá " + txtCadastrarNome.Text + ",";
                body += "<br /><br />Clique no link a seguir para ativar sua conta";
                body += "<br /><a href = 'http://localhost:54970/Activation.aspx?ActivationCode=" + usuario.ActivationCode + "'>Clique aqui para ativar sua conta.</a>";
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

        private void BuscarID(string mail)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(mail);
            ReSendActivationEmail(usuario.UserId, mail);            
        }

        protected void btnEntrarLogin_Click(object sender, EventArgs e)
        {
            int userId = 0;
            string constr = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
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
                        message = "Você não validou sua conta. Novo link de validação enviado para seu e-mail.";
                        BuscarID(Login1.UserName);
                        break;
                    default:
                        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        {
                            message = "Login realizado, bem vindo de volta.";
                            FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet);
                            Response.Redirect(Request.QueryString["ReturnUrl"]);                                
                        }
                        else
                        {
                            message = "Login realizado, bem vindo de volta.";
                            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                        }
                        break;
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
        }

        protected void btnRecuperarSenha_Click(object sender, EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
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

        protected void btnEnviarMensagemContato_Click(object sender, EventArgs e)
        {            
            int x = 0;
            while(x != 4) { 
                if (txtNomeContato.Text == string.Empty) { 
                    txtNomeContato.BorderColor = System.Drawing.Color.Red;
                    lblContato.Text = "Preencha todos os campos";
                    lblContato.ForeColor = System.Drawing.Color.Red;
                    x += 1;
                }
                else { x += 1; txtNomeContato.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ced4da"); }
                if (txtEmailContato.Text == string.Empty)
                {
                    txtEmailContato.BorderColor = System.Drawing.Color.Red;
                    lblContato.Text = "Preencha todos os campos";
                    lblContato.ForeColor = System.Drawing.Color.Red;
                    x += 1;
                }
                else { x += 1; txtEmailContato.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ced4da"); }
                if (txtTituloContato.Text == string.Empty)
                {
                    txtTituloContato.BorderColor = System.Drawing.Color.Red;
                    lblContato.Text = "Preencha todos os campos";
                    lblContato.ForeColor = System.Drawing.Color.Red;
                    x += 1;
                }
                else { x += 1; txtTituloContato.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ced4da"); }
                if (txtMensagemContato.Text == string.Empty)
                {
                    txtMensagemContato.BorderColor = System.Drawing.Color.Red;
                    lblContato.Text = "Preencha todos os campos";
                    lblContato.ForeColor = System.Drawing.Color.Red;
                    x += 1;
                }
                else { x += 1; txtMensagemContato.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ced4da"); }
            }

            if (txtNomeContato.Text != string.Empty && txtEmailContato.Text != string.Empty && txtTituloContato.Text != string.Empty && txtTituloContato.Text != string.Empty && txtMensagemContato.Text != string.Empty)
            {
                MailMessage mm = new MailMessage("sender@gmail.com", txtEmailContato.Text);
                mm.Subject = "Aggregate recebemos sua mensagem";
                string body = "Olá " + txtNomeContato.Text + ",";
                body += "<br /><br />Sua mensagem foi enviada. A equipe Aggregate agradece o seu contato. ";                
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

                MailMessage nn = new MailMessage("sender@gmail.com", "sender.email.validation@gmail.com");
                nn.Subject = "Mensagem de contato";
                string body2 = "Mensagem enviada por:<br />";
                body2 += txtNomeContato.Text + "<br />";
                body2 += txtEmailContato.Text + "<br />";
                body2 += "<br /><br />Titulo: " + txtTituloContato.Text + "<br /><br />";
                body2 += "Mensagem:<br /> " + txtMensagemContato.Text;
                nn.Body = body2;
                nn.IsBodyHtml = true;
                SmtpClient smtp2 = new SmtpClient();
                smtp2.Host = "smtp.gmail.com";
                smtp2.EnableSsl = true;
                smtp2.UseDefaultCredentials = true;
                NetworkCredential NetworkCred2 = new NetworkCredential("sender.email.validation@gmail.com", "P@ssw0rd2021");
                smtp2.Credentials = NetworkCred2;
                smtp2.Port = 587;
                smtp2.Send(nn);


                lblContato.Text = "Mensagem enviada. Agradecemos o seu contato";
                lblContato.ForeColor = System.Drawing.Color.Green;
                txtNomeContato.Text = string.Empty;
                txtEmailContato.Text = string.Empty;
                txtTituloContato.Text = string.Empty;
                txtMensagemContato.Text = string.Empty;                
            }
            

        }

        protected void btnEnviarNotificacao_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(txtEnviarEmail.Text) == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('E-mail inválido.');", true);
            }
            else {
                Notificar objNotificar = new Notificar();
                string email = txtEnviarEmail.Text;
                NotificarDAL nDAL = new NotificarDAL();
                nDAL.CadastrarEmailNotificar(email);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('E-mail cadastrado, obrigado.');", true);
            }
        }
    }
}