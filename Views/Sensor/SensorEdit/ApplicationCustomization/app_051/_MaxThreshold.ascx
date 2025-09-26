<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Max = Seat.MaxThreshForUI(Model);
    string label = "";
    MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> &nbsp <%: label %>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <%: label %>
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {

     <%if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 10, 1);
        createSpinnerModal("maxThreshNum", "Above", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < 1)
                    $("#MaximumThreshold_Manual").val(1);
                if ($("#MaximumThreshold_Manual").val() > 10)
                    $("#MaximumThreshold_Manual").val(10);
            } else {
                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
