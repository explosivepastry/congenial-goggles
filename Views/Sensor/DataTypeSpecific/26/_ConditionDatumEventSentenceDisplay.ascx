<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 26 - VoltageDetect-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Voltage is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 


