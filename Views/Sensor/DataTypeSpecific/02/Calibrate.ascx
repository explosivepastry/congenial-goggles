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

<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
  { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification))
  {%>

<form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%  if (Model.CanUpdate)
        {%>

    <div class="formBody">
        <div>
            Actual reading is <%: Html.TextBox("actual")%> degrees
            <select name="tempScale">
                <option value="F"><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></option>
                <option value="C"><%: Html.TranslateTag("Celsius","Celsius")%></option>
            </select>
            <script>

                $('#actual').addClass('editField editFieldMedium');

                $("#actual").change(function () {
                    if (!isANumber($("#actual").val()))
                        $("#actual").val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00") : "" %>);
                });
            </script>
        </div>
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>
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


<%}
  else
  {%>
<div class="formBody">
    <div>
        This sensor has been pre-calibrated and certified by <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf">View Calibration Certificate</a>
    </div>
</div>
<%}%>
