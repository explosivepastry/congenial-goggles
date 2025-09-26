<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>



<%        double intervalVal = (Model.Calibration3 / 1000d);
    if (new Version(Model.FirmwareVersion) >= new Version("14.26.17.10"))
    {
        intervalVal = Model.Calibration3;
    }
%>


<div class="row sensorEditForm" style="display: none">
    <div class="col-12 col-md-3">
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBox("Offset_Hidden", unchecked((sbyte)(CurrentZeroToOneFiftyAmp.GetHystThirdByte(Model))) / 100d, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Averaging Interval","Averaging Interval")%> (<%: Html.TranslateTag("Seconds","Seconds")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="avgInterval" id="avgInterval" value="<%=intervalVal%>" />
        <a id="avgIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accuOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accuOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggle3()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="AccumChk" id="AccumChk" <%= CurrentZeroToOneFiftyAmp.GetHystFourthByte(Model) == 1 ? "checked" : "" %>>
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.Hidden("Accum",CurrentZeroToOneFiftyAmp.GetHystFourthByte(Model))%></div>
    </div>
</div>

<hr style="color: lightgray; opacity: 0.4;" />

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="FDVOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="FDVOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggle3()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="fullChk" id="fullChk" <%= CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID) ? "checked" : "" %>>
        </div>
        <div style="display: none;"><%: Html.Hidden("FullNotiString",CurrentZeroToOneFiftyAmp.GetShowFullDataValue(Model.SensorID))%></div>
    </div>
</div>

<script type="text/javascript">

    $('#AccumChk').change(function () {
        if ($('#AccumChk').prop('checked')) {
            $('#Accum').val(1);
        } else {
            $('#Accum').val(0);
        }
    });

    $('#fullChk').change(function () {
        if ($('#fullChk').prop('checked')) {
            $('#FullNotiString').val(1);
        } else {
            $('#FullNotiString').val(0);
        }
    });
    let offAcuu = document.getElementById("accuOff");
    let onAcuu = document.getElementById("accuOn");
    let offData = document.getElementById("FDVOff");
    let onData = document.getElementById("FDVOn");
    let accuToggle = document.getElementById("AccumChk");
    let dataToggle = document.getElementById("fullChk");

    function onOffToggle3() {
        if (accuToggle.checked == true) {
            offAcuu.style.display = "none";
            onAcuu.style.display = "block";
        } else {
            onAcuu.style.display = "none";
            offAcuu.style.display = "block";
        }
        if (dataToggle.checked == true) {
            offData.style.display = "none";
            onData.style.display = "block";
        } else {
            onData.style.display = "none";
            offData.style.display = "block";
        }
        /* console.log("value", accuToggle22.checked)*/
    };
    onOffToggle3()
</script>



<script type="text/javascript">


    $(document).ready(function () {



        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 30, 1);
        createSpinnerModal("avgIntNum", "Seconds", "avgInterval", arrayForSpinner);


        $("#avgInterval").change(function () {
            if (isANumber($("#avgInterval").val())) {
                if ($("#avgInterval").val() < 1)
                    $("#avgInterval").val(1);
                if ($("#avgInterval").val() > 30)
                    $("#avgInterval").val(30)

            }
            else {
                $('#avgInterval').val(<%: intervalVal%>);

            }


        });

        $('#Offset_Hidden').change(function () {
            if (isANumber($("#Offset_Hidden").val())) {
                if ($('#Offset_Hidden').val() < -1.27)
                    $('#Offset_Hidden').val(-1.27);

                if ($('#Offset_Hidden').val() > 1.27)
                    $('#Offset_Hidden').val(1.27);
            }
            else {
                $('#Offset_Hidden').val(<%: CurrentZeroToOneFiftyAmp.GetHystThirdByte(Model) / 100d%>);
            }
        });

        <%}%>
    });

</script>


