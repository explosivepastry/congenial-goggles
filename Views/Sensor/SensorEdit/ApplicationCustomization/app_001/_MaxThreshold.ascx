<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> <%: Html.Label(label) %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number"  <%=Model.CanUpdate ? "" : "disabled"  %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    var lowVal = <%: Analog.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: Analog.GetHighValue(Model.SensorID).ToDouble()%>;

    $(function () {

        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("maxThreshNum", "Max Threshold", "MaximumThreshold_Manual", [0, 1]);

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
<%}%>
    
    });
        
</script>
<%}%>