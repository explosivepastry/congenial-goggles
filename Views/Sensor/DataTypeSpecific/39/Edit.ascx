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
          <span>Basic  <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span>
    </div>
    <div class="formBody">
        <input type="hidden" value="/Sensor/Edit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">
            <%--Sensor Name--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);%>

            <%--Heartbeat--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);%>

            <%--Min Threshold--%>
            <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/39/_MaxThreshold.ascx", Model);%>

            <%--Recovery--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);%>
        </table>
        <%:Html.Partial("Tags", Model)%>
        <div style="clear: both;"></div>
        e
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>
