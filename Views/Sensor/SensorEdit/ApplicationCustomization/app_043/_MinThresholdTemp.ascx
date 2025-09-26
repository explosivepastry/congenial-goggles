<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    bool isF = Humidity.IsFahrenheit(Model.SensorID);

    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        long number = Convert.ToInt64(Min);
        double TempMin = (BitConverter.ToInt16(BitConverter.GetBytes(number), 0) / 100.0);

        if (isF)
        {
            TempMin = TempMin.ToFahrenheit();
            TempMin = Math.Round(TempMin, 2);
        }

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State Temperature","Use Aware State Temperature")%></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_ManualTemp" id="MinimumThreshold_ManualTemp" value="<%=TempMin %>" />
        <a id="minThreshNumTemp" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThresMinVal = -40;
        var MinThresMaxVal = 250;
        let arrayForSpinner = arrayBuilder(MinThresMinVal, MinThresMaxVal, 10);

        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("minThreshNumTemp", "Below °F", "MinimumThreshold_ManualTemp", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_ManualTemp").addClass('editField editFieldSmall')

        $("#MinimumThreshold_ManualTemp").change(function () {
            let min = parseFloat($("#MinimumThreshold_ManualTemp").val());
            let max = parseFloat($("#MaximumThreshold_ManualTemp").val());

            if (isANumber(min)) {
                if (min < MinThresMinVal)
                    $("#MinimumThreshold_ManualTemp").val(MinThresMinVal);
                if (min > MinThresMaxVal)
                    $("#MinimumThreshold_ManualTemp").val(MinThresMaxVal);

                if (min > max)
                    $("#MinimumThreshold_ManualTemp").val(max);
            } else {
                $("#MinimumThreshold_ManualTemp").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>