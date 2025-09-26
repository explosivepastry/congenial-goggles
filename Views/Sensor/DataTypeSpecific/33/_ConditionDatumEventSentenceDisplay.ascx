<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 33 - MotionDetected-->
<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "No Motion Detected" : "Motion Detected") %> 
