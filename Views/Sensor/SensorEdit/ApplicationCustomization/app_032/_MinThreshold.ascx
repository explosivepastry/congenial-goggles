<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%>  <%: AC_DC_500V.GetLabel(Model.SensorID)  %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
       
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    var lowVal = <%:  (AC_DC_500V.GetLowValue(Model.SensorID).ToDouble())%>;
    var highVal = <%:  (AC_DC_500V.GetHighValue(Model.SensorID).ToDouble())%>;

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(lowVal, highVal, 10);
        createSpinnerModal("minThreshNum", "Minimum Threshold", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < lowVal)
                    $('#MinimumThreshold_Manual').val(lowVal);

                if ($('#MinimumThreshold_Manual').val() > highVal )
                    $('#MinimumThreshold_Manual').val(highVal);
            }
            else
                $('#MinimumThreshold_Manual').val(<%:  (Min)%>);
        });

    });
</script>