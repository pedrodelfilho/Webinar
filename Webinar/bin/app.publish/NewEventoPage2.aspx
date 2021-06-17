<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewEventoPage2.aspx.cs" Inherits="Webinar.NewEventoPage2" %>
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
    <div style="background: rgba(25, 31, 32, 0.8);" id="intro-container"><br /><br /><br /><br /><br />
        <div id="schedule">
            <div runat="server" class="container wow fadeInUp">
                <div class="pull-right"><br /><br />                    
                </div>
                <div class="mt-5 mb-5">
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
        <div class="text-center">
            <asp:Label runat="server" ID="ltFinal" ForeColer="Red" /><br /><br />
            <asp:Button ID="btnProx" OnClick="btnProx_Click" runat="server" class="botaosub" Text="Próximo" ></asp:Button><br /><br /><br /><br /><br /><br /><br />
        </div><br /><br /><br /><br /><br />
    </div>
</asp:Content>
