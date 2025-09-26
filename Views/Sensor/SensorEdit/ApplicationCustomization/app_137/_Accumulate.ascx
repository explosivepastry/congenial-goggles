<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int dataViewType = ThreePhase20AmpMeter.GetDataViewOption(Model.SensorID);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 888">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="ACUOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="ACUOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggleAccum()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Accumulate_ManualChkBx" id="Accumulate_ManualChkBx" <%=ThreePhase500AmpMeter.GetAccumulate(Model) > 0 ? "checked" : "" %>>
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.TextBox("Accumulate_Manual",ThreePhase20AmpMeter.GetAccumulate(Model), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
    <div>
        <!-- Add Help -->
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_109|Display Data","Display Data")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="DataView_Manual" class="form-select ms-0" name="DataView_Manual" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="1" <%= dataViewType == 1 ? "selected='selected'" : "" %>>Energy Usage</option>
            <option value="2" <%= dataViewType == 2 ? "selected='selected'" : "" %>>Energy Usage and Average</option>
            <option value="3" <%= dataViewType == 3 ? "selected='selected'" : "" %>>All Data</option>
        </select>
    </div>
    <div>
        <!-- Add Help -->
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        var DefaultYouSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
        var SureCounter = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset the counter on this sensor?","Are you sure you want to reset the counter on this sensor?")%>";
        var SureDefault = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";

        $(function () {
            var SensorID = <%: Model.SensorID%>;
        var returnUrl = $('#returns').val();
        var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();

        $('#ResetAccumulator').on("click", function () {
            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(SureCounter)) {
                $.post('/Overview/SensorResetCounter', { id: SensorID, url: returnUrl, acc: "5" }, function (result) {
                    pID.html(result);
                });
            }
        });
    });

          <% if (Model.CanUpdate)
    { %>
        //$('#DataView_Manual').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 200,
        //    headerText: 'Display Data',
        //});

        <%}%>


        //setTimeout("$('#Accumulate_ManualChkBx').iButton();", 500);
        $('#Accumulate_ManualChkBx').change(function () {
            if ($('#Accumulate_ManualChkBx').prop('checked')) {
                $('#Accumulate_Manual').val(1);
            } else {
                $('#Accumulate_Manual').val(0);
            }
        });

    });
    let AccumOff = document.getElementById("ACUOff");
    let AccumOn = document.getElementById("ACUOn");
    let accuTog = document.getElementById("Accumulate_ManualChkBx");

    function onOffToggleAccum() {
        if (accuTog.checked == true) {
            AccumOff.style.display = "none";
            AccumOn.style.display = "block";
        } else {
            AccumOn.style.display = "none";
            AccumOff.style.display = "block";
        }
        /* console.log("value", accuToggle22.checked)*/
    };
    onOffToggleAccum()
</script>
