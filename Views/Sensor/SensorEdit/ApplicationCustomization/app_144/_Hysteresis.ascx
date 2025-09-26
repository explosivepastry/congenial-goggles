<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%          

    string Hyst = "";
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
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = Pressure50PSI.GetLabel(Model.SensorID);
        if (label.ToLower() == "custom")
        {
            Pressure50PSI profile = new Pressure50PSI();
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();


            label = Pressure50PSI.GetCustomLabel(Model.SensorID);
            Hyst = (Model.Hysteresis / 10d).LinearInterpolation( profileLow.ToDouble(),0, profileHigh.ToDouble(),50).ToString();
        }
    }

    if (Model.ApplicationID == 82)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = Pressure300PSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = Pressure300PSI.GetCustomLabel(Model.SensorID);
            Pressure300PSI profile = new Pressure300PSI();
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();
            Hyst = (Model.Hysteresis / 10d).LinearInterpolation( profileLow.ToDouble(),0 , profileHigh.ToDouble(),300).ToString();
        }
    }

    if (Model.ApplicationID == 83)
    {
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

        label = PressureNPSI.GetLabel(Model.SensorID);

        if (label.ToLower() == "custom")
        {
            label = PressureNPSI.GetCustomLabel(Model.SensorID);
            PressureNPSI profile = new PressureNPSI();
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();
            Hyst = (Model.Hysteresis / 10d).LinearInterpolation(profileLow.ToDouble(),0, profileHigh.ToDouble(),500).ToString();
            
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
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();
            Hyst = (Model.Hysteresis / 10d).LinearInterpolation( profileLow.ToDouble(),0 , profileHigh.ToDouble(),750).ToString();
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
            profileLow = profile.LowValue.Value.ToString();
            int defaultVal = (Model.DefaultValue<long>("DefaultCalibration4") / 10.0).ToInt();
            profileHigh = profile.HighValue(defaultVal).Value.ToString();
            Hyst = (Model.Hysteresis / 10d).LinearInterpolation( profileLow.ToDouble(),0 , profileHigh.ToDouble(),3000).ToString();
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

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(0, 5, 1);
        createSpinnerModal("hystNum", "<%: label %>", "Hysteresis_Manual", arrayForSpinner);

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
