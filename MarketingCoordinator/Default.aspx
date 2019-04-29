<%@ Page Title="" Language="C#" MasterPageFile="~/MarketingCoordinator/MarketingCoordinatorMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Marketing_Coordinator_Default" %>

<%@ MasterType VirtualPath="~/MarketingCoordinator/MarketingCoordinatorMasterPage.master" %>
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
                            <asp:GridView ID="GridView1" CssClass="table table-hover mb-none" GridLines="None" border="0"
                                OnRowDataBound="GridView1_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                                    <asp:BoundField DataField="fileType" HeaderText="fileType" SortExpression="fileType" />
                                    <asp:TemplateField HeaderText="Student">
                                        <ItemTemplate>
                                            <a href='#'><%# Eval("StudentId") %>_<%# Eval("StudentName") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="filename" SortExpression="filename">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("filename") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFile" runat="server" Text='<%# Bind("filename") %>'></asp:Label>
                                            <br />
                                            <asp:Image ID="Image1" runat="server" Width="100px" ImageUrl='<%# Eval("filename","../Images/{0}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                            <a download href='../Images/<%# Eval("filename") %>'>Download</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DC" HeaderText="dateCreated" SortExpression="DC" />
                                    <asp:TemplateField HeaderText="Approve">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdndateCreated" runat="server" Value='<%# Eval("DC") %>' />
                                            <asp:HiddenField ID="hdnIsSent" runat="server" Value='<%# Eval("IsSent") %>' />

                                            <asp:CheckBox ID="chkChecked" CssClass="HiddenText" Text='<%# Container.DataItemIndex %>' runat="server" OnCheckedChanged="chkChecked_CheckedChanged" AutoPostBack="true" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtComment" CssClass="form-control input-sm" runat="server" Text='<%# Eval("Comment") %>' />
                                            <asp:Button ID="btnComment" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary btn-xs" runat="server" OnClick="btnComment_Click" Text="Comment" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>"
                                DeleteCommand="DELETE FROM [tblFile] WHERE [Id] = @Id"
                                InsertCommand="INSERT INTO [tblFile] ([fileType], [filename], [dateCreated]) VALUES (@fileType, @filename, @dateCreated)"
                                ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>"
                                UpdateCommand="UPDATE [tblFile] SET [fileType] = @fileType, [filename] = @filename, [dateCreated] = @dateCreated WHERE [Id] = @Id">
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

                            <asp:GridView CssClass="table table-hover mb-none" GridLines="None" border="0" ID="GridView2" OnRowDataBound="GridView2_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId"
                                DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="TaskId" HeaderText="TaskId" ReadOnly="True" SortExpression="TaskId" />
                                    <asp:BoundField DataField="FieldName" HeaderText="Title" SortExpression="FieldName" />
                                    <asp:BoundField DataField="Flag" HeaderText="Flag" SortExpression="Flag" Visible="false" />
                                    <asp:BoundField DataField="date" HeaderText="Create Date" SortExpression="date" />
                                    <asp:BoundField DataField="ClosureDate" HeaderText="Closure Date" SortExpression="ClosureDate" />
                                    <asp:BoundField DataField="FinalClosuredate" HeaderText="Final Closure Date" SortExpression="FinalClosuredate" />
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFlag" runat="server" Value='<%# Eval("Flag") %>' />
                                            <asp:Button ID="btnSubmit" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary btn-xs" runat="server" OnClick="btnSubmit_Click" Text="Close Submission" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>"
                                ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>"
                                SelectCommand="SELECT [TaskId], [FieldName], [Flag], [date], [ClosureDate], [FinalClosuredate]  FROM [ChordTaskPost] where [Flag] = 1"
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
                                        <asp:Button ID="btnInsert" runat="server" Text="Post" OnClick="btnInsert_Click" CssClass="btn btn-success" />

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
            <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" />--%>
        </Triggers>
    </asp:UpdatePanel>
    &nbsp;
</asp:Content>

