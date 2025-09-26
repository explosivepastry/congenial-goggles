<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst2 = QuadTemperature.GetHystSecondByte(Model);
    sbyte min2 = QuadTemperature.GetMinSecondByte(Model);
    sbyte max2 = QuadTemperature.GetMaxSecondByte(Model);
    bool isFarh = QuadTemperature.IsFahrenheit(Model.SensorID);
    string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";
    string datumName1 = Model.GetDatumName(1);
%>

<p class="useAwareState">Probe Two</p>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinSecondByte_Manual" id="MinSecondByte_Manual" value="<%=isFarh?  min2.ToDouble().ToFahrenheit():min2 %>" />
        <a id="secMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxSecondByte_Manual" id="MaxSecondByte_Manual" value="<%=isFarh?  max2.ToDouble().ToFahrenheit():max2 %>" />
        <a id="secMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="HystSecondByte_Manual" id="HystSecondByte_Manual" value="<%=Math.Round(hyst2.ToDouble()) %>" />
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
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName1" name="datumName1" value="<%= Model.GetDatumName(1)%>"  />
        <%: Html.ValidationMessageFor(model => datumName1)%>
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
    $("#HystSecondByte_Manual").addClass('editField editFieldSmall');
    $("#MinSecondByte_Manual").addClass('editField editFieldSmall');
    $("#MaxSecondByte_Manual").addClass('editField editFieldSmall');

    //MobiScroll - Min
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("secMinThreshNum", "<%=tempLabel %>", "MinSecondByte_Manual", arrayForSpinner);

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

    //MobiScroll - Max 
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("secMaxThreshNum", "<%=tempLabel %>", "MaxSecondByte_Manual", arrayForSpinner1);

        <%}%>

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

    //MobiScroll - Hyst
    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinnerq11 = arrayBuilder(0, <%:MaxAwareState%>, 1);
        createSpinnerModal("secHystNum", "<%=tempLabel %>", "HystSecondByte_Manual", arrayForSpinnerq11);

        <%}%>

        $("#HystSecondByte_Manual").change(function () {
            if (isANumber($("#HystSecondByte_Manual").val())){
                if ($("#HystSecondByte_Manual").val() < 0)
                    $("#HystSecondByte_Manual").val(0);
                if ($("#HystSecondByte_Manual").val() > <%:MaxAwareState%>)
                    $("#HystSecondByte_Manual").val(<%:MaxAwareState%>)
            }
            else
            {
                $('#HystSecondByte_Manual').val(<%: hyst2 %>);
            }
        });
    });
</script>
