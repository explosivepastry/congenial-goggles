<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChooseTaskToEdit
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

        foreach (NotificationRecipient recipient in Model.NotificationRecipients)
        {
            if (recipient.NotificationType == eNotificationType.Email)
            {
                emailEdited = true;
                generalAlertEdited = true;
            }

            if (recipient.NotificationType == eNotificationType.SMS)
            {
                textEdited = true;
                generalAlertEdited = true;
            }


            if (recipient.NotificationType == eNotificationType.Push_Message)
            {
                pushEdited = true;
                generalAlertEdited = true;

            }

            if (recipient.NotificationType == eNotificationType.Phone)
            {
                voiceEdited = true;
                generalAlertEdited = true;
            }

            if (recipient.NotificationType == eNotificationType.Control)
                controlEdited = true;
            if (recipient.NotificationType == eNotificationType.Local_Notifier)
                alertEdited = true;
            if (recipient.NotificationType == eNotificationType.Thermostat)
                thermoEdited = true;
            if (recipient.NotificationType == eNotificationType.SystemAction || recipient.NotificationType == eNotificationType.HTTP)
                systemEdited = true;
            if (recipient.NotificationType == eNotificationType.Group && !String.IsNullOrEmpty(Model.NotificationText))
            {
                generalAlertEdited = true;
                emailEdited = true;
            }

            if (recipient.NotificationType == eNotificationType.ResetAccumulator)
                resetEdited = true;
        }
    %>

    <%="" %>
    <%:Html.Partial("~/Views/Rule/Header.ascx") %>
    <div class=" choose-task2">
        <div class="rule_container ">
            <div class="card_container__top">
                <div class="card_container__top__title">

                    <%: Html.TranslateTag("Edit Tasks")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button  choosetasktoedit-->
                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Task Help", "Task Help") %>" data-bs-target=".taskHelp">
                        <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
                    </a>
                </div>
            </div>
            <%--   <h3 class="rule-head"><%= Html.TranslateTag("When Condition is Met") %> <span class="fu"></span></h3>--%>

            <div class="rule-sets">
                <a href="/Rule/EditSendAlert/<%=Model.NotificationID %>" class=" <%=generalAlertEdited  %> btn-lg  newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("app13") %></span>
                    <div><%string generalAlertString = generalAlertEdited ? "Edit Alert" : "Send Alert"; %></div>
                    <%: Html.TranslateTag(generalAlertString, generalAlertString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%
                if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
                {
                    string voiceString = voiceEdited ? "Edit Voice Call" : "Send Voice Call";%>

            <%}
                if (hasControlDevice)
                {
                    string controlString = controlEdited ? "Edit Control Unit" : "Command Control Unit";%>
            <div class="rule-sets">
                <a href="/Rule/EditCommandControlUnit/<%=Model.NotificationID %>" class=" <%=controlEdited  %> btn-lg newActionBtn">
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
                <a href="/Rule/EditCommandLocalAlert/<%=Model.NotificationID %>" class=" <%=alertEdited  %> btn-lg  newActionBtn">
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
                <a href="/Rule/EditCommandThermostat/<%=Model.NotificationID %>" class=" <%=thermoEdited  %> btn-lg  newActionBtn">
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
                <a href="/Rule/EditResetAccumulator/<%:Model.NotificationID %>" class=" <%=resetEdited%> btn-lg newActionBtn">
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
                <a href="/Rule/EditSystemAction/<%=Model.NotificationID %>" class=" <%=systemEdited%> btn-lg newActionBtn">
                    <span class="rule-svg"><%=Html.GetThemedSVG("actions") %></span>
                    <%: Html.TranslateTag(systemString, systemString)%>
                </a>
                <div class="fu"></div>
            </div>

            <%}%>
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
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Send Alert")%>
                            </div>
                            <div class="word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Automatically send an email, text, and/or an automated voice call to select recipients when a predefined condition is met.")%>
                            </div>
                        </div>
                        <hr />
                        <%
                            if (hasControlDevice)
                            { %>

                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Command Control Unit")%>
                            </div>
                            <div class="col word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Configuring this will, when the condition is met, send a command to one or more of your control units.")%>
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
                            <div class="col word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Configuring this will, when the condition is met, send a command and a message to one or more of your local alert devices.")%>
                            </div>
                        </div>
                        <hr />


                        <%}

                            if (hasResetAccDevice)
                            {%>
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Reset Accumulator")%>
                            </div>
                            <div class="col word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Configuring this will, when the condition is met, send a command to one or more of your sensors.")%>
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
                            <div class="col word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Configuring this will, when the condition is met, send a command to one or more of your thermostats.")%>
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
                            <div class="col word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Configuring this will, when the condition is met, trigger a system action")%>
                            </div>
                        </div>

                        <%}%>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
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
