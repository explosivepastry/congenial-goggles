<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 22 WattHours-->

<%  

    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
        Model.CompareType = eCompareType.Less_Than;

    double valcompare = 0.0;
    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, eDatumType.WattHours);
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

<div class="rule-card">
    <div class="rule-title">
        <%: Html.TranslateTag("Notify when reading is")%>:
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>
    <input class="form-control user-dets grt-less" id="CompareValue" name="CompareValue" type="text" value="<%:valcompare %>">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>
    <div>
        <select class="form-select user-dets grt-less" name="scale" id="scale">
            <%Dictionary<string, string> TempScales = ThreePhaseCurrentMeter.NotificationScaleValues();
                foreach (string key in TempScales.Keys)
                { %>
            <option value="<%:key %>" <%:Model.Scale == key ? "selected='selected'" : "" %>><%:TempScales[key]%></option>
            <%} %>
        </select>
    </div>
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
