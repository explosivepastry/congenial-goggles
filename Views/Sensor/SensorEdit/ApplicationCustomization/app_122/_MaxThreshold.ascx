<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 

        string Min = "";
        string Max = "";
        string label = VoltageMeter500VAC.GetLabel(Model.SensorID);
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    var lowVal = <%:  (VoltageMeter500VAC.GetLowValue(Model.SensorID).ToDouble())%>;
    var highVal = <%:  (VoltageMeter500VAC.GetHighValue(Model.SensorID).ToDouble())%>;

    //MobiScroll
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(lowVal, highVal, 10);
        createSpinnerModal("maxThreshNum", "<%: label %>", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $('#MaximumThreshold_Manual').change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($('#MaximumThreshold_Manual').val() < lowVal)
                    $('#MaximumThreshold_Manual').val(lowVal);

                if ($('#MaximumThreshold_Manual').val() > highVal )
                    $('#MaximumThreshold_Manual').val(highVal);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                $('#Hysteresis_Manual').change();

            }else
                $('#MaximumThreshold_Manual').val(<%:  (Max)%>);
        });
    });
</script>