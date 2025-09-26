<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.AccountSearchModel>>" %>


<% foreach (var item in Model)
    {%>
<div class="col-12 d-flex align-items-center" id="listrow_<%: item.AccountID%>" style="max-width: 100%;">

    <%Account acc = Account.Load(item.AccountID); %>

    <div title="Company : <%=item.CompanyName %>&#13;Account ID: <%=item.AccountID %>&#13;Account Number: <%=item.AccountNumber %>" class="col-4" style="overflow: hidden;">
        <%=item.CompanyName %>
        <%if (item.AccountTree.Count() > 0)
            {
                bool showParentLine = false;
                string parentLineNames = "";
                for (int i = 1; i < item.AccountTree.Length - 1; i++) // Starting at i=1 instead of 0, to skip iterating over the first entry which is Monnit Corp - AccountID "1"
                {
                    if (!showParentLine)
                        showParentLine = true;

                    string account = HttpUtility.HtmlDecode(item.AccountTree[i]);
                    parentLineNames += account + (i != item.AccountTree.Length - 2 ? " | " : "");
                }

                if (showParentLine)
                {%>
        <br />
        &nbsp;&nbsp;
                <%=parentLineNames %>
        <%}
            }
            else
            {%>
		    N/A
        <%} %>
    </div>
    <div class="col-md-2 d-none d-md-block">
        <!-- Subscription Type -->
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
            { %>
        <a href="/Settings/AdminSubscriptionDetails/<%=item.AccountID %>"><span title="Edit Subscription" style="text-align: center;" class="accountSub"><%=Html.GetThemedSVG("edit") %></span></a>&nbsp;<%} %>
        <%: item.AccountSubscriptionType %><br />
    </div>
    <div class="col-md-2 d-none d-md-block">
        <!-- Expiration Details -->
        <div class="fa fa-circle" id="expirationDate_<%=item.AccountID%>" style="color: <%= item.SubscriptionExpiration > DateTime.UtcNow ? "#39e654" : "#e63939" %>"></div>
        &nbsp;&nbsp;<%= item.SubscriptionExpiration.OVToLocalDateShort() %>
    </div>


    <div class="col-md-2 d-none d-md-block">
        <label class="col-12">
            <a href="javascript:void(0);" class="toggleAutobill link-style" id="toggleAutobill_<%:item.AccountID %>"
                data-account-id="<%:item.AccountID %>">
                <%: acc.AutoBill < 1 ? "Enable" : "Disable AutoBill For:  " + MonnitSession.CurrentCustomer.Account.AccountNumber %>
            </a>
        </label>
    </div>

</div>
<hr />
<%}%>
<style>
    .accountSub #svg_edit {
        height: 15px;
        width: 15px;
    }

    .link-style {
        color: blue;
        cursor: pointer;
    }
</style>

<script>

    $(document).ready(function () {
        $('.toggleAutobill').on('click', function () {
            var $this = $(this);
            var accountId = $this.data('account-id');
            var isEnabled = $this.text() === 'Enabled';

            $this.text(isEnabled ? 'Disabled' : 'Enabled');

            let url = `/Settings/ToggleAutoBill/${accountId}`;

            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data === 'Success') {
                        toastBuilder("Enabled", "Success");
                    } else if (data === 'Disabled') {
                        $this.text('Disabled');
                        toastBuilder(data);
                    } else {
                        showSimpleMessageModal(data);
                        $this.text(isEnabled ? 'Enabled' : 'Disabled');
                    }
                },
                error: function (xhr, status, error) {
                    showSimpleMessageModal('Request failed: ' + error);
                    $this.text(isEnabled ? 'Enabled' : 'Disabled');
                }
            });
        });
    });


</script>
