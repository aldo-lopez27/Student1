<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="Assignment4.MemberPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:Label runat="server" Text="Member:" ID="ctl00"></asp:Label>
        <asp:TextBox runat="server" ID="memberLastName"></asp:TextBox>
        <asp:TextBox runat="server" ID="memberFirstName"></asp:TextBox>
        <asp:GridView runat="server" ID="gridViewMember"></asp:GridView>
        <div>
        </div>
    </form>
    
</body>
</html>
