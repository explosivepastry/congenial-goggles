<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Time to Re-Arm (seconds)","Time to Re-Arm (seconds)")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" id="RearmTime_Custom" name="RearmTime_Custom" value="<%=Model.Calibration1 %>" />
        <a id="reArmNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>


<script type="text/javascript">

    $(function () {
        var rearm_array = [1, 2, 3, 5, 7, 10, 15, 20, 25, 30, 60, 120, 180, 300, 600];

        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("reArmNum", "Seconds", "RearmTime_Custom", rearm_array);

        <%}%>

        $("#RearmTime_Custom").change(function () {
            if (isANumber($("#RearmTime_Custom").val())) {
                if ($("#RearmTime_Custom").val() < 1)
                    $("#RearmTime_Custom").val(1);
                if ($("#RearmTime_Custom").val() > 600)
                    $("#RearmTime_Custom").val(600)
            }
            else {
                $("#RearmTime_Custom").val(<%: Model.Calibration1%>);
            }
        });
    });

    function AddReArm() {
        if ($("#RearmTime_Custom").val() < 1) {
            $("#RearmTime_Custom").val(1);
        } else if ($("#RearmTime_Custom").val() > 595) {
            $("#RearmTime_Custom").val(600)
        } else { $("#RearmTime_Custom").val(Number($("#RearmTime_Custom").val()) + 5); }
    }

    function SubReArm() {
        if ($("#RearmTime_Custom").val() < 5) {
            $("#RearmTime_Custom").val(1);
        } else if ($("#RearmTime_Custom").val() > 600) {
            $("#RearmTime_Custom").val(600)
        } else { $("#RearmTime_Custom").val(Number($("#RearmTime_Custom").val()) - 5); }
    }
</script>