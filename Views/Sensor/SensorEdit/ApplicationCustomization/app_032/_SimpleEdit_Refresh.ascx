<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%		
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

    //Profile Specific
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_032/_MinThreshold.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_032/_MaxThreshold.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_032/Calibrate.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_032/Scale.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_032/_PowerOption.ascx", Model);

    //Profile Specific Hidden

%>

<% 	
    //Hidden Defaults
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_IsSensorActive.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_MeasurementsPerTransmission.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
