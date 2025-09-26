<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    int pitchMin = Tilt.GetPitchMin(Model);
    int pitchMax = Tilt.GetPitchMax(Model);

    string label = Html.TranslateTag("Sensor/ApplicationCustomization/default|Degrees","Degrees");

%>

<p class="useAwareState">Pitch Threshold</p>

<%----- MIN Pitch Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Min Pitch Threshold","Min Pitch Threshold")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="pitchMin_Manual" id="pitchMin_Manual" value="<%=pitchMin %>" />

        <a id="pitchMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%----- MAX Pitch Threshold -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Max Pitch Threshold","Max Pitch Threshold")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="pitchMax_Manual" id="pitchMax_Manual" value="<%=pitchMax %>" />
    
        <a id="pitchMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $("#pitchMin_Manual").addClass('editField editFieldSmall');
    $("#pitchMax_Manual").addClass('editField editFieldSmall');


    $(function () {
        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner1 = arrayBuilder(-180, 180, 1);
        createSpinnerModal("pitchMinThreshNum", "<%=label%>", "pitchMin_Manual", arrayForSpinner1);

        <%}%>

        $("#pitchMin_Manual").change(function () {
            if (isANumber($("#pitchMin_Manual").val())) {
                if (parseFloat($("#pitchMin_Manual").val()) < -180) {
                    $("#pitchMin_Manual").val(-180);
                }

                if (parseFloat($("#pitchMin_Manual").val()) > parseFloat($("#pitchMax_Manual").val())){
                    $("#pitchMin_Manual").val(parseFloat($("#pitchMax_Manual").val()));
                }
            }
            else {
                $("#pitchMin_Manual").val(<%: pitchMin%>);
            }
        });
    });

    $(function () {
        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner1 = arrayBuilder(-180, 180, 1);
        createSpinnerModal("pitchMaxThreshNum", "<%=label%>", "pitchMax_Manual", arrayForSpinner1);

        <%}%>


        $("#pitchMax_Manual").change(function () {
            if (isANumber($("#pitchMax_Manual").val())) {
                if (parseFloat($("#pitchMax_Manual").val()) > 180)
                    $("#pitchMax_Manual").val(180);

                if (parseFloat($("#pitchMax_Manual").val()) < parseFloat($("#pitchMin_Manual").val()))
                    $("#pitchMax_Manual").val(parseFloat($("#pitchMin_Manual").val()));
            }
            else {
                $("#pitchMax_Manual").val(<%: pitchMax%>);
            }
        });
    });

</script>
