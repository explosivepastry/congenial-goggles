<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>


<%: Html.ValidationSummary(false)%>
<%: Html.Hidden("id", Model.SensorID)%>
<%
    double pitchVal = TriggeredTilt.GetOffset(Model);
    if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

<%} %>

<%  if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>
<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch Offset","Pitch Offset")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input type="number" name="PitchOffset" id="PitchOffset" value="<%: pitchVal %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>

<script>

    $(function () {

        $("#PitchOffset").change(function () {
            if (isANumber($("#PitchOffset").val())) {
                if ($("#PitchOffset").val() < -179.99)
                    $("#PitchOffset").val(-179.99);
                if ($("#PitchOffset").val() > 179.99)
                    $("#PitchOffset").val(179.99);



            }
            else {
                $("#PitchOffset").val(<%: pitchVal%>);
            }
        });
    });

</script>