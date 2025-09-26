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

        <div>
            Make sure the Light sensor is in a constant light condition then enter the actual
                        lux. When this button is available again the calibration cycle is complete.
        </div>
        <div class="editor-label">
            Actual LUX Reading:
        </div>
        <div class="editor-field">
            <%: Html.TextBox("actual")%>
        </div>
        <div class="editor-label">
            Filter Type:
        </div>
        <div class="editor-field">
            <select name="filter">
                <option value="1">Standard</option>
            </select>
        </div>
        <div class="editor-label">
            Ambient Temperature:
        </div>
        <div class="editor-field">
            <%: Html.TextBox("tempTarget")%>
        </div>
        <div class="editor-label">
        </div>
        <div class="editor-field">
            <input type="radio" name="ForC" id="Radio1" value="f" checked="checked" /><label
                for="f">Fahrenheit</label>
            <input type="radio" name="ForC" id="Radio2" value="c" /><label for="c"><%: Html.TranslateTag("Celsius","Celsius")%></label>
        </div>
        <div style="clear: both;" />
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




