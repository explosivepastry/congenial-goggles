<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Email Address","Email Address")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Email Address","This email will receive notifications when email is set as the contact method.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <span class="shrink-svg"><%=Html.GetThemedSVG("send") %></span>
        <%: Html.TranslateTag("Settings/UserNotification|Send Test","Send Test")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Send Test","Clicking this button will send a test notification to the device or email found directly next to the button.")%>
    </div>
</div>

<div class="onlyShowInPWA" style="display:none;">
    <hr />
    <div class="row">
        <div class="word-choice">
            <%: Html.TranslateTag("Settings/UserNotification|Allow Push Messages","Allow Push Messages")%>
        </div>
        <div class="word-def" style="white-space: normal; font-weight: 500">
            <%: Html.TranslateTag("Settings/UserNotification|Allow Push Messages","Enable/Disable push messages on the device you are currently using.")%>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="word-choice">
            <%: Html.TranslateTag("Settings/UserNotification|Allow Periodic Sync","Allow Periodic Sync")%>
        </div>
        <div class="word-def" style="white-space: normal; font-weight: 500">
            <%: Html.TranslateTag("Settings/UserNotification|Allow Push Messages","This will allow us to sync our app with the device you are currently using approximately every 12 hours.")%>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="word-choice">
            <%: Html.TranslateTag("Settings/UserNotification|Current Device","Current Device")%>
        </div>
        <div class="word-def" style="white-space: normal; font-weight: 500">
            <%: Html.TranslateTag("Settings/UserNotification|Allow Push Messages","This section will appear once the push messages have been enabled. It allows you to name and send test messages to the device you are currently using.")%>
        </div>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Devices","Devices")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Devices","The devices that have been linked to your account. By default, a name is chosen, which can be changed by clicking on the name.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <span id="makeItGrey" class="shrink-svg"><%=Html.GetThemedSVG("delete") %></span>
        <%: Html.TranslateTag("Settings/UserNotification|Delete","Delete")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Delete","Clicking this button will remove the device/email directly next to it from your profile.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Delivery Method","Delivery Method")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Delivery Method","This setting determines how you would like to receive text messages. Selecting external provider is the more affordable option but will require you to enter your provider's name. Direct Delivery will cost 1 credit per text notification. Credits can be purchased from the credit store (link located on the navigation bar).")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|SMS Provider","SMS Provider")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|SMS Provider","This field is required if you choose to receive text notifications using the external provider delivery method.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Mobile Number","Mobile Number")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Mobile Number","By filling out this field, you acknowledge that you've opted in to receiving SMS messages from us. Leave the field empty to opt out.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Voice Call Number","Voice Call Number")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Voice Call Number","Complete this field if you would like to receive voice call notifications. Each notification will cost 3 credits. Credits can be purchased from the credit store (link located on the navigation bar). Leave the field empty to opt out. ")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Rules","Rules")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Rules","This option will only appear if your user profile has rules linked to it. By clicking the number of rules, you can unlink them from your profile.")%>
    </div>
</div>

<hr />

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Settings/UserNotification|Reports","Reports")%>
    </div>
    <div class="word-def" style="white-space: normal; font-weight: 500">
        <%: Html.TranslateTag("Settings/UserNotification|Reports","This option will only appear if your user profile has reports linked to it. By clicking the number of reports, you can unlink them from your profile.")%>
    </div>
</div>


<style>
    .shrink-svg svg {
        height: 20px !important;
        fill: grey !important;
        margin-right: 0.15rem;
    }

    #makeItGrey svg path {
        fill: grey !important;
    }
</style>

<script>
    $(window).on('load', function () {
        if ($('#AllowPushMessageRow').is(':visible')) {
            $('.onlyShowInPWA').show();
        }
    });


</script>
