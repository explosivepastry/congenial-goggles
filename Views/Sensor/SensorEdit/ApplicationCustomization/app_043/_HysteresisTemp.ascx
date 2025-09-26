<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isF = Humidity.IsFahrenheit(Model.SensorID);

    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        //long HystTemp = (System.Convert.ToInt32(Hyst) & 0x0000FFFF) / 100;
        long number = Convert.ToInt64(Hyst);
        double HystTemp = (BitConverter.ToInt16(BitConverter.GetBytes(number), 0) / 100.0);

        if (isF)
        {
            HystTemp = HystTemp * 9.0 / 5.0;
            HystTemp = Math.Round(HystTemp, 2);
        }
%>

<div class="row sensorEditForm" style="padding-bottom: 0.75rem">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Aware State Buffer","Aware State Buffer")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_ManualTemp" id="Hysteresis_ManualTemp" value="<%=HystTemp %>" />
        <a id="hystNumTemp" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinnerTemp = arrayBuilder(0, 5, 1);
        createSpinnerModal("hystNumTemp", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinnerTemp);
        <%}%>

        $("#Hysteresis_ManualTemp").change(function () {
            if (isANumber($("#Hysteresis_ManualTemp").val())) {
                if ($("#Hysteresis_ManualTemp").val() < 0)
                    $("#Hysteresis_ManualTemp").val(0);
                if ($("#Hysteresis_ManualTemp").val() > 5)
                    $("#Hysteresis_ManualTemp").val(5)
            }
            else {
                $('#Hysteresis_ManualTemp').val(<%: Hyst%>);
            }
        });
    });
</script>
<%} %>