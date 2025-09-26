<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string   pressureHyst =  SootBlower.MaxThreshForUI(Model);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Pressure Delta","Active Mode Pressure Delta")%> (<%: Html.Label("PSI") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=pressureHyst %>" />
        <a  id="pressureNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12">
    </div>
</div>

<script type="text/javascript">
    $('#MaximumThreshold_Manual').addClass("editField editFieldMedium");

    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(1, 50, 1);
        createSpinnerModal("pressureNum", "PSI", "MaximumThreshold_Manual", assessment_array);
      <%}%>

        $('#MaximumThreshold_Manual').change(function ()
        {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($('#MaximumThreshold_Manual').val() < 1)
                    $('#MaximumThreshold_Manual').val(1);

                if($('#MaximumThreshold_Manual').val() > 50)
                    $('#MaximumThreshold_Manual').val(50); 
            }
            else
            {
                $('#MaximumThreshold_Manual').val(<%: pressureHyst%>); 
        }
        });
    });
</script>
