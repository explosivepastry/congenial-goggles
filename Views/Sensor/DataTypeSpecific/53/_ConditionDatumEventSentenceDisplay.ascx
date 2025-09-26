<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 53 - CarDetected-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Car is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 


