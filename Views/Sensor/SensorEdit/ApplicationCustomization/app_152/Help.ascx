<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_App152_Extras.ascx", Model); %>








<div class="clearfix"></div>
