<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> (<%: ZeroToTwentyMilliamp.GetLabel(Model.SensorID)  %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    var lowVal = <%:   (ZeroToTwentyMilliamp.GetLowValue(Model.SensorID).ToInt())%>;
    var highVal = <%:  (ZeroToTwentyMilliamp.GetHighValue(Model.SensorID).ToInt())%>;

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(lowVal, highVal, 1);
        createSpinnerModal("maxThreshNum", "Max Threshold", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < lowVal)
                    $("#MaximumThreshold_Manual").val(lowVal);

                if ($("#MaximumThreshold_Manual").val() > highVal)
                    $("#MaximumThreshold_Manual").val(highVal);
            }else
                $("#MaximumThreshold_Manual").val(<%: Max%>);
        });
    });
</script>
<%} %>