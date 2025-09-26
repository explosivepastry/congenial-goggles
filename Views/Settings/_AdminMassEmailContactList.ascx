<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Controllers.SettingsController.MassEmailContact>" %>

<%foreach (AccountThemeContact atc in Model.Contacts)
  { %>
<div class="bold col-md-6 col-sm-6 col-xs-12"><%= atc.FirstName + " " +  atc.LastName%></div>
<div class="col-md-6 col-sm-6 col-xs-12"><%= atc.Email %></div>
<div class="clearfix"></div>
<%} %>

