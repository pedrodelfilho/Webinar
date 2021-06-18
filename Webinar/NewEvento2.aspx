<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewEvento2.aspx.cs" Inherits="Webinar.NewEvento2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background: rgba(25, 31, 32, 0.8);" id="intro-container"><br /><br /><br /><br /><br /><br /><br />
         <asp:Button runat="server" ID="btnteste" OnClick="btnteste_Click" Text="PDF" />
        <asp:Label runat="server" ID="label"></asp:Label>
    </div>     
</asp:Content>
