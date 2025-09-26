<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_SecondaryDNS|Primary DNS","Primary DNS")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.GatewayDNS)%>
        <%: Html.ValidationMessageFor(model => model.GatewayDNS)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_SecondaryDNS|Secondary DNS","Secondary DNS")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.SecondaryDNS)%>
        <%: Html.ValidationMessageFor(model => model.SecondaryDNS)%>
    </div>
</div>

