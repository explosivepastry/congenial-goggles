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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_073|Aware State Overflow Count","Aware State Overflow Count")%>  (<%: label  %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
     
        var transVal = <%: FilteredPulseCounter.GetTransform(Model.SensorID)%>;

        var MinThresMinVal = (1 * transVal );
        var MinThresMaxVal = (0xFFFFFFFF * transVal);
     

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(MinThresMinVal, MinThresMaxVal, 100000000);
        createSpinnerModal("minThreshNum", "<%=label%>", "MinimumThreshold_Manual", arrayForSpinner1);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if ($("#MinimumThreshold_Manual").val() > $("#MaximumThreshold_Manual").val())
                    $("#MinimumThreshold_Manual").val($("#MaximumThreshold_Manual").val());

            }else{

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>