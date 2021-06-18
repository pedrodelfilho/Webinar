<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PainelAdministrador.aspx.cs" Inherits="Webinar.PainelAdministrador" %>
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
   <div style="background: rgba(25, 31, 32, 0.8); width:100%; height:100%; min-height:100%" id="intro-container"><br /><br /><br /><br /><br /><br />
        <div class="row">
                <div class="col-md-3">
                    <div class="align-items-left text-left p-3 py-5">
                        <h5 style="color:#ef4136"><span class="fa fa-user-secret"></span> Painel Administração</h5>
                        <ul style="list-style:none; margin-left:-20px">
                            <li class="btn-link"><asp:LinkButton runat="server" ID="btnUsuarios" OnClick="btnUsuarios_Click">Usuários</asp:LinkButton></li>
                            <li class="btn-link"><asp:LinkButton runat="server" ID="btnEventos" OnClick="btnEventos_Click">Eventos</asp:LinkButton></li>
                            <li class="btn-link"><asp:LinkButton runat="server" ID="btnPalestras" OnClick="btnPalestras_Click">Palestras</asp:LinkButton></li>
                            <li class="btn-link"><asp:LinkButton runat="server" ID="btnPendencias" OnClick="btnPendencias_Click">Solicitações Pendentes</asp:LinkButton></li>
                            <li class="btn-link"><asp:LinkButton runat="server" ID="btnPaginaInicial" OnClick="btnPaginaInicial_Click">Página Inicial</asp:LinkButton></li>
                        </ul><br /><br /><br />
                        <asp:Button runat="server" ID="btnAdicionarEvento" CssClass="botaosub" OnClick="btnAdicionarEvento_Click" Visible="false" Text="Adicionar Evento" Width="200px"/>
                        <asp:Button runat="server" ID="btnAdicionarPalestraADM" OnClick="btnAdicionarPalestra_Click" CssClass="botaosub" Visible="false" Text="Adicionar Palestra" Width="200px" />
                    </div>               
                </div>
                <hr style="border-left:1px solid #ef4136; height: 220px; margin-left: -30px; margin-right: 30px" />
                <div class="col-md-8">
                    <div class="p-3 py-5">
                        <asp:HiddenField runat="server" id="hfWasConfirmed" />
                        <asp:Panel ID="PanelUsuarios" Visible="false" runat="server">
                            <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-list-alt"></span> Listagem de Usuários</h5><br />
                                <asp:Label runat="server" ID="lblRes1" style="color: white" CssClass="labels" />
                            </div>
                            <div class="text-center">
                                <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                <asp:GridView runat="server" ID="gvUsuarios" AllowSorting="True" OnRowCommand="gvUsuarios_RowCommand" HorizontalAlign="Center" OnSorting="gvUsuarios_Sorting" HeaderStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" BackColor="Transparent" BorderColor="#999999" BorderWidth="2px" CellPadding="3" ForeColor="White"
                                GridLines="Vertical" PageSize="30" CssClass="Grid" >         
            
                                    <Columns>
                
                                        <asp:TemplateField HeaderText="ID Usuário" SortExpression="UserId" ItemStyle-HorizontalAlign="Center">                   
                                            <ItemTemplate><asp:Label ID="lblUserId" Text='<%#Eval("UserId")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Nome" SortExpression="Username">
                                            <ItemTemplate><asp:Label ID="lblNome" Text='<%#Eval("Username")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="E-mail" SortExpression="Email">                    
                                            <ItemTemplate><asp:Label ID="lblEmail" Text='<%#Eval("Email")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Data de Cadastro" SortExpression="CreatedDate">                    
                                            <ItemTemplate><asp:Label ID="lblCreatedDate" Text='<%#Eval("CreatedDate")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Último Acesso" SortExpression="LastLoginDate">
                                            <ItemTemplate><asp:Label ID="lblLastLoginDate" Text='<%#Eval("LastLoginDate")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                            <ItemTemplate><asp:Label ID="lblTIpo" Text='<%#Eval("Tipo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridPalestrante" runat="server" CommandName="SendUsuarios" CausesValidation="false" Text="Visualizar" CommandArgument='<%# Eval("UserId") %>' />
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
                        <asp:Panel ID="PanelEventos" Visible="false" runat="server">
                         <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-list-alt"></span> Listagem de Eventos</h5><br />
                                <asp:Label runat="server" ID="lblEventoRes1" Font-Size="Large" CssClass="labels" /><br />
                                <asp:Label runat="server" ID="lblEventoRes" style="color: white" CssClass="labels" />
                            </div>
                            <div class="text-center">
                                <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                <asp:GridView runat="server" ID="gvEvento" RowStyle-HorizontalAlign="Center" AllowSorting="True" OnRowCommand="gvEvento_RowCommand" HorizontalAlign="Center" OnSorting="gvEvento_Sorting" HeaderStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White" 
                                GridLines="Vertical" PageSize="30">          
            
                                    <Columns>
                
                                        <asp:TemplateField HeaderText="ADM Criador" SortExpression="Username" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblEventoUsername" Text='<%#Eval("Username")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Evento" SortExpression="EventoTitulo" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblEventoTitulo" Text='<%#Eval("EventoTitulo")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Data de Início" SortExpression="EventoDtIni" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblEventoDtIni" Text='<%#Eval("EventoDtIni")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Data de Término" SortExpression="EventoDtTer" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblEventoDtTer" Text='<%#Eval("EventoDtTer")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Moderador Responsável" SortExpression="ModResponsavel" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblEventoDtModRespavel" Text='<%#Eval("ModResponsavel")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridaDm" runat="server" OnClientClick="return getConfirmationValue();" CommandName="EncerrarEventos" CausesValidation="false" Text="Encerrar" CommandArgument='<%# Eval("IDEvento") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGridConEmpresarial" runat="server" Height="25px" ImageUrl="~/img/delete.png" CommandName="DelEvento" CommandArgument='<%# Eval("IDEvento") %>' OnClientClick="return confirm('Deseja excluir o item selecionado?');" />
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
                        <asp:Panel ID="PanelPalestras" Visible="false" runat="server">
                            <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-list-alt"></span> Listagem de Palestras</h5><br />
                                <asp:Label runat="server" ID="lblResPalestras" style="color: white" CssClass="labels" />
                            </div>
                            <div class="text-center">
                                <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                <asp:GridView runat="server" ID="gvPalestras" RowStyle-HorizontalAlign="Center" AllowSorting="True" OnRowCommand="gvPalestras_RowCommand" HorizontalAlign="Center" OnSorting="gvPalestras_Sorting" HeaderStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White" 
                                GridLines="Vertical" PageSize="30">          
            
                                    <Columns>
                
                                        <asp:TemplateField HeaderText="Palestrante" SortExpression="Palestrante" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblPalestrante" Text='<%#Eval("Palestrante")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Cadastrado por" SortExpression="Criador" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblCriador" Text='<%#Eval("Criador")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Data de Cadastro" SortExpression="PalestraDtCriacao" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblPalestraDtCriacao" Text='<%#Eval("PalestraDtCriacao")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Link" SortExpression="PalestraLink" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblLink" Text='<%#Eval("PalestraLink")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Aprovada" SortExpression="PalestraAprovada" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblPalestrAprovada" Text='<%#Eval("PalestraAprovada")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridPalestrante" runat="server" CommandName="SendPalestras" CausesValidation="false" Text="Visualizar" CommandArgument='<%# Eval("IDPalestra") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelPalestras" runat="server" Height="25px" ImageUrl="~/img/delete.png" CommandName="SendDelPalestras" CommandArgument='<%# Eval("IDPalestra") %>' OnClientClick="return confirm('Realmente deseja excluir a Palestra?');" />
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
                        <asp:Panel ID="PanelPendencias" Visible="false" runat="server">
                            <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-exclamation-triangle"></span> Listagem de Pendências</h5><br /><br />
                            </div>
                            <asp:Label runat="server" ID="lblResPalestrantes" style="color: white" CssClass="labels" />
                             <asp:Panel runat="server">
                                <div class="text-center">
                                    <h7 style="color:#ef4136;"><span class="fa fa-address-card-o"></span> Palestrantes / Apresentadores</h7><br />                               
                                    <asp:Label runat="server" ID="lblResPalestrante" style="color: white" CssClass="labels" />
                                </div>
                                <div class="text-center">
                                    <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                    <asp:GridView runat="server" ID="gvPalestrante" OnSorting="gvPalestrante_Sorting" AllowSorting="True" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" 
                                        AutoGenerateColumns="False" OnRowCommand="gvPalestrante_RowCommand" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White"
                                        GridLines="Vertical" PageSize="30">          
            
                                        <Columns>
                
                                            <asp:TemplateField HeaderText="Palestrante" SortExpression="Username" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate><asp:Label ID="lblNome" Text='<%#Eval("Username")%>' runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                
                                            <asp:TemplateField HeaderText="E-mail" SortExpression="Email" ItemStyle-HorizontalAlign="Center">                    
                                                <ItemTemplate><asp:Label ID="lblEmail" Text='<%#Eval("Email")%>' runat="server"/></ItemTemplate>
                                            </asp:TemplateField>  
                
                                            <asp:TemplateField HeaderText="Data de Cadastro" SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center">                    
                                                <ItemTemplate><asp:Label ID="lblCreatedDate" Text='<%#Eval("CreatedDate")%>' runat="server" /></ItemTemplate>
                                            </asp:TemplateField>  
                
                                            <asp:TemplateField HeaderText="Último Acesso" SortExpression="LastLoginDate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate><asp:Label ID="lblLastLoginDate" Text='<%#Eval("LastLoginDate")%>' runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnGridPalestrante" runat="server" CausesValidation="false" CommandName="SendPalestrantes" Text="Visualizar" CommandArgument='<%# Eval("UserId") %>' />
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
                                </div><br /><br />
                            </asp:Panel>
                            <asp:Panel runat="server">
                                <div class="text-center">
                                    <h7 style="color:#ef4136;"><span class="fa fa-video-camera"></span> Palestras / Seminários</h7><br />                              
                                    <asp:Label runat="server" ID="lblResPalestra" style="color: white" CssClass="labels" />
                                </div>
                                <div class="text-center">
                                    <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                    <asp:GridView runat="server" ID="gvPalestra" OnSorting="gvPalestra_Sorting" AllowSorting="True" HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnRowCommand="gvPalestra_RowCommand" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White"
                                    GridLines="Vertical" PageSize="30">
            
                                    <Columns>
                
                                        <asp:TemplateField HeaderText="ID Palestra" SortExpression="IDPalestra" ItemStyle-HorizontalAlign="Center">                   
                                            <ItemTemplate><asp:Label ID="lblIDPalestra" Text='<%#Eval("IDPalestra")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Palestrante" SortExpression="Username" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate><asp:Label ID="lblIDPalestrante" Text='<%#Eval("Username")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="Data de Criação" SortExpression="PalestraDtCriacao" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblPalestraDtCriacao" Text='<%#Eval("PalestraDtCriacao")%>' runat="server"/></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField HeaderText="Autorização de Uso" SortExpression="PalestraAutoriza" ItemStyle-HorizontalAlign="Center">                    
                                            <ItemTemplate><asp:Label ID="lblPalestraAutoriza" Text='<%#Eval("PalestraAutoriza")%>' runat="server" /></ItemTemplate>
                                        </asp:TemplateField>  
                
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGridPalestrante" runat="server" CausesValidation="false" CommandName="SendPalestra" Text="Visualizar" CommandArgument='<%# Eval("IDPalestra") %>' />
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

                        </asp:Panel>
                        <asp:Panel ID="PanelPaginaInicial" Visible="false" runat="server">
                            <div class="text-center">
                                <h5 style="color:#ef4136;"><span class="fa fa-window-restore"></span> Modificar itens na página inicial</h5><br /><br />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Introdução</label>
                                <label style="color: white" class="labels">Título</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" ID="txtTitulo" placeholder="Digite o título" />
                            </div>
                             <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Techo em destaque do título</label>
                                <asp:TextBox runat="server"  CssClass="form-control" ID="txtDestaque" placeholder="Digite o destaque" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Subtítulo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" ID="txtSubTitulo" placeholder="Digite o subtítulo" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Vídeo da intro</label>
                                <asp:TextBox runat="server"  CssClass="form-control" ID="txtLink" placeholder="Digite o link" />
                            </div><br />
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Sobre</label>
                                <label style="color: white" class="labels">Quem somos</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" ID="txtQuemSomos" placeholder="Digite a mensagem" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Quando</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" ID="txtQuando" placeholder="Digite a mensagem" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Onde</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" ID="txtOnde" placeholder="Digite a mensagem" />
                            </div><br />
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Conexão Empresarial</label>
                                <label style="color: white" class="labels">Adicionar Conexão</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNomeConexaoHome" placeholder="Digite o nome" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <asp:FileUpload ID="fuLogoConexaoEmpresarial" runat="server" style="color: white; font-size:small"/><br /><br />
                                <asp:Image runat="server" ID="imgConexaoEmpresarialHome" Width="250px" />
                            </div>
                            <div class="col-md-12 mx-auto text-center">
                                <label style="color: white" class="labels">Remover Conexão</label><br />
                                <asp:Label runat="server" CssClass="labels" ID="lblResConEmpresarial"></asp:Label>
                                <div class="text-center">
                                    <div style="display:inline-block; border-radius:8px; overflow:hidden;">
                                    <asp:GridView runat="server" ID="gvConEmpresarial" OnSorting="gvConEmpresarial_Sorting" AllowSorting="True" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" 
                                        AutoGenerateColumns="False" OnRowCommand="gvConEmpresarial_RowCommand" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White"
                                        GridLines="Vertical" PageSize="30">          
            
                                        <Columns>

                                            <asp:TemplateField HeaderText="ID" SortExpression="IDConEmpresarial">
                                                <ItemTemplate><asp:Label ID="lblIDConEmpresarial" Text='<%#Eval("IDConEmpresarial")%>' runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                
                                            <asp:TemplateField HeaderText="Nome" SortExpression="NmConEmpresarial">
                                                <ItemTemplate><asp:Label ID="lblNomeConEmpresarial" Text='<%#Eval("NmConEmpresarial")%>' runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                
                                            <asp:TemplateField HeaderText="Data de início" SortExpression="DtIniConEmpresarial">                    
                                                <ItemTemplate><asp:Label ID="lblDtini" Text='<%#Eval("DtIniConEmpresarial")%>' runat="server"/></ItemTemplate>
                                            </asp:TemplateField>
                
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnGridConEmpresarial" runat="server" Height="25px" ImageUrl="~/Images/delete.png" CommandName="SendConEmpresarial" CommandArgument='<%# Eval("IDConEmpresarial") %>' OnClientClick="return confirm('Deseja excluir o item selecionado?');" />
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
                                </div><br />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Perguntas Frequentas</label>
                                <label style="color: white" class="labels">Pergunta 1</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPergunta1" TextMode="MultiLine" Rows="2" placeholder="Digite a pergunta" />
                                <label style="color: white" class="labels">Resposta 1</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtResposta1" TextMode="MultiLine" Rows="2" placeholder="Digite a resposta" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Pergunta 2</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPergunta2" TextMode="MultiLine" Rows="2" placeholder="Digite a pergunta" />
                                <label style="color: white" class="labels">Resposta 2</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtResposta2" TextMode="MultiLine" Rows="2" placeholder="Digite a resposta" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Pergunta 3</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPergunta3" TextMode="MultiLine" Rows="2" placeholder="Digite a pergunta" />
                                <label style="color: white" class="labels">Resposta 3</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtResposta3" TextMode="MultiLine" Rows="2" placeholder="Digite a resposta" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Pergunta 4</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPergunta4" TextMode="MultiLine" Rows="2" placeholder="Digite a pergunta" />
                                <label style="color: white" class="labels">Resposta 4</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtResposta4" TextMode="MultiLine" Rows="2" placeholder="Digite a resposta" />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: white" class="labels">Pergunta 5</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPergunta5" TextMode="MultiLine" Rows="2" placeholder="Digite a pergunta" />
                                <label style="color: white" class="labels">Resposta 5</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtResposta5" TextMode="MultiLine" Rows="2" placeholder="Digite a resposta" />
                            </div><br />
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Contate-nos</label>
                                <label style="color: white" class="labels">Endereço</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEndereco" placeholder="Digite o endereço" />
                                <label style="color: white" class="labels">Telefone</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefone" placeholder="Digite o telefone" />
                                <label style="color: white" class="labels">E-mail</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail" placeholder="Digite o e-mail" />
                                <label style="color: white" class="labels">E-mail que receberá as mensagens</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmailADM" placeholder="Digite o e-mail" /><br />
                            </div>
                            <div class="col-md-6 mx-auto text-center">
                                <label style="color: #ef4136; font-size: 16px; display:block; text-align:left;" class="labels">Certificado BackGround</label>
                                <asp:Image runat="server" ID="imgBack" width="400" Height="150" />
                                <label style="color: white" class="labels">Escolher imagem de fundo para o Certificado (Resolução recomendada: 1200 x 800)</label>
                                <input runat="server" type="file" class="form-control" id="imgBackGroundCertificado" />
                            </div>
                            <div class="col-md-6 mx-auto text-center"><br />
                                <asp:LinkButton runat="server" ID="btnVisualizarPagInicial" OnClick="btnVisualizarPagInicial_Click" CssClass="botaosub btn-outline-info"><i class=""></i>&nbsp;Visualizar Página Inicial</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCertificado" OnClick="btnCertificado_Click" CssClass="botaosub btn-outline-info"><i class=""></i>&nbsp;Visualizar Cetificado</asp:LinkButton>
                                
                            </div>
                            <div class="col-md-6 mx-auto text-center"><br /><br /><br />
                                <asp:LinkButton runat="server" ID="btnAplicarPagInicial" OnClick="btnAplicarPagInicial_Click" OnClientClick="return confirm('Realmente deseja atualizar a página inicial?');" CssClass="botao btn-outline-success"><i class=""></i>&nbsp;Salvar</asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </div>
                </div>       
            </div>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <script>
         $("#<%=fuLogoConexaoEmpresarial.ClientID%>").on('change', function () {
             if (this.files[0].type.indexOf("image") > -1) {
                 var reader = new FileReader();
                 reader.onload = function (e) {
                     $('#<%=imgConexaoEmpresarialHome.ClientID%>').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
        else {                
            $('#<%=imgConexaoEmpresarialHome.ClientID%>').attr('src', '');
                alert('Não é uma imagem válida')
            }
        });
    </script>
       <script>
           $("#<%=imgBackGroundCertificado.ClientID%>").on('change', function () {
               if (this.files[0].type.indexOf("image") > -1) {
                   var reader = new FileReader();
                   reader.onload = function (e) {
                       $('#<%=imgBack.ClientID%>').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
        else {                
            $('#<%=imgBack.ClientID%>').attr('src', '');
                alert('Não é uma imagem válida')
            }
        });
       </script>
       <script>
           function getConfirmationValue() {
               if (confirm('Todas as palestras que contém nesse evento serão enviadas para o Acervo junto com o Evento. Você tem certeza que deseja continuar?')) {
                   $('#<%=hfWasConfirmed.ClientID%>').val('true')
   }
   else{
      $('#<%=hfWasConfirmed.ClientID%>').val('false')
               }
               return true;
           }
       </script>
    </div>

</asp:Content>
