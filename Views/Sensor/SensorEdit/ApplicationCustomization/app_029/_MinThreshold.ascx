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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />

        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThresMinVal = 0;
        var MinThresMaxVal = 200;

        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 100, 10);
        createSpinnerModal("minThreshNum", "Minimum Threshold", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall')

        $("#MinimumThreshold_Manual").change(function () {
            let max = parseFloat($("#MaximumThreshold_Manual").val());
            let min = parseFloat($("#MinimumThreshold_Manual").val());

            if (isANumber(min)) {
                if (min < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if (min > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if (min > max)
                    $("#MinimumThreshold_Manual").val(max);

                $("#MaximumThreshold_Manual").change();

            } else {
                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });
    });
</script>
<%} %>