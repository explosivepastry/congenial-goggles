<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 32 MagnetDetect-->

<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Reading is ") + ((Model != null && Model.CompareValue.ToLower() == "false") ? "No Magnet Detected" : "Magnet Detected") %> 


