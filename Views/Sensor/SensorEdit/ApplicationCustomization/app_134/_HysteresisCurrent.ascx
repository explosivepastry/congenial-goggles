<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string currentHyst = SootBlower.MinThreshForUI(Model);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Current Delta","Active Mode Current Delta")%> (<%: Html.Label("Amps") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=currentHyst %>" />
        <a id="currentNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12">
    </div>
</div>

<script type="text/javascript">
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    $(function () {

          <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(1, 5, 1);
        createSpinnerModal("currentNum", "Amps", "MinimumThreshold_Manual", assessment_array);

      <%}%>

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($('#MinimumThreshold_Manual').val() < 0.1)
                    $('#MinimumThreshold_Manual').val(0.1);

                if ($('#MinimumThreshold_Manual').val() > 5)
                    $('#MinimumThreshold_Manual').val(5);
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: currentHyst%>);
            }
        });
    });
</script>
