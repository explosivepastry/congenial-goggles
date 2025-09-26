<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Rest API</title>
    <link rel="shortcut icon" href="<%: Html.GetThemedContent("Favicon.ico")%>" />

    <% Html.RenderPartial("MasterIncludes");%>
    <%--<% if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Account.IsReseller)
           ViewBag.ShowResellerParameters = true;  %>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.codeSample').click(function (e) {
                e.preventDefault();
                newModal("Code Sample", $(this).attr("href"), 600, 800);
            });

            $('h3').click(function () {
                $(this).next().toggle();
            }).css("cursor", "pointer");

            $('.methodDiv').css("display", "none").css('margin-left', '20px');
        });
    </script>


    <style>
        
::-webkit-scrollbar {
  width: auto;
  /* Remove scrollbar space */
  background: transparent;
  /* Optional: just make scrollbar invisible */ }

/* Optional: show position indicator in red */
::-webkit-scrollbar-thumb {
  background: #555; }

    </style>
</head>
<body>
    <div id="window">
        <div id="inside">
            <div id="header">
                <div id="logo">
                    &nbsp;
                </div>
                <!-- logo -->
                <div id="logindisplay">
                    <% Html.RenderPartial("LogOnUserControl"); %>
                </div>
                <!-- logindisplay -->
                <div id="menucontainer">
                    <div id="top_nav">
                        <ul class="sf-menu sf-js-enabled sf-shadow">
                            <li><a id="MenuNetwork" class="sf-with-ul" href="/">Overview</a></li>
                            <li><a class="sf-with-ul" href="/api/index">Rest API</a> </li>
                            <li><a class="sf-with-ul" href="/restAPI/webhook">WebHook</a></li>
                            <%--   <li><a class="sf-with-ul" href="/APIv2/Index">API Version 2</a> </li>--%>
                        </ul>
                    </div>
                    <!-- top_nav -->
                </div>
                <!-- menucontainer -->
            </div>
            <!-- header -->
            <div id="fullForm" style="width: 100%;">
                <div class="formtitle">
                    <div id="MainTitle" style="display: none;"></div>
                    <div style="height: 20px">
                        Rest API
                    </div>
                </div>
            </div>
            <div class="logon">
                <p>
                    Rest API allows users to integrate their sensor data into 3rd party applications.
                        API calls can be made from programming language of your choice. Responses are served
                        as either XML(Extensible Markup Language) or JSON (JavaScript Object Notation).
                   Overuse could result in revocation of API portal. Overuse Example: making the same call more often than the data is updated (Datamessage Methods: 1 request per 10 minutes). 
                   Please utilize the webhook for real time Datamessages.  
                </p>
                <p>
                    *All Dates are in UTC (Universal Coordinated Time) both for the input and for the output.<br />
                    *To request a larger data load, Please contact support<br />
                </p>
            </div>
            <div id="Div1" style="width: 100%;">
                <div class="formtitle">
                    <div id="Div2" style="display: none;"></div>
                    <div style="height: 20px">
                        API End-Point
                    </div>
                </div>
            </div>

            <div class="logon">
                <%Html.RenderPartial("BaseAPIExplained");%>

                <fieldset>
                    <legend>Authentication</legend>
                    <%Html.RenderPartial("Authentication/GetAuthToken");%>

                    <%Html.RenderPartial("Authentication/Logon");%>

                    <%Html.RenderPartial("Authentication/UpdatePassword");%>

                    <%Html.RenderPartial("Authentication/ResetPassword");%>
                </fieldset>

                <fieldset>
                    <legend>Lookup</legend>
                    <%Html.RenderPartial("Lookup/TimeZones");%>

                    <%Html.RenderPartial("Lookup/TimeZonesWithRegion");%>

                    <%Html.RenderPartial("Lookup/SMSCarriers");%>

                    <%Html.RenderPartial("Lookup/GetApplicationID");%>

                    <%Html.RenderPartial("Lookup/GetDatumByType");%>

                    <%Html.RenderPartial("Lookup/AccountNumberAvailable");%>

                    <%Html.RenderPartial("Lookup/UserNameAvailable");%>

                    <%Html.RenderPartial("Lookup/SensorLookUp");%>

                    <%Html.RenderPartial("Lookup/GatewayLookUp");%>

                </fieldset>

                <fieldset>
                    <legend>Account</legend>
                    <%Html.RenderPartial("Account/CreateAccount");%>

                    <%Html.RenderPartial("Account/AccountGet"); %>

                    <%Html.RenderPartial("Account/EditAccountInformation"); %>

                    <%Html.RenderPartial("Account/EditCustomerInformation"); %>

                    <%Html.RenderPartial("Account/GetCustomerPermissions"); %>

                    <%Html.RenderPartial("Account/EditCustomerPermissions"); %>

                    <%Html.RenderPartial("Account/RetrieveUsername");%>

                    <%Html.RenderPartial("Account/CreateAccountUser");%>

                    <%Html.RenderPartial("Account/AccountUserDelete"); %>

                    <%Html.RenderPartial("Account/AccountUserList"); %>

                    <%Html.RenderPartial("Account/AccountUserGet"); %>

                    <%Html.RenderPartial("Account/AccountUserEdit"); %>

                    <%Html.RenderPartial("Account/AccountUserScheduleGet"); %>

                    <%Html.RenderPartial("Account/AccountUserScheduleSet"); %>

                    <%--  <%Html.RenderPartial("Account/AccountUserImageUpload"); %>--%>


                    <%if (MonnitSession.CurrentCustomer != null)// && MonnitSession.CurrentCustomer.Account.IsReseller)
                      { %>
                    <fieldset>
                        <legend>OEM Specific API Calls</legend>

                        <%Html.RenderPartial("Account/CreateSubAccount");%>

                        <%Html.RenderPartial("Account/RemoveSubAccount");%>

                        <%Html.RenderPartial("Account/SubAccountList");%>

                        <%Html.RenderPartial("Account/SubAccountTreeList");%>

                        <%Html.RenderPartial("Account/AccountParentEdit"); %>

                        <%Html.RenderPartial("Account/ResellerPasswordReset");%>

                        <%Html.RenderPartial("Account/SetExpirationDate");%>

                        <%Html.RenderPartial("Notification/RecentResellerNotification");%>
                    </fieldset>
                    <%} %>
                </fieldset>

                <fieldset>
                    <legend>Network</legend>
                    <%Html.RenderPartial("Network/CreateNetwork");%>

                    <%Html.RenderPartial("Network/CreateNetwork2");%>

                    <%Html.RenderPartial("Network/RemoveNetwork");%>

                    <%Html.RenderPartial("Network/NetworkList");%>
                </fieldset>

                <fieldset>
                    <legend>Gateway</legend>

                    <%Html.RenderPartial("Gateway/GatewayResetDefaults");%>

                    <%Html.RenderPartial("Gateway/GatewayReform"); %>

                    <%Html.RenderPartial("Gateway/RemoveGateway");%>

                    <%Html.RenderPartial("Gateway/AssignGateway");%>

                    <%Html.RenderPartial("Gateway/GatewayGet");%>

                    <%Html.RenderPartial("Gateway/GatewayList");%>

                    <%Html.RenderPartial("Gateway/GatewaySetHeartbeat");%>

                    <%Html.RenderPartial("Gateway/GatewaySetIP"); %>

                    <%Html.RenderPartial("Gateway/GatewaySetName"); %>

                    <%Html.RenderPartial("Gateway/GatewayPoint"); %>

                    <%Html.RenderPartial("Gateway/GatewaySetHost"); %>

                    <%Html.RenderPartial("Gateway/GatewayClearHost"); %>

                    <%Html.RenderPartial("Gateway/GatewayCellNetworkConfig"); %>

                    <%Html.RenderPartial("Gateway/GatewayAutoConfig"); %>

                    <%Html.RenderPartial("Gateway/GatewayStopAutoConfig"); %>

                    <%Html.RenderPartial("Gateway/GatewayUpdateFirmware"); %>
                </fieldset>

                <fieldset>
                    <legend>Sensor</legend>
                    <%Html.RenderPartial("Sensor/SensorResetDefaults");%>

                    <%Html.RenderPartial("Sensor/RemoveSensor");%>

                    <%Html.RenderPartial("Sensor/AssignSensor");%>

                    <%Html.RenderPartial("Sensor/SensorGet");%>

                    <%Html.RenderPartial("Sensor/SensorGetExtended");%>

                    <%Html.RenderPartial("Sensor/SensorGetCalibration");%>

                    <%Html.RenderPartial("Sensor/SensorApplicationIDGet");%>

                    <%Html.RenderPartial("Sensor/SensorNameGet");%>

                    <%//Html.RenderPartial("Sensor/WIFISensorIDGet"); Removed due to Not being used%>

                    <%Html.RenderPartial("Sensor/SensorList");%>

                    <%Html.RenderPartial("Sensor/SensorListExtended");%>

                    <%Html.RenderPartial("Sensor/NetworkIDFromSensorGet");%>
                    
                    <%Html.RenderPartial("Sensor/SensorSetHeartbeat");%>

                    <%Html.RenderPartial("Sensor/SensorSetIP");%>

                    <%Html.RenderPartial("Sensor/SensorSetName");%>

                    <%Html.RenderPartial("Sensor/SensorSetThreshold");%>

                    <%Html.RenderPartial("Sensor/SensorSetCalibration");%>

                    <%Html.RenderPartial("Sensor/SensorSetAlerts");%>

                    <%Html.RenderPartial("Sensor/SensorSetTag"); %>

                    <%Html.RenderPartial("Sensor/SensorAttributes");%>

                    <%Html.RenderPartial("Sensor/SensorAttributeSet");%>

                    <%Html.RenderPartial("Sensor/SensorAttributeRemove");%>

                    <%Html.RenderPartial("Sensor/SensorSendControlCommand");%>

                    <%Html.RenderPartial("Sensor/GetDatumNameList");%>

                    <%Html.RenderPartial("Sensor/SetDatumName");%>

                    <fieldset>
                        <legend>Sensor Group</legend>

                        <%Html.RenderPartial("Sensor/SensorGroupSensorList");%>

                        <%Html.RenderPartial("Sensor/SensorGroupList");%>

                        <%Html.RenderPartial("Sensor/SensorGroupCreate");%>

                        <%Html.RenderPartial("Sensor/SensorGroupDelete");%>

                        <%Html.RenderPartial("Sensor/SensorGroupAssignSensor");%>

                        <%Html.RenderPartial("Sensor/SensorGroupRemoveSensor");%>
                    </fieldset>


                    <%--<%Html.RenderPartial("Sensor/SensorEquipmentGet");%>

                     <%Html.RenderPartial("Sensor/SensorEquipmentSet");%>--%>
                </fieldset>

                <fieldset>
                    <legend>DataMessage</legend>
                    <%Html.RenderPartial("DataMessage/SensorDataMessages");%>

                    <%Html.RenderPartial("DataMessage/GatewayMessages");%>

                    <%Html.RenderPartial("DataMessage/AccountRecentDataMessages"); %>

                    <%Html.RenderPartial("DataMessage/SensorRecentDataMessages");%>

                    <%Html.RenderPartial("DataMessage/AccountDataMessages");%>

                    <%Html.RenderPartial("DataMessage/SensorChartMessages");%>
                </fieldset>

                <fieldset>
                    <legend>Notification</legend>
                    <%Html.RenderPartial("Notification/RecentlySentNotifications"); %>

                    <%Html.RenderPartial("Notification/SentNotifications"); %>

                    <%Html.RenderPartial("Notification/NotificationRecipientList"); %>

                    <%Html.RenderPartial("Notification/NotificationSystemActionList"); %>

                    <%Html.RenderPartial("Notification/NotificationControlUnitList"); %>

                    <%Html.RenderPartial("Notification/NotificationLocalNotifierList"); %>

                    <%Html.RenderPartial("Notification/SensorAssignedToNotification"); %>

                    <%Html.RenderPartial("Notification/GatewayAssignedToNotification"); %>

                    <%Html.RenderPartial("Notification/ToggleNotification"); %>

                    <%Html.RenderPartial("Notification/AccountNotificationList"); %>

                    <%Html.RenderPartial("Notification/SensorNotificationList"); %>

                    <%Html.RenderPartial("Notification/NotificationScheduleList"); %>

                    <%Html.RenderPartial("Notification/NotificationDailyScheduleSet"); %>

                    <%Html.RenderPartial("Notification/NotificationYearlyScheduleList"); %>

                    <%Html.RenderPartial("Notification/NotificationYearlyScheduleSet"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignSensor"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveSensor"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignGateway"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveGateway"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignRecipient"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveRecipient"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignSystemAction"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveSystemAction"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignControlUnit"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveControlUnit"); %>

                    <%Html.RenderPartial("Notification/NotificationAssignLocalNotifier"); %>

                    <%Html.RenderPartial("Notification/NotificationRemoveLocalNotifier"); %>

                    <%Html.RenderPartial("Notification/NotificationAcknowledge"); %>

                    <%Html.RenderPartial("Notification/NotificationFullReset"); %>

                    <%Html.RenderPartial("Notification/NotificationPause"); %>

                    <%Html.RenderPartial("Notification/NotificationUnpause"); %>

                    <%Html.RenderPartial("Notification/NotificationDelete"); %>

                    <%Html.RenderPartial("Notification/ApplicationNotificationCreate"); %>

                    <%Html.RenderPartial("Notification/ScheduledNotificationCreate"); %>

                    <%Html.RenderPartial("Notification/BatteryNotificationCreate"); %>

                    <%Html.RenderPartial("Notification/InactivityNotificationCreate"); %>

                    <fieldset>
                        <legend>Advanced Notification</legend>

                        <%Html.RenderPartial("Notification/AdvancedNotificationCreate"); %>

                        <%Html.RenderPartial("Notification/NotificationListAdvancedTypes"); %>

                        <%Html.RenderPartial("Notification/NotificationListAdvancedParameters"); %>

                        <%Html.RenderPartial("Notification/NotificationListAdvancedParameterOptions"); %>

                        <%Html.RenderPartial("Notification/AdvancedNotificationParameterSet"); %>

                        <%Html.RenderPartial("Notification/AdvancedNotificationParameterList"); %>

                        <%Html.RenderPartial("Notification/NotificationListAdvancedAutomatedSchedule"); %>

                        <%Html.RenderPartial("Notification/AdvancedNotificationAutomatedScheduleSet"); %>
                    </fieldset>

                </fieldset>

                <%--                <fieldset>
                    <legend>DataPush</legend>

                    <%Html.RenderPartial("DataPush/ExternalDataPushList");%>

                    <%Html.RenderPartial("DataPush/ExternalDataPush");%>

                    <%Html.RenderPartial("DataPush/ExternalDataPushSet");%>

                    <%Html.RenderPartial("DataPush/ExternalDataPushRemove");%>

                    <%Html.RenderPartial("DataPush/ExternalGatewayDataPushList");%>

                    <%Html.RenderPartial("DataPush/ExternalGatewayDataPush");%>

                    <%Html.RenderPartial("DataPush/ExternalGatewayDataPushSet");%>

                    <%Html.RenderPartial("DataPush/ExternalGatewayDataPushRemove");%>

                    <%Html.RenderPartial("DataPush/ResetExternalDataSubscription");%>
                </fieldset>--%>


                <fieldset>
                    <legend>Webhook</legend>

                    <%Html.RenderPartial("WebHook/WebHookGet");%>

                    <%Html.RenderPartial("WebHook/WebHookCreate");%>

                    <%Html.RenderPartial("WebHook/WebHookRemove");%>

                    <%Html.RenderPartial("WebHook/WebHookSetAuthentication");%>

                    <%Html.RenderPartial("WebHook/WebHookRemoveAuthentication");%>

                    <%Html.RenderPartial("WebHook/WebHookAddCookie");%>
                    
                    <%Html.RenderPartial("WebHook/WebHookRemoveCookie");%>

                    <%Html.RenderPartial("WebHook/WebHookCookieList");%>

                    <%Html.RenderPartial("WebHook/WebHookAddHeader");%>
 
                    <%Html.RenderPartial("WebHook/WebHookRemoveHeader");%>

                    <%Html.RenderPartial("WebHook/WebHookHeaderList");%>

                    <%Html.RenderPartial("WebHook/WebHookResetBroken");%>

                    <%Html.RenderPartial("WebHook/WebHookAttempts");%>

                    <%Html.RenderPartial("WebHook/WebHookResend");%>

                    <%Html.RenderPartial("WebHook/WebHookAttemptBody");%>

                    <%Html.RenderPartial("WebHook/WebhookListNotificationSettings");%>

                    <%Html.RenderPartial("WebHook/WebhookNotificationSettingsSet");%>
                </fieldset>



            </div>
            <div id="Div3" style="width: 100%;">
                <div class="formtitle">
                    <div id="Div4" style="display: none;"></div>
                    <div style="height: 20px">
                        Code Samples
                    </div>
                </div>
            </div>
            <div class="logon">
                <fieldset>
                    <legend>Code</legend>

                    <%Html.RenderPartial("CodeSampleCSharp");%>

                    <%Html.RenderPartial("CodeSamplePHP");%>

                    <%Html.RenderPartial("CodeSampleJavascript");%>
                </fieldset>
            </div>
            <div class="kill_height">
                &nbsp;
            </div>
        </div>
        <!-- inside -->
        <div id="window_btm">
        </div>
        <!--window_btm-->
    </div>
    <!-- window -->
    <% Html.RenderPartial("Footer"); %>
    <!-- footer -->
</body>
</html>
