<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PurchaseConfirmationModel>" %>

<div class="row" style="margin: 0px 0px 15px 5px;">
    <table class="table table-hover">
        <thead>
            <tr style="background-color: #e3e3e3;">
                <th></th>
                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Used","Used")%></th>
                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Retail/NotificationCredit|Remaining","Remaining")%></th>
                <th style="color: black; padding: 6px;"><%: Html.TranslateTag("Expiration","Expiration")%></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <%  int Total = 0;
                List<NotificationCredit> credits = NotificationCredit.LoadAvailable(Model.Account.AccountID);
                foreach (NotificationCredit creditgroup in credits)
                {
                    Total += creditgroup.RemainingCredits;
                    %>
                <tr id="notification_<%:creditgroup.NotificationCreditID %>">
                    <th style="color: black; padding: 6px;"><%:creditgroup.NotificationCreditType.Name %></th>
                    <td style="padding: 6px;"><%:creditgroup.UsedCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.RemainingCredits %></td>
                    <td style="padding: 6px;"><%:creditgroup.ExpirationDate > DateTime.UtcNow.AddYears(10) ? "N/A" : creditgroup.ExpirationDate.ToShortDateString() %></td>
                    <%--<td style="padding: 0px; padding-top: 6px;">
                        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin) {%>
                            <a title="<%: Html.TranslateTag("Retail/NotificationCredit|Remove Credits","Remove Credits")%>" href="/Notification/RemoveCredits/<%:creditgroup.NotificationCreditID %>" onclick="removeCredits(this); return false;">
                                <%=Html.GetThemedSVG("delete") %>
                            </a>
                        <%} %>
                    </td>--%>
                </tr>
                <%} %>
            <tr style="background-color: #e3e3e3;">
                <th style="color: black;"><%: Html.TranslateTag("Retail/NotificationCredit|Total","Total")%></th>
                <th style="color: black; padding: 6px;"></th>
                <th style="font-weight: 500; padding: 6px;"><%: Total %></th>
                <th style="color: black; padding: 6px;"></th>
                <th></th>
            </tr>
        </tbody>
    </table>
</div>
<% 
    if (!string.IsNullOrWhiteSpace(Model.PurchasedItemIDs))
    {%>
        <script>
            $(function () {
                var items = '<%:Model.PurchasedItemIDs%>';
                var itemArray = items.split('|');

                for (var i in itemArray) {
                    var itemID = itemArray[i];
                    $('#notification_' + itemID).css('border-style', 'solid').addClass("border-3 border-success");
                }
            });
        </script>
    <%}
%>