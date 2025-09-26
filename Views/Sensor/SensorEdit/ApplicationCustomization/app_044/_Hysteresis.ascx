<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%              string Hyst = "";
                string Min = "";
                string Max = "";
                MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|SRX Time Out","SRX Time Out")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner1 = arrayBuilder(1, 30, 1);
        createSpinnerModal("hystNum", "SRX Time Out", "Hysteresis_Manual", arrayForSpinner1);

        <%}%>
        $("#Hysteresis_Manual").addClass('editField editFieldMedium');

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < .01)
                    $("#Hysteresis_Manual").val(.01);
                if ($("#Hysteresis_Manual").val() > 30)
                    $("#Hysteresis_Manual").val(30)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>