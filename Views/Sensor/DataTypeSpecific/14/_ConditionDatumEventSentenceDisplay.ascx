<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 14 LuxData-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=Model.CompareValue %>  Lux


