<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OPSPrePayMobile._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="CheckResponse.aspx">
    <div>
        <asp:Label ID="Label1" runat="server" Text="DS_DATE:"></asp:Label>
        <asp:TextBox ID="DS_DATE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="DS_HOUR:"></asp:Label>
        <asp:TextBox ID="DS_HOUR" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="DS_AMOUNT:"></asp:Label>
        <asp:TextBox ID="DS_AMOUNT" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="DS_CURRENCY:"></asp:Label>
        <asp:TextBox ID="DS_CURRENCY" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="DS_ORDER:"></asp:Label>
        <asp:TextBox ID="DS_ORDER" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="DS_MERCHANTCODE:"></asp:Label>
        <asp:TextBox ID="DS_MERCHANTCODE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="DS_TERMINAL:"></asp:Label>
        <asp:TextBox ID="DS_TERMINAL" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" Text="DS_SIGNATURE:"></asp:Label>
        <asp:TextBox ID="DS_SIGNATURE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label9" runat="server" Text="DS_RESPONSE:"></asp:Label>
        <asp:TextBox ID="DS_RESPONSE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label10" runat="server" Text="DS_MERCHANTDATA:"></asp:Label>
        <asp:TextBox ID="DS_MERCHANTDATA" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label11" runat="server" Text="DS_SECUREPAYMENT:"></asp:Label>
        <asp:TextBox ID="DS_SECUREPAYMENT" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label12" runat="server" Text="DS_TRANSACTIONTYPE:"></asp:Label>
        <asp:TextBox ID="DS_TRANSACTIONTYPE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label13" runat="server" Text="DS_CARD_COUNTRY:"></asp:Label>
        <asp:TextBox ID="DS_CARD_COUNTRY" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label14" runat="server" Text="DS_AUTHORISATIONCODE:"></asp:Label>
        <asp:TextBox ID="DS_AUTHORISATIONCODE" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label15" runat="server" Text="DS_CONSUMERLANGUAGE:"></asp:Label>
        <asp:TextBox ID="DS_CONSUMERLANGUAGE" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button"  />
    </div>
    </form>
</body>
</html>
