<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        double Cal2 = Model.Calibration2 / 10d;
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_055|CT Size","CT Size")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="Calibration2_Manual" id="Calibration2_Manual" value="<%=Cal2 %>" />
        <%: label %>
        <a id="Cal2Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => Cal2)%>
    </div>
</div>

<script type="text/javascript">

    var ctSize_array = [200, 250, 300, 400, 500, 600, 800, 1000, 1200, 1500, 1600, 2000, 2400, 2500, 3000, 3500, 4000, 5000, 6000];

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("Cal2Num", "CT Size", "Calibration2_Manual", ctSize_array);

        <%}%>

        $("#Calibration2_Manual").change(function () {
            if (isANumber($("#Calibration2_Manual").val())) {
                if ($("#Calibration2_Manual").val() < 200)
                    $("#Calibration2_Manual").val(200);
                if ($("#Calibration2_Manual").val() > 6000)
                    $("#Calibration2_Manual").val(6000)
            }
            else {
                $('#Calibration2_Manual').val(<%: Cal2%>);
            }
        });
    });
</script>
<%} %>