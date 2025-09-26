<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<input id="RearmMax" type="hidden" value="<%=Model.ReportInterval * 60%>" />
<%---Re-Arm Time---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Re-Arm Time","Re-Arm Time")%> (<%: Html.Label(Html.TranslateTag("Seconds")) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="ReArmTime" id="ReArmTime" value="<%: Model.Calibration3 %>" />
        <a id="timeNum" style="cursor: pointer;"></a>
    </div>
</div>

<script type="text/javascript">

        var rearmmax = $('#RearmMax').val();
    $(function () {

<%--        <% if (Model.CanUpdate)
    { %>
        ReArmMobiscroll(rearmmax);


        <%}%>--%>

        $("#ReportInterval").change(function () {
            rearmmax = $('#ReportInterval').val() * 60;

            SetReArmMax(rearmmax);
            ReArmMobiscroll(rearmmax);
        });
    });


    function SetReArmMax(rearmmax) {
        
        if ($("#ReArmTime").val() > rearmmax) {
            $("#ReArmTime").val(rearmmax)
        }

        if ($("#ReArmTime").val() < 1) {
            $("#ReArmTime").val(1);
        }
    }

    function ReArmMobiscroll(max) {

        //const arrayForSpinner = arrayBuilder(1, max, 1);
        //createSpinnerModal("ReArmTime", "Seconds", "ReArmTime", arrayForSpinner);

    }

    const arrayForSpinner = arrayBuilder(1, parseInt(rearmmax), 1);
    createSpinnerModal("ReArmTime", "Seconds", "ReArmTime", arrayForSpinner);

</script>
