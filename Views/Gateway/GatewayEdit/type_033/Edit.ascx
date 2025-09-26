<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

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
                    id="pills-profile-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-network"
                    type="button"
                    role="tab"
                    aria-controls="pills-network"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Network","Network")%>
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
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-interfaces"
                    type="button"
                    role="tab"
                    aria-controls="pills-interfaces"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Interfaces","Interfaces")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link snmp"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-snmp"
                    type="button"
                    role="tab"
                    aria-controls="pills-snmp"
                    aria-selected="false"
                    style="<%: Model.SNMPInterface1Active ? "" :"display:none;"%>"
                    >
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|SNMP","SNMP")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link modbus"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-modbus"
                    type="button"
                    role="tab"
                    aria-controls="pills-modbus"
                    aria-selected="false"
                    style="<%: Model.ModbusInterfaceActive ? "" :"display:none;"%>"
                    >
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Modbus","Modbus")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link sntp"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-sntp"
                    type="button"
                    role="tab"
                    aria-controls="pills-sntp"
                    style="<%: Model.NTPInterfaceActive ? "" :"display:none;"%>"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_033/_Edit|SNTP","SNTP")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link http"
                    id="pills-contact-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-http"
                    type="button"
                    role="tab"
                    aria-controls="pills-http"
                    aria-selected="false"
                    style="<%: Model.HTTPInterfaceActive ? "" :"display:none;"%>">
                    <%: Html.TranslateTag("Gateway/type_033/_Edit|HTTP","HTTP")%>
                </button>
            </li>
        </ul>
        <div class="tab-content ms-1" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-general" role="tabpanel" aria-labelledby="pills-general-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_DisableWireless.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_Servers.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-network" role="tabpanel" aria-labelledby="pills-network-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_MacAddress.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayIP.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-commands" role="tabpanel" aria-labelledby="pills-commands-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_WillCallExpiration.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetInterval.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-interfaces" role="tabpanel" aria-labelledby="pills-interfaces-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_Interface.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-snmp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_GatewayInterfaceSNMP.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-modbus" role="tabpanel" aria-labelledby="pills-modbus-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceMB.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-sntp" role="tabpanel" aria-labelledby="pills-sntp-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_GatewayInterfaceNTP.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-http" role="tabpanel" aria-labelledby="pills-http-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_GatewayInterfaceHTTP.ascx", Model); %>
            </div>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>
    <div class="clearfix"></div>
</form>


<script>
    $('.form-check-input').click(function () {
        let id = $(this).attr('id');
        $(`#${id}`).is(":checked") ? $(`.${id}`).show() : $(`.${id}`).hide()
    })
</script>
