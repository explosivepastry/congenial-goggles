<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>


<% 

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Unoccupied97.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Occupied97.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Advanced97.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Additional97.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Transmissions_Before_Link.ascx", Model);
   %>






<div class="clearfix"></div>
