<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<%if (!Model.IsWiFiSensor && new Version(Model.FirmwareVersion) < new Version("2.0.0.0"))
  {%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater|Use this sensor through a repeater","Use this sensor through a repeater")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("No","No")%></label>
            <input class="form-check-input my-0 y-0 mx-2 user-dets" type="checkbox" name="UseRepeater" id="UseRepeater" <%=Model.CanUpdate ? "" : "disabled" %>   <%:Model.StandardMessageDelay > 100 ? "checked='checked'" : ""%>>
            <label class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
        </div>
    </div>
</div>

<% } %>