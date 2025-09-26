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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
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

            const arrayForSpinner = arrayBuilder(MinThresMinVal, MinThresMaxVal, 10);
            createSpinnerModal("minThreshNum", " <%:Html.Raw(Temperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MinimumThreshold_Manual", arrayForSpinner);

            $("#MinimumThreshold_Manual").change(function () {
                if (isANumber($("#MinimumThreshold_Manual").val())){
                    if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                        $("#MinimumThreshold_Manual").val(MinThresMinVal);
                    if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                        $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                    if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                        $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                } else{
                    $("#MinimumThreshold_Manual").val(<%: Min%>);
                }
            });

        <%}%>
    });
</script>
<%} %>