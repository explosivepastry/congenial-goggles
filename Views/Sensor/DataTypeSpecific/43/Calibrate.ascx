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

            if (Model.LastDataMessage != null)
            {%>

    <script type="text/javascript">
        $(function () {
            var mode = $('#mode').val();
            var val = '<%:Model.LastDataMessage.AppBase.PlotValue %>';

                $('#mode').change(function () {
                    if (mode != $('#mode').val()) {
                        $('#value').val('');
                        $('.calOptions').hide();
                    } else {
                        $('#value').val(val);
                        $('.calOptions').show();
                    }

                    switch ($(this).val()) {
                        case "0":
                            $('#calMode').text('Volts AC RMS');
                            break;
                        case "1":
                            $('#calMode').text('Volts AC Peak to Peak');
                            break;
                        case "3":
                            $('#calMode').text('Volts DC');
                            break;
                        case "4":
                            $('#calMode').text('Volts DC');
                            break;
                    }

                });

                $('#mode').change();
            });
    </script>

    <div class="formbody">
        <input type="hidden" name="last" value="<%:Model.LastDataMessage.MessageID %>" />
        <div class="editor-label">
            Sensor Mode:
        </div>
        <div class="editor-field">
            <select name="mode" id="mode">
                <option value="0" <%:Model.Calibration2 == 0 ? "selected='selected'" : "" %>>Volt AC RMS</option>
                <option value="1" <%:Model.Calibration2 == 1 ? "selected='selected'" : "" %>>Volt AC Peak to Peak</option>
                <option value="3" <%:Model.Calibration2 == 3 ? "selected='selected'" : "" %>>Volt DC US(60 Hz Sample)</option>
                <option value="4" <%:Model.Calibration2 == 4 ? "selected='selected'" : "" %>>Volt DC Europe(50 Hz Sample)</option>
            </select>
        </div>
        <div class="editor-label calOptions">
            <span id="calMode"></span>Value:
        </div>
        <div class="editor-field calOptions">
            <input name="value" id="value" value="<%:Model.LastDataMessage.AppBase.PlotValue %>" />
        </div>
        

    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>
<div style="clear: both;" />

<%  
                }
                else
                {
%>
<div class="formbody">
    Calibration is not available until sensor has sent data to server.
</div>
<div class="buttons">&nbsp; </div>
<%}
            }
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


