<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>

<hr style="color: lightgray; opacity: 0.4;" />
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 111">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accumOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accumOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="AccumChk" id="AccumChk" <%= CurrentZeroToTwentyAmp.GetHystFourthByte(Model) == 1 ? "checked" : "" %> onclick="accumToggle()">
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.TextBox("Accum",CurrentZeroToTwentyAmp.GetHystFourthByte(Model), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
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
    function accumToggle() {
        if ($('#AccumChk').is(':checked')) {
            $('#accumOff').hide();
        } else {
            $('#accumOff').show();
        }
        if ($('#AccumChk').is(':checked') === false) {
            $('#accumOn').hide();
        } else {
            $('#accumOn').show();
        }
    }
    accumToggle()
</script>
<%}%> 