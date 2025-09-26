<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%--All  Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_IsSensorActive.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model); %>

<% 
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_097/_Unoccupied.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_097/_Occupied.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_097/_Advanced.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery.ascx", Model);
  %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor.ascx", Model);%>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>