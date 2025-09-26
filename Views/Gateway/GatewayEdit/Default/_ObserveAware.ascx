<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_ObserveAware|On Aware Messages","On Aware Messages")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Wait for Heartbeat","Wait for Heartbeat")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" value="true"  id="ObserveAware" name="ObserveAware" <%=Model.ObserveAware ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Trigger Heartbeat","Trigger Heartbeat")%></label>
        </div>
        <%: Html.ValidationMessageFor(model => model.ObserveAware)%>
    </div>
</div>
