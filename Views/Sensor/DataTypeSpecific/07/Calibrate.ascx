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
<%  if (Model.CanUpdate)
    {%>
<form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%  
        string PitchVal = string.Empty;
        string RollVal = string.Empty;
        foreach (SensorAttribute sensAttr in SensorAttribute.LoadBySensorID(Model.SensorID))
        {
            if (sensAttr.Name == "RollOffSet")
                RollVal = sensAttr.Value.ToString();
            if (sensAttr.Name == "PitchOffSet")
                PitchVal = sensAttr.Value.ToString();
        }
    %>

    <div>

        <div class="editor-label">
            <h3>Allows for Pitch and Roll offsets to be placed on the sensor.</h3>
            <label>to reset save Pitch and Roll offsets as zero.</label>
        </div>

        <div class="editor-label">
            Pitch Offset
        </div>
        <div class="editor-field">
            <input name="PitchOffset" id="PitchOffset" value="<%: PitchVal %>" />
        </div>
        <div class="editor-label calOptions">
            Roll Input:
        </div>
        <div class="editor-field calOptions">
            <input name="RollOffset" id="RollOffset" value="<%: RollVal %>" />
        </div>


        <div style="clear: both;"></div>
        <div style="clear: both;"></div>


        <div style="clear: both;"></div>

    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%} else{ %>
<div class="formBody" style="font-weight: bold">
    Calibration for this sensor is not available for edit until pending transaction
                is complete.
</div>
<div class="buttons">&nbsp; </div>
<%} %>
















