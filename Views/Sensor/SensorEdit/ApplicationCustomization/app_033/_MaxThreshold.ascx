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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_033|Detection Magnitude","Detection Magnitude")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.IsDirty ? "disabled='disabled'" : "" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_033|Sample Interval","Sample Interval")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="SampleInterval_Manual" class="form-control" id="SampleInterval_Manual" value="<%=(Model.Calibration1) %>" />
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<script>
    <% if (Model.CanUpdate)
                  { %>
    $("#MaximumThreshold_Manual").change(function () {
        if (!isANumber($("#MinimumThreshold_Manual").val()))
            $('#MaximumThreshold_Manual').val(<%:  (Max)%>);

    });

    <%}%>
</script>
<%} %>