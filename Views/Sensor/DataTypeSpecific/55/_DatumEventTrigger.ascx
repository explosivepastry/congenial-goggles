<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<%

    double valcompare = 0.0;
    long DefaultMin = DifferentialPressure.DefaultMinThreshold;
    long DefaultMax = DifferentialPressure.DefaultMaxThreshold;

    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, eDatumType.DifferentialPressureData);
    if (!String.IsNullOrEmpty(Model.Scale))
    {
        UnitConversion currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();
        if (currentUnitOfMeasure == null)
        {
            currentUnitOfMeasure = listOfConversions[0];
        }
        valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;
    }

    if (valcompare < DefaultMin || valcompare > DefaultMax) valcompare = 0.0;

%>

<!--DatumType 55 - DifferentialPressureData-->
<div class="rule-card">
    <div class="rule-title">
        Notify when differential pressure is 
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>
    <input id="CompareValue" class="form-control user-dets" style="width: 200px;" name="CompareValue" type="number" value="<%:valcompare.ToString("0.#####") %>">
    <%: Html.ValidationMessageFor(model => model.CompareValue)%>


    <select name="scale" class="form-select user-dets grt-less" id="scale">
        <%Dictionary<string, string> Scales = DifferentialPressure.MeasurementScaleValue();
            foreach (string key in Scales.Keys)
            { %>

        <option value="<%:key %>" <%:Model.Scale.ToLower() == key.ToLower() ? "selected='selected'" : "" %>><%:Scales[key]%></option>
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

    $('#CompareValue').change(function (e) {
        var DefaultMin = <%= DefaultMin %>;
        var DefaultMax = <%= DefaultMax %>;
        if (isANumber($("#CompareValue").val())) {
            if ($("#CompareValue").val() < DefaultMin)
                $("#CompareValue").val(DefaultMin);
            if ($("#CompareValue").val() > DefaultMax)
                $("#CompareValue").val(DefaultMax);

            if (parseFloat($("#CompareValue").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                $("#CompareValue").val(parseFloat($("#MaximumThreshold_Manual").val()));

        } else {

            $("#CompareValue").val(0);
        }
    });

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
