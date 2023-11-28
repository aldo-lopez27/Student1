<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministratorPage.aspx.cs" Inherits="Assignment4.AdministratorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>
    <style type="text/css">
        .gridContainer {
            display: flex;
        }

        .gridView {
            flex: 1;
            margin-right: 10px; /* Add margin between grid views as needed */
        }

        .gridView:last-child {
            margin-right: 0; /* Remove margin from the last grid view */
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="gridContainer">
            <asp:GridView runat="server" AutoGenerateColumns="true" ID="gridViewMemberAdmin" CssClass="GridView" AlternatingRowStyle-CssClass></asp:GridView>
            <br />
            <asp:GridView runat="server" AutoGenerateColumns="true" ID="gridViewInstructorAdmin" CssClass="GridView" AlternatingRowStyle-CssClass></asp:GridView>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            &nbsp;
            &nbsp;<br />
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlSections" runat="server" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged" style="margin-left: 0px">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="assignMemberToSecionBtn" runat="server" OnClick="assignMemberToSecionBtn_Click" Text="Assign Member to Section" Width="233px" />
            <br />
            <br />
            <asp:Label ID="firstNaneLbl" runat="server" Text="First Name"></asp:Label>
            <asp:TextBox ID="firstNameTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lastNameLbl" runat="server" Text="Last Name"></asp:Label>
            <asp:TextBox ID="lastNameTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="phoneNumberLbl" runat="server" Text="Phone Number"></asp:Label>
            <asp:TextBox ID="phoneNumberTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="studentDateLbl" runat="server" Text="Member Join Date"></asp:Label>
            <asp:TextBox ID="memberJoinDateTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="studentEmail" runat="server" Text="Student Email"></asp:Label>
            <asp:TextBox ID="studentEmailTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />
            <asp:Button ID="addMemberInstructor" runat="server" OnClick="addMemberInstructor_Click" Text="Add Member" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="addInstructorButton" runat="server" OnClick="addInstructorButton_Click" Text="Add Instructor" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="assignSectionButton" runat="server" OnClick="assignSectionButton_Click" Text="Assign Section" />
            <br />
            <br />
            <asp:Button ID="deleteMemberBtn" runat="server" OnClick="Button1_Click" Text="Delete Member" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="deleteInustructorBtn" runat="server" OnClick="Button2_Click" Text="Delete Instructor" />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
