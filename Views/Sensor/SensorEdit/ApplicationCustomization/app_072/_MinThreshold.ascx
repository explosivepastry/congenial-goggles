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
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> <%: Html.Label(label) %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    var lowVal = <%: ZeroToTenVolts.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: ZeroToTenVolts.GetHighValue(Model.SensorID).ToDouble()%>;


    //MobiScroll
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(0, 10, 1);
        createSpinnerModal("minThreshNum", "<%=label%>", "MinimumThreshold_Manual", arrayForSpinner1);

<%}%>
         

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < lowVal)
                    $('#MinimumThreshold_Manual').val(lowVal);

                if ($('#MinimumThreshold_Manual').val() > highVal )
                    $('#MinimumThreshold_Manual').val(highVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                //$("#MaximumThreshold_Manual").change();
            }
            else
                $('#MinimumThreshold_Manual').val(<%: Min%>);

        });

    });
</script>
<%}%>