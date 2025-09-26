<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway Power Mode","Gateway Power Mode")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select" name="GatewayPowerMode" id="GatewayPowerMode" >
            <option value="0" <%= Model.GatewayPowerMode == eGatewayPowerMode.Standard ? "selected='selected'" : ""  %>><%: Html.TranslateTag("Standard","Standard")%></option>
            <option value="1" <%= Model.GatewayPowerMode == eGatewayPowerMode.Force_Low_Power ? "selected='selected'" : ""  %>><%: Html.TranslateTag("Force Low Power","Force Low Power")%></option>
            <option value="2" <%= Model.GatewayPowerMode == eGatewayPowerMode.Force_High_Power ? "selected='selected'" : ""  %>><%: Html.TranslateTag("Force High Power","Force High Power")%></option>
        </select>

        <%: Html.ValidationMessageFor(model => model.GatewayPowerMode)%>
    </div>
</div>

