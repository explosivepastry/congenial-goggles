<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 56 MoistureTension-->
<%   double CompareValue = (String.IsNullOrEmpty(Model.CompareValue)) ? 0.0d : Model.CompareValue.ToDouble();
     CompareValue = Math.Round(CompareValue, 2);

    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
        Model.CompareType = eCompareType.Less_Than;
%>


<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%>  <%=CompareValue %> <%: Html.TranslateTag("centibars","centibars")%>
