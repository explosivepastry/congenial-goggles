<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 30 DryContact-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "Loop Open" : "Loop Closed") %> 


