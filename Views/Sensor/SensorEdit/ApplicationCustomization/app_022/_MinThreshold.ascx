<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
       
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%>  (<%: ZeroToTwentyMilliamp.GetLabel(Model.SensorID)  %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var lowVal = <%:   (ZeroToTwentyMilliamp.GetLowValue(Model.SensorID).ToInt())%>;
        var highVal = <%:  (ZeroToTwentyMilliamp.GetHighValue(Model.SensorID).ToInt())%>;
     

        <% if (Model.CanUpdate)
           { %>


        let arrayForSpinner = arrayBuilder(lowVal, highVal, 1);
        createSpinnerModal("minThreshNum", "Min Threshold", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($("#MinimumThreshold_Manual").val() < lowVal)
                    $("#MinimumThreshold_Manual").val(lowVal);

                if ($("#MinimumThreshold_Manual").val() > highVal)
                    $("#MinimumThreshold_Manual").val(highVal);
            }else
                $("#MinimumThreshold_Manual").val(<%: Min%>);
        });
    });
</script>
<%} %>