<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
  {%>
<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>
    <%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{%>


    <div class="">
        <div class=" mdBox">
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_086|Degrees", "Degrees")%>

            <select name="tempScale" id="scaleType" class="tzSelect">
                <option value="F" <%= Temperature.IsFahrenheit(Model.SensorID) ? "selected" : "" %>><%: Html.TranslateTag("Fahrenheit", "Fahrenheit")%></option>
                <option value="C" <%= !Temperature.IsFahrenheit(Model.SensorID) ? "selected" : "" %>><%: Html.TranslateTag("Celsius", "Celsius")%></option>
            </select>
        </div>
        <br />
        <div class="bold " style="margin-bottom:10px;">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_086|Offset:", "Offset:")%>   <%: Html.TextBox("offset")%> 
        </div>
    </div>



    <script>
        $(document).ready(function () {


            $('#offset').addClass('editField editFieldMedium');

            $("#offset").change(function () {
                if (!isANumber($("#offset").val())) {
                    $("#offset").val(<%: Model.Calibration1 != null ? Model.Calibration1.ToString("0.00") : "" %>);
                        } else {

                            if ($("#offset").val() > 50)
                                $("#offset").val(50);

                            if ($("#offset").val() < -50)
                                $("#offset").val(-50);

                        }
                    });

                    $("#scaleType").change(function () {
                        if ($("#scaleType").val() == "C") {
                            $("#offset").val(parseFloat(parseFloat($("#offset").val()) / 1.8).toFixed(1))

                        }
                        if ($("#scaleType").val() == "F") {
                            $("#offset").val(parseFloat(parseFloat($("#offset").val()) * 1.8).toFixed(1))
                        }
                    });



                });
    </script>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by", "This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%= new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate", "View Calibration Certificate")%></a>
    </div>
</div>
<%
		}
	}%>
