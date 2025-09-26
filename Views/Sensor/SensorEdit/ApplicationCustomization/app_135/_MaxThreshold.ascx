<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Max = "";
    Max = SoilMoisture.GetMoistureThreshMax(Model).ToString();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_135|Above","Above")%>  (<%: Html.Label(SoilMoisture.GetLabel(Model.SensorID))%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MaxThresMinVal = 0;
        var MaxThresMaxVal = 240;

        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(MaxThresMinVal, MaxThresMaxVal, 10);
        createSpinnerModal("maxThreshNum", " <%:SoilMoisture.GetLabel(Model.SensorID) %>", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < MaxThresMinVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMinVal);
                if ($("#MaximumThreshold_Manual").val() > MaxThresMaxVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMaxVal);

                if (Number($('#MaximumThreshold_Manual').val()) <= Number($('#MinimumThreshold_Manual').val()))
                    $('#MaximumThreshold_Manual').val((Number($('#MinimumThreshold_Manual').val()) + 1));


            } else {

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
