<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst2 = FilteredQuadTemperature.GetHystSecondByte(Model);
    sbyte min2 = FilteredQuadTemperature.GetMinSecondByte(Model);
    sbyte max2 = FilteredQuadTemperature.GetMaxSecondByte(Model);
    bool isFarh = FilteredQuadTemperature.IsFahrenheit(Model.SensorID);

    string datumName1 = Model.GetDatumName(1);
%>

<h5>Probe Two</h5>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinSecondByte_Manual" id="MinSecondByte_Manual" value="<%=isFarh?  min2.ToDouble().ToFahrenheit():min2 %>" />
        <a id="secMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxSecondByte_Manual" id="MaxSecondByte_Manual" value="<%=isFarh?  max2.ToDouble().ToFahrenheit():max2 %>" />
        <a id="secMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="HystSecondByte_Manual" id="HystSecondByte_Manual" value="<%=Math.Round(hyst2.ToDouble()) %>" />
        <a id="secHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--QuadTemperature.GetProbe1Label(Model.SensorID)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe Two Name","Probe Two Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName1" name="datumName1" value="<%= Model.GetDatumName(1)%>"  />
        <%: Html.ValidationMessageFor(model => datumName1)%>
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
    $("#HystSecondByte_Manual").addClass('editField editFieldSmall');
    $("#MinSecondByte_Manual").addClass('editField editFieldSmall');
    $("#MaxSecondByte_Manual").addClass('editField editFieldSmall');

    $(function () {
        <% if (Model.CanUpdate){ %>  

            const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
            createSpinnerModal("secMinThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MinSecondByte_Manual", arrayForSpinner);
            createSpinnerModal("secMaxThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MaxSecondByte_Manual", arrayForSpinner);

            const arrayForSpinner1 = arrayBuilder(0, 5, 1);
            createSpinnerModal("secHystNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "HystSecondByte_Manual", arrayForSpinner1);

        <%}%>

        $("#MinSecondByte_Manual").change(function () {
            if (isANumber($("#MinSecondByte_Manual").val()))
            {
                if ($("#MinSecondByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MinSecondByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinSecondByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinSecondByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MinSecondByte_Manual").val()) > parseFloat($("#MaxSecondByte_Manual").val()))
                    $("#MinSecondByte_Manual").val(parseFloat($("#MaxSecondByte_Manual").val()));
            }
            else
            {
                $('#MinSecondByte_Manual').val(<%: isFarh?min2.ToDouble().ToFahrenheit():min2  %>);
            }
        });
    });

    $(function () {

        $("#MaxSecondByte_Manual").change(function () {
            if (isANumber($("#MaxSecondByte_Manual").val()))
            {
                if ($("#MaxSecondByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MaxSecondByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxSecondByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxSecondByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MaxSecondByte_Manual").val()) < parseFloat($("#MinSecondByte_Manual").val()))
                    $("#MaxSecondByte_Manual").val(parseFloat($("#MinSecondByte_Manual").val()));
            }
            else
            {
                $('#MaxSecondByte_Manual').val(<%: isFarh?max2.ToDouble().ToFahrenheit():max2  %>);
            }
        });
    });

    $(function () {

        $("#HystSecondByte_Manual").change(function () {
            if (isANumber($("#HystSecondByte_Manual").val())){
                if ($("#HystSecondByte_Manual").val() < 0)
                    $("#HystSecondByte_Manual").val(0);
                if ($("#HystSecondByte_Manual").val() > 5)
                    $("#HystSecondByte_Manual").val(5)
            }
            else
            {
                $('#HystSecondByte_Manual').val(<%: hyst2 %>);
            }
        });
    });
</script>
