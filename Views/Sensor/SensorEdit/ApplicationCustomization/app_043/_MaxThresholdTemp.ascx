<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    bool isF = Humidity.IsFahrenheit(Model.SensorID);

    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        long number = Convert.ToInt64(Max);
        double MaxTemp = (BitConverter.ToInt16(BitConverter.GetBytes(number), 0) / 100.0);


        if (isF)
        {
            MaxTemp = MaxTemp.ToFahrenheit();
            MaxTemp = Math.Round(MaxTemp, 2);
        }
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_ManualTemp" id="MaximumThreshold_ManualTemp" value="<%=MaxTemp %>" />
        <a id="maxThreshNumTemp" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var MaxThresMinVal = -40;
        var MaxThresMaxVal = 250;
        let arrayForSpinner = arrayBuilder(MaxThresMinVal, MaxThresMaxVal, 10);

        <% if (Model.CanUpdate)
    { %>
                      <% if (isF)
    { %>
        createSpinnerModal("maxThreshNumTemp", "Above °F", "MaximumThreshold_ManualTemp", arrayForSpinner);
			  <%}
    else
    { %>
        createSpinnerModal("maxThreshNumTemp", "Above °C", "MaximumThreshold_ManualTemp", arrayForSpinner);
			  <%} %>
        <%}%>

        $("#MaximumThreshold_ManualTemp").change(function () {
            let min = parseFloat($("#MinimumThreshold_ManualTemp").val());
            let max = parseFloat($("#MaximumThreshold_ManualTemp").val());

            if (isANumber(max)) {
                if (max < MaxThresMinVal)
                    $("#MaximumThreshold_ManualTemp").val(MaxThresMinVal);
                if (max > MaxThresMaxVal)
                    $("#MaximumThreshold_ManualTemp").val(MaxThresMaxVal);

                if (max < min)
                    $("#MaximumThreshold_ManualTemp").val(min);

            } else {

                $("#MaximumThreshold_ManualTemp").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>