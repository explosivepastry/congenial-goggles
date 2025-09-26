<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>

    <div class="formtitle">
        Calibrate Sensor
    </div>
    <form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
        <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

        <%: Html.ValidationSummary(false)%>
        <%: Html.Hidden("id", Model.SensorID)%>
        <%  if (Model.CanUpdate)
            {%>

        <div class="formbody">
            <%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0"))
              { %>
            <div class="editor-label">
                Observed Sensor Reading: (From sensor)
            </div>
            <div class="editor-field">
                <input name="observed" id="observed" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
                <%:Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID) %>
            </div>
            <div class="editor-label calOptions">
                Actual Input: (External reference value)
            </div>
            <div class="editor-field calOptions">
                <input name="actual" id="actual" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
                <%:Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID) %>
            </div>
            <%}%>
            <div style="clear: both;"></div>         
        </div>
         <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
    </form>


<%}
            else
            {
%>
<div class="formBody" style="font-weight:bold">
    Calibration for this sensor is not available for edit until pending transaction
                is complete.
</div>
<div class="buttons">&nbsp; </div>
<%
        }
%>

   