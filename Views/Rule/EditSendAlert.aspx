<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SendAlert
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        List<NotificationRecipientData> userListIncludingHigherAccounts = NotificationRecipientData.SearchPotentialRecipient(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.NotificationInProgress.AccountID, "");
        List<NotificationRecipientData> userGroups = userListIncludingHigherAccounts.FindAll(recipient => recipient.CustomerGroupID >= 0);

        List<NotificationRecipient> notificationRecipients = NotificationRecipient
            .LoadByNotificationID(Model.NotificationID)
            .ToList();

        long notiId = Model.NotificationID;

        List<long> selectedUserGroupIDs = new List<long>();

        foreach (NotificationRecipient recipient in notificationRecipients)
        {
            if (recipient.CustomerGroupID != null && recipient.CustomerGroupID > 0)
            {
                selectedUserGroupIDs.Add(recipient.CustomerGroupID);
            }
        }

        bool shouldShowAdvanced = Model.VoiceText.Length > 0 || Model.SMSText.Length > 0;
    %>

    <%:Html.Partial("~/Views/Rule/Header.ascx") %>

    <div class="column-on-sm-scr flexAB" style="gap: 1rem;">
        <%--  USERS CARD   --%>
        <div style="padding-bottom: 0;" class="rule-card_container w-100 powertour-hook" id="hook-five">
            <div>
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <span class="card-icon-top">
                            <%=Html.GetThemedSVG("recipients") %>
                            &nbsp;
                    <%: Html.TranslateTag("Settings/UserGroupEdit|Add Users to Message Recipients","Add Users to Message Recipients")%>
                        </span>
                    </div>
                </div>

                <div class="card_container__body">
                    <div class="margin-for-mobile bl" style="margin-bottom: 19px;">
                        <div id="textWrapper">
                            <div style="margin: 1rem 0px;">
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "Click the icon to enable or disable ")%>
                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 18 18">
                                    <path id="paper-plane-regular" style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" />
                                </svg>
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", " email,")%>
                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 18 16">
                                    <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M16.616,44.65a5.259,5.259,0,0,0,1.375-3.507c0-2.857-2.39-5.218-5.506-5.639A6.568,6.568,0,0,0,6.493,32c-3.59,0-6.5,2.557-6.5,5.714a5.272,5.272,0,0,0,1.375,3.507A9.448,9.448,0,0,1,.19,43.182a.913.913,0,0,0-.137.893.725.725,0,0,0,.662.5,6.586,6.586,0,0,0,3.912-1.386,7.9,7.9,0,0,0,.887.175,6.548,6.548,0,0,0,5.977,3.493,7.286,7.286,0,0,0,1.869-.243A6.6,6.6,0,0,0,17.273,48a.728.728,0,0,0,.662-.5.921.921,0,0,0-.137-.893A9.187,9.187,0,0,1,16.616,44.65Zm-12.274-3.3-.534.4a6.077,6.077,0,0,1-1.347.764c.084-.168.169-.346.25-.529L3.2,40.875,2.421,40a3.478,3.478,0,0,1-.928-2.286c0-2.168,2.29-4,5-4s5,1.832,5,4-2.29,4-5,4a6,6,0,0,1-1.531-.2Zm11.221,2.075-.772.871.484,1.111c.081.182.166.361.25.529a6.077,6.077,0,0,1-1.347-.764l-.534-.4-.622.164a6,6,0,0,1-1.531.2,5.435,5.435,0,0,1-4.1-1.775c3.165-.386,5.6-2.764,5.6-5.654,0-.121-.012-.239-.022-.357,2.012.518,3.521,2.029,3.521,3.786A3.478,3.478,0,0,1,15.563,43.429Z" transform="translate(0.007 -32)" />
                                </svg>
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", " SMS, ")%>
                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 14 18">
                                    <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M3.548,17.823a11.389,11.389,0,0,1,0-16.521.644.644,0,0,1,.775-.085L6.687,2.641a.593.593,0,0,1,.25.737L5.755,6.226a.629.629,0,0,1-.644.377l-2.035-.2a9.025,9.025,0,0,0,0,6.311l2.035-.2a.629.629,0,0,1,.644.377l1.182,2.848a.593.593,0,0,1-.25.737L4.324,17.908A.644.644,0,0,1,3.548,17.823ZM9.01,3.357a2.183,2.183,0,0,1,0,2.287.449.449,0,0,1-.682.091l-.218-.2a.414.414,0,0,1-.082-.507,1.092,1.092,0,0,0,0-1.053.414.414,0,0,1,.082-.507l.218-.2A.449.449,0,0,1,9.01,3.357ZM12.356.151a6.576,6.576,0,0,1,0,8.7.45.45,0,0,1-.64.033l-.211-.2a.412.412,0,0,1-.034-.576,5.48,5.48,0,0,0,0-7.222A.412.412,0,0,1,11.5.313l.211-.2a.45.45,0,0,1,.64.033ZM10.68,1.731a4.38,4.38,0,0,1,0,5.539.45.45,0,0,1-.651.046l-.212-.2a.411.411,0,0,1-.047-.56,3.284,3.284,0,0,0,0-4.118.411.411,0,0,1,.047-.56l.212-.2a.45.45,0,0,1,.651.046Z" transform="translate(0 0)" />
                                </svg>
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "voice, ")%>

                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 24 24">
                                    <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M15 21c0 1.598-1.392 3-2.971 3s-3.029-1.402-3.029-3h6zm.137-17.055c-.644-.374-1.042-1.07-1.041-1.82v-.003c.001-1.172-.938-2.122-2.096-2.122s-2.097.95-2.097 2.122v.003c.001.751-.396 1.446-1.041 1.82-4.668 2.709-1.985 11.715-6.862 13.306v1.749h20v-1.749c-4.877-1.591-2.193-10.598-6.863-13.306zm-3.137-2.945c.552 0 1 .449 1 1 0 .552-.448 1-1 1s-1-.448-1-1c0-.551.448-1 1-1zm-6.451 16c1.189-1.667 1.605-3.891 1.964-5.815.447-2.39.869-4.648 2.354-5.509 1.38-.801 2.956-.76 4.267 0 1.485.861 1.907 3.119 2.354 5.509.359 1.924.775 4.148 1.964 5.815h-12.903zm15.229-7.679c.18.721.05 1.446-.304 2.035l.97.584c.504-.838.688-1.869.433-2.892-.255-1.024-.9-1.848-1.739-2.351l-.582.97c.589.355 1.043.934 1.222 1.654zm.396-4.346l-.597.995c1.023.616 1.812 1.623 2.125 2.874.311 1.251.085 2.51-.53 3.534l.994.598c.536-.892.835-1.926.835-3-.001-1.98-1.01-3.909-2.827-5.001zm-16.73 2.692l-.582-.97c-.839.504-1.484 1.327-1.739 2.351-.255 1.023-.071 2.053.433 2.892l.97-.584c-.354-.588-.484-1.314-.304-2.035.179-.72.633-1.299 1.222-1.654zm-4.444 2.308c0 1.074.299 2.108.835 3l.994-.598c-.615-1.024-.841-2.283-.53-3.534.312-1.251 1.101-2.258 2.124-2.873l-.597-.995c-1.817 1.092-2.826 3.021-2.826 5z" />
                                </svg>
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "push alerts, and")%>
                                <svg style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" clip-rule="evenodd" fill-rule="evenodd" stroke-linejoin="round" stroke-miterlimit="2" width="14" height="14" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                    <path d="m11.998 2.005c5.517 0 9.997 4.48 9.997 9.997 0 5.518-4.48 9.998-9.997 9.998-5.518 0-9.998-4.48-9.998-9.998 0-5.517 4.48-9.997 9.998-9.997zm0 1.5c-4.69 0-8.498 3.807-8.498 8.497s3.808 8.498 8.498 8.498 8.497-3.808 8.497-8.498-3.807-8.497-8.497-8.497zm-5.049 8.886 3.851 3.43c.142.128.321.19.499.19.202 0 .405-.081.552-.242l5.953-6.509c.131-.143.196-.323.196-.502 0-.41-.331-.747-.748-.747-.204 0-.405.082-.554.243l-5.453 5.962-3.298-2.938c-.144-.127-.321-.19-.499-.19-.415 0-.748.335-.748.746 0 .205.084.409.249.557z" fill-rule="nonzero" />
                                </svg>
                                <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "notification groups that will receive the alert message.")%>
                            </div>
                            <div style="margin: 1rem 0px;"><%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "Click the text below the icon to set a delivery delay.")%></div>
                        </div>
                        <input style="margin-bottom: 1rem; max-width: 600px" class="form-control aSettings__input_input user-dets" type="text" id="userSearch" placeholder="<%: Html.TranslateTag("Events/ActionUserNotification|Name Filter", "Filter by recipient...")%>" />
                    </div>

                    <%--  NOTIFICATION GROUP SECTION   --%>
                    <% if (userGroups.Count > 0)
                        { %>
                    <div id="GroupListHeading" class="panel panel-heading" style="margin-bottom: 19px">
                        <div class="listHeader" style="justify-content: space-between; max-width: 500px;">
                            <div class="col-xs-6 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Notification Group", "Notification Group")%></div>
                            <div class="col-xs-2 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Enable/Disable", "Enable/Disable")%></div>
                        </div>
                    </div>
                    <div id="GroupList">
                        <%  foreach (NotificationRecipientData item in userGroups)
                            {%>
                        <div class="flexAB recipientCardAB <%= selectedUserGroupIDs.Contains(item.CustomerGroupID) ? "selectedGroup" : ""%>" data-recipientname="<%=item.FullName%>" data-groupid="<%=item.CustomerGroupID%>" onclick="toggleUserGroup(event)" style="height: 55px; align-items: center; justify-content: space-between; max-width: 500px; padding-bottom: 0.5rem;">
                            <div class="d-flex" style="align-items: center; gap: .5rem;">
                                <div style="width: 16px;">
                                    <%=Html.GetThemedSVG("user-groups") %>
                                </div>
                                <div class="notifyUsers__name">
                                    <%=item.FullName %>
                                </div>
                            </div>
                            <div class="col-2 shrink-col-2-mobile " style="display: flex; flex-direction: column;">
                                <div title="Toggle Notification Group" style="cursor: pointer; width: fit-content;">
                                    <svg class="<%= selectedUserGroupIDs.Contains(item.CustomerGroupID) ? "toggleOnColor" : "initialFill"%>" clip-rule="evenodd" fill-rule="evenodd" stroke-linejoin="round" stroke-miterlimit="2" width="24" height="24" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                        <path d="m11.998 2.005c5.517 0 9.997 4.48 9.997 9.997 0 5.518-4.48 9.998-9.997 9.998-5.518 0-9.998-4.48-9.998-9.998 0-5.517 4.48-9.997 9.998-9.997zm0 1.5c-4.69 0-8.498 3.807-8.498 8.497s3.808 8.498 8.498 8.498 8.497-3.808 8.497-8.498-3.807-8.497-8.497-8.497zm-5.049 8.886 3.851 3.43c.142.128.321.19.499.19.202 0 .405-.081.552-.242l5.953-6.509c.131-.143.196-.323.196-.502 0-.41-.331-.747-.748-.747-.204 0-.405.082-.554.243l-5.453 5.962-3.298-2.938c-.144-.127-.321-.19-.499-.19-.415 0-.748.335-.748.746 0 .205.084.409.249.557z" fill-rule="nonzero" />
                                    </svg>
                                </div>
                            </div>
                        </div>
                        <%}%>
                    </div>
                    <div class="bl"></div>
                    <%}%>

                    <%--    SINGLE USER SECTION   --%>
                    <div class="panel panel-heading" style="margin-bottom: 16px; margin-top: 19px !important; position: sticky; top: 0;">
                        <div class="listHeader">
                            <div class="col-6 shrink-col-6-mobile col-xs-6 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Recipient", "Recipient")%></div>
                            <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Email", "Email")%></div>
                            <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|SMS", "SMS")%></div>
                            <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Voice", "Voice")%></div>
                            <div style="width: auto" class="col-2 shrink-col-2-mobile col-xs-2 listHeader__titles sticky-header-title"><%: Html.TranslateTag("Events/ActionUserNotificationList|Voice", "Push")%></div>
                        </div>
                    </div>
                    <div id="userList" class="bsInset verticalScroll">
                        <%:Html.Partial("~\\Views\\Rule\\AlertNotificationList.ascx", notificationRecipients) %>
                    </div>
                </div>
            </div>
        </div>

        <div class="w-100">
            <%--    ALERT MESSAGE CARD   --%>
            <div class="rule-card_container w-100" style="gap: 0.5rem;">
                <div class="card_container__top" style="border-bottom: 1px solid #e6e6e6;">
                    <div class="card_container__top__title" style="align-items: center; border-bottom: none">
                        <span class="card-icon-top">
                            <%=Html.GetThemedSVG("alert") %>
                        &nbsp;
                        <%:Html.TranslateTag("Settings/UserGroupEdit|Alert Message","Alert Message")%>
                        </span>
                    </div>
                    <div class="btn-group" style="justify-content: flex-end;">
                        <a class="deleteButtonAB" onclick="RemoveNotifyTask();" title="<%=Html.TranslateTag("Remove") %>"><%=Html.TranslateTag("Delete Alert") %></a>
                    </div>
                </div>
                <input type="hidden" id="notiClass" value="" />
                <div style="display: flex; justify-content: center;">
                    <div class="msg_card1AB width-adjustment" style="z-index: 1">
                        <div class="txt-heading">
                            <input type="text" id="subject" name="subject" placeholder="Subject..." value="<%= Model.Subject != null ? Model.Subject : "" %>" class="msg-input user-dets" />
                            <%if (MonnitSession.AccountCan("text_override"))
                                {%>
                            <div class="merge-item" style="cursor: initial;">
                                <p class="merge-btn" style="box-shadow: none; cursor: pointer;" data-bs-toggle="modal" data-bs-target=".overrideValues" title="<%: Html.TranslateTag("Rule/SendNotification|Merge Fields")%>"><%: Html.TranslateTag("Rule/SendNotification|Merge Fields")%></p>
                            </div>
                            <%} %>
                        </div>
                        <textarea id="alertMessage" style="margin: 0 !important" class="text-box-msg user-dets send-text-area-size" placeholder="<%:Html.TranslateTag("Rule/SendNotification|Message...")%>" name="alertMessage"><%= Model.NotificationText != null ? Model.NotificationText : "" %></textarea>
                        <div class="msg-save" style="margin: 0; justify-content: space-between;">
                            <div class="d-flex overridePreview" style="cursor: pointer; gap: 0.15rem;" onclick="toggleSensorFilterOptions()">
                                <div id="addAB" class="addSVG">
                                    <%=Html.GetThemedSVG("add") %>
                                </div>
                                <div id="minusAB" class="addSVG" style="display: none;">
                                    <%=Html.GetThemedSVG("minus") %>
                                </div>
                                <strong style="color: #5A738E;">
                                    <%= Html.TranslateTag("Advanced") %>
                                </strong>
                            </div>
                            <div class="d-flex" style="align-items: center; gap: 1rem;">
                                <a href="#" class="overridePreview sendPreview" data-type="Text" data-bs-toggle="modal" data-bs-target=".previewOverride"><strong style="color: #5A738E;"><%= Html.TranslateTag("Message Preview") %></strong></a>
                            </div>
                        </div>
                        <%-- Advanced message --%>
                        <div id="advancedAlertMessages">
                            <div style="font-weight: 600; margin: 0.5rem 0">SMS/Push Specific Message</div>
                            <textarea style="margin: 0 !important" class="text-box-msg user-dets send-text-area-size" placeholder="Enter SMS/Push message..." id="alertMessageSMS" name="alertMessageSMS" maxlength="160"><%= Model.SMSText != null ? Model.SMSText : "" %></textarea>
                            <div class="msg-save">
                                <div class="d-flex" style="align-items: center; gap: 1rem;">
                                    <a href="#" class="overridePreview sendPreviewSMS" data-type="Text" data-bs-toggle="modal" data-bs-target=".previewOverrideSMS"><strong style="color: #5A738E;"><%= Html.TranslateTag("SMS Preview") %></strong></a>
                                </div>
                            </div>
                            <div style="font-weight: 600; margin: 0.5rem 0">Voice Specific Message</div>
                            <textarea style="margin: 0 !important" class="text-box-msg user-dets send-text-area-size" placeholder="Enter Voice message..." id="alertMessageVoice" name="alertMessageVoice"><%= Model.VoiceText != null ? Model.VoiceText : "" %></textarea>
                            <div class="msg-save">
                                <div class="d-flex" style="align-items: center; gap: 1rem;">
                                    <a href="#" class="overridePreview sendPreviewVoice" data-type="Text" data-bs-toggle="modal" data-bs-target=".previewOverrideVoice"><strong style="color: #5A738E;"><%= Html.TranslateTag("Voice Preview") %></strong></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Help Button Override Values -->
            <div class="modal fade overrideValues" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered tValues_modal">
                    <%=Html.Partial("_MergeFieldModalBody") %>
                </div>
            </div>

            <!-- Help Button Override Preview -->
            <div class="modal fade previewOverride" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered tValues_modal">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modal"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body " id="previewOverrideBody"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Email Preview -->
            <div class="modal fade previewOverrideEmail modalEmail" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered tValues_modal">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalEmail"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body " id="previewOverrideBodyEmail"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- SMS/Push Preview -->
            <div class="modal fade previewOverrideSMS" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered tValues_modal">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalSMS"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body " id="previewOverrideBodySMS"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Voice Preview -->
            <div class="modal fade previewOverrideVoice" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered tValues_modal">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalVoice"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body " id="previewOverrideBodyVoice"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Save Buttons -->
            <div style="display: flex; gap: 1rem; justify-content: flex-end; margin: 1rem 0;">
                <button type="button" id="cancelBtn" onclick="history.go(-1); return false;" class="btn btn-secondary">Cancel</button>
                <button type="button" class="btn btn-primary user-dets" id="saveAlert" onclick="saveForm()"><%: Html.TranslateTag("Save")%></button>
                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    <%: Html.TranslateTag("Saving...","Saving...")%>
                </button>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#alertMessageSMS').on('input', function (e) {
                var $target = $(e.currentTarget);  // Convert to jQuery object
                if ($target.val().length > 159) {
                    toastBuilder("Max length reached");
                }
            });
        });

        $('.sendPreview').click(function (e) {
            e.preventDefault();
            var msg = $("#alertMessage").val(); + $("#alertMessage").val();
            var obj = $('#previewOverrideBody');
            obj.html("content/css/myloader.ascx");

            $.post('/Rule/NotifyBodyMsgPreview/', { msg: msg }, function (data) {
                obj.html(data);
            });
        });

        $('.sendPreviewSMS').click(function (e) {
            e.preventDefault();
            var msg = $("#alertMessageSMS").val();
            var obj = $('#previewOverrideBodySMS');
            obj.html("content/css/myloader.ascx");

            $.post('/Rule/NotifyBodyMsgPreview/', { msg: msg }, function (data) {
                obj.html(data);
            });
        });

        $('.sendPreviewVoice').click(function (e) {
            e.preventDefault();
            var msg = $("#alertMessageVoice").val();
            var obj = $('#previewOverrideBodyVoice');
            obj.html("content/css/myloader.ascx");

            $.post('/Rule/NotifyBodyMsgPreview/', { msg: msg }, function (data) {
                obj.html(data);
            });
        });

        const clearInputFields = (idsOfInputsArray) => {
            idsOfInputsArray.forEach(id => {
                $(`#${id}`).val('');
            });
        };

        var failedServerResponse = false;

        async function saveNotificationSettings(notiId, Voicevalues, PostString) {
            try {
                const response = await new Promise((resolve, reject) => {
                    $.post(`/Rule/Edit${PostString}NotificationSettings/${notiId}`, Voicevalues, function (data) {
                        if (data !== 'Success') {
                            resolve(data);
                        } else {
                            resolve('Success');
                        }
                    }).fail((error) => {
                        reject(error);
                    });
                });

                if (response !== 'Success') {
                    failedServerResponse = true;
                    console.log(response);
                }
            } catch (error) {
                console.error('Error:', error);
            }
        }

        async function postNotificationRecipients(notiID, valuesToPost) {
            try {
                const response = await new Promise((resolve, reject) => {
                    $.post(`/Rule/EditPageSaveAllRecipients/${notiID}`, valuesToPost, function (data) {
                        if (data !== 'Success') {
                            resolve(data);
                        } else {
                            resolve('Success');
                        }
                    }).fail((error) => {
                        reject(error);
                    });
                });

                if (!response) {
                    failedServerResponse = true;
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    console.log(response);
                }
            } catch (error) {
                console.error('Error:', error);
            }
        }

        async function saveForm() {

            $('#saving').show();
            $('#saveAlert').hide();

            let recipientSelected = false;

            //hiddenInputToCheckOnSubmit elements found on the UserGroupListDetails page. 
            $('.hiddenInputToCheckOnSubmit').each(function () {
                if ($(this).val() >= 0) {
                    recipientSelected = true;
                }
            });

            if ($('.selectedGroup').length > 0) {
                recipientSelected = true;
            }

            if (!recipientSelected) {
                toastBuilder("Please select a recipient");
                console.log("Error: no recipient has been selected!");
                $('#saving').hide();
                $('#saveAlert').show();
                return
            }

            // Check Alert/SMS/Email/Voice Message isnt blank.
            if ($('#alertMessage').val().length === 0 && $('#alertMessageSMS').val().length === 0 && $('#alertMessageVoice').val().length === 0) {
                toastBuilder("Alert message cannot be blank");
                console.log("Error: Alert message is blank!");
                $('#saving').hide();
                $('#saveAlert').show();
                return
            }

            await saveRecipients();

            //Submit Email/Alert
            const values = {
                subject: $('#subject').val(),
                emailMsg: $('#alertMessage').val()
            }

            await saveNotificationSettings('<%=notiId%>', values, 'Email');

            //Submit SMS
            const SMSvalues = {
                SMSText: $('#alertMessageSMS').val()
            }

            await saveNotificationSettings('<%=notiId%>', SMSvalues, 'Text');

            //Submit Voice
            const Voicevalues = {
                VoiceText: $('#alertMessageVoice').val()
            }

            await saveNotificationSettings('<%=notiId%>', Voicevalues, 'Voice');

            //Submit Push
            const Pushvalues = {
                PushText: $('#alertMessageSMS').val()
            }

            //Submit Push (will be same message as sms)
            await saveNotificationSettings('<%=notiId%>', Pushvalues, 'Push');

            if (failedServerResponse) {
                console.error("Not all values posted to the server saved!")
                toastBuilder("Oops! That didn't work, please refresh your page. If this error continues, contact support.")
                return
            } else {
                toastBuilder("Success");

                setTimeout(() => {
                    window.location = '/Rule/ChooseTaskToEdit/<%=notiId%>';
                    $('#saving').hide();
                    $('#saveAlert').show();
                }, 1000)
            }
        }

        $('#userSearch').keyup(function (e) {
            e.preventDefault();
            filterUsers();
        });

        function filterUsers() {
            var query = $('#userSearch').val().trim().toLowerCase();

            $('.recipientCardAB').each(function () {
                var recipientName = $(this).data('recipientname').toLowerCase();
                if (recipientName.includes(query)) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });

            var allChildrenHidden = $('#GroupListHeading').length > 0; // Assume true initially

            $('#GroupList').children().each(function () {
                if ($(this).css('display') !== 'none') {
                    allChildrenHidden = false;
                    return false;
                }
            });

            if (allChildrenHidden) {
                $('#GroupListHeading').css('display', 'none');
            } else {
                $('#GroupListHeading').css('display', 'block');
            }
        };

        const toggleUserGroup = (e) => {

            let groupId = $(e.currentTarget).data("groupid");
            let toggleOffTheUsers = false;

            if ($(e.currentTarget).hasClass('selectedGroup')) {

                $(e.currentTarget).removeClass('selectedGroup');
                $(e.currentTarget).find("svg").removeClass("toggleOnColor");
                $(e.currentTarget).find("svg").addClass("initialFill");

                toggleOffTheUsers = true;

            } else {
                $(e.currentTarget).toggleClass('selectedGroup');
                $(e.currentTarget).find("svg").removeClass("initialFill");
                $(e.currentTarget).find("svg").addClass("toggleOnColor");
            }

        }

        function toggleSensorFilterOptions() {
            $('#advancedAlertMessages').slideToggle('slow');

            if ($('#addAB').is(':visible')) {
                $('#addAB').hide();
                $('#minusAB').show();
            } else {
                $('#addAB').show();
                $('#minusAB').hide();
            }
        }

        $(document).ready(function () {
    <% if (shouldShowAdvanced)
        { %>
            toggleSensorFilterOptions();
    <% } %>
        });
        function getUserGroupIdArray() {
            let arrayOfUserGroupIds = []
            $('#GroupList .selectedGroup').each(function () {
                arrayOfUserGroupIds.push($(this).data('groupid'))
            });
            return arrayOfUserGroupIds
        }

        async function saveRecipients() {

            let inputValues = $('.recipientCardAB').map((index, element) => {
                let recipientId = $(element).data('recipientid')
                return {
                    Id: recipientId,
                    Name: $(element).data('recipientname'),
                    Email: $(`#openSpinnerModalFor_${recipientId}_EmailInput`).val(),
                    Text: $(`#openSpinnerModalFor_${recipientId}_TextInput`).val(),
                    Voice: $(`#openSpinnerModalFor_${recipientId}_VoiceInput`).val(),
                    Push: $(`#openSpinnerModalFor_${recipientId}_PushInput`).val(),
                };
            }).get();

            let emailValuesToSubmit = {
                type: "<%=eNotificationType.Email.ToInt()%>",
                recipient: ""
            }

            let smsValuesToSubmit = {
                type: "<%=eNotificationType.SMS.ToInt()%>",
                recipient: ""
            }

            let voiceValuesToSubmit = {
                type: "<%=eNotificationType.Phone.ToInt()%>",
                recipient: ""
            }

            let pushValuesToSubmit = {
                type: "<%=eNotificationType.Push_Message.ToInt()%>",
                recipient: ""
            }

            let groupValuesToSubmit = {
                type: "<%=eNotificationType.Group.ToInt()%>",
                recipient: getUserGroupIdArray().join(",")
            }

            inputValues.forEach(recipientObj => {
                if (recipientObj.Email > -1) {
                    emailValuesToSubmit.recipient += recipientObj.Id + "|" + recipientObj.Name + "|" + recipientObj.Email + "|,";
                }

                if (recipientObj.Text > -1) {
                    smsValuesToSubmit.recipient += recipientObj.Id + "|" + recipientObj.Name + "|" + recipientObj.Text + "|,";
                }

                if (recipientObj.Voice > -1) {
                    voiceValuesToSubmit.recipient += recipientObj.Id + "|" + recipientObj.Name + "|" + recipientObj.Voice + "|,";
                }

                if (recipientObj.Push > -1) {
                    pushValuesToSubmit.recipient += recipientObj.Id + "|" + recipientObj.Name + "|" + recipientObj.Push + "|,";
                }
            })

            await postNotificationRecipients(<%=notiId%>, { emailValuesToSubmit, smsValuesToSubmit, voiceValuesToSubmit, groupValuesToSubmit, pushValuesToSubmit });
        };

        function RemoveNotifyTask(eNotiType) {
            if (confirm("Are you sure you want to delete this alert?")) {
                $.post("/Rule/RemoveNotifyTaskFromEdit/<%=notiId%>", function (data) {
                    if (data == "Success") {
                        window.location.href = "/Rule/ChooseTaskToEdit/<%=notiId%>";
                    } else {
                        $('#recipientList').html(data);
                    }
                });
            } else {
                return;
            }
        }

        function reorderSelectedGroup() {
            var $container = $('#GroupList');
            var $selectedElements = $container.find('.selectedGroup');

            $selectedElements.each(function () {
                $container.prepend($(this));
            });
        }

        reorderSelectedGroup();

    </script>



</asp:Content>
