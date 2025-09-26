<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
    
%>

<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<%----MIN Threshold----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> %
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%----Max Threshold----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> %
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

    // Min
    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        let arrayForSpinner = arrayBuilder(0, 100, 1);
        createSpinnerModal("minThreshNum", "Percent", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < 0)
                    $("#MinimumThreshold_Manual").val(0);
                if ($("#MinimumThreshold_Manual").val() > 100)
                    $("#MinimumThreshold_Manual").val(100);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
            }
            else {
                $('#MinimumThreshold_Manual').val(<%:  (Min)%>);
        }
         });
    });
    // Max
    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        let arrayForSpinner = arrayBuilder(0, 100, 1);
        createSpinnerModal("maxThreshNum", "Percent", "MaximumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < 0)
                    $("#MaximumThreshold_Manual").val(0);
                if ($("#MaximumThreshold_Manual").val() > 100)
                    $("#MaximumThreshold_Manual").val(100);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
            }
            else {
                $('#MaximumThreshold_Manual').val(<%:  (Max)%>);
            }
        });


    });
</script>
<%} %>