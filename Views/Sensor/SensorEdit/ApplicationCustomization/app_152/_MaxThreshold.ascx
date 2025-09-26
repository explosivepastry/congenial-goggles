<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Min = "";
    string Max = "";
    string label = "";
    string savedValue = "";
    string profileLow = "";
    string profileHigh = "";

    var DefaultMin = 0d;
    //var DefaultMax = MonnitApplicationBase.CallStaticMethod(Model.ApplicationID, "GetSavedValue", new object[] { Model.SensorID }).ToDouble();
    double DefaultMax = (Model.DefaultValue<long>("DefaultMaximumThreshold") / 10d);

    label = LN2Level.GetLabel(Model.SensorID);

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
    //MonnitApplicationBase.ProfileLabelForScale(Model, out label);


    if (label.ToLower() == "custom")
    {
        label = LN2Level.GetCustomLabel(Model.SensorID);
        savedValue = LN2Level.GetSavedValue(Model.SensorID).ToString();
        profileLow = LN2Level.GetLowValue(Model.SensorID).ToString();
        profileHigh = LN2Level.GetHighValue(Model.SensorID).ToString();

        DefaultMin = profileLow.ToDouble();
        DefaultMax = profileHigh.ToDouble();

        Min = (Model.MinimumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
        Max = (Model.MaximumThreshold / 10d).LinearInterpolation(0, profileLow.ToDouble(), savedValue.ToDouble(), profileHigh.ToDouble()).ToString();
    }

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> (%)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

