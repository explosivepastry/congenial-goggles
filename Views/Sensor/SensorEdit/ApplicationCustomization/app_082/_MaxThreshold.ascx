<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%          

    string Min = "";
    string Max = "";
    string label = "";
    string savedValue = "";
    string profileLow = "";
    string profileHigh = "";
    var DefaultMin = 0d;

    //Must be done by static method on type because 79, 82, 144,and 145  also use this page
    double DefaultMax = (Model.DefaultValue<long>("DefaultMaximumThreshold") / 10d);

    label = PressureNPSI.GetLabel(Model.SensorID);
    double tempMaxPSI = Model.MaximumThreshold / 10.0;

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);


    if (label.ToLower() == "custom")
    {
        label = PressureNPSI.GetCustomLabel(Model.SensorID);
        savedValue = PressureNPSI.GetSavedValue(Model.SensorID).ToString();
        profileLow = PressureNPSI.GetLowValue(Model.SensorID).ToString();
        profileHigh = PressureNPSI.GetHighValue(Model.SensorID).ToString();
        DefaultMax = profileHigh.ToDouble();

        int decimalTrunkValue = PressureNPSI.GetDecimalTrunkValue(Model.SensorID).ToInt();
        double tempMaxCustom = tempMaxPSI.LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble());
        Max = Math.Round(tempMaxCustom, decimalTrunkValue).ToString();
    }
    else
    {
        switch (label)
        {
            case "PSI":
                Max = tempMaxPSI.ToString();
                break;
            case "atm":
                DefaultMax = DefaultMax * .068;
                Max = (tempMaxPSI * .068).ToString();
                break;
            case "bar":
                DefaultMax = DefaultMax * 0.0689;
                Max = (tempMaxPSI *  0.0689).ToString();
                break;
            case "kPA":
                DefaultMax = DefaultMax * 6.89;
                Max = (tempMaxPSI *  6.89).ToString();
                break;
            case "Torr":
                DefaultMax = DefaultMax * 51.71;
                Max = (tempMaxPSI *  51.71).ToString();
                break;
        }
    }

%>

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


<script>
    var dMin = <%:(DefaultMin)%>;
    var dMax = <%:(DefaultMax)%>;

    $(function () {

               <% if (Model.CanUpdate)
    { %>
        let arrayForSpinner = arrayBuilder(Number(dMin), Number(dMax), 1);
        createSpinnerModal("maxThreshNum", "<%=label %>", "MaximumThreshold_Manual", arrayForSpinner);


        <%}%>
        $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < dMin)
                    $("#MaximumThreshold_Manual").val(dMin);
                if ($("#MaximumThreshold_Manual").val() > dMax)
                    $("#MaximumThreshold_Manual").val(dMax);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
            }
            else {
                $('#MaximumThreshold_Manual').val(<%: Max%>);
            }
        });

    });
</script>
