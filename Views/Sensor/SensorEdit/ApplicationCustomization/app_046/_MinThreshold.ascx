<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";
        long DefaultMin = 0;
        long DefaultMax = 0;

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);

        DefaultMin = DefaultMin / 10;
        DefaultMax = DefaultMax / 10;
        if (Temperature.IsFahrenheit(Model.SensorID))
        {
            DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
            DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
        }
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%>&nbsp;<span id="belowType">(<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)</span>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThresMinVal = <%=DefaultMin%>;
        var MinThresMaxVal = <%=DefaultMax%>;
     

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(MinThresMinVal, MinThresMaxVal, 10);
        createSpinnerModal("minThreshNum", "Below", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            let min = parseFloat($("#MinimumThreshold_Manual").val());
            let max = parseFloat($("#MaximumThreshold_Manual").val());

            if (isANumber(min)){
                if (min <  <%=DefaultMin%>)
                    $("#MinimumThreshold_Manual").val( <%=DefaultMin%>);
                if (min > <%=DefaultMax%>)
                    $("#MinimumThreshold_Manual").val(<%=DefaultMax%>);
                if (min > max)
                    $("#MinimumThreshold_Manual").val(max);
            } else {
                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });
    });
</script>
<%} %>