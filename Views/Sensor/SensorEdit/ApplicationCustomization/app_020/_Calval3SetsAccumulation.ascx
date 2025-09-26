<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {

%>

<%---Sensor Sleep---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|Sensor Sleep Allowed","Sensor Sleep Allowed")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="accelNo" class="form-check-label"><%: Html.TranslateTag("No","No")%></label>
            <label id="accelYes" class="form-check-label"><%: Html.TranslateTag("Yes","Yes")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="isSleepBox" id="isSleepBox" <%=Model.Calibration1   > 0 ? "checked" : "" %> onclick="accelToggle()">
        </div>
        <div style="display: none;"><%: Html.TextBox("isSleep", Model.Calibration1   > 0 ? 1:0, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        accelToggle();

    });
    //Delay because if not visible yet sizing gets all screwed up
    /*   setTimeout("$('#isSleep').iButton({ labelOn: 'No' , labelOff: 'Yes' });", 500);*/


    function accelToggle() {
        if ($('#isSleepBox').is(':checked')) {
            $('#accelNo').hide();
            $('#isSleep').val(1);
        } else {
            $('#accelNo').show();
        }

        if ($('#isSleepBox').is(':checked') === false) {
            $('#accelYes').hide();
            $('#isSleep').val(0);
        } else {
            $('#accelYes').show();
        }
    }

</script>

<%} %>


<%---Measurement Time---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|Measurement Time","Measurement Time")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Time" id="Time" value="<%: ((Model.Calibration2 / 2).ToString()) %>" />
        <a id="timeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<%---Measurement Reported per heartbeat---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|Measurements Reported Per Heartbeat","Measurements Reported Per Heartbeat")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="perCycle" id="perCycle" value="<%:  (Model.Calibration3) %>" />
        <a id="perCycleNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<%--Operating Mode--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|Option Mode","Option Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select name="mode" id="mode" class="form-select">
            <option value="0" <%:Model.Calibration4 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G Normal","2G Normal")%></option>
            <option value="1" <%:Model.Calibration4 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|2G High Pass Filter","2G High Pass Filter")%></option>
            <option value="2" <%:Model.Calibration4 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G Normal","4G Normal")%></option>
            <option value="3" <%:Model.Calibration4 == 3 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|4G High Pass Filter","4G High Pass Filter")%></option>
            <option value="4" <%:Model.Calibration4 == 4 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G Normal","8G Normal")%></option>
            <option value="5" <%:Model.Calibration4 == 5 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_020|8G High Pass Filter","8G High Pass Filter")%></option>
        </select>
    </div>
</div>


<script type="text/javascript">

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("timeNum", "Seconds", "Time", range(127, 0));

        <%}%>
        $("#Time").addClass('editField editFieldMedium');

        $("#Time").change(function () {
            if (isANumber($("#Time").val())) {
                if ($("#Time").val() < 1)
                    $("#Time").val(1);
                if ($("#Time").val() > 127)
                    $("#Time").val(127);
            }
            else {
                $("#Time").val(1);;
            }
        });

        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 255, 5);
        createSpinnerModal("perCycleNum", "Measurements Per Heartbeat", "perCycle", arrayForSpinner);

        <%}%>
        $("#perCycle").addClass('editField editFieldMedium');

        $("#perCycle").change(function () {
            if (isANumber($("#perCycle").val())) {
                if ($("#perCycle").val() < 0)
                    $("#perCycle").val(0);
                if ($("#perCycle").val() > 255)
                    $("#perCycle").val(255);
            }
            else {
                $("#perCycle").val(0);
            }

        });



    });

</script>
