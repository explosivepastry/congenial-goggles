<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 48 - CrestFactor-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Crest Factor is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=Model.CompareValue %>

