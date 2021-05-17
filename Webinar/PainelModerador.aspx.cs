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
    public partial class PainelModerador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SolicitacaoPalestrante();
                SolicitacaoPalestra();
            }
        }
        public void SolicitacaoPalestrante()
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            gvPalestrante.DataSource = uDAL.PalestrantePendente();
            gvPalestrante.DataBind();
            ViewState["sortOrder"] = "";
            BindGridView("", "");
        }
        public void SolicitacaoPalestra()
        { 
            PalestraDAL pDAL = new PalestraDAL();
            gvPalestra.DataSource = pDAL.PalestraPendente();
            gvPalestra.DataBind();
            ViewState["sortOrder"] = "";
            BindGridView2("", "");
        }
        protected void gvPalestrante_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGridView(e.SortExpression, sortOrder);
        }
        public void BindGridView(string sortExp, string sortDir)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            DataTable dt = uDAL.ListarPalestrantePendente();

            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dt.DefaultView;

                if (sortExp != string.Empty)
                {
                    dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                gvPalestrante.DataSource = dv;
                gvPalestrante.DataBind();
                lblRes1.Text = "Pendencias localizadas: " + dt.Rows.Count;
                lblRes1.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblRes1.Text = "Não possuí Palestrantes com solicitações pendentes. Obrigado";                
                lblRes1.ForeColor = System.Drawing.Color.White;
                lblRes1.Font.Size = 14;
            }
        }
        public void BindGridView2(string sortExp, string sortDir)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            DataTable dt = uDAL.ListarPalestraPendente();

            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dt.DefaultView;

                if (sortExp != string.Empty)
                {
                    dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                gvPalestra.DataSource = dv;
                gvPalestra.DataBind();
                lblRes2.Text = "Pendencias localizadas: " + dt.Rows.Count;                
                lblRes2.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblRes2.Text = "Não possui Palestras com solicitações pendentes. Obrigado";
                lblRes2.ForeColor = System.Drawing.Color.White;
                lblRes2.Font.Size = 14;
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
        public void gvPalestrante_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "SendPalestrante") return;
            int id = Convert.ToInt32(e.CommandArgument);
            Session["id"] = id;
            Response.Redirect("PreviewPalestrante.aspx");
        }
        protected void gvPalestra_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGridView2(e.SortExpression, sortOrder);
        }
        public void gvPalestra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "SendPalestra") return;
            int id = Convert.ToInt32(e.CommandArgument);
            Session["id"] = id;
            Response.Redirect("PreviewPalestra.aspx");
        }
    }
}