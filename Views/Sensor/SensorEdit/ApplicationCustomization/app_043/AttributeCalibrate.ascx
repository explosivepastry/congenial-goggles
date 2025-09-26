<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    SensorAttribute.ResetAttributeList(Model.SensorID);
    double lastHumidity = 0;
    double lastTemperature = 0;

    if (Model.LastDataMessage != null)
    {
        List<object> PlotValues = Model.LastDataMessage.AppBase.GetPlotValues(Model.SensorID);
        if (PlotValues.Count >= 2)
        {
            double humTest = PlotValues.ElementAt(0).ToDouble();
            double tempTest = PlotValues.ElementAt(1).ToDouble();

            if(!Double.IsNaN(humTest))
            {
                lastHumidity = humTest;
            }

            if (!Double.IsNaN(tempTest))
            {
                lastTemperature = tempTest;
            }
        }
    }

    /* NOT USED - We don't display the current offset, just allow user to submit "correct" values and we calculate the offset
    bool isF = HumiditySHT25.IsFahrenheit(Model.SensorID);
    List<SensorAttribute> att = SensorAttribute.LoadBySensorID(Model.SensorID);

    double TempOffset = 0;
    double HumOffset = 0;

    if (att.Count > 0)
    {
        foreach (SensorAttribute sa in att)
        {
            if (sa.Name.ToString() == "TempOffset")
            {
                TempOffset = sa.Value.ToDouble();
                if (isF)
                {
                    TempOffset = TempOffset * 1.8;
                }
            }

            if (sa.Name.ToString() == "HumOffset")
            {
                HumOffset = sa.Value.ToDouble();
            }
        }
    }
    */
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



    <%  if (true || Model.CanUpdate && Model.LastDataMessage != null)
        {%>


    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Humidity","Humidity")%></h2>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="observed" id="observed" class="form-control" readonly="readonly" value="<%: lastHumidity.ToString("0.00") %>" />%
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is","Actual reading is")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="actual" id="actual" class="form-control" value="<%: lastHumidity.ToString("0.00") %>" required />
            %
        </div>
    </div>

    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature","Temperature")%></h2>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="observedTemp" id="observedTemp" class="form-control" readonly="readonly" value="<%: lastTemperature.ToString("0.00") %>" />
            <%:Monnit.HumiditySHT25.IsFahrenheit(Model.SensorID)? "F" : "C" %>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is","Actual reading is")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="actualTemp" id="actualTemp" class="form-control" value="<%: lastTemperature.ToString("0.00") %>" required />
            <%:Monnit.HumiditySHT25.IsFahrenheit(Model.SensorID)? "F" : "C" %>
        </div>
    </div>

    <script>
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $("#actual").change(function () {
            if (!isANumber($("#actual").val()))
                $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
        });

        $("#actualTemp").change(function () {
            if (!isANumber($("#actualTemp").val()))
                $("#actualTemp").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
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