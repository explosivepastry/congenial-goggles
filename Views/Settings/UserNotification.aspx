<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Notifications
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Html.RenderPartial("UserLink", Model);%>

        <div class="col-12" id="notiSettings">
            <div class="rule-card_container w-100" style="margin-top: 54px;">
                <div class="card_container__top">
                    <div class="card_container__top__title" style="justify-content: space-between">
                        <div class="d-flex columnOnSm">
                            <%:Html.TranslateTag("Settings/UserNotification|Edit Notification Details", "Edit Notification Details")%>
                            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small;">
                                [<%= Model.FirstName%> <%= Model.LastName%>] - <%=Model.UserName%>
                            </div>
                        </div>
                        <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Settings/UserNotification|Notification Help","Notification Help") %>" data-bs-target=".pageHelp">
                            <div id="helpIconWrapper" class="" style="padding: 0.5rem;">
                                <%=Html.GetThemedSVG("circleQuestion") %>
                            </div>
                        </a>
                        <!-- pageHelp button modal -->
                        <div class="modal fade pageHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Settings/UserNotification|Notification Help","Notification Help")%></h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <% Html.RenderPartial("~\\Views\\Settings\\_NotificationHelpModalContent.ascx", Model);%>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Here will be popup window SMS Guide--%>
                    </div>
                </div>

                <% Customer c = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
                    if (c.CustomerID == Model.CustomerID && c.NotificationOptIn < c.NotificationOptOut)
                    { %>
                <br />
                <div class="form-group">
                    <div class="col-12 col-md-3">
                        <p class="card_container__top__title pb-0 mb-0" style="border-bottom: none;">
                            <%:Html.TranslateTag("Settings/UserNotification|System Notifications have been Opted Out")%>
                        </p>
                    </div>
                    <div class="col sensorEditFormInput">
                        <div class="input-group">
                            <input type="button" id="OptIn" class="btn btn-primary" value="Opt In" />
                        </div>
                    </div>
                </div>
                <%}%>

                <form method="post" action="/Settings/UserNotification/<%=Model.CustomerID %>">
                    <input id="custId" value="<%:MonnitSession.CurrentCustomer.CustomerID%>" hidden="hidden" />
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <%bool isEnterprise = ConfigData.AppSettings("IsEnterprise").ToBool();%>
                    <%AccountTheme acctTheme = AccountTheme.Find(Model.AccountID);%>
                    <!-- Email -->
                    <div id="emailErrorMessageAB" class="sensorEditForm row" style="margin-top: 1rem; display: none;">
                        <div class="col-12 col-md-3">
                        </div>
                        <div class="col sensorEditFormInput">
                            <div class="input-group mb-0">
                                <div class="col" style="color: red; margin-top: 1rem;">
                                    Invalid email address
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="sensorEditForm row" style="margin: 0.5rem 0;">
                        <div class="col-12 col-md-3" style="padding: 4px 0;">
                            <%:Html.TranslateTag("Settings/UserNotification|Email Address", "Email Address")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <div class="input-group mb-0">
                                <input id="NotificationEmail" name="NotificationEmail" type="text" class="form-control me-0" value="<%= Model.NotificationEmail%>" oninput="emailChange(event)">
                                <button class="btn btn-primary" title="<%:Html.TranslateTag("Settings/UserNotification|Send Test", "Send Test")%>" id="testEmail" value="<%:Html.TranslateTag("Test", "Test")%>" style="cursor: pointer;">
                                    <%=Html.GetThemedSVG("send") %>
                                </button>
                            </div>
                        </div>
                    </div>

                    <%if (MonnitSession.CustomerCan("See_Beta_Preview"))
                        {
                            List<CustomerPushMessageSubscription> pushMsgSubs = CustomerPushMessageSubscription.LoadByCustomerID(Model.CustomerID);
                            if (MonnitSession.CurrentCustomer.CustomerID == Model.CustomerID)
                            {
                    %>
                    <p class="subTitle">Push Notifications</p>
                    <!-- Push Message -->
                    <div id="AllowPushMessageRow" style="display: none;">
                        <div class="sensorEditForm row">
                            <div class="col-12 col-md-3">
                                <%:Html.TranslateTag("Settings/UserNotification|Allow Push Messages", "Allow Push Messages")%>
                            </div>
                            <div class="col sensorEditFormInput" style="max-width: 249px;">
                                <div style="padding: 8px 0;" class="input-group mb-0">
                                    <div class="form-check form-switch d-flex align-items-end ps-0">
                                        <input title="<%:Html.TranslateTag("Settings/UserNotification|Toggle Permission", "Toggle Permission")%>" class="form-check-input mx-2" style="margin-left: 0 !important;" type="checkbox" id="AllowPushMessage">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Push Message Periodic sync -->
                    <div id="AllowPeriodicSyncRow" style="display: none;">
                        <div class="sensorEditForm row">
                            <div class="col-12 col-md-3">
                                <%:Html.TranslateTag("Settings/UserNotification|Allow Periodic Sync", "Allow Periodic Sync")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <div class="input-group mb-0">
                                    <div class="form-check form-switch d-flex align-items-end ps-0">
                                        <input title="<%:Html.TranslateTag("Settings/UserNotification|Toggle Permission", "Toggle Permission")%>" class="form-check-input mx-2" style="margin-left: 0 !important;" type="checkbox" id="AllowPeriodicSync">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="currentDeviceRow" class="sensorEditForm row" style="display: none;">
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|Current Device", "Current Device")%>
                        </div>
                        <div class="col sensorEditFormInput" id="PushMsgTestDiv">
                            <div class="input-group mb-0">
                                <input placeholder="Device Name..." type="text" class="form-control me-0 widthAdjustment" id="PushMsgName" value="" />
                                <button class="btn btn-primary" title="<%:Html.TranslateTag("Settings/UserNotification|Send Test", "Send Test")%>" id="PushMsgTest" value="<%:Html.TranslateTag("Test", "Test")%>" style="cursor: pointer;">
                                    <%=Html.GetThemedSVG("send") %>
                                </button>
                            </div>
                        </div>
                    </div>

                    <% } %>
                    <!-- Other Customer enabled Push message devices -->
                    <div id="OtherPushMessageDevicesRow" <%=pushMsgSubs != null && pushMsgSubs.Count == 0 ? "style=display:none;" : ""%>>
                        <div class="col sensorEditFormInput">
                            <div style="padding-bottom: 0.75rem;" class="input-group mb-0">
                                <div class="col-12 col-md-3" style="display: flex; font-size: 13px; margin-top: .5rem; padding-left: 0">
                                    <%:Html.TranslateTag("Settings/UserNotification|Push Notification Enabled Devices", "Push Notification Enabled Devices")%>
                                </div>
                                <div>
                                    <%foreach (CustomerPushMessageSubscription pushMsgSub in pushMsgSubs)
                                        {%>
                                    <div class="col sensorEditForm row" id="OtherPushMessage_<%=pushMsgSub.Auth %>" data-id="<%=pushMsgSub.CustomerPushMessageSubscriptionID %>" data-name="<%=pushMsgSub.Name %>" style="flex-direction: column; gap: 0.5rem;">
                                        <div style="display: flex; width: 100%">
                                            <input placeholder="Device Name..." type="text" class="form-control me-0 widthAdjustment" style="width: 250px;" name="OtherPushMsgName_<%=pushMsgSub.CustomerPushMessageSubscriptionID %>" value="<%=pushMsgSub.Name %>" />
                                            <button type="button" class="btn btn-primary OtherPushMsgTest" title="<%:Html.TranslateTag("Settings/UserNotification|Send Test", "Send Test")%>" style="cursor: pointer; margin-left: -1px; border-top-left-radius: 0; border-bottom-left-radius: 0;"
                                                data-id="<%=pushMsgSub.CustomerPushMessageSubscriptionID %>" data-endpoint="<%=pushMsgSub.EndpointUrl%>" data-key="<%=pushMsgSub.P256DH%>" data-auth="<%=pushMsgSub.Auth%>">
                                                <%=Html.GetThemedSVG("send") %>
                                            </button>
                                            <div class="RemovePushMessage" data-id="<%=pushMsgSub.CustomerPushMessageSubscriptionID %>" title="<%:Html.TranslateTag("Settings/UserNotification|Remove device", "Remove device")%>">
                                                <%=Html.GetThemedSVG("delete") %>
                                            </div>
                                        </div>
                                        <%--<div id="OtherPushMsgTestMsg_<%=pushMsgSub.CustomerPushMessageSubscriptionID %>"></div>--%>
                                    </div>
                                    <%}%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>

                    <p class="subTitle">Call and Text Notifications</p>
                    <%--once we start using the other opt out code remove below code--%>
                    <% if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(Model.NotificationEmail))
                        {%>
                    <div class="form-group">
                        <div class="aSettings__title">
                        </div>
                        <div class="col-sm-5 col-12">
                            <span title="<%: UnsubscribedNotificationEmail.LoadByEmailAddress(Model.NotificationEmail).Reason %>"><%: Html.Label("Email has been Opted Out", new { id = "emails", style = "color:Red;" })%></span>
                            <span id="optInLink"><a href="/Settings/UserNotificaitonOptIn/?email=<%:Model.NotificationEmail %>" data-email="<%: Model.NotificationEmail %>" onclick="optIn(this); return false"><%=Html.TranslateTag("Opt in", "Opt in") %></a></span>
                        </div>
                    </div>
                    <%}%>
                    <div class="sensorEditForm row">
                        <%
                            if (!string.IsNullOrEmpty(acctTheme.FromPhone) && !isEnterprise)
                            {%>
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|Delivery Method", "Delivery Method")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <div class="form-check form-switch d-flex align-items-end ps-0">
                                <label class="form-check-label"><%:Html.TranslateTag("Settings/UserNotification|External Provider", "External Provider")%></label>
                                <input onchange="toggleInputAbility()" class="form-check-input mx-2" type="checkbox" id="toggle-event" name="DirectSMS" <%:Model.DirectSMS ? "checked='checked'" : ""%>>
                                <label class="form-check-label"><%:Html.TranslateTag("Settings/UserNotification|Direct Delivery", "Direct Delivery")%></label>

                            </div>
                        </div>
                        <%}
                            else
                            {%>


                        <%} %>
                    </div>
                    <div class="sensorEditForm row externalSMS">
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|SMS Provider", "SMS Provider")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <select onchange="toggleInputAbility()" id="UISMSCarrierID" name="UISMSCarrierID" class="form-select">
                                <option value="0"><%: Html.TranslateTag("Settings/UserNotification|Select One", "Select One")%></option>
                                <%foreach (var carrier in (SelectList)ViewData["Carriers"])
                                    {%>
                                <option <%= carrier.Value.ToLong() == Model.UISMSCarrierID ? "selected='selected'" : "" %> value="<%:carrier.Value%>"><%= carrier.Text %></option>
                                <%}%>
                            </select>
                        </div>
                    </div>
                    <div id="SMSCallErrorMessage" class="" style="display: none;">
                        <div class="col-12 col-md-3"></div>
                        <div class="col" style="color: red;">
                            Invalid mobile number
                        </div>
                    </div>
                    <div class="sensorEditFormexternalSMS">
                        <div class="col-12 col-md-3"></div>
                        <div class="col externalSMSFormat">
                            <%ViewData["Phone"] = Model.NotificationPhone; %>
                            <%:Html.Partial("~/Views/Customer/ExternalSMSProviderFormat.ascx", Model.SMSCarrier) %>
                        </div>
                    </div>
                    <div class="sensorEditForm row">
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|SMS Mobile Number", "SMS Mobile Number")%>
                            <span id="NotificationPhoneFormat" style="color: #aaa; font-size: .8em;"></span>
                        </div>
                        <div class="col sensorEditFormInput">
                            <div class="input-group mb-0">
                                <input id="NotificationPhone" name="NotificationPhone" type="text" class="form-control me-0" style="text-align: right;" value="<%: Model.NotificationPhone %>" oninput="smsChange()">
                                <input type="hidden" id="NotificationPhoneISOCode" name="NotificationPhoneISOCode" value="<%=Model.NotificationPhoneISOCode %>" />
                                <button class="btn btn-primary" id="testSMS" title="<%:Html.TranslateTag("Settings/UserNotification|Send Test", "Send Test")%>" value="Test" style="cursor: pointer; border-top-right-radius: .25rem; border-bottom-right-radius: .25rem;">
                                    <%=Html.GetThemedSVG("send") %>
                                </button>

                                <%if (!string.IsNullOrEmpty(acctTheme.FromPhone))
                                    { %>
                                <span class="ms-2" style="margin-top: 10px">
                                    <span class="directSMS" style="">
                                        <span class="smsCreditCount creditCount" title="<%:Html.TranslateTag("Settings/UserNotification|Notification Credits are required for direct delivery of SMS messages and voice notifications", "Notification Credits are required for direct delivery of SMS messages and voice notifications")%>."></span>
                                        <%:Html.TranslateTag("Settings/UserNotification|Credits", "Credits")%>
                                    </span></span>
                                <%} %>

                                <%--                                <span style="width: 100%;">
                                    <%string optOutMsg = "Upon filling out this field you acknowledge that you’ve opted-in to receiving SMS messages from us.  Empty the field to opt-out.";%>
                                    <%=Html.TranslateTag("Settings/UserNotification|" + optOutMsg, optOutMsg)%>
                                </span>--%>
                            </div>
                        </div>
                        <div class="sensorEditForm row directSMS">
                            <div class="col-12 col-md-3">
                            </div>
                            <div class="col sensorEditFormInput smsFormat">
                                <%ViewData["Phone"] = Model.NotificationPhone; %>
                            </div>
                        </div>

                    </div>

                    <!-- VOICE CALL NUMBER -->
                    <%if (!string.IsNullOrEmpty(acctTheme.FromPhone) && !isEnterprise)
                        { %>
                    <div class="sensorEditForm row">
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|Voice Call Number", "Voice Call Number")%>
                            <span id="NotificationPhone2Format" style="color: #aaa; font-size: .8em;"></span>
                        </div>
                        <div class="col sensorEditFormInput">
                            <div class="input-group d-flex align-items-center mb-0">
                                <input id="NotificationPhone2" name="NotificationPhone2" class="form-control me-0" type="text" value="<%= Model.NotificationPhone2%>" style="text-align: right;">
                                <input type="hidden" id="NotificationPhone2ISOCode" name="NotificationPhone2ISOCode" value="<%=Model.NotificationPhone2ISOCode %>" />
                                <button class="btn btn-primary rounded-end" title="<%:Html.TranslateTag("Settings/UserNotification|Send Test", "Send Test")%>" id="testVoice" value="Test" style="cursor: pointer;">
                                    <%=Html.GetThemedSVG("send") %>
                                </button>
                                <span class="ms-2">
                                    <span class="voiceCreditCount creditCount" title="<%:Html.TranslateTag("Settings/UserNotification|Notification Credits are required for direct delivery of SMS messages and voice notifications", "Notification Credits are required for direct delivery of SMS messages and voice notifications")%>."></span>
                                    <%:Html.TranslateTag("Settings/UserNotification|Credits", "Credits")%>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="">
                        <div class="col-12 col-md-3"></div>
                        <div id="voiceCallErrorMessage" class="col" style="color: red; display: none;">
                            Invalid voice call number
                        </div>
                    </div>

                    <%} %>

                    <div class="form-group directSMS">
                        <div class="aSettings__title">
                        </div>
                        <div class="col-sm-9 col-12 lgBox smsFormat">
                            <%ViewData["Phone"] = Model.NotificationPhone; %>
                        </div>
                    </div>

                    <% bool AllowNotifications = true;
                        if (Model.NotificationOptOut > DateTime.MinValue && Model.NotificationOptIn < Model.NotificationOptOut)
                        {
                            //They have Opted out... AND they have not opted back in
                            //null values in both will allow notifications to send
                            AllowNotifications = false;
                        }
                    %>
                    <div class="sensorEditForm row externalSMS ps-0" hidden="hidden">
                        <div class="col-12 col-md-3">
                            <%:Html.TranslateTag("Settings/UserNotification|Notifications", "Notifications")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input id="custId" value="<%:Model.CustomerID%>" hidden="hidden" />

                            <input type="hidden" id="optIn" value="<%: Html.TranslateTag("Settings/UserNotification|On", "On")%>" class="<%:(AllowNotifications) ? "btn-on" : "btn-off"%>" />

                            <input type="hidden" id="optOut" value="<%: Html.TranslateTag("Settings/UserNotification|Off", "Off")%>" class="<%:(AllowNotifications) ? "btn-off" : "btn-on"%>" />

                        </div>
                    </div>

                    <% List<Notification> rule = Notification.LoadByCustomerID(Model.CustomerID);%>
                    <% List<NotificationRecipient> nr = NotificationRecipient.LoadByCustomerToNotifyID(Model.CustomerID);%>

                    <p class="useAwareState"></p>
                    <% if (rule.Count > 0)
                        { %>
                    <div class="sensorEditForm row">
                        <div class="col-12 col-md-3" style="font-weight: 600;">
                            <%:Html.TranslateTag("Settings/UserNotification|Rules", "Rules")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <%:Html.TranslateTag("Settings/UserNotification|User Assigned to:", "User Assigned to:")%> <a href="#myModal" data-bs-toggle="modal"><b><span onclick="$('#myModal').modal('toggle');" style="cursor: pointer;" class="ruleCount"><%:rule.Count%> Rules</span></b></a>
                            <span id="RulesAssignedFormat" style="color: #aaa; font-size: .8em;"></span>
                        </div>
                    </div>
                    <%}%>

                    <%Html.RenderPartial("_ReportsAssignedTo", Model);%>

                    <p class="useAwareState"></p>
                    <div class="form-group">
                        <div class="col-sm-12 col-12 text-end" id="notiSettingsSave" style="margin-right: -2px">
                            <%--                            <div id="saveMessage" class="me-3" style="color: green!important; margin-left: -55px">
                            </div>--%>
                            <button id="saveButtonAB" type="Submit" value="<%:Html.TranslateTag("Save", "Save")%>" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary me-3">
                                <%:Html.TranslateTag("Save", "Save")%>
                            </button>
                            <button class="btn btn-primary me-3" id="saving" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                <%: Html.TranslateTag("Settings/UserNotification|Saving", "Saving")%>...
                            </button>
                        </div>
                    </div>
                </form>

                <%--//#region PWA test code
                <div class="row">
                    Endpoint: <input type="text" id="endpoint" value="" />
                    p256: <input type="text" id="p256dhKey" value="" />
                    auth: <input type="text" id="authKey" value="" />
                    <textarea id="pushMsg" rows="10" cols="50">{"title":"iMonnit debug", "body":"body message from non-pwa device", "icon":"/PWAIcon/512x512/", "tag":"debugTag"}</textarea>
                    <button type="button" class="btn btn-primary" id="sendPushMsg">Send Push Message</button>
                    <h3 id="sendPushMsgResult"></h3>
                </div>
                //#endregion--%>
            </div>
        </div>
    </div>

    <%--  ----------- MODAL IN A MODAL --------%>
    <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content modal_container">

                <div class="modal-header">
                    <div class="modal-user msg-user-icon"><%=Html.GetThemedSVG("accountDetails") %></div>
                    <div class="modal-select">
                        <h4 style="font-size: 18px;" class="modal-title-select"><%= Html.TranslateTag("Rules Assigned to:")%> <%:Model.FullName%></h4>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>

                </div>
                <hr style="margin: 0 24px;" />
                <h4 class="modal-delay-title"></h4>
                <div class="modal-body modal-dialog-scrollable">
                    <div class="modal-dialog-scrollable">
                        <% 
                            Dictionary<string, string> svgMap = new Dictionary<string, string>()
                            {
                                { "Email", "envelope" },
                                { "SMS", "messages" },
                                { "Local_Notifier", "app13" },
                                { "Control", "app76" },
                                { "Phone", "phone" },
                                { "Push_Message", "ringingBell" },
                                { "Thermostat", "app97" }
                            };
                            string defaultSvg = "envelope";

                            //foreach (Notification item in rule)
                            //{
                            foreach (NotificationRecipient recip in nr.OrderBy(r => r.NotificationID))
                            {
                                Notification item = rule.Where(r => r.NotificationID == recip.NotificationID).FirstOrDefault();
                                if (item == null)
                                {
                                    continue;
                                }
                                string notificationType = recip.NotificationType.ToString();
                                string svgIcon = svgMap.ContainsKey(notificationType) ? svgMap[notificationType] : defaultSvg;

                        %>
                        <div style="justify-content: space-around;" class="msg-list">
                            <div id="ruleWrapper_<%:item.NotificationID %>" data-rulewrapperid="<%:item.NotificationID %><%:recip.NotificationType.ToLong() %>" class="True btn-lg cardStyle">
                                <div class="svgIcon"><%: Html.GetThemedSVG(svgIcon) %></div>

                                <a title="<%:Html.TranslateTag("View Rule History") %>" href="/Rule/History/<%:item.NotificationID %>" style="width: 150px;"><%:item.Name %></a>

                                <%if (MonnitSession.CustomerCan("Customer_Edit_Self") && MonnitSession.CustomerCan("Notification_Edit"))
                                    {%>
                                <div title="<%:Html.TranslateTag("Add/Remove Self from Rule Recipient list") %>" style="cursor: pointer" data-notificationid="<%:item.NotificationID%>" data-notitype="<%:recip.NotificationType.ToLong() %>" onclick="saveRecipients(this);" id="checkmark"><%: Html.GetThemedSVG("check-circle") %></div>

                                <%} %>
                            </div>
                        </div>
                        <%}
                            //}%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .subTitle {
            border-top: 1px solid #eee;
            margin-bottom: 0;
            font-size: 0.95rem !important;
            font-weight: 600;
            padding: 0.5rem 0;
        }

        .True .svgIcon svg {
            fill: white !important;
        }

        .cardStyle {
            width: 261px;
            font-weight: bold;
            font-size: 1.0rem;
            display: flex;
            margin: 10px 0 10px;
            gap: 1rem;
            box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 5%) 0px 3px 6px;
            align-content: center;
            background-color: var(--option-menu-color);
            color: black;
        }

        .True {
            background-color: var(--options-hover-color);
            color: white;
        }

            .True a {
                color: white;
            }



            .True > #checkmark svg {
                position: relative;
                width: 40px;
                height: 20px;
                margin: 0;
                padding: 0;
                padding-left: 5px;
                border-radius: 12px;
                fill: white;
            }

        #checkmark {
            fill: lightgrey;
        }

        .colorGrey {
            color: var(--options-text-color);
            fill: var(--options-text-color);
        }
    </style>

    <!-- Help Modal -->
    <div class="modal fade UserNotificationHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="<%: Html.TranslateTag("Settings/UserNotification|Close", "Close")%>">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Settings/UserNotification|Notification Settings", "Notification Settings")%></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Settings/UserNotification|Direct Delivery", "Direct Delivery")%>
                        </div>
                        <div class="word-def">
                            <%:Html.TranslateTag("Settings/UserNotification|Direct delivery offers the highest level of reliablity available.  Deliverability is tracked and visible in the sent notifications", "Direct delivery offers the highest level of reliablity available. Deliverability is tracked and visible in the sent notifications")%>.
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Settings/UserNotification|External Provider", "External Provider")%>
                        </div>
                        <div class="word-def">
                            <%:Html.TranslateTag("Settings/UserNotification|External Providers are generally reliable but no deliverability tracking is available.  Because these providers do not charge a fee to send the message, notification credits are not required for sending these SMS messages", "External providers are generally reliable but no deliverability tracking is available.  Because these providers do not charge a fee to send the message, notification credits are not required for sending these SMS messages")%>.
                            <br />
                            <hr />
                            <%:Html.TranslateTag("Settings/UserNotification|With either method standard text message charges may apply from your wireless provider", "With either method standard text message charges may apply from your wireless provider")%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                </div>
            </div>
        </div>
    </div>
    <!-- End help button -->

    <script type="text/javascript">
        var optout = "<%=Html.TranslateTag("Settings/UserNotification|By Opting Out of Notifications, this User will not longer to be able to recieve device notifications. Are you sure you want to Opt Out?", "By Opting Out of Notifications, this User will not longer to be able to recieve device notifications. Are you sure you want to Opt Out?")%>";
        var consumeCredits = "<%=Html.TranslateTag("Settings/UserNotification|This will consume Notification Credits do you want to continue?", "This will consume Notification Credits do you want to continue?")%>";
        var removeFailed = "<%:Html.TranslateTag("Settings/UserNotification|Device was not able to be removed", "Device was not able to be removed")%>";
        var serverWait = "<%:Html.TranslateTag("Settings/UserNotification|Success: It can take up to 12 hours for this change to take effect on all servers", "Success: It can take up to 12 hours for this change to take effect on all servers")%>";

        $(function () {
            // #region PWA test code
            //$('#sendPushMsg').click(function (e) {
            //    e.preventDefault();
            //    $('#sendPushMsgResult').html('');
            //    var msg = $('#pushMsg').val();
            //    var endpoint = $('#endpoint').val();
            //    var p256dhKey = $('#p256dhKey').val();
            //    var auth = $('#authKey').val();

            //    $.post('/Setup/SendPushMessage/', { msg: msg, endpoint: endpoint, p256dhKey: p256dhKey, auth: auth }, function (data) {
            //        $('#sendPushMsgResult').html(data);
            //    });
            //});
            //#endregion



            window.location.search == '?focus' ? selectMobileNumber() : '';
            smsChange();
            voiceChange();

            //Bootstrap toggle -- http://www.bootstraptoggle.com
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
                autoInsertDialCode: true,
                defaultCountry: "us",
                //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                preferredCountries: ['us'],
                responsiveDropdown: true //set drop down width to match input width
                //utilsScript: "/Scripts/jqueryPlugins/libphonenumber/build/utils.js"
            }).keyup(voiceChange).change(voiceChange);
            voiceChange();


        <%if (MonnitSession.CustomerCan("See_Beta_Preview"))
        {%>
            $('.OtherPushMsgTest').click(function (e) {
                e.preventDefault();

                var id = $(this).attr('data-id');
                var endpoint = $(this).attr('data-endpoint');
                var p256dhKey = $(this).attr('data-key');
                var authKey = $(this).attr('data-auth');
                console.log(id, endpoint, p256dhKey, authKey)
                $('#OtherPushMsgTestMsg_' + id).html(`endpoint: ${endpoint} <br/> p256dhkey: ${p256dhKey} <br/> auth: ${authKey}`);

                var obj = $(this);
                var oldHtml = $(this).html();

                $.post('/Setup/SendPushMessage/', { endpoint: endpoint, p256dhKey: p256dhKey, auth: authKey }, function (data) {
                    showSimpleMessageModal(data);
                    obj.html(oldHtml);
                });
            });

            $('.RemovePushMessage').click(function () {
                var id = $(this).attr('data-id');
                var obj = $(this);

                $.post('/Setup/DeletePushMessageSubscriptionByID/', { id: id }, function (data) {
                    if (data == 'Success') {
                        obj.parent().parent().remove();
                    }
                    showSimpleMessageModal(data);
                });
            });

            // Check if PWA App
            if (platform.isStandalone) {

                // Need to check if serviceWorker has been registered for devices that don't trigger an install on '/Setup/InstallApp/' ex: iOS devices
                if ("serviceWorker" in navigator) {
                    //$('#PushMsgTestMsg').html('');
                    //printTestMsg('page is standalone');
                    //$('#AllowPushMessageRow').show();

                    navigator.serviceWorker.getRegistrations().then(regs => {
                        //printTestMsg('registration retrieved');
                        if (regs == null || (regs != null && regs.length == 0)) {
                            //printTestMsg('registration attempt');
                            navigator.serviceWorker.register("/pwa.js")
                                .then(function (registration) {
                                    callATH();
                                    // Registration was successful
                                    //printTestMsg("ServiceWorker registration successful with scope: " + registration.scope);

                                }).catch(function (err) { // registration failed :(
                                    //printTestMsg("ServiceWorker registration failed: " + err);
                                });
                        }
                    });
                }

                // Check if Notification possible
                if ('Notification' in window) {
                    $('#AllowPushMessageRow').show();

                    //printTestMsg('notification is possible');
                    //printTestMsg('service worker exists: ' + ("serviceWorker" in navigator));
                    //navigator.serviceWorker.getRegistrations().then(regs => {
                    //    printTestMsg('service worker registration:');

                    //    for (var i = 0; i < regs.length; i++) {
                    //        var reg = regs[i];
                    //        printTestMsg('reg' + i +': ' + JSON.stringify(reg));
                    //    }
                    //});

                    navigator.serviceWorker.ready.then(reg => {
                        //printTestMsg('service worker ready');

                        reg.pushManager.getSubscription().then(sub => {

                            //printTestMsg('get sub: ' + JSON.stringify(sub));

                            if (sub == undefined) {
                                //printTestMsg('get sub - show enable');

                                $('#AllowPushMessage')[0].checked = false;
                                $('#PushMsgName').prop('disabled', true);
                            } else {
                                //printTestMsg('get sub - show disable');
                                $('#AllowPushMessage')[0].checked = true;
                                var subJson = JSON.parse(JSON.stringify(sub));
                                var subID = $('#OtherPushMessage_' + subJson.keys.auth).attr('data-id');
                                var subName = $('#OtherPushMessage_' + subJson.keys.auth).attr('data-name');
                                $('#PushMsgName').attr('name', 'PushMsgName_' + subID);
                                $('#PushMsgName').val(subName);
                                $('#OtherPushMessage_' + subJson.keys.auth).remove();
                                $('#PushMsgName').prop('disabled', false);

                                //update the database with latest sub info
                                if (window.Notification.permission == 'granted') {
                                    updatePushMessageSub(sub, 'AllowPushMessage', false);
                                    $('#currentDeviceRow').show();
                                } else {
                                    deletePushMessageSub(sub, 'AllowPushMessage', false);
                                    sub.unsubscribe();
                                }
                            }
                        });

                        if ('periodicSync' in reg) {
                            //const tags = await reg.periodicSync.getTags();
                            reg.periodicSync.getTags().then(tags => {
                                if (tags.includes('pushsub-sync')) {
                                    $('#AllowPeriodicSync')[0].checked = true;
                                } else {
                                    $('#AllowPeriodicSync')[0].checked = false;
                                }
                            });

                            var shouldShowExtras = true;

                            $('#AllowPeriodicSyncRow').show();

                            $('#AllowPeriodicSync').click(function () {
                                var enable = $(this).is(':checked');

                                navigator.serviceWorker.getRegistration().then(regOnClick => {
                                    regOnClick.periodicSync.getTags().then(async tagsOnClick => {

                                        if (enable) {
                                            // Request permission
                                            const periodicSyncStatus = await navigator.permissions.query({
                                                name: 'periodic-background-sync',
                                            });

                                            if (periodicSyncStatus.state === 'granted') {
                                                $('#AllowPeriodicSync')[0].checked = true;
                                                if (!tagsOnClick.includes('pushsub-sync')) {
                                                    // https://wicg.github.io/background-sync/spec/PeriodicBackgroundSync-index.html#constants
                                                    // As of 11/22/22 minInterval can go as low as 43200000 (12 hours)

                                                    // register periodicSync
                                                    await regOnClick.periodicSync.register('pushsub-sync', {
                                                        //minInterval: 24 * 60 * 60 * 1000 // 1 day
                                                        minInterval: 12 * 60 * 60 * 1000 // 12 hours
                                                    });
                                                }

                                            } else {
                                                $('#AllowPeriodicSync')[0].checked = false;
                                                if (tagsOnClick.includes('pushsub-sync')) {
                                                    // remove registered periodicSync
                                                    await regOnClick.periodicSync.unregister('pushsub-sync');
                                                }
                                            }
                                        }
                                        else {
                                            $('#AllowPeriodicSync')[0].checked = false;
                                            if (tagsOnClick.includes('pushsub-sync')) {
                                                // remove registered periodicSync
                                                await regOnClick.periodicSync.unregister('pushsub-sync');
                                            }
                                        }
                                    });
                                });
                            });
                        }
                    });
                    //}).catch (error =>
                    //    showSimpleMessageModal('sw catch: ' + JSON.stringify(error))
                    //);

                    $('#AllowPushMessage').click(function (e) {
                        var enable = $(this).html() == "Enable";
                        //$('#PushMsgTestDiv').show();
                        //printTestMsg('AllowPushMessage click');

                        navigator.serviceWorker.getRegistration().then(reg => {

                            // Check PushSubscription
                            reg.pushManager.getSubscription().then(sub => {
                                if (sub == undefined) {
                                    //printTestMsg('AllowPushMessage ask user to register');

                                    // ask user to register for Push
                                    window.Notification.requestPermission().then(reqPermission => {
                                        //printTestMsg('AllowPushMessage permission: ' + reqPermission);
                                        if (reqPermission == "granted") {
                                            // subscribe to push service
                                            var appServerKey = urlB64ToUint8Array('<%:ConfigData.AppSettings("Vapid_PublicKey")%>');
                                            reg.pushManager.subscribe({ userVisibleOnly: true, applicationServerKey: appServerKey }).then(newSub => {
                                                updatePushMessageSub(newSub, 'AllowPushMessage', true);

                                                //printTestMsg('AllowPushMessage subscribe json: ' + JSON.stringify(newSub));
                                            });

                                        } else {
                                            //console.log("push denied: " + JSON.stringify(reqPermission));
                                        }
                                    });
                                }
                                else {
                                    //printTestMsg('AllowPushMessage sub found');

                                    // You have subscription
                                    if (enable) {
                                        //printTestMsg('AllowPushMessage sub found - update');
                                        //update the database with latest sub info
                                        updatePushMessageSub(sub, 'AllowPushMessage', true);
                                        $('#PushMsgName').prop('disabled', false);
                                    } else {
                                        //printTestMsg('AllowPushMessage sub found - delete');
                                        // Remove subscription
                                        deletePushMessageSub(sub, 'AllowPushMessage', true);
                                        sub.unsubscribe();
                                        $('#PushMsgName').prop('disabled', true);
                                    }
                                }
                            });

                            // Check if periodicSync possible
                            if ('periodicSync' in reg) {

                                // Check periodic-sync tags
                                reg.periodicSync.getTags().then(async tagsOnClick => {
                                    if (enable) {
                                        // Request permission
                                        const periodicSyncStatus = await navigator.permissions.query({
                                            name: 'periodic-background-sync',
                                        });

                                        if (periodicSyncStatus.state === 'granted') {
                                            if (!tagsOnClick.includes('pushsub-sync')) {
                                                // https://wicg.github.io/background-sync/spec/PeriodicBackgroundSync-index.html#constants
                                                // As of 11/22/22 minInterval can go as low as 43200000 (12 hours)

                                                // register periodicSync
                                                await reg.periodicSync.register('pushsub-sync', {
                                                    //minInterval: 24 * 60 * 60 * 1000 // 1 day
                                                    minInterval: 12 * 60 * 60 * 1000 // 12 hours
                                                });
                                            }

                                        } else {
                                            if (tagsOnClick.includes('pushsub-sync')) {
                                                // remove registered periodicSync
                                                await reg.periodicSync.unregister('pushsub-sync');
                                            }
                                        }
                                    }
                                    else {
                                        if (tagsOnClick.includes('pushsub-sync')) {
                                            // remove registered periodicSync
                                            await reg.periodicSync.unregister('pushsub-sync');
                                        }
                                    }
                                });
                            }
                        });
                    });

                    $('#PushMsgTest').click(function (e) {
                        e.preventDefault();

                        navigator.serviceWorker.getRegistration().then(reg => {
                            // Check PushSubscription
                            reg.pushManager.getSubscription().then(sub => {
                                if (sub != undefined) {
                                    // You have subscription
                                    var subscriptionJson = JSON.parse(JSON.stringify(sub));
                                    var endpoint = subscriptionJson.endpoint;
                                    var p256dhKey = subscriptionJson.keys.p256dh;
                                    var authKey = subscriptionJson.keys.auth;
                                    $('#PushMsgTestMsg').html(`endpoint: ${endpoint} <br/> p256dhkey: ${p256dhKey} <br/> auth: ${authKey}`)

                                    //var _endpoint = $('#endpoint').val();
                                    //var _p256dhKey = $('#p256dhKey').val();
                                    //var _auth = $('#authKey').val();

                                    //if (_endpoint == null || _endpoint.length == 0)
                                    //    $('#endpoint').val(endpoint);
                                    //if (_p256dhKey == null || _p256dhKey.length == 0)
                                    //    $('#p256dhKey').val(p256dhKey);
                                    //if (_auth == null || _auth.length == 0)
                                    //    $('#authKey').val(authKey);

                                    var obj = $(this);
                                    var oldHtml = $(this).html();
                                    $.post('/Setup/SendPushMessage/', { endpoint: endpoint, p256dhKey: p256dhKey, auth: authKey }, function (data) {
                                        showSimpleMessageModal(data);
                                        obj.html(oldHtml);
                                    });
                                }
                            });
                        });
                    });
                }
            }
            <%}%>
        });

        function printTestMsg(msg) {
            var newMsg = $('#PushMsgTestMsg').html();
            newMsg += "<br/>" + msg;
            //var commaCount = 0;
            //for (var i = 0; i < msg.length; i++) {
            //    var currentChar = msg[i];
            //    if (currentChar == ',') {
            //        commaCount++;
            //    }

            //    if (commaCount == 3) {
            //        newMsg += "<br/>";
            //        commaCount = 0;
            //    }

            //    newMsg += msg[i];
            //}
            $('#PushMsgTestMsg').html(newMsg);
        }

        <%
        string appName = MonnitSession.CurrentStyle("MobileAppName");
        string url = MonnitSession.CurrentTheme != null ? MonnitSession.CurrentTheme.Domain : "";
        string[] urlSplit = url.Split('.');
        string themeUrl = "";
        for (int i = urlSplit.Length - 1; i >= 0; i--)
        {
            if (i == 0)
                themeUrl += urlSplit[i];
            else
                themeUrl += urlSplit[i] + ".";
        }
        %>
        function callATH() {

            var ath = addToHomescreen({
                appID: "<%:themeUrl%>",
                appName: "<%:appName%>",

                onCanInstall: function (_platform, _instance) {
                    platform = _platform;
                    //printTestMsg(`---- onCanInstall - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    platformPrompt(platform);
                },

                onInstall: function (_platform) {
                    //printTestMsg(`---- onAppInstalled - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);
                    //setTimeout(function () {
                    //    checkPlatform();

                    //    if (platform.isStandalone) {
                    //        console.log('overview redirect');
                    //        window.location.href = "/Overview";
                    //    }
                    //}, 1000) // 1 second
                },

                onBeforeInstallPrompt: function (_platform) {
                    platform = _platform;
                    //printTestMsg(`---- onBeforeInstallPrompt - platform ----\r\n ${JSON.stringify(_platform)} \r\n----------------`);

                    platformPrompt(platform);
                }

            });
        }



        $('#testSMS').click(function (e) {
            e.preventDefault();
            testSMS()
        });
        $('#testVoice').click(function (e) {
            e.preventDefault();
            testVoice();
        });
        $('#testEmail').click(function (e) {
            e.preventDefault();
            testEmail();
        });

        $('#OptIn').click(function () {
            {
                $.get('/Notification/NotificationOptIn/' + $('#custId').val(), function (data) {
                    if (data == 'Success') {
                        window.location.reload();
                    }
                });
            };
        });

        $('#emailVerify').click(function () {
            $.get('/Notification/CreateTemporaryValidation/' + $('#custId').val() + '?type=email&typeValue=<%:Model.NotificationEmail%>', function (data) {
                if (data == 'Success') {
                    window.location.reload();
                }
            });
        });

        $('#smsVerify').click(function () {
            $.get('/Notification/CreateTemporaryValidation/' + $('#custId').val() + '?type=sms&typeValue=<%:Model.NotificationPhone%>', function (data) {
                if (data == 'Success') {
                    window.location.reload();
                }
            });
        });

        $('#voiceVerify').click(function () {
            $.get('/Notification/CreateTemporaryValidation/' + $('#custId').val() + '?type=voice&typeValue=<%:Model.NotificationPhone%>', function (data) {
                if (data == 'Success') {
                    window.location.reload();
                }
                else {
                    $('#voiceNotice').html("Call Failed");
                }
            });
        });

        $('#smsCodeSubmit').click(function () {
            $.get('/Notification/NotificationValidation?token=' + $('#smsCodeInput').val(), function (data) {
                if (data == 'Success') {
                    window.location.reload();
                }
                else if (data == 'InvalidCode') {
                    $('#smsError').html("Invalid Code");
                }
                else {
                    $('#smsError').html("Failed");
                }
            });
        });

        $('#optOut').click(function () {
            if (confirm(optout)) {
                $.get('/Notification/NotificationOptOut/' + $('#custId').val(), function (data) {
                    if (data == 'Success') {
                        window.location.reload();
                    }
                });
            };
        });

        $('#optIn').click(function () {
            {
                $.get('/Notification/NotificationOptIn/' + $('#custId').val(), function (data) {
                    if (data == 'Success') {
                        window.location.reload();
                    }
                });
            };
        });

        setProvider();
        setExternalSMS();

        var voiceCountryCode = "";
        function voiceChange() {
            var voiceInput = $("#NotificationPhone2");
            var voicedigits = voiceInput.val()?.replace(/\D/g, '');
            $('.displayNotificationPhone2').html(voicedigits);

            if (voicedigits !== undefined) {
                if (voicedigits.length > 0) {
                    $('#voiceCallErrorMessage').show();
                    toggleSaveButtonAbility()
                } else {
                    $('#voiceCallErrorMessage').hide();
                    toggleSaveButtonAbility()
                }

                if (voicedigits.length > 8 && voicedigits.length < 16) {
                    $('#testVoice').show();
                    $('#voiceCallErrorMessage').hide();
                    toggleSaveButtonAbility()
                }
                else {
                    $('#testVoice').hide();
                }
            }

            if (voiceInput.attr('placeholder')) {
                $('#NotificationPhoneFormat2').html("(" + voiceInput.attr('placeholder') + ")");
            } else {
                $('#NotificationPhoneFormat2').html("");
            }
            $('#NotificationPhone2ISOCode').val(voiceInput.intlTelInput("getSelectedCountryData").iso2);
            <%if (!string.IsNullOrEmpty(acctTheme.FromPhone))
        {%>
            var code = voiceInput.intlTelInput("getSelectedCountryData").iso2;
            if (code && code != voiceCountryCode) {
                voiceCountryCode = code;
                setVoiceCost();
            }
            <%}%>
        }

        function setVoiceCost() {
            $.get("/Customer/CalcCredits?code=" + voiceCountryCode + "&voice=true", function (data) {
                $('.voiceCreditCount').html("x " + data);
            });
        }

        function selectMobileNumber() {
            //For selecting the mobile phone input after pressing the Profile Information Progress button for Mobile Number
            let mobileNum = document.getElementById('NotificationPhone')
            mobileNum.scrollIntoView()
            mobileNum.focus()
            mobileNum.classList.add('selectMaintenance')
        }

        var smsCountryCode = "";
        function smsChange() {
            var smsInput = $("#NotificationPhone");
            var digits = smsInput.val().replace(/\D/g, '');
            $('.displayNotificationPhone').html(digits);


            if (digits.length > 0) {
                $('.expectedDigits').css("color", "red").show();
                toggleSaveButtonAbility()
            } else {
                $('.expectedDigits').hide();
                toggleSaveButtonAbility()
            }

            if (expectedDigits == digits.length) {
                $('#testSMS').show();
                $('.expectedDigits').hide();
                toggleSaveButtonAbility()
            }
            else {
                $('#testSMS').hide();
            }

            if ($('#toggle-event').prop('checked')) {
                if (digits.length > 0) {
                    $('#SMSCallErrorMessage').show();
                    toggleSaveButtonAbility()
                } else {
                    $('#SMSCallErrorMessage').hide();
                    toggleSaveButtonAbility()
                    $('#testSMS').show();
                }
                if (digits.length > 8 && digits.length < 16) {
                    $('#SMSCallErrorMessage').hide();
                    toggleSaveButtonAbility()
                    $('#testSMS').show();
                }
            } else {
                $('#SMSCallErrorMessage').hide();
            }

            if (smsInput.attr('placeholder')) {
                $('#NotificationPhoneFormat').html("(" + smsInput.attr('placeholder') + ")");
            } else {
                $('#NotificationPhoneFormat').html("");
            }
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

        function emailChange(event) {
            const userInput = event.target.value;
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (userInput.length > 0 && emailRegex.test(userInput)) {
                $("#emailErrorMessageAB").hide();
                toggleSaveButtonAbility();
                $('#testEmail').show();
            } else {
                $("#emailErrorMessageAB").show();
                $('#testEmail').hide();
                toggleSaveButtonAbility();
            }

            if (userInput.length === 0) {
                $("#emailErrorMessageAB").hide();
                toggleSaveButtonAbility()
            }
        }

        function setSMSCost() {
            $.get("/Customer/CalcCredits?code=" + smsCountryCode + "&voice=false", function (data) {
                $('.smsCreditCount').html("x " + data);
            });
        }

        function setExternalSMS() {
            $('#testSMS').hide();
            $('.externalSMSFormat').show();
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
                $('.externalSMSFormat').hide();
                $('.externalSMS').hide();
                $('.directSMS').show();
                //$('#DirectSMS').val('true');
                //expectedDigits.hide();

                $("#NotificationPhone").intlTelInput({
                    autoFormat: false,
                    autoInsertDialCode: true,
                    defaultCountry: "us",
                    //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
                    preferredCountries: ['us'],
                    responsiveDropdown: true //set drop down width to match input width
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
            if ($('#UISMSCarrierID').val() > 0 || confirm(consumeCredits)) {
                $.get(url, function (data) {
                    alert(data);
                });
            }
        }

        function testVoice() {
            var url = "/Customer/TestVoice?phone=" + encodeURIComponent($("#NotificationPhone").val());
            url += "&isoCode=" + $('#NotificationPhone2ISOCode').val();
            if (confirm(consumeCredits)) {
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

        function optIn(anchor) {
            $.get($(anchor).attr("href"), function (data) {
                if (data == "Success") {
                    $('#emails').text(serverWait);
                    $('#optInLink').hide();
                    showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                    window.location.href = window.location.href;
                }
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }

        function removeSystemHelp(s) {
            console.log(s);
            $.post("/Overview/ClearSystemHelp", { id: s }, function (data) {
                if (data == "Success") {
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }

        //Remove the focus() class for selected inputs
        $('input').on('blur', function () {
            $(this).removeClass('selectMaintenance');
        });

        window.location.search == '?email' ? selectEmail() : '';
        function selectEmail() {
            let email = $("#NotificationEmail");
            email.focus();
            email.addClass('selectMaintenance');
        }

        function saveRecipients(e) {

            const customerID = "<%:Model.CustomerID%>"
            const notificationID = $(e).data("notificationid");
            const notiType = $(e).data("notitype");
            const removed = '<%: Html.TranslateTag("User removed from notification") %>'
            const added = '<%: Html.TranslateTag("User added to notification") %>'

            const wrapperSelector = $('[data-rulewrapperid="' + notificationID + notiType + '"]');

            if ($(wrapperSelector).hasClass('True')) {

                $.post('/Rule/RemoveSelfFromRecipientsList/', { "notificationID": notificationID, "customerID": customerID, "notiType": notiType }, function (data) {
                    if (data == "Success") {
                        $(wrapperSelector).toggleClass('True');

                        toastBuilder(removed, "Success");
                    }
                });
            }
            else {
                $.post('/Rule/AddSelfFromRecipientsList/', { "notificationID": notificationID, "customerID": customerID, "notiType": notiType }, function (data) {
                    if (data == "Success") {
                        $(wrapperSelector).toggleClass('True');

                        toastBuilder(added, "Success");
                    }
                });
            }
        }

        //Modal
        $('#myModal').on('shown.bs.modal')

        var successString = '<%=Html.TranslateTag("Success")%>';
        //when modal opens
        $('#myModal').on('shown.bs.modal', function (e) {
            $(".msg_container").css({ opacity: 1 });
        })

        //when modal closes
        $('#myModal').on('hidden.bs.modal', function (e) {
            $(".msg_container").css({ opacity: 1 });
        })

        //if error message(s) are visable disable the save button
        function toggleSaveButtonAbility() {
            const hasVisibleErrors =
                ($('#emailErrorMessageAB').length && $('#emailErrorMessageAB').css('display') !== 'none') ||
                ($('#SMSCallErrorMessage').length && $('#SMSCallErrorMessage').css('display') !== 'none') ||
                ($('#voiceCallErrorMessage').length && $('#voiceCallErrorMessage').css('display') !== 'none') ||
                ($('.expectedDigits').length && $('.expectedDigits').css('display') !== 'none');

            if (hasVisibleErrors) {

                $('#saveButtonAB').prop('disabled', true);

            } else {
                $('#saveButtonAB').prop('disabled', false);
            }

            if (document.querySelector(".expectedDigits") == null &&
                $('#emailErrorMessageAB').css('display') == 'none' &&
                $('#SMSCallErrorMessage').css('display') == 'none' &&
                $('#voiceCallErrorMessage').css('display') == 'none'
            ) {
                $('#saveButtonAB').prop('disabled', false);

            }
        }

        //enable and disable the text input if the user doesnt select a SMS provider

        function toggleInputAbility() {
            const select = document.querySelector("#UISMSCarrierID");
            const textInput = document.querySelector("#NotificationPhone");

            if (!$('#toggle-event').prop('checked') && select.value == 0) {
                textInput.setAttribute("disabled", "disabled");
            } else {
                textInput.removeAttribute("disabled");
            }
        }

        $("#saveButtonAB").click(() => enableSaveToWorkWithDisabledField());

        function enableSaveToWorkWithDisabledField() {
            const textInput = document.querySelector("#NotificationPhone");
            textInput.removeAttribute("disabled");
        }
        toggleInputAbility()

        //Error/Success toasts will grab the message returned from the ValidationMessage

        function removeSpanTags(inputString) {
            return inputString.replace(/<span class="field-validation-error">|<\/span>/g, '');
        }

        const eMailErrorMessage = '<%= Html.ValidationMessageFor(model => model.NotificationEmail) %>';
        const sMSErrorMessage = '<%= Html.ValidationMessageFor(model => model.UISMSCarrierID) %>'
        const phoneTwoErrorMessage = '<%: Html.ValidationMessageFor(model => model.NotificationPhone2) %>'
        const messageFromBackend = "<%= string.IsNullOrEmpty(ViewBag.Message) ? "" : ViewBag.Message  %>";

        if (eMailErrorMessage?.length > 1) {
            toastBuilder(removeSpanTags(eMailErrorMessage));
        }
        else if (sMSErrorMessage?.length > 1) {
            toastBuilder(removeSpanTags(sMSErrorMessage));
        }
        else if (phoneTwoErrorMessage?.length > 1) {
            toastBuilder(removeSpanTags(phoneTwoErrorMessage));
        }
        else {
            toastBuilder(messageFromBackend)
        }

    </script>

    <style>
        #toggle-event, #AllowPeriodicSync, #AllowPushMessage {
            cursor: pointer;
        }

            #toggle-event, #AllowPeriodicSync:focus, #AllowPushMessage:focus {
                box-shadow: none;
            }

                #toggle-event:checked, #AllowPeriodicSync:checked, #AllowPushMessage:checked {
                    background-color: var(--prime-btn-color);
                    border-color: var(--prime-btn-color);
                }

        .widthAdjustment {
            width: 250px;
        }

        .modal:nth-of-type(even) {
            z-index: 1062 !important;
        }

        .modal-backdrop.show:nth-of-type(even) {
            z-index: 1061 !important;
        }

        .modal {
            background: #00000054;
        }

        .RemovePushMessage {
            display: flex;
            justify-content: center;
            align-items: center;
            padding-left: 0.5rem;
            padding-right: 0.5rem;
            border-radius: 7px;
            transition: background-color 0.3s ease-in-out;
            cursor: pointer;
        }

            .RemovePushMessage svg {
                width: 1.75rem;
                height: 1.75rem;
            }

            .RemovePushMessage:hover svg {
                opacity: 0.8;
            }

        #helpIconWrapper svg {
            width: 30px;
            fill: var(--help-highlight-color);
        }

        .columnOnSm {
            flex-direction: row;
            gap: 30px;
        }

        @media screen and (max-width: 400px) {
            .widthAdjustment {
                width: 200px;
            }
        }

        @media screen and (max-width: 580px) {

            .columnOnSm {
                flex-direction: column;
                gap: 4px;
            }
        }
    </style>
</asp:Content>
