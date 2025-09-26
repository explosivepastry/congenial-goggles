<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst4 = QuadTemperature.GetHystFourthByte(Model);
    sbyte min4 = QuadTemperature.GetMinFourthByte(Model);
    sbyte max4 = QuadTemperature.GetMaxFourthByte(Model);
    bool isFarh = QuadTemperature.IsFahrenheit(Model.SensorID);
    string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";
    string datumName3 = Model.GetDatumName(3);
%>

<p class="useAwareState">Probe Four</p>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFourthByte_Manual" id="MinFourthByte_Manual" value="<%=isFarh?  min4.ToDouble().ToFahrenheit():min4 %>" />
        <a id="fourMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFourthByte_Manual" id="MaxFourthByte_Manual" value="<%=isFarh?  max4.ToDouble().ToFahrenheit():max4 %>" />
        <a id="fourMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="HystFourthByte_Manual" id="HystFourthByte_Manual" value="<%=Math.Round(hyst4.ToDouble()) %>" />
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
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName3" name="datumName3" value="<%= Model.GetDatumName(3)%>" />
        <%: Html.ValidationMessageFor(model => datumName3)%>
    </div>
</div>

<div class="clearfix"></div>
<p class="useAwareState"></p>
<%
    long DefaultMin = -40;
    long DefaultMax = 125;
    long MaxAwareState = 12;

    if (isFarh)
    {
        DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
        DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
        MaxAwareState = MaxAwareState.ToDouble().ToFahrenheit().ToLong();
    }
                                
%>


<script>
    $("#HystFourthByte_Manual").addClass('editField editFieldSmall');
    $("#MinFourthByte_Manual").addClass('editField editFieldSmall');
    $("#MaxFourthByte_Manual").addClass('editField editFieldSmall');

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner11 = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("fourMinThreshNum", "<%=tempLabel %>", "MinFourthByte_Manual", arrayForSpinner11);

        <%}%>

        $("#MinFourthByte_Manual").change(function () {
            if (isANumber($("#MinFourthByte_Manual").val()))
            {
                if ($("#MinFourthByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MinFourthByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinFourthByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinFourthByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MinFourthByte_Manual").val()) > parseFloat($("#MaxFourthByte_Manual").val()))
                    $("#MinFourthByte_Manual").val(parseFloat($("#MaxFourthByte_Manual").val()));
            }
            else
            {
                $('#MinFourthByte_Manual').val(<%: isFarh?min4.ToDouble().ToFahrenheit():min4  %>);
            }
        });
    });

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("fourMaxThreshNum", "<%=tempLabel %>", "MaxFourthByte_Manual", arrayForSpinner1);

        <%}%>

        $("#MaxFourthByte_Manual").change(function () {
            if (isANumber($("#MaxFourthByte_Manual").val()))
            {
                if ($("#MaxFourthByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MaxFourthByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxFourthByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxFourthByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MaxFourthByte_Manual").val()) < parseFloat($("#MinFourthByte_Manual").val()))
                    $("#MaxFourthByte_Manual").val($("#MinFourthByte_Manual").val());
            }
            else
            {
                $('#MaxFourthByte_Manual').val(<%: isFarh?max4.ToDouble().ToFahrenheit():max4  %>);
            }
        });
    });

    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner = arrayBuilder(0, <%:MaxAwareState%>, 1);
        createSpinnerModal("fourHystNum", "<%=tempLabel %>", "HystFourthByte_Manual", arrayForSpinner);

        <%}%>

        $("#HystFourthByte_Manual").change(function () {
            if (isANumber($("#HystFourthByte_Manual").val())){
                if ($("#HystFourthByte_Manual").val() < 0)
                    $("#HystFourthByte_Manual").val(0);
                if ($("#HystFourthByte_Manual").val() > <%:MaxAwareState%>)
                    $("#HystFourthByte_Manual").val(<%:MaxAwareState%>)
            }
            else
            {
                $('#HystFourthByte_Manual').val(<%: hyst4 %>);
            }
        });
    });
</script>
