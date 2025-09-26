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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_102|PM2.5 Threshold","PM2.5 Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    var pmInterval_array1 = [10,50,100,250,500,750,1000];
     
    $(function () {

                <% if (Model.CanUpdate)
                   { %>

        createSpinnerModal("minThreshNum", "μg/m^3", "MinimumThreshold_Manual", pmInterval_array1);

        <%}%>

        $("#MinimumThreshold_Manual").addClass('editField editFieldMedium');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < 10)
                    $("#MinimumThreshold_Manual").val(10);
                if ($("#MinimumThreshold_Manual").val() > 1000)
                    $("#MinimumThreshold_Manual").val(1000);
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
        }
          });

     });
</script>
<%} %>