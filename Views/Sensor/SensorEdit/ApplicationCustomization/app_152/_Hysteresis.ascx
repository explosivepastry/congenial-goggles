<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string Hyst = "";
    string label = "";
    string profileLow = "";
    string profileHigh = "";

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
    MonnitApplicationBase.ProfileLabelForScale(Model, out label);

    label = LN2Level.GetLabel(Model.SensorID);

    if (label.ToLower() == "custom")
    {
        label = LN2Level.GetCustomLabel(Model.SensorID);
        LN2Level profile = new LN2Level();
        profileLow = profile.LowValue.Value.ToString();
        //int defaultVal = (Model.DefaultValue<long>("DefaultCalibration1") / 10.0).ToInt();
        profileHigh = profile.HighValue.Value.ToString();
        Hyst = (Model.Hysteresis).ToDouble().LinearInterpolation(profileLow.ToDouble(), 0, profileHigh.ToDouble(), 100).ToString();
    }
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Aware State Buffer","Aware State Buffer")%> (%)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script>

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(1, 5, 1);
        createSpinnerModal("hystNum", '%', "Hysteresis_Manual", arrayForSpinner);


        <%}%>

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
