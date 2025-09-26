<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    sbyte min1 = QuadTemperature.GetMinFirstByte(Model);
    sbyte max1 = QuadTemperature.GetMaxFirstByte(Model);
    sbyte min2 = QuadTemperature.GetMinSecondByte(Model);
    sbyte max2 = QuadTemperature.GetMaxSecondByte(Model);
    sbyte min3 = QuadTemperature.GetMinThirdByte(Model);
    sbyte max3 = QuadTemperature.GetMaxThirdByte(Model);
    sbyte min4 = QuadTemperature.GetMinFourthByte(Model);
    sbyte max4 = QuadTemperature.GetMaxFourthByte(Model);
    bool isFarh = QuadTemperature.IsFahrenheit(Model.SensorID);

    string datumName0 = Model.GetDatumName(0);
    string datumName1 = Model.GetDatumName(1);
    string datumName2 = Model.GetDatumName(2);
    string datumName3 = Model.GetDatumName(3);
%>

<!-- ADD a min/max for all for probes and set 3 of each to hidden -->

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFirstByte_Manual" id="MinFirstByte_Manual minTemp" value="<%=isFarh?  min1.ToDouble().ToFahrenheit():min1 %>" />
        <a id="firstMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFirstByte_Manual" id="MaxFirstByte_Manual maxTemp" value="<%=isFarh?  max1.ToDouble().ToFahrenheit():max1 %>" />
        <a id="firstMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<!-- HIDDEN FIELDS -->
<div style="visibility: hidden;">
    <%----- MIN Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinSecondByte_Manual" id="MinSecondByte_Manual" value="<%=isFarh?  min2.ToDouble().ToFahrenheit():min2 %>" />
    <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>

    <%----- MAX Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxSecondByte_Manual" id="MaxSecondByte_Manual" value="<%=isFarh?  max2.ToDouble().ToFahrenheit():max2 %>" />
    <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>

    <%----- MIN Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinThirdByte_Manual" id="MinThirdByte_Manual" value="<%=isFarh?  min3.ToDouble().ToFahrenheit():min3 %>" />
    <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>

    <%----- MAX Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxThirdByte_Manual" id="MaxThirdByte_Manual" value="<%=isFarh?  max3.ToDouble().ToFahrenheit():max3 %>" />
    <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>

    <%----- MIN Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFourthByte_Manual" id="MinFourthByte_Manual" value="<%=isFarh?  min4.ToDouble().ToFahrenheit():min4 %>" />
    <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>

    <%----- MAX Threshold -----%>
    <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFourthByte_Manual" id="MaxFourthByte_Manual" value="<%=isFarh?  max4.ToDouble().ToFahrenheit():max4 %>" />
    <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
</div>


<%--QuadTemperature.GetProbe1Label(Model.SensorID)--%>
<div class="row sensorEditForm" onload="loadValues()">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe One Name","Probe One Name")%>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 lgBox">
        <input type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName0" name="datumName0" value="<%= Model.GetDatumName(0)%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => datumName0)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Two Name","Probe Two Name")%>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 lgBox">
        <input type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName1" name="datumName1" value="<%= Model.GetDatumName(1)%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => datumName1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Three Name","Probe Three Name")%>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 lgBox">
        <input type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName2" name="datumName2" value="<%= Model.GetDatumName(2)%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => datumName2)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Four Name","Probe Four Name")%>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 lgBox">
        <input type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName3" name="datumName3" value="<%= Model.GetDatumName(3)%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => datumName3)%>
    </div>
</div>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<%
    long DefaultMin = -40;
    long DefaultMax = 125;

    if (isFarh)
    {
        DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
        DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
    }

%>


<script>
    // Onload set min/max for all probes to the same value
    $(document).ready(function () {
        <%= Model.GetDatumName(1)%> = <%= Model.GetDatumName(0)%>;
        <%= Model.GetDatumName(2)%> = <%= Model.GetDatumName(0)%>;
        <%= Model.GetDatumName(3)%> = <%= Model.GetDatumName(0)%>;
    })

    // When user onkeyup, set the same value for all the probes for the min/max
    $("#minTemp").keyup(function () {
        <%= min2 %> = <%= min1 %>;
        <%= min3 %> = <%= min1 %>;
        <%= min4 %> = <%= min1 %>;
    });

    $("#maxTemp").keyup(function () {
        <%= max2 %> = <%= max1 %>;
        <%= max3 %> = <%= max1 %>;
        <%= max4 %> = <%= max1 %>;
    });


</script>
