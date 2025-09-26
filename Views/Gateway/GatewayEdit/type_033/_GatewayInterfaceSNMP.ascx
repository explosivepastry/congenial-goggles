<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress1)) { Model.SNMPInterfaceAddress1 = "0.0.0.0"; } %>
<%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress3)) { Model.SNMPInterfaceAddress3 = "255.255.255.255"; } %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound IP Range Start","Inbound IP Range Start")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfaceAddress1" name="SNMPInterfaceAddress1" value="<%= Model.SNMPInterfaceAddress1%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound IP Range End","Inbound IP Range End")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfaceAddress3" name="SNMPInterfaceAddress3" value="<%= Model.SNMPInterfaceAddress3%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound","Inbound")%> <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Port","Port")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfacePort1" name="SNMPInterfacePort1" value="<%= Model.SNMPInterfacePort1%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Community String","Community String")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPCommunityString" name="SNMPCommunityString" value="<%= Model.SNMPCommunityString%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPCommunityString)%>
    </div>
</div>

<h2 class="TrapDiv"><%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Settings","Trap Settings")%></h2>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Traps","Traps")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap1Active ? "checked" : "" %> id="SNMPTrap1Active" name="SNMPTrap1Active">
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.SNMPTrap1Active)%>
    </div>
</div>

<div class="row sensorEditForm TrapDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap IP Address","Trap IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfaceAddress2" name="SNMPInterfaceAddress2" value="<%= Model.SNMPInterfaceAddress2%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2)%>
    </div>
</div>

<div class="row sensorEditForm TrapDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPTrapPort1" name="SNMPTrapPort1" value="<%= Model.SNMPTrapPort1%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1)%>
    </div>
</div>

<div class="row sensorEditForm TrapDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on Authentication Failure","Trap on Authentication Failure")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap2Active ? "checked" : "" %> id="SNMPTrap2Active" name="SNMPTrap2Active" />
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
            <%: Html.ValidationMessageFor(model => model.SNMPTrap2Active)%>
        </div>
    </div>
</div>

<div class="row sensorEditForm TrapDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on New Sensor Data","Trap on New Sensor Data")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap3Active ? "checked" : "" %> id="SNMPTrap3Active" name="SNMPTrap3Active" />
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
            <%: Html.ValidationMessageFor(model => model.SNMPTrap3Active)%>
        </div>
    </div>
</div>

<div class="row sensorEditForm TrapDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on Sensor Alarms","Trap on Sensor Alarms")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap4Active ? "checked" : "" %> id="SNMPTrap4Active" name="SNMPTrap4Active" />
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
            <%: Html.ValidationMessageFor(model => model.SNMPTrap4Active)%>
        </div>
    </div>
</div>

<script>

    $(document).ready(function () {


        setTrapVisibility();
        $('#SNMPTrap1Active').change(setTrapVisibility);

    });

    function setTrapVisibility() {
        if ($('#SNMPTrap1Active').is(":checked"))
            $('.TrapDiv').show();
        else
            $('.TrapDiv').hide();
    }

</script>



