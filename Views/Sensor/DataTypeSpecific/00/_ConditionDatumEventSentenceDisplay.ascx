<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 00 Boolean-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Sensor is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Triggered" : "Triggered") %> 

