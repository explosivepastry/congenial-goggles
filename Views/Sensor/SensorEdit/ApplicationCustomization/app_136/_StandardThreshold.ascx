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

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Change","Change")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="deltaThreshSlider" id="deltaThreshSlider" <%=deltaThresh == 0 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Threshold","Threshold")%></label>
        </div>
        <div style="display: none;"></div>
    </div>
</div>

<div id="thresholdDiv" style="display:<%= deltaThresh == 0 ? "block" : "none"%>">
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_MinThreshold.ascx", Model);%>
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_MaxThreshold.ascx", Model);%>
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_136/_Hysteresis.ascx", Model);%>
</div>
<div id="changeDiv" style="display:<%: deltaThresh == 0 ? "none" : "block"%>">
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Delta Threshold","Delta Threshold")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="deltaThresh" id="deltaThresh" value="<%=deltaThresh == 0 ? (isF ?  (Math.Round(defaultDeltaThesh * 1.8,1)) : defaultDeltaThesh ) : (isF ?  (Math.Round(deltaThresh * 1.8,1)) : deltaThresh) %>" />
            <a id="deltaThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
        </div>
    </div>
</div>

<script type="text/javascript">

    var deltaMin = Number(<%=deltaMin%>);
    var deltaMax = Number(<%=deltaMax%>);

    var deltaThresh_array = [];
 
    for (var i = deltaMin; i < deltaMax; i++) {
        deltaThresh_array.push(Math.round(i));
    }




    $(document).ready(function () {
        $('#deltaThreshSlider').change();

         <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("deltaThreshNum", '<%= isF ? "°F" : "°C"%>', "deltaThresh", deltaThresh_array);

        <%}%>

        $("#deltaThresh").change(function () {
            if (isANumber($("#deltaThresh").val())) {
                //Check if less than min
                if ($("#deltaThresh").val() < 0)
                    $("#deltaThresh").val(0);

                //Check if greater than max
                if ($("#deltaThresh").val() > deltaMax)
                    $("#deltaThresh").val(deltaMax);
            }
            else {
                $("#deltaThresh").val(<%:deltaThresh == 0 ? (isF ?  (Math.Round(defaultDeltaThesh * 1.8,1)) : defaultDeltaThesh ) : (isF ?  (Math.Round(deltaThresh * 1.8,1)) : deltaThresh)%>);
                    }
                });


        $('#deltaThreshSlider').prop('checked', '<%:deltaThresh == 0 ? "checked" : "" %>');

        $('#deltaThreshSlider').change(function () {
            if ($('#deltaThreshSlider').prop('checked')) {
                $('#changeDiv').hide();
                $('#thresholdDiv').show();
            }
            else {
                $('#thresholdDiv').hide();
                $('#changeDiv').show();
            }
        });
        $('#deltaThreshSlider').change();
    });	
</script>
