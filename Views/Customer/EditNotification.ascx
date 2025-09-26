<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<style>
    .creditCount {
        color: #808080;
        font-size: 25px;
    }
</style>
<%  using (Html.BeginForm())
    {%>

<div class="formtitle">Edit Notification Settings</div>
<div class="formBody">
    <%: Html.ValidationSummary(true) %>
    <table>
        <%AccountTheme acctTheme = AccountTheme.Find(Model.Account);%>

        <tr>
            <td style="width: 250px;">Email Address</td>
            <td style="width: 250px;"><%: Html.TextBoxFor(model => model.NotificationEmail)%>
                <% if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(Model.NotificationEmail))
                    {%>
                <span title="<%: UnsubscribedNotificationEmail.LoadByEmailAddress(Model.NotificationEmail).Reason %>"><%: Html.Label("Email has been Opted Out", new { id="emails" ,style="color:Red;" })%></span>
                <span id="optInLink"><a href="/Customer/NotificationOptIn?email=<%:Model.NotificationEmail %>" data-email="<%: Model.NotificationEmail %>" onclick="optIn(this); return false">Opt in</a></span>
                <%}%> 
            </td>

            <td><%: Html.ValidationMessageFor(model => model.NotificationEmail) %></td>
        </tr>
        <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
            { %>
        <tr>
            <td>Text (SMS) Settings</td>
            <td colspan="2">
                <img style="position: relative; left: 390px; top: 20px; margin-top: -16px; display: block;" alt="help" class="helpIcon" src="<%:Html.GetThemedContent("/images/help.png")%>" title="Direct delivery offers the highest level of reliablity available.  Deliverability is tracked and visible in the sent notifications. <br/><br/>External providers are generally reliable but no deliverability tracking is availalbe.  Because these providers do not charge a fee to send the message, notification credits are not required for sending these SMS messages. <br/><br/>With either method standard text message charges may apply from your wireless provider.">
                <input type="checkbox" id="DirectSMS" name="DirectSMS" <%:Model.DirectSMS ? "checked='checked'" : ""%> />
                <script type="text/javascript">
                    setTimeout('$("#DirectSMS").iButton({ labelOn: "Direct Delivery" , labelOff: "External Provider" });', 500);
                </script>
            </td>
        </tr>
        <%} %>
        <tr class="externalSMS">
            <td>SMS Provider</td>
            <td><%: Html.DropDownList("UISMSCarrierID", (SelectList)ViewData["Carriers"], "Select One")%></td>
            <%--<%:Html.DropDownListFor(m => m.SMSCarrierID, SMSCarrier.LoadAll(),"SMSCarrierName", "Select One") %>--%>
        </tr>
        <tr>
            <td>Mobile Number</td>
            <td><%: Html.TextBoxFor(model => model.NotificationPhone) %></td>
            <td>
                <%: Html.ValidationMessageFor(model => model.NotificationPhone) %>
                <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
                    { %>
                <span>
                    <img src="../../Content/images/credit.png" style="width: 35px;" />&nbsp;
                            <span class="directSMS smsCreditCount creditCount"></span>
                    <span class="externalSMS creditCount">x 0</span>
                    Credits *
                </span><%} %>
            </td>
        </tr>
        <tr class="externalSMS">
            <td></td>
            <td class="externalSMSFormat" colspan="2">
                <%ViewData["Phone"] = Model.NotificationPhone; %>
                <%:Html.Partial("./ExternalSMSProviderFormat",Model.SMSCarrier) %>
            </td>
            <td></td>
        </tr>
        <tr class="directSMS">
            <td></td>
            <td class="smsFormat" colspan="2">
                <%ViewData["Phone"] = Model.NotificationPhone; %>
                <%:Html.Partial("./DirectSMSProviderFormat") %>
            </td>
        </tr>
        <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
            { %>
        <tr>
            <td>Voice Call Number</td>
            <td><%: Html.TextBoxFor(model => model.NotificationPhone2) %></td>
            <td>
                <%: Html.ValidationMessageFor(model => model.NotificationPhone2) %>
                <span>
                    <img src="../../Content/images/credit.png" style="width: 35px;" />&nbsp;
                            <span class="voiceCreditCount creditCount"></span>
                    Credits *
                </span>
            </td>
        </tr>
        <%} %>

        <tr>
            <td></td>
        </tr>
        <tr id="sysNoti">
            <td colspan="2"><span class="title2">System Notifications</span></td>
            <% 
                Customer c = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
                if (c.CustomerID == Model.CustomerID)
                { %>
            <td>
                <input id="custId" value="<%:MonnitSession.CurrentCustomer.CustomerID%>" hidden="hidden" />
                <%if (c.NotificationOptIn >= c.NotificationOptOut)
                    { %>
                <%--  <input type="button" id="OptOut" class="greybutton" value="Opt Out" />
                  not allowing people to opt out of all emails this way anymore --%>
                <%}
                    else
                    {%>
                <input type="button" id="OptIn" class="bluebutton" value="Opt In" />
                <%} %>
            </td>
            <%} %>
        </tr>

        <%if (c.NotificationOptIn >= c.NotificationOptOut)
            {%>

        <tr>
            <td rowspan="2" style="vertical-align: top;">Notification types enabled for user</td>
            <td>
                <input type="checkbox" checked disabled />&nbsp;Email (Required)</td>
            <td></td>
        </tr>
        <tr>
            <td>
                <input type="checkbox" id="Text" name="SendSensorNotificationToText" <%:(Model.SendSensorNotificationToText && !string.IsNullOrEmpty(Model.NotificationPhone)) ? "checked='checked'" : ""%> />&nbsp;Text</td>
            <td></td>
        </tr>
        <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
            { %>
        <tr>
            <td></td>
            <td>
                <input type="checkbox" id="Voice" name="SendSensorNotificationToVoice" <%:Model.SendSensorNotificationToVoice ? "checked" : ""%> />&nbsp;Voice Call
            </td>
            <td></td>
        </tr>
        <%} %>
        <% if (acctTheme.AllowPushNotification == true)
            { %>
        <tr>
            <td></td>
            <td>
                <input type="checkbox" id="PushNotification" name="AllowPushNotification" <%:Model.AllowPushNotification ? "checked='checked'" : ""%> />&nbsp;Mobile Push Notification
            </td>
            <td></td>
        </tr>
        <%} %>

        <%if (acctTheme.SendMaintenanceNotification == true)
            { %>
        <tr>
            <td colspan="3"><span class="title2">System Maintenance Information</span></td>
        </tr>
        <tr>
            <td>Will be sent to user by</td>
            <td>
<%--                <input type="checkbox" id="maintenanceText" name="SendMaintenanceNotificationToEmail" <%:Model.SendMaintenanceNotificationToEmail ? "checked='checked'" : ""%> />&nbsp;Email</td>--%>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
<%--                <input type="checkbox" id="maintenanceVoice" name="SendMaintenanceNotificationToPhone" <%:Model.SendMaintenanceNotificationToPhone ? "checked='checked'" : ""%> />&nbsp;Text</td>--%>
            <td></td>
        </tr>
        <%} %>
        <%} %>
    </table>

    <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
        { %>
    <div style="margin-top: 20px;">* Notification Credits are required for direct delivery of SMS messages and voice notifications.</div>
    <div>
        Phone numbers should be entered in E.164 format. 
                <img alt="help" class="helpIcon" src="<%:Html.GetThemedContent("/images/help.png")%>" title="US Number (+1 999 888 7777)<br/>UK Number (+44 22 3333 4444)">
    </div>
    <a href="#" onclick="$(this).next().slideToggle(); return false;">Learn about E.164 formatting</a>
    <div style="display: none;">
        This format is the internationally-standardized format for all phone numbers, and it includes all the relevant information to route calls and SMS messages globally. E.164 numbers can have a maximum of fifteen digits and are usually written as follows: [+][country code][subscriber number including area code]. Phone numbers that are not formatted in E.164 may work, but it depends on the phone or handset that is being used.<br />
        <br />
        For example, to convert a US phone number (999 888 7777) to E.164 format, one would need to add the ‘+’ prefix and the country code (which is 1) in front of the number (+1 999 888 7777). In the UK and many other countries internationally, local dialing requires the addition of a 0 in front of the subscriber number. However, to use E.164 formatting, this 0 must be removed. A number such as 022 3333 4444 in the UK would be formatted as +44 22 3333 4444.
    </div>
    <%} %>
</div>
<div class="buttons">
    <input type="button" onclick="postForm($(this).closest('form'), function (data) { if (data == 'Success') { $('.refreshPic:visible').click(); } });" value="Save" class="bluebutton" />
    <div style="clear: both;"></div>
</div>
<% } %>
<script>

    $(function () {
        setExternalSMS();

        <%if (Model.NotificationOptIn < Model.NotificationOptOut)
    {%>
        //notiOptOut();
          <%}%>

        $(".helpIcon").tipTip();

        $('#DirectSMS').change(function () {
            $("#NotificationPhone").val("").attr("placeholder", "");
            setExternalSMS();
        });

        $('#OptIn').click(function () {
            $.post('/Notification/NotificationOptIn/' + $('#custId').val(), function (data) {
                if (data == 'Success') {
                    var tabContainter = $('.tabContainer').tabs();
                    var active = tabContainter.tabs('option', 'active');
                    tabContainter.tabs('load', active);
                }
            });
        });

        $('#OptOut').click(function () {
            if (confirm('By Opting Out of Notifications, this User will not longer to be able to recieve device notifications. Are you sure you want to Opt Out?')) {
                $.post('/Notification/NotificationOptOut/' + $('#custId').val(), function (data) {
                    if (data == 'Success') {
                        var tabContainter = $('.tabContainer').tabs();
                        var active = tabContainter.tabs('option', 'active');
                        tabContainter.tabs('load', active);
                    }
                });
            };
        });

        $('#UISMSCarrierID').change(function () {
            $.get('/Customer/ExternalSMSProviderFormat/' + $('#UISMSCarrierID').val() + '?phone=' + encodeURIComponent($('#NotificationPhone').val()), function (data) {
                $('.externalSMSFormat').html(data);
                smsChange();
            });
        });

        $("#NotificationPhone2").intlTelInput({
            //autoFormat: false,
            //autoHideDialCode: false,
            defaultCountry: "us",
            //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
            preferredCountries: ['us'],
            responsiveDropdown: true, //set drop down width to match input width
            utilsScript: "/Scripts/jqueryPlugins/libphonenumber/build/utils.js"
        }).keyup(voiceChange).change(voiceChange);
        voiceChange();

        $('#removeMobileDevice').click(function (e) {
            e.preventDefault();
            var lnk = $(this);
            if (confirm('Are you sure you want to remove this device?')) {
                $.get("/Notification/RemoveMobileDevice?custId=<%:Model.CustomerID%>", function (data, status) {
                    if ("success") {
                        disableUnsavedChangesAlert();
                        window.location = "/Customer/EditNotification?<%:Model.CustomerID%>";
                    }
                    showSimpleMessageModal("<%=Html.TranslateTag("Device was not able to be removed")%>");
                }
                )
            }
            e.stopImmediatePropagation();
        });
    });

    var voiceCountryCode = "";
    function voiceChange() {
        var code = $("#NotificationPhone2").intlTelInput("getSelectedCountryData").iso2;
        if (code && code != voiceCountryCode) {
            voiceCountryCode = code;
            setVoiceCost();
        }
    }

    function setVoiceCost() {
        $.get("/Customer/CalcCredits?code=" + voiceCountryCode + "&voice=true", function (data) {
            $('.voiceCreditCount').html("x " + data);
        });
    }

    //function notiOptOut() {
    //    document.getElementById("Text").disabled = true;
    //    document.getElementById("Voice").disabled = true;
    //    document.getElementById("PushNotification").disabled = true;
    //    document.getElementById("maintenanceText").disabled = true;
    //    document.getElementById("maintenanceVoice").disabled = true;
    //}

    var smsCountryCode = "";
    function smsChange() {
        var smsInput = $("#NotificationPhone");
        var digits = smsInput.val().replace(/\D/g, '');
        $('.displayNotificationPhone').html(digits);
        if (expectedDigits == digits.length) {
            $('#testSMS').show();
            $('.expectedDigits').css("color", "inherit");
        }
        else {

            $('.expectedDigits').css("color", "red");
        }

        <%if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
    {%>
        var code = smsInput.intlTelInput("getSelectedCountryData").iso2;
        if (code && code != smsCountryCode) {
            smsCountryCode = code;
            setSMSCost();
        }
        <%}%>
    }

    function setSMSCost() {
        $.get("/Customer/CalcCredits?code=" + smsCountryCode + "&voice=false", function (data) {
            $('.smsCreditCount').html("x " + data);
        });
    }



    function setExternalSMS() {
        <%if (string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
    {%>
        $('.externalSMS').show();
        $('.directSMS').hide();
        $("#NotificationPhone").keyup(smsChange).change(smsChange);

        <%}
    else
    {%>
        if ($('#DirectSMS').prop('checked')) {
            $('.externalSMS').hide();
            $('.directSMS').show();

            $("#NotificationPhone").intlTelInput({
                //autoFormat: false,
                //autoHideDialCode: false,
                defaultCountry: "us",
                //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                preferredCountries: ['us'],
                responsiveDropdown: true, //set drop down width to match input width
                utilsScript: "/Scripts/jqueryPlugins/libphonenumber/build/utils.js"
            }).keyup(smsChange).change(smsChange);
            smsChange();
        }
        else {
            $('.externalSMS').show();
            $('.directSMS').hide();
            $("#NotificationPhone").intlTelInput("destroy");
        }
        <%}%>
    }

    function testSMS() {
        var url = "/Customer/TestSMS?phone=" + encodeURIComponent($("#NotificationPhone").val());
        if (!$('#DirectSMS').prop('checked'))
            url += "&provider=" + $('#UISMSCarrierID').val();

        $.get(url, function (data) {
            alert(data);
        });
    }


    function optIn(anchor) {
        $.post($(anchor).attr("href"), function (data) {
            if (data == "Success") {
                $('#emails').text("Success: It can take up to 12 hours for this change to take effect on all servers.");
                $('#optInLink').hide();
                showSimpleMessageModal(data);
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }

        });
    }




    //$('#OptIn').click(function(event){
    //    alert(" click event works");
    //    event.preventDefault();
    //    optIn();
    //});
</script>
