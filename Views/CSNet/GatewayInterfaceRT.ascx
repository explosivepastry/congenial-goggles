<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<% using (Html.BeginForm()) {%>
    <div class="formBody">
        <%: Html.ValidationSummary(true) %>
        <%: Html.HiddenFor(model => model.Name)%>
            
        <div class="editor-label">
            Interface Active
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.RealTimeInterfaceActive)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceActive)%>
        </div>
            
            
        <div class="editor-label">
            TCP Timeout Seconds<br />(default: 70 seconds)
        </div>
        <div class="editor-field">
            <%: Html.TextBox("RealTimeInterfaceTimeout", Math.Round(Model.RealTimeInterfaceTimeout * 60))%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceTimeout)%>
        </div>
                
        <div class="editor-label">
            Port <br />
            (default: <%:Model.GatewayType.DefaultRealTimeInterfacePort %>)
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.RealTimeInterfacePort)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.RealTimeInterfacePort)%>
        </div>
                

                
        <div style="clear:both;"></div>

    </div>
    
    <div class="buttons">
        <a href="#" onclick="hideModal();" class="greybutton">Cancel</a>
        <input type="button" id="SaveRT" value="Save" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>
<% } %>
<script type="text/javascript">
    $(document).ready(function () {

        $('#SaveRT').click(function (e) {
            postForm($(this).closest('form'));
        });
    });
    
</script>
