<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_082|Stabilization Delay","Stabilization Delay")%> (milliseconds)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control delaymil" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="delay" id="delay" value="<%=PressureNPSI.GetStabalizationDelay(Model) %>" />
        <a id="delayNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    

    $(document).ready(function () {

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(500, 5000, 100);
        createSpinnerModal("delayNum", "Milliseconds", "delay", arrayForSpinner);

        $("#delay").addClass('editField editFieldMedium');

        $("#delay").change(function () {
            if (isANumber($("#delay").val())) {
                if ($("#delay").val() < 0)
                    $("#delay").val(0);
                if ($("#delay").val() > 60000)
                    $("#delay").val(60000)
            }
            else {
                $('#delay').val(0);
            }
        });
        <%}%>
    });

</script>

