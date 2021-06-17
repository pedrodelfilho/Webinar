using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webinar
{
    public partial class NewEventoPage3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var DtInicio = (DateTime)Session["DtInicio"];
            var DtTermino = (DateTime)Session["DtTermino"];
            var AllPalestras = (string[])Session["AllPalestras"];
            var AllTimes = (string[])Session["AllTimes"];
           
            var Palestras = AllPalestras.ToArray();

            lblDtInicio.Text = DtInicio.Date.ToString("dd/MM/yyyy");
            lblDtFinal.Text = DtTermino.Date.ToString("dd/MM/yyyy");
            lblQtdPalestra.Text = Palestras.Length.ToString();
        }

        protected void btnProx_Click(object sender, EventArgs e)
        {
            EventoDAL eDAL = new EventoDAL();
            Evento objEvento = new Evento();

            string cod = HttpContext.Current.User.Identity.Name;

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);

            if (inputCapaEvento.PostedFile.ContentLength > 0)
            {
                string empFilename = Path.GetFileName(inputCapaEvento.PostedFile.FileName);
                string FilecontentType = inputCapaEvento.PostedFile.ContentType;
                Stream s = inputCapaEvento.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] Databytes = br.ReadBytes((Int32)s.Length);
                objEvento.EventoCapa = Databytes;
            }
            else
            {
                lblNotificar.Text = "Defina uma figura para capa!";
                return;
            }

            var DtInicio = (DateTime)Session["DtInicio"];
            var DtTermino = (DateTime)Session["DtTermino"];            

            objEvento.IDAdm = usuario.UserId;
            objEvento.EventoTitulo = Session["TituloEvento"].ToString();
            objEvento.EventoSubTitulo = Session["SubTituloEvento"].ToString();
            objEvento.EventoSinopseP1 = Session["Sinopse1Evento"].ToString();
            objEvento.EventoSinopseP2 = Session["Sinopse2Evento"].ToString();
            objEvento.EventoDtIni = DtInicio;
            objEvento.EventoDtTer = DtTermino;
            objEvento.ModResponsavel = ddlModResp.SelectedValue;
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

        protected void btnVisu_Click(object sender, EventArgs e)
        {
            if (inputCapaEvento.PostedFile.ContentLength > 0)
            {
                string empFilename = Path.GetFileName(inputCapaEvento.PostedFile.FileName);
                string FilecontentType = inputCapaEvento.PostedFile.ContentType;
                Stream s = inputCapaEvento.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(s);
                byte[] Databytes = br.ReadBytes((Int32)s.Length);
                Session["CapaEvento"] = Databytes;
            }
            else
            {
                lblNotificar.Text = "Defina uma figura para capa!";
                return;
            }

            Session["ModEvento"] = ddlModResp.SelectedValue;

            Response.Redirect("PreviewEventos.aspx");
        }
    }
}

