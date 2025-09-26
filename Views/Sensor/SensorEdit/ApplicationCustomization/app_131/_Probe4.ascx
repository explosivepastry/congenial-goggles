<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst4 = FilteredQuadTemperature.GetHystFourthByte(Model);
    sbyte min4 = FilteredQuadTemperature.GetMinFourthByte(Model);
    sbyte max4 = FilteredQuadTemperature.GetMaxFourthByte(Model);
    bool isFarh = FilteredQuadTemperature.IsFahrenheit(Model.SensorID);

    string datumName3 = Model.GetDatumName(3);
%>

<h5>Probe Four</h5>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFourthByte_Manual" id="MinFourthByte_Manual" value="<%=isFarh?  min4.ToDouble().ToFahrenheit():min4 %>" />
        <a id="fourMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFourthByte_Manual" id="MaxFourthByte_Manual" value="<%=isFarh?  max4.ToDouble().ToFahrenheit():max4 %>" />
        <a id="fourtMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="HystFourthByte_Manual" id="HystFourthByte_Manual" value="<%=Math.Round(hyst4.ToDouble()) %>" />
        <a id="fourHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--QuadTemperature.GetProbe1Label(Model.SensorID)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Four Name","Probe Four Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName3" name="datumName3" value="<%= Model.GetDatumName(3)%>" />
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
    $("#HystFourthByte_Manual").addClass('editField editFieldSmall');
    $("#MinFourthByte_Manual").addClass('editField editFieldSmall');
    $("#MaxFourthByte_Manual").addClass('editField editFieldSmall');

    $(function () {

        <% if (Model.CanUpdate) { %>  

        const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("fourMinThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MinFourthByte_Manual", arrayForSpinner);
        createSpinnerModal("fourMaxThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MaxFourthByte_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 5, 1);
        createSpinnerModal("fourHystNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "HystFourthByte_Manual", arrayForSpinner1);

        <%}%>

        $("#MinFourthByte_Manual").change(function () {
            if (isANumber($("#MinFourthByte_Manual").val())) {
                if ($("#MinFourthByte_Manual").val() < <%:(DefaultMin)%>)
                    $("#MinFourthByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinFourthByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinFourthByte_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MinFourthByte_Manual").val()) > parseFloat($("#MaxFourthByte_Manual").val()))
                    $("#MinFourthByte_Manual").val(parseFloat($("#MaxFourthByte_Manual").val()));
            }
            else {
                $('#MinFourthByte_Manual').val(<%: isFarh?min4.ToDouble().ToFahrenheit():min4  %>);
            }
        });
    });

    $(function () {
        $("#MaxFourthByte_Manual").change(function () {
            if (isANumber($("#MaxFourthByte_Manual").val())) {
                if ($("#MaxFourthByte_Manual").val() < <%:(DefaultMin)%>)
                    $("#MaxFourthByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxFourthByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxFourthByte_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MaxFourthByte_Manual").val()) < parseFloat($("#MinFourthByte_Manual").val()))
                    $("#MaxFourthByte_Manual").val($("#MinFourthByte_Manual").val());
            }
            else {
                $('#MaxFourthByte_Manual').val(<%: isFarh?max4.ToDouble().ToFahrenheit():max4  %>);
            }
        });
    });

    $(function () {
        $("#HystFourthByte_Manual").change(function () {
            if (isANumber($("#HystFourthByte_Manual").val())) {
                if ($("#HystFourthByte_Manual").val() < 0)
                    $("#HystFourthByte_Manual").val(0);
                if ($("#HystFourthByte_Manual").val() > 5)
                    $("#HystFourthByte_Manual").val(5)
            }
            else {
                $('#HystFourthByte_Manual').val(<%: hyst4 %>);
            }
        });
    });
</script>
