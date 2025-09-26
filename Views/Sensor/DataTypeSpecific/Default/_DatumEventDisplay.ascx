<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--Default-->

<%
    Type datumType = AppDatum.getType(Model.eDatumType);
    bool isBoolDatumType = (datumType == AppDatum.getType(eDatumType.BooleanData) || datumType.BaseType == AppDatum.getType(eDatumType.BooleanData));
    bool isProbeStatus = datumType.Name == "ProbeStatus";
    UnitConversion currentUnitOfMeasure = null;
    double valcompare = Model.CompareValue.ToDouble();
    string compareValueToDisplay = Html.TranslateTag(MonnitSession.NotificationInProgress.CompareType.ToString().Replace("_", " "));
    Sensor sensor = Sensor.Load(Model.SensorID);
    List<UnitConversion> listOfConversions = Monnit.MonnitApplicationBase.GetScales(sensor, Model.eDatumType);

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

    currentUnitOfMeasure = listOfConversions.Where(conversion => conversion.UnitLabel == Model.Scale).FirstOrDefault();

    if (currentUnitOfMeasure == null)
    {
        currentUnitOfMeasure = listOfConversions[0];
    }

    valcompare = (Model.CompareValue.ToDouble() - currentUnitOfMeasure.Intercept) / currentUnitOfMeasure.Slope;

    string datumUniqueDisplayString = Monnit.MonnitApplicationBase.GetNotifyWhenString(Model.eDatumType);
    List<DropdownItemForRules> DropDownBasedOnDatumType = Monnit.MonnitApplicationBase.GetRuleDropDownValues(Model.eDatumType);
    string dropDownStringToDisplay = "";

    foreach (DropdownItemForRules item in DropDownBasedOnDatumType)
    {
        if (item.Value.ToLower() == Model.CompareValue.ToLower())
        {
            dropDownStringToDisplay = item.DisplayString;
        }
    }
%>

<div class="reading-tag1">
    <div class="hidden-xs ruleDevice__icon">
    </div>
    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

    <div class="triggerDevice__name">
        <strong style="margin-top: 10px;"><%= Html.TranslateTag(datumUniqueDisplayString) %>
            <br />
        </strong>
        <% if (isBoolDatumType || isProbeStatus)
            { %>
        <span class="reading-tag-condition"><%= Html.TranslateTag(dropDownStringToDisplay) %></span>
        <% }
            else
            { %>
        <span class="reading-tag-condition">
            <%= Html.TranslateTag(compareValueToDisplay) %>
            <% if (compareValueToDisplay == "Equal")
                { %>
               to
            <% } %>
            <%= valcompare %>
            <%= !String.IsNullOrEmpty(Model.Scale) ? currentUnitOfMeasure.UnitFrom : "" %></span>
        <% }

        %>
    </div>
</div>

