<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%
    Html.RenderPartial("~/Views/Gateway/GatewayEdit/Setup/_GatewayName.ascx", Model);
    Html.RenderPartial("~/Views/Gateway/GatewayEdit/Setup/_Heartbeat.ascx", Model);
    Html.RenderPartial("~/Views/Gateway/GatewayEdit/Setup/_SimpleSaveButton.ascx", Model);
%>
<%:Html.Hidden("ObserveAware", true)%>