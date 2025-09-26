<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>
<% 
    Dictionary<string, object> dic = new Dictionary<string, object>();
    Dictionary<string, object> ClassShort = new Dictionary<string, object>();
    ClassShort.Add("class", "short");
%>
<style>
    .refreshPic {
        display: none;
        cursor: pointer;
    }

    .ui-tabs-active .refreshPic {
        display: inline;
    }
</style>
<div class="tabContainer">
    <ul>
        <li><a id="tabHistory" href="/Notification/History/<%:Model.NotificationID %>">History
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%if (MonnitSession.CustomerCan("Notification_Edit"))
            { %>
        <li><a id="tabEdit" href="/Notification/Edit/<%:Model.NotificationID %>">Settings
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <li><a id="A1" href="/Notification/Calendar/<%:Model.NotificationID %>">Schedule
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <li>
            <%if (Model.AdvancedNotificationID != long.MinValue && AdvancedNotification.Load(Model.AdvancedNotificationID).AdvancedNotificationType == eAdvancedNotificationType.AutomatedSchedule)
                {%>
        <li>
            <a id="tabSchedule" href="/Notification/AutomatedSchedule/<%:Model.NotificationID %>">Frequency 
                 <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
            </a>
        </li>
        <%}%>
        
        <li>
            <a id="tabFrom" href="/Notification/SentFrom/<%:Model.NotificationID %>">Sent From
                <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
            </a>
        </li>
      
        <li><a id="tabRecipient" href="/Notification/Recipient/<%:Model.NotificationID %>">People to Notify
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%if (Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(s => { return s.ApplicationID == 12 || s.ApplicationID == 13 || s.ApplicationID == 76 || s.ApplicationID == 97; }).Count() > 0)
            { %>
        <li><a id="tabControl" href="/Notification/RecipientDevices/<%:Model.NotificationID %>">Devices
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <% } %>
        <%if (MonnitSession.AccountCan("use_system_actions"))
            { %>
        <li><a id="tabSystem" href="/Notification/SystemAction/<%:Model.NotificationID %>">System Actions
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <% } %>
        <% } %>
    </ul>
</div>

<script type="text/javascript">
    $(function () {
        var index = $('#tab<%:Request["tab"]%>').parent().index();
        if (index < 0)
            index = 0;
        $(".tabContainer").tabs({ active: index });

        $('.refreshPic').click(function () {

            var tabContainter = $('.tabContainer').tabs();
            //var selected = tabContainter.tabs('option', 'selected');
            var active = tabContainter.tabs('option', 'active');
            tabContainter.tabs('load', active);
        });
    });


</script>
