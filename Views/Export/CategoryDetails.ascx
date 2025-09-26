<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ReportType>>" %>



<%if (Model.Count < 1)
    {
%>
No Categories Found
<%}
    else
    {
        foreach (ReportType item in Model)
        {
            string iconClassName = "fa-book";
            string active = "false";   %>

<div class="small-list-card">
    <div class="corp-status change-rule-status" data-id="<%:item.ReportTypeID%>" c>
        <div id="reportStatusDiv_<%:item.ReportTypeID%>" class="corp-status sensorStatusOK"></div>
    </div>

    <div class="inside-report-box ">
    <a style="width: 100%;" href="/Export/ReportTemplates/<%:item.ReportTypeID %>">
    <%--    <div class="d-flex align-items-center">--%>
            <div class="divCellCenter holder holderInactive dfjcac ">
                <div class="eventIcon_container" data-id="<%:item.ReportTypeID%>" onclick="toggleReportStatus(this);">
            <%--        <div class="sensor eventIcon eventIconStatus sensorIcon "></div>--%>
                    <%=Html.GetThemedSVG("book") %>
                </div>
            </div>
            <div class="glance-text">
                <div class="glance-name" style="font-size: small;"><%=item.Name %></div>
                <div class="glance-reading" style="font-size: x-small;"><%=item.Description %></div>
            </div>

            <div class="gatewayList_detail ">

                <span class="menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                    <%=Html.GetThemedSVG("menu") %>
                </span>
                <div class="dropdown-menu ddm" style="padding: 0;">
                    <ul class="px-0 mb-0">
                        <li>
                            <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="window.location='/Export/ReportTemplates/<%:item.ReportTypeID %>'" style="justify-content:space-between;">
                                <span>    <%: Html.TranslateTag("View","View")%></span>
                            
                                <span>
                                    <%=Html.GetThemedSVG("view") %>
                                </span>
                            </div>
                        </li>
                    </ul>
                </div>

            </div>

      <%--  </div>--%>
    </a>
</div>

</div>
<%}
    } %>
<script type="text/javascript">

    $(document).ready(function () {

    });



</script>
