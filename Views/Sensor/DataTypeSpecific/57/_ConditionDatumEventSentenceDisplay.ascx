<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 57 - ProbeStatus-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Observation Mode ") + ((Model != null && Model.CompareValue.ToLower() == "end") ? "Ends" : "Begins") %> 


