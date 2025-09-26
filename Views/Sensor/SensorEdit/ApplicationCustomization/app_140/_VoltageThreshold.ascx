<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
   int VoltageThreshold =  SootBlower2.GetVoltageThreshold(Model);
%>

<div class="row sensorEditForm">
  <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|140-Voltage-Threshold-Title","Voltage Threshold")%> (<%: Html.Label("volts") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="VoltageThreshold" id="VoltageThreshold" value="<%=VoltageThreshold %>" />
        <a  id="voltageNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    $('#VoltageThreshold').addClass("editField editFieldMedium");

    $(function () {
          <% if(Model.CanUpdate) { %>


        const arrayForSpinner = arrayBuilder(0, 500, 5);
        createSpinnerModal("voltageNum", "volts", "VoltageThreshold", arrayForSpinner);

      <%}%>

        $('#VoltageThreshold').change(function () {
            if (isANumber($("#VoltageThreshold").val())) {
                if ($('#VoltageThreshold').val() < 0)
                    $('#VoltageThreshold').val(0);

                if ($('#VoltageThreshold').val() > 500)
                    $('#VoltageThreshold').val(500);
            }
            else {
                $('#VoltageThreshold').val(<%: VoltageThreshold%>);

           }
       });

  
    });
</script>
