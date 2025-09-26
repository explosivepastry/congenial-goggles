<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
   
    int twaHyst = Gas_CO.AverageHysteresis(Model);
    int twaMin = Gas_CO.AverageMinimumThreshold(Model);
    int twaMax = Gas_CO.AverageMaximumThreshold(Model);
   
    
%>

<div class="Thres34 Thres34_All Thres34_Time_Weighted_Average">
<h5>Use Aware State When Time Weighted Average</h5>

    <%----- MIN Time Weighted Average -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (PPM)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaMinimumThreshold_Manual" id="twaMinimumThreshold_Manual" value="<%=twaMin %>" /> 
            <a id="twaMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%> 
        </div>
    </div>
    
    
    <%----- MAX Time Weighted Average -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (PPM)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaMaximumThreshold_Manual" id="twaMaximumThreshold_Manual" value="<%=twaMax %>" /> 
            <a id="twaMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
        </div>
    </div>
    
    
    <%----- HYST Time Weighted Average -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Time Weighted Average Aware State Buffer","Time Weighted Average Aware State Buffer")%> (PPM)
        </div>
        <div class="col sensorEditFormInput" id="Div1">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaHysteresis_Manual" id="twaHysteresis_Manual" value="<%=twaHyst %>" /> 
            <a  id="twaHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.Hysteresis)%> 
        </div>
    </div>
</div>

<%

    var twaDefaultMin = Gas_CO.AverageDefaultMinThreshold;
    var twaDefaultMax = Gas_CO.AverageDefaultMaxThreshold;
                                
%>


<script>
    //Min TWA
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(<%:twaDefaultMin%>, <%:twaDefaultMax%>, 1);
        createSpinnerModal("twaMinThreshNum", "Minimum Threshold", "twaMinimumThreshold_Manual", arrayForSpinner);


        <%}%>

        $("#twaMinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#twaMinimumThreshold_Manual").change(function () {
            if (isANumber($("#twaMinimumThreshold_Manual").val())){
                if ($("#twaMinimumThreshold_Manual").val() < <%:(twaDefaultMin)%>)
                        $("#twaMinimumThreshold_Manual").val(<%:twaDefaultMin%>);
                if ($("#twaMinimumThreshold_Manual").val() > <%:(twaDefaultMax)%>)
                        $("#twaMinimumThreshold_Manual").val(<%:(twaDefaultMax)%>);

                if (parseFloat($("#twaMinimumThreshold_Manual").val()) > parseFloat($("#twaMaximumThreshold_Manual").val()))
                    $("#twaMinimumThreshold_Manual").val(parseFloat($("#twaMaximumThreshold_Manual").val()));
                $("#twaMaximumThreshold_Manual").change();
            }
            else
            {
                $("#twaMinimumThreshold_Manual").val(<%: twaMin%>);
            }
        });
    });

    //Max TWA
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(<%:twaDefaultMin%>, <%:twaDefaultMax%>, 1);
        createSpinnerModal("twaMaxThreshNum", "Max Threshold", "twaMaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#twaMaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#twaMaximumThreshold_Manual").change(function () {
            if (isANumber($("#twaMaximumThreshold_Manual").val())){
                if ($("#twaMaximumThreshold_Manual").val() < <%:(twaDefaultMin)%>)
                        $("#twaMaximumThreshold_Manual").val(<%:(twaDefaultMin)%>);
                if ($("#twaMaximumThreshold_Manual").val() > <%:(twaDefaultMax)%>)
                        $("#twaMaximumThreshold_Manual").val(<%:(twaDefaultMax)%>);

                if (parseFloat($("#twaMaximumThreshold_Manual").val()) < parseFloat($("#twaMinimumThreshold_Manual").val()))
                    $("#twaMaximumThreshold_Manual").val(parseFloat($("#twaMinimumThreshold_Manual").val()));
                $("#twaMinimumThreshold_Manual").change();
            }
            else
            {
                $("#twaMaximumThreshold_Manual").val(<%: twaMax%>);
            }
        });
    });

    //Hyst TWA 
    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(0, 5, 1);
        createSpinnerModal("twaHystNum", "Aware State Buffer", "twaHysteresis_Manual", arrayForSpinner);

        <%}%>

    $("#twaHysteresis_Manual").addClass('editField editFieldSmall');

    $("#twaHysteresis_Manual").change(function () {
        if (isANumber($("#twaHysteresis_Manual").val())) {
            if ($("#twaHysteresis_Manual").val() < 0)
                $("#twaHysteresis_Manual").val(0);
            if ($("#twaHysteresis_Manual").val() > 5)
                $("#twaHysteresis_Manual").val(5);
        }
        else
            $("#twaHysteresis_Manual").val(<%: twaHyst%>);
    });
});
</script>