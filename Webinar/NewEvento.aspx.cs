using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class NewEvento : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("txtDynamic")).ToList();
            int i = 1;
            foreach (string key in keys)
            {
                this.CreateTextBox("txtDynamic" + i);
                i++;
            }
        }
        protected void btnNextPanel1_Click(object sender, EventArgs e)
        {
            if (txtTituloEvento.Text == string.Empty)
            {
                txtTituloEvento.BorderColor = System.Drawing.Color.Red;
                txtTituloEvento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtTituloEvento.BorderColor = System.Drawing.Color.Empty; }
            if (txtSubTituloEvento.Text == string.Empty)
            {
                txtSubTituloEvento.BorderColor = System.Drawing.Color.Red;
                txtSubTituloEvento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtSubTituloEvento.BorderColor = System.Drawing.Color.Empty; }
            if (txtSinopseP1Evento.Text == string.Empty)
            {
                txtSinopseP1Evento.BorderColor = System.Drawing.Color.Red;
                txtSinopseP1Evento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtSinopseP1Evento.BorderColor = System.Drawing.Color.Empty; }
            if (txtSinopseP2Evento.Text == string.Empty)
            {
                txtSinopseP2Evento.BorderColor = System.Drawing.Color.Red;
                txtSinopseP2Evento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtSinopseP2Evento.BorderColor = System.Drawing.Color.Empty; }
            if (txtDataIniEvento.Text == string.Empty)
            {
                txtDataIniEvento.BorderColor = System.Drawing.Color.Red;
                txtDataIniEvento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtDataIniEvento.BorderColor = System.Drawing.Color.Empty; }
            if (txtDataTerEvento.Text == string.Empty)
            {
                txtDataTerEvento.BorderColor = System.Drawing.Color.Red;
                txtDataTerEvento.Attributes.Add("placeholder", "Campo Obrigatório");
                return;
            }
            else { txtDataTerEvento.BorderColor = System.Drawing.Color.Empty; }

            DateTime a = Convert.ToDateTime(txtDataIniEvento.Text);
            DateTime b = Convert.ToDateTime(txtDataTerEvento.Text);
            Session["DtInicio"] = a;
            Session["DtTermino"] = b;
            Session["TituloEvento"] = txtTituloEvento.Text;
            Session["SubTituloEvento"] = txtSubTituloEvento.Text;
            Session["Sinopse1Evento"] = txtSinopseP1Evento.Text;
            Session["Sinopse2Evento"] = txtSinopseP2Evento.Text;
 
            string c = (b - a).TotalDays.ToString();

            int TotalDias = Convert.ToInt32(c) + 2;
            Session["dias"] = TotalDias;

            for (int x = 1; x != TotalDias; x++)
            {
                this.CreateTextBox("txtDynamic" + x);
            }

            btnProximo.Visible = true;
            btnNextPanel1.Visible = false;
        }

        public void CreateTextBox(string x) 
        {
            string s = x.Substring(x.Length - 1, 1);

            Panel pn = new Panel();
            pn.CssClass = "row mt-1";
            addaq.Controls.Add(pn);

            Panel pnl = new Panel();
            pnl.Attributes.Add("class", "col-md-4");
            addaq.Controls.Add(pnl);

            HtmlGenericControl lb = new HtmlGenericControl("label");
            lb.Attributes.Add("class", "labels");
            lb.Attributes.Add("style", "color: white");
            lb.InnerText = "Quantidade de palestras no Dia " + s;
            addaq.Controls.Add(lb);

            TextBox txt = new TextBox();
            txt.Attributes.Add("class", "form-control");
            txt.ID = x;
            txt.Attributes.Add("type", "number");
            txt.Width = 100;
            addaq.Controls.Add(txt);

            Literal lt = new Literal();
            lt.Text = "<br />";
            addaq.Controls.Add(lt);
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            DateTime a = Convert.ToDateTime(txtDataIniEvento.Text);
            DateTime b = Convert.ToDateTime(txtDataTerEvento.Text);
            string c = (b - a).TotalDays.ToString();

            int TotalDias = Convert.ToInt32(c) + 2;

            List<int> termsList = new List<int>();
            foreach (TextBox textBox in addaq.Controls.OfType<TextBox>())
            {
                if (textBox.Text == string.Empty)
                {
                    textBox.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                else { textBox.BorderColor = System.Drawing.Color.Empty; }

                termsList.Add(Convert.ToInt32(textBox.Text));
            }            

            Session["itens"] = termsList;

            Response.Redirect("NewEventoPage2.aspx");
        }
    }
}
