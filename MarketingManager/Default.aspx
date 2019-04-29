<%@ Page Title="" Language="C#" MasterPageFile="~/MarketingManager/MarketingManagerMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Marketing_Manager_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style>
        .HiddenText label {
            display: none;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <section class="panel">
                    <header class="panel-heading">
                        <div class="panel-actions">
                            <a href="#" class="fa fa-caret-down"></a>
                            <a href="#" class="fa fa-times"></a>
                        </div>

                        <h2 class="panel-title">Student Info</h2>
                    </header>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-hover mb-none" GridLines="None" border="0"
                                runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1"
                                EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelect" />
                                            <%--  --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                                    <asp:BoundField DataField="fileType" HeaderText="fileType" SortExpression="fileType" />
                                    <asp:TemplateField HeaderText="filename" SortExpression="filename">
                                        <EditItemTemplate>
                                            <asp:Label ID="TextBox1" runat="server" Text='<%# Bind("filename") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("filename") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="dateCreated" HeaderText="dateCreated" SortExpression="dateCreated" />
                                    <asp:TemplateField HeaderText="Approved">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdndateCreated" runat="server" Value='<%# Eval("dateCreated") %>' />
                                            <asp:HiddenField ID="hdnIsSent" runat="server" Value='<%# Eval("IsSent") %>' />

                                            <asp:CheckBox ID="chkChecked" CssClass="HiddenText" Text='<%# Container.DataItemIndex %>' runat="server" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>"
                                DeleteCommand="DELETE FROM [tblFile] WHERE [Id] = @Id"
                                InsertCommand="INSERT INTO [tblFile] ([fileType], [filename], [dateCreated]) VALUES (@fileType, @filename, @dateCreated)"
                                ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>"
                                SelectCommand="SELECT [Id], [fileType], [filename], [dateCreated], [IsSent], [Comment] FROM [tblFile] where [IsSent] = 1" UpdateCommand="UPDATE [tblFile] SET [fileType] = @fileType, [filename] = @filename, [dateCreated] = @dateCreated WHERE [Id] = @Id">
                                <DeleteParameters>
                                    <asp:Parameter Name="Id" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="fileType" Type="String" />
                                    <asp:Parameter Name="filename" Type="String" />
                                    <asp:Parameter Name="dateCreated" Type="DateTime" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="fileType" Type="String" />
                                    <asp:Parameter Name="filename" Type="String" />
                                    <asp:Parameter Name="dateCreated" Type="DateTime" />
                                    <asp:Parameter Name="Id" Type="Int32" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </div>
                        <div>
                            <%--Download-buttorn--%>
                            <asp:LinkButton ID="btnDownload" runat="server" OnClick="DownloadFiles" CssClass="btn btn-success">
                                <i class="fa fa-file-archive-o"></i> Download As zip</asp:LinkButton>
                        </div>
                    </div>

                </section>
            </div>

            <div class="col-md-12">
                <section class="panel">
                    <header class="panel-heading">
                        <div class="panel-actions">
                            <a href="#" class="fa fa-caret-down"></a>
                            <a href="#" class="fa fa-times"></a>
                        </div>

                        <h2 class="panel-title">Task Info</h2>
                    </header>
                    <div class="panel-body">
                        <div class="table-responsive">

                            <asp:GridView OnRowDataBound="GridView2_RowDataBound" CssClass="table table-hover mb-none" GridLines="None" border="0" ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId"
                                DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="TaskId" HeaderText="TaskId" ReadOnly="True" SortExpression="TaskId" />
                                    <asp:BoundField DataField="FieldName" HeaderText="Title" SortExpression="FieldName" />
                                    <asp:BoundField DataField="Flag" HeaderText="Flag" SortExpression="Flag" Visible="false" />
                                    <asp:BoundField DataField="date" HeaderText="Create Date" SortExpression="date" />
                                    <asp:BoundField DataField="ClosureDate" HeaderText="Closure Date" SortExpression="ClosureDate" />
                                    <asp:BoundField DataField="FinalClosuredate" HeaderText="Final Closure Date" SortExpression="FinalClosuredate" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFlag" runat="server" Value='<%# Eval("Flag") %>' />
                                            <asp:Button ID="btnSubmit" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary btn-xs" runat="server" Text="Close Submission" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>"
                                ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>"
                                SelectCommand="SELECT [TaskId], [FieldName], [Flag], [date], [ClosureDate], [FinalClosuredate] FROM [ChordTaskPost]"
                                InsertCommand="INSERT INTO [ChordTaskPost] ([FieldName], [flag], [date]) VALUES (@FieldName, @flag, @date)">
                                <InsertParameters>
                                    <asp:Parameter Name="FieldName" Type="String" />
                                    <asp:Parameter Name="flag" Type="Boolean" />
                                    <asp:Parameter Name="date" Type="DateTime" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                        </div>
                        <div>

                            <br />
                            <br />
                            <div class="" style="display: none;">
                                <div class="form-group">
                                    <label class="col-md-3 control-label" for="inputDefault">Task Name</label>
                                    <div class="col-md-6">

                                        <asp:TextBox CssClass="form-control" ID="txtTaskName" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btnInsert" runat="server" Text="Post" CssClass="btn btn-success" />

                                    </div>
                                </div>
                            </div>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsert" />
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>
    &nbsp;
</asp:Content>
