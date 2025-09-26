<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Get the Hyst, Max, Min Values for Pascal
        string Hyst = "";
        string Max = "";
        string Min = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);

        long DefaultMin = 0;
        long DefaultMax = 0;

        MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);
        string label = AirSpeed.GetUnits(Model.SensorID).ToString();
        double areaInFeet = (AirSpeed.GetDuctAreaValue(Model.SensorID) * 3.28084);

        double minthresh = 0;
        double maxthresh = 0;
        double minUIVal = 0;
        double maxUIVal = 0;
        double minhyst = 0;
        double maxhyst = 10;
        double step = 1;




        switch (label.ToLower())
        {
            case "cfm":
                minthresh = DefaultMin * 196.85 * areaInFeet;
                maxthresh = DefaultMax * 196.85 * areaInFeet;
                minUIVal = Min.ToDouble() * 196.85 * areaInFeet;
                maxUIVal = Max.ToDouble() * 196.85 * areaInFeet;
                step = 1;
                break;
            case "cmh":
                minthresh = ((DefaultMin * 196.85 * areaInFeet) * 0.58857777021102);
                maxthresh = ((DefaultMax * 196.85 * areaInFeet) * 0.58857777021102);
                minUIVal = ((Min.ToDouble() * 196.85 * areaInFeet) * 0.58857777021102);
                maxUIVal = ((Max.ToDouble() * 196.85 * areaInFeet) * 0.58857777021102);
                step = 1;
                break;
            case "cmm":
                minthresh = ((DefaultMin * 196.85 * areaInFeet) * 35.314666212661);
                maxthresh = ((DefaultMax * 196.85 * areaInFeet) * 35.314666212661);
                minUIVal = ((Min.ToDouble() * 196.85 * areaInFeet) * 35.314666212661);
                maxUIVal = ((Max.ToDouble() * 196.85 * areaInFeet) * 35.314666212661);
                step = 1;
                break;
            case "mph":
                minthresh = (DefaultMin * 2.23694);
                maxthresh = (DefaultMax * 2.23694);
                minUIVal = (Min.ToDouble() * 2.23694);
                maxUIVal = (Max.ToDouble() * 2.23694);
                maxhyst = 22.3694;
                step = .1;
                break;
            case "kmph":
                minthresh = (DefaultMin * 3.6);
                maxthresh = (DefaultMax * 3.6);
                minUIVal = (Min.ToDouble() * 3.6);
                maxUIVal = (Max.ToDouble() * 3.6);
                maxhyst = 36;
                step = .1;

                break;
            case "knot":
                minthresh = (DefaultMin * 1.94384);
                maxthresh = (DefaultMax * 1.94384);
                minUIVal = (Min.ToDouble() * 1.94384);
                maxUIVal = (Max.ToDouble() * 1.94384);
                maxhyst = 19.4384;
                step = .1;

                break;
            default:
            case "mps":
                minthresh = DefaultMin;
                maxthresh = DefaultMax;
                minUIVal = Min.ToDouble();
                maxUIVal = Max.ToDouble();
                step = 1;
                break;
        }

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>

<%--Min Thresh--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%--Max Thresh--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%--Hyst Thresh--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<%--Altitude--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Altitude","Altitude")%> (Meters)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="DefaultCalibration4" id="DefaultCalibration4" value="<%=Model.Calibration4 %>" />
        <a id="altitudeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>





<script>
    var lowVal = <%:minthresh%>;
    var highVal = <%:maxthresh%>;
    var stepVal = <%:step%>;

    $(function () {
        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(0, 5000, 1);
        createSpinnerModal("minThreshNum", '<%: label%>', "MinimumThreshold_Manual", arrayForSpinner);
        createSpinnerModal("maxThreshNum", '<%: label%>', "MaximumThreshold_Manual", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(0, 10, 1);
        createSpinnerModal("hystThreshNum", '<%: label%>', "Hysteresis_Manual", arrayForSpinner1);
        const arrayForSpinner2 = arrayBuilder(0, 5000, 100);
        createSpinnerModal("altitudeNum", 'Meters', "DefaultCalibration4", arrayForSpinner2);

        <%}%>

        $("#MinimumThreshold_Manual").addClass('editField editFieldMedium');
        $("#MaximumThreshold_Manual").addClass('editField editFieldMedium');
        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < lowVal)
                    $("#MinimumThreshold_Manual").val(lowVal);

                if ($("#MinimumThreshold_Manual").val() > highVal)
                    $("#MinimumThreshold_Manual").val(highVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                //$("#MaximumThreshold_Manual").change();
            } else
                $("#MinimumThreshold_Manual").val(<%:Min%>);
        });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < lowVal)
                    $("#MaximumThreshold_Manual").val(lowVal);
                if ($("#MaximumThreshold_Manual").val() > highVal)
                    $("#MaximumThreshold_Manual").val(highVal);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                //$("#MinimumThreshold_Manual").change();
            } else
                $("#MaximumThreshold_Manual").val(<%: Max%>);
        });

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < <%:minhyst%>)
                    $("#Hysteresis_Manual").val(<%:minhyst%>);
                if ($("#Hysteresis_Manual").val() ><%:maxhyst%>)
                    $("#Hysteresis_Manual").val(<%:maxhyst%>);
            } else
                $("#Hysteresis_Manual").val(<%: Hyst%>);
        });

        $("#DefaultCalibration4").change(function () {
            if (isANumber($("#DefaultCalibration4").val())) {
                if ($("#DefaultCalibration4").val() < 0)
                    $("#DefaultCalibration4").val(0);
                if ($("#DefaultCalibration4").val() > 9000)
                    $("#DefaultCalibration4").val(9000)
            } else
                $("#DefaultCalibration4").val(0);
        });
    });

</script>
<%} %>