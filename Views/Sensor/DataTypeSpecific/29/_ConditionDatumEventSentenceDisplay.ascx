<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 29 - ButtonPressed-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Pressed" : "Pressed") %> 

