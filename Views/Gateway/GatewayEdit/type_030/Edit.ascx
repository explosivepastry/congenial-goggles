<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<%
    bool isEthernetCapable = ((Model.SystemOptions & 0x01) > 0);
    bool isPoECapable = ((Model.SystemOptions & 0x02) > 0);
    bool isCellCapable = ((Model.SystemOptions & 0x04) > 0);
    bool isGPSUnlocked = ((Model.SystemOptions & 0x08) > 0);
    bool isUnlocked = Model.IsUnlocked; //((Model.SystemOptions & 0x10) > 0);
    bool hasShadowConfigs = ((Model.SystemOptions & 0x20) > 0);

    //isEthernetCapable = true;
    //isPoECapable = true;
    //isCellCapable = true;
    //isUnlocked = true;
    if (Model.SKU.Contains("-IN") || Model.SKU.Contains("-in") || new Version(Model.GatewayFirmwareVersion) < new Version("2.0.1.0"))
    {
        isEthernetCapable = false;
    }

    if (new Version(Model.GatewayFirmwareVersion) < new Version("2.0.1.2") || Model.SKU.Contains("-B1") || !Model.IsGPSUnlocked)
    {
        isGPSUnlocked = false;
    }
%>

<form class="form-horizontal form-label-left" action="/Overview/GatewayEdit/<%:Model.GatewayID %>" id="gatewayEdit_<%:Model.GatewayID %>" method="post">

    <%: Html.ValidationSummary(true) %>

    <input type="hidden" id="returns" name="returns" value="/Overview/GatewayEdit/<%:Model.GatewayID %>" />


    <div class="formBody mobileNoMargin">

        <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link active"
                    id="pills-general-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-general"
                    type="button"
                    role="tab"
                    aria-controls="pills-General"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|General","General")%>
                </button>
            </li>
            <%if (isEthernetCapable)
                { %>
            <li>
                <button
                    class="nav-link"
                    id="pills-ethernet-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-ethernet"
                    type="button"
                    role="tab"
                    aria-controls="pills-ethernet"
                    aria-selected="false">
                    <%: Html.TranslateTag("Ethernet", "Ethernet")%>
                </button>
            </li>
            <%}
                if (isCellCapable)
                {%>
            <li>
                <button
                    class="nav-link"
                    id="pills-cellular-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-cellular"
                    type="button"
                    role="tab"
                    aria-controls="pills-cellular"
                    aria-selected="false">
                    <%: Html.TranslateTag("Cellular", "Cellular")%>
                </button>
            </li>
            <% }
                if (isCellCapable)
                {%>
            <li>
                <button
                    class="nav-link"
                    id="pills-commands-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-commands"
                    type="button"
                    role="tab"
                    aria-controls="pills-commands"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Commands","Commands")%>
                </button>
            </li>
            <% }
                if (isEthernetCapable)
                {%>
            <li>
                <button
                    class="nav-link"
                    id="pills-http-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-http"
                    type="button"
                    role="tab"
                    aria-controls="pills-http"
                    aria-selected="false">
                    <%: Html.TranslateTag("HttpInterface", "HTTP Interface")%>
                </button>
            </li>
            <% } %>
        </ul>

        <div class="tab-content ms-1" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-general" role="tabpanel" aria-labelledby="pills-general-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_030/_HeartBeat.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_ConnectionPreference.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_DisableWireless.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_ForceLowPower.ascx", Model); %>
                <%if (isUnlocked){ %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_030/_Servers.ascx", Model); %>
                <%} %>
                <%if (isGPSUnlocked){ %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GPSReportInterval.ascx", Model); %>
                <%} %>
            </div>
            <%if (isEthernetCapable)
                { %>
            <div class="tab-pane fade show" id="pills-ethernet" role="tabpanel" aria-labelledby="pills-ethernet-tab">

                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_MacAddress.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayIP.ascx", Model); %>
            </div>
            <%}
                if (isCellCapable)
                { %>
            <div class="tab-pane fade show" id="pills-cellular" role="tabpanel" aria-labelledby="pills-cellular-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_Cellular.ascx", Model); %>
            </div>
            <%} %>
            <div class="tab-pane fade show" id="pills-commands" role="tabpanel" aria-labelledby="pills-commands-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
            </div>
            <%if (isEthernetCapable)
                { %>
            <div class="tab-pane fade show" id="pills-http" role="tabpanel" aria-labelledby="pills-http-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_GatewayInterfaceHTTP.ascx", Model); %>
            </div>
            <%} %>
        </div>
    </div>

    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>


    <div class="clearfix"></div>
</form>