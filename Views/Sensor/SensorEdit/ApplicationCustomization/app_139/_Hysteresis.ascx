<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	if (!Monnit.VersionHelper.IsVersion_1_0(Model))
	{

		string Hyst = "";
        string MaxTresh = "";
        string MinTresh = "";

		Hyst = LightSensor_PPFD.HystForUI(Model);
        MinTresh = LightSensor_PPFD.MinThreshForUI(Model);
        MaxTresh = LightSensor_PPFD.MaxThreshForUI(Model);

		string maxHystRange = ((MaxTresh.ToDouble() - MinTresh.ToDouble()) / 2.5).ToString();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-Light-Threshold-Title","Light Threshold Buffer")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">
    $("#Hysteresis_Manual").addClass('form-control');
    $("#DeltaLightReport_Manual").addClass('form-control');

    var maxHyst = ((Number($("#MaximumThreshold_Manual").val()) - Number($("#MinimumThreshold_Manual").val())) / 2.5)

    $(function () {
          <% if (Model.CanUpdate)
             { %>
        const arrayForSpinner = arrayBuilder(0, maxHyst, 1);
        createSpinnerModal("hystNum", "Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {

            maxHyst = ((Number($("#MaximumThreshold_Manual").val()) - Number($("#MinimumThreshold_Manual").val())) / 2.5)

            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > maxHyst)
                    $("#Hysteresis_Manual").val(maxHyst)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });


        <% if (Model.CanUpdate)
                   { %>
        $(function () {
            $('#Display').mobiscroll().select({
                theme: 'ios',
                display: popLocation,
                minWidth: 200
            });
        <%}%>
        });


    });
</script>
<%} %>