<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    bool isGateway = false;
    long deviceID = -1;
    string step2Url = "";
    string step3Url = "";
    bool hideConditionHud = Request.Path.StartsWith("/Rule/ChooseSensorTemplate") || Request.Path.StartsWith("/Rule/SendAlert");
    bool hideAllButTheProgressBar = Request.Path.StartsWith("/Rule/SendAlert");
    bool makeUnclickable = Request.Path.StartsWith("/Rule/RuleComplete");
    bool isOnTasks = false;

    if (MonnitSession.NotificationInProgress.SensorID > 0)
    {
        deviceID = MonnitSession.NotificationInProgress.SensorID;
        step2Url = "/Rule/CreateApplicationRule/?ruleClass=" + MonnitSession.NotificationInProgress.NotificationClass;
        step3Url = "/Rule/ChooseSensorTemplate/" + (deviceID > 0 ? deviceID.ToString() : "") + "?isGateway=false";
    }
    else if (MonnitSession.NotificationInProgress.GatewayID > 0)
    {
        isGateway = true;
        deviceID = MonnitSession.NotificationInProgress.GatewayID;
        step2Url = "/Rule/CreateApplicationRule/?ruleClass=" + MonnitSession.NotificationInProgress.NotificationClass;
        step3Url = "/Rule/ChooseSensorTemplate/" + (deviceID > 0 ? deviceID.ToString() : "") + "?isGateway=true";
    }
    else if (MonnitSession.NotificationInProgress.AdvancedNotificationID > 0)
    {
        step2Url = "/Rule/CreateApplicationRule/?ruleClass=" + MonnitSession.NotificationInProgress.NotificationClass + "&advancedNotificationID=" + MonnitSession.NotificationInProgress.AdvancedNotificationID;
        step3Url = "/Rule/CreateAdvancedRule/";
    }

    if (Request.Path.StartsWith("/Rule/ChooseTask") || Request.Path.StartsWith("/Rule/Send") || Request.Path.StartsWith("/Rule/Command") || Request.Path.StartsWith("/Rule/CreateSystemAction") || Request.Path.StartsWith("/Rule/ConfigureResetAccumulator") || Request.Path.StartsWith("/Rule/SendAlert"))
        isOnTasks = true;

%>

<style>
    .inactiveProgressLink {
        pointer-events: none;
        cursor: default;
    }

    #ruleProgressBar li {
        cursor: pointer;
    }


    @media screen and (max-width: 500px) {

        .progressLabels {
            display: none;
        }

        li.is-active .progressLabels {
            display: block;
        }
    }
</style>

<div id="ruleProgressBar">
    <ul class="list-unstyled multi-steps rounded shadow-sm mt-4">


        <li class="<%:Request.Path.StartsWith("/Rule/ChooseType") ? "is-active" : " " %>">
            <a href="/Rule/ChooseType/">
                <span class="progressLabels"><%: Html.TranslateTag("Create a Rule","Create a Rule") %></span>
            </a>
        </li>


        <li class="<%:Request.Path.StartsWith("/Rule/CreateApplicationRule") ? "is-active" : " " %> <%= makeUnclickable ? "inactiveProgressLink" : "" %>">
            <a href="<%:step2Url %>">
                <span class="progressLabels"><%= Html.TranslateTag("Pick a Device") %></span>
            </a>
        </li>
        <li class="<%:Request.Path.StartsWith("/Rule/ChooseSensorTemplate") ? "is-active" : Request.Path.StartsWith("/Rule/CreateScheduledRule") ? "is-active" : Request.Path.StartsWith("/Rule/CreateAdvancedRule") ? "is-active" : " " %> <%= makeUnclickable ? "inactiveProgressLink" : "" %>">
            <a href="<%:step3Url %>">
                <span class="progressLabels"><%= Html.TranslateTag("Choose a Condition") %></span>
            </a>
        </li>
        <li class="<%:isOnTasks ? "is-active" : " " %> <%= makeUnclickable ? "inactiveProgressLink" : "" %>">
            <a href="/Rule/ChooseTask/">
                <span class="progressLabels"><%= Html.TranslateTag("Set Up Tasks")%></span>
            </a>
        </li>
        <li class="<%:Request.Path.StartsWith("/Rule/NameRule") ? "is-active" : " " %> <%= makeUnclickable ? "inactiveProgressLink" : "" %>">
            <a href="/Rule/NameRule/">
                <span class="progressLabels"><%= Html.TranslateTag("Name the Rule")%></span>
            </a>
        </li>
        <li class="inactiveProgressLink <%:Request.Path.StartsWith("/Rule/RuleComplete") ? "is-active" : " " %>">
            <a href="#">
                <span class="progressLabels"><%= Html.TranslateTag("Rule Complete")%></span>
            </a>
        </li>
    </ul>
</div>

<%
    if (!hideAllButTheProgressBar)
    {
%>


<%
    if (deviceID > 0 && !isGateway)
    {
        Sensor sensor = Sensor.Load(deviceID);
%>
<div class="cool ">
    <div class="sensor-tag">
        <div class="tag-title"><span><%= Html.TranslateTag("Sensor") %></span></div>
        <div class="card-tag__container">
            <div class="hidden-xs rule-tag__icon">
                <div class="icon-color iconMap" style="width: 30px; margin-left: 5px; margin-right: 10px;">
                    <%=Html.GetThemedSVG("app" + sensor.ApplicationID) %>
                </div>

            </div>

            <div class="triggerDevice__name ">
                <strong><%:System.Web.HttpUtility.HtmlDecode(sensor.SensorName) %></strong>
            </div>
        </div>
    </div>
    <%}
        if (deviceID > 0 && isGateway)
        {
            Gateway gateway = Gateway.Load(deviceID);
    %>

    <div class="cool ">
        <div class="sensor-tag">
            <div class="tag-title"><span><%= Html.TranslateTag("Gateway") %></span></div>
            <div class="card-tag__container ">
                <div class="hidden-xs rule-tag__icon">

                    <span class="sensor icon " style="width: 30px; height: 30px; display: flex;"><%=Html.GetThemedSVGForGateway(gateway.GatewayTypeID) %></span>

                </div>

                <div class="triggerDevice__name">
                    <strong><%:gateway.Name %></strong>
                </div>
            </div>
        </div>


        <%}%>


        <%
            if (!hideConditionHud)
            { %>

        <% if (MonnitSession.NotificationInProgress.NotificationByTimeID > 0)
            {
                string scheduledTime = new DateTime(2022, 1, 1, MonnitSession.NotificationInProgress.NotificationByTime.ScheduledHour, MonnitSession.NotificationInProgress.NotificationByTime.ScheduledMinute, 0).ToTimeFormatted(MonnitSession.CurrentCustomer.Preferences["Time Format"]);
        %>


        <%if (deviceID <= 0)
            { %>
        <div class="cool ">
            <%} %>
            <div class="cool2">
                <div class="sensor-tag">
                    <div class="hidden-xs ruleDevice__icon">
                    </div>
                    <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>
                    <div class="condition_name">
                        <strong style="margin-top: 10px;"><%: Html.TranslateTag("Trigger at ")%>:
            <br />
                        </strong>
                        <span class="reading-tag-condition"><%=scheduledTime %>
                        </span>
                    </div>
                </div>
            </div>
            <%} %>

            <%
                    if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Application && !String.IsNullOrEmpty(MonnitSession.NotificationInProgress.CompareValue))
                    {
                        Html.RenderPartial("~/Views/Sensor/DataTypeSpecific/Default/_DatumEventDisplay.ascx", MonnitSession.NotificationInProgress);

                    }%>

            <%if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Low_Battery && !String.IsNullOrEmpty(MonnitSession.NotificationInProgress.CompareValue))
                { %>


            <div class="reading-tag1 111">
                <div class="hidden-xs ruleDevice__icon">
                </div>
                <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>
                <div class="condition_name">
                    <strong style="margin-top: 10px;"><%: Html.TranslateTag("When battery is")%>:
            <br />
                    </strong>
                    <span class="reading-tag-condition"><%= Html.TranslateTag("Below") %> <%=MonnitSession.NotificationInProgress.CompareValue %> %
                    </span>
                </div>
            </div>

            <%}%>

            <%if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Inactivity && !String.IsNullOrEmpty(MonnitSession.NotificationInProgress.CompareValue))
                { %>

            <div class="reading-tag1">

                <div class="hidden-xs ruleDevice__icon">
                </div>
                <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

                <div class="condition_name">
                    <strong style="margin-top: 10px;"><%: Html.TranslateTag("When device is Inactive for")%>:
            <br />
                    </strong>
                    <span class="reading-tag-condition"><%= Html.TranslateTag("Greater Than") %> <%=MonnitSession.NotificationInProgress.CompareValue %> <%= Html.TranslateTag("Minutes") %>
                    </span>
                </div>
            </div>

            <%}%>

            <%if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Advanced && !String.IsNullOrEmpty(MonnitSession.NotificationInProgress.CompareValue))
                { %>

            <div class="reading-tag1 ">

                <div class="hidden-xs ruleDevice__icon">
                </div>
                <div class="tag-title"><span><%= Html.TranslateTag("Condition") %></span> </div>

                <div class="condition_name">
                    <strong style="margin-top: 10px;"><%: Html.TranslateTag("When reading meets")%>:
            <br />
                    </strong>
                    <span class="reading-tag-condition"><%= Html.TranslateTag("Advanced Parameters") %>
                    </span>
                </div>
            </div>

            <%}%>

            <%}%>

            <%}%>
            <script>

                $(function () {
                    let className = "";
                    var steps = $("ul.multi-steps li");
                    for (var i = 0; i < steps.length; i++) {
                        $(steps[i]).addClass(className);
                        if ($(steps[i]).hasClass("is-active")) {
                            className = "inactiveProgressLink";
                        }

                    }

                    $('#ruleProgressBar li').click(function (e) {
                        e.preventDefault();

                        var childElement = $(this).children()[0];
                        childElement.click();
                    });
                    $('#ruleProgressBar li a').click(function (e) {
                        e.preventDefault();
                        e.stopPropagation();

                        window.location.href = this.href;
                    });
                });

            </script>
