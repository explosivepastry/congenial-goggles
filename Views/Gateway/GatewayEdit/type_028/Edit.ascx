<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<form class="form-horizontal form-label-left" action="/Overview/GatewayEdit/<%:Model.GatewayID %>" id="gatewayEdit_<%:Model.GatewayID %>" method="post">

    <%: Html.ValidationSummary(true) %>

    <input type="hidden" id="returns" name="returns" value="/Overview/GatewayEdit/<%:Model.GatewayID %>" />


    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
    <%if (Model.GatewayFirmwareVersion != "1.0.1.0")//Cradlepoint v 1.0.1.0 
      {%>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
    <%} %>

    <div class="clearfix"></div>

    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>

    <div class="clearfix"></div>
</form>
