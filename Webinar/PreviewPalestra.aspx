<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PreviewPalestra.aspx.cs" Inherits="Webinar.PreviewPalestra" %>
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
    <div style="background: rgba(25, 31, 32, 0.8); align-content: center"><br /><br /><br /><br /><br /><br /><br />                
        <asp:Panel runat="server" ID="Panel1" HorizontalAlign="Center">
            <div class="mx-auto" style="width: 800px;">
                <asp:Table runat="server" CssClass="d-flex justify-content-between align-items-center mb-2">
                <asp:TableHeaderRow HorizontalAlign="Center">
                    <asp:TableHeaderCell ColumnSpan="15">
                        <asp:Image runat="server" ID="imgPalestra" Width="800px" />
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                    <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red; font-size: 24px" ID="lblTitulo"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red; font-size: 18px" ID="lblSubTitulo"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red; font-size: 14px" ID="lblCategoria"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 12px" ID="lblDataExibir"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 12px" ID="lblDuracao"/>
                    </asp:TableCell>
                </asp:TableRow>                                
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br /><br />"/>                        
                    </asp:TableCell>
                </asp:TableRow> 
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 14px" ID="lblSinopseP1"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow> 
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 14px" ID="lblSinopseP2"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 14px" ID="lblSinopseP3"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" style="color: white; font-size: 14px" ID="lblSinopseP4"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br />"/>                        
                    </asp:TableCell>
                </asp:TableRow> 
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label runat="server" ID="lblAutoriza" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label runat="server" style="color: red;" Text="<br /><br />"/>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="center">                        
                        <asp:Label runat="server" style="color: red; font-size: 24px" Text="Apresentador"/>                      
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div class="row">
                <div class="col-4 col-md-4 mx-auto">
                    <asp:Image runat="server" ID="imgPalestrante" CssClass="rounded-circle mt-5" Width="250px"/>
                    <br />
                    <asp:Label runat="server" ID="lblNome" style="color: white"/>
                    <br />
                    <asp:Label runat="server" ID="lblEmail" style="color: white" /><br />
                    <asp:Label runat="server" ID="lblCidade" style="color: white" />
                    <br />
                    <asp:Label runat="server" ID="lblIdade" style="color: white" />
                </div>
                <div class="col-8 col-md-8 text-left"><br /><br />
                    <asp:Label runat="server" ID="lblEspecialidade" style="color: white" /><br /><br />
                    <asp:Label runat="server" ID="lblBio1" style="color: white" /><br />
                    <asp:Label runat="server" ID="lblBio2" style="color: white" /><br />
                    <br />
                    <a runat="server" id="btnTWT"><i class="fa fa-twitter"></i></a>&nbsp;&nbsp;
                    <a runat="server" id="btnFACE"><i class="fa fa-facebook"></i></a>&nbsp;&nbsp;
                    <a runat="server" id="btnGG"><i class="fa fa-google-plus"></i></a>&nbsp;&nbsp;
                    <a runat="server" id="btnIN"><i class="fa fa-linkedin"></i></a>&nbsp;
                </div>                
            </div>
            <br /><br />
            </div>  
            <asp:LinkButton ID="btnAutorizarPalestra" OnClick="btnAutorizarPalestra_Click" runat="server" Width="170" CssClass="botao btn-success"><i class="fa fa-thumbs-o-up"></i>&nbsp;Autorizar</asp:LinkButton>   &nbsp;&nbsp;&nbsp; 
            <asp:LinkButton ID="btnNegarPalestra" OnClick="btnNegarPalestra_Click" runat="server" Width="150" CssClass="botao btn-danger"><i class="fa fa-thumbs-o-down"></i>&nbsp;Negar</asp:LinkButton>
            <asp:LinkButton ID="btnSalvarPalestra" OnClick="btnSalvarPalestra_Click" runat="server" Width="190" CssClass="botao btn-success"><i class="fa fa-thumbs-o-up"></i>&nbsp;Ok, Aprovar</asp:LinkButton>   &nbsp;&nbsp;&nbsp; 
            <asp:LinkButton ID="btnEditarPalestra" OnClientClick="JavaScript:window.history.back(1); return false;" runat="server" Width="150" CssClass="botao btn-danger"><i class="fa fa-thumbs-o-down"></i>&nbsp;Editar</asp:LinkButton>
            <asp:LinkButton ID="btnEditarADM" Visible="false" OnClick="btnEditarADM_Click" runat="server" Width="190" CssClass="botao btn-success"><i class="fa fa-thumbs-o-down"></i>&nbsp;Editar</asp:LinkButton>
        </asp:Panel> 
    <br /><br /><br /><br /><br /></div>
</asp:Content>
