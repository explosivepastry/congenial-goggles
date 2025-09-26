<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        Max = LightSensor_PPFD.MaxThreshForUI(Model);
        Min = LightSensor_PPFD.MinThreshForUI(Model);
    
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-Low-Light-Title","Low Light Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-Saturated-Light-Title","Saturated Light Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%
        long DefaultMin = 0;
        long DefaultMax = 0;
        MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);                           
%>

<script>

    $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');


    $(function () {
               <% if (Model.CanUpdate)
                  { %>

        const arrayForSpinner = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 100);
        createSpinnerModal("minThreshNum", "Low Light Threshold", "MinimumThreshold_Manual", arrayForSpinner);
        createSpinnerModal("maxThreshNum", "Saturated Light Threshold", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#MinimumThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#MinimumThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#MinimumThreshold_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                $("#MaximumThreshold_Manual").change();
            }
            else
            {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
            }

            $("#Hysteresis_Manual").change();

        });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaximumThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMax)%>);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                //$("#MaximumThreshold_Manual").change();
            }
            else{
                $('#MaximumThreshold_Manual').val(<%: Max%>);
            }

            $("#Hysteresis_Manual").change();
        });
    }); 

</script>
<%} %>