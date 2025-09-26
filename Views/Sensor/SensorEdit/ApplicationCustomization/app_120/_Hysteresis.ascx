<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        string Hyst = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        
        if (Model.Hysteresis == 4294967295)
            Model.Hysteresis = 0;  
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_120|Aware State Buffer","Aware State Buffer")%> (<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Amps","Amps")%>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script type="text/javascript">

    $(function () {

        var maxhyst = (($('#MaximumThreshold_Manual').val() - $("#MinimumThreshold_Manual").val()) * .25);

          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(1, maxhyst, 1);
        if (arrayForSpinner.length === 0)
            arrayForSpinner = arrayBuilder(1, 10, 1);

        createSpinnerModal("hystNum", "Amps", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > maxhyst)
                    $("#Hysteresis_Manual").val(maxhyst)

            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

        }


        });
    });
</script>
