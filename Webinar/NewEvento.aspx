<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewEvento.aspx.cs" Inherits="Webinar.NewEvento" %>
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
                                <label style="color: white" class="labels">Título</label>
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
                        <div class="row mt-1">
                            <div class="col-md-12"><br />                     
                                <div class="mt-1 text-center">
                                    <asp:Button ID="btnNextPanel1" OnClick="btnNextPanel1_Click" runat="server" CssClass="botao" Text="Continuar"></asp:Button>
                                </div>
                            </div>
                        </div>                            
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="Panel2" Visible="false">            
            <div id="schedule">
                <div class="container wow fadeInUp">
                    <div class="pull-right"><br /><br />
                        <asp:Button runat="server" CssClass="botao" ID="addPalestra" OnClick="addPalestra_Click" Text="+ Palestras" />
                    </div>
                    <div class="mt-5 mb-5">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="pull-left p-3 py-5">
                                    <ul runat="server" id="ulDias" class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" id="dia1" style="width:240px; margin-bottom:4px;" href="#day-1" role="tab" data-toggle="tab">Dia 1</a>
                                        </li>
                                        <%--<li id="dia2" class="nav-item">
                                            <a class="nav-link" style="width:240px; margin-bottom:4px; display: none" href="#day-2" role="tab" data-toggle="tab">Dia 2</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia3" style="width:240px; margin-bottom:4px; display: none" href="#day-3" role="tab" data-toggle="tab">Dia 3</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia4" style="width:240px; margin-bottom:4px; display: none" href="#day-4" role="tab" data-toggle="tab">Dia 4</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia5" style="width:240px; margin-bottom:4px; display: none" href="#day-5" role="tab" data-toggle="tab">Dia 5</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia6" style="width:240px; margin-bottom:4px; display: none" href="#day-6" role="tab" data-toggle="tab">Dia 6</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia7" style="width:240px; margin-bottom:4px; display: none" href="#day-7" role="tab" data-toggle="tab">Dia 7</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia8" style="width:240px; margin-bottom:4px; display: none" href="#day-8" role="tab" data-toggle="tab">Dia 8</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia9" style="width:240px; margin-bottom:4px; display: none" href="#day-9" role="tab" data-toggle="tab">Dia 9</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia10" style="width:240px; margin-bottom:4px; display: none" href="#day-10" role="tab" data-toggle="tab">Dia 10</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia11" style="width:240px; margin-bottom:4px; display: none" href="#day-11" role="tab" data-toggle="tab">Dia 11</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia12" style="width:240px; margin-bottom:4px; display: none" href="#day-12" role="tab" data-toggle="tab">Dia 12</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia13" style="width:240px; margin-bottom:4px; display: none" href="#day-13" role="tab" data-toggle="tab">Dia 13</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia14" style="width:240px; margin-bottom:4px; display: none" href="#day-14" role="tab" data-toggle="tab">Dia 14</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia15" style="width:240px; margin-bottom:4px; display: none" href="#day-15" role="tab" data-toggle="tab">Dia 15</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia16" style="width:240px; margin-bottom:4px; display: none" href="#day-16" role="tab" data-toggle="tab">Dia 16</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia17" style="width:240px; margin-bottom:4px; display: none" href="#day-17" role="tab" data-toggle="tab">Dia 17</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia18" style="width:240px; margin-bottom:4px; display: none" href="#day-18" role="tab" data-toggle="tab">Dia 18</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia19" style="width:240px; margin-bottom:4px; display: none" href="#day-19" role="tab" data-toggle="tab">Dia 19</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia20" style="width:240px; margin-bottom:4px; display: none" href="#day-20" role="tab" data-toggle="tab">Dia 20</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia21" style="width:240px; margin-bottom:4px; display: none" href="#day-21" role="tab" data-toggle="tab">Dia 21</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia22" style="width:240px; margin-bottom:4px; display: none" href="#day-22" role="tab" data-toggle="tab">Dia 22</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia23" style="width:240px; margin-bottom:4px; display: none" href="#day-23" role="tab" data-toggle="tab">Dia 23</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia24" style="width:240px; margin-bottom:4px; display: none" href="#day-24" role="tab" data-toggle="tab">Dia 24</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia25" style="width:240px; margin-bottom:4px; display: none" href="#day-25" role="tab" data-toggle="tab">Dia 25</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia26" style="width:240px; margin-bottom:4px; display: none" href="#day-26" role="tab" data-toggle="tab">Dia 26</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia27" style="width:240px; margin-bottom:4px; display: none" href="#day-27" role="tab" data-toggle="tab">Dia 27</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia28" style="width:240px; margin-bottom:4px; display: none" href="#day-28" role="tab" data-toggle="tab">Dia 28</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia29" style="width:240px; margin-bottom:4px; display: none" href="#day-29" role="tab" data-toggle="tab">Dia 29</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="dia30" style="width:240px; margin-bottom:4px; display: none" href="#day-30" role="tab" data-toggle="tab">Dia 30</a>
                                        </li>--%>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="p-3 py-5">
                                    <div runat="server" id="divDias" class="tab-content row justify-content-center">                                        
                                        <div role="tabpanel" class="col-lg-9 tab-pane fade show active" id="day-1">                                
                                            <div class="row schedule-item">
                                                <div class="col-md-2">
                                                    <input runat="server" id="timeD1P1" type="time" style="background-color: transparent; border-color: transparent; color: white;" />
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="speaker">
                                                        <asp:Image runat="server" ID="imgD1P1" CssClass="rounded-circle" />
                                                    </div>                                                    
                                                    <h4 runat="server" id="h4D1P1"></h4>
                                                    <asp:DropDownList runat="server" ID="ddlD1P1" DataSourceID="DsPalestra1" AutoPostBack="true" OnSelectedIndexChanged="ddlD1P1_SelectedIndexChanged" DataTextField="PalestraTitulo" DataValueField="PalestraTitulo" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DsPalestra1" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT PalestraTitulo FROM Palestras ORDER BY PalestraTitulo ASC"></asp:SqlDataSource>
                                                </div>
                                            </div>                                    
                                        </div>                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="col-md-4">
            <asp:Label runat="server" ID="label" ForeColor="White" Text="LABEL"></asp:Label>
        </div>
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
