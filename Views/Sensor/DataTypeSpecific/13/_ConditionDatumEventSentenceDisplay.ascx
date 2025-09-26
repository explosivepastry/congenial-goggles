<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 13 Geforce-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=Model.CompareValue %>  G

