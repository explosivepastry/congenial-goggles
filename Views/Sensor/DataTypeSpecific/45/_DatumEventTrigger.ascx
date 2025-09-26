<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<% 
    double valcompare = 0.0;

    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, eDatumType.Millimeter);
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


<!--DatumType 45 - Millimeter-->

<div class="rule-card">
    <div class="aSettings__title">
        <%= Html.TranslateTag("Notify when reading is ") %>
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>

    <input id="CompareValue" class="form-control user-dets grt-less" name="CompareValue" type="number" value="<%:valcompare%>">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>

    <select class="form-select user-dets grt-less" name="scale" id="scale">
        <%Dictionary<string, string> Scales = Vibration800.NotificationScaleValues();
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
