<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string Hyst = "";
    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
    string label = VoltageMeter500VAC.GetLabel(Model.SensorID);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<hr style="color:lightgray; opacity: 0.4;"/>

<script type="text/javascript">

    var lowVal = <%:  (VoltageMeter500VAC.GetLowValue(Model.SensorID).ToDouble())%>;
    var highVal = <%:  (VoltageMeter500VAC.GetHighValue(Model.SensorID).ToDouble())%>;

    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(lowVal, highVal, 10);
        createSpinnerModal("hystNum", "<%: label %>", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $('#Hysteresis_Manual').change(function ()
        {
            if (isANumber($("#Hysteresis_Manual").val())){
                if ($('#Hysteresis_Manual').val() < 0)
                    $('#Hysteresis_Manual').val(0);

                if($('#Hysteresis_Manual').val() > (((highVal- lowVal) * 0.25)))
                    $('#Hysteresis_Manual').val(((highVal- lowVal) * 0.25));
            }else
                $('#Hysteresis_Manual').val(<%:  (Hyst)%>);
        });
    });

</script>