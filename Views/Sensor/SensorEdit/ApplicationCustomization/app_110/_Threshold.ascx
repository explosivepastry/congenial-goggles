<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string Hyst = "";
    string Min = "";
    string Max = "";
    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);

    bool isFarh = DwellTime.IsFahrenheit(Model.SensorID);
    double MinTemp = DwellTime.GetCalVal1ByteZeroAndOne(Model);
    double MaxTemp = DwellTime.GetCalVal1ByteTwoAndThree(Model);
    int minBodySize = DwellTime.GetCalVal2ByteZero(Model);
    double calval3 = DwellTime.GetCalVal3ByteZeroAndOne(Model);
    double calVal4 = DwellTime.GetDifferentialTemperature(Model);

    double defaultMinTemp = 10;//C
    double defaultMaxTemp = 75;//C
    double calval4MinTemp = 0.0;//C
    double calval4MaxTemp = 40.0;//C

    if (isFarh)
    {

        defaultMinTemp = defaultMinTemp.ToFahrenheit();
        defaultMaxTemp = defaultMaxTemp.ToFahrenheit();
        calval4MinTemp = (calval4MinTemp * 1.8);
        calval4MaxTemp = (calval4MaxTemp * 1.8);
        calVal4 = (calVal4 * 1.8);
    }
    
    
        
%>
<h3>Advance Settings</h3>

<%--Min Threshold--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Minimum Dwell Time Threshold","Minimum Dwell Time Threshold")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%--Detection Temp--%>
<h5>Detection Temperature</h5>
<%--min--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Min Detection Temperature","Min Detection Temperature")%> <%: Html.Label(DwellTime.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinDetectionTemp_Manual" id="MinDetectionTemp_Manual" value="<%=MinTemp %>" />
        <a id="minDetectNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>
<%--max--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Max Detection Temperature","Max Detection Temperature")%> <%: Html.Label(DwellTime.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxDetectionTemp_Manual" id="MaxDetectionTemp_Manual" value="<%=MaxTemp %>" />
        <a id="maxDetectNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>


<%--Min Warm Body Size--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Min Warm Body Size","Min Warm Body Size")%> (Pixels)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinBodyTemp_Manual" id="MinBodyTemp_Manual" value="<%=minBodySize %>" />
        <a id="cal2" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<%--Reaverage Period--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Reaverage Period","Reaverage Period")%> (Minutes)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control"  <%=Model.CanUpdate ? "" : "disabled"  %> name="CalVal3_Manual" id="CalVal3_Manual" value="<%=calval3 %>" />
        <a id="cal3" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>

<%--Differential Temperature--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Differential Temperature","Differential Temperature")%> <%: Html.Label(DwellTime.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled"  %> name="CalVal4_Manual" id="CalVal4_Manual" value="<%=calVal4 %>" />
        <a id="cal4" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>


<script>
    $("#MaximumThreshold_Manual").addClass('form-control');
    $("#MinDetectionTemp_Manual").addClass('form-control');
    $("#MaxDetectionTemp_Manual").addClass('form-control');
    $("#MinBodyTemp_Manual").addClass('form-control');
    $("#CalVal2MaxTemp_Manual").addClass('form-control');
    $("#CalVal4_Manual").addClass('form-control');
    $("#CalVal3_Manual").addClass('form-control');

    $(function () {

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < 0)
                    $("#MaximumThreshold_Manual").val(0);
                if ($("#MaximumThreshold_Manual").val() > 65535)
                    $("#MaximumThreshold_Manual").val(65535);
            }else{
                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

              <% if (Model.CanUpdate)
                 { %>

        const label = "Degrees"
        const arrayForSpinner = arrayBuilder(<%: defaultMinTemp%>, <%: defaultMaxTemp%>, 10);
        createSpinnerModal("minDetectNum", label, "MinDetectionTemp_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 65535, 100);
        createSpinnerModal("maxThreshNum", "Seconds", "MaximumThreshold_Manual", arrayForSpinner1);
        createSpinnerModal("maxDetectNum", label, "MaxDetectionTemp_Manual", arrayForSpinner);

        const arrayForSpinner2 = arrayBuilder(1, 64, 1);
        createSpinnerModal("cal2", "Pixels", "MinBodyTemp_Manual", arrayForSpinner2);
        createSpinnerModal("cal3", "Minutes", "CalVal3_Manual", arrayForSpinner1);

        const arrayForSpinner3 = arrayBuilder(<%: calval4MinTemp%>, <%: calval4MaxTemp%>, 1);
        createSpinnerModal("cal4", label, "CalVal4_Manual", arrayForSpinner3);

        <%}%>

        $("#MinDetectionTemp_Manual").change(function () {
            if (isANumber($("#MinDetectionTemp_Manual").val()))
            {  
                if ($("#MinDetectionTemp_Manual").val() < <%:(defaultMinTemp)%>)
                    $("#MinDetectionTemp_Manual").val(<%:(defaultMinTemp)%>);   
                if ($("#MinDetectionTemp_Manual").val() > <%:(defaultMaxTemp)%>)
                    $("#MinDetectionTemp_Manual").val(<%:(defaultMaxTemp)%>);   
                
                if (parseFloat($("#MinDetectionTemp_Manual").val()) > parseFloat($("#MaxDetectionTemp_Manual").val()))
                    $("#MinDetectionTemp_Manual").val(parseFloat($("#MaxDetectionTemp_Manual").val()));
                //$("#MaxDetectionTemp_Manual").change();
            }
            else
            {
                $('#MinDetectionTemp_Manual').val(<%: MinTemp%>);
            }
        });

        $("#MaxDetectionTemp_Manual").change(function () {
            if (isANumber($("#MaxDetectionTemp_Manual").val())){
                if ($("#MaxDetectionTemp_Manual").val() < <%:(defaultMinTemp)%>)
                $("#MaxDetectionTemp_Manual").val(<%:(defaultMinTemp)%>);
                if ($("#MaxDetectionTemp_Manual").val() > <%:(defaultMaxTemp)%>)
            $("#MaxDetectionTemp_Manual").val(<%:(defaultMaxTemp)%>);     
                
                if (parseFloat($("#MaxDetectionTemp_Manual").val()) < parseFloat($("#MinDetectionTemp_Manual").val()))
                    $("#MaxDetectionTemp_Manual").val(parseFloat($("#MinDetectionTemp_Manual").val()));
                //$("#MinDetectionTemp_Manual").change();
        }
        else
        {
            $('#MaxDetectionTemp_Manual').val(<%: MaxTemp%>);
            }
        });

        $("#MinBodyTemp_Manual").change(function () {
            if (isANumber($("#MinBodyTemp_Manual").val()))
            {
                if ($("#MinBodyTemp_Manual").val() < 1)
                    $("#MinBodyTemp_Manual").val(1);
                if ($("#MinBodyTemp_Manual").val() > 64)
                    $("#MinBodyTemp_Manual").val(64);
            }
            else
            {
                $('#MinBodyTemp_Manual').val(<%: minBodySize%>);
            }
        });

        $("#CalVal3_Manual").change(function () {
            if (isANumber($("#CalVal3_Manual").val()))
            {
                if ($("#CalVal3_Manual").val() < 0)
                    $("#CalVal3_Manual").val(0);
                if ($("#CalVal3_Manual").val() > 65535)
                    $("#CalVal3_Manual").val(65535);
            }
            else
            {
                $('#CalVal3_Manual').val(<%: calval3%>);
            }
        });

        $("#CalVal4_Manual").change(function () {
            if (isANumber($("#CalVal4_Manual").val())){
                var number = $("#CalVal4_Manual").val();
                $("#CalVal4_Manual").val((Math.round(number * 4) / 4).toFixed(2))
                if ($("#CalVal4_Manual").val() < <%: calval4MinTemp%>)
                    $("#CalVal4_Manual").val(<%: calval4MinTemp%>);
                if ($("#CalVal4_Manual").val() ><%: calval4MaxTemp%>)
                    $("#CalVal4_Manual").val(<%: calval4MaxTemp%>);
            }
            else
            {
                $('#CalVal4_Manual').val(<%: calVal4%>);
            }
        });

    });
</script>
