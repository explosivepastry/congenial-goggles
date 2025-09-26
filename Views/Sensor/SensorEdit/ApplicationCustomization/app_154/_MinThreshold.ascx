<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
		ResistiveBridgeMeter appBase = new ResistiveBridgeMeter(Model.SensorID);
		string Min = ResistiveBridgeMeter.GetMinimumThreshold(Model, appBase).ToString();
		string Max = ResistiveBridgeMeter.GetMaximumThreshold(Model, appBase).ToString();
      
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MinThreshMinVal = <%= ResistiveBridgeMeter.GetBaseMinimumThreshold(Model, appBase) %>;
        var MinThreshMaxVal = <%= ResistiveBridgeMeter.GetBaseMaximumThreshold(Model, appBase) %>;
        const step = 10 ** (Math.round(Math.log10(MinThreshMaxVal - MinThreshMinVal)) - 1);

        <% if (Model.CanUpdate)
           { %>
        
        const arrayForSpinner = arrayBuilder(MinThreshMinVal, MinThreshMaxVal, step);
        createSpinnerModal("minThreshNum", '<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>', "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall')

        $("#MinimumThreshold_Manual").change(function () {
            let min = parseFloat($("#MinimumThreshold_Manual").val());
            let max = parseFloat($("#MaximumThreshold_Manual").val());


            if (isANumber(min)){
                if (min < MinThreshMinVal)
                    $("#MinimumThreshold_Manual").val(MinThreshMinVal);
                if (min > MinThreshMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThreshMaxVal);

                if (min > max)
                    $("#MinimumThreshold_Manual").val(max);
            } else {
                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>