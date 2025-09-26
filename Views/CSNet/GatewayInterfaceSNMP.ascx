<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<% using (Html.BeginForm()) {%>
    <div class="formBody">
        <%: Html.ValidationSummary(true) %>
        <%: Html.HiddenFor(model => model.Name)%>
          
        <div class="editor-label">
            Queue Expiration <br />(default: 720 minutes)
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.SingleQueueExpiration)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.SingleQueueExpiration)%>
        </div>

        <!-- SNMP Interface 1 -->    
            <fieldset>
                <legend>SNMP Interface 1</legend>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterface1Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface1Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface1Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfaceAddress1) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfacePort1) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrap1Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap1Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap1Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrapPort1) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort1) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1) %>
                </div>



            </fieldset>
            
            <!-- SNMP Interface 2 -->    
            <fieldset>
                <legend>SNMP Interface 2</legend>



                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterface2Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface2Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface2Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfaceAddress2) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfacePort2) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort2) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrap2Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap2Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap2Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrapPort2) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort2) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort2) %>
                </div>


            </fieldset>
            
            <!-- SNMP Interface 3 -->    
            <fieldset>
                <legend>SNMP Interface 3</legend>


                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterface3Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface3Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface3Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfaceAddress3) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfacePort3) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort3) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrap3Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap3Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap3Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrapPort3) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort3) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort3) %>
                </div>



            </fieldset>
            
            <!-- SNMP Interface 4 -->    
            <fieldset>
                <legend>SNMP Interface 4</legend>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterface4Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterface4Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterface4Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfaceAddress4) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPInterfaceAddress4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress4) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPInterfacePort4) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPInterfacePort4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort4) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrap4Active) %>
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.SNMPTrap4Active) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrap4Active) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SNMPTrapPort4) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.SNMPTrapPort4) %>
                    <%: Html.ValidationMessageFor(model => model.SNMPTrapPort4) %>
                </div>



            </fieldset>

        <div style="clear:both;"></div>

    </div>
    
    <div class="buttons">
        <a href="#" onclick="hideModal();" class="greybutton">Cancel</a>
        <input type="button" id="SaveSNMP" value="Save" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>
<% } %>
<script type="text/javascript">
    $(document).ready(function () {
                
        $('#SaveSNMP').click(function (e) {
            postForm($(this).closest('form'));
        });
    });

</script>
