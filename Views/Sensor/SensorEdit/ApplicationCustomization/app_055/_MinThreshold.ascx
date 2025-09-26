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
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%> &nbsp <%: label %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    $(function () {

                <% if (Model.CanUpdate)
    { %>

        let arrayForSpinnerMin = arrayBuilder(0, 200, 10);
        createSpinnerModal("minThreshNum", "Minimum Threshold", "MinimumThreshold_Manual", arrayForSpinnerMin);

        <%}%>

        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {

                if ($("#MinimumThreshold_Manual").val() < 0) {
                    $("#MinimumThreshold_Manual").val(0);
                }

                if ($("#MinimumThreshold_Manual").val() > 200) {
                    $("#MinimumThreshold_Manual").val(200);
                }


                if (Number($("#MinimumThreshold_Manual").val()) > Number($("#MaximumThreshold_Manual").val())) {
                    $("#MinimumThreshold_Manual").val($("#MaximumThreshold_Manual").val());
                }

            } else {
                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
<%} %>