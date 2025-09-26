<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AccountLocationOverviewCustomModel>>" %>

<%
    //Alerting - Red
    foreach (AccountLocationOverviewCustomModel account in Model.Where(m => (m.OfflineCount + m.AlertCounts) > 0))
    {%>
<%:Html.Partial("_CorporateDetailsCard",account) %>
<%}

    //Warning - Yellow
    foreach (AccountLocationOverviewCustomModel account in Model.Where(m => (m.GatewayOffline + m.SensorLowBatt > 0) && (m.OfflineCount + m.AlertCounts) == 0))
    {%>
<%:Html.Partial("_CorporateDetailsCard",account) %>
<%}

    //Good - Green
    foreach (AccountLocationOverviewCustomModel account in Model.Where(m => (m.OfflineCount + m.GatewayOffline + m.AlertCounts + m.SensorLowBatt) == 0))
    {%>
<%:Html.Partial("_CorporateDetailsCard",account) %>
<%}%>