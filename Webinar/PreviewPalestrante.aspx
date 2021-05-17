<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PreviewPalestrante.aspx.cs" Inherits="Webinar.PreviewPalestrante" %>
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
     <div style="background: rgba(6, 12, 34, 0.8);" id="intro-container"><br /><br />
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
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Nome Completo</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblNome1"/>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label style="color: red" class="labels">Data Nascimento</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblDtNasc"/>
                            </div>
                            <div class="col-md-6">
                                <label style="color: red" class="labels">Cidade - UF</label><br />    
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblCidade1"/>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-4">
                                <label style="color: red" class="labels">Sexo</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblSexo"/>
                            </div>
                            <div class="col-md-8">
                                <label style="color: red" class="labels">Nível de Escolaridade</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblEscolaridade"/>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Especialidade Educacional e/ou Pessoal</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblEspecialidade"/>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Biografia 1º Parágrafo</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblBio1"/>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Biografia 2º Parágrafo</label><br />
                                <asp:Label runat="server" style="color: white; font-size: 18px" ID="lblBio2"/>
                            </div>                            
                        </div>                            
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" Enabled="false" ID="cbReceberEmail" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." /><br />
                                <asp:CheckBox runat="server" Enabled="false" ID="cbAutorizarPerfil" style="color: white" Text="Autorizo a exibição do meu perfil ao público." />
                            </div>                            
                        </div>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="btnAutorizarPerfil" OnClick="btnAutorizarPerfil_Click" runat="server" Width="170" CssClass="botao btn-success"><i class="fa fa-thumbs-o-up"></i>&nbsp;Autorizar</asp:LinkButton>
                            <asp:LinkButton ID="btnNegarPerfil" OnClick="btnNegarPerfil_Click" runat="server" Width="150" CssClass="botao btn-danger"><i class="fa fa-thumbs-o-down"></i>&nbsp;Negar</asp:LinkButton> 
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
                                <asp:HyperLink runat="server" style="color: white; font-size: 18px" ID="linkTwiter"></asp:HyperLink><br />
                                <span class="d-block text-white-50 labels">Twitter Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/100/000000/facebook.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:HyperLink runat="server" style="color: white; font-size: 18px" ID="linkFacebook"></asp:HyperLink><br />
                                <span class="d-block text-white-50 labels">Facebook Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/google-plus--v1.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:HyperLink runat="server" style="color: white; font-size: 18px" ID="linkGoogle"></asp:HyperLink><br />
                                <span class="d-block text-white-50 labels">Google Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/linkedin.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:HyperLink runat="server" style="color: white; font-size: 18px" ID="linkLinkedin"></asp:HyperLink><br />
                                <span class="d-block text-white-50 labels">Linkedin Inc.</span>
                            </div>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </asp:Panel>   
    <br />
    </div>
</asp:Content>
