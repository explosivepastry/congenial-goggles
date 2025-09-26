<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";
        string label = "";
        string tempLabel = Temperature.IsFahrenheit(Model.SensorID) ? "°F" : "°C";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> (<%: Html.Label(tempLabel) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <%: label %>
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
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
        createSpinnerModal("minThreshNum", "<%=tempLabel %>", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

         $("#MinimumThreshold_Manual").change(function () {
             if (isANumber($("#MinimumThreshold_Manual").val())){
                 if ($("#MinimumThreshold_Manual").val() < <%:(DefaultMin)%>)
                     $("#MinimumThreshold_Manual").val(<%:(DefaultMin)%>);
                 if ($("#MinimumThreshold_Manual").val() > <%:(DefaultMax)%>)
                     $("#MinimumThreshold_Manual").val(<%:(DefaultMax)%>);

                 if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                     $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                 $("#MaximumThreshold_Manual").change();

             }else{

                 $("#MinimumThreshold_Manual").val(<%: Min%>);
             }
         });

     });
</script>
<%} %>