<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    

    <%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{%>

    <div class="form-inline">
        <div class="form-group">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Actual reading is", "Actual reading is")%> <%: Html.TextBox("actual")%> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|degrees", "degrees")%>
        </div>
        <div class="form-group">
            <select class="form-control" name="tempScale" id="tempScale">
                <option value="F"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit", "Fahrenheit")%></option>
                <option value="C"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius", "Celsius")%></option>
            </select>
        </div>
        <script>
            var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
            //MobiScroll
            //$(function () {
                //$('#tempScale').mobiscroll().select({
                //    theme: 'ios',
                //    display: popLocation,
                //    minWidth: 200
                //});
            //});

            //MobiScroll NumberPad
            $(function () {
                $('#actual').mobiscroll().numpad({

                    theme: 'ios',

                    display: popLocation,
                    min: -300,
                    max: 500,
                    scale: 1,
                    preset: 'decimal'
                });
            });


            $("#actual").change(function () {
                if (!isANumber($("#actual").val()))
                    $("#actual").val(<%: Model.LastDataMessage != null ? (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
            });
        </script>

    </div>
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
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate", "View Calibration Certificate")%></a>
    </div>
</div>
<%}
    }%>