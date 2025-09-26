<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%
    bool viewable = ZeroToFiveVolts.ShowAdvCal(Model.SensorID);

      if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 555">
        <%: Html.TranslateTag("View Power Options","View Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="podefault" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id= "podefault2" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" name="showAdvCal" id="showAdvCal" <%= viewable ? "checked='checked'" : ""  %> onclick="onOffToggle2()"  <%=Model.CanUpdate ? "" : "disabled" %>>
        </div>
    </div>
</div>


<div class="row sensorEditForm viewPower" style="display: none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Power Options","Power Options")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (Model.SensorTypeID == 4 || Model.GenerationType.ToUpper().Contains("GEN2"))//MoWi
            {%>

        <select name="power" id="power" class=" form-select ms-0 powerdrop" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%=Model.Calibration3 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%=Model.Calibration3 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital High","Digital High")%></option>
            <option value="2" <%=Model.Calibration3 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Digital Low","Digital Low")%></option>
        </select>
        <%}
            else
            { %>
        <select name="power" id="Select1" class="form-select powerdrop" <%=Model.CanUpdate ? "" : "disabled" %>>
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
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_PowerOptions|Delay in milliseconds","Delay in milliseconds")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control delaymil" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="delay" id="delay" value="<%=Model.Calibration4 %>" />
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


        let arrayForSpinner = arrayBuilder(50, 5000, 50);
        createSpinnerModal("delayNum", "Milliseconds", "delay", arrayForSpinner);

        $("#delay").change(function () {
            if (isANumber($("#delay").val())) {
                if ($("#delay").val() < 0)
                    $("#delay").val(0);
                if ($("#delay").val() > 30000)
                    $("#delay").val(30000)

            }
            else {
                $('#delay').val(<%: Model.Calibration4%>);
            }
        });
        <%}%>


    });

    function onOffToggle2() {
        if (document.getElementById("showAdvCal").checked) {
            document.getElementById("podefault").style.display = "none";
        } else {
            document.getElementById("podefault").style.display = "block";
        }
        if (document.getElementById("showAdvCal").checked === false) {
            document.getElementById("podefault2").style.display = "none";
        } else {
            document.getElementById("podefault2").style.display = "block";
        }
        
    }
    onOffToggle2()

</script>

<%}%>
