<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GerenciarConta.aspx.cs" Inherits="Webinar.GerenciarConta" %>
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
    <div style="background: rgba(25, 31, 32, 0.8); width:100%; height:100%; min-height:100%" id="intro-container"><br /><br /><br /><br /><br /><br /><br />
        <div class="row">
            <div class="col-md-3">
                <div class="align-items-left text-left p-3 py-5">
                    <h5 style="color:#ef4136"><span class="fa fa-pie-chart"></span> Gerenciar Conta</h5>
                    <ul style="list-style:none; margin-left:-20px">                        
                        <li class="btn-link"><asp:LinkButton runat="server" ID="btnAlterarSenha" OnClick="btnAlterarSenha_Click">Alterar Senha</asp:LinkButton></li>
                        <li class="btn-link"><asp:LinkButton runat="server" ID="btnEditarPerfil" OnClick="btnEditarPerfil_Click">Editar Perfil</asp:LinkButton></li>
                        <li class="btn-link"><asp:LinkButton runat="server" ID="btnMeusCertificados" OnClick="btnMeusCertificados_Click">Meus Certificados</asp:LinkButton></li>
                        <li class="btn-link"><asp:LinkButton runat="server" ID="btnMeuHistorico" OnClick="btnMeuHistorico_Click">Meu Histórico</asp:LinkButton></li>
                    </ul><br /><br /><br />
                </div>               
            </div>
            <hr style="border-left:1px solid #ef4136; height: 300px; margin-left: -30px; margin-right: 30px" />
            <div class="col-md-5">
                <div class="p-3 py-5">
                    <asp:Panel runat="server" ID="PanelAlterarSenha" Visible="false">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-pencil"></span> Alterar Senha</h4>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Senha atual</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSenhaAtual" TextMode="Password" placeholder="Digite a senha atual" />
                                <asp:Label runat="server" ID="lblSenhaAtual" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Nova senha</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNovaSenha" TextMode="Password" placeholder="Digite a nova senha" />
                                <asp:Label runat="server" ID="lblNovaSenha" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-4 text-center">
                            <div class="col-md-6">
                                <asp:Button runat="server" ID="btnSalvarNovaSenha" Text="Salvar" CssClass="botao" OnClick="btnSalvarNovaSenha_Click" />
                            </div>
                        </div>
                        <div class="row mt-4 text-center">
                            <div class="col-md-6">
                                <asp:Label runat="server" ID="lblTrocarSenha"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="PanelMeusCertificados" Visible="false">
                         <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-list-alt"></span> Meus Certificados</h5><br />
                                <asp:Label runat="server" ID="lblCertificados" style="color: white" CssClass="labels" />
                            </div>
                            <div class="text-center">
                                <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                <asp:GridView runat="server" ID="gvCertificados" AllowSorting="True" OnRowCommand="gvCertificados_RowCommand" HorizontalAlign="Center" OnSorting="gvCertificados_Sorting" HeaderStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" BackColor="Transparent" BorderColor="#999999" BorderWidth="2px" CellPadding="3" ForeColor="White" GridLines="Vertical" PageSize="30" CssClass="Grid">     
            
                                    <Columns>                
                                        <asp:TemplateField HeaderText="Evento" SortExpression="EventoTitulo" ItemStyle-HorizontalAlign="Center">                   
                                            <ItemTemplate><asp:Label ID="lblEventoTitulo" Text='<%#Eval("EventoTitulo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Palestra" SortExpression="PalestraTitulo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblPalestraTitulo" Text='<%#Eval("PalestraTitulo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Data de início" SortExpression="DtInicio" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblDtInicio" Text='<%#Eval("DtInicio")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Data de conclusão" SortExpression="DtFinal" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblDtFinal" Text='<%#Eval("DtFinal")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridPalestrante" runat="server" CommandName="SendCertificado" CausesValidation="false" Text="Visualizar" CommandArgument='<%# Eval("IDCertificado") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
            
                                    </Columns>            
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#231F20" Font-Bold="True" Font-Underline="true" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                    <PagerSettings  Position="Bottom" 
                                        Mode="NextPreviousFirstLast"  
                                        PreviousPageText="<i class='icon-previous' title='Página Anterior' /></i>" 
                                        NextPageText="<i class='icon-next' title='Próxima Página' /></i>" 
                                        FirstPageText="<i class='icon-backward' title='Primeira Página' /></i>" 
                                        LastPageText="<i class='icon-forward2' title='Última Página' /></i>" 
                                        PageButtonCount="3" />
                                </asp:GridView>
                            </div>
                            </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="PanelMeuHistorico" Visible="false">
                        <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-list-alt"></span> Meu Histórico</h5><br />
                                <asp:Label runat="server" ID="Label1" style="color: white" CssClass="labels" />
                            </div>
                            <div class="text-center">
                                <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                <asp:GridView runat="server" ID="gvHistorico" HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" BackColor="Transparent" BorderColor="#999999" BorderWidth="2px" CellPadding="3" ForeColor="White" GridLines="Vertical" PageSize="30" CssClass="Grid">     
            
                                    <Columns>                
                                        <asp:TemplateField HeaderText="Evento" SortExpression="EventoTitulo" ItemStyle-HorizontalAlign="Center">                   
                                            <ItemTemplate><asp:Label ID="lblEventoTitulo" Text='<%#Eval("EventoTitulo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Palestra" SortExpression="PalestraTitulo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblPalestraTitulo" Text='<%#Eval("PalestraTitulo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Data de início" SortExpression="Data1" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblDtInicio" Text='<%#Eval("Data1")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Último Acesso" SortExpression="Data2" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblDtFinal" Text='<%#Eval("Data2")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Progresso" SortExpression="Porcentagem" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblProgresso" Text='<%#Eval("Porcentagem")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
            
                                    </Columns>            
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#231F20" Font-Bold="True" Font-Underline="true" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                    <PagerSettings  Position="Bottom" 
                                        Mode="NextPreviousFirstLast"  
                                        PreviousPageText="<i class='icon-previous' title='Página Anterior' /></i>" 
                                        NextPageText="<i class='icon-next' title='Próxima Página' /></i>" 
                                        FirstPageText="<i class='icon-backward' title='Primeira Página' /></i>" 
                                        LastPageText="<i class='icon-forward2' title='Última Página' /></i>" 
                                        PageButtonCount="3" />
                                </asp:GridView>
                            </div>
                            </div>
                    </asp:Panel>
                </div>
            </div>
        </div><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    </div>
</asp:Content>
