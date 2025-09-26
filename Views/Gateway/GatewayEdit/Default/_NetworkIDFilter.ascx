<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_NetworkIDFilter|Define a NetworkID (Default","Define a NetworkID (Default")%>: <%:Model.GatewayType.DefaultNetworkIDFilter%>)
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.NetworkIDFilter)%>
        <%: Html.ValidationMessageFor(model => model.NetworkIDFilter)%>
    </div>
</div>

