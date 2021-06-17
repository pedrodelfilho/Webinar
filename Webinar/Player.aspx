<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="Webinar.Player" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reproduzindo vídeo</title>
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
    
</head>
    
<body onbeforeunload="ConfirmClose()" onunload="HandleOnClose()">
    <form id="form1" runat="server">
        <center>
            <div runat="server" id="div1" style="background: rgba(25, 31, 32, 0.8);"><br /><br />
                <label ID="lblTimer" runat="server"></label>
                <iframe runat="server" id="idReprodutor" width="1024" 
                        height="768" title="YouTube video player"
                        src="http://www.youtube.com/embed/fxCEcPxUbYA" 
                        frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        allowfullscreen></iframe>
                <br /><br />
                <asp:Button runat="server" ID="btnFechar" OnClick="btnFechar_Click" CssClass="botao" Text="Voltar para Página Inicial" />
                <br /><br /><br /><br /><br /><br /><br />
            </div>
        </center>
    </form>
</body>    
</html>
