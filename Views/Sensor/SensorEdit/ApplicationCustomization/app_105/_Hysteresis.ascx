<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        string unitsforhyst = UltrasonicRangerIndustrial.GetUnits(Model.SensorID).ToString();
        double minhyst = 0;
        double maxhyst = 0;
        double step = 1;
        string abbrv = UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(Model.SensorID));

        if (unitsforhyst == "Inch"||unitsforhyst == "Yard"||unitsforhyst == "Feet")
        {
            minhyst = 0;
            maxhyst = 3.9;
            step = .1;
        }

        if (unitsforhyst == "Centimeter" || unitsforhyst == "Meter")
        {
            minhyst = 0;
            maxhyst = 10;
            step = 1;
        }   
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp (<%: unitsforhyst%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(!Model.IsDirty) { %>

        const arrayForSpinner = arrayBuilderForDecimals(<%: minhyst%>, <%: maxhyst%>, <%: step%>);
        createSpinnerModal("hystNum","<%: unitsforhyst %>", "Hysteresis_Manual", arrayForSpinner);
        <%}%>

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#Hysteresis_Manual").change(function () {
            if(isANumber($("#Hysteresis_Manual").val())){
                if ($("#Hysteresis_Manual").val() < <%:minhyst%>)
                $("#Hysteresis_Manual").val(<%:minhyst%>);
                if ($("#Hysteresis_Manual").val() > <%:maxhyst%>)
                    $("#Hysteresis_Manual").val(<%:maxhyst%>)
        }
        else
        {
            $("#Hysteresis_Manual").val(<%: Hyst%>);
        }
         });
    });
</script>
<%} %>