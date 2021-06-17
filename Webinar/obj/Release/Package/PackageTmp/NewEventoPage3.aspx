<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewEventoPage3.aspx.cs" Inherits="Webinar.NewEventoPage3" %>
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
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-5">
                <div class="p-3 py-5">                    
                    <label runat="server" style="color: red" class="labels">Início do Evento</label><br />
                    <asp:Label runat="server" style="color: white" ID="lblDtInicio"/><br />
                    <label runat="server" style="color: red" class="labels">Término do Evento</label><br />
                    <asp:Label runat="server" style="color: white" ID="lblDtFinal" /><br />
                    <label runat="server" style="color: red" class="labels">Total de Palestras</label><br />
                    <asp:Label runat="server" style="color: white" ID="lblQtdPalestra" /><br /><br /><br />
                    <label runat="server" style="color: white" class="labels">Designar Moderador</label><br />
                    <asp:DropDownList runat="server" ID="ddlModResp" Width="400" DataSourceID="DsModerador" DataValueField="Username" DataTextField="Username" AppendDataBoundItems="true" CssClass="form-control">
                        <asp:ListItem Value="Todos">Todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="DsModerador" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT Users.Username FROM Moderadores LEFT JOIN Users ON IDModerador = Users.UserId ORDER BY Users.Username ASC"></asp:SqlDataSource><br />
                    <label style="color: white" class="labels">Escolha uma figura de capa</label><br />
                    <input runat="server" type="file" id="inputCapaEvento" class="form-control" />
                    <div class="text-center"><br /><br /><br />
                        <asp:Label runat="server" ID="lblNotificar" Forecolor="Red" Font-Size="Large"></asp:Label><br />
                        <asp:Button ID="btnVisu" runat="server" OnClick="btnVisu_Click" class="botaosub btn-primary" Text="Visualizar" ></asp:Button>
                        <asp:Button ID="btnProx" runat="server" OnClick="btnProx_Click" class="botaosub btn-success" style="border: 2px solid green" Text="Salvar" ></asp:Button><br />
                    </div>
                    <br /><br /><br /><br /><br />
                </div>
            </div>
        </div>
    </div>   
</asp:Content>
