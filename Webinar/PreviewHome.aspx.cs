using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DAL;
using System.Web.UI.HtmlControls;

namespace Webinar
{
    public partial class PreviewHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarHome();
            }
        }

        protected void CarregarHome()
        {

            string titulo = Session["titulo"].ToString();
            string destaque = Session["destaque"].ToString();

            int total = titulo.Length;
            int alvo = destaque.Length;
            int primeiro = titulo.LastIndexOf(destaque);
            int segundo1 = alvo + primeiro;
            int segundo2 = total - segundo1;

            string titulo1 = titulo.Substring(0, primeiro);
            string titulo2 = titulo.Substring(segundo1, segundo2);

            var i = new HtmlGenericControl("i");
            i.Attributes["class"] = "fa fa-minus-circle";

            var span = new HtmlGenericControl("span");
            span.InnerHtml = "<br />" + destaque;
            span.Attributes["class"] = "nonExpense";

            var span1 = new HtmlGenericControl("span");
            span1.InnerHtml = titulo2;
            span1.Attributes["class"] = "nonExpense";
            span1.Attributes["style"] = "color:white";

            lblTitulo.InnerText = titulo1;
            lblTitulo.Controls.Add(span);
            lblTitulo.Controls.Add(span1);
            lblSubTitulo.InnerText = Session["subtitulo"].ToString();
            lblLink.Attributes["href"] = Session["link"].ToString();
            lblQuemSomos.InnerText = Session["quemsomos"].ToString();
            lblQuando.InnerText = Session["quando"].ToString();
            lblOnde.InnerText = Session["onde"].ToString();
            lblPergunta1.InnerHtml = Session["p1"].ToString();
            lblPergunta1.Controls.Add(i);
            lblResposta1.InnerText = Session["r1"].ToString();
            lblPergunta2.InnerHtml = Session["p2"].ToString();
            lblPergunta2.Controls.Add(i);
            lblResposta2.InnerText = Session["r2"].ToString();
            lblPergunta3.InnerHtml = Session["p3"].ToString();
            lblPergunta3.Controls.Add(i);
            lblResposta3.InnerText = Session["r3"].ToString();
            lblPergunta4.InnerHtml = Session["p4"].ToString();
            lblPergunta4.Controls.Add(i);
            lblResposta4.InnerText = Session["r4"].ToString();
            lblPergunta5.InnerHtml = Session["p5"].ToString();
            lblPergunta5.Controls.Add(i);
            lblResposta5.InnerText = Session["r5"].ToString();
            lblEndereco.InnerText = Session["endereco"].ToString();
            lblTelefone.InnerText = Session["telefone"].ToString();
            lblTelefone.Attributes["href"] = "tel:+" + Session["telefone"].ToString();
            lblEmail.InnerText = Session["email"].ToString();
            lblEmail.Attributes["href"] = "mailto:" + Session["email"].ToString();
            btnEnviarMensagemContato.Enabled = true;
            btnEnviarNotificacao.Enabled = true;
        }
    }
}