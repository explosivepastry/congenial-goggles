<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

 <%  bool showGPP = ViewData["ShowGPP"].ToBool();
            if (ViewData["ShowGPP"] == null)
            {
                showGPP = HumiditySHT25.ShowGPP(Model.SensorID);
            }
        %>
<p class="useAwareState">Display</p>
<div class="row sensorEditForm" id="trAltitude" 
    <%if (!showGPP) Response.Write("style='display:none'"); %>>
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Altitude","Altitude")%> 
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Altitude" id="Altitude" value="<%:HumiditySHT25.GetAltitude(Model.SensorID) %>" />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|ft","ft")%> 
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $("#showGPP").change(function () {
            if ($("#showGPP").prop("checked")) {
                $('#trAltitude').show();
            }
            else {
                $('#trAltitude').hide();
            }
        });
    });

</script>