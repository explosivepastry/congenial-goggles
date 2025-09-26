<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Min = "";
    Min = SoilMoisture.GetMoistureThreshMin(Model).ToString();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_135|Below","Below")%> (<%: Html.Label(SoilMoisture.GetLabel(Model.SensorID))%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThresMinVal = 0;
        var MinThresMaxVal = 240;


        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(MinThresMinVal, MinThresMaxVal, 10);
        createSpinnerModal("minThreshNum", " <%:SoilMoisture.GetLabel(Model.SensorID) %>", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);


                if (Number($('#MinimumThreshold_Manual').val()) >= Number($('#MaximumThreshold_Manual').val()))
                    $('#MinimumThreshold_Manual').val((Number($('#MaximumThreshold_Manual').val()) - 1));

            } else {

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
