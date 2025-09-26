<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    long CalVal1 = Model.Calibration1 / 1000;
    string label = "";
    MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_051|Assessment Interval","Assessment Interval")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="Calibration1_Manual" id="Calibration1_Manual" value="<%=CalVal1 %>" />
        <a id="calValNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => CalVal1)%>
    </div>
</div>


<script type="text/javascript">

    var assessment_array = [1, 2, 3, 4, 5, 10, 30, 60, 120, 240, 360, 440, 600];

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("calValNum", "Seconds", "Calibration1_Manual", assessment_array);

        <%}%>
        $("#Calibration1_Manual").addClass('editField editFieldSmall');

        $("#Calibration1_Manual").change(function () {
            //Check if less than min
            if ($("#Calibration1_Manual").val() < 1)
                $("#Calibration1_Manual").val(1);

            //Check if greater than max
            if ($("#Calibration1_Manual").val() > 600)
                $("#Calibration1_Manual").val(600);
        });
    });
</script>
