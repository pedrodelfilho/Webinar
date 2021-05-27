<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PreviewUsuarios.aspx.cs" Inherits="Webinar.PreviewUsuarios" %>
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
    <div style="background: rgba(25, 31, 32, 0.8);" id="intro-container"><br /><br /><br />
    <asp:Panel ID="PanelPalestrante" Visible="false" runat="server">
        <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgPalestrante" CssClass="rounded-circle mt-5" width="250" /> 
                        <asp:Label runat="server" ID="lblNomePalestrante" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblTipoPalestrante" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblEmailPalestrante" style="color: white" />
                        <asp:Label runat="server" ID="lblCidadePalestrante" style="color: white" /><br />
                        <h7 style="color:red;"><span class="fa fa-exchange"></span> Alterar tipo da conta:</h7>
                        <asp:LinkButton runat="server" ID="TornarConvidadoPalestrante" OnClick="TornarConvidadoPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Convidado?');" Width="150px" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Convidado</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="TornarModeradorPalestrante" OnClick="TornarModeradorPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Moderador?');" Width="150px" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Moderador</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="TornarAdministradorPalestrante" OnClick="TornarAdministradorPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Administrador?');" Width="150px" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Administrador</asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-user-o"></span> Palestrante</h4>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Nome Completo</label><br />
                                <asp:Label runat="server" ID="lblNome2Palestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <label style="color: red" class="labels">Data Nascimento</label><br />
                                <asp:Label runat="server" ID="lblDtNascPalestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <label style="color: red" class="labels">Cidade - UF</label><br />
                                <asp:Label runat="server" ID="lblCidade2Palestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-4">
                                <label style="color: red" class="labels">Sexo</label><br />
                                <asp:Label runat="server" ID="lblSexoPalestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <label style="color: red" class="labels">Nível de Escolaridade</label><br />
                                <asp:Label runat="server" ID="lblEscolaridadePalestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Especialidade Educacional e/ou Pessoal</label><br />
                                <asp:Label runat="server" ID="lblEspecialidadePalestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Biografia 1º Parágrafo</label><br />
                               <asp:Label runat="server" ID="lblBio1Palestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Biografia 2º Parágrafo</label><br />
                                <asp:Label runat="server" ID="lblBio2Palestrante" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>                            
                        </div>                            
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" Enabled="false" ID="cbReceberEmailPalestrante" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." /><br />
                                <asp:CheckBox runat="server" Enabled="false" ID="cbAutorizarPerfilPalestrante" style="color: white" Text="Autorizo a exibição do meu perfil ao público." />
                            </div>                            
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
                                <asp:Label runat="server" ID="lblTwiterPalestrante" style="color: white; font-size:16px" class="font-weight-bold d-block text-white"></asp:Label><br />
                                <span class="d-block text-white-50 labels">Twitter Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/100/000000/facebook.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:Label runat="server" ID="lblFacebookPalestrante" style="color: white; font-size:16px" class="font-weight-bold d-block text-white"></asp:Label><br />
                                <span class="d-block text-white-50 labels">Facebook Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/google-plus--v1.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:Label runat="server" ID="lblGooglePalestrante" style="color: white; font-size:16px" class="font-weight-bold d-block text-white"></asp:Label><br />
                                <span class="d-block text-white-50 labels">Google Inc.</span>
                            </div>
                        </div>
                        <hr>
                        <div class="d-flex flex-row mt-3 exp-container"><img src="https://img.icons8.com/color/48/000000/linkedin.png" width="45" height="45" />
                            <div class="work-experience ml-1">
                                <asp:Label runat="server" ID="lblLinkedinPalestrante" style="color: white; font-size:16px" class="font-weight-bold d-block text-white"></asp:Label><br />
                                <span class="d-block text-white-50 labels">Linkedin Inc.</span>
                            </div>
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelUsuario" Visible="false" runat="server">
         <div class="mt-5 mb-5">
            <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgUsuario" CssClass="rounded-circle mt-5" width="150" />
                        <asp:Label runat="server" ID="lblNomeUsuario" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblTipoUsuario" style="color: white" class="font-weight-bold" />
                        <asp:Label runat="server" ID="lblEmailUsuario" style="color: white" />
                        <asp:Label runat="server" ID="lblCidadeUsuario" style="color: white" /><br />
                        <h7 ID="lblconta" style="color:red;"><span class="fa fa-exchange"></span> Alterar tipo da conta:</h7>
                        <asp:LinkButton ID="TornarConvidadoUsuario" Width="150px" runat="server" OnClick="TornarConvidadoPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Convidado?');" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Convidado</asp:LinkButton>
                        <asp:LinkButton ID="TornarPalestranteUsuario" Width="150px" runat="server" OnClick="TornarPalestranteUsuario_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Palestrante?');" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Palestrante</asp:LinkButton>
                        <asp:LinkButton ID="TornarModeradorUsuario" Width="150px" runat="server" OnClick="TornarModeradorPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Moderador?');" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Moderador</asp:LinkButton>
                        <asp:LinkButton ID="TornarAdministradorUsuario" Width="150px" runat="server" OnClick="TornarAdministradorPalestrante_Click" OnClientClick="return confirm('Realmente deseja tornar o usuário Administrador?');" CssClass="btn btn-link"><i class=""></i>&nbsp;Tornar Administrador</asp:LinkButton>                        
                        <asp:Label runat="server" ForeColor="Red" ID="lblConta"></asp:Label>
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-user-o"></span> Convidado</h4>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Nome Completo</label><br />
                                <asp:Label runat="server" ID="lblNome2Usuario" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-5">
                                <label style="color: red" class="labels">Data Nascimento</label><br />
                                <asp:Label runat="server" ID="lblDtNascUsuario" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-7">
                                <label style="color: red" class="labels">Cidade - UF</label><br />
                                <asp:Label runat="server" ID="lblCidade2Usuario" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-5">
                                <label style="color: red" class="labels">Sexo</label><br />
                                <asp:Label runat="server" ID="lblSexoUsuario" style="color: white; font-size:16px" class="labels"></asp:Label>
                            </div>
                            <div class="col-md-7">
                                <label style="color: red" class="labels">Nível de Escolaridade</label><br />
                                <asp:Label runat="server" ID="lblEscolaridadeUsuario" style="color: white; font-size:16px" class="labels"></asp:Label> 
                            </div>
                            <div class="col-md-12">
                                <label style="color: red" class="labels">Biografia</label><br />
                                <asp:Label runat="server" ID="lblBioUsuario" style="color: white; font-size:16px" class="labels"></asp:Label>                                
                            </div>
                        </div><br />
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="cbReceberEmailUsuario" Enabled="false" style="color: white" Text="Receber e-mail com notificações sobre novos eventos/seminários." />
                            </div>                          
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </asp:Panel>
    <div class="text-center">
        <asp:Label runat="server" Font-Size="18px" ID="lblResultado"></asp:Label><br /><br />
        <asp:LinkButton ID="btnEnviarEmail" Width="197px" runat="server" data-toggle="modal" data-target="#Email" CssClass="botao btn-primary"><i class="fa fa-envelope-o"></i>&nbsp;Enviar E-mail</asp:LinkButton>
        <asp:LinkButton ID="btnBanir" Width="150px" runat="server" OnClick="btnBanir_Click" OnClientClick="return confirm('Deseja realmente excluir o usuário?');" ForeColor="White" CssClass="botao btn-danger"><i class="fa fa-ban"></i>&nbsp;Banir</asp:LinkButton>
        </div><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    </div>
    <section id="Email" class="modal fade" role="dialog" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="color:red;"><span class="fa fa-envelope-o"></span> Enviar E-mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>                        
                </div>
                <div class="modal-body">
                    <div role="form">
                        <div class="form-group">
                            <label><span class="fa fa-bookmark-o"></span> Título</label>
                            <asp:TextBox runat="server" class="form-control" ID="EmailTitulo" placeholder="Digite o título"/><br />                               
                            <label><span class="fa fa-commenting-o"></span> Mensagem</label>
                            <asp:TextBox runat="server" class="form-control" ID="Emailmensagem" TextMode="MultiLine" Rows="5" placeholder="Digite a mensagem"/><br />
                            <asp:LinkButton ID="btnEnviar" OnClick="btnEnviar_Click" runat="server" CssClass="btn btn-default btn-success btn-block"><i class="fa fa-send"></i>&nbsp;Enviar</asp:LinkButton>
                        </div>
                    </div>
                </div>                    
            </div>
        </div>
    </section>
</asp:Content>
