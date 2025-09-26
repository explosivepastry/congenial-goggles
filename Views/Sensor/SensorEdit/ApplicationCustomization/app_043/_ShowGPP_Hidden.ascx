<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--Show GPP--%>

 <%  bool showGPP = ViewData["ShowGPP"].ToBool();
            if (ViewData["ShowGPP"] == null)
            {
                showGPP = HumiditySHT25.ShowGPP(Model.SensorID);
            }
        %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Show Moisture Weight","Show Moisture Weight")%> 
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Hide","Hide")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> type="checkbox" name="showGPP" id="showGPP" <%= showGPP ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Show","Show")%></label>
        </div>                               
    </div>
</div>

<div class="row sensorEditForm" id="trAltitude" <%if (!showGPP) Response.Write("style='display:none'"); %>>
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