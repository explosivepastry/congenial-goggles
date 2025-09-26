<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<% List<ReportSchedule> report = ReportSchedule.LoadByCustomerID(Model.CustomerID);%>


<% if (report.Count > 0)
    { %>
<div class="sensorEditForm row">

    <div class="col-12 col-md-3" style="font-weight:600;">
        <%:Html.TranslateTag("Settings/UserNotification|Reports", "Reports")%>
    </div>
    <div class="col sensorEditFormInput">
        <%:Html.TranslateTag("Settings/UserNotification|User Assigned to: ", "User Assigned to: ")%> <a href="#reportModal" data-bs-toggle="modal"><b><span onclick="$('#reportModal').modal('toggle');" style="cursor: pointer;" class="reportCount"><%:report.Count%> Reports</span></b></a>
        <span id="ReportsAssignedFormat" style="color: #aaa; font-size: .8em;"></span>
    </div>
</div>
<%}%>

<div class="modal fade " id="reportModal">
    <div class="modal-dialog modal-dialog-scrollable">
        <div class="modal-content modal_container">

            <div class="modal-header">
                <div class="modal-user msg-user-icon"><%=Html.GetThemedSVG("accountDetails") %></div>
                <div class="modal-select">
                    <h4 style="font-size: 18px;" class="modal-title-select"><%= Html.TranslateTag("Reports Assigned to:")%> <%:Model.FullName%></h4>
                </div>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>

            </div>
            <hr style="margin: 0 24px;" />
            <h4 class="modal-delay-title"></h4>
            <div class="modal-body modal-dialog-scrollable">
                <div class="modal-dialog-scrollable">
                    <% foreach (ReportSchedule item in report)
                        {
                            List<ReportDistribution> rr = ReportDistribution.LoadBySchedule(item.ReportScheduleID);

                            foreach (ReportDistribution recip in rr.Where(x => x.CustomerID == Model.CustomerID))
                            {
                    %>
                    <div style="justify-content: space-around;" class="msg-list">
                        <div id="reportWrapper_<%:item.ReportScheduleID %>" data-reportwrapperid="<%:item.ReportScheduleID %><%:recip.CustomerID %>" class="True btn-lg cardStyle">
                            <div class="svgIcon"><%: Html.GetThemedSVG("book") %></div>
                            <a title="<%:Html.TranslateTag("View Reports History") %>" href="/Export/ReportHistory/<%:item.ReportScheduleID %>" style="width: 150px;"><%:item.Name %></a>

                            <%if (MonnitSession.CustomerCan("Customer_Edit_Self") || MonnitSession.CustomerCan("Customer_Edit_Other"))
                                {%>
                            <div title="<%:Html.TranslateTag("Add/Remove Self from Report Recipient list") %>" style="cursor: pointer" data-reportscheduleid="<%:item.ReportScheduleID%>" onclick="saveReportRecipients(this);" id="checkmark"><%: Html.GetThemedSVG("check-circle") %></div>
                            <% }%>
                        </div>
                    </div>
                    <%}
                        }%>
                </div>
            </div>
        </div>
    </div>
</div>

<script>

    //Modal
    $('#reportModal').on('shown.bs.modal')

    var successString = '<%=Html.TranslateTag("Success")%>';
    //when modal opens
    $('#reportModal').on('shown.bs.modal', function (e) {
        $(".msg_container").css({ opacity: 1 });
    })

    //when modal closes
    $('#reportModal').on('hidden.bs.modal', function (e) {
        $(".msg_container").css({ opacity: 1 });
    })

    function saveReportRecipients(e) {
        const customerID = "<%:Model.CustomerID%>";
        const reportscheduleID = $(e).data("reportscheduleid");
        const removed = '<%: Html.TranslateTag("User removed from report") %>';
        const added = '<%: Html.TranslateTag("User added to report") %>';

        const reportWrapperSelector = $('[data-reportwrapperid="' + reportscheduleID + customerID + '"]');
        const addRecip = !$(reportWrapperSelector).hasClass('True');

        const postData = { "customerID": customerID, "add": addRecip };

        $.post('/Export/ToggleReportRecipient/' + reportscheduleID, postData, function (data) {
            if (data == "Success") {
                if (!addRecip) {
                    $(reportWrapperSelector).toggleClass('True');
                    toastBuilder(removed, "Success");

                }
                else {
                    $(reportWrapperSelector).toggleClass('True');
                    toastBuilder(added, "Success");
                }
            }
        });
    }



</script>
