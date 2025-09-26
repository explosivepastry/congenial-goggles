<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    double conHyst = CO_Meter.GetConcentrationHysteresis(Model);
    int twaMax = CO_Meter.GetTWAMaxThreshold(Model);
    int conMax = CO_Meter.GetConcentrationMaximumThreshold(Model);   
%>

<p class="useAwareState">Instantaneous Settings</p>
<%----Concentration----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|Concentration Threshold","Concentration Threshold")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="conMaximumThreshold_Manual" id="conMaximumThreshold_Manual" value="<%=conMax %>" />
        <a id="conMaxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%----Concentration buffer----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Concentration Buffer","Concentration Buffer")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="conHysteresis_Manual" id="conHysteresis_Manual" value="<%=conHyst %>" />
        <a id="conBuffNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<p class="useAwareState">TWA Settings</p>
<%----twa min----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|Time Weighted Average Threshold","Time Weighted Average Threshold")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input  class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaMaximumThreshold_Manual" id="twaMaximumThreshold_Manual" value="<%=twaMax %>" />
        <a id="twaMaxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(document).ready(function () {

                <% if (Model.CanUpdate)
                   { %>

        const arrayForSpinner = arrayBuilder(0, 50, 10);
        createSpinnerModal("conMaxNum", "PPM", "conMaximumThreshold_Manual", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(0, 10, 1);
        createSpinnerModal("conBuffNum", "PPM", "conHysteresis_Manual", arrayForSpinner1);
        const arrayForSpinner2 = arrayBuilder(0, 400, 10);
        createSpinnerModal("twaMaxNum", "PPM", "twaMaximumThreshold_Manual", arrayForSpinner2);

         <%}%>
       
        $("#conHysteresis_Manual").change(function () {
            if (isANumber($("#conHysteresis_Manual").val())){
                if($("#conHysteresis_Manual").val() <0)
                    $("#conHysteresis_Manual").val(0);
                if($("#conHysteresis_Manual").val()>10)
                    $("#conHysteresis_Manual").val(10);     
            }
            else
                $("#conHysteresis_Manual").val(<%: conHyst%>);
    });


        $("#conMaximumThreshold_Manual").change(function () {
            if (isANumber($("#conMaximumThreshold_Manual").val())){
                if ($("#conMaximumThreshold_Manual").val() < 0)
                    $("#conMaximumThreshold_Manual").val(0);
                if ($("#conMaximumThreshold_Manual").val() > 50)
                    $("#conMaximumThreshold_Manual").val(50);
                
            }
            else
            {
                $("#conMaximumThreshold_Manual").val(<%: conMax%>);
       
    }
});

        $("#twaMaximumThreshold_Manual").change(function () {
            if (isANumber($("#twaMaximumThreshold_Manual").val())){
                if ($("#twaMaximumThreshold_Manual").val() < 0)
                    $("#twaMaximumThreshold_Manual").val(0);
                if ($("#twaMaximumThreshold_Manual").val() > 400)
                    $("#twaMaximumThreshold_Manual").val(400);
              
            }
            else
            {
                $("#twaMaximumThreshold_Manual").val(<%: conMax%>);
           
        }
    });

    });

</script>
