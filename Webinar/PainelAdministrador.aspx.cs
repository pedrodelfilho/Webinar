using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Models;

namespace Webinar
{
    public partial class PainelAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) // Chamado sempre que a página "PainelAdministrador.aspx" é carregada/atualizada
        {      
            if (!IsPostBack)
            {
            }
            
        }

        protected void btnUsuarios_Click(object sender, EventArgs e) //Botão "Usuários" no Painel do Administrador
        {
            btnAdicionarEvento.Visible = false;
            btnAdicionarPalestra.Visible = false;
            PanelUsuarios.Visible = true;
            PanelEventos.Visible = false;
            PanelPalestras.Visible = false;
            PanelPendencias.Visible = false;
            PanelPaginaInicial.Visible = false;
            ViewState["sortOrder"] = "";
            BindGridViewUsuarios("", "");
        }
        protected void btnEventos_Click(object sender, EventArgs e) // Botão "Eventos" no Painel do Administrador
        {
            btnAdicionarPalestra.Visible = false;
            btnAdicionarEvento.Visible = true;
            PanelUsuarios.Visible = false;
            PanelEventos.Visible = true;
            PanelPalestras.Visible = false;
            PanelPendencias.Visible = false;
            PanelPaginaInicial.Visible = false;            
        }
        protected void btnPalestras_Click(object sender, EventArgs e) // Botão "Palestras" no Painel do Administrador
        {
            btnAdicionarEvento.Visible = false;
            btnAdicionarPalestra.Visible = true;
            PanelUsuarios.Visible = false;
            PanelEventos.Visible = false;
            PanelPalestras.Visible = true;
            PanelPendencias.Visible = false;
            PanelPaginaInicial.Visible = false;
            ViewState["sortOrder"] = "";
            BindGridViewPalestra("", "");
        }     
        protected void btnPendencias_Click(object sender, EventArgs e) // Botão "Pendências" no Painel do Administrador
        {
            btnAdicionarEvento.Visible = false;
            btnAdicionarPalestra.Visible = false;
            PanelUsuarios.Visible = false;
            PanelEventos.Visible = false;
            PanelPalestras.Visible = false;
            PanelPendencias.Visible = true;
            PanelPaginaInicial.Visible = false;
            SolicitacaoPalestrante();
            SolicitacaoPalestra();
        }
        protected void btnPaginaInicial_Click(object sender, EventArgs e) // Botão "Página Inicial" no Painel do Administrador
        {
            btnAdicionarEvento.Visible = false;
            btnAdicionarPalestra.Visible = false;
            PanelUsuarios.Visible = false;
            PanelEventos.Visible = false;
            PanelPalestras.Visible = false;
            PanelPendencias.Visible = false;
            PanelPaginaInicial.Visible = true;
            ViewState["sortOrder"] = "";
            BindGridViewPagInicial("", "");
        }
        protected void btnAdicionarPalestra_Click(object sender, EventArgs e) // Botão "Adicionar Palestra" dentro da sessão "Palestras"
        {
            Response.Redirect("NewPalestra.aspx");
        }


        public void SolicitacaoPalestrante() // Busca todos os Palestrantes com Pendências 
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            gvPalestrante.DataSource = uDAL.PalestrantePendente();
            gvPalestrante.DataBind();
            ViewState["sortOrder"] = "";
            BindGridViewPalestrantes("", "");
        }
        public void SolicitacaoPalestra() // Busca todas as Palestras com Pendências
        {
            PalestraDAL pDAL = new PalestraDAL();
            gvPalestra.DataSource = pDAL.PalestraPendente();
            gvPalestra.DataBind();
            ViewState["sortOrder"] = "";
            BindGridViewPalestras("", "");
        } 
        public string sortOrder // Funcão que ordena coluna de grid view por ordenação crescente ou decrescente 
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


        public void BindGridViewPalestrantes(string sortExp, string sortDir) // Coloca todos os Palestrantes com pendências em uma GridView
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
                lblResPalestrante.Text = "Pendencias localizadas: " + dt.Rows.Count;
                lblResPalestrante.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblResPalestrante.Text = "Não possuí Palestrantes com solicitações pendentes. Obrigado.";
                lblResPalestrante.ForeColor = System.Drawing.Color.White;
                lblResPalestrante.Font.Size = 14;
            }
        }
        public void BindGridViewPalestras(string sortExp, string sortDir) // Coloca todas as Palestras com pendências em uma GridView
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
                lblResPalestra.Text = "Pendencias localizadas: " + dt.Rows.Count;
                lblResPalestra.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblResPalestra.Text = "Não possui Palestras com solicitações pendentes. Obrigado.";
                lblResPalestra.ForeColor = System.Drawing.Color.White;
                lblResPalestra.Font.Size = 14;
            }
        }
        public void BindGridViewPalestra(string sortExp, string sortDir) // Coloca todas as Palestras em uma GridView
        {
            PalestraDAL pDAL = new PalestraDAL();
            DataTable dt = pDAL.ListarPalestras();

            foreach (DataRow row in dt.Rows)
            {
                if (row["PalestraAprovada"] is System.DBNull)
                {
                    row["PalestraAprovada"] = false;
                }
                if (row["PalestraAutoriza"] is System.DBNull)
                {
                    row["PalestraAutoriza"] = false;
                }
            }

            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dt.DefaultView;

                if (sortExp != string.Empty)
                {
                    dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                gvPalestras.DataSource = dv;
                gvPalestras.DataBind();
                lblResPalestras.Text = "Palestras localizadas: " + dt.Rows.Count;
                lblResPalestras.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblResPalestras.Text = "Nenhuma palestra encontrada.";
                lblResPalestras.ForeColor = System.Drawing.Color.White;
                lblResPalestras.Font.Size = 14;
            }
        }
        public void BindGridViewUsuarios(string sortExp, string sortDir)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            DataTable dt = uDAL.ListarUsuarios();
            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = dt.DefaultView;

                if (sortExp != string.Empty)
                {
                    dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }

                gvUsuarios.DataSource = dv;
                gvUsuarios.DataBind();
                lblRes1.Text = "Usuários localizados: " + dt.Rows.Count;
                lblRes1.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblRes1.Text = "Nenhum usuário encontrado.";
                lblRes1.ForeColor = System.Drawing.Color.White;
                lblRes1.Font.Size = 14;
            }
        } // Coloca todos os Usuários em uma GridView
        public void BindGridViewPagInicial(string sortExp, string sortDir) // Preenche as propriedades da PreviewHome.aspx com as informações correntes da Página Inicial
        {
            HomeDAL hDAL = new HomeDAL();
            Home objHome = hDAL.PreencherHome();

            txtTitulo.Text = objHome.Titulo;
            txtDestaque.Text = objHome.TituloDestaque;
            txtSubTitulo.Text = objHome.SubTitulo;
            txtLink.Text = objHome.LinkIntro;
            txtQuemSomos.Text = objHome.QuemSomos;
            txtQuando.Text = objHome.Quando;
            txtOnde.Text = objHome.Onde;
            txtPergunta1.Text = objHome.Pergunta1;
            txtResposta1.Text = objHome.Responsta1;
            txtPergunta2.Text = objHome.Pergunta2;
            txtResposta2.Text = objHome.Responsta2;
            txtPergunta3.Text = objHome.Pergunta3;
            txtResposta3.Text = objHome.Responsta3;
            txtPergunta4.Text = objHome.Pergunta4;
            txtResposta4.Text = objHome.Responsta4;
            txtPergunta5.Text = objHome.Pergunta5;
            txtResposta5.Text = objHome.Responsta5;
            txtEndereco.Text = objHome.Endereco;
            txtTelefone.Text = objHome.Telefone;
            txtEmail.Text = objHome.Email;
            txtEmailADM.Text = objHome.EmailADM;

        }


        protected void gvPalestra_Sorting(object sender, GridViewSortEventArgs e) // Chamado para ordenar dados de determinada coluna da GridView de Palestras
        {
            BindGridViewPalestras(e.SortExpression, sortOrder);
        }
        protected void gvPalestrante_Sorting(object sender, GridViewSortEventArgs e) // Chamado para ordenar dados de determinada coluna da GridView de Palestrantes com pendências
        {
            BindGridViewPalestrantes(e.SortExpression, sortOrder); 
        }
        protected void gvPalestras_Sorting(object sender, GridViewSortEventArgs e) // Chamado para ordenar dados de determinada coluna da GridView de Palestras com pendências
        {
            BindGridViewPalestra(e.SortExpression, sortOrder);
        }
        protected void gvUsuarios_Sorting(object sender, GridViewSortEventArgs e) // Chamado para ordenar dados de determinada coluna da GridView de Usuários
        {
            BindGridViewUsuarios(e.SortExpression, sortOrder);
        }
        protected void gvConEmpresarial_Sorting(object sender, GridViewSortEventArgs e) // Chamado para ordenar dados de determinada coluna da GridView de Conexões Empresariais
        {
            BindGridViewPagInicial(e.SortExpression, sortOrder);
        }


        protected void gvPalestras_RowCommand(object sender, GridViewCommandEventArgs e) // Botão "Visualizar" da GridView de Palestras, para visualizar individualmente e por completo determinada Palestra
        {
            if (e.CommandName == "SendPalestras") 
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["identificador"] = "PalestrasTotais";
                Session["IDPalestra"] = id;
                Response.Redirect("PreviewPalestra.aspx");
            }            
            if (e.CommandName == "SendDelPalestras")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                PalestraDAL pDAL = new PalestraDAL();
                pDAL.ExcluirPalestra(id);
            }
        }

        public void gvPalestra_RowCommand(object sender, GridViewCommandEventArgs e) // Botão "Visualizar" da GridView de Palestras com Pendências, para visualizar individualmente e por completo determinada Palestra
        {
            if (e.CommandName != "SendPalestra") return;            
            int id = Convert.ToInt32(e.CommandArgument);
            Session["identificador"] = "PalestrasPendente";
            Session["id"] = id;
            Response.Redirect("PreviewPalestra.aspx");
                

        }
        public void gvPalestrante_RowCommand(object sender, GridViewCommandEventArgs e) // Botão "Visualizar" da GridView de Palestrantes com Pendências, para visualizar individualmente e por completo o perfil de determinado Palestrante
        {
            if (e.CommandName != "SendPalestrantes") return;
            int id = Convert.ToInt32(e.CommandArgument);
            Session["id"] = id;
            Response.Redirect("PreviewPalestrante.aspx");
        }
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e) // Botão "Visualizar" da GridView de Usuários, para visualizar individualmente e por completo o perfil de determinado Usuário
        {
            if (e.CommandName != "SendUsuarios") return;
            int id = Convert.ToInt32(e.CommandArgument);
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarEmail(id);
                       
            Session["Tipo"] = usuario.Tipo;
            Session["id"] = id;
            Response.Redirect("PreviewUsuarios.aspx");
        }      
        protected void gvConEmpresarial_RowCommand(object sender, GridViewCommandEventArgs e) // Botão "Deletar" da GridView de Conexão Empresarial, para deletar individualmente determinada Conexão Empresarial
        {
            if (e.CommandName != "SendConEmpresarial") return;
            int id = Convert.ToInt32(e.CommandArgument);
            ConexaoDAL cDAL = new ConexaoDAL();
            cDAL.RemoverConexao(id);
        }


        protected void btnVisualizarPagInicial_Click(object sender, EventArgs e) // Botão "Visualizar Alterações", monta a "PreviewHome.aspx" para visualização com as informações recém inseridas
        {
            Session["titulo"] = txtTitulo.Text;
            Session["destaque"] = txtDestaque.Text;
            Session["subtitulo"] = txtSubTitulo.Text;
            Session["link"] = txtLink.Text;
            Session["quemsomos"] = txtQuemSomos.Text;
            Session["quando"] = txtQuando.Text;
            Session["onde"] = txtOnde.Text;
            Session["p1"] = txtPergunta1.Text;
            Session["r1"] = txtResposta1.Text;
            Session["p2"] = txtPergunta2.Text;
            Session["r2"] = txtResposta2.Text;
            Session["p3"] = txtPergunta3.Text;
            Session["r3"] = txtResposta3.Text;
            Session["p4"] = txtPergunta4.Text;
            Session["r4"] = txtResposta4.Text;
            Session["p5"] = txtPergunta5.Text;
            Session["r5"] = txtResposta5.Text;
            Session["endereco"] = txtEndereco.Text;
            Session["telefone"] = txtTelefone.Text;
            Session["email"] = txtEmail.Text;
            Session["emailadm"] = txtEmailADM.Text;

            Response.Redirect("PreviewHome.aspx");
        }
        protected void btnAplicarPagInicial_Click(object sender, EventArgs e) // Botão "Aplicar", salva no banco de dados e aplica as alterações na página inicial
        {
            string cod = HttpContext.Current.User.Identity.Name;
            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.BuscarID(cod);

            HomeDAL hDAL = new HomeDAL();
            Home objHome = new Home();

            objHome.UserId = usuario.UserId;
            objHome.TituloDestaque = txtDestaque.Text;
            objHome.Titulo = txtTitulo.Text;
            objHome.TituloDestaque = txtDestaque.Text;
            objHome.SubTitulo = txtSubTitulo.Text;
            objHome.LinkIntro = txtLink.Text;
            objHome.QuemSomos = txtQuemSomos.Text;
            objHome.Quando = txtQuando.Text;
            objHome.Onde = txtOnde.Text;
            objHome.Pergunta1 = txtPergunta1.Text;
            objHome.Responsta1 = txtResposta1.Text;
            objHome.Pergunta2 = txtPergunta2.Text;
            objHome.Responsta2 = txtResposta2.Text;
            objHome.Pergunta3 = txtPergunta3.Text;
            objHome.Responsta3 = txtResposta3.Text;
            objHome.Pergunta4 = txtPergunta4.Text;
            objHome.Responsta4 = txtResposta4.Text;
            objHome.Pergunta5 = txtPergunta5.Text;
            objHome.Responsta5 = txtResposta5.Text;
            objHome.Endereco = txtEndereco.Text;
            objHome.Telefone = txtTelefone.Text;
            objHome.Email = txtEmail.Text;
            objHome.EmailADM = txtEmailADM.Text;

            hDAL.AplicarHome(objHome);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Página inicial atualizada com sucesso.');", true);
            Response.Redirect("PainelAdministrador.aspx");
        }
    }
}