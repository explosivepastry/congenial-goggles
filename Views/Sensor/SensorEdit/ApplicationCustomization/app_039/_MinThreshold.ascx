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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Threshold","Aware State Threshold")%> Vehicles
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        let arrayForSpinner = arrayBuilder(0, 65535, 50);
        createSpinnerModal("minThreshNum", "Below", "MinimumThreshold_Manual", arrayForSpinner);

        <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < 0)
                    $("#MinimumThreshold_Manual").val(0);
                if ($("#MinimumThreshold_Manual").val() > 65535)
                    $("#MinimumThreshold_Manual").val(65535)
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
        }

         });

     });
</script>
<%} %>