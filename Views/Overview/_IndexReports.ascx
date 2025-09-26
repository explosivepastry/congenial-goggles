<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%-- 
    <!----------------------------------------------------------------------------------------------
                                    Top Grid Container 
                        1. SensorPie / 2. GatewayPie / 3. Profile / 4. *Reports*
    --------------------------------------------------------------------------------------------------> 
--%>

<!-- ------------------------
        Reports Card Container 
    ------------------------------>

<%--<div class="reportsGrid  top_card" style="min-width: 327px; overflow: hidden">--%>
    <div class="headerCard2 indexTabs">
        <div class="l-corner-hug repo-icon"><%:Html.GetThemedSVG("reports") %> </div>
        <span class="w-100">Recent Reports</span>
    </div>

    <div class="<%--recentReports--%> containMe" style="overflow-y: scroll">
        <%List<ReportSchedule> reports = (ViewBag.Reports != null ? ViewBag.Reports as List<ReportSchedule> : new List<ReportSchedule>()).OrderByDescending(x => x.LastRunDate).Take(5).ToList();
            if (reports.Count < 1)
            {%>
        <a href="/Export/ReportCategory">
            <div class="d-flex linex">
                <div class="redDot"></div>
                <div class="icon-update"><%:Html.GetThemedSVG("reports") %></div>
                Add a<span style="color: var(--primary-color); font-weight: bold;">Report</span>for this account.
            </div>
        </a>
        <%}

            foreach (ReportSchedule report in reports)
            {%>
        <a href="/Export/ReportHistory/<%:report.ReportScheduleID %>">
            <div class="d-flex linex">
                <div class="<%:report.IsActive ? "greenDot" : "greyDot" %>"></div>
                <span class="report_name" data-shorty="<%:report.Name %>" style="width: 137px;" onmouseover="$(this).css('color', 'var(--primary-color)').css('font-weight', 'bold');"
                    onmouseout="$(this).css('color', '').css('font-weight', '');;"><%:report.Name %></span>
                <div class="home-date">Last Ran: <%:report.LastRunDate.ToShortDateString() %></div>
            </div>
        </a>
        <%}%>
    </div>
<%--</div>--%>
<!--End Report -->

<script>

        /*        main Containers on page*/
        var indexTab = document.getElementsByClassName("indexTabs");
        var tab;

        for (tab = 0; tab < indexTab.length; tab++) {
            indexTab[tab].addEventListener("click", function () {
                this.classList.toggle("activeTab");
                var containMe = this.nextElementSibling;
                if (containMe.style.maxHeight) {
                    containMe.style.maxHeight = null;
                    containMe.style.marginTop = "0";

                } else {
                    containMe.style.maxHeight = containMe.scrollHeight + "px";
                    containMe.style.marginTop = "3%";

                }

            });
            indexTab[tab].click();
        }

        /*          rules triggered Container*/
        var ruleTab = document.getElementsByClassName("ruleTabs");
        var j;

        for (j = 0; j < ruleTab.length; j++) {
            ruleTab[j].addEventListener("click", function () {
                this.classList.toggle("activeTab");
                var containRule = this.nextElementSibling;
                if (containRule.style.maxHeight) {
                    containRule.style.maxHeight = null;
                } else {
                    containRule.style.maxHeight = "190px";
                }
            });
            ruleTab[j].click();
        }

</script>

