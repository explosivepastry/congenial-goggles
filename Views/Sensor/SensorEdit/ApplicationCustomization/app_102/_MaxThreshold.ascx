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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_102|PM10 Threshold","PM10 Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    var pmInterval_array = [10, 50, 100, 250, 500, 750, 1000];

    $(function () {

                <% if (Model.CanUpdate)
    { %>
        createSpinnerModal("maxThreshNum", "μg/m^3", "MaximumThreshold_Manual", pmInterval_array);

                <%}%>

        $("#MaximumThreshold_Manual").addClass('editField editFieldMedium');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < 10)
                    $("#MaximumThreshold_Manual").val(10);
                if ($("#MaximumThreshold_Manual").val() > 1000)
                    $("#MaximumThreshold_Manual").val(1000);
            }
            else {
                $('#MaximumThreshold_Manual').val(<%: Max%>);
            }
        });

    });
</script>
<%} %>