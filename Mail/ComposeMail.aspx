<%@ Page Title="" Language="C#" MasterPageFile="~/Mail/MailMasterPage.master" AutoEventWireup="true" CodeFile="ComposeMail.aspx.cs" Inherits="Mail_ComposeMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="inner-body" style="border-top-width: 165px;">
        <div class="inner-toolbar clearfix">
            <ul>
                <li>
                    <asp:LinkButton OnClick="btnSend_Click" ID="btnSend" runat="server"><i class="fa fa-send-o mr-sm"></i>Send</asp:LinkButton>
                </li>
            </ul>
        </div>
        <div class="mailbox-compose">
            <div class="form-horizontal form-bordered form-bordered">

                <div class="form-group form-group-invisible">
                    <label for="to" class="control-label-invisible">To:</label>
                    <div class="col-sm-offset-2 col-sm-9 col-md-offset-1 col-md-10">
                        <input runat="server" id="to" type="text" class="form-control form-control-invisible" data-role="tagsinput" data-tag-class="label label-primary" value="">
                    </div>
                </div>
                <div class="form-group form-group-invisible">
                    <label for="subject" class="control-label-invisible">Subject:</label>
                    <div class="col-sm-offset-2 col-sm-9 col-md-offset-1 col-md-10">
                        <input runat="server" id="subject" type="text" class="form-control form-control-invisible" value="">
                    </div>
                </div>

                <div class="form-group">
                    <div class="compose">
                        <div id="compose-field" class="compose-control">
                            <asp:TextBox ID="txtMailBody" runat ="server" CssClass="form-control" Columns="20" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

