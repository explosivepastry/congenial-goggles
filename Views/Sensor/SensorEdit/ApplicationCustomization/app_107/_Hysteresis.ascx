<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Hyst = "";
        string showVal = "";
        string DLR = "";

        Hyst = LightSensor.HystForUI(Model);
        showVal = LightSensor.GetShowDataValue(Model.SensorID).ToString();
        DLR = LightSensor.DeltaLightReportForUI(Model); 
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (Lux)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_107|Delta Light Report","Delta Light Report")%> (Lux)
    </div>
    <div class="col sensorEditFormInput" id="Div1">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="DeltaLightReport_Manual" id="DeltaLightReport_Manual" value="<%=(DLR)%>" />
        <a id="dlrNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<%--data display--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_107|Data Display","Data Display")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="FullNotiString" name="FullNotiString" class="form-select ms-0" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option <%: showVal == "0"? "selected":"" %> value="0">Lux</option>
            <option <%: showVal == "1"? "selected":"" %> value="1">Lux and Light State</option>
            <option <%: showVal == "2"? "selected":"" %> value="2">Light State</option>
        </select>
    </div>
</div>

<script type="text/javascript">
    $("#Hysteresis_Manual").addClass('editField editFieldSmall');
    $("#DeltaLightReport_Manual").addClass('editField editFieldSmall');

    $(function () {
          <% if (Model.CanUpdate)
             { %>

        const arrayForSpinner = arrayBuilder(0, 100000, 100);
        createSpinnerModal("hystNum", "Lux", "Hysteresis_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 80000, 100);
        createSpinnerModal("dlrNum", "Lux", "DeltaLightReport_Manual", arrayForSpinner1);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 10000)
                    $("#Hysteresis_Manual").val(10000)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });

        $("#DeltaLightReport_Manual").change(function () {
            if (isANumber($("#DeltaLightReport_Manual").val())) {
                if ($("#DeltaLightReport_Manual").val() < 0)
                    $("#DeltaLightReport_Manual").val(0);
                if ($("#DeltaLightReport_Manual").val() > 80000)
                    $("#DeltaLightReport_Manual").val(80000)
            }
            else {
                $('#DeltaLightReport_Manual').val(<%: DLR%>);
            }
        });
    });
</script>
<%} %>