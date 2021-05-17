<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="frmPalestra.aspx.cs" Inherits="Webinar.frmPalestra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="intro">
        <div class="intro-container wow fadeIn">
            <div class="mt-5 mb-5">
                <div class="row">
                    <div class="col-md-3">
                        <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                            <asp:Image runat="server" ID="imgCapaPalestra" Width="300" />                    
                        </div>
                    </div>
                    <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <asp:Label style="color: red; font-size:24px" runat="server" ID="lblTituloPalestra">Título</asp:Label>
                        </div>                                                       
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <asp:Label style="color: red; font-size:20px" runat="server" ID="lblSubTituloPalestra">Subtitulo</asp:Label>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-2">
                                <label style="color: white" class="labels">Duração</label>
                                <asp:Label style="color: white; align-content:flex-start" runat="server" ID="lblDuracaoPalestra">01:30</asp:Label>                                 
                            </div>
                            </div>
                        <div class="row mt-1">
                            <div class="col-md-2">
                                <label style="color: white" class="labels">Data</label>
                                <asp:Label style="color: white" runat="server" ID="lblDataPalestra">24/02/2011</asp:Label>                          
                            </div>
                        </div>
                        <div class="row mt-1">                            
                            <div class="col-md-12">
                                <asp:Label style="color: white" runat="server" ID="lblSinopseP1Palestra">Sinopse P1</asp:Label>                                
                            </div>
                        </div>
                        <div class="row mt-1"> 
                            <div class="col-md-12">
                                <asp:Label style="color: white" runat="server" ID="lblSinopseP2Palestra">Sinopse P2</asp:Label>                               
                            </div>
                        </div>
                        <div class="row mt-1"> 
                            <div class="col-md-12">
                                <asp:Label style="color: white" runat="server" ID="lblSinopseP3Palestra">Sinopse P3</asp:Label>                                
                            </div>
                        </div>
                        <div class="row mt-1"> 
                            <div class="col-md-12">
                                <asp:Label style="color: white" runat="server" ID="lblSinopseP4Palestra">Sinopse P4</asp:Label>                                 
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <asp:Label style="color: red; font-size:20px" runat="server" ID="lblApresentadoresPalestra">Apresentadores</asp:Label>
                            </div>
                        </div>
                        <asp:Image runat="server" ID="imgUsuario" CssClass="rounded-circle mt-5" width="150" />
                        <asp:Label runat="server" ID="NomeApresentador" style="color: red; font-size:14px">Nome Palestrante</asp:Label>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
