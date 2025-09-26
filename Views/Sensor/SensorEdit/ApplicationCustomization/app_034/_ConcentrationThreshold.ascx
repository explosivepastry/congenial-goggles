<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    int conHyst = Gas_CO.ConcentrationHysteresis(Model);
    int conMin = Gas_CO.ConcentrationMinimumThreshold(Model);
    int conMax = Gas_CO.ConcentrationMaximumThreshold(Model);


%>

<div class="Thres34 Thres34_All Thres34_Concentration">
<h5>Use Aware State When Concentration</h5>

    <%----- MIN Concentration -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (PPM)
        </div>
        <div class="col sensorEditFormInput">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="conMinimumThreshold_Manual" id="conMinimumThreshold_Manual" value="<%=conMin %>" /> 
            <a id="conMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
        </div>
    </div>
    
    
    <%----- MAX Concentration -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (PPM)
        </div>
        <div class="col sensorEditFormInput">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="conMaximumThreshold_Manual" id="conMaximumThreshold_Manual" value="<%=conMax %>" /> 
            <a id="conMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
        </div>
    </div>
    
    
    <%----- HYST Concentration -----%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Concentration Aware State Buffer","Concentration Aware State Buffer")%> (PPM)
        </div>
        <div class="col sensorEditFormInput" id="hyst3">
            <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="conHysteresis_Manual" id="conHysteresis_Manual" value="<%=conHyst %>" /> 
            <a  id="ConHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            <%: Html.ValidationMessageFor(model => model.Hysteresis)%> 
        </div>
    </div>
</div>

<%

    var conDefaultMin = Gas_CO.ConcentrationDefaultMinThreshold;
    var conDefaultMax = Gas_CO.ConcentrationDefaultMaxThreshold;
                                
%>


<script>
    //Min Concentration
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(<%:conDefaultMin%>, <%:conDefaultMax%>, 1);
        createSpinnerModal("conMinThreshNum", "PPM", "conMinimumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#conMinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#conMinimumThreshold_Manual").change(function () {
            let max = parseFloat($("#conMaximumThreshold_Manual").val());
            let min = parseFloat($("#conMinimumThreshold_Manual").val());

            if (isANumber($("#conMinimumThreshold_Manual").val())){
                if (min < <%:(conDefaultMin)%>)
                        $("#conMinimumThreshold_Manual").val(<%:(conDefaultMin)%>);
                if (min > <%:(conDefaultMax)%>)
                        $("#conMinimumThreshold_Manual").val(<%:(conDefaultMax)%>);

                if (min > max)
                    $("#conMinimumThreshold_Manual").val($("#conMaximumThreshold_Manual").val());

                if (min > max)
                    $("#conMinimumThreshold_Manual").val(max);
            }
            else
            { 
                $("#conMinimumThreshold_Manual").val(<%: conMin%>);
            }
         });
    });

    //Max Concentration
    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner = arrayBuilder(<%:conDefaultMin%>, <%:conDefaultMax%>, 1);
        createSpinnerModal("conMaxThreshNum", "PPM", "conMaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#conMaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#conMaximumThreshold_Manual").change(function () {
            if (isANumber($("#conMaximumThreshold_Manual").val())){
                if ($("#conMaximumThreshold_Manual").val() < <%:(conDefaultMin)%>)
                        $("#conMaximumThreshold_Manual").val(<%:(conDefaultMin)%>);
                if ($("#conMaximumThreshold_Manual").val() > <%:(conDefaultMax)%>)
                        $("#conMaximumThreshold_Manual").val(<%:(conDefaultMax)%>);

                if (parseFloat($("#conMaximumThreshold_Manual").val()) < parseFloat($("#conMinimumThreshold_Manual").val()))
                    $("#conMaximumThreshold_Manual").val(parseFloat($("#conMinimumThreshold_Manual").val()));
                $("#conMinimumThreshold_Manual").change();
            }
            else
            {
                $("#conMaximumThreshold_Manual").val(<%: conMax%>);
            }
        });
    });

        //Hyst Concentration
        $(function () {
          <% if(Model.CanUpdate) { %>

            let arrayForSpinner = arrayBuilder(0, 5, 1);
            createSpinnerModal("ConHystNum", "PPM", "conHysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#conHysteresis_Manual").addClass('editField editFieldSmall');

        $("#conHysteresis_Manual").change(function () {
            if (isANumber($("#conHysteresis_Manual").val())) {
                if ($("#conHysteresis_Manual").val() < 0)
                    $("#conHysteresis_Manual").val(0);
                if ($("#conHysteresis_Manual").val() > 5)
                    $("#conHysteresis_Manual").val(5);
            }
            else
                $("#conHysteresis_Manual").val(<%: conHyst%>);
        });
    });
</script>