<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%          

    string Min = "";
    string Max = "";
    string label = "";
    string savedValue = "";
    string profileLow = "";
    string profileHigh = "";

    // var multiplyer = PressureNPSI.GetTransform(Model.SensorID);
    var DefaultMin = 0d;
    //Must be done by static method on type because 79 and 82 also use this page
    var DefaultMax = MonnitApplicationBase.CallStaticMethod(Model.ApplicationID, "GetSavedValue", new object[] { Model.SensorID }).ToDouble();// *multiplyer;


    if (Model.ApplicationID == 79)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        label = Pressure50PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure50PSI.GetCustomLabel(Model.SensorID);
            savedValue = Pressure50PSI.GetSavedValue(Model.SensorID).ToString();
            profileLow = Pressure50PSI.GetLowValue(Model.SensorID).ToString();
            profileHigh = Pressure50PSI.GetHighValue(Model.SensorID).ToString();

            DefaultMin = profileLow.ToDouble();
            DefaultMax = profileHigh.ToDouble();

            Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
            Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();

        }


    }

    if (Model.ApplicationID == 82)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        label = Pressure300PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure300PSI.GetCustomLabel(Model.SensorID);

            savedValue = Pressure300PSI.GetSavedValue(Model.SensorID).ToString();
            profileLow = Pressure300PSI.GetLowValue(Model.SensorID).ToString();
            profileHigh = Pressure300PSI.GetHighValue(Model.SensorID).ToString();
            DefaultMin = profileLow.ToDouble();
            DefaultMax = profileHigh.ToDouble();


            Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
            Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();


        }
    }


    if (Model.ApplicationID == 83)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);



        label = PressureNPSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = PressureNPSI.GetCustomLabel(Model.SensorID);
            if (label.ToLower() == "custom")
            {
                label = PressureNPSI.GetCustomLabel(Model.SensorID);
                savedValue = PressureNPSI.GetSavedValue(Model.SensorID).ToString();
                profileLow = PressureNPSI.GetLowValue(Model.SensorID).ToString();
                profileHigh = PressureNPSI.GetHighValue(Model.SensorID).ToString();
                DefaultMin = profileLow.ToDouble();
                DefaultMax = profileHigh.ToDouble();

                Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
                Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();

            }
        }
    }

    
    if (Model.ApplicationID == 144)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        label = Pressure750PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure300PSI.GetCustomLabel(Model.SensorID);

            savedValue = Pressure300PSI.GetSavedValue(Model.SensorID).ToString();
            profileLow = Pressure300PSI.GetLowValue(Model.SensorID).ToString();
            profileHigh = Pressure300PSI.GetHighValue(Model.SensorID).ToString();
            DefaultMin = profileLow.ToDouble();
            DefaultMax = profileHigh.ToDouble();


            Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
            Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();


        }
    }

        if (Model.ApplicationID == 145)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        label = Pressure3000PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure3000PSI.GetCustomLabel(Model.SensorID);

            savedValue = Pressure3000PSI.GetSavedValue(Model.SensorID).ToString();
            profileLow = Pressure3000PSI.GetLowValue(Model.SensorID).ToString();
            profileHigh = Pressure3000PSI.GetHighValue(Model.SensorID).ToString();
            DefaultMin = profileLow.ToDouble();
            DefaultMax = profileHigh.ToDouble();


            Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
            Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();


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

        const arrayForSpinner = arrayBuilder((dMin / 10), (dMax / 10), 10);
        createSpinnerModal("minThreshNum", "<%: label %>", "MinimumThreshold_Manual", arrayForSpinner);

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
                //$("#MaximumThreshold_Manual").change();
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
            }
        });

    });
</script>
