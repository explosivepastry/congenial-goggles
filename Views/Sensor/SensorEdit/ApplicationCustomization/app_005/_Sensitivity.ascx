<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_005|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Sensitivity" id="Sensitivity" value="<%=Model.EventDetectionPeriod %>" />
        <a id="sensiNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.EventDetectionPeriod)%><span style="font-size: 11px;"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_005|Lower numbers are more sensitive","Lower numbers are more sensitive")%></span>
    </div>
</div>

<script type="text/javascript">

    var sensitivity_array = [50, 100, 300, 500, 1000];

    function getSensitivityIndex() {
        var retval = 0;
        $.each(sensitivity_array, function (index, value) {
            if (value <= $("#Sensitivity").val())
                retval = index;
        });
        return retval;
    }

    $(function () {
                <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("sensiNum", "Sensitivity", "Sensitivity", sensitivity_array);

        $("#Sensitivity").change(function () {
            if (isANumber($("#Sensitivity").val())) {
                if ($("#Sensitivity").val() < 50)
                    $("#Sensitivity").val(50);
                if ($("#Sensitivity").val() > 1000)
                    $("#Sensitivity").val(1000)
            }
            else {
                $('#Sensitivity').val(<%: Model.EventDetectionPeriod%>);

            }
        });
        <%}%>
    });
</script>
