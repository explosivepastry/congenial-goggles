<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<div class=" rule-card_container" id="hook-eight" style="margin-top:auto;">
<div class="card_container__top__title d-flex justify-content-between">
        
<%
DateTime ResumeDate = Model.resumeNotificationDate;
DateTime LocalTime = Monnit.TimeZone.GetLocalTimeById(ResumeDate > DateTime.UtcNow && ResumeDate != new DateTime(2099, 1, 1) ? ResumeDate : DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID);
bool isPaused = (ResumeDate > DateTime.UtcNow);
%>
<span id="pausetitle">
<%if (isPaused)
{ %>
<%: Html.TranslateTag("Overview/GatewayActionControl|Rule conditions paused until","Rule conditions paused until")%>: <%:(ResumeDate == new DateTime(2099, 1, 1)) ? Html.TranslateTag("Indefinitely","Indefinitely") : LocalTime.ToString() %>
<%}
else
{ %>
<%: Html.TranslateTag("Overview/GatewayActionControl|Pause Sensor from Triggering Rules","Pause Sensor from Triggering Rules") %>
<%} %>
</span>
        
    </div>
        <div class="x_content">
            <% if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Notification_Pause")))%>
            <%{%>

            <div class="d-flex flex-wrap">
                <div id="divPauseControl" class="flex-wrap row sensorEditForm" style="display:none;">
                    <label for="dateRangeSelect"><%: Html.TranslateTag("Pause Duration","Pause Duration")%>:</label>
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

                <div class="col-12 mobile-row text-end">
                    <button type="button" id="resumeButton" style="display: none" value="<%: Html.TranslateTag("Resume","Resume")%>" class="btn btn-primary" onclick="resumeNotifications(this);">
                        <%: Html.TranslateTag("Resume","Resume")%>
                    </button>
                    <button type="button" id="saveButton" style="display: none" value="<%: Html.TranslateTag("Save", "Save")%>" class="btn btn-primary" >
                        <%: Html.TranslateTag("Pause","Pause")%>
                    </button>
                </div>

                <%} %>
            </div>

            <div class="col-12">
                <b><a href="/Rule/ChooseType" class="btn btn-primary user-dets">
                   <%=Html.GetThemedSVG("add") %>
                    <%: Html.TranslateTag("Overview/AddExistingNotification|Create a new rule","Create a new rule") %></a></b>
            </div>
        </div>
</div>

<script type="text/javascript">
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    var eventTriggerActive = "<%: Html.TranslateTag("Overview/SensorNotification|Rule Triggering Active","Rule Triggering Active") %>";
    var pausedIndef = "<%: Html.TranslateTag("Overview/SensorNotification|Rule Triggering paused until: Indefinitely","Rule Triggering paused until: Indefinitely") %>";
    var pausedDateString = "<%: Html.TranslateTag("Overview/SensorNotification|Rule Triggering paused until: ","Rule Triggering paused until: ") %>";
    var notAuth = "<%: Html.TranslateTag("Overview/SensorNotification|Unauthorized: User does not have permission to acknowledge rules","Unauthorized: User does not have permission to acknowledge rules") %>";
    var failed = "<%: Html.TranslateTag("Overview/SensorNotification|failed to acknowledge rule","failed to acknowledge rule") %>";
    var areuSure = "<%: Html.TranslateTag("Overview/SensorNotification|Are you sure you want to reset this rule","Are you sure you want to reset this rule") %>?";

    $(function () {

        var sensorIsPaused = '<%=isPaused.ToString().ToLower()%>';

        if (sensorIsPaused == 'true') {
            $('#saveButton').hide();
            $('#resumeButton').show();
            $('#divPauseControl').hide();
            $('#divPauseControl').removeClass('d-flex');
        }
        else {
            $('#saveButton').show();
            $('#resumeButton').hide();
            $('#divPauseControl').show();
            $('#divPauseControl').addClass('d-flex');
        }

        setNotificationStatusClick();

        $("#saveButton").click(function () {
            if ($("#dateRangeSelect").val().length > 0) {
                postPauseDuration($("#dateRangeSelect").val());
            }
        });

    });

    $('.setpause').click(function () {
        var button = $(this).data("value");
        postPauseDuration(button);
    });

    function resumeNotifications(button) {

        $.post("/Notification/PauseDeviceNotification/<%:(ViewData["Sensor"] as Monnit.Sensor).SensorID %>",
            "&deviceType=Sensor" +
            "&button=Resume",
            function (data) {
                if (data == "Unpaused") {
                    $('#pausetitle').html(eventTriggerActive);
                    $('#saveButton').show();
                    $('#resumeButton').hide();
                    $('#divPauseControl').show();
                    $('#divPauseControl').addClass('d-flex');
                }
            },
            "text");

    }

    function postPauseDuration(button) {

        $.post("/Notification/PauseDeviceNotification/<%:(ViewData["Sensor"] as Monnit.Sensor).SensorID %>",
            "&deviceType=Sensor" +
            "&button=" + button,
            function (data) {
                if (data == "Paused") {
                    $('#pausetitle').html("Triggering paused until: Indefinitely");
                }
                else {
                    $('#pausetitle').html(pausedDateString + data);

                }
                $('#saveButton').hide();
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
