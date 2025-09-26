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
        {
            Monnit.DataMessage flexMessage = Model.LastDataMessage;

            if (flexMessage != null)
            {
                string Display = flexMessage.DisplayData;
                if (Display.Length > 30)
                    Display = Display.Substring(0, 30);

    %>
    <%: Html.Hidden("messageId", flexMessage.DataMessageGUID)%>
    <div>
        To calibrate the flex sensor:
                            <ol>
                                <li>Position the sensor with flex element in application with sensor at full or maximum bend</li>
                                <li>Wait for one or two sensor readings to come in so the system has good sample for maximum bend</li>
                                <li>Choose "Maximum Bend" from the drop down list below and press "Calibrate"</li>
                                <li>Position the sensor with flex element in application with sensor at rest or minimum bend</li>
                                <li>Wait for one or two sensor readings to come in so the system has good sample for minimum bend</li>
                                <li>Choose "Minimum Bend" from the drop down list below and press "Calibrate"</li>
                                <li>Calibration complete.  Sensor will now show readings between "Minimum Bend" 0% and "Maximum Bend" 100%</li>
                            </ol>
    </div>
    <div>
        The last reading was recorded at
                            <%:Display%>
    </div>
    <div class="editor-label">
        This will be considered
    </div>
    <div class="editor-field">
        <select name="limit">
            <option value="3">Maximum Bend</option>
            <option value="4">Minimum Bend</option>
        </select>
    </div>
    <div class="clear"></div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
    </form>
    <% }else{ %>
    <div>Unable to calibrate sensor because last reading is unavailable.</div>
    <%}%>
    <%}else{%>
   <div class="formBody" style="font-weight:bold">
    Calibration for this sensor is not available for edit until pending transaction
                is complete.
</div>
<div class="buttons">&nbsp; </div>
    <%}%>



