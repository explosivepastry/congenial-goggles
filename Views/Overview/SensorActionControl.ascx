<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<div class=" rule-card_container" id="hook-eight" style="margin-top:auto;">
    <div class="card_container__top__title d-flex justify-content-between">
            <%
                DateTime ResumeDate = Model.resumeNotificationDate;
                DateTime LocalTime = Monnit.TimeZone.GetLocalTimeById(ResumeDate > DateTime.UtcNow && ResumeDate != new DateTime(2099, 1, 1) ? ResumeDate : DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID);
                bool isPaused = (ResumeDate > DateTime.UtcNow);
                if (isPaused)
                { %>
            <span id="pausetitle"><%: Html.TranslateTag("Overview/SensorActionControl|Rule conditions paused until","Rule conditions paused until")%>: <%:(ResumeDate == new DateTime(2099, 1, 1)) ? Html.TranslateTag("Indefinitely","Indefinitely") : LocalTime.ToString() %></span>
            <%}
            else
            { %><span id="pausetitle" style="font-weight: bold"><%: Html.TranslateTag("Overview/SensorActionControl|Pause Sensor from Triggering Rules","Pause Sensor from Triggering Rules")%></span>
            <%} %>
    </div>

        <div class="x_content">
            <% if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Notification_Pause")))%>
            <%{%>
            <div class="d-flex flex-wrap">
                <div id="divPauseControl" class="flex-wrap sensorEditForm" style="display:none;">
                    <label for="dateRangeSelect"><%: Html.TranslateTag("Overview/SensorActionControl|Pause Duration","Pause Duration")%>:</label>
                    <select id="dateRangeSelect" class="form-select mx-md-2" style="width: fit-content;">
                        <option value="Manual"><%: Html.TranslateTag("Indefinitely","Indefinitely")%></option>
                        <option value="Hour"><%: Html.TranslateTag("1 Hour","1 Hour")%></option>
                        <option value="2Hour"><%: Html.TranslateTag("2 Hours","2 Hours")%></option>
                        <option value="3Hour"><%: Html.TranslateTag("3 Hours","3 Hours")%></option>
                        <option value="4Hour"><%: Html.TranslateTag("4 Hours","4 Hours")%></option>
                        <option value="Day"><%: Html.TranslateTag("1 Day","1 Day")%></option>
                        <option value="2Day"><%: Html.TranslateTag("2 Days","2 Days")%></option>
                        <option value="3Day"><%: Html.TranslateTag("3 Days","3 Days")%></option>
                        <option value="7Day"><%: Html.TranslateTag("7 Days","7 Days")%></option>
                        <option value="14Day"><%: Html.TranslateTag("14 Days","14 Days")%></option>
                        <option value="Month"><%: Html.TranslateTag("1 Month","1 Month")%></option>
                    </select>
                </div>

                <div class="d-flex mobile-row text-end" style="align-items:center">
                    <button type="button" id="resumeButton" style="display: none" class="btn btn-primary" onclick="resumeNotifications();">
                        <%: Html.TranslateTag("Resume","Resume")%>
                    </button>

                    <button type="button" id="pauseButton" style="display: none" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" onclick="pauseNotifications();">
                        <%: Html.TranslateTag("Pause","Pause")%>
                    </button>
                </div>
                <%} %>
            </div>
            <hr />

            <div class="col-12">
                <b><a href="/Rule/ChooseType" class="btn btn-primary user-dets">
                   <%=Html.GetThemedSVG("add") %>
                    <%: Html.TranslateTag("Overview/SensorActionControl|Create a new rule","Create a new rule")%>
                   </a></b>
            </div>
        </div>
</div>

<script type="text/javascript">
   
    var sensorIsPaused = '<%=isPaused.ToString().ToLower()%>';

    if (sensorIsPaused == 'true') {
        $('#pauseButton').hide();
        $('#resumeButton').show();
        $('#divPauseControl').hide();
        $('#divPauseControl').removeClass('d-flex');
    }
    else {
        $('#pauseButton').show();
        $('#resumeButton').hide();
        $('#divPauseControl').show();
        $('#divPauseControl').addClass('d-flex');
    }

    setNotificationStatusClick();

    function resumeNotifications() {

        $.post("/Notification/PauseDeviceNotification/<%:(ViewData["Sensor"] as Monnit.Sensor).SensorID %>",
            "&deviceType=Sensor" +
            "&button=Resume",
            function (data) {
                if (data == "Unpaused") {
                    $('#pausetitle').html(eventTriggerActive);
                    $('#pauseButton').show();
                    $('#resumeButton').hide();
                    $('#divPauseControl').show();
                    $('#divPauseControl').addClass('d-flex');
                }
            },
            "text");
    }

    function pauseNotifications() {

        $.post("/Notification/PauseDeviceNotification/<%:(ViewData["Sensor"] as Monnit.Sensor).SensorID %>",
            "&deviceType=Sensor" +
            "&button=" + $("#dateRangeSelect").val(),
            function (data) {
                if (data == "Paused") {
                    $('#pausetitle').html("Triggering paused until: Indefinitely");
                }
                else {
                    $('#pausetitle').html(pausedDateString + data);

                }
                $('#pauseButton').hide();
                $('#resumeButton').show();
                $('#divPauseControl').hide();
                $('#divPauseControl').removeClass('d-flex');
            },
            "text");
    }

    function setNotificationStatusClick() {
        $('.notiStatus').unbind('click').click(function (e) {
            e.preventDefault();
            var lnk = $(this);
            $.get(lnk.attr('href'), function (data) {
                if (data == "Success")
                    $('.notiStatus' + lnk.attr('id')).toggle();
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            });
        });
    }

</script>
