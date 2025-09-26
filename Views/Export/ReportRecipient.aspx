<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReportSchedule>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report Recipient
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%:Html.Partial("_ReportHeader") %>
        <div class="report-history_container">
            <div class="" id="hook-four">
                <div class="card_container__top">
                    <div class="card_container__top__title" style="gap:10px;">
                                <%=Html.GetThemedSVG("contacts") %>
                                <%: Html.TranslateTag("Export/ReportRecipient|Recipients","Recipients")%>
                            <div style="margin-left:auto;">
                                    <input type="text" id="userFilter" class="form-control user-dets" placeholder="<%: Html.TranslateTag("Export/ReportRecipient|User Name","User Name")%>" />
                            </div>
                    </div>
                </div>

                <div style="margin: 5px">
                    <font color="gray">
                        <%: Html.TranslateTag("Export/ReportRecipient|Click Recipient to enable/disable","Click Recipient to enable/disable")%>
                    </font>
                </div>

                <div class="x_content" id="reportRecipientList" style="padding: 0px;">
                <%List<ReportRecipientData> userList = ViewBag.ReportUsers; %>
<%--                    <%List<ReportRecipientData> userList = ReportRecipientData.SearchPotentialReportRecipient(MonnitSession.CurrentCustomer.CustomerID,Model.ReportScheduleID, "", Model.AccountID);%>--%>
 
                    <%=Html.Partial("ReportUserList",userList) %>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#userFilter').keyup(function (e) {
                e.preventDefault();
                loadUsers();
            });
        });

<%--        function loadUsers() {
            var query = $('#userFilter').val();
            var reportID = <%:Model.ReportScheduleID %>;
            $.post("/Export/ReportRecipientsList", { id: reportID, q: query }, function (data) {
                $('#reportRecipientList').html(data);
            });
        }--%>

        function loadUsers() {
            var query = $('#userFilter').val();
            var reportID = <%:Model.ReportScheduleID %>;
            var cust = <%:MonnitSession.CurrentCustomer.CustomerID%>
            var acc = <%:MonnitSession.CurrentCustomer.AccountID%>
                $.post("/Export/ReportRecipientsList", { customer: cust, id: reportID, q: query, accountID: acc }, function (data) {
                    $('#reportRecipientList').html(data);
                });
        }
    </script>

</asp:Content>
