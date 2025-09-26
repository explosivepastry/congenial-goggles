<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.Customer>>" %>

<%="" %>
<% 
    if (Model.Count < 1)
    {%>
<%: Html.TranslateTag("Settings/UserListDetails|No Users found for this account","No Users found for this account")%>.
<%}
    else
    {
        long accountID = ViewBag.AccountID;
        Account acc = Account.Load(accountID);

        foreach (Customer cust in Model.OrderBy(c => c.FullName))
        {
            bool islinked = cust.AccountID != accountID;
            string cursorStyle = islinked ? "context-menu" : "pointer";
            CustomerAccountLink cal = CustomerAccountLink.Load(cust.CustomerID, accountID);
            if (cal != null && cal.CustomerDeleted) continue;
%>

<div class="card-w-data">
    <div style="width: 100%;">
        <div class="viewSensorDetails eventsList__tr innerCard-holder m-1" style="height: 60px;">
            <div class="col-2" style="display: flex; justify-content: center; align-items: center; flex-direction: column;">
                <%if (cust.IsActive && MonnitSession.CustomerCan("Proxy_Login") && cust.AccountID == accountID)
                    {%>
                <a class="stopProp" href="/Account/ProxyCustomer/<%:cust.CustomerID%>" onclick="proxyCustomer(this,'/Overview'); return false;" style="display: flex; align-items: center; flex-direction: column;">
                    <%=Html.GetThemedSVG("profile") %>
                    <%: Html.TranslateTag("Settings/UserListDetails|Proxy","Proxy")%>
                </a>
                <%}
                    else
                    {

                        if (cal != null)
                        {
                            if (!cal.RequestConfirmed)
                            {
                %>

                <div style="display: flex; align-items: center; flex-direction: column;">
                    <i style="font-size: small; padding-left: 5px;" class="fa fa-share" title="<%: Html.TranslateTag("Settings/AccountUserList|Send Email Invitation","Send Email Invitation") %>"></i>
                    <%: Html.TranslateTag("Settings/UserListDetails|Pending","Pending")%>
                </div>
                <%}
                    else if ((cal.CustomerDeleted == null || !cal.CustomerDeleted))
                    {%>

                <div style="display: flex; align-items: center; flex-direction: column;">
                    <i class="fa fa-link" title="<%: Html.TranslateTag("Settings/AccountUserList|User Is Linked To Account","User Is Linked To Account") %>"></i>
                    <%: Html.TranslateTag("Settings/AccountUserList|Linked","Linked")%>
                </div>
                <%}
                    else
                    {
                %>
                <a style="display: flex; align-items: center; flex-direction: column;" class="sendInvite" data-accountid="<%: accountID %>" data-customerid="<%: cust.CustomerID %>" href="/Customer/ReAddExistingUser/">
                    <i style="font-size: small; padding-left: 5px;" class="fa fa-share" title="<%: Html.TranslateTag("Settings/AccountUserList|User Account is Unlinked -Click to send Relink Invitation","User Account is Unlinked -Click to send Relink Invitation") %>"></i>
                    <%: Html.TranslateTag("Settings/UserListDetails|Re-Link","Re-Link")%>
                </a>
                <label class="emailsent"></label>
                <%}
                        }
                    }%>
                <% if (cust.CustomerID == acc.PrimaryContact.CustomerID)
                    {%>
                <div title="Primary account contact" class="prime-user">
                    <%: Html.TranslateTag("Settings/UserListDetails|Primary","Primary")%>
                </div>
                <%} %>
                <% else if (cust.IsAdmin)
                    {%>
                <div title="Admin" class="admin-user">
                    <%: Html.TranslateTag("Admin")%>
                </div>
                <%} %>
            </div>
            <div class="card-text col-8" style="font-size: 1rem; color: #515356; height: 60px; cursor: <%: cursorStyle%>; text-align: center; display: flex; justify-content: center; align-items: center; flex-direction: column; word-break: break-all;" title="<%: Html.TranslateTag("User Name","User Name")%>: <%=cust.UserName %>" onclick="goToUsersPage(<%=cust.CustomerID %>,<%=islinked.ToString().ToLower() %>);">
                <div>
                    <%=cust.IsActive ? cust.FullName : Html.TranslateTag("Settings/UserListDetails|(Inactive)","(Inactive) ") + cust.FullName %>
                </div>
                <div class="extra" style="margin-top: 3px;">
                    <span class="grey-container" style="font-size: 15px; padding: 0px 6px;">
                        <%= cust.NotificationEmail%>
                    </span>
                </div>
            </div>

            <div class="col-2" style="font-size: smaller; padding-top: 5px; display: flex; justify-content: center; align-items: center;">
                <%if ((MonnitSession.CustomerCan("Customer_Delete") && MonnitSession.CurrentCustomer.Account.PrimaryContactID != cust.CustomerID) || (MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID && MonnitSession.CurrentCustomer.Account.PrimaryContactID != cust.CustomerID))
                    {
                        if (cal == null)
                        { %>
                <div class=" dfac " style="/*height: 100%; */ max-width: 42px;">
                    <div class="dropleft dfac " style="height: 100%;">

                        <div class="menu-hover menu-fav dropdown" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">


                            <div style="width: 100px">
                                <button type="button" data-bs-toggle="dropdown" aria-expanded="false" style="border: none; background: none;">
                                    <%=Html.GetThemedSVG("menu") %>
                                </button>

                                <ul class="dropdown-menu ddm" style="padding: 0;">
                                    <%if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID)
                                        { %><li>
                                            <a class="dropdown-item menu_dropdown_item"
                                                onclick="window.location.href='/Settings/UserDetail/<%=cust.CustomerID %>';">
                                                <span><%: Html.TranslateTag("Details", "Details")%></span>
                                                <%=Html.GetThemedSVG("accountDetails") %>
                                            </a>
                                        </li>
                                    <%}

                                        if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID)
                                        { %>
                                    <li>
                                        <a class="dropdown-item menu_dropdown_item"
                                            onclick="window.location.href='/Settings/UserPermission/<%=cust.CustomerID %>';">
                                            <span><%: Html.TranslateTag("Permissions","Permissions")%></span>
                                            <%=Html.GetThemedSVG("lock") %>
                                        </a>
                                    </li>
                                    <%}
                                        if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID)
                                        { %>
                                    <li>
                                        <a class="dropdown-item menu_dropdown_item"
                                            onclick="window.location.href='/Settings/UserPreference/<%=cust.CustomerID %>';">
                                            <span><%: Html.TranslateTag("UserPreference","Preferences")%></span>
                                            <%=Html.GetThemedSVG("preferences") %>
                            
                                        </a>
                                    </li>
                                    <% }

                                        if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID)
                                        { %>
                                    <li>
                                        <a class="dropdown-item menu_dropdown_item"
                                            onclick="window.location.href='/Settings/UserNotification/<%=cust.CustomerID %>';">
                                            <span><%: Html.TranslateTag("Notifications","Notifications")%></span>
                                            <%=Html.GetThemedSVG("notifications") %>
                            
                                        </a>
                                    </li>
                                    <% }

                                        if (MonnitSession.CustomerCan("Customer_Edit_Other") || (MonnitSession.CustomerCan("Customer_Edit_Self")) && MonnitSession.CurrentCustomer.CustomerID == cust.CustomerID)
                                        { %>
                                    <li>
                                        <a class="dropdown-item menu_dropdown_item" title="Delete <%:cust.FullName %>"
                                            onclick="removeCustomer(<%=cust.CustomerID %>,<%=accountID %>)">
                                            <span><%: Html.TranslateTag("Delete", "Delete")%></span>
                                            <%=Html.GetThemedSVG("delete") %>
                                        </a>
                                    </li>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <%}
                    else
                    {%>
                <a onclick="removeCustomerLink(<%=cust.CustomerID %>,<%=accountID %>);" style="cursor: pointer; font-size: 1.2em; padding-left: 0px; display: inline-flex; flex-direction: column; align-items: center;">
                    <%=Html.GetThemedSVG("unlink") %>
                    <%:Html.TranslateTag("Settings/UserListDetails|Unlink","Unlink")%> 
                </a>
                <%}
                    }%>
            </div>
        </div>
    </div>
</div>


<%}
    }%>
<script type="text/javascript">

    var sureDelete = "<%= Html.TranslateTag("Settings/UserListDetails|Are you sure you want to delete this customer? Deleting will remove customer from all notifications","Are you sure you want to delete this customer? Deleting will remove customer from all notifications")%>.";
    var sureUnlink = "<%= Html.TranslateTag("Settings/UserListDetails|Are you sure you want to un-link this customer? This will remove customer from linked accounts notifications.","Are you sure you want to un-link this customer? This will remove customer from linked accounts notifications")%>.";

    $(document).ready(function () {
        $('#filterdUsers').html(<%=ViewBag.UserCount%>);


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

                    if (data == 'Success')
                        showSimpleMessageModal("Successfully sent relink request.");
                    else
                        showSimpleMessageModal("Failed to send relink request.");
                });
            }
            else {
                $.post(url, { accountID: AcctID, customerID: custid }, function (data) {

                    if (data == 'Success')
                        showSimpleMessageModal("Successfully sent relink request.");
                    else
                        showSimpleMessageModal("Failed to send relink request.");
                });
            }
        });

    });

    function removeCustomer(recipientID, accID) {
        let values = {};
        values.url = `/Settings/UserDelete?id=${recipientID}&accountID=${accID}`;
        values.text = `${sureDelete}`;
        openConfirm(values);
    }

    function removeCustomerLink(recipientID, accID) {
        let values = {};
        values.url = `/Settings/UserRemoveLink?id=${recipientID}&accountID=${accID}`;
        values.text = `${sureUnlink}`;
        openConfirm(values);
    }

    function goToUsersPage(userID, isLinked) {

        if (!isLinked) {

            window.location.href = "/Settings/UserDetail/" + userID;
        }
    }

</script>

