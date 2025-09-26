<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsActive && MonnitSession.CustomerCan("Sensor_Active")){ %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
       <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_IsSensorActive|Mark this sensor active:","Mark this sensor active ")%>:
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Inactive","Inactive")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="IsActive" id="IsActive" <%= Model.IsActive? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Active","Active")%></label>
        </div>
    </div>
</div>
<%} %>