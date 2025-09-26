<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SensorGroupSensor>>" %>


<%foreach (SensorGroupSensor sgs in Model)
  {
      Sensor item = sgs.Sensor;
      string icon = "app" + item.ApplicationID; 
%>
<div class="col-lg-4 col-md-6 col-sm-12 col-xs-12">
    <div id="sensor_<%:item.SensorID%>" style="height: 70px; width: 100%;" title="<%:item.SensorName%>">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
            <div style="font-size: 1em !important;"><span class="sensor icon icon-<%:icon%> icon-xs"></span></div>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
            <label style="font-weight: normal !important; font-size: large !important; text-overflow: ellipsis; overflow: hidden; white-space: nowrap; padding-top: 12px;"><%:item.SensorName%> : </label>
        </div>
    </div>
</div>

<%} %>