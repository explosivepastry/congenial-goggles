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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enter Aware Threshold","Enter Aware Threshold")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />

        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var MaxThresMinVal = 0;
        var MaxThresMaxVal = 50000;

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner1 = arrayBuilder(0, 50000, 10);
        createSpinnerModal("maxThreshNum", "Max Threshold", "MaximumThreshold_Manual", arrayForSpinner1);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < MaxThresMinVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMinVal);
                if ($("#MaximumThreshold_Manual").val() > MaxThresMaxVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMaxVal);

                if (Number($("#MaximumThreshold_Manual").val()) < Number($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val($("#MinimumThreshold_Manual").val());
                //$("#MinimumThreshold_Manual").change();

            }else{

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>