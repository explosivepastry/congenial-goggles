<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_GatewayName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_HeartBeat.ascx", Model); %>


<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_PollRate.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_ConnectionPreferences.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_OnAwareMessages.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_OnServerLoss.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_GatewayPowerMode.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_CellularSettings.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_CommandSettings.ascx", Model); %>

<%if (Model.GatewayTypeID == 33 || Model.GatewayTypeID == 35)
    {%>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_TrapSettings.ascx", Model); %>

<%}%>


<% if (!Model.SKU.Contains("-IN") || !Model.SKU.Contains("-in"))
    {%>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_EthernetSettings.ascx", Model); %>
<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Help/_InterfaceSettings.ascx", Model); %>

<%}%>


<div class="clearfix"></div>
