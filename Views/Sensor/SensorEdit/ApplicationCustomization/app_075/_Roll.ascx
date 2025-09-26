<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    
    int rollMin = Tilt.GetRollMin(Model);
    int rollMax = Tilt.GetRollMax(Model);

    string label = Html.TranslateTag("Sensor/ApplicationCustomization/default|Degrees","Degrees");
    
%>

<p class="useAwareState">Roll Threshold</p>

<%----- MIN Roll Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Min Roll Threshold","Min Roll Threshold")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="rollMin_Manual" id="rollMin_Manual" value="<%=rollMin %>" /> 
        <a id="rollMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<%----- MAX Roll Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Max Roll Threshold","Max Roll Threshold")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="rollMax_Manual" id="rollMax_Manual" value="<%=rollMax %>" />
        <a id="rollMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>



<script>

    $(function () {
        <% if (Model.CanUpdate){ %>

        let arrayForSpinner22 = arrayBuilder(-180, 180, 1);
        createSpinnerModal("rollMinThreshNum", "<%=label%>", "rollMin_Manual", arrayForSpinner22);

        <%}%>

        $("#rollMin_Manual").addClass('editField editFieldSmall');

        $("#rollMin_Manual").change(function () {
            if (isANumber($("#rollMin_Manual").val())){
                if (parseFloat($("#rollMin_Manual").val()) < -180) {
                    $("#rollMin_Manual").val(-180);
                }

                if (parseFloat($("#rollMin_Manual").val()) > parseFloat($("#rollMax_Manual").val()))
                    $("#rollMin_Manual").val(parseFloat($("#rollMax_Manual").val()));
            }
            else
            { 
                $("#rollMin_Manual").val(<%: rollMin%>);
            }
         });
    });

    $(function () {
        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner11 = arrayBuilder(-180, 180, 1);
        createSpinnerModal("rollMaxThreshNum", "<%=label%>", "rollMax_Manual", arrayForSpinner11);

        <%}%>

        $("#rollMax_Manual").addClass('editField editFieldSmall form-control');

        $("#rollMax_Manual").change(function () {
            if (isANumber($("#rollMax_Manual").val())){
                if (parseFloat($("#rollMax_Manual").val()) > 180)
                    $("#rollMax_Manual").val(180);

                if (parseFloat($("#rollMax_Manual").val()) < parseFloat($("#rollMin_Manual").val()))
                    $("#rollMax_Manual").val(parseFloat($("#rollMin_Manual").val()));
            }
            else
            {
                $("#rollMax_Manual").val(<%: rollMax%>);
            }
        });
    });
</script>