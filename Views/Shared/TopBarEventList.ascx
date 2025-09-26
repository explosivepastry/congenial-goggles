<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NotificationTriggered>>" %>
<div id="TopBarListOfTriggeredRules">
    <ul class="p-0">
        <li>
            <a class="dropdown-item text-center" onclick="goToLinkStopProp('/Rule/Index');">
                <strong class="mx-auto"><%: Html.TranslateTag("Shared/TopBarEventList|See All Rules", "See All Rules")%></strong>
            </a>
        </li>

        <%  
            List<NotificationTriggered> AlertingNotis = Model.Where(c => c.AcknowledgedTime == DateTime.MinValue).ToList();
            List<NotificationTriggered> PendingNotis = Model.Where(c => c.AcknowledgedTime != DateTime.MinValue && c.resetTime == DateTime.MinValue).ToList();

            foreach (NotificationTriggered triggered in AlertingNotis)
            { %>

        <li class="notiMenuItem badMenu" id="ruleAcknowledge_<%=triggered.NotificationID%>">
            <div class="menuContainer">
                <div class="menu-ico1">
                    <a class="menu-icon-topBar" title="<%: Html.TranslateTag("Shared/TopBarEventList|Click to disarm future actions", "Click to disarm future actions")%>" data-id="<%:triggered.NotificationID%>" href="/Notification/AcknowledgeActiveNotifications?notificationID=<%:triggered.NotificationID%>&userID=<%: MonnitSession.CurrentCustomer.CustomerID %>&AckMethod=Browser_TopBar" onclick="AckButton(this);return false">
                        <span id="bellbell" class="resetBell rule-trigger-btn ruleInfo-Acknowledge bell-see-rules">
                            <%=Html.GetThemedSVG("notifications") %>
                        </span>
                    </a>
                </div>
                <div class="d-flex">
                    <a class="top-menu-link" title="<%: Html.TranslateTag("Shared/TopBarEventList|Triggered Rule! Click to  View.", "Triggered Rule! Click to  View.")%>" onclick="goToLinkStopProp('/Rule/History/<%=triggered.NotificationID %>');">
                        <span class="time" style="font-size: 11px;"><%=triggered.ReadingDate.OVToLocalDateTimeShort() %> - <%=triggered.Reading %></span>
                        <span class="message" style="font-size: 11px;"><b><%=Notification.Load(triggered.NotificationID).Name %></b></span>
                    </a>
                </div>
            </div>
        </li>

        <%} %>

        <% foreach (NotificationTriggered triggered in PendingNotis)
            { %>

        <li class="notiMenuItem ohNoMenu">
            <div class="menuContainer">
                <div class="menu-ico1">
                    <a class="menu-icon-topBar" title="<%: Html.TranslateTag("Shared/TopBarEventList|Click to ReArm", "Click to ReArm")%>" data-id="<%:triggered.NotificationID%>" href="/Notification/ResetPendingConditions?notificationID=<%:triggered.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%>" onclick="ResetCondition(this);return false;">
                        <span class="resetBell rule-trigger-btn ruleInfo-Reset reset-see-rules">
                            <%=Html.GetThemedSVG("reset") %>
                        </span>
                    </a>
                </div>
                <div class="d-flex">
                    <a class="top-menu-link" title="<%: Html.TranslateTag("Shared/TopBarEventList|Event Disarmed, Click to  View.", "Event Disarmed, Click to  View.")%>" onclick="goToLinkStopProp('/Rule/History/<%=triggered.NotificationID %>');">
                        <span class="time" style="font-size: 11px;"><%=Notification.Load(triggered.NotificationID).Name %>  <%: Html.TranslateTag("Shared/TopBarEventList|- Disarmed", "- Disarmed")%></span>
                        <span class="message" style="font-size: 11px;"><%= triggered.AcknowledgedBy > 0 ? Customer.Load(triggered.AcknowledgedBy).FullName : "" %> @ <%=triggered.AcknowledgedTime.OVToLocalDateTimeShort() %></span>
                    </a>
                </div>
            </div>
        </li>
        <%} %>
        <%if (MonnitSession.NewestAnnouncement.AnnouncementObj != null)
            {%>
        <li>
            <div id="AllAnnouncements" class="dropdown-item text-center py-3" style="cursor: pointer;">
                <strong><%: Html.TranslateTag("Shared/TopBarEventList|See All Announcements", "See All Announcements")%></strong>
            </div>
        </li>
        <%} %>
    </ul>
</div>

<style>
    .bell-see-rules > svg {
        width: 19px !important;
        height: 19px !important;
    }

    .reset-see-rules > svg {
        width: 22px !important;
        height: 22px;
    }
</style>

<script>

    var updateCardStyleToAcknowledged1 = (notificationID) => {
        let cardToUpdate = document.querySelector(`#smallCard_${notificationID}`);

        if (cardToUpdate) {
            cardToUpdate.classList.remove("alertBackground");
            cardToUpdate.classList.add("acknowledgedBackground");

            var resetIcon = `<%:Html.GetThemedSVG("reset")%>`;
                var iconToUpdate = document.querySelector(`#iconWrapper_${notificationID}`);

                iconToUpdate.classList.remove("ruleInfo-Acknowledge");
                iconToUpdate.classList.remove("ackAllBtn");
                iconToUpdate.classList.add("ruleInfo-Reset");
                iconToUpdate.classList.add("resetAllPendingBtn");
                iconToUpdate.title = "Condition pending reset. Click to force reset.";
                iconToUpdate.setAttribute("data-href", "/Notification/ResetPendingConditions");
                iconToUpdate.innerHTML = resetIcon
            }

        }

    var noPermAction = "<%: Html.TranslateTag("Shared/TopBarEventList|Unauthorized: User does not have permission to disarm this event\'s future actions", "Unauthorized: User does not have permission to disarm this event\'s future actions")%>";
    var failedDisarm = "<%: Html.TranslateTag("Shared/TopBarEventList|failed to disarm", "failed to disarm")%>";
    var noPermRearm =  "<%: Html.TranslateTag("Shared/TopBarEventList|Unauthorized: User does not have permission to Re-Arm event.", "Unauthorized: User does not have permission to Re-Arm event.")%>";
    var failedRearm =  "<%: Html.TranslateTag("Shared/TopBarEventList|failed to Re-Arm notification", "failed to Re-Arm notification")%>";


    $(document).ready(function () {

        $('#AllAnnouncements').click(function () {
            AnnouncementModal();
        });
    });

    function AckButton(anchor) {
        var href = $(anchor).attr('href');
        $.post(href, function (data) {
            if (data == "Success") {
                $.get('/Notification/TopBarNotiList', function (data) {
                    $('#TopBarListOfTriggeredRules').html(data);
                    const notificationID = $(anchor).attr('data-id');
                    updateCardStyleToAcknowledged1(notificationID);
                    toastBuilder("Success")
                });
            }
            else if (data == "Unauthorized") {
                showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to disarm this events future actions")%>");
            }
            else
                showSimpleMessageModal("<%=Html.TranslateTag("Failed to Re-Arm notification")%>");
        });
    }

    function ResetCondition(anchor) {
        var href = $(anchor).attr('href');
        $.post(href, function (data) {
            if (data == "Success") {
                $.get('/Notification/TopBarNotiList', function (data) {
                    $('#TopBarListOfTriggeredRules').html(data); 
                    const notificationID = $(anchor).attr('data-id');
                    const triggeredCard = $(`#smallCard_${notificationID}`);
                    if (triggeredCard.length > 0) {
                        triggeredCard.hide();
                    }
                    toastBuilder("Success")
                });
            }
            else if (data == "Unauthorized") {
                showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to disarm this events future actions")%>");
            }
            else
                showSimpleMessageModal("<%=Html.TranslateTag("Failed to disarm")%>");
        });
    }

    function goToLinkStopProp(lnk) {
        window.location.href = lnk;
    }

    function AnnouncementModal() {
        var customerid = <%=MonnitSession.CurrentCustomer.CustomerID%>;
        var accountthemeid = <%=MonnitSession.CurrentTheme.AccountThemeID%>;

        $.get("/Settings/LoadAnnouncementForPopup?customerid=" + customerid + "&accountthemeid=" + accountthemeid, function (data) {
            $('#AnnouncementModel').html(data);
            ShowModal();
        });
    }

</script>
