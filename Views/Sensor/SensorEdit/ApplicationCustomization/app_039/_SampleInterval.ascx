<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  bool isMPH = true;
    double MaxRange;
    double MinRange;
    double MaxSpeedMax;
    double MinSpeedMax;

    if (ViewData["SpeedScale"] != null)
    {
        isMPH = ViewData["SpeedScale"].ToStringSafe() == "M";
        if (isMPH)
        {

            MinRange = .01;
            MaxRange = 80.0;
            MaxSpeedMax = VehicleDetector.getMaxMPH(Model);
            MinSpeedMax = VehicleDetector.getMinMPH(Model);
        }
        else
        {
            MinRange = .01;
            MaxRange = 140.0;
            MaxSpeedMax = VehicleDetector.GetMaxKPH(Model);
            MinSpeedMax = VehicleDetector.GetMinKPH(Model);
        }
    }
    else
    {
        isMPH = VehicleDetector.IsMPH(Model.SensorID);
        if (isMPH)
        {

            MinRange = .01;
            MaxRange = 80.0;
            MaxSpeedMax = VehicleDetector.getMaxMPH(Model);
            MinSpeedMax = VehicleDetector.getMinMPH(Model);
        }
        else
        {
            MinRange = .01;
            MaxRange = 140.0;
            MaxSpeedMax = VehicleDetector.GetMaxKPH(Model);
            MinSpeedMax = VehicleDetector.GetMinKPH(Model);
        }
    }
%>
<h4>Responsiveness</h4>

<%--- MPH/KPH ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_039|Display as","Display as")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("KPH","KPH")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="SpeedScale" id="SpeedScale" <%if (isMPH) Response.Write("checked='checked'"); %>>
            <label class="form-check-label"><%: Html.TranslateTag("MPH","MPH")%></label>
        </div>
    </div>
</div>


<%--- Max Speed ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_039|Max Speed","Max Speed")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxSpeed_Manual" id="MaxSpeed_Manual" value="<%=MaxSpeedMax.ToString("0.00")%>" />
        <a id="maxSpeedNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<%--- Enable Min Speed ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_039|Enable Min Speed","Enable Min Speed")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="IsZeroing" id="IsZeroing" <%if (VehicleDetector.IsActiveZeroing(Model)) Response.Write("checked='checked' "); if (!Model.CanUpdate) Response.Write("disabled='disabled'"); %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
    </div>
</div>


<%--- Min Speed ---%>
<div class="row sensorEditForm minSpeedContainer">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_039|Min Speed","Min Speed")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control"  <%=Model.CanUpdate ? "" : "disabled" %> name="MinSpeed_Manual" id="MinSpeed_Manual" value="<%=MinSpeedMax.ToString("0.00")%>" />
        <a id="minSpeedNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>



<script type="text/javascript">


    // Show/Hide Min Speed
    $(document).ready(function(){
        if($('#IsZeroing').is(':checked')){
            $('.minSpeedContainer').show();
        }else{
            $('.minSpeedContainer').hide();
        }

        $('#IsZeroing').change(function(){            
            if($('#IsZeroing').is(':checked')){
                $('.minSpeedContainer').show();
            }else{
                $('.minSpeedContainer').hide();
            }  
        });
    });

    //Max Speed
    $(function () {
          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner1 = arrayBuilder(0, 80, 1);
        createSpinnerModal("maxSpeedNum", "Max Speed", "MaxSpeed_Manual", arrayForSpinner1);

        <%}%>
            $("#MaxSpeed_Manual").addClass("editField editFieldMedium");

            $("#MaxSpeed_Manual").change(function () {
                if (isANumber($("#MaxSpeed_Manual").val())) {
                    if ($("#MaxSpeed_Manual").val() < .01)
                        $("#MaxSpeed_Manual").val(.01);
                    if ($("#MaxSpeed_Manual").val() > <%:MaxRange%>)
                $("#MaxSpeed_Manual").val(<%:MaxRange%>);
                }
                else
                {
                    $("#MaxSpeed_Manual").val(<%: MaxSpeedMax.ToString("0.00")%>);
                }
            });


            //Min Speed

          <% if (Model.CanUpdate)
             { %>

        let arrayForSpinner = arrayBuilder(0, 80, 1);
        createSpinnerModal("minSpeedNum", "Minimum Speed", "MinSpeed_Manual", arrayForSpinner);

        <%}%>
            $("#MinSpeed_Manual").addClass("editField editFieldMedium");

            $("#MinSpeed_Manual").change(function () {
                if (isANumber($("#MinSpeed_Manual").val())) {
                    if ($("#MinSpeed_Manual").val() < .01)
                        $("#MinSpeed_Manual").val(.01);
                    if ($("#MinSpeed_Manual").val() > <%:MaxRange%>)
                    $("#MinSpeed_Manual").val(<%:MaxRange%>);
                }
                else
                {
                    $("#MinSpeed_Manual").val(<%: MinSpeedMax.ToString("0.00")%>);
                }
            });
        });
</script>
