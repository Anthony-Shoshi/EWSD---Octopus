<%@ Page Title="" Language="C#" MasterPageFile="~/Guest/GuestMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Guest_Default" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-6">
                <section class="panel">
                    <header class="panel-heading">
                        <h2 class="panel-title">Statistics By Number</h2>
                    </header>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label col-md-6">Select Year</asp:Label>
                            <div class="col-md-6">
                                <asp:DropDownList OnSelectedIndexChanged="ddlYear2_SelectedIndexChanged" AutoPostBack="true" ID="ddlYear2" runat="server" CssClass="form-control col-md-3"></asp:DropDownList>

                            </div>
                        </div>

                        <ajaxToolkit:PieChart ID="contributorNumbers" ChartHeight="300" ChartWidth="340" ChartTitleColor="#0E426C" runat="server"></ajaxToolkit:PieChart>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="ddlYear2"/>
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-6">
                <section class="panel">
                    <header class="panel-heading">
                        <h2 class="panel-title">Statistics By Percentage (%)</h2>
                    </header>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label col-md-6">Select Year</asp:Label>
                            <div class="col-md-6">
                                <asp:DropDownList OnSelectedIndexChanged="ddlyear3_SelectedIndexChanged" AutoPostBack="true" ID="ddlyear3" runat="server" CssClass="form-control col-md-3"></asp:DropDownList>

                            </div>
                        </div>

                        <ajaxToolkit:PieChart ID="chartpercentage" ChartHeight="300" ChartWidth="340" runat="server"></ajaxToolkit:PieChart>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="ddlyear3"/>

        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-6">
        <section class="panel">
            <header class="panel-heading">
                <h2 class="panel-title">Exception Reports</h2>
            </header>
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="control-label col-md-6">Select Year</asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true" ID="ddlYear" runat="server" CssClass="form-control col-md-3"></asp:DropDownList>

                    </div>
                </div>

                <ajaxToolkit:PieChart ID="chartWithoutcomment" ChartHeight="300" ChartWidth="340" runat="server"></ajaxToolkit:PieChart>
            </div>
        </section>
    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="ddlYear"/>
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>

