<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%


    double ActiveTempDeltaD = SootBlower2.GetActiveTemperatureDelta(Model);

    
    bool isF = SootBlower2.IsFahrenheit(Model.SensorID);
    int tempDeltaMin = 1; //C
    int tempDeltaMax = 50; //C
    int ActiveTempDelta = (int)Math.Round(ActiveTempDeltaD); //C

    if (isF)
    {
        tempDeltaMin = 2; //F
        tempDeltaMax = 90; //F
        ActiveTempDelta = (int)Math.Round(ActiveTempDeltaD * 1.8); //F
    }

%>

<div class="row sensorEditForm">
  <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Temperature Delta","Active Mode Temperature Delta")%> (<%: Html.Label(isF ?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" >
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveTempDelta" id="ActiveTempDelta" value="<%=ActiveTempDelta %>" />
        <a id="ActiveTempDeltaNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<script type="text/javascript">
    $('#ActiveTempDelta').addClass("editField editFieldMedium");

    $(function () {
        var tempDeltaMin = Number(<%=tempDeltaMin%>);
        var tempDeltaMax = Number(<%=tempDeltaMax%>);

          <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(tempDeltaMin, tempDeltaMax, 1);
        createSpinnerModal("ActiveTempDeltaNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "ActiveTempDelta", arrayForSpinner);

      <%}%>

        $('#ActiveTempDelta').change(function () {
            if (isANumber($("#ActiveTempDelta").val())) {
                if (Number($('#ActiveTempDelta').val()) < Number(tempDeltaMin))
                    $('#ActiveTempDelta').val(tempDeltaMin);

                if (Number($('#ActiveTempDelta').val()) > Number(tempDeltaMax))
                    $('#ActiveTempDelta').val(tempDeltaMax);
            }
            else {
                $('#ActiveTempDelta').val(<%: ActiveTempDelta%>);

            }
        });


    });
</script>
