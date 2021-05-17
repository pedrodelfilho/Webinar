using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class Master : System.Web.UI.MasterPage
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated) { BuscarNm(); }            
        }
        private void BuscarNm()
        {            
            UsuarioDAL uDAL = new UsuarioDAL();            
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.BuscarID(s);
            Palestrante objPalestrante = uDAL.ObterPalestrante(usuario.UserId);
            Convidado objConvidado = uDAL.ObterConvidado(usuario.UserId);

            (this.LoginView1.FindControl("lblOla") as Label).Text = usuario.Username;

            string[] Convidado = { "Convidado" };
            string[] Palestrante = { "Palestrante", "Convidado, Palestrante", "Palestrante, Convidado" };
            string[] Moderador = { "Moderador", "Moderador, Palestrante", "Palestrante, Moderador", "Convidado, Moderador", "Moderador, Convidado" };
            string[] Administrador = { "Administrador", "Convidado, Administrador", "Administrador, Convidado", "Administrador, Palestrante", "Palestrante, Administrador" };

            if (Convidado.Contains(usuario.Tipo)) 
            {
                (this.LoginView1.FindControl("PainelAdm") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("PainelMod") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("EditarPerfil") as HtmlAnchor).Attributes["href"] = "PainelUsuario.aspx";

                if (objConvidado.FotoConvidado == null)
                {
                    (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "~/img/anonimo.jpg";
                }
                else
                {
                    byte[] bytes = objConvidado.FotoConvidado;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "data:image/png;base64," + base64String;
                }
            }
            else if (Palestrante.Contains(usuario.Tipo)) 
            {
                (this.LoginView1.FindControl("EditarPerfil") as HtmlAnchor).Attributes["href"] = "PainelPalestrante.aspx";
                (this.LoginView1.FindControl("PainelAdm") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("PainelMod") as HtmlGenericControl).Visible = false;
                if (objPalestrante.PalestranteFoto == null)
                {
                    (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "~/img/anonimo.jpg";
                }
                else
                {
                    byte[] bytes = objPalestrante.PalestranteFoto;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "data:image/png;base64," + base64String;
                }
            }
            else if (Moderador.Contains(usuario.Tipo)) 
            {
                (this.LoginView1.FindControl("PainelAdm") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("PainelMod") as HtmlGenericControl).Visible = true;
                (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "~/img/anonimo.jpg"; 
            }
            else if (Administrador.Contains(usuario.Tipo)) 
            {
                (this.LoginView1.FindControl("PainelAdm") as HtmlGenericControl).Visible = true;
                (this.LoginView1.FindControl("PainelMod") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "~/img/anonimo.jpg"; 
            }
            else 
            {
                (this.LoginView1.FindControl("PainelAdm") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("PainelMod") as HtmlGenericControl).Visible = false;
                (this.LoginView1.FindControl("imgLogin") as Image).ImageUrl = "~/img/anonimo.jpg"; 
            }
        }        
    }
}