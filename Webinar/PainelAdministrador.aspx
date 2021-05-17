<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PainelAdministrador.aspx.cs" Inherits="Webinar.PainelAdministrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="intro">
    <div class="intro-container wow fadeIn">
        <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgPerfil" CssClass="rounded-circle mt-5" width="90" />  
                        <asp:FileUpload ID="FileUploadEmployees" runat="server" Font-Size="Smaller" CssClass="btn" />                                              
                        <asp:Label runat="server" ID="lblNome" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblEmail" style="color: white" />
                        <asp:Label runat="server" ID="lblCidade" style="color: white" />
                    </div>
                </div>
                <div class="col-md-7">
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
                                <label style="color: white" class="labels">Biografia</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtBio" Width="100%" MaxLength="600" placeholder="Digite algo sobre você"></asp:TextBox>                                
                            </div> 
                        </div>
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="cbReceberEmail" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." />
                            </div>                            
                        </div>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="btnSalvarPerfil" OnClick="btnSalvarPerfil_Click" runat="server" CssClass="btn btn-danger"><i class="fa fa-save"></i>&nbsp;Salvar</asp:LinkButton>                
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>
</div>
</asp:Content>
