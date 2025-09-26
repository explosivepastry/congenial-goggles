<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
             
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%> <%: AC_DC_500V.GetLabel(Model.SensorID) %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script type="text/javascript">

    var lowVal = <%:  (AC_DC_500V.GetLowValue(Model.SensorID).ToDouble())%>;
    var highVal = <%:  (AC_DC_500V.GetHighValue(Model.SensorID).ToDouble())%>;

    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(lowVal, highVal, 10);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $('#Hysteresis_Manual').change(function ()
        {
            if (isANumber($("#Hysteresis_Manual").val())){
                if ($('#Hysteresis_Manual').val() < 0)
                    $('#Hysteresis_Manual').val(0);

                if($('#Hysteresis_Manual').val() > ((highVal- lowVal)))
                    $('#Hysteresis_Manual').val(((highVal - lowVal)) );
            }else
                $('#Hysteresis_Manual').val(<%:  (Hyst)%>);
        });
    });

</script>