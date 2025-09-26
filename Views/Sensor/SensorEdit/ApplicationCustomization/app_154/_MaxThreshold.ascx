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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {
        var MaxThreshMinVal = <%= ResistiveBridgeMeter.GetBaseMinimumThreshold(Model, appBase) %>;
        var MaxThreshMaxVal = <%= ResistiveBridgeMeter.GetBaseMaximumThreshold(Model, appBase) %>;
        const step = 10 ** (Math.round(Math.log10(MaxThreshMaxVal - MaxThreshMinVal)) - 1);

        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(MaxThreshMinVal, MaxThreshMaxVal, step);
        createSpinnerModal("maxThreshNum", '<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>', "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            let min = parseFloat($("#MinimumThreshold_Manual").val());
            let max = parseFloat($("#MaximumThreshold_Manual").val());

            if (isANumber(max)){
                if (max < MaxThreshMinVal)
                    $("#MaximumThreshold_Manual").val(MaxThreshMinVal);
                if (max > MaxThreshMaxVal)
                    $("#MaximumThreshold_Manual").val(MaxThreshMaxVal);

                if (max < min)
                    $("#MaximumThreshold_Manual").val(min);
            } else {
                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>