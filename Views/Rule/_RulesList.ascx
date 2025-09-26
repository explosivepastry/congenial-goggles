<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification.RuleFilterResult>" %>
<%=""%> <%-- Fix for Intellisense Error CS0103 --%>
<%
    List<Notification> notifications = Model.Notifications;
    List<NotificationTriggered> notificationsTriggered = Model.NotificationsTriggered;

    if (notifications.Count < 1)
    {%>
<%: Html.TranslateTag("Events/Details|No Events found for this account","No Events found for this account")%>.
<%}
    else
    {
        if (!TempData.ContainsKey("ShowEditLink"))
            TempData.Add("ShowEditLink", MonnitSession.CustomerCan("Notification_Edit"));
        foreach (Notification item in notifications)

        {
            bool isFavorite = MonnitSession.IsNotificationFavorite(item.NotificationID);
            string svgTemp = "";
            switch (item.NotificationClass)
            {
                case eNotificationClass.Application:
                    svgTemp = Html.GetThemedSVG("sensor").ToString();
                    break;
                case eNotificationClass.Low_Battery:
                    svgTemp = Html.GetThemedSVG("lowBattery").ToString();
                    break;
                case eNotificationClass.Advanced:
                    svgTemp = Html.GetThemedSVG("gears").ToString();
                    break;
                case eNotificationClass.Inactivity:
                    svgTemp = Html.GetThemedSVG("hourglass").ToString();
                    break;
                case eNotificationClass.Timed:
                    svgTemp = Html.GetThemedSVG("clock").ToString();
                    break;
            }
            string backgroundOverwrite = "";
            string classOverwrite = "";
            string respond = "";
            string ruleHref = "";
            string ruleHrefTwo = "";
            string ruleTitle = "";
            string ruleStatus = "Ready";
            MvcHtmlString alertingIcon = new MvcHtmlString("");

            NotificationTriggered notificationTriggered = notificationsTriggered.Where(nt => nt.NotificationID == item.NotificationID).FirstOrDefault();
            if (!item.IsActive)
            {
                ruleStatus = "Disabled";
            }
            else if (notificationTriggered != null)
            {
                if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                {
                    backgroundOverwrite = "background:#ffcaca;";
                    alertingIcon = Html.GetThemedSVG("notifications");
                    classOverwrite = "Acknowledge";
                    respond = "AckAllButton(this);return false;";
                    ruleHref = "/Notification/AcknowledgeActiveNotifications";
                    ruleHrefTwo = "&AckMethod=Browser_MainList";
                    ruleTitle = "Click to disarm alarming rule ";
                    ruleStatus = "Alerting";
                }
                else if (notificationTriggered.resetTime == DateTime.MinValue)
                {
                    backgroundOverwrite = "background-color:#ffedd4;";
                    backgroundOverwrite = "background-color:#ffedd4;";
                    alertingIcon = Html.GetThemedSVG("reset");
                    classOverwrite = "Reset";
                    respond = "ResetAllPending(this);return false;";
                    ruleHref = "/Notification/ResetPendingConditions";
                    ruleTitle = "Condition pending reset. Click to force reset.";
                    ruleStatus = "Acknowledged";
                }
                else if (item.LastSent > DateTime.MinValue)
                {
                    ruleStatus = "Resolved";
                }
            }


%>
<%--RuleList--%>
<div class="small-list-card 44444" style="<%: backgroundOverwrite %>">
    <a id="actionDiv_<%:item.NotificationID%>" class="corp-status change-rule-status" data-id="<%:item.NotificationID%>" <%--title="click to <%:item.IsActive ? "disable" : "enable"%>"--%> <%--onclick="toggleRuleStatus(this);"--%> <%--style="height: 100%;--%> /*cursor: pointer; */">
        <div class="sensor corp-status sensorIcon <%:item.IsActive ? "sensorStatusOK " : "sensorStatusInactive"%>" style="height: 100%;"></div>
    </a>
    <div style="width: 100%; padding: 5px;">
        <a href="/Rule/History/<%:item.NotificationID%>">


            <div class="d-flex align-items-center viewSensorDetails eventsList__tr">
                <div class="holder holderInactive dfjcac" style="align-items:baseline; margin-bottom: 5px;">
                    <div class="svgTemp"><%=svgTemp %></div>
                     <div class="glance-name" style="margin-left:10px;font-weight:bold;color:#67696d"><%=item.Name%></div>
                </div>
                <div>
                    <div class="glance-text">
                      <%--  <div class="glance-name"><%=item.Name%></div>--%>
                        <div class="glance-reading">
                            <%: Html.TranslateTag("Events/Details|Last Sent","Last Sent")%>:
                                <%if (item.LastSent > DateTime.MinValue)
                                    { %>
                            <%:item.LastSent.OVToLocalDateTimeShort()%>
                            <%} %>
                            <br />
                            <%:Html.Partial("~/Views/Rule/_RuleConditionAsSentenceMain.ascx",item) %>
                        </div>
                    </div>
                </div>
                <%if (TempData["ShowEditLink"] != null && (bool)TempData["ShowEditLink"] == true)
                    { %>
                <div style="margin-left: auto; display: flex; align-items: center;">
                    <div>
                        <a class="rule-trigger-btn ruleInfo-<%:classOverwrite%>" title="<%:ruleTitle %>" style="margin-right: 5px" href="<%:ruleHref%>?notificationID=<%:item.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%><%:ruleHrefTwo%>" onclick="<%:respond%>" value="<%:item.IsActive  %>">
                            <%=alertingIcon %>        
                        </a>
                    </div>
                    <div class="menu-hover menu-fav"  data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                          <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex": "none" %>  ; align-items:start; justify-content:center;"  <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
           <div class="listOfFav"> <%= Html.GetThemedSVG("heart-beat")  %></div> 
                        </div>
                        <%=Html.GetThemedSVG("menu") %>
                    </div>

                    <ul class="dropdown-menu ddm" style="padding: 0;">
                        <% bool showtest = true;

                            if (notificationTriggered != null)
                            {
                                if (ruleStatus == "Alerting")
                                {
                                    showtest = false;%>
                        <li style="<%: backgroundOverwrite %>">
                            <a title="<%: Html.TranslateTag("Shared/TopBarEventList|Click to disarm alerting rule")%>" class="dropdown-item menu_dropdown_item" href="/Notification/AcknowledgeActiveNotifications?notificationID=<%:item.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%>&AckMethod=Browser_MainList" onclick="AckAllButton(this);return false;">
                                <span style="font-weight: bold;"><%: Html.TranslateTag("Acknowledge")%></span>
                                <i style="color: #ff4d2d; margin-right: 0px; font-size: 1.2em; font-weight: bold;" class="fa fa-bell-o ackBellListPage"></i>
                            </a>
                        </li>
                        <%}
                            else if (ruleStatus == "Acknowledged" && item.IsActive == true)
                            {
                                showtest = false;%>
                        <li style="<%: backgroundOverwrite %>">

                            <a title="<%: Html.TranslateTag("Acknowledged")%> <%:notificationTriggered.AcknowledgedTime.ToShortDateString() %>, <%: Html.TranslateTag("Condition pending reset. Click to force reset.")%>" class="dropdown-item menu_dropdown_item" href="/Notification/ResetPendingConditions?notificationID=<%:item.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%>" onclick="ResetAllPending(this);return false;">
                                <span style="font-weight: bold;"><%: Html.TranslateTag("Reset")%></span>
                                <%=Html.GetThemedSVG("reset") %>
                            </a>

                        </li>

                        <%}
                            } %>

                        <%if (item.NotificationRecipients.Count > 0 && showtest)
                            {%>
                        <li>
                            <a class="dropdown-item menu_dropdown_item notitest" title="Send Test" style="cursor: pointer;" id="notiTest" onclick="SendTest('<%=item.NotificationID %>');">
                                <span><%: Html.TranslateTag("Send Test","Send Test")%></span>
                                <%=Html.GetThemedSVG("sendTest") %>
                            </a>
                        </li>
                        <span id="testMessage_<%=item.NotificationID %>" style="color: red; font-weight: bold; font-size: 0.8em;"></span>
                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                        <%} %>

                        <%if (MonnitSession.CustomerCan("Notification_Disable_Network"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" data-id="<%:item.NotificationID%>" onclick="toggleRuleStatus($('#actionDiv_<%:item.NotificationID%>'),true);">
                                <span id="toggleText_<%:item.NotificationID%>"><%:item.IsActive ?  Html.TranslateTag("Disable") :Html.TranslateTag("Enable")%></span>
                                <%=Html.GetThemedSVG("disable") %>
                            </a>
                        </li>
                        <%} %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/History/<%:item.NotificationID%>">
                                <span><%: Html.TranslateTag("History","History")%></span>
                                <%=Html.GetThemedSVG("list") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/Triggers/<%:item.NotificationID%>">
                                <span><%: Html.TranslateTag("Conditions")%></span>
                                <%=Html.GetThemedSVG("conditions") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/ChooseTaskToEdit/<%:item.NotificationID%>">
                                <span><%: Html.TranslateTag("Tasks")%></span>
                                <%=Html.GetThemedSVG("tasks") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/Calendar/<%:item.NotificationID%>">
                                <span><%: Html.TranslateTag("Schedule","Schedule")%></span>
                                <%=Html.GetThemedSVG("schedule") %>
                            </a>
                        </li>
                        <%if (MonnitSession.CustomerCan("Notification_Edit"))
                            { %>
                        <hr />
                        <li>
                            <a class="dropdown-item menu_dropdown_item" onclick="deleteConfirmation(<%=item.NotificationID %>)" id="list">
                                <span>
                                    <%: Html.TranslateTag("Events/Triggers|Delete Rule")%> 
                                </span>
                                <%=Html.GetThemedSVG("delete") %>
                            </a>
                        </li>
                        <%} %>
                    </ul>
                </div>


                <%}%>

            </div>
        </a>
         <span <%=item.AlwaysSend == false ? "style='display:flex; justify-content:flex-end; align-items:center;'" : "style='display:none;'"%> title="<%: Html.TranslateTag("Has Rule Schedule","Has Rule Schedule")%>" class="pendingScheduleList">
         <%=Html.GetThemedSVG("schedule") %>
        </span>
    </div>
</div>
<% }
    } %>
<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("_RulesList.ascx") %>
    var areYouSureConfirmString = '<%= Html.TranslateTag("Are you sure you want to reset this rule?")%>';
    var areYouSureDeleteString = '<%= Html.TranslateTag("Are you sure you want to delete this rule?")%>';
    var enableString = '<%= Html.TranslateTag("Enable")%>';
    var disableString = '<%= Html.TranslateTag("Disable")%>';
    var sendingString = '<%= Html.TranslateTag("Sending")%>';
    $(document).ready(function () {

        $('#filterdEvents').html('<%:ViewBag.EventCount%>');
        $('#totalEvents').html('<%:ViewBag.TotalNotis%>');

        $('.ackBellListPage').hover(function () {
            $(this).removeClass('fa fa-bell-o').addClass('fa fa-bell-slash-o');
        }, function () {
            $(this).removeClass('fa fa-bell-slash-o').addClass('fa fa-bell-o');
        });

        $('.resetBellListPage').hover(function () {
            $(this).removeClass('fa fa-refresh').addClass('fa fa-check');
        }, function () {
            $(this).removeClass('fa fa-check').addClass('fa fa-refresh');
        });


    });

    function toggleRuleStatus(anchor) {
        var div = $(anchor).children('div.sensor');

        if (div.hasClass("sensorStatusOK")) {
            $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": false }, function (data) {
                if (data == "Success") {
                    div.addClass("sensorStatusInactive");
                    div.removeClass("sensorStatusOK");
                    $("#toggleText_" + $(anchor).data("id")).html(enableString);
                }
            });
        }
        else {
            $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": true }, function (data) {
                if (data == "Success") {
                    div.addClass("sensorStatusOK");
                    div.removeClass("sensorStatusInactive");
                    $("#toggleText_" + $(anchor).data("id")).html(disableString);
                }
            });
        }
    }

    function clearTestMessage(notificationID) {
        $('#testMess').click();
        $('#testMessage_' + notificationID).html("");
        $("#sendTestListPage_" + notificationID).html('<i style="color: #51535b; margin-right: 0px; font-size: 1.2em;" class="fa fa-share-square-o"></i>');
    }

    function deleteConfirmation(item) {
        let values = {};
        values.url = `/Rule/Delete/${item}`;
        values.text = areYouSureDeleteString;
        openConfirm(values);
    }

    function SendTest(notificationID) {
        event.stopPropagation();
        $("#sendTestListPage_" + notificationID).html(sendingString + "...");

        $.post('/Notification/Test/' + notificationID, function (data) {
            $('#testMessage_' + notificationID).html(data);
        });

        setTimeout(clearTestMessage(notificationID), 5000)
    }

    function AckAllButton(anchor) {

        var href = $(anchor).attr('href');
        $.post(href, function (data) {
            if (data == "Success") {
                window.location.href = window.location.href;
            }
            else if (data == "Unauthorized") {
                showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge rules")%>");
            }
            else
                showSimpleMessageModal("<%=Html.TranslateTag("Acknowledge rule failed.")%>");
        });
    }

    function ResetAllPending(anchor) {

        var href = $(anchor).attr('href');
        if (confirm(areYouSureConfirmString)) {
            $.post(href, function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                }
                else if (data == "Unauthorized") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to reset rules")%>");
                }
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("Reset Failed")%>");
            });
        }
    }
    $(document).ready(function () {
        $(".listOfFav svg").addClass("liked");
    });

</script>
