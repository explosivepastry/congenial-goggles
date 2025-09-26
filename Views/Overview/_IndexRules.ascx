<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<!-- ==========================
            - Rules Triggered
============================-->

<%--<div class="rulesTriggeredGrid side_card" style="overflow-y: hidden">--%>
<div class=" d-flex w-100 ruleTabs" style="align-items: center; gap: 10px; margin-top: 10px;">
    <div class="top-icon"><%:Html.GetThemedSVG("circle-rules") %></div>
    <span style="width: 80%; font-weight: bold; font-size: .9rem;">Rules Triggered</span>
    <div style="display: flex; flex-direction: column; width: 100%;">
        <div class="linex">
            <div class="pinkDot"></div>
            <span style="font-size: 10px;">Not Acknowledged</span>
        </div>
        <div class="linex">
            <div class="yellowDot"></div>
            <span style="font-size: 10px;">Acknowledged</span>
        </div>
    </div>
</div>

<div id="RuleList" class="rules-t-flow containRule" style="overflow-y: scroll;">

    <%if (MonnitSession.OverviewHomeModel.Notifications.Count < 1)
        {%>
    <a href="/Rule/ChooseType">
        <div class="d-flex linex">
            <div class="redDot"></div>
            <div class="icon-update"><%:Html.GetThemedSVG("rules") %></div>
            Add a<span style="color: var(--primary-color); font-weight: bold;">Rule</span>for this account.
        </div>
    </a>
    <%}
        else
        {
            if (!TempData.ContainsKey("ShowEditLink"))
                TempData.Add("ShowEditLink", MonnitSession.CustomerCan("Notification_Edit"));
            foreach (Notification item in MonnitSession.OverviewHomeModel.Notifications)
            {
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
                string respondClass = "";
                string ruleHref = "";
                string ruleHrefTwo = "";
                string ruleTitle = "";
                string ruleStatus = "Ready";
                MvcHtmlString alertingIcon = new MvcHtmlString("");

                NotificationTriggered notificationTriggered = MonnitSession.OverviewHomeModel.NotificationsTriggered.Where(nt => nt.NotificationID == item.NotificationID).FirstOrDefault();
                if (!item.IsActive)
                {
                    ruleStatus = "Disabled";
                }
                else if (notificationTriggered != null)
                {
                    if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                    {
                        backgroundOverwrite = "alertBackground";
                        alertingIcon = Html.GetThemedSVG("notifications");
                        classOverwrite = "Acknowledge";
                        respondClass = "ackAllBtn";
                        ruleHref = "/Notification/AcknowledgeActiveNotifications";
                        ruleHrefTwo = "Browser_MainList";
                        ruleTitle = "Click to disarm alarming rule ";
                        ruleStatus = "Alerting";
                    }
                    else if (notificationTriggered.resetTime == DateTime.MinValue)
                    {
                        backgroundOverwrite = "acknowledgedBackground";
                        alertingIcon = Html.GetThemedSVG("reset");
                        classOverwrite = "Reset";
                        respondClass = "resetAllPendingBtn";
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

    <%--   Rule Cards--%>
    <div class="">
        <div class="d-flex w-100" style="flex-direction: column; align-items: center;">
            <div id="smallCard_<%:item.NotificationID%>" class="small-list-card <%: backgroundOverwrite%>" style="height: 53px; justify-content: start; min-width: 337px;" onclick="window.location.href='/Rule/History/<%:item.NotificationID%>'">
                <div class="corp-status change-rule-status" style="height: 100%;">
                    <a id="actionDiv_<%:item.NotificationID%>" class="corp-status change-rule-status" data-id="<%:item.NotificationID%>">
                        <div class="sensor corp-status sensorIcon <%:item.IsActive ? "sensorStatusOK " : "sensorStatusInactive"%> " title="<%:item.IsActive ? "sensorStatusOK " : "sensorStatusInactive"%>"></div>
                    </a>
                </div>
                <div class="home-inside-data">
                    <div class="home-icon-cardRule"><%=svgTemp %></div>
                    <div data-shorty="<%:item.Name %>" class="home-name-card" style="width: 100px; margin-left: 5px;"><%:item.Name %></div>
                    <%--<a href="/Rule/History/<%:item.NotificationID%>" />--%>
                    <div class="home-date-card"><span>Last Sent:</span> <span><%:item.LastSent.OVToLocalDateTimeShort() %></span></div>
                    <div class="home-end-card">
                        <%if (TempData["ShowEditLink"] != null && (bool)TempData["ShowEditLink"] == true)
                            { %>
                        <div style="margin-left: auto; display: flex; align-items: center;">
                            <div>
                                <div id="iconWrapper_<%:item.NotificationID%>" data-href="<%:ruleHref%>" class="rule-trigger-btn ruleInfo-<%:classOverwrite%> <%=respondClass %>" title="<%:ruleTitle %>" style="margin-right: 5px" value="<%:item.IsActive %>" onclick="handleRuleAcknowledge(event,'<%:item.NotificationID%>','<%:ruleHrefTwo%>')">
                                    <%=alertingIcon %>
                                </div>
                            </div>
                        </div>
                        <%}%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% }
        } %>
</div>

<style>
    .alertBackground {
        background-color: #ffcaca;
    }

    .acknowledgedBackground {
        background-color: #ffedd4;
    }
</style>
<script>

    const updateCardStyleToAcknowledged = (notificationID) => {
        let cardToUpdate = document.querySelector(`#smallCard_${notificationID}`);

        if (cardToUpdate) {
            cardToUpdate.classList.remove("alertBackground");
            cardToUpdate.classList.add("acknowledgedBackground");

            const resetIcon = `<%:Html.GetThemedSVG("reset")%>`;
        const iconToUpdate = document.querySelector(`#iconWrapper_${notificationID}`);

        iconToUpdate.classList.remove("ruleInfo-Acknowledge");
        iconToUpdate.classList.remove("ackAllBtn");
        iconToUpdate.classList.add("ruleInfo-Reset");
        iconToUpdate.classList.add("resetAllPendingBtn");
        iconToUpdate.title = "Condition pending reset. Click to force reset.";
        iconToUpdate.setAttribute("data-href", "/Notification/ResetPendingConditions");
        iconToUpdate.innerHTML = resetIcon
    }

}

    function handleRuleAcknowledge(e, notificationID, ruleHrefTwo) {

        const iconToUpdate = document.querySelector(`#iconWrapper_${notificationID}`);

        e.stopPropagation();
        e.preventDefault();

        if (iconToUpdate.getAttribute("data-href") == "/Notification/AcknowledgeActiveNotifications") {

            var postData = {
                notificationID: notificationID,
                userID: <%: MonnitSession.CurrentCustomer.CustomerID %>,
                AckMethod: ruleHrefTwo
            };

            $.post("/Notification/AcknowledgeActiveNotificationsOnlyReturnString/", postData)
                .done(function (data) {
                    if (data !== "Success") {
                        toastBuilder(data);
                    } else {
                        toastBuilder("Success");

                        $.get('/Notification/TopBarNotiList')
                            .done(function (data) {
                                const list = document.querySelector("#TopBarListOfTriggeredRules");
                                list.innerHTML = data;
                            })
                            .fail(function (jqXHR, textStatus, errorThrown) {
                                console.error("Failed to load notifications:", textStatus, errorThrown);
                            });

                        updateCardStyleToAcknowledged(notificationID);
                    }
                })
                .fail(function (xhr, status, error) {
                    console.log("Error occurred:", error);
                    toastBuilder(error);
                });

        } else {
            var postData = {
                notificationID: notificationID,
                userID: <%: MonnitSession.CurrentCustomer.CustomerID %>,
            };

            $.post("/Notification/ResetPendingConditions/", postData)
                .done(function (data) {
                    if (data !== "Success") {
                        toastBuilder(data);
                    } else {
                        toastBuilder("Success");

                        $.get('/Notification/TopBarNotiList')
                            .done(function (data) {
                                const list = document.querySelector("#TopBarListOfTriggeredRules");
                                list.innerHTML = data;
                            })
                            .fail(function (jqXHR, textStatus, errorThrown) {
                                console.error("Failed to load notifications:", textStatus, errorThrown);
                            });

                        let cardToUpdate = document.querySelector(`#smallCard_${notificationID}`);
                        cardToUpdate.style.display = "none";
                    }
                })
                .fail(function (xhr, status, error) {
                    console.log("Error occurred:", error);
                    toastBuilder(error);
                });
        }
    }

    // Resize the rule list to grown and shrink with the outer card.
    const ruleListCard = document.querySelector("#HomePageRuleCard");
    const ruleList = document.querySelector("#RuleList");

    const handleResize = () => {
        let heightToCopy = ruleListCard.offsetHeight - 5;
        if (heightToCopy > 600) {
            heightToCopy = 600;
        }
        ruleList.style.maxHeight = heightToCopy + 'px';
    }

    handleResize();
    window.addEventListener('resize', handleResize)
</script>
<%--</div>--%>
<!-- Rules Triggered END -->
