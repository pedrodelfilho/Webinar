<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewEvento.aspx.cs" Inherits="Webinar.NewEvento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    input[type="time"]::-webkit-calendar-picker-indicator {
        background: none;
        display: none;
        }
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
                    <div class="d-flex justify-content-between align-items-center mb-2">
                        <h4 style="color:red;"><span class="fa fa-video-camera"></span> Criar Evento</h4>
                    </div>
                </div>                
            </div>
        </div>
        <asp:Panel runat="server" ID="Panel1">
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label runat="server" id="ini" style="color: white" class="labels">Título</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTituloEvento" placeholder="Digite o título"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTituloEvento" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Limite de 100 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>        
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Subtítulo</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSubTituloEvento" placeholder="Digite o subtítulo" />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSubTituloEvento" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Limite de 100 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>  
                        <div class="row mt-1">                            
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 1º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP1Evento" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP1Evento" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 2º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP2Evento" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP2Evento" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Data de Início</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" Width="200px" ID="txtDataIniEvento"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-6">
                                <label style="color: white" class="labels">Data de Término</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" Width="200px" ID="txtDataTerEvento"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <asp:CompareValidator ID="cvDtFinal" runat="server" ControlToCompare="txtDataIniEvento" ControlToValidate="txtDataTerEvento" ErrorMessage="Data de término deve ser maior ou igual a data de início" ForeColor="Red" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="addaq">
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="col-md-12">
            <div class="row mt-1">
                <div class="col-md-12"><br />                     
                    <div class="mt-1 text-center">
                        <asp:Button ID="btnNextPanel1" OnClick="btnNextPanel1_Click" runat="server" CssClass="botaosub" Text="Continuar"></asp:Button>
                        <asp:Button ID="btnProximo" OnClick="btnProximo_Click" runat="server" CssClass="botaosub" Text="Próximo" Visible="false"></asp:Button>
                    </div>
                </div>
            </div>     
        </div><br /><br /><br /><br /><br /><br /><br />
    </div>
    <%--<script>
        $("#<%=fuCapaEvento.ClientID%>").on('change', function () {
            if (this.files[0].type.indexOf("image") > -1) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgEvento.ClientID%>').attr('src', e.target.result);
        }
        reader.readAsDataURL(this.files[0]);
    }
    else {                
        $('#<%=imgEvento.ClientID%>').attr('src', '');
            alert('Não é uma imagem válida')
        }
    });
    </script>--%>
</asp:Content>
