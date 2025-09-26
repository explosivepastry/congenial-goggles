<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.SensorGroupSensorModel>>" %>
<% Html.RenderPartial("_SensorDetails", Model); %>