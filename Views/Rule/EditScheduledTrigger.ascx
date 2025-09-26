<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<% 
    NotificationByTime timedNotification = Model.NotificationByTime;
    double mins = timedNotification.ScheduledMinute;
    string minsWithLeadingZero = mins < 0 ? "00" : mins.ToString("00");
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Events/EditScheduleTrigger|Trigger at","Trigger at")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />
    </div>
</div>
<div class="d-flex align-items-center">
    <select class="form-select" id="ScheduledHour" name="ScheduledHour" style="width: 72px;">
        <option value="0" <%= timedNotification.ScheduledHour % 12 == 0 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|12","12")%></option>
        <option value="1" <%= timedNotification.ScheduledHour % 12 == 1 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|1","1")%></option>
        <option value="2" <%= timedNotification.ScheduledHour % 12 == 2 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|2","2")%></option>
        <option value="3" <%= timedNotification.ScheduledHour % 12 == 3 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|3","3")%></option>
        <option value="4" <%= timedNotification.ScheduledHour % 12 == 4 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|4","4")%></option>
        <option value="5" <%= timedNotification.ScheduledHour % 12 == 5 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|5","5")%></option>
        <option value="6" <%= timedNotification.ScheduledHour % 12 == 6 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|6","6")%></option>
        <option value="7" <%= timedNotification.ScheduledHour % 12 == 7 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|7","7")%></option>
        <option value="8" <%= timedNotification.ScheduledHour % 12 == 8 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|8","8")%></option>
        <option value="9" <%= timedNotification.ScheduledHour % 12 == 9 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|9","9")%></option>
        <option value="10" <%= timedNotification.ScheduledHour % 12 == 10 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|10","10")%></option>
        <option value="11" <%= timedNotification.ScheduledHour % 12 == 11 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|11","11")%></option>
    </select>
    <span class="px-1">:</span>
    <input class="form-select" style="width: 72px" id="ScheduledMinute" name="ScheduledMinute" value="<%=minsWithLeadingZero%>" min="0" max="59" />
    <select class="form-select" id="AMorPM" name="AMorPM" style="width: 72px; margin-left: 6px;">
        <option value="0" <%= timedNotification.ScheduledHour < 12 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|am","am")%></option>
        <option value="12" <%= timedNotification.ScheduledHour > 11 ? "selected":""%>><%: Html.TranslateTag("Events/EditScheduleTrigger|pm","pm")%></option>
    </select>
</div>

<script type="text/javascript">
    function triggerSettings() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scheduledHour=" + $('#ScheduledHour').val();
        settings += "&scheduledMinute=" + $('#ScheduledMinute').val();
        settings += "&AMorPM=" + $('#AMorPM').val();

        return settings;
    }
    function triggerUrl() {
        return "/Rule/EditScheduledTrigger/<%:Model.NotificationID%>";
    }

    let arrayForSpinner = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09"]
    arrayForSpinner = [...arrayForSpinner, arrayBuilder(11, 59, 1)].flat();
    createSpinnerModal("ScheduledMinute", "Minutes", "ScheduledMinute", arrayForSpinner, 0);

</script>
