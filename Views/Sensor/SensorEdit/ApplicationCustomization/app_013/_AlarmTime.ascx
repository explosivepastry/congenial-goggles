<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    string calval1_Upper = "";
    calval1_Upper = ((Model.Calibration1 >> 16).ToInt()).ToString();
    string calval1_Lower = ((Model.Calibration1 & 0xFFFF).ToInt()).ToString();
    string calval2_Upper = ((Model.Calibration2 >> 16)).ToString();
    bool calval2_Lower_Bit3 = (((Model.Calibration2 & 2048) == 2048)).ToBool();
    bool calval2_Lower_Bit2 = (((Model.Calibration2 & 1024) == 1024)).ToBool();
    bool calval2_Lower_Bit1 = (((Model.Calibration2 & 512) == 512)).ToBool();
    bool calval2_Lower_Bit0 = (((Model.Calibration2 & 256) == 256)).ToBool();
    string calval2_Lower_Byte = ((Model.Calibration2 & 0xFF)).ToString();
    string calval3_Lower = ((Model.Calibration3 & 0xFFFF)).ToString();

    string calval4 = Model.Calibration4.ToString();

    double temp1 = calval1_Lower.ToDouble() / 60;
    calval1_Lower = temp1.ToString();


    double temp2 = calval1_Upper.ToDouble() / 60;
    calval1_Upper = temp2.ToString();

%>

<%--alarm time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Alarm Time","Alarm Time")%> (Minutes)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Alarm_Time" id="Alarm_Time" value="<%=(calval1_Lower) %>" />
        <a id="alarmNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--snooze time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Snooze Time","Snooze Time")%> (Minutes)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Snooze_Time" id="Snooze_Time" value="<%=(calval1_Upper) %>" />
        <a id="snoozeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%--backlight--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable BackLight","Enable BackLight")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disable","Disable")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="BackLight" id="BackLight" <%=calval2_Lower_Bit3 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Enable","Enable")%></label>
        </div>
        <input type="hidden" name="BackLight" value="false" />
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<%--LED alarm--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable LED Alarm","Enable LED Alarm")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disable","Disable")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="LED" id="LED" <%=calval2_Lower_Bit2 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Enable","Enable")%></label>
        </div>
        <input type="hidden" name="LED" value="false" />
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<%--buzzer alarm--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Buzzer  Alarm","Enable Buzzer  Alarm")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disable","Disable")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Buzzer" id="Buzzer" <%=calval2_Lower_Bit1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Enable","Enable")%></label>
        </div>
        <input type="hidden" name="Buzzer" value="false" />
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<%--scrolling alarm--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Scrolling Display Alarm","Enable Scrolling Display Alarm")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disable","Disable")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Scroll" id="Scroll" <%=calval2_Lower_Bit0 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Enable","Enable")%></label>
        </div>
        <input type="hidden" name="Scroll" value="false" />
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<p class="useAwareState"></p>

<%--Message Scroll Speed--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Message Scroll Speed","Message Scroll Speed")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="ScrollSpeed" id="ScrollSpeed" value="<%: ((calval2_Upper.ToInt() / 1000)) %>" />
        <a id="scrollNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--Timezone--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Timezone Offset","Timezone Offset")%> (Minutes)
    </div>
    <div class="col sensorEditFormInput">
        <input hidden type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="TimeZone_Offset" id="TimeZone_Offset" value="<%:Math.Round(DateTime.UtcNow.Subtract(Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow,MonnitSession.CurrentCustomer.Account.TimeZoneID)).TotalMinutes * -1,0)%>" />
        <%--<a id="timezoneNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <span><%:  (Monnit.TimeZone.GetCurrentLocalOffsetById(MonnitSession.CurrentCustomer.Account.TimeZoneID).TotalMinutes) %></span>
    </div>
</div>


<%--LCD Contrast--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|LCD Contrast","LCD Contrast")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="CalVal4_Manual" id="CalVal4_Manual" value="<%: (calval4) %>" />
        <a id="LCDNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--Volune control--%>
<% long minThresh = Model.MinimumThreshold;
    if (minThresh != 4 && minThresh != 256 && minThresh != 512)
    {
        minThresh = 4;
    }
%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Volume Control","Volume Control")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select ms-0" id="MinimumThreshold_Manual" name="MinimumThreshold_Manual">
            <option value="4" <%: minThresh == 4 ? "selected" : "" %>>High</option>
            <option value="256" <%: minThresh == 256 ? "selected" : "" %>>Medium</option>
            <option value="512" <%: minThresh == 512 ? "selected" : "" %>>low</option>
        </select>
    </div>
</div>


<script type="text/javascript">

    $("#Alarm_Time").addClass('editField editFieldSmall');
    $("#Snooze_Time").addClass('editField editFieldMedium');

    var Alarm_array = [.25, .50, 1, 2, 5, 10];
    var Snooze_array = [.25, .5, 1, 2, 5, 10, 20, 30, 60, 120, 240, 360, 720];

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("alarmNum", "Minutes", "Alarm_Time", Alarm_array);


        $("#Alarm_Time").change(function () {
            if (isANumber($("#Alarm_Time").val())) {
                if ($("#Alarm_Time").val() < .25)
                    $("#Alarm_Time").val(.25);
                if ($("#Alarm_Time").val() > 1092)
                    $("#Alarm_Time").val(1092);
            } else
                $("#Alarm_Time").val(<%: calval1_Lower.ToDouble()%>);
        });
        <%}%>

        //Snooze Time
        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("snoozeNum", "Minutes", "Snooze_Time", Alarm_array);

        $("#Snooze_Time").change(function () {
            if (isANumber($("#Snooze_Time").val())) {
                if ($("#Snooze_Time").val() < 0.25)
                    $("#Snooze_Time").val(0.25);
                if ($("#Snooze_Time").val() > 1092)
                    $("#Snooze_Time").val(1092);
            }
            else {
                $("#Snooze_Time").val(<%:calval1_Upper.ToDouble()%>);
            }
        });
        <%}%>




        //Msg Scroll speed
        <% if (Model.CanUpdate)
    { %>
        createSpinnerModal("scrollNum", "Seconds", "ScrollSpeed", range(26, 5));

        $("#ScrollSpeed").change(function () {
            if (isANumber($("#ScrollSpeed").val())) {
                if ($("#ScrollSpeed").val() < 5)
                    $("#ScrollSpeed").val(5);
                if ($("#ScrollSpeed").val() > 63)
                    $("#ScrollSpeed").val(63);
            }
            else {
                $("#ScrollSpeed").val(<%:(calval2_Upper.ToInt() / 1000)%>);
            }
        });
        <% }%>


        // LCD Contrast
        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("LCDNum", "LCD Contrast", "CalVal4_Manual", range(63, 0));

        $("#CalVal4_Manual").change(function () {
            if (isANumber($("#CalVal4_Manual").val())) {
                if ($("#CalVal4_Manual").val() < 0)
                    $("#CalVal4_Manual").val(0);
                if ($("#CalVal4_Manual").val() > 63)
                    $("#CalVal4_Manual").val(63);
            }
            else {
                $("#CalVal4_Manual").val(<%:calval4.ToDouble()%>);
            }
        });

        <% }%>
    });
</script>
