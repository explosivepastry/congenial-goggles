<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
    
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow) && Model.LastDataMessage != null)
        {%>
   
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_055|Last Sensor Reading:","Last Sensor Reading:")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.NotificationString : "" %>
        </div>
    </div>
    
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3 calOptions">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_055|Actual Input: (External reference value)","Actual Input: (External reference value)")%>
        </div>
        <div class="col sensorEditFormInput calOptions">
              <input name="actual" id="actual" class="form-control" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "" %>" />
            Amps
        </div>
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>