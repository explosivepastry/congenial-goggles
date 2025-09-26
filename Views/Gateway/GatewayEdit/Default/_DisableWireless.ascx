<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_DisableWireless|On Server Loss","On Server Loss")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Log Sensor Data","Log Sensor Data")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" value="true" id="DisableNetworkOnServerError" name="DisableNetworkOnServerError" <%=Model.DisableNetworkOnServerError ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Disable Wireless","Disable Wireless")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.DisableNetworkOnServerError)%>
    </div>
</div>

