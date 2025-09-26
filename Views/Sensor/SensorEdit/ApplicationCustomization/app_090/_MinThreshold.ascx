<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";
        string label = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Aware State Overflow Count","Aware State Overflow Count")%>:  (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" /> 

<%--        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var transVal = <%: FilteredPulseCounter64.GetTransform(Model.SensorID)%>;
     

        <% if (Model.CanUpdate)
           { %>

<%--        let arrayForSpinner = arrayBuilder(1 * transVal, 0xFFFFFFFF * transVal, 1000000);
        createSpinnerModal("minThreshNum", "<%=label %>", "MinimumThreshold_Manual", arrayForSpinner);--%>

         <%}%>

        $('#MinimumThreshold_Manual').change(function ()
        {
            var transVal = <%: FilteredPulseCounter64.GetTransform(Model.SensorID)%>;
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < (1 * transVal ))
                    $('#MinimumThreshold_Manual').val(1 * transVal);

                if ($('#MinimumThreshold_Manual').val() > (0xFFFFFFFF * transVal ))
                    $('#MinimumThreshold_Manual').val((0xFFFFFFFF * transVal));
            }
            else
            {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
        }
        });


    });
</script>
<%} %>