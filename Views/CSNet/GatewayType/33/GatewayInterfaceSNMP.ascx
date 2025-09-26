<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="formBody">

    <%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress1)) { Model.SNMPInterfaceAddress1 = "0.0.0.0"; } %>
    <%if (string.IsNullOrEmpty(Model.SNMPInterfaceAddress3)) { Model.SNMPInterfaceAddress3 = "255.255.255.255"; } %>

    <div class="editor-label">
        Inbound IP Range Start
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.SNMPInterfaceAddress1) %>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1) %>
    </div>

    <div class="editor-label">
        Inbound IP Range End
    </div>
    <div class="editor-field">
        <%: Html.EditorFor(model => model.SNMPInterfaceAddress3) %>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3) %>
    </div>

    <div class="editor-label">
        Inbound Port
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.SNMPInterfacePort1) %>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1) %>
    </div>

    <div class="editor-label">
        Community String
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.SNMPCommunityString) %>
        <%: Html.ValidationMessageFor(model => model.SNMPCommunityString) %>
    </div>

    
    <div class="editor-label TrapDiv">
       <h2 class="TrapDiv">Trap Settings</h2>
    </div>
    <div class="editor-field">
        
    </div>

    <div class="editor-label">
        Traps
    </div>
    <div class="editor-field">
        <%: Html.CheckBoxFor(model => model.SNMPTrap1Active) %>
        <%: Html.ValidationMessageFor(model => model.SNMPTrap1Active) %>
    </div>

    <div class="editor-label TrapDiv">
        Trap IP Address
    </div>
    <div class="editor-field TrapDiv">
        <%: Html.EditorFor(model => model.SNMPInterfaceAddress2) %>
        <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2) %>
    </div>

    <div class="editor-label TrapDiv">
        Trap Port
    </div>
    <div class="editor-field TrapDiv">
        <%: Html.TextBoxFor(model => model.SNMPTrapPort1) %>
        <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1) %>
    </div>

    <div class="editor-label TrapDiv">
        Trap on Authentication Failure
    </div>
    <div class="editor-field TrapDiv">
        <%: Html.CheckBoxFor(model => model.SNMPTrap2Active) %>
        <%: Html.ValidationMessageFor(model => model.SNMPTrap2Active) %>
    </div>

    <div class="editor-label TrapDiv">
        Trap on New Sensor Data
    </div>
    <div class="editor-field TrapDiv">
        <%: Html.CheckBoxFor(model => model.SNMPTrap3Active) %>
        <%: Html.ValidationMessageFor(model => model.SNMPTrap3Active) %>
    </div>

    <div class="editor-label TrapDiv">
        Trap on Sensor Alarms
    </div>
    <div class="editor-field TrapDiv">
        <%: Html.CheckBoxFor(model => model.SNMPTrap4Active) %>
        <%: Html.ValidationMessageFor(model => model.SNMPTrap4Active) %>
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
