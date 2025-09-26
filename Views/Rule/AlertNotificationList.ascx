<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NotificationRecipient>>" %>

<div class="mh400">
    <%
        List<NotificationRecipient> assignedRecipients = MonnitSession.NotificationRecipientsInProgress;
        List<NotificationRecipientData> userListIncludingHigherAccounts = NotificationRecipientData.SearchPotentialRecipient(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.NotificationInProgress.AccountID, "");
        List<NotificationRecipientData> pushEnabledUserList = NotificationRecipientData.SearchPotentialPushMessageRecipientForAccount(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.NotificationInProgress.AccountID, "");

        List<NotificationRecipientData> allCustomersInAccount = userListIncludingHigherAccounts
            .Where(item => item.CustomerGroupID < 0)
            .ToList();

        string currentUrl = Request.Url.AbsoluteUri;

        if (currentUrl.Contains("EditSendAlert"))
        {
            assignedRecipients = Model;

            List<NotificationRecipient> selectedUserGroupIDs = assignedRecipients
                .Where(recipient => recipient.CustomerGroupID != null && recipient.CustomerGroupID > 0)
                .ToList();
        }

        var orderedCustomers = allCustomersInAccount.OrderByDescending(cust =>
            assignedRecipients.Count(m => m.CustomerToNotifyID == cust.CustomerID && m.DelayMinutes >= 0)
        );

        Dictionary<int, string> delayDictionary = new Dictionary<int, string>
            {
                { -1, "Off" },
                { 0, "No Delay" },
                { 2, "2 Mins" },
                { 5, "5 Mins" },
                { 10, "10 Mins" },
                { 15, "15 Mins" },
                { 30, "30 Mins" },
                { 45, "45 Mins" },
                { 60, "1 Hour" },
                { 120, "2 Hours" },
                { 240, "4 Hours" },
                { 360, "6 Hours" },
                { 480, "8 Hours" },
                { 600, "10 Hours" },
                { 720, "12 Hours" },
                { 960, "16 Hours" },
                { 1200, "20 Hours" },
                { 1440, "24 Hours" }
            };

        foreach (NotificationRecipientData cust in orderedCustomers)
        {
            bool emailActive = false;
            bool textActive = false;
            bool voiceActive = false;
            bool pushActive = false;
            int pushDelay = -1;
            int emailDelay = -1;
            int textDelay = -1;
            int voiceDelay = -1;

            // Will set the values if the user is still creating a rule and decides to go back into the set alert page. 
            NotificationRecipient emailLinkFromSession = assignedRecipients.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.Email; }).FirstOrDefault();
            emailActive = emailLinkFromSession != null;
            if (emailActive) emailDelay = emailLinkFromSession.DelayMinutes <= int.MinValue ? 0 : emailLinkFromSession.DelayMinutes;
            if (!delayDictionary.ContainsKey(emailDelay)) emailDelay = delayDictionary.Keys.OrderBy(k => Math.Abs(k - emailDelay)).First(); //If the delay saved is not in the dictionary grab the closest value in the dictionary.

            NotificationRecipient textLinkFromSession = assignedRecipients.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.SMS; }).FirstOrDefault();
            textActive = textLinkFromSession != null;
            if (textActive) textDelay = textLinkFromSession.DelayMinutes <= int.MinValue ? 0 : textLinkFromSession.DelayMinutes;
            if (!delayDictionary.ContainsKey(textDelay)) textDelay = delayDictionary.Keys.OrderBy(k => Math.Abs(k - textDelay)).First(); //If the delay saved is not in the dictionary grab the closest value in the dictionary.

            NotificationRecipient voiceLinkFromSession = assignedRecipients.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.Phone; }).FirstOrDefault();
            voiceActive = voiceLinkFromSession != null;
            if (voiceActive) voiceDelay = voiceLinkFromSession.DelayMinutes <= int.MinValue ? 0 : voiceLinkFromSession.DelayMinutes;
            if (!delayDictionary.ContainsKey(voiceDelay)) voiceDelay = delayDictionary.Keys.OrderBy(k => Math.Abs(k - voiceDelay)).First(); //If the delay saved is not in the dictionary grab the closest value in the dictionary.

            NotificationRecipient pushLinkFromSession = assignedRecipients.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.Push_Message; }).FirstOrDefault();
            pushActive = pushLinkFromSession != null;
            if (pushActive) pushDelay = pushLinkFromSession.DelayMinutes <= int.MinValue ? 0 : pushLinkFromSession.DelayMinutes;
            if (!delayDictionary.ContainsKey(pushDelay)) pushDelay = delayDictionary.Keys.OrderBy(k => Math.Abs(k - pushDelay)).First(); //If the delay saved is not in the dictionary grab the closest value in the dictionary.

            assignedRecipients.RemoveAll(m => { return m.CustomerToNotifyID == cust.CustomerID; });

    %>
    <!-- Contact Person -->
    <div style="font-size: 1.4em" class="flexAB recipientCardAB" data-recipientname="<%=cust.FullName %>" data-recipientid="<%=cust.CustomerID %>">
        <div class="col-6 shrink-col-6-mobile d-flex align-center-with-gap">
            <svg class="width-none-mobile" xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="#707070">
                <path id="ic_perm_identity_24px" d="M12,5.9A2.1,2.1,0,1,1,9.9,8,2.1,2.1,0,0,1,12,5.9m0,9c2.97,0,6.1,1.46,6.1,2.1v1.1H5.9V17c0-.64,3.13-2.1,6.1-2.1M12,4a4,4,0,1,0,4,4A4,4,0,0,0,12,4Zm0,9c-2.67,0-8,1.34-8,4v3H20V17C20,14.34,14.67,13,12,13Z" transform="translate(-4 -4)" fill="#707070;" />
            </svg>
            <span class="notifyUsers__name">
                <%=cust.FullName %>
            </span>
        </div>
        <input type="hidden" id="scroller" />
        <div class="col-2AB shrink-col-2-mobile" style="display: flex; flex-direction: column">
            <span title="Toggle email" onclick="handleToggleNotificationMethod('<%:cust.CustomerID %>','Email')" style="cursor: pointer; width: fit-content;">
                <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 18">
                    <path id="paper-plane-regular_<%:cust.CustomerID %>" class="<%= emailDelay != -1 ? "toggleOnColor" : "initialFill"%>" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" />
                </svg>
            </span>
            <a id="openSpinnerModalFor_<%:cust.CustomerID %>_Email" title="<%=cust.NotificationEmail %>" data-customerid="<%= cust.CustomerID %>" data-add="<%= !emailActive %>" data-type="Email" class="nr<%=cust.CustomerID%> green-on-hover" style="cursor: pointer; width: fit-content;">
                <span id="EmailSpan_<%:cust.CustomerID %>" style="font-size: 0.6em">
                    <%=delayDictionary[emailDelay] %>
                </span>
            </a>
            <input name="Email" type="hidden" id="openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput" value="<%=emailDelay %>" class="hiddenInputToCheckOnSubmit" />
        </div>

        <div class="col-2AB shrink-col-2-mobile" style="display: flex; flex-direction: column">
            <%if (cust.SendSensorNotificationToText)
                { %>

            <span onclick="handleToggleNotificationMethod('<%:cust.CustomerID %>','Text')" style="cursor: pointer; width: fit-content;">
                <svg xmlns="http://www.w3.org/2000/svg" width="18" height="16" viewBox="0 0 18 16">
                    <path id="comments-regular_<%:cust.CustomerID %>" class="<%= textDelay != -1 ? "toggleOnColor" : "initialFill"%>" d="M16.616,44.65a5.259,5.259,0,0,0,1.375-3.507c0-2.857-2.39-5.218-5.506-5.639A6.568,6.568,0,0,0,6.493,32c-3.59,0-6.5,2.557-6.5,5.714a5.272,5.272,0,0,0,1.375,3.507A9.448,9.448,0,0,1,.19,43.182a.913.913,0,0,0-.137.893.725.725,0,0,0,.662.5,6.586,6.586,0,0,0,3.912-1.386,7.9,7.9,0,0,0,.887.175,6.548,6.548,0,0,0,5.977,3.493,7.286,7.286,0,0,0,1.869-.243A6.6,6.6,0,0,0,17.273,48a.728.728,0,0,0,.662-.5.921.921,0,0,0-.137-.893A9.187,9.187,0,0,1,16.616,44.65Zm-12.274-3.3-.534.4a6.077,6.077,0,0,1-1.347.764c.084-.168.169-.346.25-.529L3.2,40.875,2.421,40a3.478,3.478,0,0,1-.928-2.286c0-2.168,2.29-4,5-4s5,1.832,5,4-2.29,4-5,4a6,6,0,0,1-1.531-.2Zm11.221,2.075-.772.871.484,1.111c.081.182.166.361.25.529a6.077,6.077,0,0,1-1.347-.764l-.534-.4-.622.164a6,6,0,0,1-1.531.2,5.435,5.435,0,0,1-4.1-1.775c3.165-.386,5.6-2.764,5.6-5.654,0-.121-.012-.239-.022-.357,2.012.518,3.521,2.029,3.521,3.786A3.478,3.478,0,0,1,15.563,43.429Z" transform="translate(0.007 -32)" />
                </svg>
            </span>

            <a id="openSpinnerModalFor_<%:cust.CustomerID %>_Text" title="<%=cust.NotificationPhone %>" data-customerid="<%: cust.CustomerID %>" data-add="<%=!textActive %>" data-type="SMS" class="nr<%:cust.CustomerID %> green-on-hover" style="cursor: pointer; width: fit-content;">
                <span id="TextSpan_<%:cust.CustomerID %>" style="font-size: 0.6em">
                    <%=delayDictionary[textDelay] %>
                </span>
            </a>
            <input name="Text" type="hidden" id="openSpinnerModalFor_<%:cust.CustomerID %>_TextInput" value="<%=textDelay %>" class="hiddenInputToCheckOnSubmit" />
            <%} %>
        </div>


        <div class="col-2AB shrink-col-2-mobile" style="display: flex; flex-direction: column">
            <%if (cust.SendSensorNotificationToVoice)
                {%>

            <span onclick="handleToggleNotificationMethod('<%:cust.CustomerID %>','Voice')" style="cursor: pointer; width: fit-content;">
                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="18" viewBox="0 0 14 18">
                    <path id="phone-volume-solid_<%:cust.CustomerID %>" class="<%= voiceDelay != -1 ? "toggleOnColor" : "initialFill"%>" d="M3.548,17.823a11.389,11.389,0,0,1,0-16.521.644.644,0,0,1,.775-.085L6.687,2.641a.593.593,0,0,1,.25.737L5.755,6.226a.629.629,0,0,1-.644.377l-2.035-.2a9.025,9.025,0,0,0,0,6.311l2.035-.2a.629.629,0,0,1,.644.377l1.182,2.848a.593.593,0,0,1-.25.737L4.324,17.908A.644.644,0,0,1,3.548,17.823ZM9.01,3.357a2.183,2.183,0,0,1,0,2.287.449.449,0,0,1-.682.091l-.218-.2a.414.414,0,0,1-.082-.507,1.092,1.092,0,0,0,0-1.053.414.414,0,0,1,.082-.507l.218-.2A.449.449,0,0,1,9.01,3.357ZM12.356.151a6.576,6.576,0,0,1,0,8.7.45.45,0,0,1-.64.033l-.211-.2a.412.412,0,0,1-.034-.576,5.48,5.48,0,0,0,0-7.222A.412.412,0,0,1,11.5.313l.211-.2a.45.45,0,0,1,.64.033ZM10.68,1.731a4.38,4.38,0,0,1,0,5.539.45.45,0,0,1-.651.046l-.212-.2a.411.411,0,0,1-.047-.56,3.284,3.284,0,0,0,0-4.118.411.411,0,0,1,.047-.56l.212-.2a.45.45,0,0,1,.651.046Z" transform="translate(0 0)" />
                </svg>
            </span>
            <a id="openSpinnerModalFor_<%:cust.CustomerID %>_Voice" title="<%=cust.NotificationPhone2 %>" data-customerid="<%: cust.CustomerID %>" data-add="<%=!voiceActive %>" data-type="Phone" class="nr<%:cust.CustomerID %> green-on-hover" style="cursor: pointer; width: fit-content;">
                <span id="VoiceSpan_<%:cust.CustomerID %>" style="font-size: 0.6em">
                    <%=delayDictionary[voiceDelay] %>
                </span>
            </a>
            <input name="Voice" type="hidden" id="openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput" value="<%=voiceDelay %>" class="hiddenInputToCheckOnSubmit" />
            <%} %>
        </div>


        <div class="col-2AB shrink-col-2-mobile" style="display: flex; flex-direction: column">
            <%if (pushEnabledUserList.Any(user=> user.CustomerID == cust.CustomerID) && MonnitSession.CustomerCan("See_Beta_Preview"))
                {%>
            <span onclick="handleToggleNotificationMethod('<%:cust.CustomerID %>','Push')" style="cursor: pointer; width: fit-content;">
                <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24">
                    <path id="push-noti-icon_<%:cust.CustomerID %>" class="<%= pushDelay != -1 ? "toggleOnColor" : "initialFill"%>" d="M15 21c0 1.598-1.392 3-2.971 3s-3.029-1.402-3.029-3h6zm.137-17.055c-.644-.374-1.042-1.07-1.041-1.82v-.003c.001-1.172-.938-2.122-2.096-2.122s-2.097.95-2.097 2.122v.003c.001.751-.396 1.446-1.041 1.82-4.668 2.709-1.985 11.715-6.862 13.306v1.749h20v-1.749c-4.877-1.591-2.193-10.598-6.863-13.306zm-3.137-2.945c.552 0 1 .449 1 1 0 .552-.448 1-1 1s-1-.448-1-1c0-.551.448-1 1-1zm-6.451 16c1.189-1.667 1.605-3.891 1.964-5.815.447-2.39.869-4.648 2.354-5.509 1.38-.801 2.956-.76 4.267 0 1.485.861 1.907 3.119 2.354 5.509.359 1.924.775 4.148 1.964 5.815h-12.903zm15.229-7.679c.18.721.05 1.446-.304 2.035l.97.584c.504-.838.688-1.869.433-2.892-.255-1.024-.9-1.848-1.739-2.351l-.582.97c.589.355 1.043.934 1.222 1.654zm.396-4.346l-.597.995c1.023.616 1.812 1.623 2.125 2.874.311 1.251.085 2.51-.53 3.534l.994.598c.536-.892.835-1.926.835-3-.001-1.98-1.01-3.909-2.827-5.001zm-16.73 2.692l-.582-.97c-.839.504-1.484 1.327-1.739 2.351-.255 1.023-.071 2.053.433 2.892l.97-.584c-.354-.588-.484-1.314-.304-2.035.179-.72.633-1.299 1.222-1.654zm-4.444 2.308c0 1.074.299 2.108.835 3l.994-.598c-.615-1.024-.841-2.283-.53-3.534.312-1.251 1.101-2.258 2.124-2.873l-.597-.995c-1.817 1.092-2.826 3.021-2.826 5z" />
                </svg>
            </span>
            <a id="openSpinnerModalFor_<%:cust.CustomerID %>_Push" data-customerid="<%: cust.CustomerID %>" data-add="<%=!pushActive %>" data-type="Push" class="nr<%:cust.CustomerID %> green-on-hover" style="cursor: pointer; width: fit-content;">
                <span id="PushSpan_<%:cust.CustomerID %>" style="font-size: 0.6em">
                    <%=delayDictionary[pushDelay] %>
                </span>
            </a>
            <input name="Push" type="hidden" id="openSpinnerModalFor_<%:cust.CustomerID %>_PushInput" value="<%=pushDelay %>" class="hiddenInputToCheckOnSubmit" />
            <%} %>
        </div>

        <script>
            var valuesArrayInStrings_<%:cust.CustomerID %> = ["<%: Html.TranslateTag("Off")%>","<%: Html.TranslateTag("No Delay")%>", "<%: Html.TranslateTag("2 Mins")%>", "<%: Html.TranslateTag("5 Mins")%>", "<%: Html.TranslateTag("10 Mins")%>", "<%: Html.TranslateTag("15 Mins")%>", "<%: Html.TranslateTag("30 Mins")%>", "<%: Html.TranslateTag("45 Mins")%>", "<%: Html.TranslateTag("1 Hour")%>", "<%: Html.TranslateTag("2 Hours")%>", "<%: Html.TranslateTag("4 Hours")%>", "<%: Html.TranslateTag("6 Hours")%>", "<%: Html.TranslateTag("8 Hours")%>", "<%: Html.TranslateTag("10 Hours")%>", "<%: Html.TranslateTag("12 Hours")%>", "<%: Html.TranslateTag("16 Hours")%>", "<%: Html.TranslateTag("20 Hours")%>", "<%: Html.TranslateTag("24 Hours")%>"]
            var valuesArrayInNumbers_<%:cust.CustomerID %> = [-1, 0, 2, 5, 10, 15, 30, 45, 60, 120, 240, 360, 480, 600, 720, 960, 1200, 1440];

            var inputElement<%=cust.CustomerID %>_Email = document.getElementById('openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput');
            var inputElement<%=cust.CustomerID %>_Text = document.getElementById('openSpinnerModalFor_<%:cust.CustomerID %>_TextInput');
            var inputElement<%=cust.CustomerID %>_Voice = document.getElementById('openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput');
            var inputElement<%=cust.CustomerID %>_Push = document.getElementById('openSpinnerModalFor_<%:cust.CustomerID %>_PushInput');

            var emailModalValueToHighlight = null;

            if (inputElement<%=cust.CustomerID %>_Email) {
            inputElement<%=cust.CustomerID %>_Email.addEventListener('change', changeEmailSpinnerHighlightedValue)

                function changeEmailSpinnerHighlightedValue(initialPageLoad, customerId, indexToHighlight) {
                    let inputValue = $(`#openSpinnerModalFor_${customerId}_EmailInput`).val();
                    emailModalValueToHighlight = valuesArrayInNumbers_<%:cust.CustomerID%>.indexOf(Number(inputValue));

                    if (initialPageLoad) {
                        return;
                    } else {
                        $(`.openSpinnerModalFor_${customerId}_Email`).remove();
                        createSpinnerModal(`openSpinnerModalFor_${customerId}_Email`, "Time To Delay", `openSpinnerModalFor_${customerId}_EmailInput`, valuesArrayInStrings_<%:cust.CustomerID %>, indexToHighlight);
                    }
                }
                changeEmailSpinnerHighlightedValue(true,<%:cust.CustomerID %>, null);
            }

            var textModalValueToHighlight = null;

            if (inputElement<%=cust.CustomerID %>_Text) {
                inputElement<%=cust.CustomerID %>_Text.addEventListener('change', changeTextSpinnerHighlightedValue)

                function changeTextSpinnerHighlightedValue(initialPageLoad, customerId, indexToHighlight) {
                    let inputValue = $(`#openSpinnerModalFor_${customerId}_TextInput`).val();
                    textModalValueToHighlight = valuesArrayInNumbers_<%:cust.CustomerID%>.indexOf(Number(inputValue));
                    if (initialPageLoad) {
                        return;
                    } else {
                        $(`.openSpinnerModalFor_${customerId}_Text`).remove();
                        createSpinnerModal(`openSpinnerModalFor_${customerId}_Text`, "Time To Delay", `openSpinnerModalFor_${customerId}_TextInput`, valuesArrayInStrings_<%:cust.CustomerID %>, indexToHighlight);
                    }
                }
                changeTextSpinnerHighlightedValue(true,<%:cust.CustomerID %>, null);
            }

            var voiceModalValueToHighlight = null;

            if (inputElement<%=cust.CustomerID %>_Voice) {
               inputElement<%=cust.CustomerID %>_Voice.addEventListener('change', changeVoiceSpinnerHighlightedValue);

                function changeVoiceSpinnerHighlightedValue(initialPageLoad, customerId, indexToHighlight) {
                    let inputValue = $(`#openSpinnerModalFor_${customerId}_VoiceInput`).val();
                    voiceModalValueToHighlight = valuesArrayInNumbers_<%:cust.CustomerID%>.indexOf(Number(inputValue));
                    if (initialPageLoad) {
                        return;
                    } else {
                        $(`.openSpinnerModalFor_${customerId}_Voice`).remove();
                        createSpinnerModal(`openSpinnerModalFor_${customerId}_Voice`, "Time To Delay", `openSpinnerModalFor_${customerId}_VoiceInput`, valuesArrayInStrings_<%:cust.CustomerID %>, indexToHighlight);
                    }
                }
                changeVoiceSpinnerHighlightedValue(true,<%:cust.CustomerID %>, null);
            }

            var pushModalValueToHighlight = null;

            if (inputElement<%=cust.CustomerID %>_Push) {
               inputElement<%=cust.CustomerID %>_Push.addEventListener('change', changePushSpinnerHighlightedValue);

                function changePushSpinnerHighlightedValue(initialPageLoad, customerId, indexToHighlight) {
                    let inputValue = $(`#openSpinnerModalFor_${customerId}_PushInput`).val();
                    pushModalValueToHighlight = valuesArrayInNumbers_<%:cust.CustomerID%>.indexOf(Number(inputValue));
                    if (initialPageLoad) {
                        return;
                    } else {
                        $(`.openSpinnerModalFor_${customerId}_Push`).remove();
                        createSpinnerModal(`openSpinnerModalFor_${customerId}_Push`, "Time To Delay", `openSpinnerModalFor_${customerId}_PushInput`, valuesArrayInStrings_<%:cust.CustomerID %>, indexToHighlight);
                    }
                }
                changePushSpinnerHighlightedValue(true,<%:cust.CustomerID %>, null);
            }

            createSpinnerModal("openSpinnerModalFor_<%:cust.CustomerID %>_Email", "Time To Delay", "openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput", valuesArrayInStrings_<%:cust.CustomerID %>, emailModalValueToHighlight);
            createSpinnerModal("openSpinnerModalFor_<%:cust.CustomerID %>_Text", "Time To Delay", "openSpinnerModalFor_<%:cust.CustomerID %>_TextInput", valuesArrayInStrings_<%:cust.CustomerID %>, textModalValueToHighlight);
            createSpinnerModal("openSpinnerModalFor_<%:cust.CustomerID %>_Voice", "Time To Delay", "openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput", valuesArrayInStrings_<%:cust.CustomerID %>, voiceModalValueToHighlight);
            createSpinnerModal("openSpinnerModalFor_<%:cust.CustomerID %>_Push", "Time To Delay", "openSpinnerModalFor_<%:cust.CustomerID %>_PushInput", valuesArrayInStrings_<%:cust.CustomerID %>, pushModalValueToHighlight);

            //Email observer
            var observer<%=cust.CustomerID %>_Email = new MutationObserver(function (mutationsList, observer) {

                for (let mutation of mutationsList) {

                    if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                        observer<%=cust.CustomerID %>_Email.disconnect();

                        changeDelayString(<%=cust.CustomerID %>, "Email");

                        if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput.value)) {
                            openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput.value = stringToNumber(openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput.value);
                        }

                        ensureIconIsRightColor('openSpinnerModalFor_<%:cust.CustomerID %>_EmailInput', 'paper-plane-regular_<%:cust.CustomerID %>');

                        observer<%=cust.CustomerID %>_Email.observe(inputElement<%=cust.CustomerID %>_Email, { attributes: true });
                    }
                }
            });

            if (inputElement<%=cust.CustomerID %>_Email) {
            observer<%=cust.CustomerID %>_Email.observe(inputElement<%=cust.CustomerID %>_Email, { attributes: true });
            }

            //Text observer
            var observer<%=cust.CustomerID %>_Text = new MutationObserver(function (mutationsList, observer) {

                for (let mutation of mutationsList) {

                    if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                        observer<%=cust.CustomerID %>_Text.disconnect();

                        changeDelayString(<%=cust.CustomerID %>, "Text");

                        if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(openSpinnerModalFor_<%:cust.CustomerID %>_TextInput.value)) {
                            openSpinnerModalFor_<%:cust.CustomerID %>_TextInput.value = stringToNumber(openSpinnerModalFor_<%:cust.CustomerID %>_TextInput.value)
                        }

                        ensureIconIsRightColor('openSpinnerModalFor_<%:cust.CustomerID %>_TextInput', 'comments-regular_<%:cust.CustomerID %>');

                        observer<%=cust.CustomerID %>_Text.observe(inputElement<%=cust.CustomerID %>_Text, { attributes: true });
                    }
                }
            });

            if (inputElement<%=cust.CustomerID %>_Text) {
            observer<%=cust.CustomerID %>_Text.observe(inputElement<%=cust.CustomerID %>_Text, { attributes: true });
            }

            //Voice Observer
            var observer<%=cust.CustomerID %>_Voice = new MutationObserver(function (mutationsList, observer) {

                for (let mutation of mutationsList) {

                    if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                        observer<%=cust.CustomerID %>_Voice.disconnect();

                        changeDelayString(<%=cust.CustomerID %>, "Voice");

                        if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput.value)) {
                          openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput.value = stringToNumber(openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput.value)
                        }

                        ensureIconIsRightColor('openSpinnerModalFor_<%:cust.CustomerID %>_VoiceInput', 'phone-volume-solid_<%:cust.CustomerID %>');

                        observer<%=cust.CustomerID %>_Voice.observe(inputElement<%=cust.CustomerID %>_Voice, { attributes: true });
                    }
                }
            });


            if (inputElement<%=cust.CustomerID %>_Voice) {
                observer<%=cust.CustomerID %>_Voice.observe(inputElement<%=cust.CustomerID %>_Voice, { attributes: true });
            }

            //Push Observer
            var observer<%=cust.CustomerID %>_Push = new MutationObserver(function (mutationsList, observer) {

                for (let mutation of mutationsList) {

                    if (mutation.type === 'attributes' && mutation.attributeName === 'value') {
                        observer<%=cust.CustomerID %>_Push.disconnect();

                        changeDelayString(<%=cust.CustomerID %>, "Push");

                        if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(openSpinnerModalFor_<%:cust.CustomerID %>_PushInput.value)) {
                          openSpinnerModalFor_<%:cust.CustomerID %>_PushInput.value = stringToNumber(openSpinnerModalFor_<%:cust.CustomerID %>_PushInput.value)
                        }

                        ensureIconIsRightColor('openSpinnerModalFor_<%:cust.CustomerID %>_PushInput', 'push-noti-icon_<%:cust.CustomerID %>');

                        observer<%=cust.CustomerID %>_Push.observe(inputElement<%=cust.CustomerID %>_Push, { attributes: true });
                    }
                }
            });


            if (inputElement<%=cust.CustomerID %>_Push) {
                observer<%=cust.CustomerID %>_Push.observe(inputElement<%=cust.CustomerID %>_Push, { attributes: true });
            }

            function stringToNumber(string) {
                var matchingIndex = valuesArrayInStrings_<%:cust.CustomerID %>.indexOf(string);
                return valuesArrayInNumbers_<%:cust.CustomerID %>[matchingIndex];
            }


            function numberToString(string) {
                var matchingIndex = valuesArrayInNumbers_<%:cust.CustomerID %>.indexOf(Number(string));
                return valuesArrayInStrings_<%:cust.CustomerID %>[matchingIndex];
            }


            function handleToggleNotificationMethod(customerId, deliveryMethodString) {
                const $inputElement = $(`#openSpinnerModalFor_${customerId}_${deliveryMethodString}Input`);
                let currentValue = $inputElement.val();

                if (parseInt(currentValue) >= 0) {
                    currentValue = "Off";
                    $(`#${deliveryMethodString}Span_${customerId}`).text('Off');
                    switch (deliveryMethodString) {
                        case "Voice":
                            changeVoiceSpinnerHighlightedValue(false, customerId, 0);
                            break;
                        case "Text":
                            changeTextSpinnerHighlightedValue(false, customerId, 0);
                            break;
                        case "Email":
                            changeEmailSpinnerHighlightedValue(false, customerId, 0);
                            break;
                    }

                } else {
                    currentValue = "No Delay";
                    $(`#${deliveryMethodString}Span_${customerId}`).text('No Delay');
                    switch (deliveryMethodString) {
                        case "Voice":
                            changeVoiceSpinnerHighlightedValue(false, customerId, 1);
                            break;
                        case "Text":
                            changeTextSpinnerHighlightedValue(false, customerId, 1);
                            break;
                        case "Email":
                            changeEmailSpinnerHighlightedValue(false, customerId, 1);
                            break;
                    }
                }

                $inputElement.val(currentValue).trigger('change');
                changeDelayString(customerId, deliveryMethodString)
            }


            function ensureIconIsRightColor(inputWithCurrentValueId, pathElementId) {
                const $svgPathElement = $(`#${pathElementId}`);
                const $inputElement = $(`#${inputWithCurrentValueId}`);
                let currentValue = $inputElement.val();

                if (parseInt(currentValue) >= 0) {
                    $svgPathElement.removeClass("initialFill");
                    $svgPathElement.addClass("toggleOnColor");
                } else {
                    $svgPathElement.removeClass("toggleOnColor");
                    $svgPathElement.addClass("initialFill");
                }
            }


            function changeDelayString(customerId, deliveryMethodString) {
                let inputValueToDisplay = $(`#openSpinnerModalFor_${customerId}_${deliveryMethodString}Input`).val();
                if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(inputValueToDisplay)) {
                    inputValueToDisplay = stringToNumber(inputValueToDisplay);
                }
                if (inputValueToDisplay !== null || inputValueToDisplay !== undefined) {
                    let displayText = numberToString(inputValueToDisplay);
                    $(`#${deliveryMethodString}Span_${customerId}`).text(displayText);
                }
            }

            function getIndexToHighlight(value) {
                if (valuesArrayInStrings_<%:cust.CustomerID %>.includes(value)) {
                    return valuesArrayInStrings_<%:cust.CustomerID %>.indexOf(value)
                } else {
                    return valuesArrayInNumbers_<%:cust.CustomerID %>.indexOf(Number(value))
                }
            }
        </script>
    </div>

    <%} %>
    <%--    hidden inputs to save the values from higher Accounts--%>
    <%
        foreach (NotificationRecipient recipientAtHigherLevel in assignedRecipients)
        {  %>

    <% if (recipientAtHigherLevel.NotificationType == eNotificationType.Email)
        { %>
    <input name="EmailFromHigherAccount" data-recipientname="<%=recipientAtHigherLevel.CustomerToNotify.FullName%>" data-recipientid="<%=recipientAtHigherLevel.CustomerToNotifyID%>" type="hidden" id="openSpinnerModalFor_<%=recipientAtHigherLevel.CustomerToNotifyID%>_EmailInput" value="<%=recipientAtHigherLevel.DelayMinutes %>" class="hiddenInputToCheckOnSubmit recipientCardAB" />

    <%  }

        if (recipientAtHigherLevel.NotificationType == eNotificationType.SMS)
        { %>
    <input name="SMSFromHigherAccount" type="hidden" id="openSpinnerModalFor_<%=recipientAtHigherLevel.CustomerToNotifyID%>_TextInput" value="<%=recipientAtHigherLevel.DelayMinutes %>" class="hiddenInputToCheckOnSubmit recipientCardAB" />

    <%  }

        if (recipientAtHigherLevel.NotificationType == eNotificationType.Phone)
        { %>
    <input name="VoiceFromHigherAccount" type="hidden" id="openSpinnerModalFor_<%=recipientAtHigherLevel.CustomerToNotifyID%>_VoiceInput" value="<%=recipientAtHigherLevel.DelayMinutes %>" class="hiddenInputToCheckOnSubmit recipientCardAB" />

    <%  }

        } %>
</div>

<style>



</style>

<script>
    var popLocation = "<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>";
</script>
