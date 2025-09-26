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

    <% if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>

    <div>
        <%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0") || Model.SensorTypeID == 4)//Post 2.0 or WIFI
          { %>


        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_089|Calibrate Current for","Calibrate Current for")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="spanZero" name="spanZero" class="form-select">
                    <option value="zero">Low Current</option>
                    <option value="span">High Current</option>
                </select>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3 calOptions">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_089| Actual Input:"," Actual Input:")%>
            </div>
            <div class="col sensorEditFormInput calOptions">
                <input name="spanZeroTxt" id="spanZeroTxt" class="form-control" />
            </div>
        </div>
        <script>
            $('#spanZero').addClass('editField editFieldMedium');
            $('#spanZeroTxt').addClass('editField editFieldMedium');

            $("#spanZeroTxt").change(function () {
                if (!isANumber($("#spanZeroTxt").val())) {
                    $("#spanZeroTxt").val(0);
                }
                else {
                    var amt = $("#spanZeroTxt").val();

                    if (amt < 0)
                        $("#spanZeroTxt").val(0);

                    if (amt > 120)
                        $("#spanZeroTxt").val(120);

                }
            });
        </script>


        <%} %>

        <div style="clear: both;" />
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>