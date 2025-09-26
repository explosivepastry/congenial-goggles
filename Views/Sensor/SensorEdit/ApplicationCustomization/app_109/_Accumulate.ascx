<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int dataViewType = ThreePhaseCurrentMeter.GetDataViewOption(Model.SensorID);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 666">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accumOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="accumOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Accumulate_ManualChkBx" id="Accumulate_ManualChkBx" <%=ThreePhaseCurrentMeter.GetAccumulate(Model) > 0 ? "checked" : "" %> onclick="accumToggle()">
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.TextBox("Accumulate_Manual",ThreePhaseCurrentMeter.GetAccumulate(Model), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
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
        <select class="ms-0 form-select" name="DataView_Manual" <%=Model.CanUpdate ? "" : "disabled" %>>
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

        //setTimeout("$('#Accumulate_ManualChkBx').iButton();", 500);
        $('#Accumulate_ManualChkBx').change(function () {
            if ($('#Accumulate_ManualChkBx').prop('checked')) {
                $('#Accumulate_Manual').val(1);
            } else {
                $('#Accumulate_Manual').val(0);
            }
        });

    });

    function accumToggle() {
        if (document.getElementById("Accumulate_ManualChkBx").checked) {
            document.getElementById("accumOff").style.display = "none";
        } else {
            document.getElementById("accumOff").style.display = "block";
        }
        if (document.getElementById("Accumulate_ManualChkBx").checked === false) {
            document.getElementById("accumOn").style.display = "none";
        } else {
            document.getElementById("accumOn").style.display = "block";
        }
    }
    accumToggle()


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

</script>
