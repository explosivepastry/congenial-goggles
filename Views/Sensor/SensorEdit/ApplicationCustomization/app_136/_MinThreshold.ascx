<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
bool isF = Temperature.IsFahrenheit(Model.SensorID);
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";


        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%>&nbsp;<span id="belowType">(<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)</span>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%
    long DefaultMin = -40;
    long DefaultMax = 125;

    if (isF)
    {
        DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
        DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
    }

%>


<script>

    $(function () {

                <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 1);
        createSpinnerModal("minThreshNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < <%:(DefaultMin)%>)
                     $("#MinimumThreshold_Manual").val(<%:(DefaultMin)%>);
                 if ($("#MinimumThreshold_Manual").val() > <%:(DefaultMax)%>)
                     $("#MinimumThreshold_Manual").val(<%:(DefaultMax)%>);

                 if (parseFloat($("#MinimumThreshold_Manual").val()) >= parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat(Number($("#MaximumThreshold_Manual").val()) - Number(1)));

   
             } else {

                 $("#MinimumThreshold_Manual").val(<%: Min%>);
             }
         });

    });
</script>
<%} %>