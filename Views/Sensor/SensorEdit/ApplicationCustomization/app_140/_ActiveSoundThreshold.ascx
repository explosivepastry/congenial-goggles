<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
   double ActiveSoundThreshold =  SootBlower2.GetActiveSoundThreshold(Model);
%>

<div class="row sensorEditForm">
  <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|140-Sound-Threshold-Title","Active Mode Sound Threshold")%> (<%: Html.Label("dB") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveSoundThreshold" id="ActiveSoundThreshold" value="<%=ActiveSoundThreshold %>" />
        <a  id="soundNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    $('#ActiveSoundThreshold').addClass("editField editFieldMedium");

    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(60, 130, 5);
        createSpinnerModal("soundNum", "dB", "ActiveSoundThreshold", arrayForSpinner);

      <%}%>

        $('#ActiveSoundThreshold').change(function () {
            if (isANumber($("#ActiveSoundThreshold").val())) {
                if ($('#ActiveSoundThreshold').val() < 60)
                    $('#ActiveSoundThreshold').val(60);

                if ($('#ActiveSoundThreshold').val() > 130)
                    $('#ActiveSoundThreshold').val(130);
            }
            else {
                $('#ActiveSoundThreshold').val(<%: ActiveSoundThreshold%>);

            }
        });

  
    });
</script>
