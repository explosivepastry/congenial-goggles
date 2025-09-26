<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Hyst = "";
        string label = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_102|PM1.0 Threshold","PM1.0 Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">
    var pmInterval_array = [10, 50, 100, 250, 500, 750, 1000];

    $(function () {
                <% if (Model.CanUpdate)
    { %>
        createSpinnerModal("hystNum", "μg/m^3", "Hysteresis_Manual", pmInterval_array);
        <%}%>

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $('#Hysteresis_Manual').change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 10)
                    $("#Hysteresis_Manual").val(10);
                if ($("#Hysteresis_Manual").val() > 1000)
                    $("#Hysteresis_Manual").val(1000);
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>
<%} %>