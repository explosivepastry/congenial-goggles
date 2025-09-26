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
    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%  if (Model.CanUpdate)
        {%>

    <div>
        <%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0") || Model.SensorTypeID == 4)//Post 2.0 or WIFI
          { 
        %>
        <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />
        <div class="editor-label">
            Observed Sensor Reading:
        </div>
        <div class="editor-field">
            <input name="observed" id="observed" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
        </div>
        <div class="editor-label calOptions">
            Actual Input:
        </div>
        <div class="editor-field calOptions">
            <input name="actual" id="actual" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
        </div>

        <script>
            $('#observed').addClass('editField editFieldMedium');
            $('#actual').addClass('editField editFieldMedium');

            $("#actual").change(function () {
                if (!isANumber($("#actual").val()))
                    $("#actual").val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>);
     });
 </script>

        <div style="clear: both;"></div>
        <div style="clear: both;"></div>

    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
    <%}
          else
          {%>
    <div class="formBody" style="font-weight: bold">
        Calibration for this sensor is not available.
    </div>
    <%} %>
    <%}
        else
        {
    %>
    <div class="formBody" style="font-weight: bold">
        Calibration for this sensor is not available for edit until pending transaction
                is complete.
    </div>
    <div class="buttons">&nbsp; </div>
    <%
    }
    %>
</form>












