<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;

      
    }
    ViewData["HtmlAttributes"] = dic;

%>

    <form action="/Sensor/AdvancedEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false) %>

    <div class="formtitle">
         <span> <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span> <span><a style="margin: -3px;" class="bluebutton" href="/Sensor/MultiEdit?id=<%:Model.SensorID %>">Mass Edit</a></span>
    </div>
    <div class="formBody">    
            <input type="hidden" value="/Sensor/AdvancedEdit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">
            <%--Sensor Name--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);%>


            <%--Scale
             <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/2/_TemperatureScale.ascx", Model);%>--%>


            <%--Is Sensor Active--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_IsSensorActive.ascx", Model);%>
            <%--Heartbeat--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);%>

            <%--Use with Repeater--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_UseWithRepeater.ascx", Model);%>

            <%--Time of Day Active--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_TimeOfDay.ascx", Model);%>

            <%Html.RenderPartial("~/Views/Sensor/SensorEdit/_Aware.ascx", Model);%>
            <%--Active State Interval--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_ActiveStateInterval.ascx", Model);%>

            <%--Measurements Per Transmission--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_MeasurementsPerTransmission.ascx", Model);%>

            <%--Threshold--%>
            <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/2/_Threshhold.ascx", Model); %>

            <%--Hysteresis--%>
            <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/2/_Hysteresis.ascx", Model); %>

            <%--Syncronize Offset--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SyncronizeOffset.ascx", Model);%>

            <%--Recovery--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);%>

            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);%>
        </table>
        <%:Html.Partial("Tags", Model)%>
        <div style="clear:both;"></div>   
    </div>
   <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>