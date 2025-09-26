<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    
    string HarmonicMin = "";
    string HarmonicMax = "";
    string HarmonicPeakAware = "";
    string HarmonicRMSAware = "";

    HarmonicMin = Vibration800.GetHarmonicMin(Model).ToString();
    HarmonicMax = Vibration800.GetHarmonicMax(Model).ToString();
    HarmonicPeakAware = Vibration800.GetHarmonicPeakAware(Model).ToString();
    HarmonicRMSAware = Vibration800.GetHarmonicRMSAware(Model).ToString();
    
    bool FullNotiString = Vibration800.GetShowFullDataValue(Model.SensorID);    
       
%>

<h5>Harmonic Analysis</h5>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Minimum Threshold","Minimum Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="harmMinThreshold_Manual" id="harmMinThreshold_Manual" value="<%=HarmonicMin %>" />
        <a id="harmMinNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Maximum Threshold","Maximum Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="harmMaxThreshold_Manual" id="harmMaxThreshold_Manual" value="<%=HarmonicMax %>" />
        <a id="harmMaxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3 111">
       <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="FDOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="FDOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggle3()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= Vibration800.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %>>
        </div>
          <div style="display: none;"><%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<div class="clearfix"></div>
<h5></h5>

<script>
    let off1 = document.getElementById("FDOff");
    let on1 = document.getElementById("FDOn");
    let accuToggle22 = document.getElementById("fullChk");

    var MinVal = <%=HarmonicMin%>;
    var MaxVal = <%=HarmonicMax%>;

    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        const arrayForSpinnerThres = arrayBuilder(50, 6000, 10);
        createSpinnerModal("harmMinNum", "Minimum Threshold", "harmMinThreshold_Manual", arrayForSpinnerThres);
        createSpinnerModal("harmMaxNum", "Maximum Threshold", "harmMaxThreshold_Manual", arrayForSpinnerThres);
         <%}%>

        $("#harmMinThreshold_Manual").change(function () {
            if (isANumber($("#harmMinThreshold_Manual").val())){
                if ($("#harmMinThreshold_Manual").val() < 50)
                    $("#harmMinThreshold_Manual").val(50);
                if ($("#harmMinThreshold_Manual").val() > 6000)
                    $("#harmMinThreshold_Manual").val(6000);

                if (parseFloat($("#harmMinThreshold_Manual").val()) > parseFloat($("#harmMaxThreshold_Manual").val()))
                    $("#harmMinThreshold_Manual").val(parseFloat($("#harmMaxThreshold_Manual").val()));
             }else{

                $("#harmMinThreshold_Manual").val(MinVal);
             }
         });

        $("#harmMaxThreshold_Manual").change(function () {
            if (isANumber($("#harmMaxThreshold_Manual").val())){
                if ($("#harmMaxThreshold_Manual").val() < 50)
                    $("#harmMaxThreshold_Manual").val(50);
                if ($("#harmMaxThreshold_Manual").val() > 6000)
                    $("#harmMaxThreshold_Manual").val(6000);

                if (parseFloat($("#harmMaxThreshold_Manual").val()) < parseFloat($("#harmMinThreshold_Manual").val()))
                    $("#harmMaxThreshold_Manual").val(parseFloat($("#harmMinThreshold_Manual").val()));

            }else{

                $("#MaximumThreshold_Manual").val(MaxVal);
            }
        });


        $('#fullChk').change(function () {
            if ($('#fullChk').prop('checked')) {
                $('#FullNotiString').val(1);
            } else {
                $('#FullNotiString').val(0);
            }
        });

    });


    function onOffToggle3() {
        if (accuToggle22.checked == true) {
            off1.style.display = "none";
            on1.style.display = "block";
        } else {
            on1.style.display = "none";
            off1.style.display = "block";
        }
    };
    onOffToggle3()

</script>