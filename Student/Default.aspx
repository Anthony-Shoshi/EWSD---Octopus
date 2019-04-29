<%@ Page Title="" Language="C#" MasterPageFile="~/Student/StudentMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Student_Default" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        ul.countdown {
            list-style: none;
            padding: 0;
            display: block;
            text-align: center;
        }

            ul.countdown li {
                display: inline-block;
            }

                ul.countdown li span {
                    font-size: 80px;
                    font-weight: 300;
                    line-height: 80px;
                }

                ul.countdown li.seperator {
                    font-size: 80px;
                    line-height: 70px;
                    vertical-align: top;
                }

                ul.countdown li p {
                    color: #a7abb1;
                    font-size: 14px;
                }
    </style>

    <script>
        //preview_image function js
        function preview_image(event) {
            var reader = new FileReader();
            reader.onload = function () {
                var output = document.getElementById('ContentPlaceHolder1_Image1');
                output.src = reader.result;
            }
            reader.readAsDataURL(event.target.files[0]);
        }//End of preview_image function js
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="alert alert-danger" id="divAlertCountdown" runat="server" visible="false">
                <div id="divCountdown" runat="server" style="text-align: center;"></div>
                <ul class="countdown">
                    <li><span class="days">00</span>
                        <p class="days_ref">days</p>
                    </li>
                    <li class="seperator">.</li>
                    <li><span class="hours">00</span>
                        <p class="hours_ref">hours</p>
                    </li>
                    <li class="seperator">:</li>
                    <li><span class="minutes">00</span>
                        <p class="minutes_ref">minutes</p>
                    </li>
                    <li class="seperator">:</li>
                    <li><span class="seconds">00</span>
                        <p class="seconds_ref">seconds</p>
                    </li>
                </ul>

            </div>
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
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:GridView CssClass="table table-hover mb-none" ID="GridView1" OnRowDataBound="GridView1_RowDataBound"
                                OnRowCommand="GridView1_OnRowCommand" OnRowUpdating="GridView1_OnRowUpdating"
                                GridLines="None" border="0" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                                    <asp:BoundField DataField="fileType" HeaderText="fileType" SortExpression="fileType" />
                                    <asp:BoundField DataField="filename" HeaderText="filename" SortExpression="filename" />
                                    <asp:TemplateField HeaderText="Date Created">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateCreated" runat="server" Text='<%#Eval("dateCreated") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IsSent" HeaderText="Approved" SortExpression="IsSent" />
                                    <asp:TemplateField HeaderText="Comment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton Visible="False" runat="server" CommandName="update" ID="btnEdit" CommandArgument='<%# Container.DataItemIndex %>' Text="Edit" CssClass="btn btn-warning btn-xs" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server"><%#Eval("StudentId") %></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Literal ID="Literal1" Visible="False" runat="server"></asp:Literal>
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

                        <h2 class="panel-title">New Task Info</h2>
                    </header>
                    <div class="panel-body">
                        <div class="table-responsive">



                            <div>
                                <asp:GridView CssClass="table table-hover mb-none" GridLines="None" border="0" ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId"
                                    DataSourceID="SqlDataSource3" EmptyDataText="There are no data records to display.">
                                    <Columns>
                                        <asp:BoundField DataField="FieldName" HeaderText="Title" SortExpression="FieldName" />
                                        <asp:BoundField DataField="date" HeaderText="Create Date" SortExpression="date" />
                                        <asp:TemplateField HeaderText="Closure Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCloseDate" runat="server" Text='<%#Eval("ClosureDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Closure Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfinalDate" runat="server" Text='<%#Eval("FinalClosuredate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>"
                                    ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>"
                                    SelectCommand="SELECT [TaskId], [FieldName], [Flag], [date], [ClosureDate], [FinalClosureDate]  FROM [ChordTaskPost] Where [Flag] = 1"></asp:SqlDataSource>
                                <asp:HiddenField runat="server" ID="hdnId" Value="0" />
                                <div class="alert alert-success" id="divtermsAndConditions" runat="server">
                                    <strong>Terms And Conditions</strong>
                                    <br />
                                    <asp:CheckBox ID="chkAgree" AutoPostBack="true" runat="server" OnCheckedChanged="chkAgree_CheckedChanged" />
                                    I Agree with all the terms and conditions.
                                </div>
                            </div>

                            <div id="divUpload" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="col-md-3 control-label" for="">Coordinator</label>
                                    <div class="col-md-6">

                                        <asp:DropDownList ID="ddlCoord" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label" for="">File Type</label>
                                    <div class="col-md-6">

                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem Value="Document"></asp:ListItem>
                                            <asp:ListItem Value="Image"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label" for="">File</label>
                                    <div class="col-md-6">

                                        <asp:Image ID="Image1" runat="server" Width="100px" />
                                        <br />
                                        <asp:Label ID="lblPhoto" runat="server"></asp:Label>
                                        <br />
                                        <asp:FileUpload ID="FileUpload1" runat="server" onChange="preview_image(event);" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnInsert" runat="server" Text="Upload" OnClick="btnInsert_Click" CssClass="btn btn-success" />
                                    <asp:Button Visible="False" ID="btnDelete" runat="server" Text="Update" OnClick="btnDelete_Click" CssClass="btn btn-info" />
                                </div>

                            </div>


                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="Flag" HeaderText="Flag" ReadOnly="True" SortExpression="Flag" />
                                </Columns>
                            </asp:GridView>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>" ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>" SelectCommand="SELECT [Flag] FROM [ChordTaskPost] WHERE [Flag] = 1"></asp:SqlDataSource>

                        </div>
                    </div>
                </section>
            </div>
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnduration" Value="0" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnInsert" />
            <asp:PostBackTrigger ControlID="chkAgree" />
            <asp:PostBackTrigger ControlID="GridView1" />
            <asp:PostBackTrigger ControlID="btnDelete" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="assets/javascripts/jquery1.9.1.js"></script>
    <script src="assets/javascripts/jquery.downCount.js"></script>
    <script src="assets/javascripts/moment.js"></script>
    <script class="source" type="text/javascript">
        var _date = $('#hdnduration').val();
        var CreateDate = moment(_date, "DD/MM/YYYY").format("DD-MM-YYYY");
        $('.countdown').downCount({
            date: moment(_date, "DD/MM/YYYY HH:mm:ss a").format("MM-DD-YYYY HH:mm:ss"),
            offset: +6
        }, function () {
            //alert('WOOT WOOT, done!');
        });
    </script>
</asp:Content>

