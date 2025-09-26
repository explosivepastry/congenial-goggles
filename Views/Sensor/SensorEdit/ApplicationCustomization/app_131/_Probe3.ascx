<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst3 = FilteredQuadTemperature.GetHystThirdByte(Model);
    sbyte min3 = FilteredQuadTemperature.GetMinThirdByte(Model);
    sbyte max3 = FilteredQuadTemperature.GetMaxThirdByte(Model);
    bool isFarh = FilteredQuadTemperature.IsFahrenheit(Model.SensorID);

    string datumName2 = Model.GetDatumName(2);
%>

<h5>Probe Three</h5>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinThirdByte_Manual" id="MinThirdByte_Manual" value="<%=isFarh?  min3.ToDouble().ToFahrenheit():min3 %>" />
        <a id="thirdMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxThirdByte_Manual" id="MaxThirdByte_Manual" value="<%=isFarh?  max3.ToDouble().ToFahrenheit():max3 %>" />
        <a id="thirdMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="HystThirdByte_Manual" id="HystThirdByte_Manual" value="<%=Math.Round(hyst3.ToDouble()) %>" />
        <a id="thirdHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--QuadTemperature.GetProbe1Label(Model.SensorID)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Three Name","Probe Three Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName2" name="datumName2" value="<%= Model.GetDatumName(2)%>" />
        <%: Html.ValidationMessageFor(model => datumName2)%>
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
    $("#HystThirdByte_Manual").addClass('editField editFieldSmall');
    $("#MinThirdByte_Manual").addClass('editField editFieldSmall');
    $("#MaxThirdByte_Manual").addClass('editField editFieldSmall');

    $(function () {


    <% if (Model.CanUpdate){ %>  

        const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("thirdMinThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MinThirdByte_Manual", arrayForSpinner);
        createSpinnerModal("thirdMaxThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MaxThirdByte_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 5, 1);
        createSpinnerModal("thirdHystNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "HystThirdByte_Manual", arrayForSpinner1);

        <%}%>

        $("#MinThirdByte_Manual").change(function () {
            if (isANumber($("#MinThirdByte_Manual").val())) {
                if ($("#MinThirdByte_Manual").val() < <%:(DefaultMin)%>)
                    $("#MinThirdByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinThirdByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinThirdByte_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MinThirdByte_Manual").val()) > parseFloat($("#MaxThirdByte_Manual").val()))
                    $("#MinThirdByte_Manual").val(parseFloat($("#MaxThirdByte_Manual").val()));
            }
            else {
                $('#MinThirdByte_Manual').val(<%: isFarh?min3.ToDouble().ToFahrenheit():min3  %>);
            }
        });
    });

    $(function () {

        $("#MaxThirdByte_Manual").change(function () {
            if (isANumber($("#MaxThirdByte_Manual").val())) {
                if ($("#MaxThirdByte_Manual").val() < <%:(DefaultMin)%>)
                    $("#MaxThirdByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxThirdByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxThirdByte_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MaxThirdByte_Manual").val()) < parseFloat($("#MinThirdByte_Manual").val()))
                    $("#MaxThirdByte_Manual").val(parseFloat($("#MinThirdByte_Manual").val()));
            }
            else {
                $('#MaxThirdByte_Manual').val(<%: isFarh?max3.ToDouble().ToFahrenheit():max3  %>);
            }
        });
    });

    $(function () {

        $("#HystThirdByte_Manual").change(function () {
            if (isANumber($("#HystThirdByte_Manual").val())) {
                if ($("#HystThirdByte_Manual").val() < 0)
                    $("#HystThirdByte_Manual").val(0);
                if ($("#HystThirdByte_Manual").val() > 5)
                    $("#HystThirdByte_Manual").val(5)
            }
            else {
                $('#HystThirdByte_Manual').val(<%: hyst3 %>);
            }
        });
    });
</script>
