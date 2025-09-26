<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 24 - DoorData-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Door is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? Html.TranslateTag("Open") : Html.TranslateTag("Closed")) %>


