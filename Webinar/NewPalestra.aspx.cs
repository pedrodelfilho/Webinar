using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class NewPalestra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioDAL uDAL = new UsuarioDAL();
                string s = HttpContext.Current.User.Identity.Name;
                Usuario usuario = uDAL.BuscarID(s);

                string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado", "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };

                if (Palestrante.Contains(usuario.Tipo)) { ddlPalestrantes.SelectedValue = usuario.Username; ddlPalestrantes.Enabled = false; }
            }
        }

        protected void btnSalvarPalestra_Click(object sender, EventArgs e)
        {
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (cbAutorizarPalestra.Checked) { lblcb.Visible = false; }
            else
            {
                lblcb.Visible = true;
                return;
            }

            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            PalestraDAL pDAL = new PalestraDAL();

            Usuario usuario = uDAL.BuscarID(cod);

            int criador = usuario.UserId;
            Usuario usuario1 = uDAL.BuscarName(ddlPalestrantes.SelectedValue); 
            Usuario usuario2 = uDAL.BuscarID(usuario1.Email);
            int pales = usuario2.UserId;

            Palestra objPalestra = new Palestra();
            if (Administrador.Contains(usuario.Tipo))
            {
                objPalestra.PalestraAprovada = true;
            }
            else { objPalestra.PalestraAprovada = false; }
            if (fuImageCapa.HasFile)
            {
                string empFilename = Path.GetFileName(fuImageCapa.PostedFile.FileName);
                string FilecontentType = fuImageCapa.PostedFile.ContentType;
                Stream s = fuImageCapa.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] FotoCapa = br.ReadBytes((Int32)s.Length);
                objPalestra.PalestraCapa = FotoCapa;
            }
            else
            {
                if (Session["fotocapa"] == null)
                {
                    objPalestra.PalestraCapa = null;
                }
                else
                {
                    byte[] FotoCapa = (byte[])Session["fotocapa"];
                    string base64String = Convert.ToBase64String(FotoCapa, 0, FotoCapa.Length);
                    objPalestra.PalestraCapa = FotoCapa;
                }
            }


            objPalestra.IDPalestrante = pales;
            objPalestra.PalestraCriador = criador;
            objPalestra.PalestraLink = txtLinkPalestra.Text;
            objPalestra.PalestraDtCriacao = DateTime.Now;
            objPalestra.PalestraCategoria = ddlCategoria.SelectedValue;
            objPalestra.PalestraTitulo = txtTituloPalestra.Text;
            objPalestra.PalestraSubTitulo = txtSubTituloPalestra.Text;
            objPalestra.PalestraSinopseP1 = txtSinopseP1Palestra.Text;
            objPalestra.PalestraSinopseP2 = txtSinopseP2Palestra.Text;
            objPalestra.PalestraSinopseP3 = txtSinopseP3Palestra.Text;
            objPalestra.PalestraSinopseP4 = txtSinopseP4Palestra.Text;
            objPalestra.PalestraDuracao = txtTempoPalestra.Text;
            objPalestra.PalestraData = Convert.ToDateTime(txtDataPalestra.Text);
            
            objPalestra.PalestraAutoriza = cbAutorizarPalestra.Checked;

            pDAL.InserirPalestra(objPalestra);

            txtLinkPalestra.Text = String.Empty;
            ddlCategoria.SelectedValue = null;
            txtTituloPalestra.Text = String.Empty;
            txtSubTituloPalestra.Text = String.Empty;
            txtSinopseP1Palestra.Text = String.Empty;
            txtSinopseP2Palestra.Text = String.Empty;
            txtSinopseP3Palestra.Text = String.Empty;
            txtSinopseP4Palestra.Text = String.Empty;
            txtTempoPalestra.Text = String.Empty;
            txtDataPalestra.Text = String.Empty;
            cbAutorizarPalestra.Checked = false;
            imgPalestra.ImageUrl = string.Empty;

            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Palestra criada com sucesso.');", true);
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {            

            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            PalestraDAL pDAL = new PalestraDAL();

            Usuario usuario = uDAL.BuscarID(cod);
            int criador = usuario.UserId;
            Usuario usuario1 = uDAL.BuscarName(ddlPalestrantes.SelectedValue);
            Usuario usuario2 = uDAL.BuscarID(usuario1.Email);
            int pales = usuario2.UserId;

            if (fuImageCapa.HasFile)
            {
                string empFilename = Path.GetFileName(fuImageCapa.PostedFile.FileName);
                string FilecontentType = fuImageCapa.PostedFile.ContentType;
                Stream s = fuImageCapa.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] FotoCapa = br.ReadBytes((Int32)s.Length);
                Session["fotocapa"] = FotoCapa;
            }
            else
            {
                if (Session["fotocapa"] == null)
                {
                    Session["fotocapa"] = null;
                }
                else
                {
                    byte[] FotoCapa = (byte[])Session["fotocapa"];
                    string base64String = Convert.ToBase64String(FotoCapa, 0, FotoCapa.Length);
                    Session["fotocapa"] = FotoCapa;
                }
            }

            Session["identificador"] = "NovaPalestra";
            Session["palestrante"] = pales;
            Session["criador"] = criador;
            Session["data"] = date;
            Session["categoria"] = ddlCategoria.SelectedValue;
            Session["titulo"] = txtTituloPalestra.Text;
            Session["subtitulo"] = txtSubTituloPalestra.Text;
            Session["link"] = txtLinkPalestra.Text;
            Session["bio1"] = txtSinopseP1Palestra.Text;
            Session["bio2"] = txtSinopseP2Palestra.Text;
            Session["bio3"] = txtSinopseP3Palestra.Text;
            Session["bio4"] = txtSinopseP4Palestra.Text;
            Session["tempopalestra"] = txtTempoPalestra.Text;
            Session["datapalestra"] = txtDataPalestra.Text;
            Session["foto2"] = fuImageCapa.PostedFile.InputStream;
            Session["email"] = usuario.Email;
            Session["autoriza"] = cbAutorizarPalestra.Checked;

            Response.Redirect("PreviewPalestra.aspx");
        }

        protected void btnVoltarPerfil_Click(object sender, EventArgs e)
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