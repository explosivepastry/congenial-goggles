<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    Monnit.Gateway gateway = Monnit.Gateway.LoadBySensorID(Model.SensorID);
    string[] simstrings = gateway.MacAddress.Split('|'); ;
    if (simstrings.Length < 3) simstrings = new string[] { "errorParseStr", "errorParseStr", "errorParseStr" };
    bool isGPSUnlocked = ((gateway.SystemOptions & 0x08) > 0);
    bool showOtaUpdate = true;


    if (new Version(gateway.GatewayFirmwareVersion) < new Version("2.0.1.2") || gateway.SKU.Contains("-B1") || !gateway.IsGPSUnlocked)
    {
        isGPSUnlocked = false;
    }

    if (!MonnitSession.IsEnterpriseAdmin && !MonnitSession.IsEnterprise)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(gateway.SKU))
            {
                string latestVersion = MonnitUtil.GetLatestFirmwareVersionFromMEA(gateway.SKU, gateway.GatewayType.IsGatewayBase);

                if (!latestVersion.Contains("Failed") && latestVersion != gateway.GatewayFirmwareVersion)
                {
                    showOtaUpdate = true;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Log("_LTESensor[.ascx][GetLatestFirmwareVersionFromMEA] ");
        }
    }

%>
<div class="x-content">
    <form class="form-horizontal form-label-left" action="/Overview/InterfaceEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
        <%: Html.ValidationSummary(false)%>
        <input type="hidden" value="/Overview/InterfaceEdit/<%:Model.SensorID %>" name="returns" id="returns" />
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GatewayName.ascx", gateway); %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_HeartBeat.ascx", gateway); %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ObserveAware.ascx", gateway); %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ForceLowPower.ascx", gateway); %>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor|Reset to Factory Defaults","Reset to Factory Defaults")%>
            </div>

            <div class="col sensorEditFormInput">
                <a id="resetToFactory" class="btn btn-secondary btn-sm"><%: Html.TranslateTag("Reset Default","Reset Default")%></a>
            </div>
        </div>

        <%--<%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetDefault.ascx", gateway); %>--%>


        <%--        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_RemoteNetworkReset.ascx", gateway); %>--%>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_RemoteNetworkReset|SIM Reset","SIM Reset")%>
            </div>
            <div class="col sensorEditFormInput">
                <a href="#" id="Reform" class="btn btn-secondary btn-sm"><%: Html.TranslateTag("Reset SIM","Reset SIM")%></a>
            </div>
        </div>




        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_ResetInterval.ascx", gateway); %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_033/_Servers.ascx", gateway); %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Type_030/_Cellular.ascx", gateway); %>

        <!-- If the gateway is unlocked-->
        <%if (gateway.IsUnlocked)
            { %>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("URL","URL")%>
            </div>

            <div class="col sensorEditFormInput">
                <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="ServerHostAddress" name="ServerHostAddress" value="<%= gateway.ServerHostAddress%>" />
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Port","Port")%>
            </div>

            <div class="col sensorEditFormInput">
                <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="Port" id="Port" value="<%=gateway.Port%>" />
            </div>
        </div>
        <%} %>

        <%if (showOtaUpdate)
            {%>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor|Update Firmware","Update Firmware")%>
            </div>

            <div class="col sensorEditFormInput">
                <%if (gateway.ForceToBootloader)
                    { %>
                <%: Html.TranslateTag("Pending","Pending")%>
                <%}
                    else
                    { %>
                <button id="btnOtaUpdate" class="btn btn-secondary" type="button"><%: Html.TranslateTag("Update","Update")%></button>
                <%} %>
            </div>
        </div>
        <%} %>

        <%if (isGPSUnlocked)
            { %>
        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_GPSReportInterval.ascx", gateway); %>
        <%} %>

        <%--        <div class="formBody mobileNoMargin">
            <div id="tabs">
                <div class="col-12">
                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link active"
                                id="pills-snmp-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-snmp"
                                type="button"
                                role="tab"
                                aria-controls="pills-snmp"
                                aria-selected="false">
                                <%: Html.TranslateTag("SNMP","SNMP")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-modbus-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-modbus"
                                type="button"
                                role="tab"
                                aria-controls="pills-modbus"
                                aria-selected="false">
                                <%: Html.TranslateTag("Modbus","Modbus")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-sntp-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-sntp"
                                type="button"
                                role="tab"
                                aria-controls="pills-sntp"
                                aria-selected="false">
                                <%: Html.TranslateTag("SNTP","SNTP")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-http-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-http"
                                type="button"
                                role="tab"
                                aria-controls="pills-http"
                                aria-selected="false">
                                <%: Html.TranslateTag("HTTP","HTTP")%>
                            </button>
                        </li>
                    </ul>



<%--                    <div class="tab-content ms-1" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills-snmp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate SNMP Interface","Activate SNMP Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.SNMPInterface1Active ? "checked" : "" %> id="snmp" name="SNMPInterface1Active">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.SNMPInterface1Active)%>
                                </div>
                            </div>
                            <div class="snmp">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceSNMP.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-modbus" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate Modbus Interface","Activate Modbus Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.ModbusInterfaceActive ? "checked" : "" %> id="modbus" name="ModbusInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.ModbusInterfaceActive)%>
                                </div>
                            </div>

                            <div class="modbus">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceMB.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-sntp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate SNTP Interface","Activate SNTP Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.NTPInterfaceActive ? "checked" : "" %> id="sntp" name="NTPInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                </div>
                                <%: Html.ValidationMessageFor(g => gateway.NTPInterfaceActive)%>
                            </div>
                            <div class="sntp">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceNTP.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-http" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate Http Interface","Activate Http Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.HTTPInterfaceActive ? "checked" : "" %> id="http" name="HTTPInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.HTTPInterfaceActive)%>
                                </div>
                            </div>

                            <div class="http">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceHTTP.ascx", gateway); %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <hr />
        <div class="row">
            <div class="col-12 text-end">
                <button type="button" onclick="$(this).hide();$('#saving').show();postForm($('#simpleEdit_<%:Model.SensorID %>'));" value="<%: Html.TranslateTag("Save", "Save")%>" class="btn btn-primary">Save</button>
                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    Loading...
                </button>
            </div>
        </div>
    </form>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        if ($('#snmp').is(":checked"))
            $('.snmp').show();
        else
            $('.snmp').hide();

        if ($('#modbus').is(":checked"))
            $('.modbus').show();
        else
            $('.modbus').hide();

        if ($('#sntp').is(":checked"))
            $('.sntp').show();
        else
            $('.sntp').hide();

        if ($('#http').is(":checked"))
            $('.http').show();
        else
            $('.http').hide();

        $('.form-check-input').click(function () {
            let id = $(this).attr('id');
            $(`#${id}`).is(":checked") ? $(`.${id}`).show() : $(`.${id}`).hide()
        })



        $('#GPSReportInterval_Slider').slider({
            value: getGPSIntervalIndex(),
            min: 0,
            max: reportInterval_array.length - 1,
            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#GPSReportInterval').val(reportInterval_array[ui.value]);

        }
    });

        $("#GPSReportInterval").addClass('editField editFieldSmall');
        $("#GPSReportInterval").change(function () {
            //Check if less than min
            if ($("#GPSReportInterval").val() < 10)
                $("#GPSReportInterval").val(10);

            //Check if greater than max
            if ($("#GPSReportInterval").val() > 720)
                $("#GPSReportInterval").val(720);

            $('#GPSReportInterval_Slider').slider("value", getGPSIntervalIndex());

        });

        function getGPSIntervalIndex() {
            var retval = 0;
            $.each(reportInterval_array, function (index, value) {
                if (value <= $("#GPSReportInterval").val())
                    retval = index;
            });

            return retval;
        }

        var reformConfirm = "<%: Html.TranslateTag("Gateway/_RemoteNetworkReset|Are you sure you want to reset the SIM?","Are you sure you want to reset the SIM?")%>";

        $('#Reform').click(function (e) {

            e.preventDefault();

            let values = {};
            let GatewayID = <%: gateway.GatewayID%>;
            let returnUrl = $('#returns').val();
            values.partialTag = $('#gatewayEdit_<%:gateway.GatewayID %>').parent();
            values.url = `/Overview/GatewayReform?id=${GatewayID}&url=${returnUrl}`;
            values.text = `${reformConfirm}`;

            openConfirm(values);
        });
    });

</script>
