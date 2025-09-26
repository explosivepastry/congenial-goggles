<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ExternalSubscriptionPreference>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Webhook Notification
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        List<Monnit.ExternalDataSubscription> Webhooks = ExternalDataSubscription.LoadAllByAccountID(Model.AccountID);
        ExternalDataSubscription DataEDS = Webhooks.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook).FirstOrDefault();
        ExternalDataSubscription NtfcEDS = Webhooks.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification).FirstOrDefault();
        
    %>
    <div class="container-fluid">
        <div class="col-12">

            <%--<%ExternalDataSubscription exdata = MonnitSession.CurrentCustomer.Account.ExternalDataSubscription;

                if (exdata == null)
                    exdata = new ExternalDataSubscription();%>--%>

            <%Html.RenderPartial("_APILink"); %>
        </div>

        <div class="col-12" >
            <div class="x_panel shadow-sm rounded gy-5">
                <div class="card_container__top">
                    <div class="card_container__top__title" style="overflow: unset;">
                        <%:Html.TranslateTag("Export/DataPushNotification|Webhook Notifications","Webhook Notifications")%>
                    </div>
                </div>

                <div class="x_content" style="padding: 10px;">
                    <form id="ExternalDataPreferenceSettingsForm">
                        <%: Html.HiddenFor(model => model.AccountID)%>

                        <div class="form-group row">
                            <div class="bold col-sm-10 col-12">
                                <span><%:Html.TranslateTag("Export/DataPushNotification|These settings allow you to customize when and to whom emails are sent when your External Data Push fails multiple times in a row. You will only be notified once every 24 hours, unless your data push failures exceed 100, in which case you will be notified of your data push being disabled.","These settings allow you to customize when and to whom emails are sent when your External Data Push fails multiple times in a row. You will only be notified once every 24 hours, unless your data push failures exceed 100, in which case you will be notified of your data push being disabled.")%></span>
                            </div>
                        </div>

                        <div class="col-6">
                        <div class="form-group  row">
                            <div class="bold col-sm-3 col-12">
                               <%:Html.TranslateTag("Export/DataPushNotification|Last Email Date","Last Email Date")%>
                            </div>

                            <div class="col-sm-9 col-12 lgBox">
                                <%: Model.LastEmailDate.OVToLocalDateTimeShort()%>
                            </div>
                        </div>

                         <div class="form-group row">
                               <div class="bold col-12">
                                    <%:Html.TranslateTag("Export/DataPushNotification|Current Broken Count","Current Broken Count")%>
                                </div>

                                <ul>
                                    <li>
                                        <div class="row">
                                            <div class="col-4"><%:Html.TranslateTag("Export/DataPushNotification|Data Webhook","Data Webhook")%></div>
                                            <div class="col-4 lgBox">
                                                <%: DataEDS == null ? "N/A" : DataEDS.BrokenCount.ToString() %>
                                            </div>

                                            <div class="col-4">
                                                <input type="button" <%: DataEDS == null ? "disabled" : "onclick=resetBroken(" + DataEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Export/DataPushNotification|Reset","Reset")%>" class="btn <%: DataEDS == null ? "btn-secondary" : "btn-primary" %>" />
                                            </div>
                                        </div>
                                    </li>

                                    <li>
                                        <div class="row">
                                            <div class="col-4"><%:Html.TranslateTag("Export/DataPushNotification|Rule Webhook","")%></div>
                                            <div class="col-4 lgBox">
                                                <%: NtfcEDS == null ? "N/A" : NtfcEDS.BrokenCount.ToString() %>
                                            </div>

                                            <div class="col-4">
                                                <input type="button" <%: NtfcEDS == null ? "disabled" : "onclick=resetBroken(" + NtfcEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Export/DataPushNotification|Reset","Reset")%>" class="btn <%: NtfcEDS == null ? "btn-secondary" : "btn-primary" %>" />
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>

                        <div class="form-group row">
                            <div class="bold col-sm-3">
                                <%:Html.TranslateTag("Export/DataPushNotification|Broken Count Notification Threshold","Broken Count Notification Threshold")%>
                            </div>

                            <div class="col-sm-9 col-12 lgBox">
                                <input class="form-control" style="width: 250px;" id="BrokenCountNotificationThreshold" name="BrokenCountNotificationThreshold" type="text" value="<%=Model.UsersBrokenCountLimit %>">
                                <div class="editor-error"><%:Html.ValidationMessageFor(model => model.UsersBrokenCountLimit) %></div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="bold col-sm-3">
                                <%:Html.TranslateTag("Export/DataPushNotification|User to be notified","User to be notified")%>
                            </div>

                            <div class="col-sm-9 col-12 lgBox">
                                <div class="editor-field">
                                    <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>
                                    <%:Html.DropDownListFor(model => model.UserId, custlist, "FullName") %>
                                </div>

                                <div class="editor-error">
                                </div>

                            </div>
                        </div>
                     </div>

                        <div class="text-end">
                            <input id="submitExternalData" class="btn btn-primary" type="button" value="<%:Html.TranslateTag("Export/DataPushNotification|Save","Save")%>" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var resetBrokenString = "<%: Html.TranslateTag("Reset broken count to 0?","Reset broken count to 0?")%>";

        $('#UserId').addClass('form-select');
        $('#UserId').css("width", "250px")

        $(function () {

            $('#submitExternalData').click(function () {
                var Form = $('#ExternalDataPreferenceSettingsForm');
                var formCollection = Form.serialize();
                $.post("/Account/ExternalDataPreferenceSettings/<%:Model.ExternalSubscriptionPreferenceID%>", formCollection, function (data) {
                    if (data == "Success") {
                        window.location.href = "/Export/DataPushNotification/<%:Model.ExternalSubscriptionPreferenceID%>";
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to save settings")%>");
                    }
                });
            });
        });

        function resetBroken(edsID) {
            if (confirm(resetBrokenString)) {
                $.get('/Export/ResetBrokenExternalDataSubscription/' + edsID, function (data) {
                    if (data == 'Success') {
                        showSimpleMessageModal("<%=Html.TranslateTag("Success")%>");
                        window.location.reload();
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            }
        }

    </script>

</asp:Content>
