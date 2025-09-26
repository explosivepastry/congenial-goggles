<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    New Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form method="post" action="/Setup/NewCustomer/<%=Model.CustomerID %>">
        <input id="custId" value="<%:MonnitSession.CurrentCustomer.CustomerID%>" hidden="hidden" />

        <%AccountTheme acctTheme = AccountTheme.Find(Model.AccountID);%>

        <div class="newuser_design">

            <h2 class="welcome-title">
                <%: Html.TranslateTag("Setup/UserDetail|NewCustomer","New User")%>
            </h2>
            <hr class="line-user" />
            <div class="UD-title2">
                <%: Html.TranslateTag("Setup/UserDetail|Details","User Details")%>
            </div>
            <div class="newcust-row">
                <div class="new-login">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("Email / Login","Email / Login")%>:
                    </div>

                    <div class="input-group mb-0">
                        <input id="NotificationEmail" name="NotificationEmail" type="text" class="form-control me-0 " value="<%= Model.NotificationEmail%>">
                        <button class="btn btn-primary" title="<%:Html.TranslateTag("Send Test","Send Test")%>" id="testEmail" value="Test" style="cursor: pointer;">
                            <%=Html.GetThemedSVG("send") %>
                        </button>
                    </div>


                </div>

                <div class="new-login">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("First Name","First Name")%>:
                    </div>
                    <input type="text" name="FirstName" class="form-control " value="<%= Model.FirstName %>">
                </div>

                <div class="new-login">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("Last Name","Last Name")%>:
                    </div>
                    <input type="text" name="LastName" class="form-control " value="<%= Model.LastName %>">
                </div>
            </div>



            <%---  SMS--%>
            <div class="UD-title2">
                <%: Html.TranslateTag("SMS","SMS")%>&nbsp;<p class="option-cust">(<%:Html.TranslateTag ("Optional for Notifications")%>)</p>

                <div class="info-question1">
                    <div class="circleQuestion " data-bs-toggle="modal" data-bs-target="#smsHelpModal">
                        <%=Html.GetThemedSVG("circleQuestion") %>
                    </div>
                </div>

            </div>



            <div class="newcust-row">
                <div class="sms-switch form-switch   ps-0">
                    <label class="form-check-label"><%:Html.TranslateTag ("External Provider")%></label>
                    <input class="form-check-input mx-2" type="checkbox" id="toggle-event" name="DirectSMS" <%:Model.DirectSMS ? "checked='checked'" : ""%>>
                    <label class="form-check-label"><%:Html.TranslateTag ("Direct Delivery")%></label>
                </div>

                <div class="new-login externalSMS">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("SMS Provider","SMS Provider")%>:
                    </div>
                    <select id="UISMSCarrierID" name="UISMSCarrierID" class="UISMSCarrierID sms-dropdown form-select">
                        <option value="0"><%: Html.TranslateTag("Settings/UserNotification|Select One","Select One")%></option>
                        <optgroup label="Most Common" />
                        <%int CurrentRank = 0;
                            foreach (SMSCarrier carrier in Monnit.SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID))
                            {
                                if (CurrentRank <= 1 && carrier.Rank > 1){%><optgroup label="Additional Options" /><%}
                                                                                  CurrentRank = carrier.Rank;
                                    %>
                        <option value="<%:carrier.SMSCarrierID%>"><%= carrier.SMSCarrierName%></option>
                        <%}%>
                    </select>
                </div>


                <div class="new-login tel-input ">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("Settings/UserNotification|SMS Number","SMS Number")%>:
                    </div>

                    <div class="input-newuser">
                        <input id="NotificationPhone" onkeyup="smsChange();" name="NotificationPhone" type="text" class="form-control me-0 " style="text-align: right; max-width: 229px;" value="<%: Model.NotificationPhone %>">
                        <input type="hidden" id="NotificationPhoneISOCode" name="NotificationPhoneISOCode" value="<%=Model.NotificationPhoneISOCode %>" />
                        <button class="btn btn-primary" id="testSMS" title="<%:Html.TranslateTag("Send Test","Send Test")%>" value="Test" style="cursor: pointer;">
                            <%=Html.GetThemedSVG("send") %>
                        </button>
                    </div>
                </div>

                <%if (!string.IsNullOrEmpty(acctTheme.FromPhone))
                    { %>
                <div class="new-login directSMS">
                    <div class="user-info-align">
                    </div>
                    <span class="ms-2 mt-2 directSMS">
                        <span class="smsCreditCount creditCount" title="<%:Html.TranslateTag("Settings/UserNotification|Notification Credits are required for direct delivery of SMS messages and voice notifications","Notification Credits are required for direct delivery of SMS messages and voice notifications")%>."></span>
                        <%:Html.TranslateTag("Settings/UserNotification|Credits","Credits")%>
                    </span>
                </div>
                <%} %>

                <div class="new-login externalSMS">
                    <div class="user-info-align">
                    </div>
                    <div class="externalSMSFormat sms-require">
                        <%:Html.Partial("~/Views/Customer/ExternalSMSProviderFormat.ascx",Model.SMSCarrier) %>
                    </div>
                </div>
            </div>

            <div class="UD-title2">
                <%: Html.TranslateTag("Setup/UserDetail|NewCustomer","Automated Call")%>&nbsp;<p class="option-cust">(<%:Html.TranslateTag ("Optional for Notifications")%>)</p>
                <div class="info-question1">
                    <div class="circleQuestion " data-bs-toggle="modal" data-bs-target="#voiceHelpModal">
                        <%=Html.GetThemedSVG("circleQuestion") %>
                    </div>
                </div>
            </div>
            <div class="newcust-row ">
                <div class="new-login tel-input">
                    <div class="user-info-align">
                        <%: Html.TranslateTag("Settings/UserNotification|Voice Number","Voice Number")%>:
                    </div>
                    <div class="input-newuser">
                        <input type="hidden" id="NotificationPhone2ISOCode" name="NotificationPhone2ISOCode" value="<%=Model.NotificationPhone2ISOCode %>" />
                        <input id="NotificationPhone2" <%--onkeydown="phoneNumberFormatter()"--%> name="NotificationPhone2" type="text" class="form-control me-0 " style="text-align: right; max-width: 229px;" value="<%: Model.NotificationPhone2 %>">
                        <button class="btn btn-primary" title="<%:Html.TranslateTag("Send Test","Send Test")%>" id="testVoice" value="Test" style="cursor: pointer;">
                            <%=Html.GetThemedSVG("send") %>
                        </button>
                    </div>

                </div>
                <div class="new-login">
                    <div class="user-info-align">
                    </div>
                    <div class="input-newuser">
                        <span class="ms-2 mt-2">
                            <span class="voiceCreditCount creditCount" title="<%:Html.TranslateTag("Settings/UserNotification|Notification Credits are required for direct delivery of SMS messages and voice notifications","Notification Credits are required for direct delivery of SMS messages and voice notifications")%>."></span>
                            <%:Html.TranslateTag("Settings/UserNotification|Credits","Credits")%>
                        </span>
                    </div>

                </div>
            </div>

            <div class="cust-row2">
                <div class="field-validation-error text-end" id="notiSettingsSave">
                    <%if (ViewData.ModelState.Keys.Contains("NotificationEmailRequired"))
                        {%><div><%:Html.TranslateTag("Email/Login is Required") %></div>
                    <%} %>
                    <%if (ViewData.ModelState.Keys.Contains("NotificationEmailNotUnique"))
                        {%><div><%:Html.TranslateTag("Email/Login Must Be Unique") %></div>
                    <%} %>
                    <%if (ViewData.ModelState.Keys.Contains("NotificationEmailInvalid"))
                        {%><div><%:Html.TranslateTag("Invalid Email Address") %></div>
                    <%} %>

                    <%if (ViewData.ModelState.Keys.Contains("FirstName"))
                        {%><div><%:Html.TranslateTag("First Name is Required") %></div>
                    <%} %>
                    <%if (ViewData.ModelState.Keys.Contains("LastName"))
                        {%><div><%:Html.TranslateTag("Last Name is Required") %></div>
                    <%} %>

                    <%if (ViewData.ModelState.Keys.Contains("NotificationPhoneInvalid"))
                        {%><div><%:Html.TranslateTag("Invalid SMS Number") %></div>
                    <%} %>
                    <%if (ViewData.ModelState.Keys.Contains("UISMSCarrierID"))
                        {%><div><%:Html.TranslateTag("Provider Required") %></div>
                    <%} %>
                    <%if (ViewData.ModelState.Keys.Contains("NotificationPhoneInvalidLength"))
                        {%><div><%:Html.TranslateTag("Invalid number of digits for selected provider") %></div>
                    <%} %>

                    <%if (ViewData.ModelState.Keys.Contains("NotificationPhone2Invalid"))
                        {%><div><%:Html.TranslateTag("Invalid Voice Number") %></div>
                    <%} %>

                    <%if (ViewData.ModelState.Keys.Contains("Unknown"))
                        {%><div><%:Html.TranslateTag("Unknown Error") %></div>
                    <%} %>

                    <div id="saveMessage">
                    </div>
                    <button type="Submit" value="<%:Html.TranslateTag("Save Changes","Save Changes")%>" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary">
                        <%:Html.TranslateTag("Save Changes","Save Changes")%>
                    </button>
                    <button class="btn btn-primary me-3" id="saving" style="display: none;" type="button" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        Saving
                    </button>
                </div>
            </div>

        </div>
    </form>

    <!-- SMS Help Modal -->
    <div class="modal fade" id="smsHelpModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><%: Html.TranslateTag ("SMS Delivery Methods") %></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col- word-choice">
                            <%: Html.TranslateTag("Settings/UserNotification|External Provider","External Provider")%>
                        </div>
                        <div class="word-def">
                            <%:Html.TranslateTag("Settings/UserNotification|External Providers Help Text","External Providers are generally reliable but no deliverability tracking is available. This is a Free Service.")%>
                            <br />
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col- word-choice">
                            <%: Html.TranslateTag("Settings/UserNotification|Direct Delivery","Direct Delivery")%>
                        </div>
                        <div class="word-def">
                            <%:Html.TranslateTag("Settings/UserNotification|Direct Delivery Help Text","Direct delivery offers the highest level of reliablity available.  These providers charge a fee to send SMS messages.  Each SMS message will require Notification Credits avaialble on your account.")%>
                            <hr />
                            <%:Html.TranslateTag("Settings/UserNotification|Standard Message Rate","With either method standard text message charges may apply from your wireless provider.")%>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- Voice Help Modal -->
    <div class="modal fade" id="voiceHelpModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><%: Html.TranslateTag ("Automated Call") %></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: blue"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="word-def">
                            <%:Html.TranslateTag("Settings/UserNotification|Voice Help Text","Using this method you will receive an automated call.  Notification Credits will need to be available on the account for each call.  The number of credits required will depend on the location of the number being dialed.")%>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>

        $(function () {
            smsChange();
            voiceChange();

            var message = '<%= string.IsNullOrEmpty(ViewBag.Message) ? "":ViewBag.Message  %>';
            $('#saveMessage').html(message);

            if (message == "Success") {
                setTimeout(function () {
                    window.location.href = "/Setup/AccountWelcome/";
                }, 1000); // 1 second wait
            }

            $('#toggle-event').change(function () {
                setExternalSMS();
                smsChange();

            });
            $('#NotificationPhone').change(function () {
                smsChange();
            });
            $('#NotificationPhone2').change(function () {
                voiceChange();
            });

            $('#UISMSCarrierID').change(function () {
                $.get('/Customer/ExternalSMSProviderFormat/' + $('#UISMSCarrierID').val() + '?phone=' + encodeURIComponent($('#NotificationPhone').val()), function (data) {
                    $('.externalSMSFormat').html(data);
                    smsChange();
                });
            });
            $("#NotificationPhone2").intlTelInput({
                autoFormat: false,
                //autoHideDialCode: false,
                defaultCountry: "us",
                //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                preferredCountries: ['us'],
                responsiveDropdown: true, //set drop down width to match input width
               // utilsScript: "/Scripts/jqueryPlugins/libphonenumber/build/utils.js"
            }).keyup(voiceChange).change(voiceChange);
            voiceChange();
        });
        $('#testSMS').click(function (e) {
            e.preventDefault();
            testSMS()
        });
        $('#testVoice').click(function (e) {
            e.preventDefault();
            testVoice()
        });
        $('#testEmail').click(function (e) {
            e.preventDefault();
            testEmail()
        });


        setProvider();
        setExternalSMS();
        var voiceCountryCode = "";
        function voiceChange() {
            var code = $("#NotificationPhone2").intlTelInput("getSelectedCountryData").iso2;
            if (code && code != voiceCountryCode) {
                voiceCountryCode = code;
                setVoiceCost();
            }
            var voiceInput = $("#NotificationPhone2");
            var voicedigits = voiceInput.val().replace(/\D/g, '');
            if (voicedigits.length < 10) {
                $('#testVoice').hide();
            }
            else {
                $('#testVoice').show();
            }
            if (voiceInput.attr('placeholder')) {
                $('#NotificationPhone2Format').html("(" + voiceInput.attr('placeholder') + ")");
            } else {
                $('#NotificationPhone2Format').html("");
            }
            $('#NotificationPhone2ISOCode').val(voiceInput.intlTelInput("getSelectedCountryData").iso2);
        }
        function setVoiceCost() {
            $.get("/Customer/CalcCredits?code=" + voiceCountryCode + "&voice=true", function (data) {
                $('.voiceCreditCount').html("x " + data);
            });
        }


        var smsCountryCode = "";
        function smsChange() {
            var smsInput = $("#NotificationPhone");
            var digits = smsInput.val().replace(/\D/g, '');
            //$('.displayNotificationPhone').html(digits);
            if (expectedDigits == digits.length || $('#toggle-event').prop('checked')) {
                $('#testSMS').show();
                $('.expectedDigits').css("color", "#32cd32");
            }
            else {
                $('#testSMS').hide();
                $('.expectedDigits').css("color", "red");
            }
            //if (smsInput.attr('placeholder')) {
            //    $('#NotificationPhoneFormat').html("(" + smsInput.attr('placeholder') + ")");
            //} else {
            //    $('#NotificationPhoneFormat').html("");
            //}
            $('#NotificationPhoneISOCode').val(smsInput.intlTelInput("getSelectedCountryData").iso2)
        <%if (!string.IsNullOrEmpty(acctTheme.FromPhone))
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
            //$('#testSMS').hide();
            //$('#testVoice').hide();
        <%if (string.IsNullOrEmpty(acctTheme.FromPhone))
        {%>
            $('.externalSMS').show();
            $('.directSMS').hide();
            $("#NotificationPhone").attr("placeholder", "");
            $("#NotificationPhone").keyup(smsChange).change(smsChange);

            <%}
        else
        {%>
            if ($('#toggle-event').prop('checked')) {
                $('.externalSMS').hide();
                $('.directSMS').show();
                //$('#DirectSMS').val('true');

                $("#NotificationPhone").intlTelInput({
                    autoFormat: false,
                    //autoHideDialCode: false,
                    defaultCountry: "us",
                    //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                    preferredCountries: ['us'],
                    responsiveDropdown: true, //set drop down width to match input width
                    //utilsScript: "/Scripts/jqueryPlugins/libphonenumber/build/utils.js"
                }).keyup(smsChange).change(smsChange);
                smsChange();
            }
            else {
                $('.externalSMS').show();
                $('.directSMS').hide();
                $("#NotificationPhone").intlTelInput("destroy");
                $("#NotificationPhone").attr("placeholder", "");
            }
        <%}%>
        }
        function setProvider() {
            <%if (Model.SMSCarrierID.ToInt() > 0)
        {%>
            $("#UISMSCarrierID").val("<%: Model.SMSCarrierID%>");
            <%}%>
        }
        function testSMS() {
            var url = "/Customer/TestSMS?phone=" + encodeURIComponent($("#NotificationPhone").val());
            url += "&isoCode=" + $('#NotificationPhoneISOCode').val();
            url += "&provider=" + $('#UISMSCarrierID').val();
            if ($('#UISMSCarrierID').val() > 0 || confirm("This will consume Notification Credits do you want to continue?")) {
                $.get(url, function (data) {
                    alert(data);
                });
            }
        }
        function testVoice() {
            var url = "/Customer/TestVoice?phone=" + encodeURIComponent($("#NotificationPhone").val());
            url += "&isoCode=" + $('#NotificationPhone2ISOCode').val();
            if (confirm("This will consume Notification Credits do you want to continue?")) {
                $.get(url, function (data) {
                    alert(data);
                });
            }
        }

        function testEmail() {
            var url = "/Customer/TestEmail?address=" + encodeURIComponent($("#NotificationEmail").val());

            $.get(url, function (data) {
                alert(data);
            });
        }


    </script>

</asp:Content>
