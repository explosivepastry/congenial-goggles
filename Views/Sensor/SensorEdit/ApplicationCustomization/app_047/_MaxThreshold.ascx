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
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(1, 65535, 50);
        createSpinnerModal("maxThreshNum", "Pulses", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $('#MaximumThreshold_Manual').addClass('editField editFieldMedium')

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < 1)
                    $("#MaximumThreshold_Manual").val(1);
                if ($("#MaximumThreshold_Manual").val() > 65535)
                    $("#MaximumThreshold_Manual").val(65535);

                if ($("#MaximumThreshold_Manual").val() < $("#MinimumThreshold_Manual").val())
                    $("#MaximumThreshold_Manual").val($("#MinimumThreshold_Manual").val());
                $("#MinimumThreshold_Manual").change();

            }else{

                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
<%} %>