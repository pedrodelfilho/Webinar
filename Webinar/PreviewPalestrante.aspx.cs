using System;
using System.Collections.Generic;
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
    public partial class PreviewPalestrante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            if (!IsPostBack)
            {
                if (Session["HomePage"].Equals("SIM")) { btnAutorizarPerfil.Visible = false; btnNegarPerfil.Visible = false; id = Convert.ToInt32(HttpContext.Current.Request.QueryString["a"]); }
                UsuarioDAL uDAL = new UsuarioDAL();
                Usuario objUsuario = uDAL.ObterUsuario(id);
                Palestrante objPalestrante = uDAL.ObterPalestrante(id);

                if (objPalestrante == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Algo deu errado, tente novamente.'" + id + ");", true);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblNome.Text = objUsuario.Username;
                    lblEmail.Text = objUsuario.Email;
                    lblNome1.Text = objUsuario.Username;

                    if (objPalestrante.PalestranteFoto == null)
                    {
                        imgPalestrante.ImageUrl = "~/img/anonimo.jpg";
                    }
                    else
                    {
                        byte[] bytes = objPalestrante.PalestranteFoto;
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        imgPalestrante.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    try
                    {
                        DateTime a = Convert.ToDateTime(objPalestrante.PalestranteDtNasc);
                        string b = a.ToString("yyyy-MM-dd");
                        lblDtNasc.Text = b;
                    }
                    catch { lblDtNasc.Text = "dd/MM/yyyy"; }
                    
                    try { lblCidade.Text = objPalestrante.PalestranteCidadeUF; } catch { lblCidade.Text = "Não cadastrado"; }
                    try { lblCidade1.Text = objPalestrante.PalestranteCidadeUF; } catch { lblCidade1.Text = "Não cadastrado"; }
                    lblCidade.Text = objPalestrante.PalestranteCidadeUF;
                    switch (objPalestrante.PalestranteSexo)
                    {
                        case 'M':
                            lblSexo.Text = "Masculino";
                            break;
                        case 'F':
                            lblSexo.Text = "Feminino";
                            break;
                        case 'O':
                            lblSexo.Text = "Outros";
                            break;
                    }
                    lblEscolaridade.Text = objPalestrante.PalestranteFormacao;
                    lblEspecialidade.Text = objPalestrante.PalestranteEspecialidade;
                    lblBio1.Text = objPalestrante.PalestranteBioP1;
                    lblBio2.Text = objPalestrante.PalestranteBioP2;
                    cbReceberEmail.Checked = objPalestrante.PalestranteReceberEmail;
                    cbAutorizarPerfil.Checked = objPalestrante.PalestranteAutoriza;

                    if (objPalestrante.PalestranteTwiter == string.Empty) { linkTwiter.Text = "Não informado"; }
                    else { linkTwiter.Text = objPalestrante.PalestranteTwiter; }
                    if (objPalestrante.PalestranteFacebook == string.Empty) { linkFacebook.Text = "Não informado"; }
                    else { linkFacebook.Text = objPalestrante.PalestranteFacebook; }
                    if (objPalestrante.PalestranteGoogle == string.Empty) { linkGoogle.Text = "Não informado"; }
                    else { linkGoogle.Text = objPalestrante.PalestranteGoogle; }
                    if (objPalestrante.PalestranteLinkedin == string.Empty) { linkLinkedin.Text = "Não informado"; }
                    else { linkLinkedin.Text = objPalestrante.PalestranteLinkedin; }
                }
            }
        }

        protected void btnAutorizarPerfil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            uDAL.AutorizarPalestrante(id);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Perfil do palestrante Autorizado');", true);
            Usuario usuario = uDAL.BuscarEmail(id);
            string mail = usuario.Email;
            string nome = usuario.Username;
            using (MailMessage mm = new MailMessage("sender@gmail.com", mail))
            {

                mm.Subject = "Aggregate autorização de perfil";
                string body = "Olá " + nome + ",";
                body += "<br /><br />Seu perfil foi verificado e aprovado por um moderador.";
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
            Response.Redirect("PainelModerador.aspx");
        }
        protected void btnNegarPerfil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            uDAL.NegarPalestrante(id);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Perfil do palestrante Negado');", true);
            Usuario usuario = uDAL.BuscarEmail(id);
            string mail = usuario.Email;
            string nome = usuario.Username;
            using (MailMessage mm = new MailMessage("sender@gmail.com", mail))
            {

                mm.Subject = "Aggregate autorização de perfil";
                string body = "Olá " + nome + ",";
                body += "<br /><br />Seu perfil foi verificado e negado por um moderador.";
                body += "<br /><br />Por favor revise as informações do seu perfil.";
                body += "<br /><br />Caso você acredite que foi uma decisão injusta, envie um e-mail para o administrador.";
                body += "<br />Acesse o site e envie sua solicitação através dos canais de contato.";
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
            Response.Redirect("PainelModerador.aspx");
        }
    }
}