<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;


    }

    ViewData["HtmlAttributes"] = dic;
%>
<form action="/Sensor/AdvancedEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false)%>


    <div class="formtitle">
          <span> <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span>
    </div>
    <div class="formBody">
        <input type="hidden" value="/Sensor/AdvancedEdit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">
            <% 
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_UseWithRepeater.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_TimeOfDay.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_ActiveStateInterval.ascx", Model);

                if (Model.MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
                {
                    Html.RenderPartial("~/Views/Sensor/SensorEdit/_MeasurementsPerTransmission.ascx", Model);
                    Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/24/_Thresholds.ascx", Model);
                    // Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/24/_Hysteresis.ascx", Model);
                    Html.RenderPartial("~/Views/Sensor/SensorEdit/_SyncronizeOffset.ascx", Model);
                }
                else
                {
                    Html.RenderPartial("~/Views/Sensor/SensorEdit/_EventDetection.ascx", Model);
                    Html.RenderPartial("~/Views/Sensor/SensorEdit/_RearmTime.ascx", Model);
                }

                Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);
            %>
        </table>
        <%:Html.Partial("Tags", Model)%>
        <div style="clear: both;"></div>
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>
