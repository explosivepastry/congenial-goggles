<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%> %
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThresMinVal = 8;
        var MinThresMaxVal = 75;
     

        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(8, 75, 5);
        createSpinnerModal("minThreshNum", "Milliseconds", "MinimumThreshold_Manual", arrayForSpinner);


        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));

            }else{

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

        <%}%>
    });
</script>
<%} %>