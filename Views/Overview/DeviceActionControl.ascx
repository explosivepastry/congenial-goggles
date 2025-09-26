<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    string deviceType = Model.GetType().Name;
    string eventTriggeringActiveLabel = Html.TranslateTag("Pause " + deviceType + " from Triggering Rules");
    long deviceID = deviceType == "Sensor" ? ((Sensor)Model).SensorID : ((Gateway)Model).GatewayID;
    DateTime ResumeDate = Model.resumeNotificationDate;
    DateTime LocalTime = Monnit.TimeZone.GetLocalTimeById(ResumeDate > DateTime.UtcNow && ResumeDate != new DateTime(2099, 1, 1) ? ResumeDate : DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    bool isPaused = (ResumeDate > DateTime.UtcNow);
    PreferenceType prefType = PreferenceType.Find("Default Rule Pause Duration");
%>

<div class="d-flex w-100" >
    <div class="rule-card_container w-100 marginLeftOnLgScreen" style="height:100%; margin-top:0;"> 
        <div class="card_container__top__title">
            <span id="pausetitle" style="font-weight: bold">
                <%
                    if (isPaused)
                    { 
                %>
                    <%: Html.TranslateTag("Rule conditions paused until","Rule conditions paused until")%>: <%:(ResumeDate == new DateTime(2099, 1, 1)) ? Html.TranslateTag("Indefinitely","Indefinitely") : LocalTime.ToString() %>
                <%
                    }
                    else
                    { 
                %>
                    <%: eventTriggeringActiveLabel %>
                <%
                    } 
                %>
            </span>
        </div>
        <div class="x_content">
        <% if (prefType != null && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Notification_Pause")))%>
        <%{
                List<PreferenceTypeOption> PauseDurationOptions = prefType.Options;
                string PauseDurationPreference = MonnitSession.CurrentCustomer.Preferences["Default Rule Pause Duration"];
            %>

            <div class="d-flex flex-wrap" style="justify-content: space-between;">
                <div class="flex-wrap sensorEditForm">
                    <div id="divPauseControl" class="flex-wrap sensorEditForm" style="display:none;">
                        <label for="<%: Html.TranslateTag("dateRangeSelect","dateRangeSelect")%>"><%: Html.TranslateTag("Overview/DeviceActionControl|Pause Duration","Pause Duration")%>:</label>
                        <select id="dateRangeSelect" class="form-select mx-md-2" style="width: fit-content;">
                            <%foreach (PreferenceTypeOption option in PauseDurationOptions) {
                                    var translatedText = Html.TranslateTag("Overview/DeviceActionControl|" + option.Name, option.Name);
                              %>
                            <option <%:option.Value == PauseDurationPreference ? "selected": "" %> value="<%:option.Value%>"><%:translatedText%></option>
                            <%}%>
                        </select>
                    </div>

                    <div class="d-flex mobile-row text-end" style="align-items:center">
                        <button type="button" id="resumeButton" style="display: none" class="btn btn-primary" onclick="resumeNotifications();">
                            <%: Html.TranslateTag("Overview/DeviceActionControl|Resume","Resume")%>
                        </button>

                        <button type="button" id="pauseButton" style="display: none" value="<%: Html.TranslateTag("Overview/DeviceActionControl|Save","Save")%>" class="btn btn-primary" onclick="pauseNotifications();">
                            <%: Html.TranslateTag("Overview/DeviceActionControl|Pause","Pause")%>
                        </button>
                    </div>
                </div>
                 </div>
        <%} %>
           
            <hr />
            <div class="col-12">
                <b><a href="/Rule/ChooseType" class="btn btn-primary user-dets">
                    <%=Html.GetThemedSVG("add") %>
                    <%: Html.TranslateTag("Overview/DeviceActionControl|Create a new rule","Create a new rule")%></a></b>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
   
    var sensorIsPaused = '<%=isPaused.ToString().ToLower()%>';

    if (sensorIsPaused == 'true') {
        $('#pauseButton').hide();
        $('#resumeButton').show();
        $('#divPauseControl').hide();
    }
    else {
        $('#pauseButton').show();
        $('#resumeButton').hide();
        $('#divPauseControl').show();
    }

    setNotificationStatusClick();

    function resumeNotifications() {

        $.post("/Notification/PauseDeviceNotification/<%: deviceID %>",
            "&deviceType=<%: deviceType %>" +
            "&button=Resume",
            function (data) {
                if (data == "Unpaused") {
                    $('#pausetitle').html("<%: eventTriggeringActiveLabel %>");
                    $('#pauseButton').show();
                    $('#resumeButton').hide();
                    $('#divPauseControl').show();
                    $('.rule-on-off .pausesvg').prop('hidden', true);
                }
            },
            "text");
    }

    function pauseNotifications() {

        $.post("/Notification/PauseDeviceNotification/<%: deviceID %>",
            "&deviceType=<%: deviceType %>" +
            "&button=" + $("#dateRangeSelect").val(),
            function (data) {
                if (data == "Paused") {
                    $('#pausetitle').html("Triggering paused until: Indefinitely");
                }
                else {
                    $('#pausetitle').html("<%: Html.TranslateTag("Rule Triggering paused until: ") %>"  + data);
                }
                $('#pauseButton').hide();
                $('#resumeButton').show();
                $('#divPauseControl').hide();
                $('.rule-on-off .pausesvg').prop('hidden', false);
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
