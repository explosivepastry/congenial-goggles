<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%int rearmTime = AltaPIRBase.GetRearmTime(Model); %>


<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_RearmTime|Time to Re-Arm (seconds)","Time to Re-Arm (seconds)")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" <%=Model.CanUpdate ? "" : "disabled" %> id="RearmTime" name="RearmTime" value="<%=rearmTime %>" />
        <a id="reArmNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => rearmTime)%>
    </div>
</div>

<script type="text/javascript">
    $(function () {
        var rearm_array = [1, 2, 3, 5, 7, 10, 15, 20, 25, 30, 60, 120, 180, 300, 600];

        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("reArmNum", "Seconds", "RearmTime", rearm_array);
        <%}%>

        $("#RearmTime").change(function () {
            if (isANumber($("#RearmTime").val())) {
                if ($("#RearmTime").val() < 1)
                    $("#RearmTime").val(1);
                if ($("#RearmTime").val() > 600)
                    $("#RearmTime").val(600)
            }
            else {
                $("#RearmTime").val(1);
            }
        });
    });

    function AddReArm() {
        if ($("#RearmTime").val() < 1) {
            $("#RearmTime").val(1);
        } else if ($("#RearmTime").val() > 595) {
            $("#RearmTime").val(600)
        } else { $("#RearmTime").val(Number($("#RearmTime").val()) + 5); }
    }

    function SubReArm() {
        if ($("#RearmTime").val() < 5) {
            $("#RearmTime").val(1);
        } else if ($("#RearmTime").val() > 600) {
            $("#RearmTime").val(600)
        } else { $("#RearmTime").val(Number($("#RearmTime").val()) - 5); }
    }

</script>
