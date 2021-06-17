using DAL;
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
    public partial class NewEventoPage2 : System.Web.UI.Page
    {
        Panel pnlDiv9;
        string[] ll = null;
        string[] lll = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int dias = Convert.ToInt32(Session["dias"]);
            var lista = (List<int>)Session["itens"];

            int[] listt = lista.ToArray();

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+listt[0]+" - "+listt[1]+" - "+ listt[2] + " - " + listt[3] + " - "+ listt[4] + "')", true);

            for (int x = 1; x != dias; x++)
            {
                this.AddDias(x);
                for (int y = 0; y < listt[x-1]; y++)
                {                    
                    this.AddPalestras(x, y+1);                                     
                }
            }

            SqlDataSource sql = new SqlDataSource();
            sql.ID = "DsPalestra";
            sql.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
            sql.SelectCommand = "SELECT PalestraTitulo FROM Palestras ORDER BY PalestraTitulo ASC";
            divDias.Controls.Add(sql);
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
        public void AddPalestras(int dd, int pp)
        {
            Panel pnlDiv10 = new Panel();
            pnlDiv10.CssClass = "row schedule-item";
            pnlDiv9.Controls.Add(pnlDiv10);

            Panel pnlDiv11 = new Panel();
            pnlDiv11.CssClass = "col-md-2";
            pnlDiv10.Controls.Add(pnlDiv11);

            TextBox PalestraHora = new TextBox();
            PalestraHora.Attributes.Add("type", "time");
            PalestraHora.Width = 120;
            PalestraHora.Attributes.Add("class", "form-control");
            PalestraHora.Attributes.Add("style", "background-color: transparent; border-color: transparent; color: white;");
            PalestraHora.Attributes.Add("id", "PalestraHoraDia" + dd.ToString() + "Palestra" + pp.ToString());
            pnlDiv11.Controls.Add(PalestraHora);

            Panel pnlDiv12 = new Panel();
            pnlDiv12.CssClass = "col-md-10";
            pnlDiv10.Controls.Add(pnlDiv12);

            Panel pnlDiv13 = new Panel();
            pnlDiv13.CssClass = "speaker";
            pnlDiv12.Controls.Add(pnlDiv13);

            Image img = new Image();
            img.CssClass = "rounded-circle";
            img.Attributes.Add("id", "ImgDia" + dd.ToString() + "Palestra" + pp.ToString());
            pnlDiv13.Controls.Add(img);

            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.Attributes.Add("id", "h4Dia" + dd.ToString() + "Palestra" + pp.ToString());

            DropDownList ddl = new DropDownList();
            ddl.Attributes.Add("id", "ddlDia" + dd.ToString() + "Palestra" + pp.ToString());
            ddl.Attributes.Add("class", "form-control");
            ddl.Width = 400;
            ddl.DataSourceID = "DsPalestra";
            ddl.DataTextField = "PalestraTitulo";
            ddl.DataValueField = "PalestraTitulo";
            ddl.AppendDataBoundItems = true;
            pnlDiv12.Controls.Add(ddl);
        }
        public void btnProx_Click(object sender, EventArgs e)
        {
            List<string> termsListt = new List<string>();
            List<DropDownList> lst = new List<DropDownList>();
            GetDropDownControls(GetListOfControlCollection(this.Form.Controls), ref lst);

            foreach (DropDownList item in lst)
            {
                var selectedValue = item.SelectedValue;
                termsListt.Add(selectedValue);
            }

            var dist = termsListt.Distinct().ToArray();
            ll = termsListt.ToArray();

            if (dist.Length != ll.Length)
            {                
                ltFinal.Text = "Existem palestras duplicadas!";
                ltFinal.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                ltFinal.Text = string.Empty;
                List<string> TimeList = new List<string>();
                List<TextBox> lst1 = new List<TextBox>();
                GetTextBoxControls(GetListOfControlCollection(this.Form.Controls), ref lst1);

                foreach (TextBox item in lst1)
                {
                    var TextValue = item.Text;
                    TimeList.Add(TextValue);
                }
                lll = TimeList.ToArray();

                for (int x = 0; x != ll.Length; x++)
                {
                    if (lll[x] == string.Empty)
                    {
                        ltFinal.Text = "Existem palestras sem horário definido de apresentação!";
                        ltFinal.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else { ltFinal.Text = string.Empty; }
                }
            }            
            
            Session["AllPalestras"] = ll;
            Session["AllTimes"] = lll;

            Response.Redirect("NewEventopage3.aspx");
            
        }
        void GetDropDownControls(List<Control> controls, ref List<DropDownList> lst)
        {
            foreach (Control item in controls)
            {
                if (item.Controls.Count == 0 && item is DropDownList)
                    lst.Add((DropDownList)item);
                else
                    if (item.Controls.Count > 0)
                    GetDropDownControls(GetListOfControlCollection(item.Controls), ref lst);
            }
        }
        void GetTextBoxControls(List<Control> controls, ref List<TextBox> lst1)
        {
            foreach (Control item in controls)
            {
                if (item.Controls.Count == 0 && item is TextBox)
                    lst1.Add((TextBox)item);
                else
                    if (item.Controls.Count > 0)
                    GetTextBoxControls(GetListOfControlCollection(item.Controls), ref lst1);
            }
        }
        List<Control> GetListOfControlCollection(ControlCollection controls)
        {
            List<Control> result = new List<Control>();
            foreach (Control item in controls)
            {
                result.Add(item);
            }
            return result;
        }
        
    }
}