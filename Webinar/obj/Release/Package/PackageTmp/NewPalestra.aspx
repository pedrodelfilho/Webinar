<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewPalestra.aspx.cs" Inherits="Webinar.NewPalestra" %>
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
<div style="background: rgba(25, 31, 32, 0.8);" id="intro-container"><br /><br /><br />
        <div class="row">
                <div class="col-md-3">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <asp:Image runat="server" ID="imgPalestra" width="300px" />                      
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <h4 style="color:red;"><span class="fa fa-video-camera"></span> Adicionar Palestra</h4>
                        </div>
                        <asp:Label runat="server" Font-Size="8" style="color:red;">Visando a garantia de privacidade dos dados e buscando entregar a melhor interatividade ao usuário, toda e qualquer criação e alteração será colocada em espera de aprovação por algum Moderador ou Administrador do sistema.</asp:Label><br />
                        <asp:Label runat="server" Font-Size="8" style="color:yellow">Caro Palestrante, antes de iniciar a criação de uma Palestra, tenha todos os campos do seu perfil preenchido, inclusive a foto.</asp:Label>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Título</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTituloPalestra" placeholder="Digite o título" />
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTituloPalestra" ErrorMessage="Título é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ForeColor="Red" Display="Dynamic" ControlToValidate="txtTituloPalestra" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Limite de 100 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>        
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Subtítulo</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSubTituloPalestra" placeholder="Digite o subtítulo" />
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtSubTituloPalestra" ErrorMessage="Subtítulo é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSubTituloPalestra" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="Limite de 100 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>     
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Link</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtLinkPalestra" placeholder="Digite o link" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLinkPalestra" ErrorMessage="Link é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ForeColor="Red" Display="Dynamic" ControlToValidate="txtLinkPalestra" ValidationExpression="^[\s\S]{0,200}$" ErrorMessage="Limite de 200 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Categoria</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" Width="100%" CssClass="form-control" DataSourceID="DsCategoria"  DataTextField="DsCurso" DataValueField="DsCurso" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="DsCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT DsCurso FROM Cursos ORDER BY DsCurso ASC"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategoria" ErrorMessage="Categoria é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>   
                        <div class="row mt-2">                            
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 1º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP1Palestra" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSinopseP1Palestra" ErrorMessage="Sinopse 1 é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP1Palestra" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 2º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP2Palestra" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP2Palestra" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 3º Parágrafo</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP3Palestra" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox>  
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator6" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP3Palestra" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Sinopse 4º Parágrafo</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine" ID="txtSinopseP4Palestra" Width="100%" MaxLength="600" placeholder="Digite algo sobre" Rows="5"></asp:TextBox> 
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator7" ForeColor="Red" Display="Dynamic" ControlToValidate="txtSinopseP4Palestra" ValidationExpression="^[\s\S]{0,600}$" ErrorMessage="Limite de 600 caracteres excedido."></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-12">
                                <label style="color: white" class="labels">Palestrante</label>
                                <asp:DropDownList ID="ddlPalestrantes" runat="server" Width="100%" CssClass="form-control" DataSourceID="Dspalestrante"  DataTextField="Username" DataValueField="Username" AppendDataBoundItems="true">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="dsPalestrante" runat="server" ConnectionString="<%$ ConnectionStrings:AggregateBD %>" SelectCommand="SELECT DISTINCT p.Username FROM Palestrantes JOIN Users p on Palestrantes.IDPalestrante = p.UserId"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPalestrantes" ErrorMessage="Palestrante é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label style="color: white" class="labels">Tempo estimado da palestra</label>
                                <asp:TextBox runat="server"  CssClass="form-control" TextMode="Time" ID="txtTempoPalestra" Width="100%" MaxLength="600" placeholder=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTempoPalestra" ErrorMessage="Duração é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label style="color: white" class="labels">Data da apresentação</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="Date" Width="200px" ID="txtDataPalestra"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDataPalestra" ErrorMessage="Data é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>                        
                        <div class="row mt-2">
                            <div style="margin-left: 15px">
                                <asp:CheckBox runat="server" ID="cbAutorizarPalestra" style="color: white" Text="Autorizo a utilização e publicação deste conteúdo" /><asp:Label ID="lblcb" runat="server" ForeColor="Red" Text="&nbsp;Precisamos de sua autorização" Font-Size="16px" Visible="false"></asp:Label>
                            </div>                            
                        </div>
                        <label style="color: white; font-size:medium;" class="labels">Foto de capa:</label>
                            <asp:FileUpload ID="fuImageCapa" runat="server" style="color: white; font-size:small"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="fuImageCapa" Display="Dynamic" ErrorMessage="Foto é obrigatório." ForeColor="Red"></asp:RequiredFieldValidator>
                        <div class="mt-5 text-center">
                            <asp:LinkButton ID="btnSalvarPalestra" OnClick="btnSalvarPalestra_Click" runat="server" Width="200" CssClass="botaosub"><i class="fa fa-save"></i>&nbsp;Salvar</asp:LinkButton><br />
                            <asp:LinkButton Visible="false" ID="btnAtualizarPalestra" OnClick="btnAtualizarPalestra_Click" runat="server" CssClass="botaosub"><i class="fa fa-save"></i>&nbsp;Atualizar</asp:LinkButton><br />
                            <asp:Label Font-Size="X-Small" runat="server" ForeColor="Red">Antes de salvar, visualize a criação dá página no botão "Visualizar"</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3 py-5">
                        <br /><br /><br /><br /><br /><br /><br /><br />
                        
                        <div class="d-flex flex-row mt-3 exp-container">
                            <asp:LinkButton ID="btnVisualizar" OnClick="btnVisualizar_Click" runat="server" Width="180" CssClass="botao"><i class="fa fa-eye"></i>&nbsp;Visualizar</asp:LinkButton>
                        </div>
                        <asp:Label Font-Size="X-Small" runat="server" ForeColor="Red">Visualizar design real da página. Essa opção<br />não salvará a criação, apenas demonstrará<br />o resultado final.</asp:Label>  
                    </div>
                </div>                                
            </div>
    </div>
    <script>
        $("#<%=fuImageCapa.ClientID%>").on('change', function () {
            if (this.files[0].type.indexOf("image") > -1) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPalestra.ClientID%>').attr('src', e.target.result);
        }
        reader.readAsDataURL(this.files[0]);
    }
    else {                
        $('#<%=imgPalestra.ClientID%>').attr('src', '');
            alert('Não é uma imagem válida')
        }
    });
    </script>
</asp:Content>
