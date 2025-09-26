<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        long Cal1 = Model.Calibration1;
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heading Offset","Heading Offset")%> Degrees
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration1" id="Calibration1" value="<%=Cal1 %>" />
        <a id="Cal1Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<div class="form-group">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 359, 10);
        createSpinnerModal("Cal1Num", "Offset Degrees", "Calibration1", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Calibration1").val() < 0)
                    $("#Calibration1").val(0);
                if ($("#Calibration1").val() > 359)
                    $("#Calibration1").val(359)

            }
            else {
                $('#Calibration1').val(<%: Cal1%>);

            }


        });
    });
</script>
<%} %>