<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>
<p class="useAwareState">Use Aware State</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled"  %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    var lowVal = <%: Analog.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: Analog.GetHighValue(Model.SensorID).ToDouble()%>;

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("minThreshNum", "Min Threshold", "MinimumThreshold_Manual", [0, 1]);
    
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
        <%}%>
    });

</script>
<%}%>