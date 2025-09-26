<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Averaging Interval","Averaging Interval")%> &nbsp Seconds
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="avgInterval" id="avgInterval" value="<%=(Current.GetCalVal3Lower(Model) / 1000d) %>" />
        <a id="avgIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_089|Current Shift Aware","Current Shift Aware")%> Amps
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="currentShift" id="currentShift" value="<%=(Model.Calibration4 / 100d).ToString() %>" />
          <a id="currentShiftA" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<script type="text/javascript">
    var assessment_array = [1, 2, 5, 10, 30, 60, 120, 180];

    $(function () {

        <% if (Model.CanUpdate)
    { %>
        createSpinnerModal("avgIntNum", "Seconds", "avgInterval", assessment_array,2);
    });
        <%}%>

    $("#avgIntNum").addClass('editField editFieldSmall');

    $("#avgInterval").change(function () {
        if (isANumber($("#avgInterval").val())) {
            if ($("#avgInterval").val() < 0)
                $("#avgInterval").val(0);
            if ($("#avgInterval").val() > 180)
                $("#avgInterval").val(180)
            setAproxTime();
        }
        else 
            $("#avgInterval").val(<%: Current.GetCalVal3Lower(Model) / 100d%>);
    });

    $('#currentShift').addClass("editField editFieldMedium");

    let arrayForSpinnerDecimal = [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"];
    let arrayForSpinner = arrayBuilder(-1000, 1000, 1);
    createSpinnerModal("currentShiftA", "Amps", "currentShift", arrayForSpinner, 1001, arrayForSpinnerDecimal);

</script>
