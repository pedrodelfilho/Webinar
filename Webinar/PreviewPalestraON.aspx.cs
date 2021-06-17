using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Webinar
{
    public partial class PreviewPalestraON : System.Web.UI.Page
    {

        int id = Convert.ToInt32(HttpContext.Current.Request.QueryString["a"]); // ID do Evento
        int id1 = Convert.ToInt32(HttpContext.Current.Request.QueryString["b"]);// ID da Palestra

        protected void Page_Load(object sender, EventArgs e)
        {
           
            PalestraDAL pDAL = new PalestraDAL();
            DataTable dt = pDAL.PreencherHomePalestraAcervo();

            if (dt != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    btnAssistir.Visible = true;
                    btnSeIncrever.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Para assitir este conteúdo, é necessário realizar o Login!');", true);
                    btnAssistir.Visible = false;
                    btnSeIncrever.Visible = false;
                }
            }
            else
            {
                btnAssistir.Visible = false;
                btnSeIncrever.Visible = true;
            }
            

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
                lblDataExibir.Text = objPalestra.PalestraData.ToString("dd/MM/yyyy HH:mm");
                lblDuracao.Text = objPalestra.PalestraDuracao;
                lblSinopseP1.Text = objPalestra.PalestraSinopseP1;
                lblSinopseP2.Text = objPalestra.PalestraSinopseP2;
                lblSinopseP3.Text = objPalestra.PalestraSinopseP3;
                lblSinopseP4.Text = objPalestra.PalestraSinopseP4;
                
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

        protected void btnSeIncrever_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string cod = HttpContext.Current.User.Identity.Name;
                UsuarioDAL uDAL = new UsuarioDAL();
                Usuario usuario = uDAL.BuscarID(cod);

                EventoDAL eDAL = new EventoDAL();
                DataTable dt = eDAL.VerifySendLinkEvento(usuario.UserId, id, id1);

                if (dt.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Você já esta inscrito. Obrigado.');", true);
                }
                else
                {
                    eDAL.SendLinkEvento(usuario.UserId, id, id1);
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Inscrição realizada com sucesso. No dia do evento, você receberá o link de acesso 1 hora antes do início. Obrigado.');", true);
                }
            }
            else
            {               
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Para se inscrever é necessário realizar o Login!');", true);                
            }
        }

        protected void btnAssistir_Click(object sender, EventArgs e)
        {
            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);

            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.ObterPalestra(id);

            Session["idUser"] = usuario.UserId;
            Session["idPalestra"] = id;
            Session["link"] = objPalestra.PalestraLink;
            Response.Redirect("Player.aspx");
        }
    }    
}