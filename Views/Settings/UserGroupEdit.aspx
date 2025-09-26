<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<CustomerGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UserGroupEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <div class="rule-card_container w-100 mt-4" style="gap: 1rem; padding: 1rem;">
            <div class="card_container__top">
                <div class="card_container__top__title" style="align-items: center">
                    <span class="card-icon-top">
                        <%=Html.GetThemedSVG("user-groups") %>
                        &nbsp;
                        <%if (Model.CustomerGroupID < 0)
                            { %>   <%:Html.TranslateTag("Settings/UserGroupEdit|Create","Create")%>    <%} %>
                        <%:Html.TranslateTag("Settings/UserGroupEdit|Notification Group","Notification Group")%>
                    </span>
                    &nbsp;
                </div>
            </div>
            <div class="x_content" id="userGroupDiv">
                <div class="powertour-hook" id="hook-two" style="background-color: white;">
                    <div class="form-group d-flex" style="gap: 1rem; align-items: center;">
                        <label class="" for="networkName">
                            <%: Html.TranslateTag("Settings/UserGroupEdit|Name","Name")%>
                        </label>
                        <input type="text" id="Name" name="Name" required="required" value="<%=Model.Name %>" class="form-control user-dets aSettings__input_input" style="height: 36px" placeholder="Group name...">
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <br />

    <div class="rule-card_container w-100 mt-6 powertour-hook" id="hook-five" style="max-height: 600px; <%= Model.CustomerGroupID <= 0 ? "display:none;": "" %>">
        <div class="card_container">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <span class="card-icon-top">
                        <%=Html.GetThemedSVG("recipients") %>
                            &nbsp;
                    <%: Html.TranslateTag("Settings/UserGroupEdit|Recipients and Delivery Methods","Recipients and Delivery Methods")%>
                    </span>
                </div>
            </div>

            <div class="card_container__body">
                <div class="dfac flex-col-on-mobile align-start-on-mobile margin-for-mobile" style="margin-bottom: 16px; justify-content: space-between;">
                    <div id="textWrapper">
                        <div style="margin: 1rem 0px;">
                            <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "Click the icon to enable or disable ")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 18 18">
                                <path id="paper-plane-regular" style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" />
                            </svg>
                            <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", " email,")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 18 16">
                                <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M16.616,44.65a5.259,5.259,0,0,0,1.375-3.507c0-2.857-2.39-5.218-5.506-5.639A6.568,6.568,0,0,0,6.493,32c-3.59,0-6.5,2.557-6.5,5.714a5.272,5.272,0,0,0,1.375,3.507A9.448,9.448,0,0,1,.19,43.182a.913.913,0,0,0-.137.893.725.725,0,0,0,.662.5,6.586,6.586,0,0,0,3.912-1.386,7.9,7.9,0,0,0,.887.175,6.548,6.548,0,0,0,5.977,3.493,7.286,7.286,0,0,0,1.869-.243A6.6,6.6,0,0,0,17.273,48a.728.728,0,0,0,.662-.5.921.921,0,0,0-.137-.893A9.187,9.187,0,0,1,16.616,44.65Zm-12.274-3.3-.534.4a6.077,6.077,0,0,1-1.347.764c.084-.168.169-.346.25-.529L3.2,40.875,2.421,40a3.478,3.478,0,0,1-.928-2.286c0-2.168,2.29-4,5-4s5,1.832,5,4-2.29,4-5,4a6,6,0,0,1-1.531-.2Zm11.221,2.075-.772.871.484,1.111c.081.182.166.361.25.529a6.077,6.077,0,0,1-1.347-.764l-.534-.4-.622.164a6,6,0,0,1-1.531.2,5.435,5.435,0,0,1-4.1-1.775c3.165-.386,5.6-2.764,5.6-5.654,0-.121-.012-.239-.022-.357,2.012.518,3.521,2.029,3.521,3.786A3.478,3.478,0,0,1,15.563,43.429Z" transform="translate(0.007 -32)" />
                            </svg>
                            <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", " SMS,")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 14 18">
                                <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M3.548,17.823a11.389,11.389,0,0,1,0-16.521.644.644,0,0,1,.775-.085L6.687,2.641a.593.593,0,0,1,.25.737L5.755,6.226a.629.629,0,0,1-.644.377l-2.035-.2a9.025,9.025,0,0,0,0,6.311l2.035-.2a.629.629,0,0,1,.644.377l1.182,2.848a.593.593,0,0,1-.25.737L4.324,17.908A.644.644,0,0,1,3.548,17.823ZM9.01,3.357a2.183,2.183,0,0,1,0,2.287.449.449,0,0,1-.682.091l-.218-.2a.414.414,0,0,1-.082-.507,1.092,1.092,0,0,0,0-1.053.414.414,0,0,1,.082-.507l.218-.2A.449.449,0,0,1,9.01,3.357ZM12.356.151a6.576,6.576,0,0,1,0,8.7.45.45,0,0,1-.64.033l-.211-.2a.412.412,0,0,1-.034-.576,5.48,5.48,0,0,0,0-7.222A.412.412,0,0,1,11.5.313l.211-.2a.45.45,0,0,1,.64.033ZM10.68,1.731a4.38,4.38,0,0,1,0,5.539.45.45,0,0,1-.651.046l-.212-.2a.411.411,0,0,1-.047-.56,3.284,3.284,0,0,0,0-4.118.411.411,0,0,1,.047-.56l.212-.2a.45.45,0,0,1,.651.046Z" transform="translate(0 0)" />
                            </svg>
                            <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "voice, and ")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" viewBox="0 0 24 24">
                                <path style="fill: <%:MonnitSession.CurrentStyle("OptionsIconColor")%>" d="M15 21c0 1.598-1.392 3-2.971 3s-3.029-1.402-3.029-3h6zm.137-17.055c-.644-.374-1.042-1.07-1.041-1.82v-.003c.001-1.172-.938-2.122-2.096-2.122s-2.097.95-2.097 2.122v.003c.001.751-.396 1.446-1.041 1.82-4.668 2.709-1.985 11.715-6.862 13.306v1.749h20v-1.749c-4.877-1.591-2.193-10.598-6.863-13.306zm-3.137-2.945c.552 0 1 .449 1 1 0 .552-.448 1-1 1s-1-.448-1-1c0-.551.448-1 1-1zm-6.451 16c1.189-1.667 1.605-3.891 1.964-5.815.447-2.39.869-4.648 2.354-5.509 1.38-.801 2.956-.76 4.267 0 1.485.861 1.907 3.119 2.354 5.509.359 1.924.775 4.148 1.964 5.815h-12.903zm15.229-7.679c.18.721.05 1.446-.304 2.035l.97.584c.504-.838.688-1.869.433-2.892-.255-1.024-.9-1.848-1.739-2.351l-.582.97c.589.355 1.043.934 1.222 1.654zm.396-4.346l-.597.995c1.023.616 1.812 1.623 2.125 2.874.311 1.251.085 2.51-.53 3.534l.994.598c.536-.892.835-1.926.835-3-.001-1.98-1.01-3.909-2.827-5.001zm-16.73 2.692l-.582-.97c-.839.504-1.484 1.327-1.739 2.351-.255 1.023-.071 2.053.433 2.892l.97-.584c-.354-.588-.484-1.314-.304-2.035.179-.72.633-1.299 1.222-1.654zm-4.444 2.308c0 1.074.299 2.108.835 3l.994-.598c-.615-1.024-.841-2.283-.53-3.534.312-1.251 1.101-2.258 2.124-2.873l-.597-.995c-1.817 1.092-2.826 3.021-2.826 5z"/>
                            </svg>

                            <%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "push alerts for each recipient.")%>
                        </div>
                        <div style="margin: 1rem 0px;"><%: Html.TranslateTag("Events/ActionUserNotification|Click on icon to enable/disable Recipient Notifications", "Click the text below the icon to set a delivery delay.")%></div>
                    </div>
                    <input class="form-control aSettings__input_input user-dets" type="text" id="userSearch" placeholder="<%: Html.TranslateTag("Events/ActionUserNotification|Name Filter", "Filter by recipient...")%>" />
                </div>

                <div class="panel panel-heading" style="margin-bottom: 16px;">
                    <div class="listHeader">
                        <div class="col-6 shrink-col-6-mobile col-xs-6 listHeader__titles"><%: Html.TranslateTag("Events/ActionUserNotificationList|Recipient", "Recipient")%></div>
                        <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles"><%: Html.TranslateTag("Events/ActionUserNotificationList|Email", "Email")%></div>
                        <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles"><%: Html.TranslateTag("Events/ActionUserNotificationList|SMS", "SMS")%></div>
                        <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles"><%: Html.TranslateTag("Events/ActionUserNotificationList|Voice", "Voice")%></div>
                        <div class="col-2AB shrink-col-2-mobile col-xs-2 listHeader__titles"><%: Html.TranslateTag("Events/ActionUserNotificationList|Push", "Push")%></div>
                    </div>
                </div>

                <div id="userList" class="bsInset verticalScroll">
                    <% List<CustomerGroupLink> customers = CustomerGroupLink.LoadByCustomerGroupID(ViewBag.GroupID);%>
                    <%=Html.Partial("CustomerGroupLinkList", customers)%>
                </div>
            </div>
        </div>
    </div>

    <div class="d-flex w-100" style="align-items: center; justify-content: space-between; margin-top: 1rem;">
        <div id="wrapperForSometimesSpan">
        </div>
        <div>
            <button class="btn btn-secondary" type="submit" id="back" value="<%: Html.TranslateTag("Back","Back")%>">
                <%: Html.TranslateTag("Back","Back")%>
            </button>
            <button class="btn btn-primary" type="submit" id="save" onclick="postUserGroup()" value="<%: Html.TranslateTag("Save","Save")%>">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
            <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                <%: Html.TranslateTag("Saving...","Saving...")%>
            </button>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#userSearch').keyup(function (e) {
                e.preventDefault();
                filterUsers();
            });

            $('#userFilter').change(function (e) {
                e.preventDefault();
                loadUsers();
            });
        });

        $('#back').on("click", function (event) {
            event.preventDefault();
            window.history.back();
        });

        function loadUsers() {
            var query = $('#userSearch').val();
            var type = $('#userFilter').val();
            var accID = <%=Model.AccountID%>;
            $.post("/Settings/UserGroupUserFilter", { id: accID, groupID: <%=Model.CustomerGroupID%>, userType: type, q: query }, function (data) {
                if (data == "Failed") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else {
                    $('#userList').html(data);
                }
            });
        }

        function postUserGroup() {
            $('#save').hide();
            $('#saving').show();
            const userGroupName = $("#Name").val();

            let collectionToSubmit = $('.recipientCardAB').map((index, element) => {
                let recipientId = $(element).data('recipientid')
                return {
                    Id: recipientId,
                    Name: $(element).data('recipientname'),
                    Email: $(`#openSpinnerModalFor_${recipientId}_EmailInput`).val(),
                    Text: $(`#openSpinnerModalFor_${recipientId}_TextInput`).val(),
                    Voice: $(`#openSpinnerModalFor_${recipientId}_VoiceInput`).val(),
                    Push: $(`#openSpinnerModalFor_${recipientId}_PushInput`).val(),
                };
            }).get();


            // Remove the users that don't have any notification method enabled.
            collectionToSubmit = collectionToSubmit.filter(userObj => {
                return !(
                    (userObj.Email === '-1' || userObj.Email === undefined) &&
                    (userObj.Text === '-1' || userObj.Text === undefined) &&
                    (userObj.Voice === '-1' || userObj.Voice === undefined) &&
                    (userObj.Push === '-1' || userObj.Push === undefined)
                );
            });

            if (collectionToSubmit.length === 0 && $('#hook-five').is(':visible')) {
                toastBuilder("Please select one delivery method for at least one recipient");
                $('#save').show();
                $('#saving').hide();
                return;
            }

            if (userGroupName.length === 0 && $('#hook-five').is(':visible')) {
                toastBuilder("Please enter a group name");
                $('#save').show();
                $('#saving').hide();
                return;
            }

            const jsonValuesToSave = JSON.stringify(collectionToSubmit);

            $.post("/Settings/UserGroupEdit/<%=Model.CustomerGroupID%>", { userGroupName: userGroupName, jsonValues: jsonValuesToSave }, function (data) {
                if (data == "Failed") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else if ($('#hook-five').is(':visible')) {
                    window.location.href = '/Settings/UserGroupList/<%=MonnitSession.CurrentCustomer.AccountID %>';
                } else {
                    window.location.href = `/Settings/UserGroupEdit/${data}`;
                }
                $('#save').show();
                $('#saving').hide();
            });
        };


        function filterUsers() {
            const filterQuery = $('#userSearch').val().trim().toLowerCase();

            $('.recipientCardAB').each(function () {
                var recipientName = $(this).data('recipientname').toLowerCase();
                if (recipientName.includes(filterQuery)) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        };

        if ("<%=ViewBag.message %>" == "Success") {
            toastBuilder("Group saved", "<%=ViewBag.message %>");
        } else {
            toastBuilder("<%=ViewBag.message %>");
        }

    </script>

</asp:Content>
