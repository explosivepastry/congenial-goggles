<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (new Version(Model.FirmwareVersion) >= new Version("2.3.0.0"))
    {

        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
         
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensitivity Buffer","Sensitivity Buffer")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script type="text/javascript">

    //Sensitivity Buffer
    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner = arrayBuilder(0, 50, 5);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>
        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 50)
                    $("#Hysteresis_Manual").val(50)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>
<%} %>