<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        bool showTriggered = false;
        if (ViewBag.showTriggered != null)
            showTriggered = ViewBag.showTriggered;
        %>

    <!-- page content -->
    <div class="container-fluid">
        <%:Html.Partial("Header") %>
        <!-- Event List View -->
        <div class="rule-card_container w-100" id="hook-three">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="col-md-6 col-6" style="width: 50%;">
                        <%: Html.TranslateTag("Events/History|Rule")%> <%: Html.TranslateTag("Events/History|History")%> <%--<span id="historyLabel" style="cursor:pointer;" onclick="showActionHistory();"><%: Html.TranslateTag("Events/History|History")%></span><span style="vertical-align:central;"> | </span><span id="activeLabel" style="cursor:pointer;" onclick="showActiveActions();"><%: Html.TranslateTag("Events/History|Triggered")%></span>--%>
                    </div>
                    <div id="datePickDiv" class="col-6 d-flex justify-content-end">
                        <%Html.RenderPartial("MobiDateRange");%>
                        <a onClick="exportHistory()" title="<%: Html.TranslateTag("Export","Export")%>" class="ms-2 helpIco" style="cursor: pointer; float: right;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18.5" viewBox="0 0 18 18.5">
                                <g id="Group_910" data-name="Group 910" transform="translate(-425 -830)">
                                    <path id="iconmonstr-upload-18" d="M14.609,6.158A5.547,5.547,0,0,0,9,1,5.547,5.547,0,0,0,3.391,6.158,4.036,4.036,0,0,0,0,10.091a4.064,4.064,0,0,0,4.125,4H6.6a8.741,8.741,0,0,1-.433-1.455H4.125A2.589,2.589,0,0,1,1.5,10.091C1.5,8.057,3.359,7.3,4.825,7.385,4.7,4.318,6.481,2.455,9,2.455c2.59,0,4.418,2.034,4.175,4.931,1.309-.033,3.325.546,3.325,2.705a2.589,2.589,0,0,1-2.625,2.545H11.986a4.853,4.853,0,0,0,.374,1.455h1.514a4.064,4.064,0,0,0,4.125-4A4.036,4.036,0,0,0,14.609,6.158Z" transform="translate(425 829)" fill="#707070" />
                                    <path id="ic_trending_flat_24px" d="M22,12,18,8v3H13.626v2H18v3Z" transform="translate(446.301 826.5) rotate(90)" fill="#707070" />
                                </g>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
            <div class="x_content" id="eventHistory">
                <%--<%=Html.Partial("EventHistoryList")%>--%>
            </div>
        </div>
    </div>

    <!-- Help Button Modal -->
    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Help","Help")%></h4>

                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                       
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                
                </div>
            </div>
        </div>
    </div>
    <!-- End help button -->
    <!-- page content -->
    <!-- Custom Theme Scripts -->
    <script src="/Scripts/events.js"></script>

    <script type="text/javascript">
        //$('#activeLabel').removeClass("labelSelectedText");
        //$('#activeLabel').addClass("labelUnSelectedText");
        //$('#historyLabel').removeClass("labelUnSelectedText");
        //$('#historyLabel').addClass("labelSelectedText");
        let mobiDataDestElem = '#eventHistory';
    <%--    let mobiDataPayload = { id: <%=Model.NotificationID%> , showTriggered: '<%=showTriggered%>' };--%>
        let mobiDataPayload = { id: <%=Model.NotificationID%> , showTriggered: false};
        let mobiDataController = '/Rule/RuleHistoryRefresh';

        function exportHistory() {
            disableUnsavedChangesAlert();
            window.location = '/Export/ExportNotificationData/' + '<%=Model.NotificationID%>';
            if ((datePickInst.getTempVal()[1] - datePickInst.getTempVal()[0]) / (1000 * 60 * 60 * 24) > 7) {
                showAlertModal('<%: Html.TranslateTag("Events/HistoryExport|Only 7 days of data can be exported in one request!") %>');
            }
        }

 <%--       function showActiveActions() {
            $('#datePickDiv').removeClass("showMobi");
            $('#datePickDiv').addClass("hideMobi");
            $('#historyLabel').removeClass("labelSelectedText");
            $('#historyLabel').addClass("labelUnSelectedText");
            $('#activeLabel').removeClass("labelUnSelectedText");
            $('#activeLabel').addClass("labelSelectedText");

            mobiDataPayload = { id: <%=Model.NotificationID%> , showTriggered: true };
            mobiDataRefresh();
        }--%>


      <%--  function showActionHistory() {

            $('#datePickDiv').addClass("showMobi");
            $('#datePickDiv').removeClass("hideMobi");
            $('#activeLabel').removeClass("labelSelectedText");
            $('#activeLabel').addClass("labelUnSelectedText");
            $('#historyLabel').removeClass("labelUnSelectedText");
            $('#historyLabel').addClass("labelSelectedText");

            mobiDataPayload = { id: <%=Model.NotificationID%> , showTriggered: false };
            mobiDataRefresh();
        }--%>

    </script>

        <style>

            .hideMobi {
                visibility:hidden;
            }

                 .showMobi {
                visibility:visible;
            }

        .labelSelectedText {
            font-size:17px;
            text-decoration:underline;
        }

          .labelUnSelectedText {
            font-size:15px;
            text-decoration:none;
        }

    </style>

</asp:Content>
