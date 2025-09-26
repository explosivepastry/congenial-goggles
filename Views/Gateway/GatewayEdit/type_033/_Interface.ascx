<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Interface|Activate SNMP Interface","Activate SNMP Interface")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPInterface1Active ? "checked" : "" %> id="snmp" name="SNMPInterface1Active">
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.SNMPInterface1Active)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Interface|Activate Modbus Interface","Activate Modbus Interface")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.ModbusInterfaceActive ? "checked" : "" %> id="modbus" name="ModbusInterfaceActive">
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.ModbusInterfaceActive)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Interface|Activate SNTP Interface","Activate SNTP Interface")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.NTPInterfaceActive ? "checked" : "" %> id="sntp" name="NTPInterfaceActive">
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.NTPInterfaceActive)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Interface|Activate Http Interface","Activate Http Interface")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.HTTPInterfaceActive ? "checked" : "" %> id="http" name="HTTPInterfaceActive">
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.HTTPInterfaceActive)%>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        setSNMPVisibility();
        $('#SNMPInterface1Active').change(setSNMPVisibility);

        setModbusVisibility();
        $('#ModbusInterfaceActive').change(setModbusVisibility);

        setTimeVisibility();
        $('#NTPInterfaceActive').change(setTimeVisibility);

        setHttpVisibility();
        $('#HTTPInterfaceActive').change(setHttpVisibility);

    });

    function setSNMPVisibility() {
        if ($('#SNMPInterface1Active').is(":checked"))
            $('#snmpclick').show();
        else
            $('#snmpclick').hide();
    }

    function setModbusVisibility() {
        if ($('#ModbusInterfaceActive').is(":checked"))
            $('#modbusclick').show();
        else
        $('#modbusclick').hide();
    }

    function setTimeVisibility() {
        if ($('#NTPInterfaceActive').is(":checked"))
            $('#ntpclick').show();
        else
            $('#ntpclick').hide();
    }

    function setHttpVisibility() {
        if ($('#HTTPInterfaceActive').is(":checked"))
            $('#httpclick').show();
        else
            $('#httpclick').hide();
    }
</script>
