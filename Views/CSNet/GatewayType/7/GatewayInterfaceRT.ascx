<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>



<div class="formBody">
    
    <div class="editor-label">
        TCP Timeout (Seconds)<br />
        (default: <%:Model.GatewayType.DefaultRealTimeInterfaceTimeout %> seconds)
    </div>
    <div class="editor-field">
        <%: Html.TextBox("RealTimeInterfaceTimeout", Math.Round(Model.RealTimeInterfaceTimeout))%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceTimeout)%>
    </div>

    <div class="editor-label">
        Port
        <br />
        (default: <%:Model.GatewayType.DefaultRealTimeInterfacePort %>)
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.RealTimeInterfacePort)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.RealTimeInterfacePort)%>
    </div>


</div>



