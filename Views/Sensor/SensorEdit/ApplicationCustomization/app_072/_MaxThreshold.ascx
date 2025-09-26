<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Get Pressure label for profile
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        //Get the Hyst, Max, Min Values for Volts
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> <%: Html.Label(label) %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    var lowVal = <%: ZeroToFiveVolts.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: ZeroToFiveVolts.GetHighValue(Model.SensorID).ToDouble()%>;

    //MobiScroll
    $(function () {

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(0, 10, 1);
        createSpinnerModal("maxThreshNum", "<%=label%>", "MaximumThreshold_Manual", arrayForSpinner1);
<%}%>
        $('#MaximumThreshold_Manual').change(function () {      
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($('#MaximumThreshold_Manual').val() < lowVal)
                    $('#MaximumThreshold_Manual').val(lowVal);

                if ($('#MaximumThreshold_Manual').val() > highVal )
                    $('#MaximumThreshold_Manual').val(highVal);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                //$("#MinimumThreshold_Manual").change();
            }
            else
                $('#MaximumThreshold_Manual').val(<%: Max%>);

            });

    
    });
        
</script>
<%}%>