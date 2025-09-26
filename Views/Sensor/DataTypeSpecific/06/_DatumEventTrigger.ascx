<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%
    double valcompare = 0.0;

    string label = string.Empty;
    double lowVal = double.MinValue;
    double highVal = double.MinValue;
    List<long> sensorIDList = new List<long>();
    string tempLabel = string.Empty;
    double transformVal = double.MinValue;
    if (Model.SensorID != long.MinValue)
    {
        Sensor sens = Sensor.Load(Model.SensorID);
        label = PressureNPSI.GetLabel(Model.SensorID);
        lowVal = PressureNPSI.GetLowValue(Model.SensorID);
        highVal = PressureNPSI.GetHighValue(Model.SensorID);
        transformVal = PressureNPSI.GetTransform(Model.SensorID);
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == sens.ApplicationID; }).Select(sensid => sensid.SensorID).ToList();
    }
    else
    {
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == 83 || appid.ApplicationID == 79 || appid.ApplicationID == 82 || appid.ApplicationID == 144 || appid.ApplicationID == 145; }).Select(sensid => sensid.SensorID).ToList();
    }

    label = Model.Scale;
    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, eDatumType.Pressure);
    if (!String.IsNullOrEmpty(Model.Scale))
    {
        UnitConversion currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();
        if (currentUnitOfMeasure == null)
        {
            currentUnitOfMeasure = listOfConversions[0];
        }
        valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;
    }
%>

<!--DatumType 6 - pressure-->
<div class="rule-card">
    <div class="rule-title">
        <%= Html.TranslateTag("Notify when pressure is") %>
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>

    <input class="form-control user-dets grt-less" id="CompareValue" name="CompareValue" type="number" value="<%:valcompare%>" style="width: 60px;">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>

    <select class="form-select user-dets grt-less" name="scale" id="scale">
        <%Dictionary<string, string> Scales = Pressure300PSI.MeasurementScaleValue();
            foreach (string key in Scales.Keys)
            { %>
        <option value="<%:key %>" <%:Model.Scale == key ? "selected='selected'" : "" %>><%:Scales[key]%></option>
        <% 
            }%>
    </select>
</div>

<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scale=" + $('#scale').val();
        return settings;
    }

    const ConversionDictionaryArray = [
    <% foreach (UnitConversion x in listOfConversions)
    { %>
        {
            "Slope": <%: x.Slope %>,
            "Intercept": <%: x.Intercept %>,
            "UnitFrom": "<%: x.UnitFrom %>",
            "UnitTo": "<%: x.UnitTo %>",
            "UnitLabel": "<%: x.UnitLabel %>"
        },
    <% } %>
    ];
    showBaseUnitsMessage("CompareValue", "scale", ConversionDictionaryArray);
</script>
