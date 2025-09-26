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
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>  (Seconds)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var deltaInterval_array = [1, 2, 5, 10, 30, 60, 180, 300];
        var minInterval = 0.1;

               <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("minThreshNum", "Seconds", "MinimumThreshold_Manual", deltaInterval_array);

        <%}%>

        $("#MinimumThreshold_Manual").addClass('editField editFieldMedium');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < 0)
                    $("#MinimumThreshold_Manual").val(0);
                if ($("#MinimumThreshold_Manual").val() > 300)
                    $("#MinimumThreshold_Manual").val(300);
            }
            else {
                $('#MinimumThreshold_Manual').val(<%: Min%>);
            }
        });
    });
</script>
<%} %>