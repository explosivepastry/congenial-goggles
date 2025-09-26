<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Min = "";
        string Max = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
  
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Re-arm","Re-arm")%> (Seconds)
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
        var deltaInterval_array = [1, 2, 5, 10, 30, 60, 180, 300];
        var minInterval = 0.1;

               <% if (Model.CanUpdate)
                  { %>

        createSpinnerModal("maxThreshNum", "Seconds", "MaximumThreshold_Manual", deltaInterval_array);

        <%}%>

        $("#MaximumThreshold_Manual").addClass('editField editFieldMedium');

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < 0)
                    $("#MaximumThreshold_Manual").val(0);
                if ($("#MaximumThreshold_Manual").val() > 300)
                    $("#MaximumThreshold_Manual").val(300);
            }
            else {
                $('#MaximumThreshold_Manual').val(<%: Max%>);
        }
        });

    });
</script>
<%} %>