<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PurchaseConfirmationModel>" %>


<div class="form-group sub-list__container hasScroll-sm">
    <% List<AccountSubscription> subList = AccountSubscription.LoadByAccountID(Model.Account.AccountID);
        AccountSubscription subscription = subList != null && subList.Count > 0 ? subList.Where(x => x.AccountSubscriptionID == Model.PurchasedItemIDs.ToLong()).FirstOrDefault() : null;
        if (subscription == null)
        {
            subscription = subList != null && subList.Count > 0 ? subList.OrderByDescending(x => x.ExpirationDate).FirstOrDefault() : null;
        }
        if (subscription != null && subscription.AccountSubscriptionTypeID != AccountSubscriptionType.BasicID)
        {%>
            <div class="col-sm-10 col-12 dfac pd5" id="premiere_<%:subscription.AccountSubscriptionID %>">
                <span style="font-size: 14px; display: flex; align-items: center;">
                    <svg xmlns="http://www.w3.org/2000/svg" width="7" height="7" viewBox="0 0 10 10">
                        <path id="ic_lens_24px" d="M7,2a5,5,0,1,0,5,5A5,5,0,0,0,7,2Z" transform="translate(-2 -2)" fill="#21ce99" />
                    </svg>
                    &nbsp;&nbsp;
                    <%: subscription.AccountSubscriptionType.Name%>
                </span>
                &nbsp;&nbsp;&nbsp; 
                <span style="color: #B4B4B4; font-size: 11px;">
                    Expires: <%= subscription.ExpirationDate.OVToLocalDateTimeShort() %>
                </span>
            </div>
            <br />
        <%} %>
    <div class="clearfix"></div>
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
                    $('#premiere_' + itemID).css('border-style', 'solid').addClass("border-3 border-success");
                }
            });
        </script>
    <%}
%>