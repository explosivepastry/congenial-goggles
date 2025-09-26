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

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Above","Above")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MaxThresMinVal = <%=DefaultMin%>;
        var MaxThresMaxVal = <%=DefaultMax%>;

        <% if (Model.CanUpdate){ %>

            const arrayForSpinner = arrayBuilder(MaxThresMinVal, MaxThresMaxVal, 10);
            createSpinnerModal("maxThreshNum", " <%:Html.Raw(Temperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MaximumThreshold_Manual", arrayForSpinner);

            $("#MaximumThreshold_Manual").change(function () {
                if (isANumber($("#MaximumThreshold_Manual").val())){
                    if ($("#MaximumThreshold_Manual").val() < MaxThresMinVal)
                        $("#MaximumThreshold_Manual").val(MaxThresMinVal);
                    if ($("#MaximumThreshold_Manual").val() > MaxThresMaxVal)
                        $("#MaximumThreshold_Manual").val(MaxThresMaxVal);

                    if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                        $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                } else {

                    $("#MaximumThreshold_Manual").val(<%: Max%>);
                }
            });
        <%}%>
    });
</script>
<%} %>