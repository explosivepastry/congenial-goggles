<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        long HumidMin = (System.Convert.ToInt32(Min) >> 16) / 100;

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State Humidity","Use Aware State Humidity")%></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> 
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=HumidMin %>" />
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

        let arrayForSpinner = arrayBuilder(MinThresMinVal, 100, 10);
        createSpinnerModal("minThreshNum", "Below", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall')

        $("#MinimumThreshold_Manual").change(function () {
            let min = parseFloat($("#MinimumThreshold_Manual").val());
            let max = parseFloat($("#MaximumThreshold_Manual").val());

            if (isANumber(min)){
                if (min < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if (min > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if (min > max)
                    $("#MinimumThreshold_Manual").val(max);
            }else{
                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>