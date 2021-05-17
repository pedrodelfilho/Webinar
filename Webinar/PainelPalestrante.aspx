<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PainelPalestrante.aspx.cs" Inherits="Webinar.PainelPalestrante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.parallax {
  background-repeat: no-repeat;
  background-attachment: scroll;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: rgba(6, 12, 34, 0.8);" id="intro-container"><br />
    <asp:Panel ID="PanelPerfil" runat="server">
        <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgUsuario" CssClass="rounded-circle mt-5" width="150" />  
                        <asp:FileUpload ID="fuUsuario" runat="server" Width="120px" Font-Size="X-Small" CssClass="btn" />   
                        <asp:Label runat="server" ID="lblNome" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblEmail" style="color: white" />
                        <asp:Label runat="server" ID="lblCidade" style="color: white" /><br /><br /><br /><br /><br />
                        <asp:Button runat="server" ID="btnAddPalestra" OnClick="btnAddPalestra_Click" Text="Adicionar nova Palestra" CssClass="btn btn-danger" />
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-pencil"></span> Editar Perfil</h4>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Nome Completo</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNome" placeholder="Digite o nome" />
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Data Nascimento</label>
                                <asp:TextBox runat="server" Width="100%" class="form-control" TextMode="Date" ID="txtDtNasc" placeholder="01/01/2000"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Cidade - UF</label>
                                <asp:DropDownList ID="ddlCidade" runat="server" Width="100%" CssClass="form-control" DataSourceID="DsCidade"  DataTextField="NomeCidade" DataValueField="NomeCidade" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="DsCidade" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT NomeCidade FROM Cidades ORDER BY NomeCidade ASC"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-4">
                                <label style="color: white" class="labels">Sexo</label>
                                <asp:DropDownList runat="server" Width="100%" CssClass="form-control" ID="ddlSexo">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                    <asp:ListItem Value="Feminino">Feminino</asp:ListItem>
                                    <asp:ListItem Value="Outros">Outros</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-8">
                                <label style="color: white" class="labels">Nível de Escolaridade</label>
                                <asp:DropDownList ID="ddlEscolaridade" runat="server" Width="100%" CssClass="form-control" DataSourceID="DescEscolaridade"  DataTextField="DsEscolaridade" DataValueField="DsEscolaridade" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="DescEscolaridade" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT DsEscolaridade FROM NvEscolaridade"></asp:SqlDataSource>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Especialidade Educacional e/ou Pessoal</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEspecialidade" placeholder="Digite a especialidade" />
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Biografia 1º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtBio1" Width="100%" MaxLength="600" placeholder="Digite algo sobre você" Rows="3"></asp:TextBox>                                
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Biografia 2º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="txtBios2" Width="100%" MaxLength="600" placeholder="Digite algo sobre você" Rows="3"></asp:TextBox>                                
                            </div>
                        </div>                            
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="cbReceberEmail" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." />
                            </div>                            
                        </div>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="btnSalvarPerfil" runat="server" CssClass="btn btn-danger"><i class="fa fa-save"></i>&nbsp;Salvar</asp:LinkButton>                
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center experience">
                            <h4 style="color:red;"><span class="fa fa-pencil"></span>Editar Rede Social</h4>
                        </div>
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:ImageButton ID="btnTwiter" runat="server" ImageUrl="https://i.imgur.com/azSfBM3.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" Enabled="false" BorderColor="Transparent" BackColor="Transparent" Width="300px" class="font-weight-bold d-block text-white" Text="https://twitter.com/"></asp:TextBox><br />
                                <asp:button runat="server" ID="btnEditTwt" Font-Size="X-Small" Text="Adicionar" Width="90" />
                                <span class="d-block text-white-50 labels">Twitter Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="https://img.icons8.com/color/100/000000/facebook.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" Enabled="false" BorderColor="Transparent" BackColor="Transparent" Width="300px" class="font-weight-bold d-block text-white" Text="https://pt-br.facebook.com/"></asp:TextBox><br />
                                <asp:button runat="server" ID="Button1" Font-Size="X-Small" Text="Adicionar" Width="90" />
                                <span class="d-block text-white-50 labels">Facebook Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="https://img.icons8.com/color/48/000000/google-plus--v1.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" Enabled="false" BorderColor="Transparent" BackColor="Transparent" Width="300px" class="font-weight-bold d-block text-white" Text="https://myaccount.google.com"></asp:TextBox><br />
                                <asp:button runat="server" ID="Button2" Font-Size="X-Small" Text="Adicionar" Width="90" />
                                <span class="d-block text-white-50 labels">Google Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="https://img.icons8.com/color/48/000000/linkedin.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" Enabled="false" BorderColor="Transparent" BackColor="Transparent" Width="300px" class="font-weight-bold d-block text-white" Text="https://br.linkedin.com/"></asp:TextBox><br />
                                <asp:button runat="server" ID="Button3" Font-Size="X-Small" Text="Adicionar" Width="90" />
                                <span class="d-block text-white-50 labels">Linkedin Inc.</span>
                            </div>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelNewPalestra" Visible="false" runat="server">    
        <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="Image1" CssClass="rounded-circle mt-5" width="150" />
                        <asp:Label runat="server" ForeColor="White" Font-Size="14" Text="Foto de Capa"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="150px" />
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-video-camera"></span> Adicionar Palestra</h4>
                        </div>
                        <asp:Label runat="server" Font-Size="8" style="color:red;">Visando a garantia de privacidade dos dados e buscando entregar a melhor interatividade ao usuário, toda e qualquer criação e alteração será colocada em espera de aprovação por algum Moderador ou Administrador do sistema.</asp:Label>                        <br />
                        <asp:Label runat="server" Font-Size="8" style="color:yellow">Caro Palestrante, antes de iniciar a criação de uma Palestra, tenha todos os campos do seu perfil preenchido, inclusive a foto.</asp:Label>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Título</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox1" placeholder="Digite o título" />
                            </div>
                        </div>        
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Subtítulo</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" placeholder="Digite o subtítulo" />
                            </div>
                        </div>     
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Link</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox7" placeholder="Digite o link" />
                            </div>
                        </div>   
                        <div class="row mt-2">                            
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 1º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="TextBox4" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>                                
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 2º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="TextBox5" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>                                
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 3º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="TextBox3" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>                                
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 4º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="TextBox6" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>                                
                            </div>
                            <div class="col-md-4">
                                <label style="color: white" class="labels">Tempo estimado da palestra</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="Time" ID="TextBox8" Width="100%" MaxLength="600" placeholder=""></asp:TextBox>                                
                            </div>
                        </div>                        
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="CheckBox1" style="color: white" Text="Altorizo a publicação deste conteúdo na página inicial" />
                            </div>                            
                        </div>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="LinkButton1" runat="server" Width="200" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Salvar</asp:LinkButton><br />
                            <asp:Label Font-Size="X-Small" runat="server" ForeColor="Red">Antes de salvar, visualize a criação dá página no botão "Visualizar"</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3 py-5">
                        <br /><br /><br /><br /><br /><br /><br /><br />
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:LinkButton ID="btnVoltarPerfil" runat="server" Width="200" CssClass="btn btn-danger" OnClick="btnVoltarPerfil_Click" ><i class="fa fa-reply"></i>&nbsp;Voltar ao Perfil</asp:LinkButton>
                        </div>
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:LinkButton ID="LinkButton3" runat="server" Width="200" CssClass="btn btn-danger"><i class="fa fa-eye"></i>&nbsp;Visualizar</asp:LinkButton>
                        </div>
                            <asp:Label Font-Size="X-Small" runat="server" ForeColor="Red">Visualizar design real da página. Essa opção <br />não salvará a criação, apenas demonstrará <br />o resultado final.</asp:Label><br />  
                        </div>
                    </div>
                </div>                
            </div>
    </asp:Panel>
        <br />
    </div>
</asp:Content>
