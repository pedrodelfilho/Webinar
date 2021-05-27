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
using System.Web.UI.HtmlControls;
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
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
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
            catch (ArgumentException e) { return false; }

            try { return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)); }
            catch (RegexMatchTimeoutException) { return false; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarHome();
        }

        protected void CarregarHome()
        {
            HomeDAL hDAL = new HomeDAL();
            Home objHome = hDAL.PreencherHome();

            string titulo = objHome.Titulo;
            string destaque = objHome.TituloDestaque;

            int total = titulo.Length;
            int alvo = destaque.Length;
            int primeiro = titulo.LastIndexOf(destaque);
            int segundo1 = alvo + primeiro;
            int segundo2 = total - segundo1;

            string titulo1 = titulo.Substring(0, primeiro);
            string titulo2 = titulo.Substring(segundo1, segundo2);

            var i = new HtmlGenericControl("i");
            i.Attributes["class"] = "fa fa-minus-circle";
            var ii = new HtmlGenericControl("i");
            ii.Attributes["class"] = "fa fa-minus-circle";
            var iii = new HtmlGenericControl("i");
            iii.Attributes["class"] = "fa fa-minus-circle";
            var iiii = new HtmlGenericControl("i");
            iiii.Attributes["class"] = "fa fa-minus-circle";
            var iiiii = new HtmlGenericControl("i");
            iiiii.Attributes["class"] = "fa fa-minus-circle";

            var span = new HtmlGenericControl("span");
            span.InnerHtml = "<br />" + destaque;

            var span1 = new HtmlGenericControl("span");
            span1.InnerHtml = titulo2;
            span1.Attributes["style"] = "color:white";

            lblTitulo.InnerText = titulo1;
            lblTitulo.Controls.Add(span);
            lblTitulo.Controls.Add(span1);
            lblSubTitulo.InnerText = objHome.SubTitulo;
            lblLink.Attributes["href"] = objHome.LinkIntro;
            lblQuemSomos.InnerText = objHome.QuemSomos;
            lblQuando.InnerText = objHome.Quando;
            lblOnde.InnerText = objHome.Onde;
            lblPergunta1.InnerHtml = objHome.Pergunta1;
            lblResposta1.InnerText = objHome.Responsta1;
            lblPergunta2.InnerHtml = objHome.Pergunta2;
            lblResposta2.InnerText = objHome.Responsta2;
            lblPergunta3.InnerHtml = objHome.Pergunta3;
            lblResposta3.InnerText = objHome.Responsta3;
            lblPergunta4.InnerHtml = objHome.Pergunta4;
            lblResposta4.InnerText = objHome.Responsta4;
            lblPergunta5.InnerHtml = objHome.Pergunta5;
            lblResposta5.InnerText = objHome.Responsta5;
            lblEndereco.InnerText = objHome.Endereco;
            lblTelefone.InnerText = objHome.Telefone;
            lblTelefone.Attributes["href"] = "tel:+55" + objHome.Telefone;
            lblEmail.InnerText = objHome.Email;
            lblEmail.Attributes["href"] = "mailto:" + objHome.Email;
            lblPergunta1.Controls.Add(i);
            lblPergunta2.Controls.Add(ii);
            lblPergunta3.Controls.Add(iii);
            lblPergunta4.Controls.Add(iiii);
            lblPergunta5.Controls.Add(iiiii);
            CarregarHomePalestrantes();
        }
        protected void CarregarHomePalestrantes()
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            DataTable dt = uDAL.PalestranteRandom();

            byte[] bytes1 = (byte[])dt.Rows[0]["PalestranteFoto"];
            string base64String1 = Convert.ToBase64String(bytes1, 0, bytes1.Length);
            imgPalestrante1.Attributes["src"] = "data:image/png;base64," + base64String1;
            int id1 = Convert.ToInt32(dt.Rows[0]["IDPalestrante"]);
            Usuario p1 = uDAL.BuscarEmail(id1);
            aPalestrante1.InnerText = p1.Username;
            aPalestrante1.HRef = "PreviewPalestrante.aspx?a=" + id1;
            pPalestrante1.InnerText = dt.Rows[0]["PalestranteEspecialidade"].ToString();
            twtPalestrante1.Attributes["href"] = dt.Rows[0]["PalestranteTwiter"].ToString();
            facePalestrante1.Attributes["href"] = dt.Rows[0]["PalestranteFacebook"].ToString();
            ggPalestrante1.HRef = dt.Rows[0]["PalestranteGoogle"].ToString();
            inPalestrante1.HRef = dt.Rows[0]["PalestranteLinkedin"].ToString();

            byte[] bytes2 = (byte[])dt.Rows[1]["PalestranteFoto"];
            string base64String2 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
            imgPalestrante2.Attributes["src"] = "data:image/png;base64," + base64String2;
            int id2 = Convert.ToInt32(dt.Rows[1]["IDPalestrante"]);
            Usuario p2 = uDAL.BuscarEmail(id2);
            aPalestrante2.InnerText = p2.Username;
            aPalestrante2.HRef = "PreviewPalestrante.aspx?a=" + id2;
            pPalestrante2.InnerText = dt.Rows[1]["PalestranteEspecialidade"].ToString();
            twtPalestrante2.Attributes["href"] = dt.Rows[1]["PalestranteTwiter"].ToString();
            facePalestrante2.Attributes["href"] = dt.Rows[1]["PalestranteFacebook"].ToString();
            ggPalestrante2.HRef = dt.Rows[1]["PalestranteGoogle"].ToString();
            inPalestrante2.HRef = dt.Rows[1]["PalestranteLinkedin"].ToString();


            byte[] bytes3 = (byte[])dt.Rows[2]["PalestranteFoto"];
            string base64String3 = Convert.ToBase64String(bytes3, 0, bytes3.Length);
            imgPalestrante3.Attributes["src"] = "data:image/png;base64," + base64String3;
            int id3 = Convert.ToInt32(dt.Rows[2]["IDPalestrante"]);
            Usuario p3 = uDAL.BuscarEmail(id3);
            aPalestrante3.InnerText = p3.Username;
            aPalestrante3.HRef = "PreviewPalestrante.aspx?a=" + id3;
            pPalestrante3.InnerText = dt.Rows[2]["PalestranteEspecialidade"].ToString();
            twtPalestrante3.Attributes["href"] = dt.Rows[2]["PalestranteTwiter"].ToString();
            facePalestrante3.Attributes["href"] = dt.Rows[2]["PalestranteFacebook"].ToString();
            ggPalestrante3.HRef = dt.Rows[2]["PalestranteGoogle"].ToString();
            inPalestrante3.HRef = dt.Rows[2]["PalestranteLinkedin"].ToString();


            byte[] bytes4 = (byte[])dt.Rows[3]["PalestranteFoto"];
            string base64String4 = Convert.ToBase64String(bytes4, 0, bytes4.Length);
            imgPalestrante4.Attributes["src"] = "data:image/png;base64," + base64String4;
            int id4 = Convert.ToInt32(dt.Rows[3]["IDPalestrante"]);
            Usuario p4 = uDAL.BuscarEmail(id4);
            aPalestrante4.InnerText = p4.Username;
            aPalestrante4.HRef = "PreviewPalestrante.aspx?a=" + id4;
            pPalestrante4.InnerText = dt.Rows[3]["PalestranteEspecialidade"].ToString();
            twtPalestrante4.Attributes["href"] = dt.Rows[3]["PalestranteTwiter"].ToString();
            facePalestrante4.Attributes["href"] = dt.Rows[3]["PalestranteFacebook"].ToString();
            ggPalestrante4.HRef = dt.Rows[3]["PalestranteGoogle"].ToString();
            inPalestrante4.HRef = dt.Rows[3]["PalestranteLinkedin"].ToString();


            byte[] bytes5 = (byte[])dt.Rows[4]["PalestranteFoto"];
            string base64String5 = Convert.ToBase64String(bytes5, 0, bytes5.Length);
            imgPalestrante5.Attributes["src"] = "data:image/png;base64," + base64String5;
            int id5 = Convert.ToInt32(dt.Rows[4]["IDPalestrante"]);
            Usuario p5 = uDAL.BuscarEmail(id5);
            aPalestrante5.InnerText = p5.Username;
            aPalestrante5.HRef = "PreviewPalestrante.aspx?a=" + id5;
            pPalestrante5.InnerText = dt.Rows[4]["PalestranteEspecialidade"].ToString();
            twtPalestrante5.Attributes["href"] = dt.Rows[4]["PalestranteTwiter"].ToString();
            facePalestrante5.Attributes["href"] = dt.Rows[4]["PalestranteFacebook"].ToString();
            ggPalestrante5.HRef = dt.Rows[4]["PalestranteGoogle"].ToString();
            inPalestrante5.HRef = dt.Rows[4]["PalestranteLinkedin"].ToString();

            byte[] bytes6 = (byte[])dt.Rows[5]["PalestranteFoto"];
            string base64String6 = Convert.ToBase64String(bytes6, 0, bytes6.Length);
            imgPalestrante6.Attributes["src"] = "data:image/png;base64," + base64String6;
            int id6 = Convert.ToInt32(dt.Rows[5]["IDPalestrante"]);
            Usuario p6 = uDAL.BuscarEmail(id6);
            aPalestrante6.InnerText = p6.Username;
            aPalestrante6.HRef = "PreviewPalestrante.aspx?a=" + id6;
            pPalestrante6.InnerText = dt.Rows[5]["PalestranteEspecialidade"].ToString();
            twtPalestrante6.Attributes["href"] = dt.Rows[5]["PalestranteTwiter"].ToString();
            facePalestrante6.Attributes["href"] = dt.Rows[5]["PalestranteFacebook"].ToString();
            ggPalestrante6.HRef = dt.Rows[5]["PalestranteGoogle"].ToString();
            inPalestrante6.HRef = dt.Rows[5]["PalestranteLinkedin"].ToString();
            Session["HomePage"] = "SIM";
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
                catch { ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ocorreu um erro ao enviar o e-mail para validação da conta. Tente realizar o Login com o e-mail e senha que realizou o cadastro.');", true); }
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