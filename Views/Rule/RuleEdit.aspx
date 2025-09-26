<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Action Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% bool shouldShowMoreOptions = Model.SnoozeDuration > 0; %>

    <div class="container-fluid">
        <%:Html.Partial("Header") %>

        <!-- Event List View -->

        <div class="rule-card_container">
            <div class="">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%=Html.GetThemedSVG("notifications") %>
                        <span class="ms-3" style="margin-left: 10px;">
                            <%: Html.TranslateTag("Events/Actions|Advanced Settings")%>
                        </span>
                    </div>

                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".pageHelp">
                        <div class="help-hover" style="padding: 0.5rem;">
                            <%=Html.GetThemedSVG("circleQuestion") %>
                        </div>
                    </a>

                    <!-- Help Modal -->
                    <div class="modal fade pageHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Overview/SensorHome|Advanced Rule Settings","Advanced Rule Settings")%></h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="word-choice">
                                            <%: Html.TranslateTag("Name","Name")%>
                                        </div>
                                        <div class="word-def">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|A unique name given to a rule to help easily identify in other parts of iMonnit.","A unique name given to a rule to help easily identify in other parts of iMonnit.")%>
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="word-choice">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|Continuously alert until acknowledged","Continuously alert until acknowledged")%>
                                        </div>
                                        <div class="word-def">
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Snooze on","On")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help|The system will continuously send alerts until the alert is acknowledged within iMonnit.","The system will continuously send alerts until the alert is acknowledged within iMonnit.")%>
                                            <br />
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Snooze off","Off")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help|The system will only send one alert each time the rule is triggered.","The system will only send one alert each time the rule is triggered.")%>
                                            <br />
                                            <br />
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="word-choice">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|Continue to alert every","Continue to alert every")%>
                                        </div>
                                        <div class="word-def">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|The amount of time, in minutes, before the same alert is sent again. Alerts will stop after 24 hours if not acknowledged.","The amount of time, in minutes, before the same alert is sent again. Alerts will stop after 24 hours if not acknowledged.")%>
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="word-choice">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|Snooze each trigger","Snooze Each Trigger")%>
                                        </div>
                                        <div class="word-def">
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Jointly","Jointly")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help|No further alerts will be sent if another sensor triggers the same rule.","No further alerts will be sent if another sensor triggers the same rule.")%>
                                            <br />
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Independently","Independently")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help|Additional alerts will be sent if another sensor triggers the rule again.","Additional alerts will be sent if another sensor triggers the rule again.")%>
                                            <br />
                                            <br />
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="word-choice">
                                            <%: Html.TranslateTag("Rule/RuleEdit/Help|Acknowledgement Mode","Acknowledgement Mode")%>
                                        </div>
                                        <div class="word-def">
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Manually","Manually")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help| All alerts will require manual acknowledgment even if the triggering condition has returned to normal."," All alerts will require manual acknowledgment even if the triggering condition has returned to normal.")%>
                                            <br />
                                            <br />
                                            <b><%: Html.TranslateTag("Rule/RuleEdit/Help|Auto","Auto")%></b> - <%: Html.TranslateTag("Rule/RuleEdit/Help|You will no longer receive alerts once the triggering condition returns to normal.","You will no longer receive alerts once the triggering condition returns to normal.")%>
                                            <br />
                                            <br />
                                            <hr />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card_container__body">
                    <div class="card_container__body__content">
                        <form id="settingsForm">
                            <div class="row sensorEditForm ms-0">
                                <label class="col-12 ps-0" for="subject">
                                    <%: Html.TranslateTag("Name", "Name")%>
                                </label>
                                <div class="col sensorEditFormInput ps-0">
                                    <input type="text" id="name" name="name" required="required" value="<%=Model.Name %>" class="form-control user-dets">
                                </div>
                            </div>

                            <%if (MonnitSession.AccountCan("text_notification") || MonnitSession.AccountCan("voice_notification"))
                                { %>

                            <div id="advancedSettings">
                                <div class="col-12">
                                    <div class="form-group" style="margin-top: 1em;">
                                        <div class="col-12">
                                            <label for="SnoozeDuration"><%: Html.TranslateTag("Events/Actions|Continuously alert until acknowledged", "Continuously alert until acknowledged")%></label>
                                            <br />
                                            <div class="form-check form-switch d-flex ps-2">
                                                <input class="form-check-input mx-2" type="checkbox" name="SnoozeToggle" id="SnoozeToggleElement" onclick="toggleMoreOptions()" <%= shouldShowMoreOptions ? "checked" : "" %>>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group toggleOptions" style="margin-top: 1em; display: <%= shouldShowMoreOptions ? "block" : "none" %>">
                                        <div class="col-12 input-container">
                                            <label for="SnoozeDuration"><%: Html.TranslateTag("Events/Actions|Continue to alert every (in Minutes)", "Continue to alert every")%>:</label>
                                            <div class="d-flex" style="max-width: 250px">
                                                <input id="SnoozeDuration" name="SnoozeDuration" class="form-control user-dets" style="border-top-right-radius: 0; border-bottom-right-radius: 0;"
                                                    type="number" min="0" max="720" value="<%:Model.SnoozeDuration %>">
                                                <div class="blue-input">minutes</div>
                                                <%: Html.ValidationMessageFor(model => model.SnoozeDuration)%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" style="margin-top: 1em;">
                                        <div class="col-12">
                                            <label for="_ApplySnoozeByTriggerDevice"><%: Html.TranslateTag("Events/Actions|Snooze each trigger", "Snooze each trigger")%>:</label>
                                            <br />
                                            <div class="form-check form-switch d-flex ps-2">
                                                <label class="form-check-label">Jointly</label>
                                                <input class="form-check-input mx-2" type="checkbox" name="_ApplySnoozeByTriggerDevice" id="ApplySnoozeByTriggerDevice" <%= Model.ApplySnoozeByTriggerDevice ? "checked" : "" %>>
                                                <label class="form-check-label">Independently</label>
                                            </div>
                                            <%: Html.ValidationMessageFor(model => model.ApplySnoozeByTriggerDevice)%>
                                        </div>
                                    </div>
                                    <% if (MonnitSession.AccountCan("notification_extra_settings"))
                                        { %>
                                    <div class="form-group" style="margin-top: 1em;">
                                        <div class="col-12">
                                            <label for="_CanAutoAcknowledge"><%: Html.TranslateTag("Events/Actions|Acknowledgement Mode", "Acknowledgement Mode")%>:</label>
                                            <br />
                                            <div class="form-check form-switch d-flex ps-2">
                                                <label class="form-check-label">Manually</label>
                                                <input class="form-check-input mx-2" type="checkbox" name="_CanAutoAcknowledge" id="CanAutoAcknowledge" <%= Model.CanAutoAcknowledge ? "checked" : "" %>>
                                                <label class="form-check-label">Auto</label>
                                            </div>
                                            <%: Html.ValidationMessageFor(model => model.ApplySnoozeByTriggerDevice)%>
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <%}
                                else
                                { %>
                            <%: Html.Hidden("SnoozeDuration", Model.SnoozeDuration) %>
                            <%: Html.Hidden("_CanAutoAcknowledge", Model.CanAutoAcknowledge) %>
                            <%: Html.Hidden("_ApplySnoozeByTriggerDevice", Model.ApplySnoozeByTriggerDevice) %>
                            <%} %>
                            <div class="w-100 d-flex">
                                <button id="SaveBtn" type="button" class="btn btn-primary" value="<%: Html.TranslateTag("Save", "Save")%>" style="margin-left: auto;">
                                    <%: Html.TranslateTag("Save", "Save")%>
                                </button>
                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>


    </div>
    <!-- End help button -->

    <style>
        .blue-input {
            display: flex;
            align-items: center;
            justify-content: center;
            padding: .25rem;
            background: #0d6efd;
            color: white;
            border-top-right-radius: .25rem;
            border-bottom-right-radius: .25rem;
        }

        /* Removes the number input arrows */
        input[type=number]::-webkit-outer-spin-button,
        input[type=number]::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        input[type=number] {
            -moz-appearance: textfield; /* Firefox */
        }
    </style>

    <%  
        bool sendNoti = false;
        List<NotificationRecipientData> sendNotiList = new List<NotificationRecipientData>();
        List<NotificationRecipientData> nrdList = NotificationRecipientData.SearchPotentialNotificationRecipient(MonnitSession.CurrentCustomer.CustomerID, Model.NotificationID, "");

        foreach (var item in nrdList)
        {
            if (item.SendSensorNotificationToText || item.SendSensorNotificationToVoice)
            {
                sendNotiList.Add(item);
            }
        }
        if (sendNotiList.Count > 0)
            sendNoti = true;
    %>
    <script src="/Scripts/events.js"></script>
    <script>
        var notAllowedString = '<%: Html.TranslateTag("Character not allowed")%>';
        $(function () {

            document.getElementById("name").addEventListener("beforeinput", (event) => {
                if (event.data != null && (event.data.includes('>') || event.data.includes('<'))) {
                    toastBuilder(`${notAllowedString}: < or >`);
                    event.preventDefault();
                }

            });

            $("#SnoozeDuration").change(function (e) {
                var snooze = Number($("#SnoozeDuration").val());
                if (snooze < 0)
                    $("#SnoozeDuration").val(0);
                if (snooze > 720)
                    $("#SnoozeDuration").val(720);
            });

            $('#SaveBtn').on('click touchstart', function () {

                var body = $('#settingsForm').serialize();

                $.post("/Rule/RuleEdit/<%=Model.NotificationID%>", body)
                    .done(function (data) {
                        toastBuilder("Success");
                    })
                    .fail(function (xhr, status, error) {
                        let statusCode = xhr.status;
                        let statusText = xhr.statusText;
                        let errMsgTitle = xhr.responseText.match(/<title>.*<\/title>/g)[0];
                        let errMsgTitleText = $(errMsgTitle)[0].innerHTML;
                        toastBuilder(`${statusCode}: ${statusText}`);
                    });
            });
        });

        let toggleOn = <%= shouldShowMoreOptions ? "true" : "false" %>;

        const toggleMoreOptions = () => {

            toggleOn = (toggleOn == true) ? false : true;

            const optionsToToggleVisibility = $(".toggleOptions");
            const snoozeInput = $("#SnoozeDuration");

            if (!toggleOn) {
                snoozeInput.val(0);
            }

            optionsToToggleVisibility.each(function () {
                $(this).slideToggle("slow");
            });
        }

    </script>

</asp:Content>
