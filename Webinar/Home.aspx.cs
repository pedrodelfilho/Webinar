using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class Home : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void VerificarUsuario()
        {
            string Usr = txtCadastrarNome.Text;
            string Mail = txtCadastrarEmail.Text;

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.VerificarUsuarios(Usr, Mail);

            if ( usuario == null)
            {
                Usuario objUsuario = new Usuario();
                objUsuario.Username = txtCadastrarNome.Text;
                objUsuario.City = ddlCidade.SelectedValue;
                objUsuario.NascDate = Convert.ToDateTime(txtDtNasc.Text).Date;
                objUsuario.Password = txtCadastrarSenha.Text;
                objUsuario.Email = txtCadastrarEmail.Text;

                uDAL.InserirUsuario(objUsuario);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Nome de usuário e/ou E-mail já possuí cadastro.');", true);
            }
        }

        protected void btnCadastrarUsuario_Click(object sender, EventArgs e)
        {
            VerificarUsuario();         
            
        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            
        }
    }
}