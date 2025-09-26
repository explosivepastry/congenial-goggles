<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
  { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

    <%  if (Model.CanUpdate)
        {%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|Operating Mode","Operating Mode")%>
        </div>
        <div class="col sensorEditFormInput">

            <select name="mode" id="mode" class="form-select">
                <option value="0" <%=Model.Calibration4 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G Normal","2G Normal")%></option>
                <option value="1" <%=Model.Calibration4 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G High Pass Filter","2G High Pass Filter")%></option>
                <option value="2" <%=Model.Calibration4 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G Normal","4G Normal")%></option>
                <option value="3" <%=Model.Calibration4 == 3 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G High Pass Filter","4G High Pass Filter")%></option>
                <option value="4" <%=Model.Calibration4 == 4 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G Normal","8G Normal")%></option>
                <option value="5" <%=Model.Calibration4 == 5 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G High Pass Filter","8G High Pass Filter")%></option>
            </select>
        </div>
    </div>
    <script>
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        //MobiScroll
        //$(function () {
        //    $('#mode').mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        minWidth: 200
        //    });
        //});

    </script>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>
<% else
        {%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%}%>