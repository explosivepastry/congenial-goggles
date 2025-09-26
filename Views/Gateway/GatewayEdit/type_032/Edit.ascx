<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%
    bool isEthernetCapable = ((Model.SystemOptions & 0x01) > 0);
    bool isPoECapable = ((Model.SystemOptions & 0x02) > 0);
    bool isCellCapable = ((Model.SystemOptions & 0x04) > 0);
    bool isUnlocked = ((Model.SystemOptions & 0x10) > 0);
    bool hasShadowConfigs = ((Model.SystemOptions & 0x20) > 0);
%>

<form class="form-horizontal form-label-left" action="/Overview/GatewayEdit/<%:Model.GatewayID %>" id="gatewayEdit_<%:Model.GatewayID %>" method="post">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="returns" name="returns" value="/Overview/GatewayEdit/<%:Model.GatewayID %>" />
    <div class="formBody mobileNoMargin">
        <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link active"
                    id="pills-home-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-general"
                    type="button"
                    role="tab"
                    aria-controls="pills-General"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|General","General")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-commands"
                    type="button"
                    role="tab"
                    aria-controls="pills-commands"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Commands","Commands")%>
                </button>
            </li>
        </ul>

        <div class="tab-content ms-1" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-general" role="tabpanel" aria-labelledby="pills-general-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_IMSI.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_032/_ForceLowPower.ascx", Model); %>
                <%if (Model.IsUnlocked)
                    { %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_032/_Servers.ascx", Model); %>
                <%} %>
            </div>
            <div class="tab-pane fade" id="pills-commands" role="tabpanel" aria-labelledby="pills-commands-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
            </div>
        </div>
    </div>

    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>

</form>

