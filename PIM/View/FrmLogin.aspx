<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="PIM.View.FrmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Iniciar Sessão</title> 
 <link href="~/Assets/css/style.css" rel="stylesheet" />

</head>
<body>
    <form id="formLogin" runat="server">
        <div id="Login">
              <div id="InternoLogin">
            <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Iniciar Sessão"></asp:Label>
       
    
                <br />
          
                <asp:TextBox CssClass="TextBox" ID="txbID_Login" Visible="false" runat="server"></asp:TextBox>
                <br />
               
                <asp:Label CssClass="Label" ID="lblDS_Usuario" runat="server" Width="100%" Text="Usuário"></asp:Label>
                <asp:TextBox CssClass="TextBox"  ID="txbDS_Usuario"  runat="server" MaxLength="20"
                     placeholder="Nome de usuário"></asp:TextBox>

                  <br />
             
                     
                <asp:Label CssClass="Label" ID="lblDS_Senha" runat="server" Width="100%" Text="Senha"></asp:Label>
                <asp:TextBox CssClass="TextBox"  ID="txbDS_Senha"  runat="server" MaxLength="20"
                     placeholder="Senha de usuário"></asp:TextBox>

                  <br />

                 <asp:Label CssClass="MSg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                     <br />
                <br />
                <asp:Button CssClass="Button" ID="btnAcessar" runat="server" Text="Acessar" OnClick="btnAcessar_Click" />
                            
            </div>
              <br />
                
        </div>
    </form>
</body>
</html>
