<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 27 - ActivityDetect-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Activity is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 


