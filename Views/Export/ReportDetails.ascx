<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ReportSchedule>>" %>

<%--<style>
    .reportName {
        cursor: pointer;
    }
</style>--%>

<%if (Model.Count < 1)
    {
%>
<%: Html.TranslateTag("Export/ReportDetails|No Reports. Click Add Report to create a new report.","No Reports. Click Add Report to create a new report.")%>
<%}
    else
    {

        List<ReportQuery> reportQueries = ReportQuery.LoadByAccount(MonnitSession.CurrentTheme.AccountThemeID, MonnitSession.CurrentCustomer.AccountID, MonnitSession.AccountCan("view_maps"), long.MinValue);
        List<ReportType> reportTypes = ReportType.LoadAll();

        foreach (ReportSchedule item in Model)
        {
            bool isFavorite = MonnitSession.IsReportFavorite(item.ReportScheduleID);
            ReportQuery reportQuery = reportQueries.Where(q => q.ReportQueryID == item.ReportQueryID).FirstOrDefault();
            ReportType reportType = null;
            if (reportQuery != null)
            {
                reportType = reportTypes.Where(t => t.ReportTypeID == reportQuery.ReportTypeID).FirstOrDefault();
            }
%>

<div class="small-list-card">

    <div class="corp-status change-rule-status" data-id="<%:item.ReportScheduleID%>">
        <div id="reportStatusDiv_<%:item.ReportScheduleID%>" class="corp-status <%:item.IsActive ? "sensorStatusOK" : "sensorStatusInactive"%>"></div>
    </div>


    <div class="inside-report-box ">
        <a href="/Export/ReportHistory/<%:item.ReportScheduleID %>">

            <div class="divCellCenter holder holderInactive dfjcac ">

                <%=Html.GetThemedSVG("book") %>
            </div>

            <div class="glance-text ">
                <div class="glance-name" style="font-size: .9rem;"><%=item.Name%></div>
                <div style="font-size: x-small;"><%: Html.TranslateTag("Template")%>: <%=reportQuery == null ? "" : Html.TranslateTag(reportQuery.Name)%></div>
                <div style="font-size: x-small;"><%: Html.TranslateTag("Category")%>: <%=reportType == null ? "" : Html.TranslateTag(reportType.Name)%></div>

                <%
                    DateTime ReportTime = item.LastRunDate.OVToLocalDateTimeShort().ToDateTime();

                    DateTime minValue = ("01/01/0001 12:00:00 AM").ToDateTime();

                    if (ReportTime == minValue)
                    {%>
                <div class="glance-reading" style="font-size: x-small;"><%: Html.TranslateTag("Export/ReportDetails|Last Run Date:", "Last Run")%>: </div>

                <%}
                    else
                    {%>
                <div class="glance-reading" style="font-size: x-small;"><%: Html.TranslateTag("Export/ReportDetails|Last Run Date:", "Last Run")%>: <%:ReportTime%></div>
                <%}%>
            </div>

        </a>

        <div class="gatewayList_detail ">
            <div class="menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex" : "none"%>; align-items: start; justify-content: center;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
                    <div class="listOfFav"><%= Html.GetThemedSVG("heart-beat")  %></div>
                </div>

                <%=Html.GetThemedSVG("menu") %>
            </div>

            <div class="dropdown-menu ddm" style="padding: 0;">
                <ul class="ps-0 mb-0">
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Export/ReportHistory/<%:item.ReportScheduleID %>'">
                            <%: Html.TranslateTag("Export/ReportDetails|View","View")%>
                            <span>
                                <%=Html.GetThemedSVG("view") %>
                            </span>
                        </div>
                    </li>

                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items " onclick="event.preventDefault(); window.location='/Export/ReportEdit/<%:item.ReportScheduleID %>'">
                            <%: Html.TranslateTag("Export/ReportDetails|Edit","Edit")%>
                            <span>
                                <%=Html.GetThemedSVG("edit") %>
                            </span>
                        </div>
                    </li>

                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Export/ReportRecipient/<%:item.ReportScheduleID %>'">
                            <%: Html.TranslateTag("Export/ReportDetails|Recipients","Recipients")%>
                            <span>
                                <%=Html.GetThemedSVG("recipients") %>
                            </span>
                        </div>
                    </li>
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" data-id="<%:item.ReportScheduleID%>" onclick="toggleReportStatus(<%:item.ReportScheduleID%>);">
                            <span id="toggleText_<%:item.ReportScheduleID%>"><%:item.IsActive ?  Html.TranslateTag("Disable") :Html.TranslateTag("Enable")%></span>
                            <%=Html.GetThemedSVG("disable") %>
                        </div>
                    </li>
                    <hr class="my-0">
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" data-deletereport="<%:item.ReportScheduleID%>" onclick="event.preventDefault();removeReport(<%:item.ReportScheduleID %>)">
                            <%: Html.TranslateTag("Export/ReportDetails|Delete","Delete")%>
                            <span>
                                <%=Html.GetThemedSVG("delete") %>
                            </span>
                        </div>
                    </li>
                </ul>

            </div>
        </div>
    </div>

</div>
<%}
    } %>

<script type="text/javascript">

    $(document).ready(function () {
        $('#filterdReports').html('<%:ViewBag.ReportCount%>');
    });

    function toggleReportStatus(reportID) {

        var statusDiv = $("#reportStatusDiv_" + reportID);
        var textDiv = $("#toggleText_" + reportID);

        if (statusDiv.hasClass("sensorStatusOK")) {
            $.post("/Export/SetActive", { "id": reportID, "active": false }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusInactive");
                    statusDiv.removeClass("sensorStatusOK");
                    textDiv.html("<%=Html.TranslateTag("Enable")%>");
                }
            });
        }
        else {
            $.post("/Export/SetActive", { "id": reportID, "active": true }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusOK");
                    statusDiv.removeClass("sensorStatusInactive");
                    textDiv.html("<%=Html.TranslateTag("Disable")%>");
                }
            });
        }
    }

    function removeReport(item) {
        let values = {};
        values.url = `/Export/Delete?id=${item}`;
        values.text = "<%: Html.TranslateTag("Export/ReportDetails|Are you sure you want to delete this report?","Are you sure you want to delete this report?")%>"
        openConfirm(values);
        e.stopImmediatePropagation();
    }

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(document).ready(function () {
        $(".listOfFav svg").addClass("liked");
    });
</script>
