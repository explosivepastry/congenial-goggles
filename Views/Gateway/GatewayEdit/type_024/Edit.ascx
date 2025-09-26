<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<form class="form-horizontal form-label-left" action="/Overview/GatewayEdit/<%:Model.GatewayID %>" id="gatewayEdit_<%:Model.GatewayID %>" method="post">

    <%: Html.ValidationSummary(true) %>

    <input type="hidden" id="returns" name="returns" value="/Overview/GatewayEdit/<%:Model.GatewayID %>" />


    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>

    <%if (Model.GatewayTypeID != null)
      { %>

    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_IMSI.ascx", Model); %>
    <%if (new Version(Model.GatewayFirmwareVersion) >= new Version("1.4.3.4")) {
        Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model);
    }%>

    <div class="row sensorEditForm">
      <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Force Transmit on Aware","Force Transmit on Aware")%>
      </div>
      <div class="col sensorEditFormInput">
        <%: Html.CheckBoxFor(model => model.ObserveAware)%> 
      </div>
    </div> 

    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ForceLowPower.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>

    <%} %>


    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>

    <div class="clearfix"></div>
</form>
