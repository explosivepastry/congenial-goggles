<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateScheduledRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <!-- Event List View -->
<div class="card_container shadow-sm rounded w-100">
    <div class="card_container__top">
        <div class="card_container__top__title">
            <%: Html.TranslateTag("Rule/CreateScheduledRule|Trigger Settings","Trigger Settings")%>
        </div>
    </div>
    <div class="card_container__body">
            <div class="">
                <h2><%: Html.TranslateTag("Rule/CreateScheduledRule|Trigger Conditions","Trigger Conditions")%></h2>
                <div class="clearfix"></div>
            </div>
            <div class="">
                <div class="row sensorEditForm">
                    <div class="col-12">
                        <%: Html.TranslateTag("Rule/CreateScheduledRule|Trigger at ","Trigger at ")%>
                    </div>
                    <div class="col sensorEditFormInput d-flex flex-wrap">
                        <input class="aSettings__input_input" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Equal %>" />

                        <% NotificationByTime timedNotification = MonnitSession.NotificationInProgress.NotificationByTime;%>
                        <select class="form-select" style="width:100px;" id="ScheduledHour" name="ScheduledHour">
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
                        :
                            <select class="form-select" style="width:100px;" id="ScheduledMinute" name="ScheduledMinute">
                                <option value="00" <%= timedNotification.ScheduledMinute == 00 ? "selected":""%>>00</option>
                                <option value="15" <%= timedNotification.ScheduledMinute == 15 ? "selected":""%>>15</option>
                                <option value="30" <%= timedNotification.ScheduledMinute == 30 ? "selected":""%>>30</option>
                                <option value="45" <%= timedNotification.ScheduledMinute == 45 ? "selected":""%>>45</option>
                            </select>
                        <select class="form-select" style="width:100px;" id="AMorPM" name="AMorPM">
                            <option value="0" <%= timedNotification.ScheduledHour < 12 ? "selected":""%>><%: Html.TranslateTag("Rule/CreateRuleTrigger|am","am")%></option>
                            <option value="12" <%= timedNotification.ScheduledHour > 11 ? "selected":""%>><%: Html.TranslateTag("Rule/CreateRuleTrigger|pm","pm")%></option>
                        </select>
                    </div>
                </div>

                <div class="text-end">
                    <button type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary mt-2" onclick="$(this).hide();$('#saving').show();createTrigger(this);">
                        <%: Html.TranslateTag("Save","Save")%>
                    </button>
                    <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <%= Html.TranslateTag("Saving") %>...
                    </button>
                </div>
                <div id="result"></div>
            </div>
    </div>
</div>
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

        $.post("/Rule/CreateRuleTrigger", settings, function (partial) {
            $("#newRuleConfigurationHolder").html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

            //Show loader for at leat 500ms
            var timeout = 500 - (Date.now() - beginAjaxTime);
            if (timeout < 0) timeout = 0;
            setTimeout(function () {
                $('#result').html(partial);
            }, timeout);

            window.location.href = "/Rule/RuleComplete";

        });
    }
</script>
<!-- Event List View -->
    </asp:Content>
