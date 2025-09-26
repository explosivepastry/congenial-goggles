<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
        double hystMax = 250000;
        double max = Resistance.MaxThreshForUI(Model).ToDouble();
        if (max < hystMax)
            hystMax = max;

        hystMax = (hystMax * 0.25);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />

        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    //MobiScroll
    $(function () {
        var hMax = <%=hystMax%>;

          <% if(Model.CanUpdate) { %>

        let arrayForSpinner1 = arrayBuilder(0, hMax, 1);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner1);

        <%}%>

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > hMax)
                    $("#Hysteresis_Manual").val(hMax)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
                }
        });
    });
</script>
<%} %>