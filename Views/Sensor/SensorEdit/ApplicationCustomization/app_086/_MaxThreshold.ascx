<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";
  
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> (<%: Html.Label(tempLabel) %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <%: label %>
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%
        long DefaultMin = -200;
        long DefaultMax = 3000;

        if (Thermocouple.IsFahrenheit(Model.SensorID))
        {
            DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
            DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
        }
%>

<script>

    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(<%:(DefaultMin)%>, <%:(DefaultMax)%>, 10);
        createSpinnerModal("maxThreshNum", "<%=tempLabel %>", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>
        $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaximumThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                $("#MaximumThreshold_Manual").change();

            }else{

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>