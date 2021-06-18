using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Uno.Extensions;

namespace Webinar
{
    public partial class PreviewEventoON : System.Web.UI.Page
    {
        string model = HttpContext.Current.Request.QueryString["a"].ToString();
        Panel pnlDiv9;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            EventoDAL eDAL = new EventoDAL();
            Evento evento = eDAL.ObterEvento(model);

            lblDtInicio.Text = evento.EventoDtIni.ToString("dd/MM/yyyy");
            lblDtTermino.Text = evento.EventoDtTer.ToString("dd/MM/yyyy");
            lblSinopse1Evento.Text = evento.EventoSinopseP1;
            lblSinopse2Evento.Text = evento.EventoSinopseP2;
            lblTitulo.Text = evento.EventoTitulo;
            lblSubTituloEvento.Text = evento.EventoSubTitulo;
            int id = evento.IDEvento;

            byte[] bytes1 = evento.EventoCapa;
            string base64String1 = Convert.ToBase64String(bytes1, 0, bytes1.Length);

            imgEvento.ImageUrl = "data:image/png;base64," + base64String1;

            PalestraDAL pDAL = new PalestraDAL();
            DataTable dt = pDAL.ContarPalestras(id);

            List<int> PalestraDia = new List<int>();
            int x = 0;
            foreach (DataRow dr in dt.Rows)
            {                
                int qtd = Convert.ToInt32(dr["Quantidade"]);
                PalestraDia.Insert(x, qtd);
                x++;
            }

            int dias = PalestraDia.Count;
            int[] QtdPorDia = PalestraDia.ToArray();

            DataTable dd = pDAL.ObterPalestraEmEvento(id);
            
            int z = 0;
            for (int xx = 1; xx < dias+1; xx++)
            {
                this.AddDias(xx);

                for (int y = 0; y < QtdPorDia[xx - 1]; y++)
                {                   
                    int idPalestra = Convert.ToInt32(dd.Rows[z]["IDPalestra"]);
                    string pall = dd.Rows[z]["Username"].ToString();
                    byte[] bytes = (byte[])dd.Rows[z]["PalestranteFoto"];
                    string foto = Convert.ToBase64String(bytes, 0, bytes.Length);
                    string pal = dd.Rows[z]["PalestraTitulo"].ToString();
                    DateTime time = Convert.ToDateTime(dd.Rows[z]["PalestraData"]);
                    string tim = time.ToString("HH:mm");
                    this.AddPalestras(id, idPalestra, pall, foto, pal, tim);
                    z++;
                }
            }
            

        }
        public void AddDias(int xx)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "nav-item");
            ulDias.Controls.Add(li);

            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes.Add("class", "nav-link");
            a.ID = "dia" + xx.ToString();
            a.Attributes.Add("style", "width:240px; margin-bottom:4px;");
            a.Attributes.Add("href", "#day" + xx.ToString());
            a.Attributes.Add("role", "tab");
            a.Attributes.Add("data-toggle", "tab");
            a.InnerText = "Dia " + xx.ToString();
            li.Controls.Add(a);

            pnlDiv9 = new Panel();
            pnlDiv9.Attributes.Add("role", "tabpanel");
            pnlDiv9.CssClass = "col-lg-9 tab-pane fade";
            pnlDiv9.Attributes.Add("id", "day" + xx.ToString());
            divDias.Controls.Add(pnlDiv9);
        }
        public void AddPalestras(int id1, int id, string pp, string f, string p, string t)
        {
            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes.Add("href", "PreviewPalestraON.aspx?a=" + id + "&b=" + id1);
            pnlDiv9.Controls.Add(a);

            Panel pnlDiv10 = new Panel();
            pnlDiv10.CssClass = "row schedule-item";
            a.Controls.Add(pnlDiv10);

            Panel pnlDiv11 = new Panel();
            pnlDiv11.CssClass = "col-md-3";
            pnlDiv10.Controls.Add(pnlDiv11);            

            Label PalestraHora = new Label();
            PalestraHora.Text = t;
            PalestraHora.Attributes.Add("style", "color: White; font-size: 20px;");
            pnlDiv11.Controls.Add(PalestraHora);

            Panel pnlDiv12 = new Panel();
            pnlDiv12.CssClass = "col-md-9";
            pnlDiv10.Controls.Add(pnlDiv12);

            Panel pnlDiv13 = new Panel();
            pnlDiv13.CssClass = "speaker";
            pnlDiv12.Controls.Add(pnlDiv13);

            Image img = new Image();
            img.ImageUrl = "data:image;base64," + f;
            pnlDiv13.Controls.Add(img);

            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerText = p;
            pnlDiv12.Controls.Add(h4);

            HtmlGenericControl palestrante = new HtmlGenericControl("p");
            palestrante.InnerText = pp;
            pnlDiv12.Controls.Add(palestrante);
        }
    }
}