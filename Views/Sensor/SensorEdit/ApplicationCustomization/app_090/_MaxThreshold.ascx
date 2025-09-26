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
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Overflow Count","Overflow Count")%>:  (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />

<%--        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    $(function () {


        var transVal = <%: FilteredPulseCounter64.GetTransform(Model.SensorID)%>;


        <% if (Model.CanUpdate)
           { %>

        //let arrayForSpinner = arrayBuilder(1 * transVal, 0xFFFFFFFF * transVal, 1000000);
        //createSpinnerModal("maxThreshNum", "Amps", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($('#MaximumThreshold_Manual').val() < (1 * transVal ))
                    $('#MaximumThreshold_Manual').val(1 * transVal);

                if ($('#MaximumThreshold_Manual').val() > (0xFFFFFFFF * transVal ))
                    $('#MaximumThreshold_Manual').val((0xFFFFFFFF * transVal ));
            }
            else
                $('#MaximumThreshold_Manual').val(<%: Max%>);
        });

    });
</script>
<%} %>