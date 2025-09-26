<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Hyst = "";
        Hyst = SoilMoisture.GetMoistureAwareBuffer(Model).ToString();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (<%: Html.Label(SoilMoisture.GetLabel(Model.SensorID)) %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MoistureAwareBuffer" id="MoistureAwareBuffer" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(0, 10, 1);
        createSpinnerModal("hystNum", " <%:SoilMoisture.GetLabel(Model.SensorID) %>", "MoistureAwareBuffer", arrayForSpinner);

        <%}%>

        $("#MoistureAwareBuffer").change(function () {
            if (isANumber($("#MoistureAwareBuffer").val())) {
                if ($("#MoistureAwareBuffer").val() < 0)
                    $("#MoistureAwareBuffer").val(0);
                if ($("#MoistureAwareBuffer").val() > 10)
                    $("#MoistureAwareBuffer").val(10)
            }
            else {
                $('#MoistureAwareBuffer').val(<%: Hyst%>);
        }
        });
    });
</script>
<%} %>