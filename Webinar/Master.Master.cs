using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            Usuario usuario = uDAL.VerificarUsuarios(s);

            (this.LoginView1.FindControl("lblOla") as Label).Text = "Olá, " + usuario.Username;

        }
        protected void btnGerenciar_Click(object sender, EventArgs e)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            string s = HttpContext.Current.User.Identity.Name;
            Usuario usuario = uDAL.VerificarUsuarios(s);

            if (usuario.Tipo == "Convidado") { Response.Redirect("PainelUsuario.aspx"); }
            else if (usuario.Tipo == "Administrador") { Response.Redirect("PainelAdministrador.aspx"); }
            else if (usuario.Tipo == "Palestrante") { Response.Redirect("PainelPalestrante.aspx"); }
            else if (usuario.Tipo == "Moderador") { Response.Redirect("PainelModerador.aspx"); }
            else { Response.Redirect("Default.aspx"); }
        }
    }
}