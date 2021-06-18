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

            try
            {
                if (Session["EditarADM"].ToString() == "Sim")
                {
                    CarregarPalestraADM();
                    return;
                }
            }
            catch { }
            novaPalestra();
            
           
        }
        protected void novaPalestra()
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado", "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };

            if (Palestrante.Contains(usuario.Tipo)) { ddlPalestrantes.SelectedValue = usuario.Username; ddlPalestrantes.Enabled = false; }
        }
        protected void CarregarPalestraADM()
        {
            txtDataPalestra.Attributes.Remove("TextMode");
            int id = Convert.ToInt32(Session["ID"]);            
            PalestraDAL pDAL = new PalestraDAL();
            Palestra p = pDAL.ObterPalestra(id);
            int idPalestrante = p.IDPalestrante;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario us = uDAL.BuscarEmail(idPalestrante);

            if (p.IDEvento != 0)
            {
                EventoDAL eDAL = new EventoDAL();
                Evento ev = eDAL.ObterEvento2(p.IDEvento);
                lblParticipaEvento.Text = "Palestra relacionada ao evento \"" + ev.EventoTitulo + "\"";
                lblParticipaEvento.Visible = true;
                btnParticiparEvento.Visible = true;
            }
            else
            {
                if (p.Acervo == false)
                {
                    lblAcervo.Text = "Palestra continuada";
                    lblAcervo.Visible = true;
                    btnAcervo.Visible = true;
                }
            }

            txtTituloPalestra.Text = p.PalestraTitulo;
            txtSubTituloPalestra.Text = p.PalestraSubTitulo;
            txtLinkPalestra.Text = p.PalestraLink;
            ddlCategoria.SelectedValue = p.PalestraCategoria;
            txtSinopseP1Palestra.Text = p.PalestraSinopseP1;
            txtSinopseP2Palestra.Text = p.PalestraSinopseP2;
            txtSinopseP3Palestra.Text = p.PalestraSinopseP3;
            txtSinopseP4Palestra.Text = p.PalestraSinopseP4;
            ddlPalestrantes.SelectedValue = us.Username;
            txtTempoPalestra.Text = p.PalestraDuracao;
            txtDataPalestra.Text = p.PalestraData.ToString("yyyy-MM-dd");
            cbAutorizarPalestra.Checked = p.PalestraAutoriza;
            byte[] bytes = p.PalestraCapa;
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgPalestra.ImageUrl = "data:image/png;base64," + base64String;

            btnAtualizarPalestra.Visible = true;
            btnSalvarPalestra.Visible = false;
            btnVisualizar.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
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

        protected void btnAtualizarPalestra_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(Session["ID"]);
            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            PalestraDAL pDAL = new PalestraDAL();

            Usuario usuario = uDAL.BuscarID(cod);

            int criador = usuario.UserId;
            Usuario usuario1 = uDAL.BuscarName(ddlPalestrantes.SelectedValue);
            Usuario usuario2 = uDAL.BuscarID(usuario1.Email);
            int pales = usuario2.UserId;

            Palestra objPalestra = new Palestra();
            objPalestra.PalestraAprovada = true;

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
                Palestra pa = pDAL.ObterPalestra(id);
                
                byte[] FotoCapa = pa.PalestraCapa;
                string base64String = Convert.ToBase64String(FotoCapa, 0, FotoCapa.Length);
                objPalestra.PalestraCapa = FotoCapa;
                
            }

            objPalestra.IDPalestra = id;
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

            pDAL.AtualizarPalestra(objPalestra);
            Response.Redirect("Default.aspx");
        }

        protected void btnAcervo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["ID"]);
            PalestraDAL pDAL = new PalestraDAL();
            pDAL.AddPalestraAcervo(id);
        }

        protected void btnParticiparEvento_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["ID"]);
            EventoDAL eDAL = new EventoDAL();            
            PalestraDAL pDAL = new PalestraDAL();
            Palestra p = pDAL.ObterPalestra(id);

            if (p.IDEvento != 0)
            {
                Evento eve = eDAL.ObterEvento2(p.IDEvento);
                if (eve.Acervo == false)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Não foi possível remover. Essa Palestra esta participando do evento \"" + eve.EventoTitulo + "\" que ainda esta em andamento.');", true);
                    return;
                }
                else
                {
                    pDAL.RemoverPalestraDoEvento(id);
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Removido com suceso.');", true);
                }
            }
        }
    }
}