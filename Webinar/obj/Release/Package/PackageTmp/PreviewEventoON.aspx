<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PreviewEventoON.aspx.cs" Inherits="Webinar.PreviewEventoON" %>
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
 .event-banner-img {
    position: relative;
    height: 100%;
    border-style: none;
    border-radius: 12px;
    background-position: 50% 50%;
    background-size: cover;
    background-repeat: no-repeat;
    box-shadow: 0 30px 80px 10px rgb(0 0 0 / 12%), 0 11px 30px -7px rgb(0 0 0 / 30%);
    -webkit-transition: all 600ms ease;
    transition: all 600ms ease;
    max-height: 640px;
    min-height: 480px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="background: rgba(25, 31, 32, 0.8);" id="intro-container"><br /><br /><br />
        <asp:Panel runat="server">
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                            <asp:Image runat="server" CssClass="event-banner-img" ID="imgEvento"/>
                        </div>
                        <div class="text-center">
                            <asp:Label runat="server" ID="lblTitulo" Font-Size="XX-Large" ForeColor="#ef4136"></asp:Label><br />
                            <asp:Label runat="server" ID="lblSubTituloEvento" Font-Size="Large" ForeColor="#ef4136"></asp:Label><br /><br /><br />
                        </div>                    
                        <asp:Label runat="server" CssClass="labels" ForeColor="#ef4136" Text="Data início"></asp:Label><br />
                        <asp:Label ID="lblDtInicio" runat="server" ForeColor="White" Font-Size="Medium"></asp:Label><br /> 
                        <asp:Label runat="server" CssClass="labels" ForeColor="#ef4136" Text="Data término"></asp:Label><br />
                        <asp:Label ID="lblDtTermino" runat="server" ForeColor="White" Font-Size="Medium"></asp:Label><br /><br /><br />
                        <asp:Label ID="lblSinopse1Evento" runat="server" Font-Size="Small" ForeColor="White"></asp:Label><br /><br />
                        <asp:Label ID="lblSinopse2Evento" runat="server" Font-Size="Small" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>           
        </asp:Panel>
        <asp:Panel ID="PanelEvento" runat="server">
            <div id="schedule">
                <div runat="server" class="container wow fadeInUp">
                    <div class="pull-right">                        
                    </div>
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Label runat="server" Text="Calendário com programação" Font-Size="XX-Large" ForeColor="#ef4136"></asp:Label>
                    </div>
                    <div class="mt-1 mb-5">
                        <div class="row">                             
                            <div class="col-md-3">
                                <div class="pull-left p-3 py-5">
                                    <ul runat="server" id="ulDias" class="nav nav-tabs" role="tablist">
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="p-3 py-5">
                                    <div runat="server" id="divDias" class="tab-content row justify-content-center">                               
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>            
        </asp:Panel>
    </div>
</asp:Content>
