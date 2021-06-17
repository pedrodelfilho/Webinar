using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using DAL;
using Models;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

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
            ViewState["sortOrder"] = "";
            BindGridViewHistorico("", "");
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
        public void BindGridViewHistorico(string sortExp, string sortDir)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);
            int id = usuario.UserId;
            CertificadoDAL cDAL = new CertificadoDAL();
            gvHistorico.DataSource = cDAL.HistoricoCertificados(id);
            gvHistorico.DataBind();
           
        }
        protected void gvCertificados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "SendCertificado")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CertificadoDownload(id);
            }
        }
        protected void gvCertificados_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGridViewCertificados(e.SortExpression, sortOrder);
        }        
        public void CertificadoDownload(int id)
        {
            CertificadoDAL cDAL = new CertificadoDAL();
            DataTable dt = cDAL.GetBackgroundCertificado();
            Certificados ObterCert = cDAL.ObterCertificado(id);  

            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);

            int idPalestra = ObterCert.IDPalestra;
            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.ObterPalestra(idPalestra);

            TimeSpan time = ObterCert.Alvo;
            string time1 = time.ToString();
            string[] t = time1.Split(':');
            string tempo = null;
            int t1 = Convert.ToInt32(t[0]);
            int t2 = Convert.ToInt32(t[1]);
            int t3 = Convert.ToInt32(t[2]);
            if (t1 != 0)
            {
                tempo = t1.ToString() + " horas ";
            }
            if (t2 != 0)
            {
                tempo += t2.ToString() + " minutos ";
            }
            DateTime fim = ObterCert.DtFinal;
            string mes = null;
            switch (fim.Month)
            {
                case 01:
                    mes = "janeiro";
                    break;
                case 02:
                    mes = "feveiro";
                    break;
                case 03:
                    mes = "março";
                    break;
                case 04:
                    mes = "abril";
                    break;
                case 05:
                    mes = "maio";
                    break;
                case 06:
                    mes = "junho";
                    break;
                case 07:
                    mes = "julho";
                    break;
                case 08:
                    mes = "agosto";
                    break;
                case 09:
                    mes = "setembro";
                    break;
                case 10:
                    mes = "outubro";
                    break;
                case 11:
                    mes = "novembro";
                    break;
                case 12:
                    mes = "dezembro";
                    break;
            }
            string name = usuario.Username;
            string curso = objPalestra.PalestraTitulo;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Certificado";

            byte[] bytes = (byte[])dt.Rows[0]["BackgroundCert"];
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XImage imagen = XImage.FromStream(ms);

                PdfPage page = document.AddPage();
                page.Width = 1200;
                page.Height = 900;
                XFont font = new XFont("Tahoma", 32, XFontStyle.BoldItalic);
                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(imagen, 0, 0, 1200, 900);
                gfx.DrawString("Certificamos que " + name, font, XBrushes.Black, new XRect(0, 300, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("concluiu com êxito " + tempo + "total da Palestra", font, XBrushes.Black, new XRect(0, 330, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString(curso, font, XBrushes.Black, new XRect(0, 360, page.Width, page.Height), XStringFormats.TopCenter);
                gfx.DrawString("em " + fim.Day + " de " + mes + " de " + fim.Year, font, XBrushes.Black, new XRect(0, 390, page.Width, page.Height), XStringFormats.TopCenter);
                const string filename = "Certificado.pdf";
                document.Save(filename);
                Process.Start(filename);
            }            
        }
    }
}