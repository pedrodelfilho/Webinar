<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PainelModerador.aspx.cs" Inherits="Webinar.PainelModerador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="background: rgba(6, 12, 34, 0.8);" id="intro-container"><br /><br /><br /><br /><br /><br /><br /><br />
        <asp:Panel runat="server" HorizontalAlign="Center">
            <div class="align-items-center">
                <h4 style="color:red; font-size:28px"><span class="fa fa-exclamation-triangle"></span> Solicitações pendentes de aprovação</h4>
            </div><br /><br />
            <div class="align-items-center">
                <h5 style="color:red; font-size:20px"><span class="fa fa-address-card-o"></span> Palestrantes / Apresentadores</h5>
            </div>
            <asp:Label runat="server" ID="lblRes1" style="color: white" CssClass="labels" />

            <asp:GridView runat="server" ID="gvPalestrante" OnSorting="gvPalestrante_Sorting" AllowSorting="True" HorizontalAlign="Center" 
                    AutoGenerateColumns="False" OnRowCommand="gvPalestrante_RowCommand" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White"
                    GridLines="Vertical" PageSize="30">          
            
                    <Columns>
                
                        <asp:TemplateField HeaderText="ID Usuário" SortExpression="UserId">                   
                            <ItemTemplate><asp:Label ID="lblUserId" Text='<%#Eval("UserId")%>' runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="Nome" SortExpression="Username" ControlStyle-Width="180px">
                            <ItemTemplate><asp:Label ID="lblNome" Text='<%#Eval("Username")%>' runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="E-mail" SortExpression="Email">                    
                            <ItemTemplate><asp:Label ID="lblEmail" Text='<%#Eval("Email")%>' runat="server"/></ItemTemplate>
                        </asp:TemplateField>  
                
                        <asp:TemplateField HeaderText="Data de Cadastro" SortExpression="CreatedDate" ControlStyle-Width="300">                    
                            <ItemTemplate><asp:Label ID="lblCreatedDate" Text='<%#Eval("CreatedDate")%>' runat="server" /></ItemTemplate>
                        </asp:TemplateField>  
                
                        <asp:TemplateField HeaderText="Último Acesso" SortExpression="LastLoginDate">
                            <ItemTemplate><asp:Label ID="lblLastLoginDate" Text='<%#Eval("LastLoginDate")%>' runat="server" ControlStyle-Width="160px" /></ItemTemplate>
                        </asp:TemplateField>
                
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnGridPalestrante" runat="server" CausesValidation="false" CommandName="SendPalestrante" Text="Visualizar" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
            
                    </Columns>            
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="#060C22" Font-Bold="True" Font-Underline="true" ForeColor="White" />
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
                </asp:GridView><br /><br /><br /><br />
        </asp:Panel>
        <asp:Panel runat="server" HorizontalAlign="Center">
        <div class="align-items-center">
            <h5 style="color:red; font-size:20px"><span class="fa fa-video-camera"></span> Palestras / Seminários</h5>
        </div>
        <asp:Label runat="server" ID="lblRes2" style="color: white" CssClass="labels" />

        <asp:GridView runat="server" ID="gvPalestra" OnSorting="gvPalestra_Sorting" AllowSorting="True" HorizontalAlign="Center" 
            AutoGenerateColumns="False" OnRowCommand="gvPalestra_RowCommand" BackColor="Transparent" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="White"
            GridLines="Vertical" PageSize="30">
            
            <Columns>
                
                <asp:TemplateField HeaderText="ID Palestra" SortExpression="IDPalestra">                   
                    <ItemTemplate><asp:Label ID="lblIDPalestra" Text='<%#Eval("IDPalestra")%>' runat="server" /></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="ID Palestrante" SortExpression="UserId" ControlStyle-Width="180px">
                    <ItemTemplate><asp:Label ID="lblIDPalestrante" Text='<%#Eval("UserId")%>' runat="server" /></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Data de Criação" SortExpression="PalestraDtCriacao">                    
                    <ItemTemplate><asp:Label ID="lblPalestraDtCriacao" Text='<%#Eval("PalestraDtCriacao")%>' runat="server"/></ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Autorização de Uso" SortExpression="PalestraAutoriza" ControlStyle-Width="300">                    
                    <ItemTemplate><asp:Label ID="lblPalestraAutoriza" Text='<%#Eval("PalestraAutoriza")%>' runat="server" /></ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnGridPalestrante" runat="server" CausesValidation="false" CommandName="SendPalestra" Text="Visualizar" CommandArgument='<%# Eval("IDPalestra") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            
            </Columns>            
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="#060C22" Font-Bold="True" Font-Underline="true" ForeColor="White" />
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
    </asp:Panel>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /></div>
</asp:Content>
