<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>


<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>


<form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>
    <%if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {%>

   <%-- <input type="hidden" name="last" value="<%:Model.LastDataMessage.DataMessageGUID %>" />--%>


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%string mode = string.Empty;
              switch (Model.Calibration2.ToLong())
              {
                  case 0: //used in Gen1
                  case 131072: //Bitshifting value of 0 added to 131072 (used in Alta/Gen2 includes oversampling)
                      mode = "Volt AC RMS";
                      break;
                  case 1: //used in Gen1
                  case 131073: //Bitshifting value of 1 added to 131072 (used in Alta/Gen2 includes oversampling)
                      mode = "Volt AC Peak to Peak";
                      break;
                  case 3: //used in Gen1
                  case 131075: //Bitshifting value of 3 added to 131072 (used in Alta/Gen2 includes oversampling)
                      mode = "Volt DC US(60 Hz Sample)";
                      break;
                  case 4: //used in Gen1
                  case 131076: //Bitshifting value of 4 added to 131072 (used in Alta/Gen2 includes oversampling)
                      mode = "Volt DC Europe(50 Hz Sample)";
                      break;
                  default:
                      break;
              }%>
        </div>
        <div class="col sensorEditFormInput">
        <span id="calMode"><%:mode%></span> Value:
             
        </div>

    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3 calOptions">
            <%: Html.TranslateTag("Actual Input: (External reference value)","Actual Input: (External reference value)")%>
        </div>
        <div class="col sensorEditFormInput calOptions">
            <input name="actual" id="actual" value="<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000")) : "" %>" />
            <%:Monnit.VoltageMeter500VAC.GetLabel(Model.SensorID) %>
        </div>

    </div>


    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%} %>