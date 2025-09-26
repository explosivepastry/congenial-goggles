<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
   double ActivePressureDelta =  SootBlower2.GetActivePressureDelta(Model);
   ActivePressureDelta =  Math.Round(ActivePressureDelta); //Rounded to fulfill "don't show decimal in the UI"
%>

<div class="row sensorEditForm">
  <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Pressure Delta","Active Mode Pressure Delta")%> (<%: Html.Label("PSI") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="ActivePressureDelta" id="ActivePressureDelta" value="<%=ActivePressureDelta %>" />
        <a  id="pressureNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    $('#ActivePressureDelta').addClass("editField editFieldMedium");


    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(1, 50, 1);
        createSpinnerModal("pressureNum", "PSI", "ActivePressureDelta", arrayForSpinner);

      <%}%>

        $('#ActivePressureDelta').change(function () {
            if (isANumber($("#ActivePressureDelta").val())) {
                if ($('#ActivePressureDelta').val() < 1)
                    $('#ActivePressureDelta').val(1);

                if ($('#ActivePressureDelta').val() > 50)
                    $('#ActivePressureDelta').val(50);
            }
            else {
                $('#ActivePressureDelta').val(<%: ActivePressureDelta%>);

            }
        });

  
    });
</script>
