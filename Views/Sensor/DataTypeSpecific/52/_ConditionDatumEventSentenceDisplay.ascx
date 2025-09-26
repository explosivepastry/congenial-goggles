<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 52 - PPB-->
<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + Model.CompareType.ToString().Replace("_"," ")%> <%=Model.CompareValue %> PPB

