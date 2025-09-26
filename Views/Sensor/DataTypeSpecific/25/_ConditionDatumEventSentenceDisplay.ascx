<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 25 - Water Detect-->


<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Water is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Not Detected" : "Detected") %> 


