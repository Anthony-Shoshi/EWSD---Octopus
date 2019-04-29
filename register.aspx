<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
    </p>

    <asp:Label ID="Label6" runat="server" Text="Registration Form"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <asp:Label runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Register As a"></asp:Label>
&nbsp;<asp:DropDownList ID="ddRole" runat="server">
        <asp:ListItem Value="Admin">Admin</asp:ListItem>
        <asp:ListItem>Marketing Manager</asp:ListItem>
        <asp:ListItem>Marketing Coordinator</asp:ListItem>
        <asp:ListItem>Faculty</asp:ListItem>
        <asp:ListItem>Student</asp:ListItem>
    </asp:DropDownList>
    <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Select Department"></asp:Label>
        <asp:DropDownList ID="ddDepartment" runat="server">
            <asp:ListItem>Information Technology</asp:ListItem>
            <asp:ListItem>Mathematics</asp:ListItem>
            <asp:ListItem>History</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Email Address"></asp:Label>
        <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Phone Number"></asp:Label>
        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
        <br />
        <br />
        Password<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnGetStarted_Click" />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        <br />
        <br />
 
</asp:Content>

