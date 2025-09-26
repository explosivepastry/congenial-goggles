<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string MinT = "";
        string MaxT = "";
        string DistanceOrDepthLabel = "Distance";
        string unit = UltrasonicRangerIndustrial.GetUnits(Model.SensorID).ToString();

        double step = 1;
        double DefaultMin = UltrasonicRangerIndustrial.DefaultMinThreshForUI(Model);
        double DefaultMax = UltrasonicRangerIndustrial.DefaultMaxThreshForUI(Model);
        double UIDefaultMin = DefaultMin;
        double UIDefaultMax = DefaultMax;
        double tankDepth = UltrasonicRangerIndustrial.TankDepthForUI(Model);

        bool showDepth = UltrasonicRangerIndustrial.GetShowDepth(Model.SensorID);

        MinT = UltrasonicRangerIndustrial.MinThreshForUI(Model);
        MaxT = UltrasonicRangerIndustrial.MaxThreshForUI(Model);
        string UIMinT = MinT;
        string UIMaxT = MaxT;

        string label = unit;

        switch (unit)
        {
            case "Meter":
                label = "Meters";
                step = .01;
                break;
            case "Inch":
                label = "Inches";
                step = .1;
                break;
            case "Feet":
                label = "Feet";
                step = .01;
                break;
            case "Yard":
                label = "Yards";
                step = .01;
                break;
            case "Centimeter":
            default:
                label = "Centimeters";
                step = 10;
                break;
        }


        if (showDepth)
        {

            DistanceOrDepthLabel = "Tank Depth";
            UIDefaultMax = (tankDepth - DefaultMin.ToDouble()).ToLong();
            UIDefaultMin = (tankDepth - DefaultMax.ToDouble()).ToLong();
            UIMaxT = (tankDepth - MinT.ToDouble()).ToString();
            UIMinT = (tankDepth - MaxT.ToDouble()).ToString();
        } 
  
    
%>
<p class="useAwareState"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Use Aware State ","Use Aware State")%> (<%: Html.TranslateTag(DistanceOrDepthLabel,DistanceOrDepthLabel)%>)</p>
<%----MIN Threshold----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%:label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=UIMinT %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%----Max Threshold----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (<%:label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=UIMaxT %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    var MinThresMinVal = Number(<%=UIDefaultMin%>);
    var MinThresMaxVal = Number(<%=UIDefaultMax%>);
    var stepUnit = Number(<%=step%>);

    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        const arrayForSpinner = arrayBuilderForDecimals(MinThresMinVal, MinThresMaxVal, stepUnit);
        createSpinnerModal("minThreshNum", "<%: label%>", "MinimumThreshold_Manual", arrayForSpinner);
        createSpinnerModal("maxThreshNum", "<%: label%>", "MaximumThreshold_Manual", arrayForSpinner);

         <%}%>

         $("#MinimumThreshold_Manual").change(function () {
             if (isANumber($("#MinimumThreshold_Manual").val())){
                 if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                     $("#MinimumThreshold_Manual").val(MinThresMinVal);
                 if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                     $("#MinimumThreshold_Manual").val(MinThresMaxVal);

                 if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                     $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
             }else{
                 $("#MinimumThreshold_Manual").val(<%: UIMinT%>);
             }
         });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < MinThresMinVal)
                    $("#MaximumThreshold_Manual").val(MinThresMinVal);
                if ($("#MaximumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MaximumThreshold_Manual").val(MinThresMaxVal);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
            } else {
                $("#MaximumThreshold_Manual").val(<%: UIMaxT%>);
                     }
                 });
    });

</script>
<%} %>