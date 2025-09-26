<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Min = "";
    string Max = "";
    string label = ZeroToTwoHundredVolts.GetLabel(Model.SensorID);
    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> (<%: label%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    var lowVal = <%: ZeroToTwoHundredVolts.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: ZeroToTwoHundredVolts.GetHighValue(Model.SensorID).ToDouble()%>;

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(lowVal, 200, 5);
        createSpinnerModal("maxThreshNum", '<%:label%>', "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $('#MaximumThreshold_Manual').change(function () {
        
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($('#MaximumThreshold_Manual').val() < lowVal)
                    $('#MaximumThreshold_Manual').val(lowVal);

                if ($('#MaximumThreshold_Manual').val() > highVal )
                    $('#MaximumThreshold_Manual').val(highVal);
            }
            else
                $('#MaximumThreshold_Manual').val(<%: Max%>);
        });
    });
</script>