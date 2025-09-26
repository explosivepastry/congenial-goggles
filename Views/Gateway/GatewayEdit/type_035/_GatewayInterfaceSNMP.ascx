<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress1)) { Model.SNMPInterfaceAddress1 = "0.0.0.0"; } %>
<%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress3)) { Model.SNMPInterfaceAddress3 = "255.255.255.255"; } %>
<%if (Model.SNMPInterfacePort1 == int.MinValue) { Model.SNMPInterfacePort1 = 0; } %>
<%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress2)) { Model.SNMPInterfaceAddress2 = "0.0.0.0"; } %>
<%if (Model.SNMPTrapPort1 == int.MinValue) { Model.SNMPTrapPort1 = 0; } %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound IP Range Start","Inbound IP Range Start")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfaceAddress1" name="SNMPInterfaceAddress1" value="<%=Model.SNMPInterfaceAddress1%>" />
        <a id="inboundStart" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound IP Range End","Inbound IP Range End ")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPInterfaceAddress3" name="SNMPInterfaceAddress3" value="<%=Model.SNMPInterfaceAddress3%>" />
        <a id="inboundEnd" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Inbound Port","Inbound Port ")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="SNMPInterfacePort1" name="SNMPInterfacePort1" value="<%=Model.SNMPInterfacePort1%>" />
        <a id="inboundPort" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Community String","Community String ")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="SNMPCommunityString" name="SNMPCommunityString" value="<%=Model.SNMPCommunityString%>" />
        <a id="communityString" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.SNMPCommunityString)%>
    </div>
</div>

<!-- Trap Settings -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <strong><%: Html.TranslateTag("Gateway/_Interface|Trap Settings","Trap Settings")%></strong>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap1Active ? "checked" : "" %> id="SNMPTrap1Active" name="SNMPTrap1Active">
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
        </div>
        <%: Html.ValidationMessageFor(g => Model.SNMPTrap1Active)%>
    </div>
    <div id="trapDiv" class="ps-0">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap IP Address","Trap IP Address")%>
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="SNMPInterfaceAddress2" name="SNMPInterfaceAddress2" value="<%=Model.SNMPInterfaceAddress2%>" />
                <a id="trapIPAddress" style="cursor: pointer;"></a>
                <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2)%>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port ")%>
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="number" id="SNMPTrapPort1" name="SNMPTrapPort1" value="<%=Model.SNMPTrapPort1%>" />
                <a id="trapPort" style="cursor: pointer;"></a>
                <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1)%>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on Authentication Failure","Trap on Authentication Failure")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0">
                    <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap2Active ? "checked" : "" %> id="SNMPTrap2Active" name="SNMPTrap2Active">
                    <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
                </div>
                <%: Html.ValidationMessageFor(g => Model.SNMPTrap2Active)%>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on New Sensor Data","Trap on New Sensor Data")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0">
                    <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap3Active ? "checked" : "" %> id="SNMPTrap3Active" name="SNMPTrap3Active">
                    <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
                </div>
                <%: Html.ValidationMessageFor(g => Model.SNMPTrap3Active)%>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap on Sensor Alarms","Trap on Sensor Alarms")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0">
                    <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.SNMPTrap4Active ? "checked" : "" %> id="SNMPTrap4Active" name="SNMPTrap4Active">
                    <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
                </div>
                <%: Html.ValidationMessageFor(g => Model.SNMPTrap4Active)%>
            </div>
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
            $('#trapDiv').show();
        else
            $('#trapDiv').hide();
    }

</script>
