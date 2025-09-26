<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);       
%>

<p class="useAwareState">Use Aware State</p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Minimum","Minimum")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number"<%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>


<script>

    var minVal = <%: Resistance.GetLowValue(Model.SensorID) %>;
    var maxVal = <%: Resistance.GetHighValue(Model.SensorID) %>;

    var ohm_array = [0, 10, 50 ,100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 125000, maxVal];

    //MobiScroll
    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner1 = arrayBuilder(0, 100, 1);
        createSpinnerModal("minThreshNum", '<%: label %>', "MinimumThreshold_Manual", arrayForSpinner1);

        <%}%>
        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($("#MinimumThreshold_Manual").val() < minVal)
                    $("#MinimumThreshold_Manual").val(minVal);
                if ($("#MinimumThreshold_Manual").val() > maxVal)
                    $("#MinimumThreshold_Manual").val(maxVal);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
                //$("#MinimumThreshold_Manual").change();

            }else{

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>