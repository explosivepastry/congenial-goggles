 <%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 

    bool isF = Temperature.IsFahrenheit(Model.SensorID);
    double deltaThresh = LCD_Temperature.GetDeltaThresh(Model);
    double defaultDeltaThesh = LCD_Temperature.DefaultDeltaThreshold;


    double deltaMin = 0.5;
    double deltaMax = 50;

    if(isF)
    {
        deltaMin = (deltaMin * 1.8);
        deltaMax = (deltaMax * 1.8);
    }

%>

    <input hidden type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="deltaThreshSlider" id="deltaThreshSlider" <%=deltaThresh == 0 ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("Threshold","Threshold")%>" data-off="<%: Html.TranslateTag("Change","Change")%>" />

<div id="thresholdDiv" style="display:<%= deltaThresh == 0 ? "block" : "none"%>">
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_MinThreshold.ascx", Model);%>
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_MaxThreshold.ascx", Model);%>
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_Hysteresis.ascx", Model);%>
</div>


            <%: Html.TranslateTag("Delta Threshold","Delta Threshold")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
       
            <input hidden class="aSettings__input_input" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="deltaThresh" id="deltaThresh" value="<%=deltaThresh == 0 ? (isF ?  (Math.Round(defaultDeltaThesh * 1.8,1)) : defaultDeltaThesh ) : (isF ?  (Math.Round(deltaThresh * 1.8,1)) : deltaThresh) %>" />
            <%: Html.ValidationMessageFor(model => model.Hysteresis)%>

<script type="text/javascript">

    var deltaMin = Number(<%=deltaMin%>);
    var deltaMax = Number(<%=deltaMax%>);

    var deltaThresh_array = [];
 
    for (var i = deltaMin; i < deltaMax; i++) {
        deltaThresh_array.push(Math.round(i));
    }

</script>
