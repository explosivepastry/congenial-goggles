<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    string[] observe = new string[0];

    if (Model.LastDataMessage != null)
        observe = Model.LastDataMessage.Data.Split(',');
%>

<%
  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%   if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
         {%>


    <%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0") || Model.SensorTypeID == 4)//Post 2.0 or WIFI
      { %>
   
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input name="observed" id="observed" readonly="readonly"value="<%: observe.Length > 0 ? observe[1]:"" %>" />
        <%: Html.TranslateTag("Average Current","Average Current")%>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Actual Input: (External reference value)","Actual Input: (External reference value)")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                 <input name="actual" id="actual" value="<%:  observe.Length > 0 ? observe[1]:""  %>" />
          <%: Html.TranslateTag("Average Current","Average Current")%>
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <%}%>


    <div style="clear: both;"></div>


    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>

<script>
    $(function () {
        $('#actual').change(function () {

            if (isANumber($('#actual').val())) {
                var maxCalibrate = 20;
                var minCalibrate = 2;


                if ($('#actual').val() < 0)
                    $('#actual').val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>);

                    if ($('#actual').val() > maxCalibrate)
                    $('#actual').val(maxCalibrate);

                if ($('#actual').val() < minCalibrate)
                    $('#actual').val(minCalibrate);

                }
                else {

                    $('#actual').val(<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.###") : "" %>);
            }
        });
    });
</script>