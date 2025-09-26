<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {
        byte Accumulate = CurrentZeroToTwentyAmp.GetHystFourthByte(Model);
%>

<hr style="color: lightgray; opacity: 0.4;" />
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accuOff22" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accuOn22" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggle3()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="AccumChk" id="AccumChk1" <%= Accumulate == 1 ? "checked" : "" %>>
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;">
            <%: Html.TextBox("Accum", Accumulate, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        </div>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">
    let off1 = document.getElementById("accuOff22");
    let on1 = document.getElementById("accuOn22");
    let accuToggle22 = document.getElementById("AccumChk1");

    $('#AccumChk1').change(function () {
        if ($('#AccumChk1').prop('checked')) {
            $('#Accum').val(1);
        } else {
            $('#Accum').val(0);
        }
    });
    function onOffToggle3() {
        if (accuToggle22.checked == true) {
            off1.style.display = "none";
            on1.style.display = "block";
        } else {
            on1.style.display = "none";
            off1.style.display = "block";
        }
        /* console.log("value", accuToggle22.checked)*/
    };
    onOffToggle3()
</script>
<%}%> 