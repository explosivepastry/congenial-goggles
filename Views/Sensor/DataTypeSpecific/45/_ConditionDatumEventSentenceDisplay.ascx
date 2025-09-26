<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 45 - Millimeter-->

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

    if (valcompare.ToDouble() < 0) valcompare = 0;
%>

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=valcompare %> <%= Model.Scale %>

