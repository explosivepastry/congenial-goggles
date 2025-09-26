<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_ChannelMask|Channel Mask (Default","Channel Mask (Default")%>: <%:Model.GatewayType.DefaultChannelMask%>)
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.ChannelMask)%>
        <%: Html.ValidationMessageFor(model => model.ChannelMask)%>
    </div>
</div>

