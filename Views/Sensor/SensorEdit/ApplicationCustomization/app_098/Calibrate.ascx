<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%   if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%  
             RiceTilt data = RiceTilt.Deserialize(Model.FirmwareVersion, Model.LastDataMessage.Data);
             double lastPitch = data.Pitch;
          
    %>


    <input type="hidden" name="lastReading" value="<%=lastPitch %>" />

    <div class="editor-label">

        <label></label>
    </div>
    <div class="form-group">
        <div class="bold col-md-12 col-sm-12 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Allows for Pitch offset to be placed on the sensor.","Allows for Pitch offset to be placed on the sensor.")%>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|To zero out offset click Calibrate","To zero out offset click Calibrate")%>
        </div>

    </div>
    <div class="clear"></div>
    <br />
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Last Reading:","Last Reading:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <%: lastPitch %>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>