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

    <%   if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>

      
            <%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0"))
              { %>
     <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079| Observed Sensor Reading: (From sensor)"," Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
              <input name="observed" id="observed" class="form-control" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>" />
                <%:Monnit.PressureNPSI.GetLabel(Model.SensorID) %>
        </div>
    </div>
     <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Actual Input: (External reference value)","Actual Input: (External reference value)")%>
        </div>
        <div class="col sensorEditFormInput">
                 <input name="actual" id="actual" class="form-control" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>" />
                <%:Monnit.PressureNPSI.GetLabel(Model.SensorID) %>
        </div>
    </div>

            <%}%>
  
   
        <script>
            $(function () {
                $('#actual').change(function () {

                    if (isANumber($('#actual').val())) {
                        var maxCalibrate = <%: PressureNPSI.GetSavedValue(Model.SensorID)%>;


                        if ($('#actual').val() < 0)
                            $('#actual').val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>);

                        if ($('#actual').val() > maxCalibrate)
                            $('#actual').val(maxCalibrate);
                    }
                    else {

                        $('#actual').val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>);
                    }
                });
            });
        </script>




        <div style="clear: both;" />
   
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>