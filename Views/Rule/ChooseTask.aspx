<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChooseTask
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        bool hasControlDevice = false;
        bool hasLocalAlertDevice = false;
        bool hasThermostsatDevice = false;
        bool hasResetAccDevice = false;
        NotificationRecipientDevice sensorList = ViewBag.RecipientDevices;
        if (sensorList != null)
        {
            hasControlDevice = (sensorList.ControlUnitList.Count() > 0);
            hasLocalAlertDevice = (sensorList.LocalAlertList.Count() > 0);
            hasThermostsatDevice = (sensorList.ThermostatList.Count() > 0);
            hasResetAccDevice = (sensorList.ResetAccList.Count() > 0);
        }

        bool emailEdited = false, textEdited = false, pushEdited = false, voiceEdited = false, generalAlertEdited = false;
        bool controlEdited = false, alertEdited = false, thermoEdited = false, systemEdited = false, resetEdited = false;

        foreach (NotificationRecipient recipient in MonnitSession.NotificationRecipientsInProgress)
        {
            if (recipient.NotificationType == eNotificationType.Email) 
            { 
                { emailEdited = true; generalAlertEdited = true; }
            } 

            if (recipient.NotificationType == eNotificationType.SMS) { textEdited = true; generalAlertEdited = true; }

            if (recipient.NotificationType == eNotificationType.Push_Message)
                generalAlertEdited = true;
            if (recipient.NotificationType == eNotificationType.Phone) { voiceEdited = true; generalAlertEdited = true; }

            if (recipient.NotificationType == eNotificationType.Control)
                controlEdited = true;
            if (recipient.NotificationType == eNotificationType.Local_Notifier)
                alertEdited = true;
            if (recipient.NotificationType == eNotificationType.Thermostat)
                thermoEdited = true;
            if (recipient.NotificationType == eNotificationType.SystemAction || recipient.NotificationType == eNotificationType.HTTP)
                systemEdited = true;
            if (recipient.NotificationType == eNotificationType.Group) {generalAlertEdited = true; emailEdited = true;}

            if (recipient.NotificationType == eNotificationType.ResetAccumulator)
                resetEdited = true;
        }
    %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>
    <div class=" choose-task2">
        <div class="rule_container ">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Set Up Tasks")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button  choosetask-->
                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Task Help", "Task Help") %>" data-bs-target=".taskHelp">
                        <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
                    </a>
                </div>
            </div>
            <h3 class="rule-title" style="margin-left: 5px;"><%= Html.TranslateTag("When Condition is Met") %> <span class="fu"></span></h3>

            <div class="rule-sets">
                <a href="/Rule/SendAlert/" class=" <%=generalAlertEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("app13") %></span>
                    <div><%string generalAlertString = generalAlertEdited ? "Edit Alert" : "Send Alert"; %></div>
                    <%: Html.TranslateTag(generalAlertString, generalAlertString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%--            <div class="rule-sets">
                <a href="/Rule/SendEmailNotification/" class=" <%=emailEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("envelope") %></span>
                    <div><%string emailString = emailEdited ? "Edit E-mail" : "Send E-Mail"; %></div>
                    <%: Html.TranslateTag(emailString, emailString)%>
                </a>
                <div class="fu"></div>
            </div>
            <%
                string textString = textEdited ? "Edit Text" : "Send Text";
            %>
            <div class="rule-sets ">
                <a href="/Rule/SendTextNotification/" class=" <%=textEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("messages") %></span>
                    <%: Html.TranslateTag(textString, textString)%>      
                </a>
                <div class="fu"></div>
            </div>--%>

<%--            <%if (MonnitSession.CustomerCan("See_Beta_Preview"))
                {%>
            <div class="rule-sets ">
                <a href="/Rule/SendPushNotification/" class=" <%=pushEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("ringingBell") %></span>
                    <div><%string pushString = pushEdited ? "Edit Push Notification" : "Push Notification"; %></div>
                    <%: Html.TranslateTag(pushString, pushString)%>      
                </a>
                <div class="fu"></div>
            </div>
            <%}%>--%>

            <%
                if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
                {
                    string voiceString = voiceEdited ? "Edit Voice Call" : "Send Voice Call";%>
            <%--            <div class="rule-sets">
                <a href="/Rule/SendVoiceNotification/" class="<%=voiceEdited %> btn-lg newActionBtn">
                    <span class="rule-svg testing-img"><%=Html.GetThemedSVG("phone") %></span>
                    <%: Html.TranslateTag(voiceString, voiceString)%>
                       
                </a>
                <div class="fu"></div>
            </div>--%>

            <%}
                if (hasControlDevice)
                {
                    string controlString = controlEdited ? "Edit Control Unit" : "Command Control Unit";%>

            <div class="rule-sets">
                <a href="/Rule/CommandControlUnit/" class=" <%=controlEdited  %> btn-lg newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("app76") %></span>
                    <%: Html.TranslateTag(controlString, controlString)%>
                </a>
                <div class="fu"></div>
            </div>


            <%}
                if (hasLocalAlertDevice)
                {
                    string alertString = alertEdited ? "Edit Local Alert" : "Command Local Alert";%>

            <div class="rule-sets">
                <a href="/Rule/CommandLocalAlert/" class=" <%=alertEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("app13") %></span>
                    <%: Html.TranslateTag(alertString, alertString)%>
                </a>
                <div class="fu"></div>
            </div>


            <%}
                if (hasThermostsatDevice)
                {
                    string thermoString = thermoEdited ? "Edit Thermostat" : "Control Thermostat";%>

            <div class="rule-sets">
                <a href="/Rule/CommandThermostat/" class=" <%=thermoEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("app97") %></span>
                    <%: Html.TranslateTag(thermoString, thermoString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%}
                if (hasResetAccDevice)
                {
                    string resetString = resetEdited ? "Edit Reset Accumulator" : "Configure Reset Accumulator";%>
            <div class="rule-sets">
                <a href="/Rule/ConfigureResetAccumulator/" class=" <%=resetEdited%> btn-lg newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("resetAcc") %></span>
                    <%: Html.TranslateTag(resetString, resetString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%}
                if (MonnitSession.AccountCan("use_system_actions"))
                {
                    string systemString = systemEdited ? "Edit System Action" : "Create System Action";%>
            <div class="rule-sets">
                <a href="/Rule/CreateSystemAction/" class=" <%=systemEdited%> btn-lg newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("actions") %></span>
                    <%: Html.TranslateTag(systemString, systemString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%}%>
            <div class="rule-sets btn-next" style="margin-top: 20px;">
                <a href="/Rule/NameRule/" class=" btn btn-primary">
                    <%: Html.TranslateTag(" Done adding tasks", "Done adding tasks")%>
                </a>
                <div class="fu"></div>
            </div>
        </div>
    </div>

    <div class="modal fade taskHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/ChooseType|Tasks")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <%--                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send E-Mail")%>
                            </div>
                            <div class="word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send an email to select recipients when a predefined condition is met.")%>
                            </div>
                        </div>--%>

                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send Alert")%>
                            </div>
                            <div class="word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send an email, text, and/or an automated voice call to select recipients when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />

                        <%if (MonnitSession.AccountCan("text_notification"))
                            {%>
<%--                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send Text")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send an SMS text to select recipients when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />--%>
                        <%}
                            if (MonnitSession.AccountCan("voice_notification"))
                            {%>
<%--                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send Voice Message")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Select recipients receive an automated voice call when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />--%>
                        <%}
                            if (MonnitSession.CustomerCan("See_Beta_Preview"))
                            {%>
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send Push Message")%>
                            </div>
                            <div class=" word-def">
                                Automatically send an Push Notification to select recipients when a predefined condition is met.
                            </div>
                        </div>
                        <hr />
                        <%}

                            if (hasControlDevice)
                            { %>

                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Command Control Unit")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send a command to one or more Control Units when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />

                        <%}
                            if (hasLocalAlertDevice)
                            {%>
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Command Local Alert")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send a command to one or more Local Alert devices when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />
                        <%}

                            if (hasResetAccDevice)
                            { %>

                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Reset Accumulator")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically sends a Reset Accumulator command to one or more Sensors when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />

                        <%}

                            if (hasThermostsatDevice)
                            {%>
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Control Thermostat")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send a command to one or more Thermostats when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />
                        <%}
                            if (MonnitSession.AccountCan("use_system_actions"))
                            {%>
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Create System Action")%>
                            </div>
                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically trigger a System Action when a predefined condition is met.")%>
                            </div>
                        </div>
                        <%}%>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <style>
        svg {
            height: 25px;
        }

        .True {
            background-color: #b4b8bf;
            color: white;
        }

            .True .rule-svg svg {
                fill: white !important;
            }

            .True ~ .fu {
                content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 512 512'%3E%3C!--! Font Awesome Pro 6.2.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --%3E%3Cpath fill='green' d='M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z'/%3E%3C/svg%3E");
                position: relative;
                width: 20px;
                height: 20px;
                margin: 0;
                padding: 0;
                right: 14px;
                background: white;
                border-radius: 12px;
            }
    </style>

</asp:Content>
