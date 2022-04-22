<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardInfo.aspx.cs" Inherits="WebApplication1.CardInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="CardInfoForm" runat="server">
        <div>
            <asp:TextBox ID="Amount" placeholder="Amount" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="OnSubmitClicked"/>
        </div>
    </form>
</body>
</html>
