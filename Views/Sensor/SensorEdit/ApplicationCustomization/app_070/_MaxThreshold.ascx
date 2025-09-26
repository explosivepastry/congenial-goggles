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

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maximum","Maximum")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number"<%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
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
        createSpinnerModal("maxThreshNum", '<%: label %>', "MaximumThreshold_Manual", arrayForSpinner1);

        <%}%>
        $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < minVal)
                    $("#MaximumThreshold_Manual").val(minVal);
                if ($("#MaximumThreshold_Manual").val() > maxVal)
                    $("#MaximumThreshold_Manual").val(maxVal);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
                //$("#MinimumThreshold_Manual").change();

            }else{

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>