<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%		
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

    //Profile Specific
%>

<%
    //Profile Specific Hidden
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_009/_EventDetection.ascx", Model);
%>

<% 	
    //Hidden Defaults
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_RearmTime.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
