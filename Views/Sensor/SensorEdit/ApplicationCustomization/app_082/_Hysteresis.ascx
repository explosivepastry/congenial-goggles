<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%          

    string Hyst = "";
    string label = "";
    string savedValue = PressureNPSI.GetSavedValue(Model.SensorID).ToString();
    string profileLow = "";
    string profileHigh = "";
    int decimalTrunkValue = PressureNPSI.GetDecimalTrunkValue(Model.SensorID).ToInt();
    var DefaultMin = 0d;

    //Must be done by static method on type because 79, 82, 83, 144, and 145 also use this page
    double DefaultMax = (Model.DefaultValue<long>("DefaultMaximumThreshold") / 10d);

    if (Model.ApplicationID == 79)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = Pressure50PSI.GetLabel(Model.SensorID);
        if (label.ToLower() == "custom")
        {
            Pressure50PSI profile = new Pressure50PSI();
            profile.SetSensorAttributes(Model.SensorID);
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue((Model.DefaultValue<long>("DefaultCalibration4") / 10).ToInt()).Value.ToString();
            label = Pressure50PSI.GetCustomLabel(Model.SensorID);

            double hyst = Model.Hysteresis;
            //Reversed linear 
            double x = hyst / Math.Pow(10, decimalTrunkValue + 1);
            x = x.LinearInterpolation(0, profile.LowValue.Value.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble());
            x = Math.Round(x, decimalTrunkValue);

            Hyst = x.ToString();
        }
        else
        {
            Hyst = Math.Round(Pressure50PSI.HystForUI(Model).ToDouble() * 10d, 0).ToString();
        }

    }

    if (Model.ApplicationID == 82)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        label = Pressure300PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            Pressure300PSI profile = new Pressure300PSI();
            profile.SetSensorAttributes(Model.SensorID);
            profileLow = profile.LowValue.Value.ToString();
            label = Pressure300PSI.GetCustomLabel(Model.SensorID);
            profileHigh = profile.HighValue((Model.DefaultValue<long>("DefaultCalibration4") / 10).ToInt()).Value.ToString();
            double diff = Math.Abs((profileHigh.ToDouble() - profileLow.ToDouble()) / 300);

            //if (diff != 0)
            //{
            //    double y = Model.Hysteresis * diff / Math.Pow(10, decimalTrunkValue + 1);
            //    Hyst = Math.Round(y, decimalTrunkValue).ToString();
            //}
            //else
            //{
            //    Hyst = Math.Round(((Model.Hysteresis * 1d) / 10), decimalTrunkValue).ToString();
            //}
            
            double hyst = Model.Hysteresis.ToDouble();
            double x = hyst;
            x = x.LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble());
            //x = Math.Round(x, decimalTrunkValue);

            Hyst = x.ToString();
        }
        else
        {
            Hyst = Math.Round(Pressure300PSI.HystForUI(Model).ToDouble() * 10d, decimalTrunkValue).ToString();
        }

    }

    if (Model.ApplicationID == 83)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = PressureNPSI.GetLabel(Model.SensorID);
        DefaultMax = MonnitApplicationBase.CallStaticMethod(83, "GetSavedValue", new object[] { Model.SensorID }).ToDouble();// *multiplyer;
        if (label.ToLower() == "custom")
        {
            label = PressureNPSI.GetCustomLabel(Model.SensorID);

            PressureNPSI profile = new PressureNPSI();
            profile.SetSensorAttributes(Model.SensorID);

            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue((Model.DefaultValue<long>("DefaultCalibration4") / 10).ToInt()).Value.ToString();
            //Hyst = Math.Round(PressureNPSI.HystForUI(Model).ToDouble(), decimalTrunkValue).ToString();
            double diff = Math.Abs((profileHigh.ToDouble() - profileLow.ToDouble()) / defaultVal);

            if (diff != 0)
            {
                double y = Model.Hysteresis * diff / Math.Pow(10, decimalTrunkValue + 1);
                Hyst = (Math.Round(y, decimalTrunkValue) / 10).ToString();
                //Hyst = Math.Round(((Model.Hysteresis * diff) / 100), decimalTrunkValue).ToString();
            }
            else
            {
                Hyst = Math.Round(((Model.Hysteresis * 1d) / 100), decimalTrunkValue).ToString();
            }
        }
        else
        {
            Hyst = Math.Round(PressureNPSI.HystForUI(Model).ToDouble() * 10d, decimalTrunkValue).ToString();
        }
    }

    if (Model.ApplicationID == 144)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = Pressure750PSI.GetLabel(Model.SensorID);
        if (label.ToLower() == "custom")
        {
            label = Pressure750PSI.GetCustomLabel(Model.SensorID);

            Pressure750PSI profile = new Pressure750PSI();
            profile.SetSensorAttributes(Model.SensorID);

            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue((Model.DefaultValue<long>("DefaultCalibration4") / 10).ToInt()).Value.ToString();

            double diff = Math.Abs((profileHigh.ToDouble() - profileLow.ToDouble()) / defaultVal);

            if (diff != 0)
            {
                double y = Model.Hysteresis * diff / Math.Pow(10, decimalTrunkValue + 1);
                Hyst = Math.Round(y, decimalTrunkValue).ToString();
                //Hyst = Math.Round(((Model.Hysteresis * diff) / 10), decimalTrunkValue).ToString();
            }
            else
            {
                Hyst = Math.Round(((Model.Hysteresis * 1d) / 10), decimalTrunkValue).ToString();
            }
        }
        else
        {
            Hyst = Math.Round(Pressure750PSI.HystForUI(Model).ToDouble() * 10d, decimalTrunkValue).ToString();
        }

    }

    if (Model.ApplicationID == 145)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = Pressure3000PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure3000PSI.GetCustomLabel(Model.SensorID);

            Pressure3000PSI profile = new Pressure3000PSI();
            profile.SetSensorAttributes(Model.SensorID);

            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();

            double diff = Math.Abs((profileHigh.ToDouble() - profileLow.ToDouble()) / defaultVal);

            if (diff != 0)
            {

                double y = Model.Hysteresis * diff / Math.Pow(10, decimalTrunkValue + 1);
                Hyst = Math.Round(y, decimalTrunkValue).ToString();
                //Hyst = Math.Round(((Model.Hysteresis * diff) / 10), decimalTrunkValue).ToString();
            }
            else
            {
                Hyst = Math.Round(((Model.Hysteresis * 1d) / 10), decimalTrunkValue).ToString();
            }
        }
        else
        {
            Hyst = Math.Round(Pressure3000PSI.HystForUI(Model).ToDouble() * 10d, decimalTrunkValue).ToString();
        }

    }
%>

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

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 5, 1);
        createSpinnerModal("hystNum", "<%=label %>", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 5)
                    $("#Hysteresis_Manual").val(5)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>
