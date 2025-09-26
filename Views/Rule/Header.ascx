<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<div class="mb-4">
    <div class="col-12 view-btns_container top-nav-gap-row shadow-sm rounded">
        <div class="view-btns-half view-btns deviceView_btn_row rounded">
            <a href="/Rule" class="btn btn-lg">
                <div class=" btn-secondaryToggle btn-lg btn-fill "><%=Html.GetThemedSVG("rules") %><span class="extra"><%: Html.TranslateTag("Rules") %></span></div>
            </a>
            <a href="/Rule/History/<%:Model.NotificationID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/History/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
                    <%=Html.GetThemedSVG("list") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("History", "History")%>
                    </span>
                </div>
            </a>
            <%if (MonnitSession.CustomerCan("Notification_Edit"))
                {%>
            <a href="/Rule/Triggers/<%:Model.NotificationID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/Triggers/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
                    <%=Html.GetThemedSVG("conditions") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Events/Header|Conditions")%>
                    </span>
                </div>
            </a>
            <a href="/Rule/ChooseTaskToEdit/<%:Model.NotificationID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/ChooseTaskToEdit/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
                    <%=Html.GetThemedSVG("tasks") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Events/Header|Tasks")%>
                    </span>
                </div>
            </a>
            <a href="/Rule/RuleEdit/<%:Model.NotificationID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/RuleEdit/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle">
                    <%=Html.GetThemedSVG("edit") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Events/Header|Edit", "Edit")%>
                    </span>
                </div>
            </a>

            <%if (Model.AdvancedNotificationID != long.MinValue && AdvancedNotification.Load(Model.AdvancedNotificationID).AdvancedNotificationType == eAdvancedNotificationType.AutomatedSchedule)
                {%>
            <a href="/Rule/AutomatedSchedule/<%:Model.NotificationID %>" class="deviceView_btn_row__device <%:Request.Path.StartsWith("/Rule/AutomatedSchedule/") ? "calendar-fill-mobile" : " " %>">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/AutomatedSchedule/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle calendar-mobile-fill">
                    <%=Html.GetThemedSVG("schedule") %>
                    <span class="extra"><%: Html.TranslateTag("Events/Header|Schedule", "Schedule")%>
                    </span>
                </div>
            </a>
            <%}
                else
                { %>
            <a href="/Rule/Calendar/<%:Model.NotificationID %>" class="deviceView_btn_row__device <%:Request.Path.StartsWith("/Rule/Calendar/") ? "calendar-fill-mobile" : " " %>">
                <div class="btn-<%:Request.Path.StartsWith("/Rule/Calendar/") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill btn-secondaryToggle calendar-mobile-fill">
                    <%=Html.GetThemedSVG("schedule") %>
                    <span class="extra"><%: Html.TranslateTag("Events/Header|Schedule", "Schedule")%>
                    </span>
                </div>
            </a>
            <%} %>
            <%} %>
        </div>
    </div>
    <%MvcHtmlString iconClassName = new MvcHtmlString("");
        switch (Model.NotificationClass)
        {
            case eNotificationClass.Application:
                iconClassName = Html.GetThemedSVG("sensor");
                break;
            case eNotificationClass.Low_Battery:
                iconClassName = Html.GetThemedSVG("lowBattery");
                break;
            case eNotificationClass.Timed:
                iconClassName = Html.GetThemedSVG("clock");
                break;
            case eNotificationClass.Inactivity:
                iconClassName = Html.GetThemedSVG("hourglass");
                break;
            case eNotificationClass.Advanced:
                iconClassName = Html.GetThemedSVG("gears");
                break;
        }




        bool showtest = true;
        var notificationTriggered = NotificationTriggered.LoadActiveByNotificationID(Model.NotificationID);
        if ((notificationTriggered != null && notificationTriggered.Count > 0) && notificationTriggered.FirstOrDefault().AcknowledgedTime != DateTime.MinValue && notificationTriggered.FirstOrDefault().resetTime == DateTime.MinValue && Model.IsActive == true)
            showtest = false;
    %>
    <%
        string backgroundOverwrite = "";
        string classOverwrite = "";
        string respond = "";
        string ruleHref = "";
        string ruleHrefTwo = "";
        string ruleTitle = "";
        string ruleStatus = "Ready";

        MvcHtmlString alertingIcon = new MvcHtmlString("");
        //List<NotificationTriggered> notificationTriggered = NotificationTriggered.LoadActiveByNotificationID(Model.NotificationID);
        if (notificationTriggered != null && notificationTriggered.Count > 0)
        {
            if (notificationTriggered.FirstOrDefault().AcknowledgedTime == DateTime.MinValue)
            {
                showtest = false;
                backgroundOverwrite = "background:#ffcaca;";
                alertingIcon = Html.GetThemedSVG("notifications");
                classOverwrite = "Acknowledge";
                respond = "AckAllButton(this);return false;";
                ruleHref = "/Notification/AcknowledgeActiveNotifications";
                ruleHrefTwo = "&AckMethod=Browser_MainList";
                ruleTitle = "Click to disarm alarming rule ";
                ruleStatus = "Alerting";
            }
            else if (notificationTriggered.FirstOrDefault().AcknowledgedTime != DateTime.MinValue && notificationTriggered.FirstOrDefault().resetTime == DateTime.MinValue && Model.IsActive == true)
            {
                showtest = false;
                backgroundOverwrite = "background-color:#ffedd4;";
                alertingIcon = Html.GetThemedSVG("reset");
                classOverwrite = "Reset";
                respond = "ResetAllPending(this);return false;";
                ruleHref = "/Notification/ResetPendingConditions";
                ruleTitle = "Condition pending reset. Click to force reset.";
                ruleStatus = "Acknowledged";
            }
            else if (Model.LastSent > DateTime.MinValue)
            {
                ruleStatus = "Resolved";
            }
        }

        if (!Model.IsActive)
        {
            ruleStatus = "Disabled";
        }

        bool isFavorite = MonnitSession.IsNotificationFavorite(Model.NotificationID);
        string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
        string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
    %>

    <div class="" data-intro="How would you like to view this data?" data-step="1" data-position="left">
        <div class="rule-info d-flex" style="width: clamp(295px, 50%, 50%)">
            <div style="width: 100%; padding: 10px;" title="<%=ruleStatus %>">
                <div class="card_container__top" style="border-bottom: .4px solid #e6e6e6">
                    <div class="card_container__top__title" style="margin: 0 5px; border-bottom: none;">
                        <%= Html.TranslateTag("Rule Information") %>
                    </div>
                    <div class="ruleSelectOptionsTop" style="cursor: pointer;">

                        <%if (Model.AlwaysSend.ToInt() == 0)
                           {%>
                        <td width="150" style="text-align: center;">
                            <div>
                                <span title="Sensor Schedule" display: flex; padding-top: 1px;" id="sensorSchedule" style="cursor:default;">
                                    <%=Html.GetThemedSVG("schedule") %>
                                </span>
                            </div>
                        </td>
                        <%} %>

                        <span title="<%:isFavorite ? "Unfavorite Rule" : "Favorite Rule"%>" id="favoriteItem" data-id="<%=Model.NotificationID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                            onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'notification')">
                            <%:Html.GetThemedSVG("heart-beat") %>                
                        </span>


                        <%if (Model.IsActive && MonnitSession.CustomerCan("Notification_Disable_Network"))
                            {%>
                        <div>
                            <a id="ruleGreen<%:Model.NotificationID %>" style="cursor: pointer" data-id="<%:Model.NotificationID %>" onclick="toggleRuleStatus(<%:Model.NotificationID %>);" title=" <%:Model.IsActive ? "Click to Disable" : "Click to Enable"%>">
                                <svg xmlns="http://www.w3.org/2000/svg" id="svg_disable" class="svg_icon" viewBox="0 0 15 15">
                                    <path fill="#37BC9B" d="M11.333,3H9.667v8.333h1.667Zm4.025,1.808L14.175,5.992A5.767,5.767,0,0,1,16.333,10.5,5.833,5.833,0,1,1,6.817,5.983L5.642,4.808A7.493,7.493,0,1,0,18,10.5,7.444,7.444,0,0,0,15.358,4.808Z" transform="translate(-3 -3)" />
                                </svg>
                            </a>
                        </div>
                        <%}
                            else if (MonnitSession.CustomerCan("Notification_Disable_Network"))
                            {%>
                        <div>
                            <a id="ruleGrey<%:Model.NotificationID %>" style="cursor: pointer" data-id="<%:Model.NotificationID %>" onclick="toggleRuleStatus(<%:Model.NotificationID %>);" title=" <%:Model.IsActive ? "Click to Disable" : "Click to Enable"%>">
                                <svg xmlns="http://www.w3.org/2000/svg" id="svg_disable" class="svg_icon" viewBox="0 0 15 15">
                                    <path fill="#a4a4a47a" d="M11.333,3H9.667v8.333h1.667Zm4.025,1.808L14.175,5.992A5.767,5.767,0,0,1,16.333,10.5,5.833,5.833,0,1,1,6.817,5.983L5.642,4.808A7.493,7.493,0,1,0,18,10.5,7.444,7.444,0,0,0,15.358,4.808Z" transform="translate(-3 -3)" />
                                </svg>
                            </a>
                        </div>
                        <%} %>
                        <div>
                            <a class="rule-trigger-btn ruleInfo-<%:classOverwrite%>" title="<%:ruleTitle %>" style="margin-right: 5px" href="<%:ruleHref%>?notificationID=<%:Model.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%><%:ruleHrefTwo%>" onclick="<%:respond%>" value="<%:Model.IsActive  %>">
                                <%=alertingIcon %>
                            </a>
                        </div>

                        <%if (Model.NotificationRecipients.Count > 0 && showtest)
                            {%>
                        <td width="150" style="text-align: center;">
                            <div>
                                <a title="Send Test" href="#" display: flex; padding-top: 1px;" id="notiTest">
                                    <%=Html.GetThemedSVG("sendTest") %>
                                </a>
                            </div>
                            <br />
                            <span id="testMessage" style="color: red; font-weight: bold; font-size: 0.8em;"></span>
                        </td>
                        <%} %>

                        <div>
                            <%if (MonnitSession.CustomerCan("Notification_Edit"))
                                { %>
                            <a onclick="deleteConfirmation(<%=Model.NotificationID %>)">

                                <%=Html.GetThemedSVG("delete") %>
                            </a>
                            <%} %>
                        </div>
                    </div>
                </div>

                <div class="rule-on-off" style="<%: backgroundOverwrite%>;">
                    <table class="rI-dets" width="100%" style="padding: 3px;">
                        <div id="ruleONOFF" data-id="<%:Model.NotificationID%>">
                            <div id="ruleStatusDiv_<%:Model.NotificationID %>" class="sensor corp-status sensorIcon <%:Model.IsActive ? "sensorStatusOK" : "sensorStatusInactive"%>">
                            </div>
                        </div>
                        <tr class="viewSensorDetails" style="width: 100%; display: flex; justify-content: space-between; align-items: center;">
                            <td width="70" class="powertour-hook" id="hook-one">
                                <div class="divCellCenter holder holderInactive dfjcac">
                                    <div class="notify-icon d-flex"><%=iconClassName %></div>
                                </div>
                            </td>
                            <td valign="middle" style="width: 70%;">
                                <div class="glance-text">
                                    <div class="rule-name"><%=Model.Name%></div>
                                    <div class="glance-reading">
                                        <%: Html.TranslateTag("Events/Header|Last Sent","Last Sent")%>: 
                                        <%if (Model.LastSent > DateTime.MinValue)
                                            { %>
                                        <%:Model.LastSent.OVToLocalTimeShort()%>, <%:Model.LastSent.OVToLocalDateShort()%> ,
                                        <%} %>
                                        <br />
                                       <div id="RuleInfoCardConditionText">
                                        <%:Html.Partial("~/Views/Rule/_RuleConditionAsSentenceMain.ascx") %>
                                       </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>


</div>
<!-- End Event Info Panel -->

<script>

    <%= ExtensionMethods.LabelPartialIfDebug("Header.ascx")  %>

    var areYouSureConfirmString = "<%= Html.TranslateTag("Are you sure you want to reset this rule?")%>";
    var areYouSureDeleteString = "<%= Html.TranslateTag("Are you sure you want to delete this rule?")%>";
    var enableString = "<%= Html.TranslateTag("Click to Enable")%>";
    var disableString = "<%= Html.TranslateTag("Click to Disable")%>";

    $(document).ready(function () {
        $('#notiTest').click(function (e) {
            $.post('/Notification/Test/<%:Model.NotificationID %>', function (data) {

                if (data == "Please assign a device to this notification first") {
                    toastBuilder("Please assign a device to this notification first")
                } else {
                    toastBuilder(data, "Success");
                }
            });
            setTimeout(clearTestMessage, 5000)
        });

        isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
    });


    function clearTestMessage() {
        $('#testMessage').html("");
    }
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    );

    function AckAllButton(anchor) {

        var href = $(anchor).attr('href');
        $.post(href, function (data) {
            if (data == "Success") {
                location.reload();
            }
            else if (data == "Unauthorized") {
                showSimpleMessageModal("<%: Html.TranslateTag("Unauthorized: User does not have permission to acknowledge rules.")%>");
            }
            else
                showSimpleMessageModal("<%: Html.TranslateTag("Acknowledge rule failed.")%>");
        });

    }

    function ResetAllPending(anchor) {
        let values = {};
        values.text = areYouSureConfirmString;
        values.url = $(anchor).attr('href');
        values.callback = function (data) {
            if (data == "Success") {
                location.reload();
            }
            else if (data == "Unauthorized") {
                showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to reset rules")%>");
            }
            else
                showSimpleMessageModal("<%=Html.TranslateTag("Reset Failed")%>");
        }
        openConfirm(values);
    }

    function deleteConfirmation(item) {
        let values = {};
        values.url = `/Rule/Delete/${item}`;
        values.text = areYouSureDeleteString;
        values.redirect = "/Rule/Index";
        openConfirm(values);
    }

    function toggleRuleStatus(notificationID) {
        var statusDiv = $("#ruleStatusDiv_" + notificationID);
        var AckMethod = "Full reset from Setting Rule inactive";
        if (statusDiv.hasClass("sensorStatusOK")) {
            $.post("/Rule/ToggleRule", { "id": notificationID, "ackMethod": AckMethod, "active": false }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusInactive");
                    statusDiv.removeClass("sensorStatusOK");
                    $("#rule" + notificationID).prop('title', 'Click to Enable');
                    window.location.reload();
                }
            });
        }
        else {
            $.post("/Rule/ToggleRule", { "id": notificationID, "ackMethod": AckMethod, "active": true }, function (data) {
                if (data == "Success") {
                    statusDiv.addClass("sensorStatusOK");
                    statusDiv.removeClass("sensorStatusInactive");
                    $("#rule" + notificationID).prop('title', 'Click to Disable');
                    window.location.reload();
                }
            });
        }
    }
</script>

<style>
    #svg_arrowLeft {
        fill: #666 !important;
    }

    #svg_sendTest {
        width: 25px;
        height: 25px;
    }
</style>
