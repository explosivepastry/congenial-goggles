<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>

<form method="post" id="CreditPreferenceSettingsForm" class="form-horizontal form-label-left">

    <%:Html.Hidden("accountID", MonnitSession.CurrentCustomer.AccountID)%>

    <%
        Account acc = Account.Load(Model.AccountID);
        bool currentCustomerHasDefaultPayment = MonnitSession.CurrentCustomer.Account.DefaultPaymentID > 0 ? true : false;
    %>

    <input type="hidden" name="creditClassification" value="<%=eCreditClassification.Notification.ToInt() %>" />

    <div style="margin: 0px 0px 15px 5px;">
        <table class="table table-hover">
            <thead>
                <tr style="background-color: #e3e3e3;">
                    <th></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Used","Used")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Remaining","Remaining")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Expiration","Expiration")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Activation Date","Activation Date")%></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <%foreach (NotificationCredit creditgroup in NotificationCredit.LoadAvailable(Model.AccountID))
                    {%>
                <tr>
                    <th style="color: black; padding: 6px;"><%:creditgroup.NotificationCreditType.Name %></th>
                    <td style="padding: 6px;"><%:creditgroup.UsedCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.RemainingCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.ExpirationDate > DateTime.UtcNow.AddYears(10) ? "N/A" : creditgroup.ExpirationDate.ToShortDateString() %></td>
                    <td style="padding: 6px;"><%:creditgroup.ActivationDate > DateTime.UtcNow.AddYears(10) ? "N/A" : creditgroup.ActivationDate.ToShortDateString() %></td>
                    <td style="padding: 0px; padding-top: 6px;"><%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                                                                    {%>
                        <a title="<%: Html.TranslateTag("Retail/NotificationCredit|Remove Credits","Remove Credits")%>" href="/Notification/RemoveCredits/<%:creditgroup.NotificationCreditID %>" onclick="removeCredits(this); return false;">
                            <%=Html.GetThemedSVG("delete") %></a></td>
                    <%} %>
                </tr>
                <%} %>
                <tr style="background-color: #e3e3e3;">
                    <th style="color: black;"><%: Html.TranslateTag("Retail/NotificationCredit|Total","Total")%></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th style="font-weight: 500; padding: 6px;"><%: Model.CreditsAvailable < 0 ? "0": Model.CreditsAvailable.ToString() %></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th></th>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="row">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Notify when credits remaining is below","Notify when credits remaining is below")%>: 
            </div>
        </div>
        <div class="col-8">
            <div class="form-group">
                <input class="form-control" style="width: 250px;" id="CreditCompareValue" name="CreditCompareValue" type="text" value="<%=Model.CreditCompareValue %>">
            </div>
        </div>
    </div>
    <div class="">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|User to be notified","User to be notified")%>: 
            </div>
        </div>
        <div class="col-8">
            <div class="form-group">
                <%List<Customer> custlist = Customer.LoadAllForReseller(MonnitSession.CurrentCustomer.CustomerID, Model.AccountID); %>

                <select class="form-select" id="UserId" name="UserId" style="width: 250px;">
                    <%foreach (Customer cust in custlist)
                        { %>
                    <option value="<%=cust.CustomerID %>" <%=cust.CustomerID == Model.UserId ? "selected='selected'" : "" %>><%=cust.FullName %></option>
                    <%} %>
                </select>
            </div>
        </div>
        <%if (MonnitSession.CurrentTheme.Theme == "Default" && MonnitSession.CustomerCan("See_Beta_Preview"))
            {%>

        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Also auto purchase more credit:","Also auto purchase more credits:")%>
            </div>
        </div>

        <select class="form-select" id="EnableAutoPurchase" name="EnableAutoPurchase" style="width: 250px;">
            <option value="on" <%=acc.AutoPurchase ? "selected" : "" %>>100 Credits</option>
            <option value="off" <%=acc.AutoPurchase ? "" : "selected" %>>No auto purchase</option>
        </select>
            <div id="enterYourCardFool" style="display:<%:!currentCustomerHasDefaultPayment && acc.AutoPurchase ? "block" : "none"%>">
                <p class="red" style="margin-top: 5px; margin-bottom: 10px">Must have credit card on file to enable auto purchase.</p>
                <a style="color: <%=MonnitSession.CurrentStyle("OptionsIconColor")%>" href="/Retail/PaymentOption/<%=MonnitSession.CurrentCustomer.AccountID%>">Enter credit card here</a>
            </div>
        <%}%>
        <br />
        <div class="bold col-4"></div>
        <div class="col-12 text-end">
            <button type="button" id="submitCreditSettings" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary user-dets">
                <%:Html.TranslateTag("Save","Save")%>
            </button>
        </div>
    </div>
</form>

<script type="text/javascript">
    $(document).ready(function () {
        const currentCustomerHasDefaultPayment = <%= currentCustomerHasDefaultPayment ? "true" : "false" %>;
        const displayCardEnterText = document.querySelector("#enterYourCardFool");
        const selectElement = document.querySelector("#EnableAutoPurchase");

        if (selectElement) {
            selectElement.addEventListener("change", () => {
                if (selectElement.value === "on" && !currentCustomerHasDefaultPayment) {
                    displayCardEnterText.style.display = "block";
                } else {
                    displayCardEnterText.style.display = "none";
                }
            });
        }

        $('#submitCreditSettings').click(function () {
            const creditform = $('#CreditPreferenceSettingsForm').serialize();

            $.post("/Retail/CreditSetting/<%:Model.CreditSettingID%>", creditform, function (data) {
                if (data === "Success") {
                    window.location.href = "/Retail/NotificationCredit/<%=Model.AccountID%>";
                    toastBuilder("Success");
                } else if (data === "Credit Card Required") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings. Please enter your credit card information.")%>");
                } else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings")%>");
                }
            });
        });
    });
</script>



