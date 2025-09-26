<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string hyst = Tilt.HystForUI(Model);
    string label = Html.TranslateTag("Sensor/ApplicationCustomization/default|Degrees","Degrees");
    
%>


<%----- HYST Concentration -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/default|Aware State Buffer","Aware State Buffer")%> (<%: label %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=hyst %>" />
        <a id="HystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script>

    $("#Hysteresis_Manual").addClass('editField editFieldSmall');

    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner1 = arrayBuilder(0, 5, 1);
        createSpinnerModal("HystNum", "<%=label%>", "Hysteresis_Manual", arrayForSpinner1);

        <%}%>

            $("#Hysteresis_Manual").change(function () {
                if (isANumber($("#Hysteresis_Manual").val())) {
                    if ($("#Hysteresis_Manual").val() < 0)
                        $("#Hysteresis_Manual").val(0);
                    if ($("#Hysteresis_Manual").val() > 5)
                        $("#Hysteresis_Manual").val(5);
                }
                else
                    $("#Hysteresis_Manual").val(<%: hyst%>);
        });
        });
</script>
