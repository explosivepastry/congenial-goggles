<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%          

    string Min = "";
    string label = "";
    string savedValue = "";
    string profileLow = "";
    string profileHigh = "";
    double DefaultMin = 0d;

    //Must be done by static method on type because 79, 82, 83 ,144,and 145  also use this page
    double DefaultMax = (Model.DefaultValue<long>("DefaultMaximumThreshold") / 10d);
    label = PressureNPSI.GetLabel(Model.SensorID);
    double tempMinPSI = Model.MinimumThreshold / 10.0;

    if (label.ToLower() == "custom")
    {
        label = PressureNPSI.GetCustomLabel(Model.SensorID);
        savedValue = PressureNPSI.GetSavedValue(Model.SensorID).ToString();
        profileLow = PressureNPSI.GetLowValue(Model.SensorID).ToString();
        profileHigh = PressureNPSI.GetHighValue(Model.SensorID).ToString();
        DefaultMax = profileHigh.ToDouble();

        int decimalTrunkValue = PressureNPSI.GetDecimalTrunkValue(Model.SensorID).ToInt();
        double tempMinCustom = tempMinPSI.LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble());
        Min = Math.Round(tempMinCustom, decimalTrunkValue).ToString();
    }
    else
    {
        switch (label)
        {
            case "PSI":
                Min = tempMinPSI.ToString();
                break;
            case "atm":
                DefaultMax = DefaultMax * .068;
                Min = (tempMinPSI * .068).ToString();
                break;
            case "bar":
                DefaultMax = DefaultMax * 0.0689;
                Min = (tempMinPSI * 0.0689).ToString();
                break;
            case "kPA":
                DefaultMax = DefaultMax * 6.89;
                Min = (tempMinPSI * 6.89).ToString();
                break;
            case "Torr":
                DefaultMax = DefaultMax * 51.71;
                Min = (tempMinPSI * 51.71).ToString();
                break;
        }
    }
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>

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

<script>
    var dMin = <%:(DefaultMin)%>;
    var dMax = <%:(DefaultMax)%>;

    $(function () {

                <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(Number(dMin), Number(dMax), 1);
        createSpinnerModal("minThreshNum", "<%=label %>", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < dMin)
                    $("#MinimumThreshold_Manual").val(dMin);
                if ($("#MinimumThreshold_Manual").val() > dMax)
                    $("#MinimumThreshold_Manual").val(dMax);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
            }
        });

    });
</script>
