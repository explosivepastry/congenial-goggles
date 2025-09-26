<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string Hyst = "";

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
    bool isF = SootBlower.IsFahrenheit(Model.SensorID);
    double tempDeltaMin = 1;
    double tempDeltaMax = 50;

    if (isF)
    {
        tempDeltaMin = (tempDeltaMin.ToDouble() * 1.8);
        tempDeltaMax = (tempDeltaMax.ToDouble() * 1.8);

    }
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Temperature Delta","Active Mode Temperature Delta")%> (<%: Html.Label(isF ?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12">
    </div>
</div>

<script type="text/javascript">
    $('#Hysteresis_Manual').addClass("editField editFieldMedium");

    $(function () {
        var tempDeltaMin = Number(<%=tempDeltaMin%>);
        var tempDeltaMax = Number(<%=tempDeltaMax%>);

          <% if (Model.CanUpdate)
    { %>
        const arrayForSpinner = arrayBuilder(tempDeltaMin, tempDeltaMax, 1);
        createSpinnerModal("hystNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "Hysteresis_Manual", assessment_array);
      <%}%>

        $('#Hysteresis_Manual').change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if (Number($('#Hysteresis_Manual').val()) < Number(tempDeltaMin))
                    $('#Hysteresis_Manual').val(tempDeltaMin);

                if (Number($('#Hysteresis_Manual').val()) > Number(tempDeltaMax))
                    $('#Hysteresis_Manual').val(tempDeltaMax);
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

            }
        });


    });
</script>
