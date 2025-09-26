<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 36 - AirflowDetect-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 
