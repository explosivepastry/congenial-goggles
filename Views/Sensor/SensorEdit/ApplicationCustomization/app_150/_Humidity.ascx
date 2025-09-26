<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string MinHumidity = Motion_RH_WaterDetect.GetHumidityMinThresh(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    string MaxHumidity = Motion_RH_WaterDetect.GetHumidityMaxThresh(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    string HystHumidity = Motion_RH_WaterDetect.GetHumidityAwareBuffer(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    long HumidityDefaultMin = 0;
    long HumidityDefaultMax = 100;
    double TempHystMax = 10;
%>
<h5 style="font-weight:bold;">Humidity:</h5>
<h5>&nbsp;Enter Aware State</h5>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        &nbsp;&nbsp; <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below")%>  (<%: Html.Label("%") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Humidity" id="MinimumThreshold_Humidity" value="<%=MinHumidity %>" />

        <a id="minThreshNumHumidity" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        &nbsp;&nbsp; <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above")%>  (<%: Html.Label("%") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Humidity" id="MaximumThreshold_Humidity" value="<%=MaxHumidity %>" />

        <a id="maxThreshNumHumidity" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Aware State Buffer","Aware State Buffer")%>  (<%: Html.Label("%") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Humidity" id="Hysteresis_Humidity" value="<%=HystHumidity %>" />
        <a id="hystNumHumidity" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>
<br />

<script>

    $(function () {
        var HumidityThresMinVal = Number(<%=HumidityDefaultMin%>);
        var HumidityThresMaxVal = Number(<%=HumidityDefaultMax%>);
        var HumidityHystMaxVal = Number(<%=TempHystMax%>);

        <% if (Model.CanUpdate)
    { %>


        const arrayForSpinner = arrayBuilder(HumidityThresMinVal, HumidityThresMaxVal, 10);
        createSpinnerModal("minThreshNumHumidity", "%", "MinimumThreshold_Humidity", arrayForSpinner);
        createSpinnerModal("maxThreshNumHumidity", "%", "MaximumThreshold_Humidity", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, HumidityHystMaxVal, 1);
        createSpinnerModal("hystNumHumidity", "%", "Hysteresis_Humidity", arrayForSpinner1);

         <%}%>
        $("#MinimumThreshold_Humidity").addClass('editField editFieldSmall')

        $("#MinimumThreshold_Humidity").change(function () {
            if (isANumber($("#MinimumThreshold_Humidity").val())) {
                if ($("#MinimumThreshold_Humidity").val() < HumidityThresMinVal)
                    $("#MinimumThreshold_Humidity").val(HumidityThresMinVal);
                if ($("#MinimumThreshold_Humidity").val() > HumidityThresMaxVal)
                    $("#MinimumThreshold_Humidity").val(HumidityThresMaxVal);

                if (parseFloat($("#MinimumThreshold_Humidity").val()) > parseFloat($("#MaximumThreshold_Humidity").val()))
                    $("#MinimumThreshold_Humidity").val(parseFloat($("#MaximumThreshold_Humidity").val())); 
            } else {
                $("#MinimumThreshold_Humidity").val(<%: MinHumidity%>);
            }
        });

        $("#MaximumThreshold_Humidity").change(function () {
            if (isANumber($("#MaximumThreshold_Humidity").val())) {
                if ($("#MaximumThreshold_Humidity").val() < HumidityThresMinVal)
                    $("#MaximumThreshold_Humidity").val(HumidityThresMinVal);
                if ($("#MaximumThreshold_Humidity").val() > HumidityThresMaxVal)
                    $("#MaximumThreshold_Humidity").val(HumidityThresMaxVal);

                if (parseFloat($("#MaximumThreshold_Humidity").val()) < parseFloat($("#MinimumThreshold_Humidity").val()))
                    $("#MaximumThreshold_Humidity").val(parseFloat($("#MinimumThreshold_Humidity").val()));
            } else {

                $("#MaximumThreshold_Humidity").val(<%: MaxHumidity%>);
            }
        });

        $("#Hysteresis_Humidity").change(function () {
            if (isANumber($("#Hysteresis_Humidity").val())) {
                if ($("#Hysteresis_Humidity").val() < 0)
                    $("#Hysteresis_Humidity").val(0);
                if ($("#Hysteresis_Humidity").val() > HumidityHystMaxVal)
                    $("#Hysteresis_Humidity").val(HumidityHystMaxVal)
            }
            else {
                $('#Hysteresis_Humidity').val(<%: HystHumidity%>);
            }
        });
    });
</script>
