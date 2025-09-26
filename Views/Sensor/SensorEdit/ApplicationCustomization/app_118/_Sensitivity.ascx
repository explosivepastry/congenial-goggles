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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled"  %> name="SensitivityThreshold_Manual" id="SensitivityThreshold_Manual" value="<%=(Model.Calibration1) %>" />
        <a  id="sensivityNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_118|Stop Timer","Stop Timer")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control"  <%=Model.CanUpdate ? "" : "disabled"  %> name="StopTimer_Manual" id="StopTimer_Manual" value="<%= (Model.Calibration2) %>" />
        <a  id="stopTimerNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>


<script type="text/javascript">

    $(function () {
           <% if (Model.CanUpdate)
             { %>
        const arrayForSpinner = arrayBuilder(0, 10000, 100);
        createSpinnerModal("sensivityNum", "Sensitivity", "SensitivityThreshold_Manual", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(0, 120, 10);
        createSpinnerModal("stopTimerNum", "Seconds", "StopTimer_Manual", arrayForSpinner1);
      <%}%>
   

    $("#SensitivityThreshold_Manual").change(function () {
        if (isANumber($("#SensitivityThreshold_Manual").val())) {
            if ($("#SensitivityThreshold_Manual").val() < 0)
                $("#SensitivityThreshold_Manual").val(0);
            if ($("#SensitivityThreshold_Manual").val() > 10000)
                $("#SensitivityThreshold_Manual").val(10000);
        }
        else {
            $('#SensitivityThreshold_Manual').val(<%:(Model.Calibration1)%>);
        }
    });

    $("#StopTimer_Manual").change(function () {
        if (isANumber($("#StopTimer_Manual").val())) {
            if ($("#StopTimer_Manual").val() < 0)
                $("#StopTimer_Manual").val(0);
            if ($("#StopTimer_Manual").val() > 120)
                $("#StopTimer_Manual").val(120);
        }
        else {
            $('#StopTimer_Manual').val(<%:(Model.Calibration2)%>);
        }
    });
    });
</script>
<%} %>