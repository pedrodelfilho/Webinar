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
    public partial class PainelPalestrante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPalestrante();
            }
        }

        protected void btnAddPalestra_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewPalestra.aspx");
        }
        protected void btnEditTwiter_Click(object sender, EventArgs e)
        {
            txtTwiter.Enabled = true;
            txtTwiter.Focus();
        }

        protected void btnEditFacebook_Click(object sender, EventArgs e)
        {
            txtFacebook.Enabled = true;
            txtFacebook.Focus();
        }

        protected void btnEditLinkedin_Click(object sender, EventArgs e)
        {
            txtLinkedin.Enabled = true;
            txtLinkedin.Focus();
        }

        protected void btnEditGoogle_Click(object sender, EventArgs e)
        {
            txtGoogle.Enabled = true;
            txtGoogle.Focus();
        }

        protected void btnSalvarPerfil_Click(object sender, EventArgs e)
        {
            if (cbAutorizarPerfil.Checked) { lblcb.Visible = false; }
            else 
            {
                lblcb.Visible = true;
                return;
            }
            DateTime a = DateTime.MinValue;
            DateTime c = Convert.ToDateTime(txtDtNasc.Text);
            if (DateTime.Compare(a, c) != -1)
            {
                lblDt.Visible = true;
                return;
            }
            else { lblDt.Visible = false; }

            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);
            Usuario objUsuario = new Usuario();

            objUsuario.UserId = usuario.UserId;
            objUsuario.Username = txtNome.Text;
            uDAL.AtualizarNomeUsuario(objUsuario);

            Palestrante objPalestrante = new Palestrante();
            objPalestrante.IDPalestrante = usuario.UserId;

            if (formFile.PostedFile.ContentLength > 0)
            {
                string empFilename = Path.GetFileName(formFile.PostedFile.FileName);
                string FilecontentType = formFile.PostedFile.ContentType;
                Stream s = formFile.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] Databytes = br.ReadBytes((Int32)s.Length);
                objPalestrante.PalestranteFoto = Databytes;
                objPalestrante.PerfilAprovado = false;
            }
            else
            {
                Palestrante objPalestrante1 = uDAL.ObterPalestrante(objPalestrante.IDPalestrante);
                if (objPalestrante1.PalestranteFoto == null)
                {
                    lblFoto.Visible = true;
                    return;
                }
                else
                { lblFoto.Visible = false; }
            }

            objPalestrante.PalestranteDtNasc = Convert.ToDateTime(txtDtNasc.Text);
            objPalestrante.PalestranteCidadeUF = ddlCidade.SelectedValue;
            switch (ddlSexo.SelectedValue)
            {
                case "Masculino":
                    objPalestrante.PalestranteSexo = 'M';
                    break;
                case "Feminino":
                    objPalestrante.PalestranteSexo = 'F';
                    break;
                case "Outros":
                    objPalestrante.PalestranteSexo = 'O';
                    break;
            }
            objPalestrante.PalestranteFormacao = ddlEscolaridade.SelectedValue;
            objPalestrante.PalestranteEspecialidade = txtEspecialidade.Text;
            objPalestrante.PalestranteBioP1 = txtBio1.Text;
            objPalestrante.PalestranteBioP2 = txtBio2.Text;
            objPalestrante.PalestranteReceberEmail = cbReceberEmail.Checked;
            objPalestrante.PalestranteAutoriza = cbAutorizarPerfil.Checked;
            objPalestrante.PalestranteTwiter = txtTwiter.Text;
            objPalestrante.PalestranteFacebook = txtFacebook.Text;
            objPalestrante.PalestranteGoogle = txtGoogle.Text;
            objPalestrante.PalestranteLinkedin = txtLinkedin.Text;

            uDAL.InserirPalestranteAtualizado(objPalestrante);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Perfil Atualizado');", true);

            if (cbReceberEmail.Checked)
            {
                NotificarDAL nDAL = new NotificarDAL();
                Notificar email = nDAL.VerificarEmail(cod);

                if (email.NotificarEmail == null)
                {
                    nDAL.CadastrarEmailNotificar(cod);
                }
            }

            CarregarPalestrante();

        }

        protected void CarregarPalestrante()
        {
            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);
            Usuario objUsuario = uDAL.ObterUsuario(usuario.UserId);
            Palestrante objPalestrante = uDAL.ObterPalestrante(usuario.UserId);

            lblNome.Text = objUsuario.Username;
            lblEmail.Text = objUsuario.Email;
            txtNome.Text = objUsuario.Username;
            aPalestrante1.InnerText = objUsuario.Username;

            if (objPalestrante.PalestranteFoto == null)
            {
                imgPalestrante.ImageUrl = "~/img/anonimo.jpg";
                imgPalestrante1.Attributes["src"] = "~/img/anonimo.jpg";
            }
            else
            {
                byte[] bytes = objPalestrante.PalestranteFoto;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgPalestrante.ImageUrl = "data:image/png;base64," + base64String;
                imgPalestrante1.Attributes["src"] = "data:image/png;base64," + base64String;
                LoginView LoginView = this.Master.FindControl("LoginView1") as LoginView;
                (LoginView.FindControl("imgLogin") as Image).ImageUrl = "data:image/png;base64," + base64String;
            }
            try
            {
                DateTime a = Convert.ToDateTime(objPalestrante.PalestranteDtNasc);
                string b = a.ToString("yyyy-MM-dd");
                txtDtNasc.Text = b;
            }
            catch { txtDtNasc.Text = "dd/MM/yyyy"; }

            
            
            try { lblCidade.Text = objPalestrante.PalestranteCidadeUF; } catch { lblCidade.Text = null; }
            ddlCidade.SelectedValue = objPalestrante.PalestranteCidadeUF;
            switch (objPalestrante.PalestranteSexo)
            {
                case 'M':
                    ddlSexo.SelectedValue = "Masculino";
                    break;
                case 'F':
                    ddlSexo.SelectedValue = "Feminino";
                    break;
                case 'O':
                    ddlSexo.SelectedValue = "Outros";
                    break;
            }
            ddlEscolaridade.SelectedValue = objPalestrante.PalestranteFormacao;
            txtEspecialidade.Text = objPalestrante.PalestranteEspecialidade;
            pPalestrante1.InnerText = objPalestrante.PalestranteEspecialidade;
            txtBio1.Text = objPalestrante.PalestranteBioP1;
            txtBio2.Text = objPalestrante.PalestranteBioP2;
            cbReceberEmail.Checked = objPalestrante.PalestranteReceberEmail;
            cbAutorizarPerfil.Checked = objPalestrante.PalestranteAutoriza;
            if (objPalestrante.PalestranteTwiter == string.Empty) { txtTwiter.Text = "Não informado"; twtPalestrante1.HRef = objPalestrante.PalestranteTwiter; }
            else { txtTwiter.Text = objPalestrante.PalestranteTwiter; }
            if (objPalestrante.PalestranteFacebook == string.Empty) { txtFacebook.Text = "Não informado"; facePalestrante1.HRef = objPalestrante.PalestranteFacebook; }
            else { txtFacebook.Text = objPalestrante.PalestranteFacebook; }
            if (objPalestrante.PalestranteGoogle == string.Empty) { txtGoogle.Text = "Não informado"; ggPalestrante1.HRef = objPalestrante.PalestranteGoogle; }
            else { txtGoogle.Text = objPalestrante.PalestranteGoogle; }
            if (objPalestrante.PalestranteLinkedin == string.Empty) { txtLinkedin.Text = "Não informado"; inPalestrante1.HRef = objPalestrante.PalestranteLinkedin; }
            else { txtLinkedin.Text = objPalestrante.PalestranteLinkedin; }
            
        }

        protected void cbAutorizarPerfilValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = cbAutorizarPerfil.Checked;
        }       
    }
}