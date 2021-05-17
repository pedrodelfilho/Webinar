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
    public partial class PainelUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUsuario();
            }

        }

        protected void btnSalvarPerfil_Click(object sender, EventArgs e)
        {
            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);
            Usuario objUsuario = new Usuario();

            objUsuario.UserId = usuario.UserId;
            objUsuario.Username = txtNome.Text;
            uDAL.AtualizarNomeUsuario(objUsuario);

            Convidado objConvidado = new Convidado();
            objConvidado.UserId = usuario.UserId;

            if(fuUsuario.HasFile)
            {
                string empFilename = Path.GetFileName(fuUsuario.PostedFile.FileName);
                string FilecontentType = fuUsuario.PostedFile.ContentType;
                Stream s = fuUsuario.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] Databytes = br.ReadBytes((Int32)s.Length);
                objConvidado.FotoConvidado = Databytes;
            }
            else { objConvidado.FotoConvidado = null; }

            
            switch (ddlSexo.SelectedValue)
            {
                case "Masculino":
                    objConvidado.SexoConvidado = 'M';
                    break;
                case "Feminio":
                    objConvidado.SexoConvidado = 'F';
                    break;
                case "Outros":
                    objConvidado.SexoConvidado = 'O';
                    break;
            }
            objConvidado.ConvidadoBioP1 = txtBio.Text;
            objConvidado.ConvidadoDtNasc = txtDtNasc.Text;
            objConvidado.EscolaridadeConvidado = ddlEscolaridade.SelectedValue;
            objConvidado.ConvidadoCidadeUF = ddlCidade.SelectedValue;
            objConvidado.ConvidadoReceberEmail = cbReceberEmail.Checked;

            uDAL.InserirConvidadoAtualizado(objConvidado);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Perfil Atualizado');", true);

            if (cbReceberEmail.Checked)
            {
                NotificarDAL nDAL = new NotificarDAL();
                Notificar email = nDAL.VerificarEmail(cod);

                if(email.NotificarEmail == null)
                {
                    nDAL.CadastrarEmailNotificar(cod);
                }
            }
            CarregarUsuario();

        }  
        
        protected void CarregarUsuario()
        {

            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);
            Usuario objUsuario = uDAL.ObterUsuario(usuario.UserId);
            Convidado objConvidado = uDAL.ObterConvidado(usuario.UserId);

            lblNome.Text = objUsuario.Username;
            lblEmail.Text = objUsuario.Email;
            txtNome.Text = objUsuario.Username;

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
            
            try { lblCidade.Text = objConvidado.ConvidadoCidadeUF; } catch { lblCidade.Text = null; }
            try { txtDtNasc.Text = objConvidado.ConvidadoDtNasc; } catch { txtDtNasc.Text = "01/01/2000"; }
            ddlCidade.SelectedValue = objConvidado.ConvidadoCidadeUF;
            ddlEscolaridade.SelectedValue = objConvidado.EscolaridadeConvidado;
            txtBio.Text = objConvidado.ConvidadoBioP1;
            switch (objConvidado.SexoConvidado)
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
            cbReceberEmail.Checked = objConvidado.ConvidadoReceberEmail;
        }
    }
}