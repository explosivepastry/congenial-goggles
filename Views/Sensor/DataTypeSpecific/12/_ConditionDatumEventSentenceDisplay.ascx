<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 12 - Count-->

<%
    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
    {
        Model.CompareType = eCompareType.Less_Than;
    }

    if (Model != null && Model.CompareType == eCompareType.Greater_Than_or_Equal)
    {
        Model.CompareType = eCompareType.Greater_Than;
    }

    Type datumType = AppDatum.getType(Model.eDatumType);
    bool isBoolDatumType = (datumType == AppDatum.getType(eDatumType.BooleanData) || datumType.BaseType == AppDatum.getType(eDatumType.BooleanData));
    UnitConversion currentUnitOfMeasure = null;
    double valcompare = Model.CompareValue.ToDouble();

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
%>

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%= valcompare %> <%= !String.IsNullOrEmpty(Model.Scale) ? currentUnitOfMeasure.UnitFrom : "" %>