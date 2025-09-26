<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
    
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

    <%if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%  
        string PitchVal = string.Empty;
        string RollVal = string.Empty;
        foreach (SensorAttribute sensAttr in SensorAttribute.LoadBySensorID(Model.SensorID))
        {
            if (sensAttr.Name == "RollOffSet")
                RollVal = sensAttr.Value.ToString();
            if (sensAttr.Name == "PitchOffSet")
                PitchVal = sensAttr.Value.ToString();
        }
    %>

       <div class="row sensorEditForm">
        <div class="col-12">
            <h3> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Allows for Pitch and Roll offsets to be placed on the sensor.","Allows for Pitch and Roll offsets to be placed on the sensor.")%></h3>
           <br /> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|to reset save Pitch and Roll offsets as zero.","to reset save Pitch and Roll offsets as zero.")%>
        </div>
    </div>
    
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch Offset","Pitch Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="PitchOffset" class="form-control" id="PitchOffset" value="<%: PitchVal %>" />
        </div>
    </div>
    
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Roll Offset:","Roll Offset:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="RollOffset" class="form-control" id="RollOffset" value="<%: RollVal %>" />
        </div>
    </div>
       

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>