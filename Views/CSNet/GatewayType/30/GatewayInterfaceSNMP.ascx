<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="formBody">
      <div id="accordion">
        <h3>SNMP Interface 1</h3>
        <div>
            
                <div class="editor-label">
                    SNMP Address
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1) %>
                </div>

                <div class="editor-label">
                    Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1) %>
                </div>

                <div class="editor-label">
                    Trap Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap1Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap1Active) %>
                </div>

                <div class="editor-label">
                    Trap Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1) %>
                </div>
            
        </div>

        <h3>SNMP Interface 2</h3>
        <div>
            

                <div class="editor-label">
                    SNMP Interface 2 Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface2Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface2Active) %>
                </div>

                <div class="editor-label">
                    SNMP Address
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2) %>
                </div>

                <div class="editor-label">
                    Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort2) %>
                </div>

                <div class="editor-label">
                    Trap Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap2Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap2Active) %>
                </div>

                <div class="editor-label">
                    Trap Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort2) %>
                </div>
           
        </div>
        <h3>SNMP Interface 3</h3>
        <div>
            


                <div class="editor-label">
                    SNMP Interface 3 Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface3Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface3Active) %>
                </div>

                <div class="editor-label">
                    SNMP Address
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3) %>
                </div>

                <div class="editor-label">
                    Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort3) %>
                </div>

                <div class="editor-label">
                    Trap Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap3Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap3Active) %>
                </div>

                <div class="editor-label">
                    Trap Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort3) %>
                </div>
            
        </div>
        <h3>SNMP Interface 4</h3>
        <div>
            

                <div class="editor-label">
                    SNMP Interface 4 Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface4Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface4Active) %>
                </div>

                <div class="editor-label">
                    SNMP Address
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress4) %>
                </div>

                <div class="editor-label">
                    Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort4) %>
                </div>

                <div class="editor-label">
                    Trap Active
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap4Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap4Active) %>
                </div>

                <div class="editor-label">
                    Trap Port
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort4) %>
                </div>
            
        </div>
    </div>
</div>

<script>
    $("#accordion").accordion({
        collapsible: true,
        heightStyle: "content"
       
    });
</script>


