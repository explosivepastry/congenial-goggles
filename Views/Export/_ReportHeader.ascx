<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>

<%  ReportSchedule report = ReportSchedule.Load(Model.ReportScheduleID);
    ReportQuery query = ReportQuery.Load(Model.ReportQueryID);
    bool isFavorite = MonnitSession.IsReportFavorite(Model.ReportScheduleID);
    string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
    string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
%>

<%Html.RenderPartial("_ReportLink", Model); %>

<div class="special-grid " id="hook-one">
    <div class="report_Container " style="margin-bottom: 1rem;">
        <div class="card_container__top">
            <div class="card_container__top__title  d-flex" style="flex-wrap: nowrap; gap: 20px; cursor: pointer;">
                <%: Html.TranslateTag("Export/_ReportHeader|Report Information","Details") %>

                <div class="report-trash-heart">
                    <div>
                        <span title="<%:isFavorite ? "Unfavorite Report" : "Favorite Report"%>" id="favoriteItem" data-id="<%=Model.ReportScheduleID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                            onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'reportschedule')">
                            <%:Html.GetThemedSVG("heart-beat") %>                
                        </span>
                    </div>

                </div>
                <%if (Model.IsActive)
                    {%>
                <div>
                    <a id="reportGreen<%:Model.ReportScheduleID %>" color: green" data-id="<%:Model.ReportScheduleID %>" onclick="toggleReportStatus(<%:Model.ReportScheduleID %>);" title=" <%:Model.IsActive ? "Click to Disable" : "Click to Enable"%>">
                        <svg xmlns="http://www.w3.org/2000/svg" id="svg_disable" class="svg_icon" viewBox="0 0 15 15">
                            <path fill="#37BC9B" d="M11.333,3H9.667v8.333h1.667Zm4.025,1.808L14.175,5.992A5.767,5.767,0,0,1,16.333,10.5,5.833,5.833,0,1,1,6.817,5.983L5.642,4.808A7.493,7.493,0,1,0,18,10.5,7.444,7.444,0,0,0,15.358,4.808Z" transform="translate(-3 -3)" />
                        </svg>
                    </a>
                </div>
                <%}
                    else
                    {%>
                <div>
                    <a id="reportGrey<%:Model.ReportScheduleID %>" color: lightslategrey" data-id="<%:Model.ReportScheduleID %>" onclick="toggleReportStatus(<%:Model.ReportScheduleID %>);" title=" <%:Model.IsActive ? "Click to Disable" : "Click to Enable"%>">
                        <svg xmlns="http://www.w3.org/2000/svg" id="svg_disable" class="svg_icon" viewBox="0 0 15 15">
                            <path fill="#a4a4a47a" d="M11.333,3H9.667v8.333h1.667Zm4.025,1.808L14.175,5.992A5.767,5.767,0,0,1,16.333,10.5,5.833,5.833,0,1,1,6.817,5.983L5.642,4.808A7.493,7.493,0,1,0,18,10.5,7.444,7.444,0,0,0,15.358,4.808Z" transform="translate(-3 -3)" />
                        </svg>
                    </a>
                </div>
                <%}%>
                <div>
                    <a class="end-icons" onclick="removeReport(<%:Model.ReportScheduleID%>)" title="<%: Html.TranslateTag("Export/_ReportHeader|Delete Report","Delete Report")%>">
                        <%=Html.GetThemedSVG("delete") %>
                    </a>
                </div>
            </div>
    </div>


    <div style="margin: 5px">
        <%--            <font color="gray">
                <%: Html.TranslateTag("Export/_ReportHeader|Click on icon to enable/disable","Click on icon to enable/disable")%>
            </font>--%>
    </div>

    <div id="ruleONOFF" data-id="<%:Model.ReportScheduleID%>" class="rule-on-off" style="height: auto;" title=" <%:Model.IsActive ? "disable" : "enable"%>">
        <div id="reportStatusDiv_<%:Model.ReportScheduleID %>" class="corp-status change-rule-status <%:Model.IsActive ? "sensorStatusOK" : "sensorStatusInactive"%>" style="margin-right: 10px; height: auto;"></div>
        <div class="report-inside">
            <div><%=Html.GetThemedSVG("book") %></div>
            <div class="mainSensorCardInside">
                <a href="/Export/ReportHistory/<%:Model.ReportScheduleID%>">
                    <div class="glance-text">
                        <div class="glance-name"><%=Model.Name%></div>
<%--                        <div class="glance-reading" style="font-size: small;"><%: Html.TranslateTag("Export/ReportDetails|Last Run Date", "Last Run")%>: --%>

                <%
                    DateTime ReportTime = Model.LastRunDate.OVToLocalDateTimeShort().ToDateTime();

                    DateTime minValue = ("01/01/0001 12:00:00 AM").ToDateTime();

                    if (ReportTime == minValue)
                    {%>
                <div class="glance-reading" style="font-size: small;"><%: Html.TranslateTag("Export/ReportDetails|Last Run Date:", "Last Run")%>: </div>

                <%}
                    else
                    {%>
                <div class="glance-reading" style="font-size: small;"><%: Html.TranslateTag("Export/ReportDetails|Last Run Date:", "Last Run")%>: <%:ReportTime%></div>
                <%}%>
                           <div class="glance-reading" style="font-size: small;"> 
                            <%:query.Name %>
                            </div>

                         <div class="glance-reading" style="font-size: small;"><%: Html.TranslateTag("Export/ReportDetails|Report Frequency", "Report Frequency")%>: 
                            <%:Model.ScheduleType%>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
    </div>

</div>


<script type="text/javascript">

    function removeReport(item) {
        let values = {};
        values.url = `/Export/Delete?id=${item}`;
        values.text = 'Are you sure you want to delete this report?';
        values.redirect = '/Export/ReportIndex';
        openConfirm(values);
        e.stopImmediatePropagation();
    }

    function toggleReportStatus(reportID) {

        var statusDiv = $("#reportStatusDiv_" + reportID);

        if (statusDiv.hasClass("sensorStatusOK")) {
            $.post("/Export/SetActive", { "id": reportID, "active": false }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusInactive");
                    statusDiv.removeClass("sensorStatusOK");
                    $("#report" + reportID).prop('title', 'Click to Enable');
                    window.location.reload();
                }
            });
        }
        else {
            $.post("/Export/SetActive", { "id": reportID, "active": true }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusOK");
                    statusDiv.removeClass("sensorStatusInactive");
                    $("#report" + reportID).prop('title', 'Click to Disable');
                    window.location.reload();
                }
            });
        }
    }

    $(function () {
        isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
    });
</script>
