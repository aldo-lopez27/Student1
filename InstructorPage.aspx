<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorPage.aspx.cs" Inherits="Assignment4.InstructorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label runat="server" Text="Instructor:" ID="ctl00"></asp:Label>
        <asp:TextBox runat="server" ID="instructorLastName"></asp:TextBox>
        <asp:TextBox runat="server" ID="instructorFirstName"></asp:TextBox>
        <asp:GridView runat="server" ID="gridViewInstructor"></asp:GridView>
        <div>
        </div>
    </form>
</body>
</html>
