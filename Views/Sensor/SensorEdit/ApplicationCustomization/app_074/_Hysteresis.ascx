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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%= label %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">
    $('#Hysteresis_Manual').addClass("editField editFieldMedium");

    var lowVal = <%: ZeroToTenVolts.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: ZeroToTenVolts.GetHighValue(Model.SensorID).ToDouble()%>;

    //MobiScroll
    $(function () {
          <% if(Model.CanUpdate) { %>


        let arrayForSpinner1 = arrayBuilder(lowVal, highVal, 1);
        createSpinnerModal("hystNum", "<%=label%>", "Hysteresis_Manual", arrayForSpinner1);

      <%}%>

        $('#Hysteresis_Manual').change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($('#Hysteresis_Manual').val() < 0)
                    $('#Hysteresis_Manual').val(0);

                if ($('#Hysteresis_Manual').val() > ((highVal - lowVal)))
                    $('#Hysteresis_Manual').val(((highVal - lowVal)));
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
        }

        });

  
    });
</script>
<%} %>