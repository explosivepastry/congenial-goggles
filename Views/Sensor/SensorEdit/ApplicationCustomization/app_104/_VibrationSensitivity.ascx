<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 

    string VibrationSensitivity = "";
    VibrationSensitivity = Vibration800.GetVibrationSensitivityThreshold(Model).ToString();
        
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Vibration Sensitivity Threshold","Vibration Sensitivity Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationSensitivity_Manual" id="VibrationSensitivity_Manual" value="<%=VibrationSensitivity %>" />
        <a id="VibNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>
<script type="text/javascript">

    $(function () {

               <% if (Model.CanUpdate)
                  { %>
        const arrayForSpinnerWhole = arrayBuilder(0, 32, 1);
        createSpinnerModal("VibNum", "Sensitivity Threshold", "VibrationSensitivity_Manual", arrayForSpinnerWhole, null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);
        <%}%>

        $("#VibrationSensitivity_Manual").addClass('editField editFieldSmall');

        $('#VibrationSensitivity_Manual').change(function () {
            if (isANumber($("#VibrationSensitivity_Manual").val())) {
                if ($("#VibrationSensitivity_Manual").val() < 0)
                    $("#VibrationSensitivity_Manual").val(0);
                if ($("#VibrationSensitivity_Manual").val() > 32)
                    $("#VibrationSensitivity_Manual").val(32);
            }
            else {
        //        $('#VibrationSensitivity_Manual').val(<%: VibrationSensitivity%>);
        }
        });
    });


</script>




