<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 49 - Centimeter-->

<% 
    double valcompare = 0.0;

    string label = Model.Scale;
    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, eDatumType.Centimeter);
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
        <%= Html.TranslateTag("Notify when distance/depth is") %>
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>
    <input id="CompareValue" class="form-control user-dets grt-less" name="CompareValue" type="number" value="<%:valcompare %>">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>
    <select name="scale" id="scale" class="form-select user-dets grt-less">
        <%Dictionary<string, string> Scales = UltrasonicRangerIndustrial.NotificationScaleValues();
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
