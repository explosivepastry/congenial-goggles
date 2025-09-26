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
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Above","Above")%>&nbsp;<span id="aboveType">(<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)</span>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%
        long DefaultMin = -40;
        long DefaultMax = 125;

        if (HandheldFoodProbe.IsFahrenheit(Model.SensorID))
        {
            DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
            DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
        }

           
%>

<script>
    var DefaultMin = Number(<%:DefaultMin%>);
    var DefaultMax = Number(<%:DefaultMax%>);

    $(function () {
        var DefaultMin = Number(<%:DefaultMin%>);
        var DefaultMax = Number(<%:DefaultMax%>);


               <% if (Model.CanUpdate)
                  { %>

        const arrayForSpinner = arrayBuilder(DefaultMin, DefaultMax, 1);
        createSpinnerModal("maxThreshNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>
        $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaximumThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MaximumThreshold_Manual").val()) <= parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat(Number($("#MinimumThreshold_Manual").val()) + Number(1)));


            }else{

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>