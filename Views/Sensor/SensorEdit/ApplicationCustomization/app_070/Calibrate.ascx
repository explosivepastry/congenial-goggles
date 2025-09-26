<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
    
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <% if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>

    <div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Observed Sensor Reading:","Observed Sensor Reading:")%>
            </div>
            <div class="col sensorEditFormInput">
               <input name="observed" id="observed" class="form-control" value="<%:( Model.LastDataMessage != null && Model.LastDataMessage.AppBase.IsValid) ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "0" %>" />
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3 calOptions">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Actual Input:","Actual Input:")%>
            </div>
            <div class="col sensorEditFormInput calOptions">
                <input name="actual" id="actual" class="form-control" value="<%:( Model.LastDataMessage != null && Model.LastDataMessage.AppBase.IsValid)  ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "0" %>" />
            </div>
        </div>

        <div style="clear: both;" />
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>