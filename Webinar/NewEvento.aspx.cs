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
            
        }

        //protected void btnRemover_Click(object sender, EventArgs e)
        //{
        //    List<ListItem> removedItems = new List<ListItem>();
        //    foreach (ListItem item in lstLeft.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            item.Selected = false;
        //            lstRight.Items.Add(item);
        //            removedItems.Add(item);
        //        }
        //    }
        //    foreach (ListItem item in removedItems)
        //    {
        //        lstLeft.Items.Remove(item);
        //    }
        //}

        //protected void btnAdicionar_Click(object sender, EventArgs e)
        //{
        //    List<ListItem> removedItems = new List<ListItem>();
        //    foreach (ListItem item in lstRight.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            item.Selected = false;
        //            lstLeft.Items.Add(item);
        //            removedItems.Add(item);
        //        }
        //    }
        //    foreach (ListItem item in removedItems)
        //    {
        //        lstRight.Items.Remove(item);
        //    }
        //}

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
            Painel2();
        }   
            
            
        protected void Painel2()
        {
          
            Panel1.Visible = false;
            Panel2.Visible = true;

            DateTime a = Convert.ToDateTime(txtDataIniEvento.Text);
            DateTime b = Convert.ToDateTime(txtDataTerEvento.Text);
            string c = (b - a).TotalDays.ToString();

            int TotalDias = Convert.ToInt32(c) + 2;

            for (int x = 2; x != TotalDias; x++)
            {
                var lidia = new HtmlGenericControl("li");
                lidia.Attributes["class"] = "nav-item";
                lidia.Attributes["id"] = "dia" + x.ToString();

                var adia = new HtmlGenericControl("a");
                adia.Attributes["class"] = "nav-link";
                adia.Attributes["style"] = "width:240px; margin-bottom:4px;";
                adia.Attributes["href"] = "#day-" + x.ToString();
                adia.Attributes["role"] = "tab";
                adia.Attributes["data-toggle"] = "tab";
                adia.InnerText = "Dia " + x.ToString();

                lidia.Controls.Add(adia);
                ulDias.Controls.Add(lidia);

                var div1 = new HtmlGenericControl("div");
                div1.Attributes["role"] = "toppanel";
                div1.Attributes["class"] = "col-lg-9 tab-pane fade";
                div1.Attributes["id"] = "day-" + x.ToString();

                var div2 = new HtmlGenericControl("div");
                div2.Attributes["class"] = "row schedule-item";

                var div3 = new HtmlGenericControl("div");
                div3.Attributes["class"] = "col-md-2";

                var input1 = new HtmlGenericControl("input");
                input1.Attributes["runat"] = "server";
                input1.Attributes["id"] = "timeD" + x.ToString() + "P" + x.ToString();
                input1.Attributes["style"] = "background-color: transparent; border-color: transparent; color: white;";

                var div4 = new HtmlGenericControl("div");
                div4.Attributes["class"] = "col-md-10";

                var div5 = new HtmlGenericControl("div");
                div5.Attributes["class"] = "speaker";

                Image img = new Image();
                img.ID = "imgD" + x.ToString() + "P" + x.ToString();
                img.CssClass = "rounded-circle";

                var h4 = new HtmlGenericControl("h4");
                h4.Attributes["runat"] = "server";
                h4.ID = "h4D" + x.ToString() + "P" + x.ToString();

                DropDownList ddl = new DropDownList();
                ddl.Attributes["runat"] = "server";
                ddl.ID = "ddlD" + x.ToString() + "P" + x.ToString();
                ddl.DataSourceID = "DsPalestra" + x.ToString();
                ddl.AutoPostBack = true;
                ddl.DataTextField = "PalestraTitulo";
                ddl.DataValueField = "PalestraTitulo";
                ddl.AppendDataBoundItems = true;

                SqlDataSource sql = new SqlDataSource();
                sql.ID = "DsPalestra" + x.ToString();
                sql.ConnectionString = "<%$ ConnectionStrings:AggregateBD %>";
                sql.SelectCommand = "SELECT PalestraTitulo FROM Palestras ORDER BY PalestraTitulo ASC";

                div5.Controls.Add(img);
                div4.Controls.Add(div5);
                div4.Controls.Add(h4);
                div4.Controls.Add(ddl);
                div4.Controls.Add(sql);
                div3.Controls.Add(input1);

                div2.Controls.Add(div3);
                div2.Controls.Add(div4);

                div1.Controls.Add(div2);

                //TextWriter texto = new StreamWriter("D:\\Users\\Pedro\\Desktop\\texto.txt");
                //texto.WriteLine(div1);
                //texto.Close();
                
            }
        }

        protected void ddlD1P1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string ddl = ddlD1P1.SelectedValue;
            PalestraDAL pDAL = new PalestraDAL();
            DataTable dt = pDAL.AdicionarPalestraEmEvento(ddl);

            byte[] bytes1 = (byte[])dt.Rows[0]["PalestranteFoto"];
            string base64String1 = Convert.ToBase64String(bytes1, 0, bytes1.Length);
            imgD1P1.ImageUrl = "data:image/png;base64," + base64String1;
            h4D1P1.InnerText = dt.Rows[0]["Username"].ToString();
            Painel2();
        }

        protected void addPalestra_Click(object sender, EventArgs e)
        {
            Painel2();
            Painel3();
        }
        protected void Painel3()
        {
            int x = 20;
            var div1 = new HtmlGenericControl("div");
            div1.Attributes["role"] = "toppanel";
            div1.Attributes["class"] = "col-lg-9 tab-pane fade";
            div1.Attributes["id"] = "day-" + x.ToString();

            var div2 = new HtmlGenericControl("div");
            div2.Attributes["class"] = "row schedule-item";

            var div3 = new HtmlGenericControl("div");
            div3.Attributes["class"] = "col-md-2";

            var input1 = new HtmlGenericControl("input");
            input1.Attributes["runat"] = "server";
            input1.Attributes["id"] = "timeD" + x.ToString() + "P" + x.ToString();
            input1.Attributes["style"] = "background-color: transparent; border-color: transparent; color: white;";

            var div4 = new HtmlGenericControl("div");
            div4.Attributes["class"] = "col-md-10";

            var div5 = new HtmlGenericControl("div");
            div5.Attributes["class"] = "speaker";

            Image img = new Image();
            img.ID = "imgD" + x.ToString() + "P" + x.ToString();
            img.CssClass = "rounded-circle";

            var h4 = new HtmlGenericControl("h4");
            h4.Attributes["runat"] = "server";
            h4.ID = "h4D" + x.ToString() + "P" + x.ToString();

            DropDownList ddl = new DropDownList();
            ddl.Attributes["runat"] = "server";
            ddl.ID = "ddlD" + x.ToString() + "P" + x.ToString();
            ddl.DataSourceID = "DsPalestra" + x.ToString();
            ddl.AutoPostBack = true;
            ddl.DataTextField = "PalestraTitulo";
            ddl.DataValueField = "PalestraTitulo";
            ddl.AppendDataBoundItems = true;

            SqlDataSource sql = new SqlDataSource();
            sql.ID = "DsPalestra" + x.ToString();
            sql.ConnectionString = "<%$ ConnectionStrings:AggregateBD %>";
            sql.SelectCommand = "SELECT PalestraTitulo FROM Palestras ORDER BY PalestraTitulo ASC";

            div5.Controls.Add(img);
            div4.Controls.Add(div5);
            div4.Controls.Add(h4);
            div4.Controls.Add(ddl);
            div4.Controls.Add(sql);
            div3.Controls.Add(input1);

            div2.Controls.Add(div3);
            div2.Controls.Add(div4);

            div1.Controls.Add(div2);
            //divDias.Controls.Add(div1);
        }
    }
}
