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
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Model.MinimumThreshold / 100 %>" />
        <%: label %>
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var MinThresMinVal = <%=Min%>;
        var MinThresMaxVal = <%=Max%>;

        <% if (Model.CanUpdate)
            { %>

        let arrayForSpinner = arrayBuilder(MinThresMinVal, MinThresMaxVal, 1);
        createSpinnerModal("minThreshNum", "Min Threshold", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                $("#MaximumThreshold_Manual").change();

            } else {

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>