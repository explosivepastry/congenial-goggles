<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>
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
                    aria-controls="pills-general"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|General","General")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link"
                    id="pills-network-tab"
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
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link"
                    id="pills-interface-tab"
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
                    id="pills-snmp-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-snmp"
                    type="button"
                    role="tab"
                    aria-controls="pills-snmp"
                    aria-selected="false"
                    style="<%: Model.SNMPInterface1Active || Model.SNMPInterface2Active || Model.SNMPInterface3Active || Model.SNMPInterface4Active ? "" :"display:none;"%>"
                    >
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|SNMP","SNMP")%>
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link modbus"
                    id="pills-modbus-tab"
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
                    id="pills-sntp-tab"
                    data-bs-toggle="pill"
                    data-bs-target="#pills-sntp"
                    type="button"
                    role="tab"
                    aria-controls="pills-sntp"
                    style="<%: Model.RealTimeInterfaceActive ? "" :"display:none;"%>"
                    aria-selected="false">
                    <%: Html.TranslateTag("Gateway/type_007/_Edit|Real Time","Real Time")%>
                </button>
            </li>
        </ul>

        <div class="tab-content ms-1" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-general" role="tabpanel" aria-labelledby="pills-general-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", Model); %>
                <%if (new Version(Model.GatewayFirmwareVersion) >= new Version("3.1.0.0")) { 
                    Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_PollInterval.ascx", Model);
                }%>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_Servers.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-network" role="tabpanel" aria-labelledby="pills-network-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_MacAddress.ascx", Model); %>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayIP.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-commands" role="tabpanel" aria-labelledby="pills-commands-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", Model); %>
                <%if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware"))
                  {
                      Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_UpdateFirmware.ascx", Model);
                  }%>
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-interfaces" role="tabpanel" aria-labelledby="pills-interfaces-tab">
                <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_Interface.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-snmp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceSNMP.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-modbus" role="tabpanel" aria-labelledby="pills-modbus-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceMB.ascx", Model); %>
            </div>
            <div class="tab-pane fade" id="pills-sntp" role="tabpanel" aria-labelledby="pills-sntp-tab">
                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayInterfaceRT.ascx", Model); %>
            </div>
        </div>
    </div>
    <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_SaveButton.ascx", Model); %>
    <div class="clearfix"></div>
</form>
<script type="text/javascript">
    $('.form-check-input').click(function () {
        let id = $(this).attr('id');
        $(`#${id}`).is(":checked") ? $(`.${id}`).show() : $(`.${id}`).hide()
    })
</script>