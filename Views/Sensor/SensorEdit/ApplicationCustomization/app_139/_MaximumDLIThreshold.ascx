<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-DLIThreshold-Title","DLI Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumDLIThreshold" id="MaximumDLIThreshold" value="<%=LightSensor_PPFD.GetMaximumDLIThreshold(Model) %>" />
        <a id="DLIThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessage("MaximumDLIThreshold")%>
    </div>
</div>

<script>

    $(function () {
        var DLIThresMinVal = 0;
        var DLIThresMaxVal = 500;

               <% if (Model.CanUpdate)
                  { %>

        const arrayForSpinner = arrayBuilder(DLIThresMinVal, DLIThresMaxVal, 10);
        createSpinnerModal("DLIThreshNum", "Max Threshold PAR DLI", "MaximumDLIThreshold", arrayForSpinner);

        <%}%>

        $("#MaximumDLIThreshold").change(function () {
            if (isANumber($("#MaximumDLIThreshold").val())) {
                if ($("#MaximumDLIThreshold").val() < DLIThresMinVal)
                    $("#MaximumDLIThreshold").val(DLIThresMinVal);
                if ($("#MaximumDLIThreshold").val() > DLIThresMaxVal)
                    $("#MaximumDLIThreshold").val(DLIThresMaxVal);
            } else {

                $("#MaximumDLIThreshold").val(500);
            }
        });

    });
</script>
