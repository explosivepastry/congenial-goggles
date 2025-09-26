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
<form action="/Sensor/Edit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>

    <div class="formtitle">
           <span>Basic <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span> <span><a style="margin: -3px;" class="bluebutton" href="/Sensor/MultiEdit?id=<%:Model.SensorID %>">Mass Edit</a></span>
    </div>
    <div class="formBody">
        <input type="hidden" value="/Sensor/Edit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">
            <%--Sensor Name--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx"); %>

            <%--Is Sensor Active--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_IsSensorActive.ascx"); %>

            <%--Heartbeat--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx"); %>


            <%--AwareStateTHreshHold --%>
            <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/2/_AwareStateThreshHold.ascx"); %>

            <%--Use with Repeater--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_UseWithRepeater.ascx"); %>

            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx"); %>
        </table>
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>

