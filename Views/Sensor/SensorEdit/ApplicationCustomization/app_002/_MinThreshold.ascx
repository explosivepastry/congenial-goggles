<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    bool isF = Temperature.IsFahrenheit(Model.SensorID);
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
        if (isF)
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
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
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
              <% if (isF)
    { %>
        MinThresMobiscroll(MinThresMinVal, MinThresMaxVal, 'Minimum Threshold °F');
			  <%}
    else
    { %>
        MinThresMobiscroll(MinThresMinVal, MinThresMaxVal, 'Minimum Threshold °C');
			  <%} %>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));

                $('#maxThreshNum').mobiscroll('option', {
                    min: $('#MinimumThreshold_Manual').val()
                });

            } else {

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

        <%}%>

        function MinThresMobiscroll(min, max, headertext) {

            createSpinnerModal("minThreshNum", headertext, "MinimumThreshold_Manual", [-40, -30, -20, -10, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120,]);
        }

    });
</script>
<%} %>