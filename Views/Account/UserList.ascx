<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Account>" %>

<div class="formtitle">
    Users

    <%if ((Model.CurrentSubscription.Can(Feature.Find("muliple_users")) && MonnitSession.CustomerCan("Customer_Create")) || MonnitSession.CustomerCan("Support_Advanced"))
        {  %>

    <%if (Model.CurrentSubscription.Can(Feature.Find("link_users")) || MonnitSession.CustomerCan("Support_Advanced"))
        { %>
    <%-- >--%> <a href="/Customer/AddExistingUser/<%:Model.AccountID %>" class="bluebutton" id="AddExisting" style="margin: -3px 20px 0px 0px;">Link Existing User</a>
    <%} %>
    <a href="/Customer/Create/<%:Model.AccountID %>" class="bluebutton" id="newUser" style="margin: -3px 20px 0px 0px;">Add New User</a>
    <%} %>
</div>
<%-- for customers that are not part of this account we need a way to tag them so that everyone know's they are not the usual users
    of the account that these are special users added to the account by the admin of the account.  
--%>
<table border="0" style="width: 100%;">
    <tr>
        <th style="width: 20px;"></th>
        <th>
            <a href="/Account/UserList/<%:Model.AccountID %>?OrderBy=LastName" class="sort">Last Name</a>
        </th>
        <th>
            <a href="/Account/UserList/<%:Model.AccountID %>?OrderBy=FirstName" class="sort">First Name</a>
        </th>
        <th>
            <a href="/Account/UserList/<%:Model.AccountID %>?OrderBy=Email" class="sort">Notification</a>
        </th>
        <th>
            <a href="/Account/UserList/<%:Model.AccountID %>?OrderBy=Email" class="sort">Admin</a>
        </th>
        <th colspan="3"></th>
        <th style="width: 20px;"></th>
    </tr>
    <% 
        IEnumerable<Customer> Customers = Customer.LoadAllByAccount(Model.AccountID);
        string orderby = Request.QueryString["OrderBy"];
        switch (orderby)
        {
            case "FirstName":
                Customers = Customers.OrderBy((cust) => { return cust.FirstName; });
                break;
            case "Email":
                Customers = Customers.OrderBy((cust) => { return cust.NotificationEmail; });
                break;
            case "Admin":
                Customers = Customers.OrderBy((cust) => { return cust.IsAdmin.ToInt(); });
                break;
            case "LastName":
            default:
                Customers = Customers.OrderBy((cust) => { return cust.LastName; });
                break;
        }

        bool alt = true;
        foreach (var item in Customers)
        {
            alt = !alt;%>
    <tr class="<%: alt ? "alt" : "" %> viewUserDetails <%:item.CustomerID %>">
        <td></td>
        <td title="<%=item.IsActive? "User Name: " + item.UserName : "Multiple users only available with Premiere Subscription" %>">
            <h4>
                <%if (!item.IsActive)
                    {%>
            (Inactive)
            <%}%>
                <%=item.LastName%>
            </h4>
        </td>
        <td title="<%=item.IsActive? "User Name: " + item.UserName : "Multiple users only available with Premiere Subscription" %>">
            <h4>

                <%=item.FirstName%> 
            </h4>
        </td>
        <td>Email: <a href="mailto:<%:item.NotificationEmail%>"><%:item.NotificationEmail%></a>
            <%if (item.SendSensorNotificationToText && !string.IsNullOrEmpty(item.NotificationPhone))
                { %>
            <br />
            SMS: <%:item.NotificationPhone%>
            <%if (!item.DirectSMS)
                { %>
                (<%:item.SMSCarrier == null ? "Unknown" : item.SMSCarrier.SMSCarrierName%>)
                <%}%>
            <%}%>
            <%if (item.SendSensorNotificationToVoice && !string.IsNullOrEmpty(item.NotificationPhone2))
                { %>
            <br />
            Voice: <%:item.NotificationPhone2%>
            <%}%>
        </td>
        <td>
            <%if (item.IsAdmin && item.AccountID == Model.AccountID)
                { %>
            <img src="<%:Html.GetThemedContent("/images/good.png")%>" alt="Administrator" />
            <%}%>
                  
        </td>
        <td style="text-align: right;">
            <%
                CustomerAccountLink cal = null;
                if (item.IsActive && MonnitSession.CustomerCan("Proxy_Login") && item.AccountID == Model.AccountID)
                {%>
            <a class="stopProp" href="/Account/ProxyCustomer/<%:item.CustomerID%>" onclick="proxyCustomer(this); return false;">
                <img src="<%:Html.GetThemedContent("/images/proxy.png")%>" alt="Proxy Login" title="Proxy Login" /></a>
            <%}
                else
                {
                    cal = CustomerAccountLink.Load(item.CustomerID, Model.AccountID);
                    if (cal != null)
                    {
                        if (!cal.RequestConfirmed)
                        {%>
            <img title="Pending User Account Link" style="width: 30px" src="<%:Html.GetThemedContent("/images/Prelink.png")%>" alt="User has not registered account" />
            <a class="sendInvite" data-accountid="<%: Model.AccountID %>" data-invitorid="<%: MonnitSession.CurrentCustomer.CustomerID %>" data-customerid="<%: item.CustomerID %>" href="/Customer/SendEmailInvitation/">
                <img title="Send Email Invitation" style="width: 30px" src="<%:Html.GetThemedContent("/images/invite-2.png")%>" alt="Send Email Invitation" /></a>
            <label class="emailsent"></label>
            <%}
                else if ((cal.CustomerDeleted == null || !cal.CustomerDeleted))
                {%>
            <img class="stopProp" title="User Is Linked To Account" style="width: 30px" src="<%:Html.GetThemedContent("/images/Linked.png")%>" alt="User Is Linked To Account" />
            <%}
                else
                {%>
            <a class="sendInvite" data-accountid="<%: Model.AccountID %>" data-customerid="<%: item.CustomerID %>" href="/Customer/ReAddExistingUser?accountID=<%=cal.AccountID %>&customerID=<%=item.CustomerID %>">
                <img title="User Account is Unlinked -Click to send Relink Invitation" style="width: 30px" src="<%:Html.GetThemedContent("/images/Unlinked-2.png")%>" alt="User Account is Unlinked - Click to send Relink Invitation" /></a>
            <label class="emailsent"></label>
            <%}
                    }
                }%>
        </td>
        <td style="text-align: right;">
            <%if (MonnitSession.CustomerCan("Unlock_User"))
                {%>
            <%if (item.isLocked())
                {%>
            <a class="LockedUser" href="/Account/Unlock?customerid=<%:item.CustomerID %>&guid=<%:item.GUID %>" onclick="unlockUser()">
                <img src="<%:Html.GetThemedContent("/images/Locked.png")%>" alt="User is locked." title="Locked User." style="height: 25px; width: auto;" /></a>
            <%}
                else
                {%>
            <img src="<%:Html.GetThemedContent("/images/Unlocked.png")%>" alt="User is unlocked." title="Unlocked User." style="height: 25px; width: auto;" />
            <%    }
                }%>
        </td>
        <td style="text-align: right;">
            <%if ((MonnitSession.CustomerCan("Customer_Delete") && MonnitSession.CurrentCustomer.Account.PrimaryContactID != item.CustomerID) || (item.CustomerID == MonnitSession.CurrentCustomer.CustomerID && item.CustomerID != MonnitSession.CurrentCustomer.Account.PrimaryContactID))
                {
                    if (cal == null)
                    { %>
            <a class="stopProp" href="/Customer/Delete?id=<%:item.CustomerID %>&accountID=<%: Model.AccountID %>" title="Delete <%: item.UserName %>" onclick="newModal($(this).attr('title'),$(this).attr('href'), 310, 600); return false;">
                <img src="/Content/images/delete.png" alt="Delete" /></a>
            <%}
                else
                {%>
            <a class="calRemove" href="/Customer/RemoveCustomerLink?id=<%:item.CustomerID %>&accountID=<%: Model.AccountID %>" title="Delete <%: item.UserName %>">
                <img style="width: 30px;" src="/Content/images/Notification/Remove.png" alt="Delete" /></a>
            <%}
                }%>
        </td>
        <td></td>
    </tr>

    <!-- Detail Row Insert -->
    <tr class="<%: alt ? "alt" : "" %> holdUserDetails" style="display: none; border-top-width: 0px;" data-userid="<%:item.CustomerID %>">
        <td colspan="<%:MonnitSession.CurrentCustomer.IsAdmin ? "12" : "10"%>" style="padding: 0px 30px 30px 30px">
            <div id="loadingGIF" class="text-center" style="display: none;">
                <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                    <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                </div>
            </div>
        </td>
    </tr>
    <!-- Detail Row End -->

    <%}%>
</table>

<div class="buttons">
    <%:Html.Partial("AccountButtons") %>
    <div style="clear: both;"></div>
</div>

<script>
    $(function () {
        $('#newUser').click(function (e) {
            e.preventDefault()

            ajaxDiv('userList', $(this).attr("href"));
        });

        $('.sort').click(function (e) {
            e.preventDefault()

            ajaxDiv('userList', $(this).attr("href"));
        }).css('font-weight', 'bold').css('text-decoration', 'none');

        $('.viewUserDetails').click(function () {
            var tr = $(this).next();
            var hide = tr.is(":visible");

            $('.viewUserDetails').css('border-bottom-width', '1px');
            $('.holdUserDetails').hide().children().empty().html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

            if (!hide) {
                $(this).css('border-bottom-width', '0px');
                tr.show();

                $.get("/Customer/Data?id=" + tr.data('userid') + "&accountID=<%: Model.AccountID%>&tab=" + tabToShow, function (data) {
                    tr.children().html(data);
                });
            }

            tabToShow = "";
        }).css('cursor', 'pointer');

        if ('<%:Request["user"].ToStringSafe()%>'.length > 0) {
            tabToShow = '<%:Request["tab"].ToStringSafe()%>';
            $('.viewUserDetails.<%:Request["user"].ToStringSafe()%>').click();

            setTimeout("$(window).scrollTop($('.viewUserDetails.<%:Request["user"].ToStringSafe()%>').offset().top);", 1000);
        }

        $('.stopProp').click(function (e) {
            e.stopPropagation();
        });

        $('.calRemove').click(function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            if (confirm("Are you sure you want to remove user for the account")) {
                $.post(url, function (data) {

                    if (data == "Success")
                        window.location.href = window.location.href;
                });
            }
        });

        $('.sendInvite').click(function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            var anchor = $(this);
            var url = anchor.attr('href');
            var AcctID = anchor.data("accountid");
            var custid = anchor.data("customerid");
            var sendingid = anchor.data("invitorid");

            if (sendingid != null) {
                $.post(url, { accountID: AcctID, customerID: custid, fromID: sendingid }, function (data) {

                    $('.emailsent').empty();

                    if (data == 'Success')
                        $("#emailsent").append("Successfully sent email.");
                    else
                        $("#emailsent").append("Failed to send email.");
                });
            }
            else {
                $.post(url, { accountID: AcctID, customerID: custid }, function (data) {

                    $('.emailsent').empty();

                    if (data == 'Success')
                        $("#emailsent").append("Successfully sent email.");
                    else
                        $("#emailsent").append("Failed to send email.");
                });
            }
        });
    });

    var tabToShow = '';
</script>
