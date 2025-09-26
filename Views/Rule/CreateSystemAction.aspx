<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create System Action
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <% 
        List<ExternalDataSubscription> externalDataSubscriptions = ExternalDataSubscription.LoadAllByAccountID(MonnitSession.NotificationInProgress.AccountID);
        ExternalDataSubscription NotificationWebhook = externalDataSubscriptions.Where(eds => eds.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification).FirstOrDefault();
    %>

    <!-- Event List View -->


    <div class="csa_container">
        <div class="system-action_container">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Rule/CreateSystemAction|Create System Action")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button  createsystemaction-->
                    <a class="helpIco help-hover" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|System Action Help") %>" data-bs-target=".systemActionHelp">
                        <%=Html.GetThemedSVG("circleQuestion") %>
                    </a>
                </div>
            </div>
            <div class="rule-card-action" style="padding: 10px;">
                <form id="ActionForm" style="max-width: 315px; margin-top: 10px;">

                    <div class="  section system-action-select">
                        <div class="col-12  sub-text">
                            <label for="subject"><%: Html.TranslateTag("Rule/CreateSystemAction|Select Type of Action")%></label>
                        </div>
                        <div class="col-md-4 col-12">
                            <select name="action" id="Action" class="form-select user-dets select-action" style="width: 250px;">
                                <%foreach (ActionToExecute action in ActionToExecute.LoadAll())
                                    {%>
                                <option value="<%=action.ActionToExecuteID %>"><%=action.Description %></option>
                                <%}
                                    if (NotificationWebhook != null)
                                    {%>
                                <option value="-1"><%: Html.TranslateTag("Rule Webhook")%></option>
                                <%} %>
                                <%
                                    if (ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.Account.AccountID).Any(x => x.ApplicationID == 90))
                                    {
                                %>
                                <option value="-2"><%: Html.TranslateTag("Reset Accumulator")%></option>
                                <%
                                    }
                                %>
                            </select>
                            <%if (NotificationWebhook != null)
                                {%>
                            <input type="hidden" name="NotificationWebhookID" value="<%=NotificationWebhook.ExternalDataSubscriptionID %>" />
                            <%} %>
                        </div>

                    </div>

                    <div class=" section system-action-select">
                        <div class="col-12  sub-text">
                            <label for="DelayMinutes"><%: Html.TranslateTag("Rule/CreateSystemAction|Time Delay")%></label>
                        </div>

                        <div class="col-md-4 col-12">
                            <select name="DelayMinutes" class="form-select user-dets select-action" style="width: 250px;">
                                <option value="0"><%: Html.TranslateTag("Rule/CreateSystemAction|No Delay","No Delay")%></option>
                                <option value="2"><%: Html.TranslateTag("Rule/CreateSystemAction|2 Minute Delay","2 Minute Delay")%></option>
                                <option value="5"><%: Html.TranslateTag("Rule/CreateSystemAction|5 Minute Delay","5 Minute Delay")%></option>
                                <option value="10"><%: Html.TranslateTag("Rule/CreateSystemAction|10 Minute Delay","10 Minute Delay")%></option>
                                <option value="15"><%: Html.TranslateTag("Rule/CreateSystemAction|15 Minute Delay","15 Minute Delay")%></option>
                                <option value="30"><%: Html.TranslateTag("Rule/CreateSystemAction|30 Minute Delay","30 Minute Delay")%></option>
                                <option value="45"><%: Html.TranslateTag("Rule/CreateSystemAction|45 Minute Delay","45 Minute Delay")%></option>
                                <option value="60"><%: Html.TranslateTag("Rule/CreateSystemAction|1 Hour Delay","1 Hour Delay")%></option>
                                <option value="120"><%: Html.TranslateTag("Rule/CreateSystemAction|2 Hour Delay","2 Hour Delay")%></option>
                                <option value="240"><%: Html.TranslateTag("Rule/CreateSystemAction|4 Hour Delay","4 Hour Delay")%></option>
                                <option value="360"><%: Html.TranslateTag("Rule/CreateSystemAction|6 Hour Delay","6 Hour Delay")%></option>
                                <option value="480"><%: Html.TranslateTag("Rule/CreateSystemAction|8 Hour Delay","8 Hour Delay")%></option>
                                <option value="600"><%: Html.TranslateTag("Rule/CreateSystemAction|10 Hour Delay","10 Hour Delay")%></option>
                                <option value="720"><%: Html.TranslateTag("Rule/CreateSystemAction|12 Hour Delay","12 Hour Delay")%></option>
                                <option value="960"><%: Html.TranslateTag("Rule/CreateSystemAction|16 Hour Delay","16 Hour Delay")%></option>
                                <option value="1200"><%: Html.TranslateTag("Rule/CreateSystemAction|18 Hour Delay","20 Hour Delay")%></option>
                                <option value="1440"><%: Html.TranslateTag("Rule/CreateSystemAction|24 Hour Delay","24 Hour Delay")%></option>
                            </select>
                        </div>

                    </div>


                    <div class=" section system-action-select">
                        <div class="col-12  sub-text">
                            <label for="TargetRule"><%: Html.TranslateTag("Rule/CreateSystemAction|Target Rule")%></label>
                        </div>
                        <div class="col-md-6 col-12">
                            <select id="TargetRule" name="TargetRule" class="form-select user-dets select-action" style="width: 250px;">
                                <option value="-1">{<%: Html.TranslateTag("Target Self")%>}</option>
                                <% foreach (Notification item in ViewBag.NotificationList)
                                    {%>
                                <option value="<%=item.NotificationID%>"><%=item.Name %></option>
                                <%} %>
                            </select>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class=" section system-action-select" style="display: none;">
                        <div class="col-12  sub-text">
                            <label for="TargetAccumulator"><%: Html.TranslateTag("Rule/CreateSystemAction|Target Accumulator")%></label>
                        </div>
                        <div class="col-md-6 col-12">
                            <select id="TargetAccumulator" name="TargetAccumulatorID" class="form-select user-dets select-action" style="width: 250px;">
                                <% foreach (var item in Sensor.LoadByAccountIDAndApplicationID(MonnitSession.CurrentCustomer.Account.AccountID, 90))
                                    {%>
                                <option value="<%= item.SensorID %>"><%= item.SensorName %></option>
                                <%} %>
                            </select>
                        </div>
                        <div class="clearfix"></div>
                    </div>


                    <button id="AddButton" type="button" class="tiny-add  user-dets " style="width: 250px; margin-left: 8px; margin-bottom: 10px"
                        value="<%: Html.TranslateTag("Add","Add")%>">
                        <%: Html.TranslateTag("Add","Add")%>
                    </button>
            </div>
            </form>
           
       
        <%
            List<NotificationRecipient> systemActionList = MonnitSession.NotificationRecipientsInProgress.Where(nr => nr.NotificationType == eNotificationType.SystemAction || nr.NotificationType == eNotificationType.HTTP || nr.NotificationType == eNotificationType.ResetAccumulator).ToList();
        %>
            <div class="action-place">
                <div id="systemActionHolder" class="csa-list">
                    <div class="" id="systemActionList">
                        <%=Html.Partial("CreateSystemActionList", systemActionList) %>
                    </div>
                </div>
                <div class="done-btn">
                    <a href="/Rule/ChooseTask/" class="btn btn-primary user-dets"><%= Html.TranslateTag("Done")%> </a>
                    <button style="margin-left:10px;" type="button" id="cancelBtn" onclick="history.go(-1); return false;" class="btn btn-secondary"><%: Html.TranslateTag("Cancel","Cancel")%></button>
                </div>
            </div>

        </div>
    </div>





    <!-- pageHelp button modal -->
    <div class="modal fade systemActionHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <%=Html.Partial("_SystemActionHelpModalBody",NotificationWebhook) %>
        </div>
    </div>

    <script type="text/javascript">
        var failedAction = "<%: Html.TranslateTag("Rule/CreateSystemAction|Failed to set action")%>";

        $(document).ready(function () {
            $('#AddButton').click(function () {
                var body = $('#ActionForm').serialize();
                var href = "/Rule/SetSystemAction/";
                $.post(href, body, function (data) {
                    if (data == "Failed") {
                        $('#systemActionList').html(failedAction);
                    }
                    else
                        loadSystemActionList();
                });
            });

            $('select#Action').change(function () {
                if ($('select#Action').children(':selected').html() == "<%: Html.TranslateTag("Rule Webhook") %>") {
                    $('select#TargetRule').parent().parent().hide();
                    $('select#TargetAccumulator').parent().parent().hide();
                }
                else if ($('select#Action').children(':selected').html() == "<%: Html.TranslateTag("Reset Accumulator") %>") {
                    $('select#TargetRule').parent().parent().hide();
                    $('select#TargetAccumulator').parent().parent().show();
                }
                else {
                    $('select#TargetRule').parent().parent().show();
                    $('select#TargetAccumulator').parent().parent().hide();
                }
            });

        });

        function loadSystemActionList() {
            $('#systemActionHolder').show();
            $.get("/Rule/SystemActionList/", function (data) {
                $('#systemActionList').html(data);
            });
        }

        function removeSystemAction(notificationType, properties, delay, executeID) {

            $.post("/Rule/SystemActionDelete/", { notificationType: notificationType, properties: properties, delay: delay, executeID: executeID }, function (data) {
                if (data == "Success") {
                    loadSystemActionList();
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    </script>
</asp:Content>
