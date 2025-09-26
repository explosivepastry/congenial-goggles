<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Min = "";
    string Max = "";
    string label = ZeroToTwoHundredVolts.GetLabel(Model.SensorID);
    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<p class="useAwareState">Use Aware State</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%>  (<%: label%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
       
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {

        var lowVal = <%: ZeroToTwoHundredVolts.GetLowValue(Model.SensorID).ToDouble()%>;
        var highVal = <%: ZeroToTwoHundredVolts.GetHighValue(Model.SensorID).ToDouble()%>;
     
        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(lowVal, 200, 5);
        createSpinnerModal("minThreshNum", '<%: label%>', "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < lowVal)
                    $('#MinimumThreshold_Manual').val(lowVal);

                if ($('#MinimumThreshold_Manual').val() > highVal )
                    $('#MinimumThreshold_Manual').val(highVal);
            }
            else
                $('#MinimumThreshold_Manual').val(<%: Min%>);

        });

    });
</script>
