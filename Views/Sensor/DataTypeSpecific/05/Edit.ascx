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
<form action="/Sensor/Edit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false)%>


<div class="formtitle">
      <span>Basic  <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span>
</div>
<div class="formBody">
    <input type="hidden" value="/Sensor/Edit/<%:Model.SensorID %>" name="returns" id="returns" />
    <table style="width: 100%;">
        <%
        Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);
        Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);
        Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);
        Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);
        %>
    </table>
    <%:Html.Partial("Tags", Model)%>
    <div style="clear: both;"></div>
</div>
 <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>

</form>


