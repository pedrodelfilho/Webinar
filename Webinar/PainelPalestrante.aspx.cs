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
    public partial class PainelPalestrante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVoltarPerfil_Click(object sender, EventArgs e)
        {   
            PanelNewPalestra.Visible = false;
            PanelPerfil.Visible = true;

        }

        protected void btnAddPalestra_Click(object sender, EventArgs e)
        {
            PanelPerfil.Visible = false;
            PanelNewPalestra.Visible = true;
        }
    }
}