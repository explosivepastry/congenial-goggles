<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

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

    <%  if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {%>
    <%string[] lastValues = null; %>
    <%if (Model.LastDataMessage != null)
        {%>

    <div class="formBody">

        <input type="radio" name="calType" value="1">
        Moisture Calibration 

        <input type="radio" name="calType" value="2">
        Temperature Calibration
        

       <div id="cal-1" style="display: none; margin-top: 15px;">

           <% lastValues = Model.LastDataMessage.Data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

               string tempVal = SoilMoisture.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temperature.ToString();
               string cTempVal = tempVal;

               if (Monnit.SoilMoisture.IsFahrenheit(Model.SensorID))
               {
                   tempVal = tempVal.ToDouble().ToFahrenheit().ToString("0.00");

               }

               double resistanceValue = 0;
               if (lastValues.Length > 2)
                   resistanceValue = lastValues[2].ToDouble();

           %>
           <input type="hidden" name="resistance" value="<%=resistanceValue  %>"/>

           <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Moisture","Moisture")%></h2>
           <div class="form-group">
               <div class="bold col-md-3 col-sm-3 col-xs-12">
                   <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
               </div>
               <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                    <input name="obsTemp" id="obsTemp" type="hidden" value="<%: cTempVal %>" />
                   <input name="observed" id="observed" readonly="readonly" value="<%: lastValues[0] %>" />
                   (<%: Html.Label(SoilMoisture.GetLabel(Model.SensorID))%>) @ <%=tempVal.ToString() %>  <%:Monnit.SoilMoisture.IsFahrenheit(Model.SensorID)? "F" : "C" %>             
               </div>
           </div>
           <div style="clear: both;"></div>
           <br />
           <div class="form-group">
               <div class="bold col-md-3 col-sm-3 col-xs-12">
                   <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is","Actual reading is")%>
               </div>
               <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                   <input name="actual" id="actual" value="<%: Model.LastDataMessage != null ? lastValues[0] : "" %>" required />
                   (<%: Html.Label(SoilMoisture.GetLabel(Model.SensorID))%>)              
               </div>
           </div>
           <div style="clear: both;"></div>
           <br />




       </div>
        <div id="cal-2" style="display: none; margin-top: 15px;">
            <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature","Temperature")%></h2>
            <div class="form-group">
                <div class="bold col-md-3 col-sm-3 col-xs-12">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
                </div>
                <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                    <input name="observedTemp" id="observedTemp" readonly="readonly" value="<%: Model.LastDataMessage != null ? tempVal.ToString() : "" %>" />
                    <%:Monnit.SoilMoisture.IsFahrenheit(Model.SensorID)? "F" : "C" %>
                </div>
            </div>
            <div style="clear: both;"></div>
            <br />
            <div class="form-group">
                <div class="bold col-md-3 col-sm-3 col-xs-12">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is","Actual reading is")%>
                </div>
                <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                    <input name="actualTemp" id="actualTemp" value="<%: Model.LastDataMessage != null ? tempVal.ToString() : "" %>" required />
                    <%:Monnit.SoilMoisture.IsFahrenheit(Model.SensorID)? "F" : "C" %>
                </div>
            </div>
            <div style="clear: both;"></div>
            <br />
        </div>

    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>

    <script type="text/javascript">

        $(document).ready(function () {
            $("input[name=calType]").on("change", function () {

                $('#cal-1').hide();
                $('#cal-2').hide();
                $("#cal-" + $(this).val()).show();
            });


        });

        $('#actual').addClass('editField editFieldMedium');
        $('#actualTemp').addClass('editField editFieldMedium');

        $("#actual").change(function () {
            if (!isANumber($("#actual").val()))
                $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
        });

        $("#actualTemp").change(function () {
            if (!isANumber($("#temperatureActual").val()))
                $("#temperatureActual").val(<%: Model.LastDataMessage != null ?  (tempVal) : "" %>);
        });
    </script>

    <%} %> 
</form>
<div class="buttons">&nbsp; </div>
<%
    }
%>