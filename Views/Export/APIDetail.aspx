<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    APIDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
             <%:Html.Partial("_APILink") %>

            <div id="fullForm" style="width: 100%;">
                <div class="formtitle">
                <div id="MainTitle"><h1><%: Html.TranslateTag("Export/APIDetail|Rest API","Rest API")%></h1></div>
                </div>
            </div>
            <div class="logon">
                <p>
					<%: Html.TranslateTag(@"Export/APIDetail|Rest API allows users to integrate their sensor data into 3rd party applications.
                        API calls can be made from programming language of your choice. Responses are served
                    as either XML(Extensible Markup Language) or JSON (JavaScript Object Notation).",@"Rest API allows users to integrate their sensor data into 3rd party applications.
                        API calls can be made from programming language of your choice. Responses are served
                        as either XML(Extensible Markup Language) or JSON (JavaScript Object Notation).")%>
                </p>
                <p>					
					<%: Html.TranslateTag("Export/APIDetail|*All Dates are in UTC (Universal Coordinated Time) both for the input and for the output."
														  ,"*All Dates are in UTC (Universal Coordinated Time) both for the input and for the output.")%>
					<br />
                </p>
            </div>
			<br />
	
            <div id="Div1" style="width: 100%;">
                <div class="formtitle">
                    <div id="Div2" style="display: none;"></div>
                    <div style="height: 20px">
						<%: Html.TranslateTag("Export/APIDetail|API End-Point","API End-Point")%>
                    </div>
                </div>
            </div>
	
			<hr />

            <div class="logon">
                <%Html.RenderPartial("~/Views/API/BaseAPIExplained.ascx");%>

                <fieldset>
                <legend><%: Html.TranslateTag("Export/APIDetail|Authentication","Authentication")%></legend>
                    <%Html.RenderPartial("~/Views/API/Authentication/GetAuthToken.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Authentication/Logon.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Authentication/UpdatePassword.ascx");%>
                <%Html.RenderPartial("~/Views/API/Authentication/ResetPassword.ascx");%>

                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Lookup","Lookup")%></legend>

                    <%Html.RenderPartial("~/Views/API/Lookup/TimeZones.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Lookup/TimeZonesWithRegion.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Lookup/SMSCarriers.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Lookup/GetApplicationID.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Lookup/GetDatumByType.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Lookup/AccountNumberAvailable.ascx");%>
                <%Html.RenderPartial("~/Views/API/Lookup/UserNameAvailable.ascx");%>

                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Account","Account")%></legend>

                    <%Html.RenderPartial("~/Views/API/Account/CreateAccount.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/EditAccountInformation.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/EditCustomerInformation.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/GetCustomerPermissions.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/EditCustomerPermissions.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/RetrieveUsername.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/CreateAccountUser.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserDelete.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserEdit.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserScheduleGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Account/AccountUserScheduleSet.ascx");%>

                    <%--  <%Html.RenderPartial("~/Views/API/Account/AccountUserImageUpload.ascx");%>--%>

                    <%if (MonnitSession.CurrentCustomer != null)// && MonnitSession.CurrentCustomer.Account.IsReseller)
                      { %>
                    <fieldset>
                <legend><%: Html.TranslateTag("Export/APIDetail|Reseller/OEM Specific API Calls","OEM Specific API Calls")%></legend>

                        <%Html.RenderPartial("~/Views/API/Account/CreateSubAccount.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/RemoveSubAccount.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/SubAccountList.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/SubAccountTreeList.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/AccountParentEdit.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/ResellerPasswordReset.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Account/SetExpirationDate.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/RecentResellerNotification.ascx");%>

                    </fieldset>
                    <%} %>
                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Network","Network")%></legend>

                    <%Html.RenderPartial("~/Views/API/Network/CreateNetwork.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Network/CreateNetwork2.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Network/RemoveNetwork.ascx");%>
                <%Html.RenderPartial("~/Views/API/Network/NetworkList.ascx");%>

                </fieldset>

                <fieldset>
           <legend><%: Html.TranslateTag("Export/APIDetail|Gateway","Gateway")%></legend>

                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayResetDefaults.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayReform.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/RemoveGateway.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/AssignGateway.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewaySetHeartbeat.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewaySetName.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayPoint.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayCellNetworkConfig.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayAutoConfig.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Gateway/GatewayStopAutoConfig.ascx");%>
                <%Html.RenderPartial("~/Views/API/Gateway/GatewayUpdateFirmware.ascx");%>

                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Sensor","Sensor")%></legend>
 
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorResetDefaults.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/RemoveSensor.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/AssignSensor.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorGetExtended.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorGetCalibration.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorApplicationIDGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorNameGet.ascx");%>
                    <%//Html.RenderPartial("Sensor/WIFISensorIDGet"); Removed due to Not being used%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorListExtended.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/NetworkIDFromSensorGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetName.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetHeartbeat.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetThreshold.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetCalibration.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetAlerts.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSetTag.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorAttributes.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorAttributeSet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorAttributeRemove.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorSendControlCommand.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/GetDatumNameList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SetDatumName.ascx");%>

                    <fieldset>
                <legend><%: Html.TranslateTag("Export/APIDetail|Sensor Group","Sensor Group")%></legend>

                        <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupSensorList.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupList.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupCreate.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupDelete.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupAssignSensor.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Sensor/SensorGroupRemoveSensor.ascx");%>

                    </fieldset>
                    <%--<%Html.RenderPartial("~/Views/API/Sensor/SensorEquipmentGet.ascx");%>
                     <%Html.RenderPartial("~/Views/API/Sensor/SensorEquipmentSet.ascx");%>--%>
                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|DataMessage","DataMessage")%></legend>
            
                    <%Html.RenderPartial("~/Views/API/DataMessage/SensorDataMessages.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataMessage/GatewayMessages.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataMessage/AccountRecentDataMessages.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataMessage/SensorRecentDataMessages.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataMessage/AccountDataMessages.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataMessage/SensorChartMessages.ascx");%>

                </fieldset>

                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Notification","Notification")%></legend>
    
                    <%Html.RenderPartial("~/Views/API/Notification/RecentlySentNotifications.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/SentNotifications.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRecipientList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationSystemActionList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationControlUnitList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationLocalNotifierList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/SensorAssignedToNotification.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/GatewayAssignedToNotification.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/ToggleNotification.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/AccountNotificationList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/SensorNotificationList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationScheduleList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationDailyScheduleSet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationYearlyScheduleList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationYearlyScheduleSet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignSensor.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveSensor.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignGateway.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveGateway.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignRecipient.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveRecipient.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignSystemAction.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveSystemAction.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignControlUnit.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveControlUnit.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAssignLocalNotifier.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationRemoveLocalNotifier.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationAcknowledge.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationFullReset.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationPause.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationUnpause.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/NotificationDelete.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/ApplicationNotificationCreate.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/ScheduledNotificationCreate.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/BatteryNotificationCreate.ascx");%>
                    <%Html.RenderPartial("~/Views/API/Notification/InactivityNotificationCreate.ascx");%>

                    <fieldset>
                <legend><%: Html.TranslateTag("Export/APIDetail|Advanced Notification","Advanced Notification")%></legend>

                        <%Html.RenderPartial("~/Views/API/Notification/AdvancedNotificationCreate.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/NotificationListAdvancedTypes.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/NotificationListAdvancedParameters.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/NotificationListAdvancedParameterOptions.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/AdvancedNotificationParameterSet.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/AdvancedNotificationParameterList.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/NotificationListAdvancedAutomatedSchedule.ascx");%>
                        <%Html.RenderPartial("~/Views/API/Notification/AdvancedNotificationAutomatedScheduleSet.ascx");%>

                </fieldset>
            </fieldset>

                <%--                <fieldset>
                    <legend>DataPush</legend>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalDataPushList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalDataPush.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalDataPushSet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalDataPushRemove.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalGatewayDataPushList.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalGatewayDataPush.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalGatewayDataPushSet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ExternalGatewayDataPushRemove.ascx");%>
                    <%Html.RenderPartial("~/Views/API/DataPush/ResetExternalDataSubscription.ascx");%>
                </fieldset>--%>


                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Webhook","Webhook")%></legend>

                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookGet.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookCreate.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookRemove.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookResetBroken.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookAttempts.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookResend.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebHookAttemptBody.ascx");%>
                    <%Html.RenderPartial("~/Views/API/WebHook/WebhookListNotificationSettings.ascx");%>
                <%Html.RenderPartial("~/Views/API/WebHook/WebhookNotificationSettingsSet.ascx");%>

                </fieldset>
        </div>

            <div id="Div3" style="width: 100%;">
                <div class="formtitle">
                    <div id="Div4" style="display: none;"></div>
                    <div style="height: 20px">
                    <%: Html.TranslateTag("Export/APIDetail|Code Samples","Code Samples")%>
                    </div>
                </div>
            </div>

            <div class="logon">
                <fieldset>
            <legend><%: Html.TranslateTag("Export/APIDetail|Code","Code")%></legend>

                    <%Html.RenderPartial("~/Views/API/CodeSampleCSharp.ascx");%>
                    <%Html.RenderPartial("~/Views/API/CodeSamplePHP.ascx");%>
                <%Html.RenderPartial("~/Views/API/CodeSampleJavascript.ascx");%>

                </fieldset>
            </div>
         
</asp:Content>
