<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%: ZeroToTwentyMilliamp.GetLabel(Model.SensorID)  %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(Model.CanUpdate) { %>


        let arrayForSpinner = arrayBuilder(0, 20, 1);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 20)
                    $("#Hysteresis_Manual").val(20)

            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

        }


        });
    });
</script>
<%} %>