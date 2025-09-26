<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Interface|Queue Expiration","Queue Expiration")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" id="SingleQueueExpiration" name="SingleQueueExpiration" value="<%= Model.SingleQueueExpiration%>"  />
        <%: Html.ValidationMessageFor(model => model.SingleQueueExpiration)%>
    </div>
</div>

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
        <%: Html.TranslateTag("Gateway/_Interface|Activate Real Time Interface","Activate Real Time Interface")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.RealTimeInterfaceActive ? "checked" : "" %> id="sntp" name="RealTimeInterfaceActive">
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceActive)%>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        setSNMPVisibility();
        $('#SNMPInterface1Active').change(setSNMPVisibility);

        setModbusVisibility();
        $('#ModbusInterfaceActive').change(setModbusVisibility);

        setRealTimeVisibility();
        $('#RealTimeInterfaceActive').change(setRealTimeVisibility);

        $('.bootTooggle').bootstrapToggle();
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

    function setRealTimeVisibility() {
        if ($('#RealTimeInterfaceActive').is(":checked"))
            $('#realtimeclick').show();
        else
            $('#realtimeclick').hide();
    }
</script>
