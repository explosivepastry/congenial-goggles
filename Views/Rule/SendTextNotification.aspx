<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Choose Action
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%  List<NotificationRecipientData> userList = NotificationRecipientData.SearchPotentialRecipient(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.NotificationInProgress.AccountID, "");

        List<NotificationRecipient> assignedRecipients = MonnitSession.NotificationRecipientsInProgress;
    %>

    <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <div class="msg_container">
        <div class="msg-title" style="width: auto;">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Send Text", "Send Text")%>
            </div>
            <div class="col-xs-2" id="newRefresh" style="padding-left: 0px;">
                <div style="float: right; margin-bottom: 5px;">
                    <div class="btn-group" style="height: 45px;">
                        <a onclick="removeNotifyTask('<%=eNotificationType.SMS.ToInt() %>');" style="cursor: pointer;" title="<%=Html.TranslateTag("Remove") %>"><%=Html.GetThemedSVG("delete") %></a>
                    </div>
                </div>
            </div>
        </div>

        <div class="msg-box">
            <%--<input type="hidden" id="notiName" value="<%=MonnitSession.NotificationInProgress.Name %>" />--%>
            <input type="hidden" id="notiClass" value="" />
            <%-- ----------SMS  CARD 2   ------------%>

            <div class="msg_card1">
                <div class="msg-top">
                    <div onclick="$('#myModal').modal('toggle');" class="user-inputs-box" style="cursor: pointer; display: flex">
                        <span class="send-to"><%= Html.TranslateTag("To") %>:</span>
                        <a class="msg-icon2" data-bs-toggle="modal" href="#myModal">
                            <div class="msg-icon"><%=Html.GetThemedSVG("circle-add") %> </div>
                        </a>
                        <div>
                            <div class="user-stamp" id="emailrecipients"><%=Html.Partial("_SendToNamesList", assignedRecipients.Where(m=>m.NotificationType == eNotificationType.SMS).ToList()) %></div>
                            <%--  🔻add recipients and create new modal --%>
                        </div>
                    </div>
                    <%--🔺------------------------------------%>
                </div>

                <div class="msg-sub">
                    <div class="txt-heading">
                        <%if (MonnitSession.AccountCan("text_override"))
                            {%>
                        <a class="merge-item" data-bs-toggle="modal" data-bs-target=".overrideValues" title="<%: Html.TranslateTag("Rule/SendNotification|Merge Fields")%>" style="cursor: pointer;">
                            <p class="merge-btn"><%: Html.TranslateTag("Rule/SendNotification|Merge Fields")%></p>
                        </a>
                        <%} %>
                    </div>
                    <textarea class="text-box-msg user-dets send-text-area-size" id="SMSText" maxlength="160" name="SMSText"><%=MonnitSession.NotificationInProgress.SMSText%></textarea>
                </div>
                <a href="#" class="msg-preview overridePreview" data-type="Text" data-bs-toggle="modal" data-bs-target=".previewOverride">
                    <strong><%= Html.TranslateTag("Message Preview") %></strong>
                </a>
                <div class="msg-save">
                    <div id="saveNotificationBtnSaveMsg"></div>
                    <button type="button" style="margin-right: 10px;" class="btn btn-primary user-dets" id="saveNotificationBtn"><%: Html.TranslateTag("Save")%></button>
                    <button type="button" id="cancelBtn" onclick="history.go(-1); return false;" class="btn btn-secondary"><%: Html.TranslateTag("Cancel","Cancel")%></button>
                </div>
            </div>
        </div>
    </div>

    <!-- Help Button Override Values -->
    <div class="modal fade overrideValues" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered tValues_modal">
            <%=Html.Partial("_MergeFieldModalBody") %>
        </div>
    </div>

    <!-- Help Button Override Preview -->
    <div class="modal fade previewOverride" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered tValues_modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modal"><%: Html.TranslateTag("Rule/SendNotification|Message Preview","Message Preview")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body " id="previewOverrideBody"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close", "Close")%></button>
                </div>
            </div>
        </div>
    </div>
    <%--  ----------- MODAL IN A MODAL --------%>

    <div class="modal fade " id="myModal">
        <div class="modal-dialog modal-dialog-scrollable">
            <div class="modal-content modal_container">
                <div class="modal-header">
                    <div class="modal-user msg-user-icon"><%=Html.GetThemedSVG("accountDetails") %></div>
                    <div class="modal-select">
                        <h4 class="modal-title-select"><%= Html.TranslateTag("Select Contacts") %></h4>
                        <span>
                            <input class="msg-input user-dets" type="text" id="recipientFilter" style="max-width: 250px;" placeholder="<%: Html.TranslateTag("Search Name","Search Name")%>" />
                        </span>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-hidden="true"></button>
                </div>
                <hr style="margin: 0 24px;" />
                <h4 class="modal-delay-title"></h4>
                <div class="modal-body modal-dialog-scrollable">
                    <div class="modal-dialog-scrollable">
                        <div class="msg-list" id="recipientList">
                            <%=Html.Partial("RecipientList", userList)%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="#" onclick="saveRecipients();" id="contactSave" data-bs-dismiss="modal" class="btn btn-primary btn-outline-dark user-dets"><%= Html.TranslateTag("Save") %></a>
                </div>
            </div>
        </div>
    </div>


    <style>
        svg {
            height: 100px;
            fill: #FFF;
        }

        #svg_api path {
            fill: white !important;
        }

        .x_panel .svg_icon {
            height: 25px;
            width: 25px;
        }

        .errorMsg {
            font-size: 20px;
            color: red;
            padding-right: 20px;
        }

        .successMsg {
            padding-right: 20px;
            font-size: 20px;
            color: green;
        }

        .spinnerAlign {
            margin-left: 45%;
        }


        /*------Modal in a Modal */



        .modal:nth-of-type(even) {
            z-index: 1062 !important;
        }

        .modal-backdrop.show:nth-of-type(even) {
            z-index: 1061 !important;
        }

        .modal {
            background: #00000054;
        }
    </style>


    <script>

        $('#myModal').on('shown.bs.modal', function () { $('#recipientFilter').focus(); })

        var successString = '<%=Html.TranslateTag("Success")%>';

        //when modal opens
        $('#myModal').on('shown.bs.modal', function (e) {
            $(".msg_container").css({ opacity: 1 });
        })

        //when modal closes
        $('#myModal').on('hidden.bs.modal', function (e) {
            $(".msg_container").css({ opacity: 1 });
        })

        $(function () {

            $('#recipientFilter').keyup(function (e) {
                e.preventDefault();
                $('.recipientHolder').hide();
                let query = $('#recipientFilter').val().toLowerCase();
                if (query.length > 0) {
                    $('.recipientHolder[filter*=' + query + ']').show();
                } else {
                    $('.recipientHolder').show();
                }
            });

            $('.overridePreview').click(function (e) {
                e.preventDefault();
                var msg = $("#SMSText").val().replace(/\n/g, '<br>\n');;
                var obj = $('#previewOverrideBody');
                obj.html("content/css/myloader.ascx");

                $.post('/Rule/NotifyBodyMsgPreview/', { msg: msg }, function (data) {
                    obj.html(data);
                });
            });

            $('#saveNotificationBtn').click(function () {

                $('.errorMsg').remove();
                $('#saveNotificationBtnSaveMsg').hide();

                var values = new Object();
                values["type"] = "<%=eNotificationType.SMS%>";

                values["SMSText"] = $("#SMSText").val();

                var recipients = $('.notifyUser.selected');
                if (recipients.length == 0) {
                    toastBuilder("No Recipient Selected");

                } else {
                    $.post('/Rule/SaveTextNotificationSettings/', values, function (data) {
                        if (data == "Success") {
                            toastBuilder("Success");
                            setTimeout(function () {
                                window.location.href = "/Rule/ChooseTask/";
                            }, 1000); // 1 second wait

                        } else {
                            var errors = data.split("|");
                            for (var i = 0; i < errors.length; i++) {
                                var errorData = errors[i].split(':');
                                var errorMsg = errorData[1];
                                toastBuilder(errorMsg);
                            }

                            $(window).scrollTop($('.errorMsg:first').offset().top);
                        }
                    });
                }
            });
        });

        function removeNotifyTask(eNotiType) {

            $.post("/Rule/RemoveNotifyTask/", { NotiType: eNotiType }, function (data) {
                if (data == "Success") {

                    window.location.href = "/Rule/ChooseTask/";

                } else {
                    $('#recipientList').html(data);
                }
            });

        }

        function saveRecipients() {

            var values = new Object();
            values["type"] = "<%=eNotificationType.SMS.ToInt()%>";
            var recipients = $('.notifyUser.selected');
            var recipientString = '';
            for (var i = 0; i < recipients.length; i++) {
                var recipient = recipients[i];
                var custID = $(recipient).attr('data-id');
                var custName = $(recipient).attr('data-name').replace(",", "");
                var groupID = $(recipient).attr('data-groupid');
                var delay = $(recipient).attr('data-delay');

                if (i == recipients.length - 1) {
                    recipientString += custID + "|" + custName + "|" + delay + (groupID.length > 0 ? "|" + groupID : "");
                } else {
                    recipientString += custID + "|" + custName + "|" + delay + (groupID.length > 0 ? "|" + groupID : "") + ",";
                }
            }
            values["recipient"] = recipientString;

            $.post('/Rule/SaveRecipients/', values, function (data) {
                $('#emailrecipients').html(data);
            });
        }

    </script>
</asp:Content>
