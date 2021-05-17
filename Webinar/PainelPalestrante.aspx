<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PainelPalestrante.aspx.cs" Inherits="Webinar.PainelPalestrante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .botao {
        background: #ef4136;
        border: 0;
        padding: 10px 40px;
        color: #fff;
        transition: all ease-in-out 0.3s;
        border-radius: 50px;
        cursor: pointer;
    }
    .botaosub {
          color: #fff;
          background: transparent;
          padding: 10px 40px;
          border-radius: 50px;
          border: 2px solid #ef4136;
          transition: all ease-in-out 0.3s;
          font-weight: 500;
          margin-left: 8px;
          margin-top: 2px;
          line-height: 1;
          font-size: 14px;
          cursor: pointer;
        }
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: rgba(6, 12, 34, 0.8);" id="intro-container"><br /><br /><br />
    <asp:Panel ID="PanelPerfil" runat="server">
        <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgPalestrante" CssClass="rounded-circle mt-5" width="250" /> 
                        <asp:Label runat="server" ID="lblNome" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblEmail" style="color: white" />
                        <asp:Label runat="server" ID="lblCidade" style="color: white" />
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
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNome" MaxLength="40" placeholder="Digite o nome" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="Dynamic" ForeColor="Red" ControlToValidate="txtNome" ValidationExpression="^[\s\S]{0,40}$" ErrorMessage="Limite de 40 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Data Nascimento</label>
                                <asp:TextBox runat="server" Width="100%" class="form-control" TextMode="Date" ID="txtDtNasc"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtNasc" ErrorMessage="Data de Nascimento é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Cidade - UF</label>
                                <asp:DropDownList ID="ddlCidade" runat="server" Width="100%" CssClass="form-control" DataSourceID="DsCidade"  DataTextField="NomeCidade" DataValueField="NomeCidade" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="DsCidade" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT NomeCidade FROM Cidades ORDER BY NomeCidade ASC"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCidade" ErrorMessage="Cidade-UF é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSexo" ErrorMessage="Sexo é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-8">
                                <label style="color: white" class="labels">Nível de Escolaridade</label>
                                <asp:DropDownList ID="ddlEscolaridade" runat="server" Width="100%" CssClass="form-control" DataSourceID="DescEscolaridade"  DataTextField="DsEscolaridade" DataValueField="DsEscolaridade" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="DescEscolaridade" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT DsEscolaridade FROM NvEscolaridade"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlEscolaridade" ErrorMessage="Escolaridade é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Especialidade Educacional e/ou Pessoal</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEspecialidade" MaxLength="100" placeholder="Digite a especialidade" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEspecialidade" ErrorMessage="Especialidade é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtEspecialidade" ForeColor="Red" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Limite de 100 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Biografia 1º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtBio1" Width="100%" MaxLength="600" placeholder="Digite algo sobre você" Rows="3"></asp:TextBox>  
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBio1" ErrorMessage="Biografia 1 é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBio1" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Biografia 2º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="txtBio2" Width="100%" MaxLength="600" placeholder="Digite algo sobre você" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBio2" ErrorMessage="Biografia 2 é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBio2" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>                            
                        </div>                            
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="cbReceberEmail" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." /><br />
                                <asp:CheckBox runat="server" ID="cbAutorizarPerfil" style="color: white" Text="Autorizo a exibição do meu perfil ao público."/><asp:Label ID="lblcb" runat="server" ForeColor="Red" Text="&nbsp;Precisamos de sua autorização" Font-Size="16px" Visible="false"></asp:Label>
                            </div>                            
                        </div>
                            <label style="color: white; font-size:medium;" class="labels">Alterar foto de perfil:</label>
                            <asp:FileUpload ID="fuPalestrante" runat="server" style="color: white; font-size:small" Width="122px"/><asp:Label ID="lblFoto" runat="server" ForeColor="Red" Text="&nbsp;Foto é obrigatório" Font-Size="16px" Visible="false"></asp:Label>                            
                            <asp:Label runat="server" ID="LabelFuPalestrante"></asp:Label>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="btnSalvarPerfil" OnClick="btnSalvarPerfil_Click" runat="server" CssClass="botaosub"><i class="fa fa-save"></i>&nbsp;Salvar</asp:LinkButton>                
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center experience">
                            <h4 style="color:red;">&nbsp;</h4>
                        </div><br />
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://i.imgur.com/azSfBM3.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" ID="txtTwiter" BorderColor="Transparent" BackColor="Transparent" MaxLength="200" Width="300px" class="font-weight-bold d-block text-white" placeholder="https://twitter.com/"></asp:TextBox><br />
                                <span class="d-block text-white-50 labels">Twitter Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/100/000000/facebook.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" ID="txtFacebook" BorderColor="Transparent" BackColor="Transparent" MaxLength="200" Width="300px" class="font-weight-bold d-block text-white" placeholder="https://pt-br.facebook.com/"></asp:TextBox><br />
                                <span class="d-block text-white-50 labels">Facebook Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/google-plus--v1.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" ID="txtGoogle" BorderColor="Transparent" BackColor="Transparent" MaxLength="200" Width="300px" class="font-weight-bold d-block text-white" placeholder="https://myaccount.google.com"></asp:TextBox><br />
                                <span class="d-block text-white-50 labels">Google Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/linkedin.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:TextBox runat="server" ID="txtLinkedin" BorderColor="Transparent" BackColor="Transparent" MaxLength="200" Width="300px" class="font-weight-bold d-block text-white" placeholder="https://br.linkedin.com/"></asp:TextBox><br />
                                <span class="d-block text-white-50 labels">Linkedin Inc.</span>
                            </div>
                        </div><br /><br />
                        <asp:Button runat="server" ID="btnAddPalestra" OnClick="btnAddPalestra_Click" Text="Adicionar nova Palestra" CssClass="botao" />
                    </div>
                </div>                
            </div>
        </div>
    </asp:Panel>   
        <br />
    </div>
    <script>
    $("#<%=fuPalestrante.ClientID%>").on('change', function () {
        if (this.files[0].type.indexOf("image") > -1) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#<%=imgPalestrante.ClientID%>').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
        else {                
            $('#<%=imgPalestrante.ClientID%>').attr('src', '');
            alert('Não é uma imagem válida')
        }
    });
    </script>    
</asp:Content>
