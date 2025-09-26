<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<% using (Html.BeginForm()) {%>
    <div class="formBody">
        <%: Html.ValidationSummary(true) %>
        <%: Html.HiddenFor(model => model.Name)%>
        
            
        <div class="editor-label">
            Interface Active
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.ModbusInterfaceActive)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.ModbusInterfaceActive)%>
        </div>
          
        <div class="editor-label">
            Queue Expiration <br />(default: 720 minutes)
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.SingleQueueExpiration)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.SingleQueueExpiration)%>
        </div>
            
        <div class="editor-label">
            TCP Timeout Minutes<br />(default: 5 Minutes)
        </div>
        <div class="editor-field">
            <%: Html.TextBox("ModbusInterfaceTimeout", Model.ModbusInterfaceTimeout)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.ModbusInterfaceTimeout)%>
        </div>
                
        <div class="editor-label">
            Port <br />
            (default: 502)
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.ModbusInterfacePort)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.ModbusInterfacePort)%>
        </div>
                

        <div style="clear:both;"></div>

    </div>
    
    <div class="buttons">
        <a href="#" onclick="hideModal();" class="greybutton">Cancel</a>
        <input type="button" id="SaveModbus" value="Save" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>
<% } %>
<script type="text/javascript">
    $(document).ready(function () {

        $('#SaveModbus').click(function (e) {
            postForm($(this).closest('form'));
        });
    });

</script>
