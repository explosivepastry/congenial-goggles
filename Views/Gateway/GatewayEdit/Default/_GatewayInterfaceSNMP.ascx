<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="formBody">
    <div id="accordion">
        <h3 class="headerGatewayAB"><%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface","SNMP Interface")%> 1</h3>
        <div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Address","SNMP Address")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPInterfaceAddress1" name="SNMPInterfaceAddress1" value="<%= Model.SNMPInterfaceAddress1%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1)%>
                </div>
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Port","Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPInterfacePort1" name="SNMPInterfacePort1" value="<%= Model.SNMPInterfacePort1%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1)%>
                </div>
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Active","Trap Active")%>
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPTrap1Active" <%=Model.SNMPTrap1Active ? "checked" : "" %> name="SNMPTrap1Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap1Active)%>
                </div>
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPTrapPort1" name="SNMPTrapPort1" value="<%= Model.SNMPTrapPort1%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1)%>
                </div>
            </div>

        </div>

        <h3 class="headerGatewayAB"><%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface","SNMP Interface")%> 2</h3>
        <div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface 2 Active","SNMP Interface Active")%> 2
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPInterface2Active" <%=Model.SNMPInterface2Active ? "checked" : "" %> name="SNMPInterface2Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface2Active)%>
                </div>
            </div>


            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Address","SNMP Address")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPInterfaceAddress2" name="SNMPInterfaceAddress2" value="<%= Model.SNMPInterfaceAddress2%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2)%>
                </div>
            </div>


            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Port","Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPInterfacePort2" name="SNMPInterfacePort2" value="<%= Model.SNMPInterfacePort2%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort2)%>
                </div>
            </div>


            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Active","Trap Active")%>
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPTrap2Active" <%=Model.SNMPTrap2Active ? "checked" : "" %> name="SNMPTrap2Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap2Active)%>
                </div>
            </div>


            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPTrapPort2" name="SNMPTrapPort2" value="<%= Model.SNMPTrapPort2%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort2)%>
                </div>
            </div>

        </div>
        <h3 class="headerGatewayAB"><%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface","SNMP Interface")%> 3</h3>
        <div>



            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface 3 Active","SNMP Interface Active")%> 3
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPInterface3Active" <%=Model.SNMPInterface3Active ? "checked" : "" %> name="SNMPInterface3Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface3Active)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Address","SNMP Address")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input class="form-control" type="text" id="SNMPInterfaceAddress3" name="SNMPInterfaceAddress3" value="<%= Model.SNMPInterfaceAddress3%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Port","Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" class="form-control" id="SNMPInterfacePort3" name="SNMPInterfacePort3" value="<%= Model.SNMPInterfacePort3%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort3)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Active","Trap Active")%>
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPTrap3Active" <%=Model.SNMPTrap3Active ? "checked" : "" %> name="SNMPTrap3Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap3Active)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" id="SNMPTrapPort3" class="form-control" name="SNMPTrapPort3" value="<%= Model.SNMPTrapPort3%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort3)%>
                </div>
            </div>

        </div>
        <h3 class="headerGatewayAB"><%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface","SNMP Interface")%> 4</h3>
        <div>


            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Interface 4 Active","SNMP Interface Active")%> 4
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPInterface4Active" <%=Model.SNMPInterface4Active ? "checked" : "" %> name="SNMPInterface4Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface4Active)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|SNMP Address","SNMP Address")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" id="SNMPInterfaceAddress4" class="form-control" name="SNMPInterfaceAddress4" value="<%= Model.SNMPInterfaceAddress4%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress4)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Port","Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" id="SNMPInterfacePort4" class="form-control" name="SNMPInterfacePort4" value="<%= Model.SNMPInterfacePort4%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort4)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Active","Trap Active")%>
                </div>
                <div class="col sensorEditFormInput">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="SNMPTrap4Active" <%=Model.SNMPTrap4Active ? "checked" : "" %> name="SNMPTrap4Active">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap4Active)%>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_GatewayInterfaceSNMP|Trap Port","Trap Port")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" id="SNMPTrapPort4" class="form-control" name="SNMPTrapPort4" value="<%= Model.SNMPTrapPort4%>" />
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort4)%>
                </div>
            </div>

        </div>
    </div>
</div>

<style>
    .headerGatewayAB:hover {
        color: var(--options-icon-color)
    }

    .headerGatewayAB {
        cursor: pointer;
        color: #515356;
        font-size: 0.90rem;
        font-weight: 600;
    }
</style>

<script>

    $(document).ready(function () {

        $('.bootTooggle').bootstrapToggle();
    });


    $("#accordion").accordion({
        collapsible: true,
        heightStyle: "content"

    });
</script>


