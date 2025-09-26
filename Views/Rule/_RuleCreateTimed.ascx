<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>

<style>
    .schedule-time > select {
        margin-left: 0.25rem;
        margin-right: 0.25rem;
        width: fit-content;
    }
</style>

<div class="pick-time-condition">
    <div class="card_container__top">
        <div class="card_container__top__title">
            <%: Html.TranslateTag("Choose Condition","Choose a Condition")%>
        </div>
        <br />
    </div>
    <div class="clearfix"></div>
    <div class="card_container__body">
        <div class="row sensorEditForm">
            <div class="rule-title" style="padding-left: 0;">
                <%: Html.TranslateTag("Rule/CreateScheduledRule|Schedule for ","Schedule for")%>
            </div>
            <div class="schedule-time" style="padding-left: 0; max-width: 270px; justify-content: space-between;">
                <input class="aSettings__input_input" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />

                <% NotificationByTime timedNotification = MonnitSession.NotificationInProgress.NotificationByTime;%>
                <select class="form-select" id="ScheduledHour" name="ScheduledHour" style="width: 72px; margin-left: 0;">
                    <option value="0" <%= timedNotification.ScheduledHour % 12 == 0 ? "selected":""%>>12</option>
                    <option value="1" <%= timedNotification.ScheduledHour % 12 == 1 ? "selected":""%>>1</option>
                    <option value="2" <%= timedNotification.ScheduledHour % 12 == 2 ? "selected":""%>>2</option>
                    <option value="3" <%= timedNotification.ScheduledHour % 12 == 3 ? "selected":""%>>3</option>
                    <option value="4" <%= timedNotification.ScheduledHour % 12 == 4 ? "selected":""%>>4</option>
                    <option value="5" <%= timedNotification.ScheduledHour % 12 == 5 ? "selected":""%>>5</option>
                    <option value="6" <%= timedNotification.ScheduledHour % 12 == 6 ? "selected":""%>>6</option>
                    <option value="7" <%= timedNotification.ScheduledHour % 12 == 7 ? "selected":""%>>7</option>
                    <option value="8" <%= timedNotification.ScheduledHour % 12 == 8 ? "selected":""%>>8</option>
                    <option value="9" <%= timedNotification.ScheduledHour % 12 == 9 ? "selected":""%>>9</option>
                    <option value="10" <%= timedNotification.ScheduledHour % 12 == 10 ? "selected":""%>>10</option>
                    <option value="11" <%= timedNotification.ScheduledHour % 12 == 11 ? "selected":""%>>11</option>
                </select>
                <span style="font-weight: 900; font-size: large; font-family: serif;">:</span>
                <input class="form-select" style="width: 72px" id="ScheduledMinute" name="ScheduledMinute" value="<%= timedNotification.ScheduledMinute.ToDouble() < 0 ? "00" : timedNotification.ScheduledMinute.ToString()%>" min="0" max="59" />
                <select class="form-select" id="AMorPM" name="AMorPM" style="width: 72px">
                    <option value="0" <%= timedNotification.ScheduledHour < 12 ? "selected":""%>><%: Html.TranslateTag("Rule/CreateRuleTrigger|am","am")%></option>
                    <option value="12" <%= timedNotification.ScheduledHour > 11 ? "selected":""%>><%: Html.TranslateTag("Rule/CreateRuleTrigger|pm","pm")%></option>
                </select>
            </div>
        </div>

        <div class="text-end pic-save">
            <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                <%= Html.TranslateTag("Saving") %>...
            </button>
        </div>
        <div class="clearfix"></div>
        <div id="result"></div>

        <script type="text/javascript">
            function createTrigger(btn) {
                btn = $(btn);
                btn.hide();
                var beginAjaxTime = Date.now();

                var settings = "compareType=" + $('#CompareType').val();
                settings += "&compareValue=" + $('#CompareValue').val();
                settings += "&scheduledHour=" + $('#ScheduledHour').val();
                settings += "&scheduledMinute=" + $('#ScheduledMinute').val();
                settings += "&AMorPM=" + $('#AMorPM').val();

                $.post("/Rule/AddRuleConditions", settings, function (data) {
                    if (data == "Success") {
                        window.location.href = "/Rule/ChooseTask";
                    }
                });
            }

            let arrayForSpinner = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09"]
            arrayForSpinner = [...arrayForSpinner, arrayBuilder(11, 59, 1)].flat();
            createSpinnerModal("ScheduledMinute", "Minutes", "ScheduledMinute", arrayForSpinner, 0);

        </script>

    </div>
</div>

<!-- Event List View -->

