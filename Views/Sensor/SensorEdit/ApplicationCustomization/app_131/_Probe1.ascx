<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst1 = FilteredQuadTemperature.GetHystFirstByte(Model);
    sbyte min1 = FilteredQuadTemperature.GetMinFirstByte(Model);
    sbyte max1 = FilteredQuadTemperature.GetMaxFirstByte(Model);
    bool isFarh = FilteredQuadTemperature.IsFahrenheit(Model.SensorID);
    string datumName0 = Model.GetDatumName(0);
%>

<h5>Probe One</h5>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFirstByte_Manual" id="MinFirstByte_Manual" value="<%=isFarh?  min1.ToDouble().ToFahrenheit():min1 %>" />
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
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFirstByte_Manual" id="MaxFirstByte_Manual" value="<%=isFarh?  max1.ToDouble().ToFahrenheit():max1 %>" />
        <a id="firstMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="HystFirstByte_Manual" id="HystFirstByte_Manual" value="<%=Math.Round(hyst1.ToDouble()) %>" />
        <a id="firstHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--QuadTemperature.GetProbe1Label(Model.SensorID)--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Probe One Name","Probe One Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName0" name="datumName0" value="<%= Model.GetDatumName(0)%>"  />
        <%: Html.ValidationMessageFor(model => datumName0)%>
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
    $("#HystFirstByte_Manual").addClass('editField editFieldSmall');
    $("#MinFirstByte_Manual").addClass('editField editFieldSmall');
    $("#MaxFirstByte_Manual").addClass('editField editFieldSmall');

    $(function () {
        <% if (Model.CanUpdate) { %>

            const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
            createSpinnerModal("firstMinThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MinFirstByte_Manual", arrayForSpinner);
            createSpinnerModal("firstMaxThreshNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "MaxFirstByte_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 5, 1);
        
            createSpinnerModal("firstHystNum", " <%:Html.Raw(FilteredQuadTemperature.IsFahrenheit(Model.SensorID) ? "\u00B0 F" : "\u00B0 C")%>", "HystFirstByte_Manual", arrayForSpinner1);
        <%}%>

        $("#MinFirstByte_Manual").change(function () {
            if (isANumber($("#MinFirstByte_Manual").val()))
            {
                if ($("#MinFirstByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MinFirstByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinFirstByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinFirstByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MinFirstByte_Manual").val()) > parseFloat($("#MaxFirstByte_Manual").val()))
                    $("#MinFirstByte_Manual").val(parseFloat($("#MaxFirstByte_Manual").val()));
            }
            else
            {
                $('#MinFirstByte_Manual').val(<%: isFarh?min1.ToDouble().ToFahrenheit():min1  %>);
            }
        });
    });
    $(function () {
        $("#MaxFirstByte_Manual").change(function () {
            if (isANumber($("#MaxFirstByte_Manual").val()))
            {
                if ($("#MaxFirstByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MaxFirstByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxFirstByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxFirstByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MaxFirstByte_Manual").val()) < parseFloat($("#MinFirstByte_Manual").val()))
                    $("#MaxFirstByte_Manual").val($("#MinFirstByte_Manual").val());
            }
            else
            {
                $('#MaxFirstByte_Manual').val(<%: isFarh?max1.ToDouble().ToFahrenheit():max1  %>);
            }
        });
    });

    $(function () {
            $("#HystFirstByte_Manual").change(function () {
                if (isANumber($("#HystFirstByte_Manual").val())){
                    if ($("#HystFirstByte_Manual").val() < 0)
                        $("#HystFirstByte_Manual").val(0);
                    if ($("#HystFirstByte_Manual").val() > 5)
                        $("#HystFirstByte_Manual").val(5)
                }
                else
                {
                    $('#HystFirstByte_Manual').val(<%: hyst1 %>);
                }
            });
        });
</script>
