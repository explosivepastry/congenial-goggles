<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";


        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />

        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MaxThresMinVal = 0;
        var MaxThresMaxVal = 200;

        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 100, 10);
        createSpinnerModal("maxThreshNum", "Maximum Threshold", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            let max = parseFloat($("#MaximumThreshold_Manual").val());
            let min = parseFloat($("#MinimumThreshold_Manual").val());

            if (isANumber(max)) {
                if (max < MaxThresMinVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMinVal);
                if (max > MaxThresMaxVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMaxVal);

                if (max < min)
                    $("#MaximumThreshold_Manual").val(min);
            } else {

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>