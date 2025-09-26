<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
        string Min = "";
        string Max = "";
        string label = VoltageMeter500VAC.GetLabel(Model.SensorID);
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%>  (<%: label  %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
       
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    var lowVal = <%:  (VoltageMeter500VAC.GetLowValue(Model.SensorID).ToDouble())%>;
    var highVal = <%:  (VoltageMeter500VAC.GetHighValue(Model.SensorID).ToDouble())%>;

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(lowVal, highVal, 10);
        createSpinnerModal("minThreshNum", "<%: label %>", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < lowVal)
                    $('#MinimumThreshold_Manual').val(lowVal);

                if ($('#MinimumThreshold_Manual').val() > highVal )
                    $('#MinimumThreshold_Manual').val(highVal);

                
                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));

                $('#Hysteresis_Manual').change();
            }
            else
                $('#MinimumThreshold_Manual').val(<%:  (Min)%>);
        });

    });
</script>