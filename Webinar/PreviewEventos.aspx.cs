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

namespace Webinar
{
    public partial class PreviewEventos : System.Web.UI.Page
    {
        Panel pnlDiv9;
        protected void Page_Load(object sender, EventArgs e)
        {
            var DtInicio = (DateTime)Session["DtInicio"];
            var DtTermino = (DateTime)Session["DtTermino"];
            var TituloEvento = Session["TituloEvento"];
            var SubTituloEvento = Session["SubTituloEvento"];

            imgEvento.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])Session["CapaEvento"]);

            lblDtInicio.Text = DtInicio.ToString("dd/MM/yyyy");
            lblDtTermino.Text = DtTermino.ToString("dd/MM/yyyy");
            lblSinopse1Evento.Text = Session["Sinopse1Evento"].ToString();
            lblSinopse2Evento.Text = Session["Sinopse2Evento"].ToString();
            lblSubTituloEvento.Text = SubTituloEvento.ToString();
            lblTitulo.Text = TituloEvento.ToString();

            int dias = Convert.ToInt32(Session["dias"]);
            var AllPalestras = (string[])Session["AllPalestras"];
            var AllTimes = (string[])Session["AllTimes"];
            var lista = (List<int>)Session["itens"];

            int[] QtdPorDia = lista.ToArray();

            PalestraDAL pDAL = new PalestraDAL();
            List<string> palestrante = new List<string>();
            List<string> palestranteFoto = new List<string>();
            for (int x = 0; x < AllPalestras.Length; x++)
            {
                DataTable dt = pDAL.AdicionarPalestraEmEvento(AllPalestras[x]);
                palestrante.Add(dt.Rows[0]["Username"].ToString());
                byte[] bytes = (byte[])dt.Rows[0]["PalestranteFoto"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                palestranteFoto.Add(base64String);
            }


            int z = 0;
            for (int x = 1; x != dias; x++)
            {
                this.AddDias(x);
                
                for (int y = 0; y < QtdPorDia[x - 1]; y++)
                {
                    string pall = palestrante[z];
                    var foto = palestranteFoto[z];
                    string pal = AllPalestras[z];
                    string tim = AllTimes[z];
                    this.AddPalestras(pall, foto, pal, tim);
                    z++;
                }
            }
        }
        public void AddDias(int x)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "nav-item");
            ulDias.Controls.Add(li);

            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes.Add("class", "nav-link");
            a.ID = "dia" + x.ToString();
            a.Attributes.Add("style", "width:240px; margin-bottom:4px;");
            a.Attributes.Add("href", "#day" + x.ToString());
            a.Attributes.Add("role", "tab");
            a.Attributes.Add("data-toggle", "tab");
            a.InnerText = "Dia " + x.ToString();
            li.Controls.Add(a);

            pnlDiv9 = new Panel();
            pnlDiv9.Attributes.Add("role", "tabpanel");
            pnlDiv9.CssClass = "col-lg-9 tab-pane fade";
            pnlDiv9.Attributes.Add("id", "day" + x.ToString());
            divDias.Controls.Add(pnlDiv9);
        }
        public void AddPalestras(string pp, string f, string p, string t)
        {
            Panel pnlDiv10 = new Panel();
            pnlDiv10.CssClass = "row schedule-item";
            pnlDiv9.Controls.Add(pnlDiv10);

            Panel pnlDiv11 = new Panel();
            pnlDiv11.CssClass = "col-md-3";
            pnlDiv10.Controls.Add(pnlDiv11);

            TextBox PalestraHora = new TextBox();
            PalestraHora.Attributes.Add("type", "time");
            PalestraHora.Width = 120;
            PalestraHora.Attributes.Add("class", "form-control");
            PalestraHora.Attributes.Add("style", "background-color: transparent; border-color: transparent; color: white;");
            PalestraHora.Text = t;
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
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var DtInicio = (DateTime)Session["DtInicio"];
            var DtTermino = (DateTime)Session["DtTermino"];

            EventoDAL eDAL = new EventoDAL();
            Evento objEvento = new Evento();

            string cod = HttpContext.Current.User.Identity.Name;

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);

            objEvento.IDAdm = usuario.UserId;
            objEvento.EventoTitulo = Session["TituloEvento"].ToString();
            objEvento.EventoSubTitulo = Session["SubTituloEvento"].ToString();
            objEvento.EventoSinopseP1 = Session["Sinopse1Evento"].ToString();
            objEvento.EventoSinopseP2 = Session["Sinopse2Evento"].ToString();
            objEvento.EventoDtIni = DtInicio;
            objEvento.EventoDtTer = DtTermino;
            objEvento.EventoCapa = (byte[])Session["CapaEvento"];
            objEvento.ModResponsavel = Session["ModEvento"].ToString();
            eDAL.InserirEvento(objEvento);

            Evento evento = eDAL.ObterEvento(Session["TituloEvento"].ToString());
            int id = evento.IDEvento;

            PalestraDAL pDAL = new PalestraDAL();

            int dias = Convert.ToInt32(Session["dias"]);
            var AllPalestras = (string[])Session["AllPalestras"];
            var AllTimes = (string[])Session["AllTimes"];
            var lista = (List<int>)Session["itens"];
            int[] QtdPorDia = lista.ToArray();

            DateTime data;

            int z = 0;
            for (int x = 1; x != dias; x++)
            {
                for (int y = 0; y < QtdPorDia[x - 1]; y++)
                {
                    data = DtInicio.AddDays(x - 1);
                    string s = AllTimes[z];
                    string[] a = s.Split(':');
                    TimeSpan ts = new TimeSpan(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), 0);
                    DateTime dt = data.Date + ts;
                    string tl = AllPalestras[z].ToString();
                    pDAL.AdicionarNoEvento(dt, id, tl);
                    z++;
                }
            }

            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Evento criado com sucesso.');", true);
            Response.Redirect("PainelAdministrador.aspx");
        }
    }
}