<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


    <div class="formBody">
               
        <div class="editor-label">
            TCP Timeout (Minutes)<br />(default: 5 Minutes)
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
            <%: Html.TextBox("ModbusInterfacePort", Model.ModbusInterfacePort)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.ModbusInterfacePort)%>
        </div>
    </div>
    
    


