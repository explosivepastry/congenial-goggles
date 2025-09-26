<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isF = Gas_CO.IsFahrenheit(Model.SensorID);
    string temperatureLabel = isF ? "(°F)" : "(°C)";


    double tempHyst = Gas_CO.TemperatureHysteresis(Model);
    double tempMin = Gas_CO.TemperatureMinimumThreshold(Model);
    double tempMax = Gas_CO.TemperatureMaximumThreshold(Model);

    if (Gas_CO.IsFahrenheit(Model.SensorID))
    {
        tempHyst = tempHyst * 9 / 5;
        tempMin = tempMin.ToFahrenheit();
        tempMax = tempMax.ToFahrenheit();
    }


%>

<div class="Thres34 Thres34_All">
<h5>Use Aware State When Temperature</h5>

    <%----- MIN Temperature -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> <%: temperatureLabel %>
        </div>
        <div class="col sensorEditFormInput">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="tempMinimumThreshold_Manual" id="tempMinimumThreshold_Manual" value="<%=tempMin %>" /> 
            <a id="tempMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
        </div>
    </div>
    
    
    <%----- MAX Temperature -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> <%: temperatureLabel %>
        </div>
        <div class="col sensorEditFormInput">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="tempMaximumThreshold_Manual" id="tempMaximumThreshold_Manual" value="<%=tempMax %>" /> 
            <a id="tempMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
        </div>
    </div>
    
    
    <%----- HYST Temperature -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Temperature Aware State Buffer","Temperature Aware State Buffer")%> <%: temperatureLabel %>
        </div>
        <div class="col sensorEditFormInput" id="hyst3">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="tempHysteresis_Manual" id="tempHysteresis_Manual" value="<%=tempHyst %>" /> 
            <a  id="tempHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
        </div>
    </div>
</div>

<%
   
    var tempDefaultMin = Gas_CO.TemperatureDefaultMinThreshold / 10.0;
    var tempDefaultMax = Gas_CO.TemperatureDefaultMaxThreshold / 10.0;

    if (Gas_CO.IsFahrenheit(Model.SensorID))
    {
        var tdmin = tempDefaultMin.ToDouble().ToFahrenheit().ToInt();
        var tdmax = tempDefaultMax.ToDouble().ToFahrenheit().ToInt();
        tempDefaultMax = (Int16)tdmax;
        tempDefaultMin = (Int16)tdmin;
    }
                                
%>


<script>
    //Min Concentration
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(tempDefaultMin, tempDefaultMax, 1);
        createSpinnerModal("tempMinThreshNum", "Min Threshold", "tempMinimumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#tempMinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#tempMinimumThreshold_Manual").change(function () {
            if (isANumber($("#tempMinimumThreshold_Manual").val())){
                if ($("#tempMinimumThreshold_Manual").val() < <%:(tempDefaultMin)%>)
                        $("#tempMinimumThreshold_Manual").val(<%:(tempDefaultMin)%>);
                if ($("#tempMinimumThreshold_Manual").val() > <%:(tempDefaultMax)%>)
                        $("#tempMinimumThreshold_Manual").val(<%:(tempDefaultMax)%>);

                if (parseFloat($("#tempMinimumThreshold_Manual").val()) > parseFloat($("#tempMaximumThreshold_Manual").val()))
                    $("#tempMinimumThreshold_Manual").val(parseFloat($("#tempMaximumThreshold_Manual").val()));
                $("#tempMaximumThreshold_Manual").change();
            }
            else
            { 
                $("#tempMinimumThreshold_Manual").val(<%: tempMin%>);
            }
        });
    });

    //Max Concentration
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(tempDefaultMin, tempDefaultMax, 1);
        createSpinnerModal("tempMaxThreshNum", "Max Threshold", "tempMaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#tempMaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#tempMaximumThreshold_Manual").change(function () {
            if (isANumber($("#tempMaximumThreshold_Manual").val())){
                if ($("#tempMaximumThreshold_Manual").val() < <%:(tempDefaultMin)%>)
                        $("#tempMaximumThreshold_Manual").val(<%:(tempDefaultMin)%>);
                if ($("#tempMaximumThreshold_Manual").val() > <%:(tempDefaultMax)%>)
                        $("#tempMaximumThreshold_Manual").val(<%:(tempDefaultMax)%>);

                if ($("#tempMaximumThreshold_Manual").val() < $("#tempMinimumThreshold_Manual").val())
                    $("#tempMaximumThreshold_Manual").val($("#tempMinimumThreshold_Manual").val());

                if (parseFloat($("#tempMaximumThreshold_Manual").val()) < parseFloat($("#tempMinimumThreshold_Manual").val()))
                    $("#tempMaximumThreshold_Manual").val(parseFloat($("#tempMinimumThreshold_Manual").val()));
                $("#tempMinimumThreshold_Manual").change();
            }
            else
            {
                $("#tempMaximumThreshold_Manual").val(<%: tempMax%>);
            }
        });
    });

    //Hyst Concentration
    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(0, 1, 1);
        createSpinnerModal("tempHystNum", "Aware State Buffer", "conHysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#tempHysteresis_Manual").addClass('editField editFieldSmall');

        $("#tempHysteresis_Manual").change(function () {
            if (isANumber($("#tempHysteresis_Manual").val())) {
                if ($("#tempHysteresis_Manual").val() < 0)
                    $("#tempHysteresis_Manual").val(0);
                if ($("#tempHysteresis_Manual").val() > 5)
                    $("#tempHysteresis_Manual").val(5);
                }
                else
                $("#tempHysteresis_Manual").val(<%: tempHyst%>);
        });
        });
</script>