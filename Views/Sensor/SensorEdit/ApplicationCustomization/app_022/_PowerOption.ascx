<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%
    bool viewable = ZeroToTwentyMilliamp.ShowAdvCal(Model.SensorID);

      if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                {

%>
<hr style="color:lightgray; opacity: 0.4;"/>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|View Power Options","View Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="vpoOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="vpoOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" <%=Model.CanUpdate ? "" : "disabled" %> type="checkbox" name="showAdvCal" id="showAdvCal" <%= viewable ? "checked='checked'" : ""  %> onclick="viewPowerToggle()">
        </div>
    </div>
</div>


<div class="row sensorEditForm viewPower" style="display: none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Power Options","Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (Model.GenerationType.ToUpper().Contains("GEN2"))
            {%>

        <select name="power" id="power" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
        </select>
        <%}
            else
            { %>
        <select name="power" id="Select1" class="form-select ms-0" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
            <option value="3" <%=Model.Calibration3 == 3 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Amplifier SP9 Active Low","Amplifier SP9 Active Low")%></option>
        </select>
        <%}%>
    </div>
</div>

<div class="row sensorEditForm viewPower" style="display: none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Delay in Milliseconds","Delay in Milliseconds")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="delay" id="delay" class="form-control" value="<%=Model.Calibration4 %>" />
        <a id="delayNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>




<script type="text/javascript">

    $(document).ready(function () {

        if ($('#showAdvCal').is(":checked")) {
            $('.viewPower').show();
        }
        else {
            $('.viewPower').hide();
        }

        $('#showAdvCal').change(function () {
            if ($(this).is(":checked")) {
                $('.viewPower').show();
            }
            else {
                $('.viewPower').hide();
            }

        });

               <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 3000, 1);
        createSpinnerModal("delayNum", "Milliseconds", "delay", arrayForSpinner);

        $("#delay").change(function () {
            if (isANumber($("#delay").val())) {
                if ($("#delay").val() < 0)
                    $("#delay").val(0);
                if ($("#delay").val() > 30000)
                    $("#delay").val(30000)

            }
            else {
                $('#delay').val(0);

            }
        });
        <%}%>
    });

    function viewPowerToggle() {
        if ($('#showAdvCal').is(':checked')) {
            $('#vpoOff').hide();
        } else {
            $('#vpoOff').show();
        }
        if ($('#showAdvCal').is(':checked') === false) {
            $('#vpoOn').hide();
        } else {
            $('#vpoOn').show();
        }
    }
    viewPowerToggle()
</script>

<%}%>
