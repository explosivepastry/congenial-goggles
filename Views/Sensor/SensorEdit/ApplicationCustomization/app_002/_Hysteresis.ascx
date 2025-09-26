<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isF = Temperature.IsFahrenheit(Model.SensorID);
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>



<script type="text/javascript">


    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("hystNum", 'Aware State Buffer ' + '(<%= isF ? "°F" : "°C"%>)', "Hysteresis_Manual", [0, 1, 2, 3, 4, 5]);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 5)
                    $("#Hysteresis_Manual").val(5)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>

<%} %>