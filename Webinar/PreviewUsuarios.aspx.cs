using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webinar
{
    public partial class PreviewUsuarios : System.Web.UI.Page
    {     
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            string tipo = Convert.ToString(Session["Tipo"]);

            string[] Convidado = { "Convidado" };
            string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado" };
            string[] Moderador = { "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (!IsPostBack)
            {
                if (Palestrante.Contains(tipo))
                {
                    PanelPalestrante.Visible = true;
                    PanelUsuario.Visible = false;
                    CarregarPalestrante();
                }
                else
                {
                    if (Convidado.Contains(tipo))
                    {
                        TornarConvidadoUsuario.Visible = false;
                    }
                    if (Moderador.Contains(tipo))
                    {
                        TornarModeradorUsuario.Visible = false;
                    }
                    if (Administrador.Contains(tipo))
                    {
                        TornarConvidadoUsuario.Visible = false;
                        TornarModeradorUsuario.Visible = false;
                        TornarAdministradorUsuario.Visible = false;
                        TornarPalestranteUsuario.Visible = false;
                        btnBanir.Enabled = false;
                        lblConta.Text = "Não é possivel realizar alterações em contas 'Administrador'";
                    }
                    PanelUsuario.Visible = true;
                    PanelPalestrante.Visible = false;
                    CarregarUsuario();
                }
            }            
        }

        protected void CarregarPalestrante()
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);
            Palestrante objPalestrante = uDAL.ObterPalestrante(id);
            lblNomePalestrante.Text = objUsuario.Username;
            lblEmailPalestrante.Text = objUsuario.Email;
            lblNome2Palestrante.Text = objUsuario.Username;
            lblTipoPalestrante.Text = objUsuario.Tipo;
            

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
                DateTime dtnasc = (objPalestrante.PalestranteDtNasc).Date;
                lblDtNascPalestrante.Text = dtnasc.ToString("dd/MM/yyyy");
            }
            catch { lblDtNascPalestrante.Text = "dd/MM/yyyy"; }

            try { lblCidadePalestrante.Text = objPalestrante.PalestranteCidadeUF; } catch { lblCidadePalestrante.Text = null; }
            lblCidade2Palestrante.Text = objPalestrante.PalestranteCidadeUF;
            switch (objPalestrante.PalestranteSexo)
            {
                case 'M':
                    lblSexoPalestrante.Text = "Masculino";
                    break;
                case 'F':
                    lblSexoPalestrante.Text = "Feminino";
                    break;
                case 'O':
                    lblSexoPalestrante.Text = "Outros";
                    break;
            }
            lblEscolaridadePalestrante.Text = objPalestrante.PalestranteFormacao;
            lblEspecialidadePalestrante.Text = objPalestrante.PalestranteEspecialidade;
            lblBio1Palestrante.Text = objPalestrante.PalestranteBioP1;
            lblBio2Palestrante.Text = objPalestrante.PalestranteBioP2;
            cbReceberEmailPalestrante.Checked = objPalestrante.PalestranteReceberEmail;
            cbAutorizarPerfilPalestrante.Checked = objPalestrante.PalestranteAutoriza;
            lblTwiterPalestrante.Text = objPalestrante.PalestranteTwiter;
            lblFacebookPalestrante.Text = objPalestrante.PalestranteFacebook;
            lblGooglePalestrante.Text = objPalestrante.PalestranteGoogle;
            lblLinkedinPalestrante.Text = objPalestrante.PalestranteLinkedin;
        }

        protected void CarregarUsuario()
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Convidado objConvidado = uDAL.ObterConvidado(id);
            Usuario objUsuario = uDAL.ObterUsuario(id);

            lblNomeUsuario.Text = objUsuario.Username;
            lblEmailUsuario.Text = objUsuario.Email;
            lblNome2Usuario.Text = objUsuario.Username;
            lblTipoUsuario.Text = objUsuario.Tipo;

            if (objConvidado != null) 
            {
                if (objConvidado.FotoConvidado == null)
                {
                    imgUsuario.ImageUrl = "~/img/anonimo.jpg";
                }
                else
                {
                    byte[] bytes = objConvidado.FotoConvidado;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgUsuario.ImageUrl = "data:image/png;base64," + base64String;
                }
                try 
                {
                    DateTime dtnasc = Convert.ToDateTime(objConvidado.ConvidadoDtNasc).Date;
                    objConvidado.ConvidadoDtNasc = dtnasc.ToString("dd/MM/yyyy");
                }
                catch { objConvidado.ConvidadoDtNasc = string.Empty; }
                

                lblCidadeUsuario.Text = objConvidado.ConvidadoCidadeUF;
                try { lblDtNascUsuario.Text = objConvidado.ConvidadoDtNasc; } catch { lblDtNascUsuario.Text = null; }
                lblCidade2Usuario.Text = objConvidado.ConvidadoCidadeUF;
                lblEscolaridadeUsuario.Text = objConvidado.EscolaridadeConvidado;
                lblBioUsuario.Text = objConvidado.ConvidadoBioP1;
                switch (objConvidado.SexoConvidado)
                {
                    case 'M':
                        lblSexoUsuario.Text = "Masculino";
                        break;
                    case 'F':
                        lblSexoUsuario.Text = "Feminino";
                        break;
                    case 'O':
                        lblSexoUsuario.Text = "Outros";
                        break;
                }
                cbReceberEmailUsuario.Checked = objConvidado.ConvidadoReceberEmail;
            }

            
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Convidado objConvidado = uDAL.ObterConvidado(id);
            Usuario objUsuario = uDAL.ObterUsuario(id);

            try
            {
                MailMessage mm = new MailMessage("sender@gmail.com", objUsuario.Email);
                mm.Subject = "Athon Aggregate, mensagem do administrador";
                string body = EmailTitulo.Text;
                body += "<br /><br />" + Emailmensagem.Text;
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
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('E-mail enviado com sucesso para " + objUsuario.Username + "');", true);
            }
            catch { ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Erro ao enviar o e-mail. Tente novamente mais tarde.');", true); }
        }

        protected void btnBanir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);

            string[] Palestrante = { "Convidado, Palestrante", "Palestrante, Convidado", "Moderador, Palestrante", "Palestrante, Moderador", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (Palestrante.Contains(objUsuario.Tipo))
            {
                PalestraDAL pDAL = new PalestraDAL();
                Palestra objPalestra = pDAL.VerificarPalestrante(id);
                if (objPalestra == null)
                {
                    try { uDAL.ExcluirConvidado(id); } catch {; }
                    try { uDAL.ExcluirPalestrante(id); } catch {; }
                    try { uDAL.ExcluirModerador(id); } catch {; }
                    try { uDAL.ExcluirUsuario(id); } catch {; }
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Usuário excluido');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Não foi possível excluir o usuário, o mesmo é palestrante em algumas palestras existentes. Exclua as palestras vinculadas ao usuário primeiro.');", true);
                }
            }
            Redirecionar();
        }

        protected void TornarConvidadoPalestrante_Click(object sender, EventArgs e)
        {            
            int id = Convert.ToInt32(Session["id"]);
            string tipo = "Convidado";
            string destino = "Convidado";
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);
            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.VerificarPalestrante(id);
            if (objPalestra == null)
            {
                Convidado objConvidado = uDAL.ObterConvidado(id);

                if (objConvidado == null)
                {
                    uDAL.AlterarTipoConta(id, destino);
                }
                uDAL.TipoDeConta(id, tipo);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('O usuário '" + objUsuario.Username + ", agora faz parte do grupo de Convidados.);", true);
                uDAL.ExcluirPalestrante(id);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Não foi possível excluir o usuário, o mesmo é palestrante em algumas palestras existentes. Exclua as palestras vinculadas ao usuário primeiro.');", true);
            }            
            Redirecionar();
        }

        protected void TornarModeradorPalestrante_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);

            string[] obj = { "Convidado, Palestrante", "Palestrante, Convidado" };
            string tipo = null;

            if (obj.Contains(objUsuario.Tipo))
            {
                tipo = "Palestrante, Moderador";
            }
            else { tipo = objUsuario.Tipo + ", Moderador"; }

            Moderador objModerador = uDAL.ObterModerador(id);
            string destino = "Moderador";
            if (objModerador == null)
            {
                uDAL.AlterarTipoConta(id, destino);
            }
            uDAL.TipoDeConta(id, tipo);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('O usuário '" + objUsuario.Username + ", agora faz parte do grupo de Moderadores.);", true);
                        
            Redirecionar();
        }

        protected void TornarAdministradorPalestrante_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);
            string destino = "Administrador";
            Administrador objAdministrador = uDAL.ObterAdministrador(id);
            string tipo = objUsuario.Tipo + ", Administrador";
            if (objAdministrador == null)
            {
                uDAL.AlterarTipoConta(id, destino);
            }
            uDAL.TipoDeConta(id, tipo);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('O usuário '" + objUsuario.Username + ", agora faz parte do grupo de Administradores.);", true);      
            Redirecionar();
        }

        protected void TornarPalestranteUsuario_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["id"]);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario objUsuario = uDAL.ObterUsuario(id);
            string destino = "Palestrante";
            Palestrante objPalestrante = uDAL.ObterNovoPalestrante(id);
            string tipo = "Palestrante";
            if (objPalestrante == null)
            {
                if (objUsuario.Tipo == "Convidado")
                {
                    tipo = "Convidado, Palestrante";
                }
                uDAL.AlterarTipoConta(id, destino);
                uDAL.ExcluirModerador(id);                
            }
            uDAL.TipoDeConta(id, tipo);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('O usuário '" + objUsuario.Username + ", agora faz parte do grupo de Palestrantes.);", true);
            Redirecionar();
        }

        protected void Redirecionar()
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            string[] Convidado = { "Convidado" };
            string[] Palestrante = { "Convidado, Palestrante", "Palestrante, Convidado" };
            string[] Moderador = { "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (Convidado.Contains(usuario.Tipo)) { Response.Redirect("PainelUsuario.aspx"); }
            else if (Administrador.Contains(usuario.Tipo)) { Response.Redirect("PainelAdministrador.aspx"); }
            else if (Palestrante.Contains(usuario.Tipo)) { Response.Redirect("PainelPalestrante.aspx"); }
            else if (Moderador.Contains(usuario.Tipo)) { Response.Redirect("PainelModerador.aspx"); }
            else { Response.Redirect("Default.aspx"); }
        }
    }
}