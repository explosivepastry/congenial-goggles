<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        //Get Pressure label for profile
        string label = "";


        //Get the Hyst, Max, Min Values for Pascal
        string Hyst = "";
        string Max = "";
        string Min = "";
        string savedValue = "";
        string profileLow = "";
        string profileHigh = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);
        double DefaultMin = DifferentialPressure.DefaultMinThreshold.ToDouble();
        double DefaultMax = DifferentialPressure.GetSavedValue(Model.SensorID);
        double step = 1;
        double hystMax = 100;

        label = DifferentialPressure.GetLabel(Model.SensorID);


        switch (label.ToLower())
        {

            case "torr":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.0075006168), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.0075006168), 4);
                hystMax = (100d * 0.0075006168);
                Hyst = ((Model.Hysteresis / 10d) * 0.0075006168).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.0075006168).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.0075006168).ToString("0.####");
                step = 0.001;
                break;
            case "psi":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.0001450377), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.0001450377), 4);
                hystMax = (100d * 0.0001450377);
                Hyst = ((Model.Hysteresis / 10d) * 0.0001450377).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.0001450377).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.0001450377).ToString("0.####");
                step = 0.0001;
                break;
            case "inaq":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.0040185981), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.0040185981), 4);
                hystMax = (100d * 0.0040185981);
                Hyst = ((Model.Hysteresis / 10d) * 0.0040185981).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.0040185981).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.0040185981).ToString("0.####");
                step = 0.0001;
                label = "inH20";
                break;
            case "inhg":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.000296134), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.000296134), 4);
                hystMax = (100d * 0.000296134);
                Hyst = ((Model.Hysteresis / 10d) * 0.000296134).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.000296134).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.000296134).ToString("0.####");
                step = 0.0001;
                break;
            case "mmhg":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.0075006376), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.0075006376), 4);
                hystMax = (100d * 0.0075006376);
                Hyst = ((Model.Hysteresis / 10d) * 0.0075006376).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.0075006376).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.0075006376).ToString("0.####");
                step = 0.0001;
                break;
            case "mmwc":
                DefaultMin = Math.Round((DifferentialPressure.DefaultMinThreshold * 0.1019744289), 4);
                DefaultMax = Math.Round((DifferentialPressure.DefaultMaxThreshold * 0.1019744289), 4);
                hystMax = (100d * 0.1019744289);
                Hyst = ((Model.Hysteresis / 10d) * 0.1019744289).ToString("0.####");
                Min = ((Model.MinimumThreshold / 10d) * 0.1019744289).ToString("0.####");
                Max = ((Model.MaximumThreshold / 10d) * 0.1019744289).ToString("0.####");
                step = 0.01;
                break;
            case "custom":
                label = DifferentialPressure.GetCustomLabel(Model.SensorID);
                savedValue = DifferentialPressure.GetSavedValue(Model.SensorID).ToString();
                profileLow = DifferentialPressure.GetLowValue(Model.SensorID).ToString();
                profileHigh = DifferentialPressure.GetHighValue(Model.SensorID).ToString();
                hystMax = Math.Round((100d * DifferentialPressure.GetTransform(Model.SensorID)), 4);
                DefaultMin = Math.Round(profileLow.ToDouble(), 4);
                DefaultMax = Math.Round(profileHigh.ToDouble(), 4);

                Hyst = (Model.Hysteresis / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString("0.####");
                Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString("0.####");
                Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString("0.####");
                break;
            case "pascal":
            default:
                DefaultMin = DifferentialPressure.DefaultMinThreshold;
                DefaultMax = DifferentialPressure.DefaultMaxThreshold;

                Hyst = (Model.Hysteresis / 10d).ToString();
                Min = (Model.MinimumThreshold / 10d).ToString();
                Max = (Model.MaximumThreshold / 10d).ToString();

                break;
        }

        if (label == "inAq")
            label = "inH20";
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%: Html.Label(label) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />

        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_103|Above","Above")%> (<%: Html.Label(label) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />

        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />

        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>



<script type="text/javascript">

    $(function () {
        var stepUnit = <%=step%>;
        var dMin = <%:(DefaultMin)%>;
        var dMax = <%:(DefaultMax)%>;
        var hmax = <%=hystMax%>;

        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinnerThres = arrayBuilder(dMin, dMax, stepUnit);
        createSpinnerModal("minThreshNum", "<%: label %>", "MinimumThreshold_Manual", arrayForSpinnerThres);
        createSpinnerModal("maxThreshNum", "<%: label %>", "MaximumThreshold_Manual", arrayForSpinnerThres);

        const arrayForSpinnerHyst = arrayBuilder(0, hmax, stepUnit);
        createSpinnerModal("hystNum", "<%: label %>", "Hysteresis_Manual", arrayForSpinnerHyst);

        <%}%>


        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < dMin)
                    $("#MinimumThreshold_Manual").val(dMin);
                if ($("#MinimumThreshold_Manual").val() > dMax)
                    $("#MinimumThreshold_Manual").val(dMax);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                $("#MaximumThreshold_Manual").change();

            } else {

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < dMin)
                    $("#MaximumThreshold_Manual").val(dMin);
                if ($("#MaximumThreshold_Manual").val() > dMax)
                    $("#MaximumThreshold_Manual").val(dMax);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                $("#MaximumThreshold_Manual").change();

            } else {
                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > hmax)
                    $("#Hysteresis_Manual").val(hmax)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>
<%} %>