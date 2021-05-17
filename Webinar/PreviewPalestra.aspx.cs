using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class PreviewPalestra : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["identificador"].Equals("PalestrasTotais")) { PainelADMPalestras(); }
            else if (Session["identificador"].Equals("PalestrasPendente")) { PainelADMPalestrasPendentes(); }            
            else { NewPalestra(); }            
        }

        protected void PainelADMPalestrasPendentes()
        {
            btnAutorizarPalestra.Visible = true;
            btnNegarPalestra.Visible = true;
            btnSalvarPalestra.Visible = false;
            btnEditarPalestra.Visible = false;
            int id = Convert.ToInt32(Session["id"]);
            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.ObterPalestra(id);
            UsuarioDAL uDAL = new UsuarioDAL();
            Palestrante objPalestrante = uDAL.ObterNovoPalestrante(objPalestra.IDPalestrante);
            Usuario objUsuario = uDAL.ObterUsuario(objPalestrante.IDPalestrante);
            Usuario objUsuario2 = uDAL.ObterUsuario(objPalestra.PalestraCriador);


            if (objPalestra == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Algo deu errado, tente novamente.');", true);
            }
            else
            {
                byte[] bytes = objPalestra.PalestraCapa;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgPalestra.ImageUrl = "data:image/png;base64," + base64String;

                lblTitulo.Text = objPalestra.PalestraTitulo;
                lblSubTitulo.Text = objPalestra.PalestraSubTitulo;
                lblCategoria.Text = objPalestra.PalestraCategoria;
                lblDataExibir.Text = Convert.ToString(objPalestra.PalestraData);
                lblDuracao.Text = objPalestra.PalestraDuracao;
                lblSinopseP1.Text = objPalestra.PalestraSinopseP1;
                lblSinopseP2.Text = objPalestra.PalestraSinopseP2;
                lblSinopseP3.Text = objPalestra.PalestraSinopseP3;
                lblSinopseP4.Text = objPalestra.PalestraSinopseP4;
                if (objPalestra.PalestraAutoriza == false)
                {
                    lblAutoriza.Text = "O criador optou em não autorizar a publicação";
                    lblAutoriza.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblAutoriza.Text = "Publicação de palestra autorizado";
                    lblAutoriza.ForeColor = System.Drawing.Color.Green;
                }

                int idPalestrante = objPalestrante.IDPalestrante;
                Palestrante palestrante = uDAL.BuscarIDPalestrante(idPalestrante);
                Usuario usuario = uDAL.BuscarEmail(idPalestrante);

                DateTime birthdate = palestrante.PalestranteDtNasc.Date;
                DateTime today = DateTime.Now.Date;
                var age = today.Year - birthdate.Year;
                if (birthdate > today.AddYears(-age)) age--;

                byte[] bytes1 = palestrante.PalestranteFoto;
                string base64String1 = Convert.ToBase64String(bytes1, 0, bytes1.Length);
                imgPalestrante.ImageUrl = "data:image/png;base64," + base64String1;
                lblNome.Text = usuario.Username;
                lblEmail.Text = usuario.Email;
                lblCidade.Text = palestrante.PalestranteCidadeUF;
                lblIdade.Text = Convert.ToString(age) + " anos";
                lblEspecialidade.Text = palestrante.PalestranteEspecialidade;
                lblBio1.Text = palestrante.PalestranteBioP1;
                lblBio2.Text = palestrante.PalestranteBioP2;
                btnTWT.HRef = palestrante.PalestranteTwiter;
                btnFACE.HRef = palestrante.PalestranteFacebook;
                btnGG.HRef = palestrante.PalestranteGoogle;
                btnIN.HRef = palestrante.PalestranteLinkedin;
            }
        }
        protected void PainelADMPalestras()
        {
            btnAutorizarPalestra.Visible = false;
            btnNegarPalestra.Visible = false;
            btnSalvarPalestra.Visible = false;
            btnEditarPalestra.Visible = true;
            btnEditarPalestra.Text = "Voltar";

            int id = Convert.ToInt32(Session["IDPalestra"]);
            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.ObterPalestra(id);
            UsuarioDAL uDAL = new UsuarioDAL();
            Palestrante objPalestrante = uDAL.ObterNovoPalestrante(objPalestra.IDPalestrante);
            Usuario objUsuario = uDAL.ObterUsuario(objPalestrante.IDPalestrante);
            Usuario objUsuario2 = uDAL.ObterUsuario(objPalestra.PalestraCriador);


            if (objPalestra == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Algo deu errado, tente novamente.');", true);
            }
            else
            {
                byte[] bytes = objPalestra.PalestraCapa;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgPalestra.ImageUrl = "data:image/png;base64," + base64String;

                lblTitulo.Text = objPalestra.PalestraTitulo;
                lblSubTitulo.Text = objPalestra.PalestraSubTitulo;
                lblCategoria.Text = objPalestra.PalestraCategoria;
                lblDataExibir.Text = Convert.ToString(objPalestra.PalestraData);
                lblDuracao.Text = objPalestra.PalestraDuracao;
                lblSinopseP1.Text = objPalestra.PalestraSinopseP1;
                lblSinopseP2.Text = objPalestra.PalestraSinopseP2;
                lblSinopseP3.Text = objPalestra.PalestraSinopseP3;
                lblSinopseP4.Text = objPalestra.PalestraSinopseP4;
                if (objPalestra.PalestraAutoriza == false)
                {
                    lblAutoriza.Text = "O criador optou em não autorizar a publicação";
                    lblAutoriza.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblAutoriza.Text = "Publicação de palestra autorizado";
                    lblAutoriza.ForeColor = System.Drawing.Color.Green;
                }

                int idPalestrante = objPalestrante.IDPalestrante;
                Palestrante palestrante = uDAL.BuscarIDPalestrante(idPalestrante);
                Usuario usuario = uDAL.BuscarEmail(idPalestrante);

                DateTime birthdate = palestrante.PalestranteDtNasc.Date;
                DateTime today = DateTime.Now.Date;
                var age = today.Year - birthdate.Year;
                if (birthdate > today.AddYears(-age)) age--;

                byte[] bytes1 = palestrante.PalestranteFoto;
                string base64String1 = Convert.ToBase64String(bytes1, 0, bytes1.Length);
                imgPalestrante.ImageUrl = "data:image/png;base64," + base64String1;
                lblNome.Text = usuario.Username;
                lblEmail.Text = usuario.Email;
                lblCidade.Text = palestrante.PalestranteCidadeUF;
                lblIdade.Text = Convert.ToString(age) + " anos";
                lblEspecialidade.Text = palestrante.PalestranteEspecialidade;
                lblBio1.Text = palestrante.PalestranteBioP1;
                lblBio2.Text = palestrante.PalestranteBioP2;
                btnTWT.HRef = palestrante.PalestranteTwiter;
                btnFACE.HRef = palestrante.PalestranteFacebook;
                btnGG.HRef = palestrante.PalestranteGoogle;
                btnIN.HRef = palestrante.PalestranteLinkedin;
            }
        }
        protected void NewPalestra()
        {
            btnAutorizarPalestra.Visible = false;
            btnNegarPalestra.Visible = false;
            btnSalvarPalestra.Visible = true;
            btnEditarPalestra.Visible = true;

            string link = Convert.ToString(Session["link"]);
            string categoria = Convert.ToString(Session["categoria"]);
            string titulo = Convert.ToString(Session["titulo"]);
            string subtitulo = Convert.ToString(Session["subtitulo"]);
            string bio1 = Convert.ToString(Session["bio1"]);
            string bio2 = Convert.ToString(Session["bio2"]);
            string bio3 = Convert.ToString(Session["bio3"]);
            string bio4 = Convert.ToString(Session["bio4"]);
            string tempopalestra = Convert.ToString(Session["tempopalestra"]);
            DateTime datapalestra = Convert.ToDateTime(Session["datapalestra"]);
            string d = datapalestra.ToString("dd/MM/yyyy");

            string fotopalestrante = Session["fotocapa"].ToString();

            imgPalestra.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])Session["fotocapa"]);
            lblTitulo.Text = titulo;
            lblSubTitulo.Text = subtitulo;
            lblCategoria.Text = categoria;
            lblDataExibir.Text = d;
            lblDuracao.Text = tempopalestra;
            lblSinopseP1.Text = bio1;
            lblSinopseP2.Text = bio2;
            lblSinopseP3.Text = bio3;
            lblSinopseP4.Text = bio4;

            int idPalestrante = Convert.ToInt32(Session["palestrante"]);

            UsuarioDAL uDAL = new UsuarioDAL();
            Palestrante palestrante = uDAL.BuscarIDPalestrante(idPalestrante);
            Usuario usuario = uDAL.BuscarEmail(idPalestrante);

            DateTime birthdate = palestrante.PalestranteDtNasc.Date;
            DateTime today = DateTime.Now.Date;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            byte[] bytes = palestrante.PalestranteFoto;
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgPalestrante.ImageUrl = "data:image/png;base64," + base64String;
            lblNome.Text = usuario.Username;
            lblEmail.Text = usuario.Email;
            lblCidade.Text = palestrante.PalestranteCidadeUF;
            lblIdade.Text = Convert.ToString(age) + " anos";
            lblEspecialidade.Text = palestrante.PalestranteEspecialidade;
            lblBio1.Text = palestrante.PalestranteBioP1;
            lblBio2.Text = palestrante.PalestranteBioP2;
            btnTWT.HRef = palestrante.PalestranteTwiter;
            btnFACE.HRef = palestrante.PalestranteFacebook;
            btnGG.HRef = palestrante.PalestranteGoogle;
            btnIN.HRef = palestrante.PalestranteLinkedin;
        }
        protected void btnAutorizarPalestra_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            PalestraDAL pDAL = new PalestraDAL();
            pDAL.AutorizarPalestra(id);
            Palestra objPalestra = pDAL.ObterPalestra(id);
            UsuarioDAL uDAL = new UsuarioDAL();
            Palestrante objPalestrante = uDAL.ObterNovoPalestrante(objPalestra.IDPalestrante);
            Usuario objUsuario = uDAL.ObterUsuario(objPalestrante.IDPalestrante);            
            using (MailMessage mm = new MailMessage("sender@gmail.com", objUsuario.Email))
            {

                mm.Subject = "Aggregate autorização de palestra";
                string body = "Olá " + objUsuario.Username + ",";
                body += "<br /><br />A palestra: ";
                body += "<br />Título: " + objPalestra.PalestraTitulo;
                body += "<br />Foi aprovada por um moderador.";
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
            Redirecionar();
        }

        protected void btnNegarPalestra_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            PalestraDAL pDAL = new PalestraDAL();
            pDAL.NegarPalestra(id);
            Palestra objPalestra = pDAL.ObterPalestra(id);
            UsuarioDAL uDAL = new UsuarioDAL();
            Palestrante objPalestrante = uDAL.ObterNovoPalestrante(objPalestra.IDPalestrante);
            Usuario objUsuario = uDAL.ObterUsuario(objPalestrante.IDPalestrante);
            using (MailMessage mm = new MailMessage("sender@gmail.com", objUsuario.Email))
            {

                mm.Subject = "Aggregate autorização de palestra";
                string body = "Olá " + objUsuario.Username + ",";
                body += "<br /><br />A palestra, ";
                body += "<br />Título: " + objPalestra.PalestraTitulo;
                body += "<br />Foi negada por um moderador.";
                body += "<br /><br />Por favor revise as informações da Palestra.";
                body += "<br /><br />Se você acredita que foi uma decisão injusta, envie um e-mail para o administrador.";
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
            Redirecionar();
        }

        protected void btnSalvarPalestra_Click(object sender, EventArgs e)
        {
            int idPalestrante = Convert.ToInt32(Session["palestrante"]);
            int idCriador = Convert.ToInt32(Session["criador"]);
            string link = Convert.ToString(Session["link"]);
            string categoria = Convert.ToString(Session["categoria"]);
            string titulo = Convert.ToString(Session["titulo"]);
            string subtitulo = Convert.ToString(Session["subtitulo"]);
            string bio1 = Convert.ToString(Session["bio1"]);
            string bio2 = Convert.ToString(Session["bio2"]);
            string bio3 = Convert.ToString(Session["bio3"]);
            string bio4 = Convert.ToString(Session["bio4"]);
            string tempopalestra = Convert.ToString(Session["tempopalestra"]);
            DateTime datapalestra = Convert.ToDateTime(Session["datapalestra"]);
            Boolean autoriza = Convert.ToBoolean(Session["autoriza"]);
            byte[] FotoCapa = (byte[])Session["fotocapa"];

            PalestraDAL pDAL = new PalestraDAL();

            Palestra objPalestra = new Palestra();
            objPalestra.PalestraCapa = FotoCapa;
            objPalestra.IDPalestrante = idPalestrante;
            objPalestra.PalestraCriador = idCriador;
            objPalestra.PalestraLink = link;
            objPalestra.PalestraDtCriacao = DateTime.Now;
            objPalestra.PalestraCategoria = categoria;
            objPalestra.PalestraTitulo = titulo;
            objPalestra.PalestraSubTitulo = subtitulo;
            objPalestra.PalestraSinopseP1 = bio1;
            objPalestra.PalestraSinopseP2 = bio2;
            objPalestra.PalestraSinopseP3 = bio3;
            objPalestra.PalestraSinopseP4 = bio4;
            objPalestra.PalestraDuracao = tempopalestra;
            objPalestra.PalestraData = Convert.ToDateTime(datapalestra);
            objPalestra.PalestraAutoriza = autoriza;

            pDAL.InserirPalestra(objPalestra);
            Redirecionar();
        }

        protected void Redirecionar()
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado" };
            string[] Moderador = { "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (Palestrante.Contains(usuario.Tipo)) { Response.Redirect("PainelPalestrante.aspx"); }
            else if (Moderador.Contains(usuario.Tipo)) { Response.Redirect("PainelPalestrante.aspx"); }
            else if (Administrador.Contains(usuario.Tipo)) { Response.Redirect("PainelAdministrador.aspx"); }
            else { Response.Redirect("Default.aspx"); }
        }

    }
}