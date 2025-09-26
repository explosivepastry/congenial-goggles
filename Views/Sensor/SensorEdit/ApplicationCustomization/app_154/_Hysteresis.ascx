<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

		string Hyst = ResistiveBridgeMeter.GetHysteresis(Model).ToString();

        
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Aware State Buffer","Aware State Buffer")%> (<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
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
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(0, 100, 1);
        createSpinnerModal("hystNum", '<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>', "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 100)
                    $("#Hysteresis_Manual").val(100)

            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

        }


        });
    });
</script>
<%} %>