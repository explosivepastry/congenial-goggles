<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>


<form method="post" id="CreditPreferenceSettingsForm" class="form-horizontal form-label-left">

    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <%: Html.Hidden("accountID",MonnitSession.CurrentCustomer.AccountID)%>
    <input type="hidden" name="creditClassification" value="<%=eCreditClassification.Notification.ToInt() %>" />

<%--    <div class=" bold col-xs-6 col-sm-6 col-md-6 col-lg-6" style="padding: 0px; width: 100%; margin: 0px; margin-left: 30px;">--%>
    <div class="row" style="margin: 0px 0px 15px 5px;">
        <table class="table tableCard_shadow" style="width: 50%; background-color: white; margin: 0px; padding: 0px;">
            <thead>
                <tr style="background-color: #e3e3e3; border-radius: 8px 8px 0px 0px;">
                    <th style="border-radius: 8px 0px 0px 0px;"></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Used","Used")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Remaining","Remaining")%></th>
                    <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Expiration","Expiration")%></th>
                    <th style="border-radius: 0px 8px 0px 0px;"></th>
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
                    <td style="padding: 0px; padding-top: 6px;"><%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                                                                    {%>
                        <a title="<%: Html.TranslateTag("Retail/NotificationCredit|Remove Credits","Remove Credits")%>" href="/Notification/RemoveCredits/<%:creditgroup.NotificationCreditID %>" onclick="removeCredits(this); return false;">
                            <%=Html.GetThemedSVG("delete") %></a></td>
                    <%} %>
                </tr>
                <%} %>
                 <tr style="background-color: #e3e3e3; border-radius: 0px 0px 0px 8px;">
                    <th style="border-radius: 0px 0px 0px 8px;color:black;"><%: Html.TranslateTag("Retail/NotificationCredit|Total","Total")%></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th style="font-weight:500; padding: 6px;"><%: Model.CreditsAvailable < 0 ? "0": Model.CreditsAvailable.ToString() %></th>
                    <th style="color: black; padding: 6px;"></th>
                    <th style="border-radius: 0px 0px 8px 0px;"></th>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="row">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px; margin-left: 20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|Notify when credits remaining is below","Notify when credits remaining is below")%>: 
           
            </div>
        </div>
        <div class="col-8" style="margin-left: 25px;">
            <div class="form-group">
                <input class="aSettings__input_input" style="width: 152px; height: 25px; border: none; border: 1px solid #e3e3e3; border-radius: 4px;" id="CreditCompareValue" name="CreditCompareValue" type="text" value="<%=Model.CreditCompareValue %>">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="bold" style="font-weight: bold;">
            <div class="form-group" style="padding-top: 5px; margin-left: 20px;">
                <%: Html.TranslateTag("Retail/CreditNotificationSettings|User to be notified","User to be notified")%>: 
           
            </div>
        </div>
        <div class="col-8" style="margin-left: 25px;">
            <div class="form-group">
<%--                <%List<Customer> custlist = Customer.LoadAllByAccount(Model.AccountID);%>--%>
                    <%List<Customer> custlist = Customer.LoadAllForReseller(MonnitSession.CurrentCustomer.CustomerID, Model.AccountID); %>

                <select class="tzSelect" id="UserId" name="UserId" style="width: 152px; height: 25px;">
                    <%foreach (Customer cust in custlist)
                        { %>
                    <option value="<%=cust.CustomerID %>" <%=cust.CustomerID == Model.UserId ? "selected='selected'" : "" %>><%=cust.FullName %></option>
                    <%} %>
                </select>
            </div>
        </div>
        <div class="col-12 text-end">
            <button type="button" id="submitCreditSettings" value="<%:Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                <%:Html.TranslateTag("Save","Save")%>
            </button>
        </div>
    </div>
</form>

<script type="text/javascript">
    $(function () {
        var failedCredit = '<%: Html.TranslateTag("Retail/CreditNotificationSettings|Failed to save credit settings","Failed to save credit settings")%>';


        $('#submitCreditSettings').click(function () {
            var creditform = $('#CreditPreferenceSettingsForm').serialize();;

            $.post("/Retail/CreditSetting/<%:Model.CreditSettingID%>", creditform, function (data) {
                if (data == "Success") {
                    window.location.href = "/Retail/NotificationCredit/<%=Model.AccountID%>";
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to save credit settings")%>");
                }
            });
        });


    });
</script>


