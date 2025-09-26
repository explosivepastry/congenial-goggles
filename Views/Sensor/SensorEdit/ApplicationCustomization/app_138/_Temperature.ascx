<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    bool isF = PIRHumidity.IsFahrenheit(Model.SensorID);
    string MinTemp = PIRHumidity.GetTemperatureMinThresh(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    string MaxTemp = PIRHumidity.GetTemperatureMaxThresh(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    string HystTemp = PIRHumidity.GetTemperatureAwareBuffer(Model).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
    long DefaultMin = -50;
    long DefaultMax = 150;
    double TempHystMax = 10;

    if (isF)
    {
        DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
        DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
        MinTemp = MinTemp.ToDouble().ToFahrenheit().ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
        MaxTemp = MaxTemp.ToDouble().ToFahrenheit().ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
        HystTemp = (HystTemp.ToDouble() * 1.8).ToString("#0.##", System.Globalization.CultureInfo.InvariantCulture);
        TempHystMax = 18;
    }

%>
<p class="useAwareState">Enter Aware State Temperature</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%>  (<%: Html.Label(isF?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Temp" id="MinimumThreshold_Temp" value="<%=MinTemp %>" />

        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%>    (<%: Html.Label(isF?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Temp" id="MaximumThreshold_Temp" value="<%=MaxTemp %>" />

        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (<%: Html.Label(isF?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Temp" id="Hysteresis_Temp" value="<%=HystTemp %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script>

    $(function () {
        var TempThresMinVal = Number(<%=DefaultMin%>);
        var TempThresMaxVal = Number(<%=DefaultMax%>);
        var TempHystMaxVal = Number(<%=TempHystMax%>);

        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(TempThresMinVal, TempThresMaxVal, 10);
        createSpinnerModal("minThreshNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "MinimumThreshold_Temp", arrayForSpinner);
        createSpinnerModal("maxThreshNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "MaximumThreshold_Temp", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(0, TempHystMaxVal, 1);
        createSpinnerModal("hystNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "Hysteresis_Temp", arrayForSpinner1);

         <%}%>
        $("#MinimumThreshold_Temp").addClass('editField editFieldSmall')

        $("#MinimumThreshold_Temp").change(function () {
            if (isANumber($("#MinimumThreshold_Temp").val())) {
                if ($("#MinimumThreshold_Temp").val() < TempThresMinVal)
                    $("#MinimumThreshold_Temp").val(TempThresMinVal);
                if ($("#MinimumThreshold_Temp").val() > TempThresMaxVal)
                    $("#MinimumThreshold_Temp").val(TempThresMaxVal);

                if (parseFloat($("#MinimumThreshold_Temp").val()) > parseFloat($("#MaximumThreshold_Temp").val()))
                    $("#MinimumThreshold_Temp").val(parseFloat($("#MaximumThreshold_Temp").val()));
            } else {
                $("#MinimumThreshold_Temp").val(<%: MinTemp%>);
            }
        });

        $("#MaximumThreshold_Temp").change(function () {
            if (isANumber($("#MaximumThreshold_Temp").val())) {
                if ($("#MaximumThreshold_Temp").val() < TempThresMinVal)
                    $("#MaximumThreshold_Temp").val(TempThresMinVal);
                if ($("#MaximumThreshold_Temp").val() > TempThresMaxVal)
                    $("#MaximumThreshold_Temp").val(TempThresMaxVal);

                if (parseFloat($("#MaximumThreshold_Temp").val()) < parseFloat($("#MinimumThreshold_Temp").val()))
                    $("#MaximumThreshold_Temp").val(parseFloat($("#MinimumThreshold_Temp").val()));     
            } else {
                $("#MaximumThreshold_Temp").val(<%: MaxTemp%>);
            }
        });

        $("#Hysteresis_Temp").change(function () {
            if (isANumber($("#Hysteresis_Temp").val())) {
                if ($("#Hysteresis_Temp").val() < 0)
                    $("#Hysteresis_Temp").val(0);
                if ($("#Hysteresis_Temp").val() > TempHystMaxVal)
                    $("#Hysteresis_Temp").val(TempHystMaxVal)
            }
            else {
                $('#Hysteresis_Temp').val(<%: HystTemp%>);
            }

        });

    });
</script>
