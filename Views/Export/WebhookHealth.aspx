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

        <div class="col-12">
            <div class="x_panel shadow-sm rounded gy-5">
                <div class="card_container__top">
                    <div class="card_container__top__title" style="overflow: unset;">
                        <%:Html.TranslateTag("Export/WebhookHealth|Webhook Health","Webhook Health")%>
                    </div>
                </div>

                <div class="x_content" style="padding: 10px;">
                    <div class="form-group row" style="border-bottom: 0.2px solid #e6e6e6;">
                        <div class="bold col-sm-10 col-12">
                            <div><%:Html.TranslateTag("Export/DataPushInfo|These settings allow you to customize when and to whom emails are sent when your webhook connection fails. If the number of failures exceeds the \"Broken Count Notification Threshold\" the \"User to be notified\" will receive an email alerting them. The email will be sent once every 24 hours while number of failures remains in excess of the number of failures exceeds the \"Broken Count Notification Threshold\".","These settings allow you to customize when and to whom emails are sent when your webhook connection fails. If the number of failures exceeds the \"Broken Count Notification Threshold\" the \"User to be notified\" will receive an email alerting them. The email will be sent once every 24 hours while number of failures remains in excess of the number of failures exceeds the \"Broken Count Notification Threshold\".")%></div>
                            <br />
                            <div><%:Html.TranslateTag("Export/DataPushInfo|We will attempt to resend webhook messages that have been failed to be received until the number of total failures exceeds " + ExternalDataSubscription.killRetryLimit + ", after which time each message will only be attempted to be delivered once. If a message is delivered successfully the \"Broken Count\" will be reset to zero.","We will attempt to resend webhook messages that have been failed to be received until the number of total failures exceeds " + ExternalDataSubscription.killRetryLimit + ", after which time each message will only be attempted to be delivered once. If a message is delivered successfully the \"Broken Count\" will be reset to zero.")%></div>
                            <br />
                            <div><%:Html.TranslateTag("Export/DataPushInfo|We will stop attempting to deliver all webhook messages once the number of failures exceeds " +  ExternalDataSubscription.killRetryLimit + ". You will then be required to visit this page to reset the \"Broken Count\" before we will resume attempting delivery.","We will stop attempting to deliver all webhook messages once the number of failures exceeds " +  ExternalDataSubscription.killRetryLimit + ". You will then be required to visit this page to reset the \"Broken Count\" before we will resume attempting delivery.")%></div>
                            <br />
                            <%--<div>   <% string sauce = $"hello {ExternalDataSubscription.killRetryLimit}"; %> </div>--%> <%--Something is forcing views to compile in <= C# 5--%>
                        </div>
                    </div>
                </div>

                <div class="x_content" style="padding: 10px;">
                    <form id="ExternalDataPreferenceSettingsForm" method="post">
                        <%: Html.HiddenFor(model => model.AccountID)%>

                        <%--<div style="clear: both"></div>--%>
                        <div class="col-6">
                            <div class="form-group  row">
                                <div class="bold col-sm-4 col-12">
                                    <%:Html.TranslateTag("Export/WebhookHealth|Last Email Date","Last Email Date")%>
                                </div>

                                <div class="col-sm-4 col-12 lgBox">
                                    <%: Model.LastEmailDate == DateTime.MinValue ? "N/A" : Model.LastEmailDate.OVToLocalDateTimeShort() %>
                                </div>
                            </div>

                            <div class="form-group  row">
                                <div class="col-sm-12" style="font-weight: bold;"><%:Html.TranslateTag("Export/WebhookHealth|Current Broken Count","Current Broken Count")%> </div>
                            </div>

                            <div class="form-group  row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3 col-12"><%:Html.TranslateTag("Export/WebhookHealth|Data Webhook","Data Webhook")%></div>
                                <div class="col-sm-4 col-12 lgBox">
                                    <%: DataEDS == null ? "N/A" : DataEDS.BrokenCount.ToString() %>
                                </div>

                                <div class="col-sm-4 col-12">
                                    <input type="button" <%: DataEDS == null ? "disabled" : "onclick=resetBroken(" + DataEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Export/WebhookHealth|Reset","Reset")%>" class="btn btn-secondary" />
                                </div>
                            </div>

                            <div class="form-group  row">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3 col-12"><%: Html.TranslateTag("Export/WebhookHealth|Rule Webhook","Rule Webhook")%></div>
                                <div class="col-sm-4 col-12 lgBox">
                                    <%: NtfcEDS == null ? "N/A" : NtfcEDS.BrokenCount.ToString() %>
                                </div>

                                <div class="col-sm-4 col-12">
                                    <input type="button" <%: NtfcEDS == null ? "disabled" : "onclick=resetBroken(" + NtfcEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Export/WebhookHealth|Reset","Reset")%>" class="btn btn-secondary" />
                                </div>
                            </div>

                            <%--<div class="form-group row">
                                <div class="bold col-12">
                                    Current Broken Count
                                </div>
                                <ul>
                                    <li>
                                        <div class="row">
                                            <div class="col-4">Data Webhook</div>
                                            <div class="col-4 lgBox">
                                                <%: DataEDS == null ? "N/A" : DataEDS.BrokenCount.ToString() %>
                                            </div>
                                            <div class="col-4">
                                                <input type="button" <%: DataEDS == null ? "disabled" : "onclick=resetBroken(" + DataEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Reset","Reset")%>" class="btn btn-secondary" />
                                            </div>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="row">
                                            <div class="col-4">Notification Webhook</div>
                                            <div class="col-4 lgBox">
                                                <%: NtfcEDS == null ? "N/A" : NtfcEDS.BrokenCount.ToString() %>
                                            </div>
                                            <div class="col-4">
                                                <input type="button" <%: NtfcEDS == null ? "disabled" : "onclick=resetBroken(" + NtfcEDS.ExternalDataSubscriptionID + ");" %> value="<%: Html.TranslateTag("Reset","Reset")%>" class="btn btn-secondary" />
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>--%>

                            <div class="form-group row">
                                <div class="bold col-sm-3">
                                    <%: Html.TranslateTag("Export/WebhookHealth|Broken Count Notification Threshold","Broken Count Notification Threshold")%>
                                </div>

                                <div class="col-sm-9 col-12 lgBox">
                                    <input class="form-control" style="width: 250px;" id="UsersBrokenCountLimit" name="UsersBrokenCountLimit" type="text" value="<%=Model.UsersBrokenCountLimit %>">
                                    <%--<div class="editor-error"><%:Html.ValidationMessageFor(model => model.UsersBrokenCountLimit) %></div>--%>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="bold col-sm-3">
                                    <%: Html.TranslateTag("Export/WebhookHealth|User to be notified","User to be notified")%>
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
                            <input id="submitExternalData" class="btn btn-primary" type="submit" value="<%: Html.TranslateTag("Export/WebhookHealth|Save","Save")%>" />
                        </div>
                    </form>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">

        var resetBrokenString = '<%: Html.TranslateTag("Export/WebhookHealth|Reset broken count to 0?","Reset broken count to 0?")%>';

        $('#UserId').addClass('form-select');
        $('#UserId').css("width", "250px")

        <%--$(function () {
            $('#submitExternalData').click(function () {
                var Form = $('#ExternalDataPreferenceSettingsForm');
                var formCollection = Form.serialize();
                $.post("/Account/ExternalDataPreferenceSettings/<%:Model.ExternalSubscriptionPreferenceID%>", formCollection, function (data) {
                    if (data == "Success") {
                        window.location.href = "/Export/DataPushNotification/<%:Model.ExternalSubscriptionPreferenceID%>";
                    }
                    else {
                        alert("Failed to save  settings");
                    }
                });
            });
        });--%>

        function resetBroken(edsID) {
            if (confirm(resetBrokenString)) {
                $.get('/Export/ResetBrokenExternalDataSubscription/' + edsID, function (data) {
                    if (data == 'Success') {
                        showSimpleMessageModal("<%=Html.TranslateTag("Success","Success")%>");
                        window.location.reload();
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed","Failed")%>");
                    }
                });
            }
        }

    </script>

</asp:Content>
