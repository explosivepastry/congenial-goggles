<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();%>

<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form2" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden2" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>


    <%  if (Model.CanUpdate)
        {%>

    <div class="formBody">
        <div class="form-group">
            <div class="bold col-12">
                <input class="form-check-input" type="radio" name="calType" value="1">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Temperature","Temperature")%>
                <br />
                <input class="form-check-input" type="radio" name="calType" value="2">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|BaseLine","BaseLine")%>
            </div>
        </div>

        <div id="cal-1" style="display: none;">
            <div class="row sensorEditForm ms-0">
                <div class="col-12 col-md-3 ps-0">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Actual reading is","Actual reading is")%> 
                    <div class="d-flex align-items-center">
                        <%: Html.TextBox("actual")%> <span class="ms-1"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|degrees","degrees")%></span>
                    </div>
                </div>
            </div>
            <div class="col sensorEditFormInput">
                <select name="tempScale" class="form-select">
                    <option value="F"><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></option>
                    <option value="C"><%: Html.TranslateTag("Celsius","Celsius")%></option>
                </select>
            </div>
            <div class="clear"></div>
            <br />
            <script>

                $('#actual').addClass('editField editFieldMedium');

                $("#actual").change(function () {
                    if (!isANumber($("#actual").val()))
                        $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
                });
            </script>
        </div>

        <div id="cal-2" style="display: none;">

            <div class="form-group">
                <div class="bold col-sm-3 col-12">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|To set harmonic baseLines click calibrate.","To set harmonic baseLines click calibrate.")%>
                </div>
                <div class="col-sm-9 col-12 mdBox">
                </div>
            </div>
        </div>
    </div>
            <div class="clear"></div>
        <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[name=calType]").on("change", function () {

            $('#cal-1').hide();
            $('#cal-2').hide();
            $("#cal-" + $(this).val()).show();
        });
    });

</script>

<%}%>
<% else
    {%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%}%>
<%}
    else
    {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>
