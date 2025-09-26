<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst3 = QuadTemperature.GetHystThirdByte(Model);
    sbyte min3 = QuadTemperature.GetMinThirdByte(Model);
    sbyte max3 = QuadTemperature.GetMaxThirdByte(Model);
    bool isFarh = QuadTemperature.IsFahrenheit(Model.SensorID);
    string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";
    string datumName2 = Model.GetDatumName(2);
%>

<p class="useAwareState">Probe Three</p>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinThirdByte_Manual" id="MinThirdByte_Manual" value="<%=isFarh?  min3.ToDouble().ToFahrenheit():min3 %>" />
        <a id="thirdMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxThirdByte_Manual" id="MaxThirdByte_Manual" value="<%=isFarh?  max3.ToDouble().ToFahrenheit():max3 %>" />
        <a id="thirdMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="HystThirdByte_Manual" id="HystThirdByte_Manual" value="<%=Math.Round(hyst3.ToDouble()) %>" />
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
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName2" name="datumName2" value="<%= Model.GetDatumName(2)%>" />
        <%: Html.ValidationMessageFor(model => datumName2)%>
    </div>
</div>

<div class="clearfix"></div>

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
    $("#HystThirdByte_Manual").addClass('editField editFieldSmall');
    $("#MinThirdByte_Manual").addClass('editField editFieldSmall');
    $("#MaxThirdByte_Manual").addClass('editField editFieldSmall');

    //MobiScroll - Min
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("thirdMinThreshNum", "<%=tempLabel %>", "MinThirdByte_Manual", arrayForSpinner);

        <%}%>

        $("#MinThirdByte_Manual").change(function () {
            if (isANumber($("#MinThirdByte_Manual").val()))
            {
                if ($("#MinThirdByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MinThirdByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinThirdByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinThirdByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MinThirdByte_Manual").val()) > parseFloat($("#MaxThirdByte_Manual").val()))
                    $("#MinThirdByte_Manual").val(parseFloat($("#MaxThirdByte_Manual").val()));
            }
            else
            {
                $('#MinThirdByte_Manual').val(<%: isFarh?min3.ToDouble().ToFahrenheit():min3  %>);
            }
        });
    });

    //MobiScroll - Max 
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinnerq = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("thirdMaxThreshNum", "<%=tempLabel %>", "MaxThirdByte_Manual", arrayForSpinnerq);

        <%}%>

        $("#MaxThirdByte_Manual").change(function () {
            if (isANumber($("#MaxThirdByte_Manual").val()))
            {
                if ($("#MaxThirdByte_Manual").val() < <%:(DefaultMin)%>)
                      $("#MaxThirdByte_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaxThirdByte_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaxThirdByte_Manual").val(<%:(DefaultMax)%>); 
                
                if (parseFloat($("#MaxThirdByte_Manual").val()) < parseFloat($("#MinThirdByte_Manual").val()))
                    $("#MaxThirdByte_Manual").val(parseFloat($("#MinThirdByte_Manual").val()));
            }
            else
            {
                $('#MaxThirdByte_Manual').val(<%: isFarh?max3.ToDouble().ToFahrenheit():max3  %>);
            }
        });
    });

    //MobiScroll - Hyst
    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinnerq11 = arrayBuilder(0, <%:MaxAwareState%>, 1);
        createSpinnerModal("thirdHystNum", "<%=tempLabel %>", "HystThirdByte_Manual", arrayForSpinnerq11);
        <%}%>

        $("#HystThirdByte_Manual").change(function () {
            if (isANumber($("#HystThirdByte_Manual").val())){
                if ($("#HystThirdByte_Manual").val() < 0)
                    $("#HystThirdByte_Manual").val(0);
                if ($("#HystThirdByte_Manual").val() > <%:MaxAwareState%>)
                    $("#HystThirdByte_Manual").val(<%:MaxAwareState%>)
            }
            else
            {
                $('#HystThirdByte_Manual').val(<%: hyst3 %>);
            }
        });
    });
</script>
