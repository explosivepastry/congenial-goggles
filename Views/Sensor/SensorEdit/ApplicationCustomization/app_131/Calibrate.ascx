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

    <%   if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {
             int probe1 = FilteredQuadTemperature.GetCalVal1FirstByte(Model);
             int probe2 = FilteredQuadTemperature.GetCalVal1SecondByte(Model);
             int probe3 = FilteredQuadTemperature.GetCalVal1ThirdByte(Model);
             int probe4 = FilteredQuadTemperature.GetCalVal1FourthByte(Model);
             bool isFarh = FilteredQuadTemperature.IsFahrenheit(Model.SensorID);
    %>



    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Choose Calibration Type","Choose Calibration Type")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <select id="type" name="type">
                <option value="basic">Basic Offset</option>
                <option value="cali">Full Calibration</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />


    <div id="basic">
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Basic OffSet","Basic OffSet")%> (<%: FilteredQuadTemperature.GetProbe1Label(Model.SensorID) %>)
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <%: Html.TextBox("probe1", probe1 ) %>
            </div>
        </div>
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Basic OffSet","Basic OffSet")%>  (<%: FilteredQuadTemperature.GetProbe2Label(Model.SensorID) %>)
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <%: Html.TextBox("probe2", probe2) %>
            </div>
        </div>
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Basic OffSet","Basic OffSet")%> (<%: FilteredQuadTemperature.GetProbe3Label(Model.SensorID) %>)
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <%: Html.TextBox("probe3", probe3) %>
            </div>
        </div>
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Basic OffSet","Basic OffSet")%> (<%: FilteredQuadTemperature.GetProbe4Label(Model.SensorID) %>)
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <%: Html.TextBox("probe4", probe4) %>
            </div>
        </div>

    </div>
    <div class="clear"></div>
    <br />




    <div class="form-group" id="cali">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092| Actual reading is"," Actual reading is")%> <%: Html.TextBox("actual")%> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|degrees","degrees")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <select name="tempScale">
                <option value="F"><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></option>
                <option value="C"><%: Html.TranslateTag("Celsius","Celsius")%></option>
            </select>

        </div>
    </div>
    <div class="clear"></div>
    <br />


    <script>


        $('#cali').hide();

        $('#actual').addClass('editField editFieldMedium');
        $('#probe1').addClass('editField editFieldMedium');
        $('#probe2').addClass('editField editFieldMedium');
        $('#probe3').addClass('editField editFieldMedium');
        $('#probe4').addClass('editField editFieldMedium');
        var isF = '<%: isFarh%>';
        $('#type').change(function () {
            var type = $('#type').val();
            if (type == "cali") {
                $('#basic').hide();
                $('#cali').show();
            }
            else {
                $('#basic').show();
                $('#cali').hide();
            }
        });

        $('#probe1').change(function () {
            if (!isANumber($('#probe1').val())) {
                $('#probe1').val(<%: probe1 %>);
            }
            else {
                if (isF == 'True') {
                    if ($('#probe1').val() < -40)
                        $('#probe1').val(-40);

                    if ($('#probe1').val() > 257)
                        $('#probe1').val(257);
                }
                else {
                    if ($('#probe1').val() < -40)
                        $('#probe1').val(-40);

                    if ($('#probe1').val() > 125)
                        $('#probe1').val(125);
                }
            }
        });

        $('#probe2').change(function () {
            if (!isANumber($('#probe2').val())) {
                $('#probe2').val(<%: probe2 %>);
            }
            else {
                if (isF == 'True') {
                    if ($('#probe2').val() < -40)
                        $('#probe2').val(-40);

                    if ($('#probe2').val() > 257)
                        $('#probe2').val(257);
                }
                else {
                    if ($('#probe2').val() < -40)
                        $('#probe2').val(-40);

                    if ($('#probe2').val() > 125)
                        $('#probe2').val(125);
                }
            }
        });

        $('#probe3').change(function () {
            if (!isANumber($('#probe3').val())) {
                $('#probe3').val(<%: probe3 %>);
                }
                else {
                    if (isF == 'True') {
                        if ($('#probe3').val() < -40)
                            $('#probe3').val(-40);

                        if ($('#probe3').val() > 257)
                            $('#probe3').val(257);
                    }
                    else {
                        if ($('#probe3').val() < -40)
                            $('#probe3').val(-40);

                        if ($('#probe3').val() > 125)
                            $('#probe3').val(125);
                    }
                }
            });

            $('#probe4').change(function () {
                if (!isANumber($('#probe4').val())) {
                    $('#probe4').val(<%: probe4 %>);
                }
                else {
                    if (isF == 'True') {
                        if ($('#probe4').val() < -40)
                            $('#probe4').val(-40);

                        if ($('#probe4').val() > 257)
                            $('#probe4').val(257);
                    }
                    else {
                        if ($('#probe4').val() < -40)
                            $('#probe4').val(-40);

                        if ($('#probe4').val() > 125)
                            $('#probe4').val(125);
                    }
                }
            });

            $("#actual").change(function () {
                if (!isANumber($("#actual").val())) {
                    $("#actual").val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00") : "" %>);
                }
                else {
                    if (isF == 'True') {
                        if ($('#actual').val() < -40)
                            $('#actual').val(-40);

                        if ($('#actual').val() > 257)
                            $('#actual').val(257);
                    }
                    else {
                        if ($('#actual').val() < -40)
                            $('#actual').val(-40);

                        if ($('#actual').val() > 125)
                            $('#actual').val(125);
                    }
                }
            });
    </script>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>

<%}
  else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow)) 
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
