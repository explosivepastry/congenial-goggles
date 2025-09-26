<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst1 = QuadTemperature.GetHystFirstByte(Model);
    sbyte min1 = QuadTemperature.GetMinFirstByte(Model);
    sbyte max1 = QuadTemperature.GetMaxFirstByte(Model);
    bool isFarh = QuadTemperature.IsFahrenheit(Model.SensorID);
    string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";

    string datumName0 = Model.GetDatumName(0);
%>

<p class="useAwareState">Probe One</p>

<%----- MIN Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFirstByte_Manual" id="MinFirstByte_Manual" value="<%=isFarh?  min1.ToDouble().ToFahrenheit():min1 %>" />
        <a id="firstMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFirstByte_Manual" id="MaxFirstByte_Manual" value="<%=isFarh?  max1.ToDouble().ToFahrenheit():max1 %>" />
        <a id="firstMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<%----- HYST -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_092|Aware State Buffer","Aware State Buffer")%> &nbsp <%: Html.Label(tempLabel) %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="HystFirstByte_Manual" id="HystFirstByte_Manual" value="<%=Math.Round(hyst1.ToDouble()) %>" />
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
        <input type="text" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName0" name="datumName0" value="<%= Model.GetDatumName(0)%>" />
        <%: Html.ValidationMessageFor(model => datumName0)%>
    </div>
</div>

<div class="clearfix"></div>
<%--<div class="ln_solid"></div>--%>

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
    $("#HystFirstByte_Manual").addClass('editField editFieldSmall');
    $("#MinFirstByte_Manual").addClass('editField editFieldSmall');
    $("#MaxFirstByte_Manual").addClass('editField editFieldSmall');

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinnerq11 = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("firstMinThreshNum", "<%=tempLabel %>", "MinFirstByte_Manual", arrayForSpinnerq11);

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
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinnerq = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 5);
        createSpinnerModal("firstMaxThreshNum", "<%=tempLabel %>", "MaxFirstByte_Manual", arrayForSpinnerq);

        <%}%>

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

    //MobiScroll - Hyst
    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner = arrayBuilder(0, <%:MaxAwareState%>, 1);
        createSpinnerModal("firstHystNum", "<%=tempLabel %>", "HystFirstByte_Manual", arrayForSpinner);

        <%}%>

            $("#HystFirstByte_Manual").change(function () {
                if (isANumber($("#HystFirstByte_Manual").val())){
                    if ($("#HystFirstByte_Manual").val() < 0)
                        $("#HystFirstByte_Manual").val(0);
                    if ($("#HystFirstByte_Manual").val() > <%:MaxAwareState%>)
                        $("#HystFirstByte_Manual").val(<%:MaxAwareState%>)
                }
                else
                {
                    $('#HystFirstByte_Manual').val(<%: hyst1 %>);
                }
            });
        });
</script>
