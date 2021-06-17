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
    public partial class GaleriaAcervo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarAcervo();
        }

        protected void CarregarAcervo()
        {
            PalestraDAL pDAL = new PalestraDAL();
            DataTable dt = pDAL.ListarPalestrasHome();

            foreach (DataRow dr in dt.Rows)
            {
                string titulo = dr["PalestraTitulo"].ToString();
                byte[] bytes = (byte[])dr["PalestraCapa"];
                string image = Convert.ToBase64String(bytes, 0, bytes.Length);
                int id = Convert.ToInt32(dr["IDPalestra"]);
                PalestraAcervo(image, id, titulo);
            }
        }
        public void PalestraAcervo(string image, int id, string titulo)
        {
            Panel div1 = new Panel();
            div1.Attributes.Add("class", "col col-lg-3 mx-4");
            Panel1.Controls.Add(div1);

            Panel div2 = new Panel();
            div2.Attributes.Add("class", "venue-gallery");
            div1.Controls.Add(div2);

            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes.Add("data-gall", "venue-gallery");
            a.Attributes.Add("href", "PreviewPalestraON.aspx?a=" + id);
            div2.Controls.Add(a);

            HtmlGenericControl h = new HtmlGenericControl("h2");
            h.InnerText = titulo;
            h.Attributes.Add("style", "font-size: 16px; color: White;");
            a.Controls.Add(h);

            HtmlGenericControl img = new HtmlGenericControl("img");
            img.Attributes.Add("src", "data:image/png;base64," + image);
            //img.Attributes.Add("style", "max-width: 400px; max-height: 210px;");
            img.Attributes.Add("class", "img-fluid");
            a.Controls.Add(img);

            Literal lt = new Literal();
            lt.Text = "<br /><br />";
            div1.Controls.Add(lt);
        }
    }
}