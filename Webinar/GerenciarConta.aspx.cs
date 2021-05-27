using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class GerenciarConta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            PanelAlterarSenha.Visible = true;
            PanelMeuHistorico.Visible = false;
            PanelMeusCertificados.Visible = false;
        }
        protected void btnMeusCertificados_Click(object sender, EventArgs e)
        {
            PanelAlterarSenha.Visible = false;
            PanelMeuHistorico.Visible = false;
            PanelMeusCertificados.Visible = true;
            ViewState["sortOrder"] = "";
            BindGridViewCertificados("", "");
        }
        protected void btnMeuHistorico_Click(object sender, EventArgs e)
        {
            PanelAlterarSenha.Visible = false;
            PanelMeuHistorico.Visible = true;
            PanelMeusCertificados.Visible = false;
        }
        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            string[] Convidado = { "Convidado" };
            string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado" };
            string[] Moderador = { "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (Convidado.Contains(usuario.Tipo))
            {
                Response.Redirect("PainelUsuario.aspx");
            }
            else if (Palestrante.Contains(usuario.Tipo))
            {
                Response.Redirect("PainelPalestrante.aspx");
            }
            else if (Moderador.Contains(usuario.Tipo))
            {
                Response.Redirect("PainelUsuario.aspx");                
            }
            else if (Administrador.Contains(usuario.Tipo))
            {
                Response.Redirect("PainelUsuario.aspx");                
            }
            else { Response.Redirect("Default.aspx"); }
        }
        protected void btnSalvarNovaSenha_Click(object sender, EventArgs e)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            if(Criptografia.GetMD5Hash(txtSenhaAtual.Text.Trim()) == usuario.Password)
            {
                txtSenhaAtual.BorderColor = System.Drawing.Color.Empty;
                lblSenhaAtual.Text = string.Empty;
                string a = txtNovaSenha.Text;
                if(a.Length < 8)
                {
                    txtNovaSenha.BorderColor = System.Drawing.Color.Red;
                    lblNovaSenha.Text = "A senha deve conter no minimo 8 digitos";
                    return;
                }
                else
                {
                    txtNovaSenha.BorderColor = System.Drawing.Color.Empty;
                    lblNovaSenha.Text = string.Empty;
                    try
                    {
                        int id = usuario.UserId;
                        string pw = Criptografia.GetMD5Hash(txtNovaSenha.Text.Trim());
                        uDAL.AtualizarSenhaUsuario(id, pw);
                        lblTrocarSenha.Text = "Senha alterada com sucesso";
                        lblTrocarSenha.ForeColor = System.Drawing.Color.Green;
                    }
                    catch
                    {
                        lblTrocarSenha.Text = "Ocorreu um erro, tente novamente mais tarde";
                        lblTrocarSenha.ForeColor = System.Drawing.Color.Red;
                    }
                }                
            }
            else
            {
                txtSenhaAtual.BorderColor = System.Drawing.Color.Red;
                lblSenhaAtual.Text = "Senha incorreta";
            }
        }

        public string sortOrder 
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        public void BindGridViewCertificados(string sortExp, string sortDir)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);
            int id = usuario.UserId;
            CertificadoDAL cDAL = new CertificadoDAL();
            DataTable dt = cDAL.ListarCertificados(id);

            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dt.DefaultView;

                if (sortExp != string.Empty)
                {
                    dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                gvCertificados.DataSource = dv;
                gvCertificados.DataBind();
                lblCertificados.Text = "Localizado " + dt.Rows.Count + " certificados.";
                lblCertificados.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblCertificados.Text = "Nenhum certificado localizado, verifique o andamento de suas vizualizações em \"Meu Histórico\".";
                lblCertificados.ForeColor = System.Drawing.Color.White;
                lblCertificados.Font.Size = 14;
            }
        }

        protected void gvCertificados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "SendCertificados")
            {
                int id = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void gvCertificados_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGridViewCertificados(e.SortExpression, sortOrder);
        }
    }
}