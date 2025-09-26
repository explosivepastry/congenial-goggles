<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 02 Temperature-->

<%  double CompareValue = ((String.IsNullOrEmpty(Model.CompareValue)) || Model.CompareValue == double.MinValue.ToString()) ? 0.0d : Model.CompareValue.ToDouble();
    if (Model.Scale == "F")
    {
        CompareValue = Monnit.Application_Classes.DataTypeClasses.TemperatureData.CelsiusToFahrenheit(CompareValue);
        CompareValue = Math.Round(CompareValue, 2);
    }

     if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
     {
         Model.CompareType = eCompareType.Less_Than;
     }

     if (Model != null && Model.CompareType == eCompareType.Greater_Than_or_Equal)
     {
         Model.CompareType = eCompareType.Greater_Than;
     }
%>
<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ")+ Model.CompareType.ToString().Replace("_", " ")%> <%= CompareValue%>° <%= Model.Scale%>
