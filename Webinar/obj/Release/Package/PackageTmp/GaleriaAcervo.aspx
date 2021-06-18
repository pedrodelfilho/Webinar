<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GaleriaAcervo.aspx.cs" Inherits="Webinar.GaleriaAcervo" %>
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
        <div class="container-fluid">

        <div class="section-header">
          <h2>Acervos</h2>
          <p>Galeria</p>
        </div>

      </div>
        
      <div class="container-fluid venue-gallery-container">
        <div class="row no-gutters justify-content-md-center" runat="server" id="Panel1">


        </div>
      </div>
     </div>
</asp:Content>
