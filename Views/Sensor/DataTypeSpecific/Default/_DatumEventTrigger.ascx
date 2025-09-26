<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--Default-->

<% 
    Type datumType = AppDatum.getType(Model.eDatumType);
    bool isBoolDatumType = (datumType == AppDatum.getType(eDatumType.BooleanData) || datumType.BaseType == AppDatum.getType(eDatumType.BooleanData));
    bool isProbeStatus = datumType.Name == "ProbeStatus";
    string unitOfMeasureToInitiallySelect = "";
    double valcompare = Model.CompareValue.ToDouble() == double.MinValue ? 0 : Model.CompareValue.ToDouble();
    string label = Model.Scale;
    bool shouldShowEqualSelectOption = Monnit.MonnitApplicationBase.ShouldShowEqualSelectOption(Model.eDatumType);
    string datumUniqueDisplayString = Monnit.MonnitApplicationBase.GetNotifyWhenString(Model.eDatumType);

    bool isTWA = Model.DatumIndex == 1;

    List<DropdownItemForRules> DropDownBasedOnDatumType = Monnit.MonnitApplicationBase.GetRuleDropDownValues(Model.eDatumType);
    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(Model.SensorID, Model.eDatumType);

    if (isTWA  && Model.ApplicationID == 116)
    {
        listOfConversions[0].UnitLabel = "TWA";
    }

    //In the rare case that a sensor has two of the same datum type that also allows custom scales for those datums. Ex: app 153. 
    var customConversions = listOfConversions.Where(c => c.UnitLabel == "Custom").ToList();
    if (customConversions.Count > 1)
    {
        if (Model.DatumIndex == 1)
        {
            listOfConversions.Remove(customConversions.Last());
        }
        else
        {
            listOfConversions.Remove(customConversions.First());
        }

    }

    if (!String.IsNullOrEmpty(Model.Scale) && !isBoolDatumType)
    {
        Sensor sensor = Sensor.Load(Model.SensorID);
        UnitConversion currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();

        if (currentUnitOfMeasure == null)
        {
            currentUnitOfMeasure = listOfConversions[0];
        }

        valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;

        foreach (UnitConversion key in listOfConversions)
        {
            if (Model.Scale == key.UnitLabel)
            {
                unitOfMeasureToInitiallySelect = key.UnitLabel;
            }
        }
        if (unitOfMeasureToInitiallySelect.Length < 1)
        {
            unitOfMeasureToInitiallySelect = "Custom";
        }
    }
%>




<!--Default-->
<div class="rule-card">
    <div class="rule-title">
        <%= Html.TranslateTag(datumUniqueDisplayString) %>
    </div>
    <div>
        <select class="form-select user-dets grt-less" id="<%= isBoolDatumType || isProbeStatus ? "CompareValue" : "CompareType" %>" name="CompareType">
            <% if (isBoolDatumType || isProbeStatus)
                { %>
            <%foreach (DropdownItemForRules item in DropDownBasedOnDatumType)
                { %>
            <option value="<%: item.Value %>" <%:Model != null && (Model.CompareValue == item.Value) ? "selected='selected'" : "" %>><%: item.DisplayString %></option>
            <% } %>
            <% }
                else
                { %>
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than", "Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than", "Less Than")%></option>
            <% if (shouldShowEqualSelectOption)
                {%>
            <option value="Equal" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Equal) ? "selected=selected" : "" %>><%: Html.TranslateTag("Equal To", "Equal To")%></option>
            <% } %>
            <% } %>
        </select>
    </div>

    <% if (!isBoolDatumType && !isProbeStatus)
        { %>
    <input id="CompareValue" name="CompareValue" class="form-control user-dets grt-less" type="number" value="<%:valcompare.ToString("0.##") %>">
    <% } %>

    <%: Html.ValidationMessageFor(model => model.CompareValue)%>


    <% if (listOfConversions.Count == 1 && listOfConversions[0].UnitTo == "" && listOfConversions[0].UnitLabel != "")
        { %>
    <input type="text" value="<%: listOfConversions[0].UnitLabel %>" style="background-image: none;" class="form-select user-dets grt-less" readonly="readonly" />
    <% }
        else if (listOfConversions.Count > 1)
        { %>
    <select name="scale" id="scale" class="form-select user-dets grt-less">
        <% foreach (UnitConversion key in listOfConversions)
            { %>
        <option value="<%: key.UnitLabel %>" <% if (unitOfMeasureToInitiallySelect == key.UnitLabel)
            { %>selected="selected"
            <% } %>><%: key.UnitLabel %></option>
        <% } %>
    </select>
    <% } %>
</div>

<input class="aSettings__input_input" id="<%= isBoolDatumType || isProbeStatus ? "CompareType" : ""%>" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />

<script type="text/javascript">
	<%: ExtensionMethods.LabelPartialIfDebug("Default_DatumEventTrigger.ascx") %>

    function datumConfigs() {
        const settings = {
            compareType: $('#CompareType').val(),
            compareValue: $('#previewMessage').data('to-post') ? $('#previewMessage').data('to-post') : $('#CompareValue').val(),
            scale: $('#scale').val()
        }
        return settings;
    }

    <% if (listOfConversions.Count > 0)
    { %>
    const ConversionDictionaryArray = [
    <% foreach (UnitConversion x in listOfConversions)
    {
        if (!double.IsNaN(x.Slope) & !double.IsNaN(x.Intercept))%>
        {
            "Slope": <%: x.Slope %>,
            "Intercept": <%: x.Intercept %>,
            "UnitFrom": "<%: x.UnitFrom %>",
            "UnitTo": "<%: x.UnitTo %>",
            "UnitLabel": "<%: x.UnitLabel %>"
        },
    <%}%>
    ];
    showBaseUnitsMessage("CompareValue", "scale", ConversionDictionaryArray);
   <% } %>
    console.log(ConversionDictionaryArray)

</script>
