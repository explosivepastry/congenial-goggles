<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%     DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    //purgeclassic
    Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();


    SensorAttribute.ResetAttributeList(Model.SensorID);
    List<SensorAttribute> att = SensorAttribute.LoadBySensorID(Model.SensorID);

%>


<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

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



    <%  if (Model.CanUpdate && Model.LastDataMessage != null)
        {%>



    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Humidity","Humidity")%></h2>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="observed" id="observed" class="form-control" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00") : "" %>" />%
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Humidity Offset","Humidity Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="HumOffset" id="HumOffset" class="form-control"
                value="<%: Model.Calibration2 == 0xFFFF0000L || Model.Calibration2 == 4294901760 || Model.Calibration2 == -65536 ? "0.00" : (Model.Calibration2 / 100.0).ToString("0.00") %>" required />
            %
        </div>
    </div>
    <%

        bool isF = HumiditySHT25.IsFahrenheit(Model.SensorID);

        string tempVal = HumiditySHT25.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temperature.ToString();
        string tempOffsetVal = Model.Calibration1 == 0xFFFF0000L ? "0.00" : (Model.Calibration1 / 100.0).ToString("0.00");

        if (isF)
        {
            tempVal = tempVal.ToDouble().ToFahrenheit().ToString("0.00");
            tempOffsetVal = Math.Round((tempOffsetVal.ToDouble() * 1.8), 2).ToString("0.00");
        }
    %>



    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature","Temperature")%></h2>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="observedTemp" id="observedTemp" class="form-control" readonly="readonly" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" />
            <%:Monnit.HumiditySHT25.IsFahrenheit(Model.SensorID)? "F" : "C" %>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature Offset","Temperature Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input id="TempOffset" name="TempOffset" class="form-control" type="text"
                value="<%: Model.Calibration1 == 0xFFFF0000L || Model.Calibration1 == 4294901760 || Model.Calibration1 == -65536 ? "0.00" : tempOffsetVal %>" required />
            <%: isF ? "F" : "C" %>
        </div>
    </div>

    <script>


        $(function () {



            $("#TempOffset").change(function () {
                if (!isANumber($("#TempOffset").val())) {
                    $("#TempOffset").val(<%: tempOffsetVal%>);
                } else {

                    if ($("#TempOffset").val() > 50)
                        $("#TempOffset").val(50);

                    if ($("#TempOffset").val() < -50)
                        $("#TempOffset").val(-50);

                }
            });


            $("#HumOffset").change(function () {
                if (!isANumber($("#HumOffset").val())) {
                    $("#HumOffset").val(<%: Model.Calibration1 != 0xFFFF0000L ? (Model.Calibration2 / 100.0).ToString("0.00") : "" %>);
                } else {

                    if ($("#HumOffset").val() > 50)
                        $("#HumOffset").val(50);

                    if ($("#HumOffset").val() < -50)
                        $("#HumOffset").val(-50);

                }
            });


        });

    </script>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>


<%}
    else
    {
%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%
    }
%>


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