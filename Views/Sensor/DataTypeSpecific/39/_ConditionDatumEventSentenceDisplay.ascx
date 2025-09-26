<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 39 - VehicleDetect-->
<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Vehicle is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 

