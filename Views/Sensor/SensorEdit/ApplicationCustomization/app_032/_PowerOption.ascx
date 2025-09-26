<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%
    bool viewable = ZeroToTwentyMilliamp.ShowAdvCal(Model.SensorID);

     if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                { %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|View Power Options","View Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="vpoNo" class="form-check-label"><%: Html.TranslateTag("No","No")%></label>
            <label id="vpoYes" class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
            <input class="form-check-input my-0 y-0 mx-2" <%=Model.CanUpdate ? "" : "disabled" %> type="checkbox" name="showAdvCal" id="showAdvCal" <%= viewable ? "checked='checked'" : ""  %> onclick="viewPowerToggle()">
        </div>
    </div>
</div>

<div class="row sensorEditForm viewPower" style="display:none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Power Options","Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (Model.GenerationType.ToUpper().Contains("GEN2"))
          {%>

        <select name="power" id="power" class="form-select" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
        </select>
        <%}
          else
          { %>
        <select style="margin:0" name="power" id="Select1" class="form-select" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
            <option value="3" <%=Model.Calibration3 == 3 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Amplifier SP9 Active Low","Amplifier SP9 Active Low")%></option>
        </select>
        <%}%>
    </div>
</div>

<div class="row sensorEditForm viewPower" style="display:none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Delay in milliseconds","Delay in milliseconds")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="delay" id="delay" class="form-control" value="<%=Model.Calibration4 %>" />
        <a id="delayNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    
    $(document).ready(function () {

        if ($('#showAdvCal').is(":checked"))
        {
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

        let arrayForSpinner = arrayBuilder(50, 5000, 50);
        createSpinnerModal("delayNum", "Milliseconds", "delay", arrayForSpinner);

        $("#delay").change(function () {
            if (isANumber($("#delay").val())) {
                if ($("#delay").val() < 50)
                    $("#delay").val(50);
                if ($("#delay").val() > 5000)
                    $("#delay").val(5000)

            }
            else {
                $('#delay').val(0);

            }

        });
        <%}%>


    });
    function viewPowerToggle() {
        if ($('#showAdvCal').is(':checked')) {
            $('#vpoNo').hide();
        } else {
            $('#vpoNo').show();
        }
        if ($('#showAdvCal').is(':checked') === false) {
            $('#vpoYes').hide();
        } else {
            $('#vpoYes').show();
        }
    }
    viewPowerToggle()

</script>

<%}%>
