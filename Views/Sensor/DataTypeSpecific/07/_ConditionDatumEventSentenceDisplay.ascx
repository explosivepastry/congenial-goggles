<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 7 - Angle-->

<%  double CompareValue = ((String.IsNullOrEmpty(Model.CompareValue)) || Model.CompareValue == double.MinValue.ToString()) ? 0.0d : Model.CompareValue.ToDouble();

    if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
        Model.CompareType = eCompareType.Less_Than;
%>


<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=CompareValue %> <%= Html.TranslateTag("Degrees") %>
   
