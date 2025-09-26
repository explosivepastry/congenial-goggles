<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<NotificationRecorded>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <!-- page content -->
    <div class="container-fluid view-btns_container">
        <!-- Event List View -->
        <div class="x_panel shadow-sm rounded powertour-hook" id="hook-three">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="col-md-6 col-6" style="width: 50%;">
                        <%: Html.TranslateTag("Events/Ack|Acknowledge")%>
                    </div>

                </div>
            </div>
            <div class="x_content" id="eventHistory">
                <div class="clearfix"></div>

                <%  
                    string actionStatus = "Resolved";
                    Customer customer = (Customer)ViewBag.Customer;
                    long TimeZoneID = customer.Account.TimeZoneID;

                    var HistoryList = NotificationHistory.LoadOngoingByNotificationID(Model.NotificationID, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow.AddMinutes(60), 15);
                    if (HistoryList.Count < 1)
                    {%>
                <%: Html.TranslateTag("Events/EventHistoryList|All events have been acknowledged.", "All events have been acknowledged.")%>
                <%}
                    else
                    {%>

                <div class="accordion accordion-flush" id="accordionFlush">

                    <% foreach (NotificationHistory item in HistoryList.Values.OrderByDescending(c => c.Event.StartTime))
                        {
                            if (item.Event.NotificationTriggeredID <= 0)
                            { continue; }

                            string deviceName = "";


                            if (item.Event.SensorNotificationID > 0)
                            {
                                SensorNotification sensNoti = SensorNotification.Load(item.Event.SensorNotificationID);
                                if (sensNoti != null)
                                {
                                    Sensor s = Sensor.Load(sensNoti.SensorID);
                                    if (s != null)
                                        deviceName = s.SensorName;
                                }
                            }
                            else if (item.Event.GatewayNotificationID > 0)
                            {
                                GatewayNotification gateNoti = GatewayNotification.Load(item.Event.GatewayNotificationID);
                                if (gateNoti != null)
                                {
                                    Gateway g = Gateway.Load(gateNoti.GatewayID);
                                    if (g != null)
                                        deviceName = g.Name;
                                }
                            }

                            if (string.IsNullOrEmpty(deviceName))
                            {
                                deviceName = "[Device Removed from Action]";
                            }

                            String acknowledgedByName = string.Empty;
                            if (string.IsNullOrEmpty(item.Event.AcknowledgedBy))
                            {
                                acknowledgedByName = string.IsNullOrEmpty(item.Event.AcknowledgeMethod) ? "System_Auto" : item.Event.AcknowledgeMethod; ;
                            }
                            else
                            {
                                acknowledgedByName = item.Event.AcknowledgedBy;
                            }


                            if (item.Event.AcknowledgedTime == DateTime.MinValue)
                            {
                                actionStatus = "Alerting";
                            }
                            else if (item.Event.resetTime == DateTime.MinValue && Notification.Load(item.Event.NotificationID).IsActive == true)
                            {
                                actionStatus = "Acknowledged";
                            }
                            else
                            {
                                actionStatus = "Resolved";
                            }%>

                    <div class="gridPanel trigger-card col-12 shadow-sm rounded">
                        <div title="<%=item.Event.NotificationID %>">

                            <div class="accordion-item">
                                <div id="actionBar_<%:item.Event.NotificationTriggeredID %>" style="font-size: 1.4em" class="col-12 trigger-card__title actionBar<%=actionStatus%>">
                                    <div class="col-6 d-flex" style="font-size: 15px; font-weight: bold; color: black;">
                                        <%=deviceName%> | <%=item.Event.Reading %>
                                    </div>
                                    <div class="col-3" style="font-size: 12px;"><strong><%: Html.TranslateTag("Date", "Date")%>:</strong> <%=item.Event.StartTime.OVToLocalDateTimeShort(TimeZoneID) %></div>
                                    <div class="col-3 text-end trigger-card__title__mobile-end" style="font-size: 12px;">

                                        <%if (customer.Can(CustomerPermissionType.Find("Notification_Can_Acknowledge")) && customer.CanSeeAccount(customer.AccountID))
                                            {%>

                                        <%if (actionStatus == "Alerting")
                                            {%>
                                        <span style="color: red; font-weight: bold;"><%: Html.TranslateTag("Events/EventHistoryList|Alerting")%> <%=Html.GetThemedSVG("notifications") %> </span>
                                        <br />
                                        <a class="btn btn-primary" title="<%: Html.TranslateTag("Events/EventHistoryList|Click to disarm future actions")%>" href="/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>" onclick="AckButton(this);return false">
                                            <%: Html.TranslateTag("Events/EventHistoryList|Acknowledge")%></a>
                                        <%}
                                            else
                                            {%>
                                        <span title="<%= acknowledgedByName%> : <%: item.Event.AcknowledgedTime.OVToLocalDateTimeShort(TimeZoneID) %>" style="color: #33cc33; font-weight: bold;"><%: Html.TranslateTag("Events/EventHistoryList|Acknowledged")%>
                                            <%=Html.GetThemedSVG("notificationsOff") %>
                                        </span>
                                        <%}%>

                                        <% }
                                        else
                                        {%>

                                        <span style="color: #33cc33; font-weight: bold;"><%: Html.TranslateTag("Events/EventHistoryList|Unauthorized to Acknowledge")%>
                                            <%=Html.GetThemedSVG("notifications") %>
                                        </span>

                                        <% }%>

                                    </div>
                                </div>
                            </div>

                            <div id="flush-collapse<%:item.Event.NotificationTriggeredID %>" class="accordion-collapse collapse col-12" aria-labelledby="flush-heading<%:item.Event.NotificationTriggeredID %>" data-bs-parent="#accordionFlush">
                                <div id="content"></div>
                            </div>
                            <div style="font-size: 1.4em; font-weight: bold; padding-bottom: 10px;" class="col-12 dfjcsb trigger-card__innerTitle">
                                <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Type", "Type")%></div>
                                <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Target", "Target")%></div>
                                <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Status", "Status")%></div>
                                <div class="col-3 ps-0 trigger-card__innerTitle__date" style="font-size: 0.7em"><%: Html.TranslateTag("Date", "Date")%></div>

                            </div>
                            <%foreach (NotificationAction action in item.NotificationActionList.OrderByDescending(c => c.NotificationDate))
                                { %>
                            <div style="font-size: 1.4em" class="eventHistoryList col-12 dfjcsb">
                                <div class="col-3" style="font-size: 0.6em">
                                    <% Sensor sens = null;
                                        string EventTarget = "";
                                        string Status = action.Status == null ? "Unavailable" : action.Status;
                                        switch (action.NotificationType)
                                        {
                                            case eNotificationType.Email: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 18 18">
                                                <path id="paper-plane-regular" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" class="list-icon_fill-1" />
                                            </svg>
                                        </span>
                                        <span>
                                            <%: Html.TranslateTag("Email", "Email")%>
                                        </span>
                                    </a>
                                    <%
                                            EventTarget = action.RecipientCustomer;
                                            break;
                                        case eNotificationType.SMS: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="16" viewBox="0 0 18 16">
                                                <path id="comments-regular" d="M16.616,44.65a5.259,5.259,0,0,0,1.375-3.507c0-2.857-2.39-5.218-5.506-5.639A6.568,6.568,0,0,0,6.493,32c-3.59,0-6.5,2.557-6.5,5.714a5.272,5.272,0,0,0,1.375,3.507A9.448,9.448,0,0,1,.19,43.182a.913.913,0,0,0-.137.893.725.725,0,0,0,.662.5,6.586,6.586,0,0,0,3.912-1.386,7.9,7.9,0,0,0,.887.175,6.548,6.548,0,0,0,5.977,3.493,7.286,7.286,0,0,0,1.869-.243A6.6,6.6,0,0,0,17.273,48a.728.728,0,0,0,.662-.5.921.921,0,0,0-.137-.893A9.187,9.187,0,0,1,16.616,44.65Zm-12.274-3.3-.534.4a6.077,6.077,0,0,1-1.347.764c.084-.168.169-.346.25-.529L3.2,40.875,2.421,40a3.478,3.478,0,0,1-.928-2.286c0-2.168,2.29-4,5-4s5,1.832,5,4-2.29,4-5,4a6,6,0,0,1-1.531-.2Zm11.221,2.075-.772.871.484,1.111c.081.182.166.361.25.529a6.077,6.077,0,0,1-1.347-.764l-.534-.4-.622.164a6,6,0,0,1-1.531.2,5.435,5.435,0,0,1-4.1-1.775c3.165-.386,5.6-2.764,5.6-5.654,0-.121-.012-.239-.022-.357,2.012.518,3.521,2.029,3.521,3.786A3.478,3.478,0,0,1,15.563,43.429Z" transform="translate(0.007 -32)" class="list-icon_fill-2" />
                                            </svg>
                                        </span>
                                        <span>
                                            <%: Html.TranslateTag("Text (SMS)", "Text (SMS)")%>
                                        </span>
                                    </a>
                                    <%
                                            EventTarget = action.RecipientCustomer;
                                            break;
                                        case eNotificationType.Local_Notifier_Display:
                                        case eNotificationType.Local_Notifier: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="18" viewBox="0 0 15 18">
                                                <path id="podcast-solid" d="M8.954,17.176C8.782,17.844,8.131,18,7.5,18s-1.282-.156-1.454-.824a26.8,26.8,0,0,1-.689-4.669c0-1.236,1.043-1.538,2.143-1.538s2.143.3,2.143,1.538a26.831,26.831,0,0,1-.689,4.669Zm-3.7-7.032a3.455,3.455,0,0,1-.964-2.552,3.306,3.306,0,0,1,3.085-3.23,3.294,3.294,0,0,1,3.341,3.372,3.449,3.449,0,0,1-.966,2.41.218.218,0,0,0,.021.323,2.316,2.316,0,0,1,.711.892.2.2,0,0,0,.316.066A5.174,5.174,0,0,0,12.32,7.609,4.96,4.96,0,0,0,7.639,2.674a4.944,4.944,0,0,0-4.961,5.06A5.169,5.169,0,0,0,4.2,11.425a.2.2,0,0,0,.317-.066,2.315,2.315,0,0,1,.711-.892.218.218,0,0,0,.021-.323ZM7.5,0A7.69,7.69,0,0,0,0,7.875a7.916,7.916,0,0,0,4.21,7.081.2.2,0,0,0,.286-.222c-.08-.545-.145-1.088-.181-1.559a.214.214,0,0,0-.09-.159,6.275,6.275,0,0,1-2.618-5.18A6.06,6.06,0,0,1,7.484,1.688a6.051,6.051,0,0,1,5.909,6.187,6.268,6.268,0,0,1-2.7,5.2c-.033.493-.1,1.075-.188,1.66a.2.2,0,0,0,.286.222A7.915,7.915,0,0,0,15,7.875,7.69,7.69,0,0,0,7.5,0Zm0,5.625a2.2,2.2,0,0,0-2.143,2.25A2.2,2.2,0,0,0,7.5,10.125a2.2,2.2,0,0,0,2.143-2.25A2.2,2.2,0,0,0,7.5,5.625Z" class="list-icon_fill-1" />
                                            </svg>
                                        </span>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|Local Notifier", "Local Notifier")%>
                                        </span>
                                    </a>
                                    <%
                                            sens = Sensor.Load(action.SentToDeviceID);
                                            EventTarget = sens == null ? "" : sens.SensorName;
                                            break;
                                        case eNotificationType.Control: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 22 22">
                                                <path id="ic_settings_input_composite_24px" d="M5,2A1,1,0,0,0,3,2v8.612H1V12H7V10.612H5ZM9,12.294a2.959,2.959,0,0,0,2,2.78V23h2V15.074a2.94,2.94,0,0,0,2-2.78V12H13.619a2.634,2.634,0,0,1-.354,1.469A1.436,1.436,0,0,1,12,14.147a1.4,1.4,0,0,1-1.187-.679A3.515,3.515,0,0,1,10.406,12H9ZM1,16a3.01,3.01,0,0,0,2,2.82V23H5V18.82A3.01,3.01,0,0,0,7,16V12H5.781v4a1.746,1.746,0,0,1-.422,1.2A1.888,1.888,0,0,1,4,17.781,1.649,1.649,0,0,1,2.672,17.2,2,2,0,0,1,2.219,16V12H1Zm20-5.388V2a1,1,0,0,0-2,0v8.612H17V12l1.313.734v-.892h3.422v.8L23,12V10.612ZM13,2a1,1,0,0,0-2,0V6H9v6h1.406V7.248h3.213V12H15V6H13Zm4,14a3.01,3.01,0,0,0,2,2.82V23h2V18.82A2.991,2.991,0,0,0,23,16V12l-1.266.641v3.368a1.95,1.95,0,0,1-.422,1.195A1.452,1.452,0,0,1,20,17.875c-.66,0-.812-.186-1.234-.672a2.2,2.2,0,0,1-.453-1.195V12.641L17,12Z" transform="translate(-1 -1)" fill="#4063AE" />
                                            </svg>
                                        </span>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|Control Unit", "Control Unit")%>
                                        </span>
                                    </a>
                                    <%
                                            sens = Sensor.Load(action.SentToDeviceID);
                                            EventTarget = sens == null ? "" : sens.SensorName;
                                            break;
                                        case eNotificationType.SystemAction: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="14" height="16" viewBox="0 0 15 18">
                                                <path id="running-solid" d="M9.808,3.375a1.71,1.71,0,0,0,1.731-1.688,1.731,1.731,0,0,0-3.462,0A1.71,1.71,0,0,0,9.808,3.375ZM4.1,11.161l-.534,1.214H1.154a1.125,1.125,0,1,0,0,2.25H3.947A1.729,1.729,0,0,0,5.537,13.6l.317-.721-.385-.221a3.377,3.377,0,0,1-1.37-1.5Zm9.747-3.287H12.259L11.319,6A3.442,3.442,0,0,0,9.091,4.212L6.528,3.469a3.516,3.516,0,0,0-2.915.6L2.183,5.14a1.106,1.106,0,0,0-.213,1.577,1.174,1.174,0,0,0,1.618.208L5.018,5.857a1.145,1.145,0,0,1,.911-.216l.53.154L5.109,8.867a2.228,2.228,0,0,0,.948,2.824l3.064,1.764-.991,3.084a1.12,1.12,0,0,0,.756,1.409A1.18,1.18,0,0,0,9.232,18a1.152,1.152,0,0,0,1.1-.789l1.141-3.553a1.676,1.676,0,0,0-.78-1.912L8.485,10.475,9.614,7.723l.731,1.457a1.746,1.746,0,0,0,1.554.945h1.947a1.125,1.125,0,1,0,0-2.25Z" class="list-icon_fill-2" />
                                            </svg>
                                        </span>
                                        <%--                        <span style="width: 30px;">
                                            <%=Html.GetThemedSVG("app101") %>
                                        </span>--%>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|System Action", "System Action")%>
                                        </span>
                                    </a>
                                    <%
                                            string[] temp = action.SerializedRecipientProperties.Split('|');
                                            Notification noti = Notification.Load(temp[1].ToLong());
                                            EventTarget = noti == null ? "" : noti.Name;
                                            break;
                                        case eNotificationType.Phone: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 18">
                                                <path id="iconmonstr-phone-13" d="M1.942.991,4.075,0,7.106,5.917,5.063,6.923c-.4.944,1.619,4.717,2.473,4.779.067-.043,2-1,2-1l3.082,5.949s-2.073,1.015-2.14,1.047A3.267,3.267,0,0,1,9.1,18C4.863,17.986.035,10.177,0,5-.012,3.2.559,1.712,1.942.991Zm1.447.955-.767.378C-1.348,4.4,5.755,18.214,9.859,16.339l.729-.356L8.884,12.7l-.769.376c-2.372,1.16-6.2-6.164-3.791-7.453l.755-.373L3.39,1.947ZM15,12.75h-.75V3H15Zm-1.5-1.5h-.75V4.5h.75Zm3-.75h-.75V5.25h.75ZM12,9.75h-.75V6H12ZM10.5,9H9.75V6.75h.75ZM18,9h-.75V6.75H18ZM9,8.25H8.25V7.5H9Z" transform="translate(0)" fill-rule="evenodd" class="list-icon_fill-3" />
                                            </svg>
                                        </span>
                                        <%--                        <span style="width: 30px;">
                                            <%=Html.GetThemedSVG("app50") %>
                                        </span>--%>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|Voice", "Voice")%> 
                                        </span>
                                    </a>
                                    <%
                                            EventTarget = action.RecipientCustomer;
                                            break;
                                    %>

                                    <%    case eNotificationType.HTTP: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                                                <path id="iconmonstr-rocket-7" d="M7.411 21.39l-4.054 2.61-.266-1.053c-.187-.744-.086-1.534.282-2.199l2.617-4.729c.387 1.6.848 3.272 1.421 5.371zm13.215-.642l-2.646-4.784c-.391 1.656-.803 3.22-1.369 5.441l4.032 2.595.266-1.053c.186-.743.085-1.533-.283-2.199zm-10.073 3.252h2.895l.552-2h-4l.553 2zm1.447-24c-3.489 2.503-5 5.488-5 9.191 0 3.34 1.146 7.275 2.38 11.809h5.273c1.181-4.668 2.312-8.577 2.347-11.844.04-3.731-1.441-6.639-5-9.156zm.012 2.543c1.379 1.201 2.236 2.491 2.662 3.996-.558.304-1.607.461-2.674.461-1.039 0-2.072-.145-2.641-.433.442-1.512 1.304-2.824 2.653-4.024z" fill-rule="evenodd" class="list-icon_fill-4" />
                                            </svg>
                                        </span>
                                        <%--                        <span style="width: 30px;">
                                            <%=Html.GetThemedSVG("rocket") %>
                                        </span>--%>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|Notification Webhook", "Notification Webhook") %>
                                        </span>
                                    </a>
                                    <%
                                            EventTarget = "Notification Webhook";
                                            NotificationRecorded ntfcRcrd = NotificationRecorded.Load(action.NotificationRecordedID);
                                            Status = !ntfcRcrd.Delivered ? "Undelivered" : ntfcRcrd.Status;
                                            break;
                                    %>

                                    <%  case eNotificationType.Thermostat: %>
                                    <a class="dfac">
                                        <span style="width: 20px;">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="13" height="20" viewBox="0 0 11 18">
                                                <path id="iconmonstr-weather-136" d="M7.5,1.066c.7,0,1.783.939,1.783,1.559V8.269a2.492,2.492,0,0,0,1.179,2.288,3.125,3.125,0,0,1,1.245,2.569c0,1.861-2.108,3.7-4.207,3.7s-4.271-1.837-4.271-3.7a3.186,3.186,0,0,1,1.31-2.569A2.491,2.491,0,0,0,5.717,8.269V2.625C5.717,2,6.8,1.066,7.5,1.066ZM7.5,0A2.809,2.809,0,0,0,4.538,2.625V8.269a1.438,1.438,0,0,1-.6,1.144A4.656,4.656,0,0,0,2,13.125C2,15.818,4.461,18,7.5,18S13,15.818,13,13.125a4.656,4.656,0,0,0-1.942-3.712,1.438,1.438,0,0,1-.6-1.144V2.625A2.809,2.809,0,0,0,7.5,0ZM9.063,11.038A2.643,2.643,0,0,1,7.923,8.623V3.75H7.077V8.623a2.722,2.722,0,0,1-1.233,2.415,2.408,2.408,0,0,0-.855,2.087c0,1.45,1.268,2.05,2.512,2.05s2.465-.6,2.465-2.05A2.443,2.443,0,0,0,9.063,11.038Z" transform="translate(-2)" fill="#E24E4E" />
                                            </svg>
                                        </span>
                                        <span>
                                            <%: Html.TranslateTag("Events/EventHistoryList|Thermostat", "Thermostat")%> 
                                        </span>
                                    </a>
                                    <%
                                            EventTarget = action.SentTo;
                                            break;
                                        } %>
                                </div>
                                <div class="col-3" style="font-size: 0.6em" title="<%=action.SentTo %>"><%=EventTarget %></div>
                                <div class="col-3" style="font-size: 0.6em"><%= Status %></div>
                                <div class="col-3" style="font-size: 0.6em">
                                    <span class="date-time_container">
                                        <%=action.NotificationDate.OVToLocalDateTimeShort(TimeZoneID) %>
                                    </span>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <%} %>
                        </div>
                    </div>


                    <%} %>

                    <!-- History-->

                </div>
                <script>

                    function AckButton(anchor) {
                        var href = $(anchor).attr('href');
                        $.post(href, function (data) {
                            if (data == "Success") {
                                window.location.href = window.location.href;
                            }
                            else {
                                let values = {};
                                values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';
                                values.text = '<%: Html.TranslateTag("Events/EventHistoryList|failed to acknowledge notification", "failed to acknowledge notification")%>';
                                openConfirm(values);
                                $('#modalCancel').hide();
                            }
                        });
                    }

                </script>

                <%} %>
            </div>
        </div>
    </div>
    <!-- page content -->

    <style>
        .actionBarAlerting {
            background-color: #ffcaca;
        }

        .actionBarAcknowledged {
            background-color: #ffffcc;
        }
    </style>
</asp:Content>
