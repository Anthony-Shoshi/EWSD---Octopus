<%@ Page Title="" Language="C#" MasterPageFile="~/Presentation/MasterPage.master" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="Presentation_frmLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Label ID="lblLogin" runat="server" Visible="False"></asp:Label>
    <section class="body-sign">
        <div class="center-sign">
            <div class="panel panel-sign">
                <div class="panel-title-sign mt-xl text-right">
                    <h2 class="title text-uppercase text-bold m-none"><i class="fa fa-user mr-xs"></i>Sign In</h2>
                </div>
                <div class="panel-body">
                    <div class="form-group mb-lg">
                        <label>Email</label>
                        <div class="input-group input-group-icon">
                            <asp:TextBox ID="txtEmailAddress" required oninvalid="this.setCustomValidity('Please Enter email')" oninput="setCustomValidity('')" CssClass="form-control input-lg" runat="server"></asp:TextBox>

                            <span class="input-group-addon">
                                <span class="icon icon-lg">
                                    <i class="fa fa-user"></i>
                                </span>
                            </span>
                        </div>
                    </div>

                    <div class="form-group mb-lg">
                        <div class="clearfix">
                            <label class="pull-left">Password</label>

                        </div>
                        <div class="input-group input-group-icon">
                            <asp:TextBox ID="txtPassword" required oninvalid="this.setCustomValidity('Please Enter password')" oninput="setCustomValidity('')" runat="server" CssClass="form-control input-lg" TextMode="Password"></asp:TextBox>

                            <span class="input-group-addon">
                                <span class="icon icon-lg">
                                    <i class="fa fa-lock"></i>
                                </span>
                            </span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4 col-sm-offset-8 text-right">
                            <asp:Button ID="btnLogin" CssClass="btn btn-primary hidden-xs" runat="server" Text="Login " OnClick="btnLogin_Click" />

                        </div>
                    </div>

                    <p class="text-center">
                        <asp:Button Visible="false" ID="btnRegistration" CssClass="btn btn-success hidden-xs" runat="server" OnClick="btnRegistration_Click" Text="Not yet registered" />

                        <asp:Button Visible="false" ID="btnBack" CssClass="btn btn-danger hidden-xs" runat="server" OnClick="btnBack_Click" Text="Back" />

                    </p>
                </div>
            </div>

        </div>
    </section>

</asp:Content>

