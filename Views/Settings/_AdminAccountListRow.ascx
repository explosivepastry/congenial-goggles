<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.AccountSearchModel>>" %>


<% foreach (var item in Model)
    {%>
<div class="col-12 d-flex align-items-center" id="listrow_<%: item.AccountID%>" style="max-width:100%;">

    <div title="Company : <%=item.CompanyName %>&#13;Account ID: <%=item.AccountID %>&#13;Account Number: <%=item.AccountNumber %>" class="col-4" style="overflow: hidden;">
        <%=item.CompanyName %>
        <%if (item.AccountTree.Count() > 0)
          {
            bool showParentLine = false;
            string parentLineNames = "";
            for(int i = 1; i < item.AccountTree.Length - 1; i++) // Starting at i=1 instead of 0, to skip iterating over the first entry which is Monnit Corp - AccountID "1"
            {
                if (!showParentLine)
                    showParentLine = true;

                string account =  HttpUtility.HtmlDecode(item.AccountTree[i]);
                parentLineNames += account + (i != item.AccountTree.Length-2 ? " | " : "");
            }

            if (showParentLine)
            {%>
                <br />&nbsp;&nbsp;
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
        <a href="/Settings/AdminSubscriptionDetails/<%=item.AccountID %>"><span title="Edit Subscription" style=" text-align: center;" class="accountSub"><%=Html.GetThemedSVG("edit") %></span></a>&nbsp;<%} %>
        <%: item.AccountSubscriptionType %><br />
    </div>
    <div class="col-md-2 d-none d-md-block">
        <!-- Expiration Details -->
        <div class="fa fa-circle" id="expirationDate_<%=item.AccountID%>" style="color: <%= item.SubscriptionExpiration > DateTime.UtcNow ? "#39e654" : "#e63939" %>"></div>
        &nbsp;&nbsp;<%= item.SubscriptionExpiration.OVToLocalDateShort() %>
    </div>
    <div class="col-1">
        <%if (MonnitSession.CustomerCan("Account_View") || MonnitSession.CustomerCan("Navigation_View_Administration"))
            {
                if (MonnitSession.CustomerCan("Account_View"))
                { %>
        <a style="font-size: 1.5em; vertical-align: middle;" class="" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>">
            <%=Html.GetThemedSVG("login") %>
        </a>
        <%}
            else
            { %>
        <input style="width: 85px;" type="text" placeholder="Access Token" id="accessToken_<%=item.AccountID %>" />&nbsp;
        <a style="font-size: 1.5em; vertical-align: middle;" href="/Account/ProxySubAccount/" onclick="viewAccount(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>">
            <%=Html.GetThemedSVG("login") %>
        </a>
        <%} %>
        <span id="proxyMessage_<%=item.AccountID %>" class="bold" style="color: red;"></span>
        <%}%>
    </div>
    <div class="col-6 col-md-3 col-lg-2">
        <%= item.FirstName + " " + item.LastName%>
    </div>
    <div class="col-1 d-flex justify-content-end pe-2">

        <div class="AlignTop" style="display: flex; align-items: center;justify-content: end;">
            <%if (MonnitSession.CustomerCan("Proxy_Login"))%>
            <%{ %>
            <a href="/Account/ProxyCustomer/<%:item.CustomerID %>" onclick="proxyCustomer(this,'/Overview'); return false;">
                <%=Html.GetThemedSVG("accountDetails") %>
            </a>
            <%} %>
            <%else
                {%>

            <% } %>
            <div style="height: 100%; width: 20px;" class="df">
                <div style="padding: 5px 10px;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                    <%=Html.GetThemedSVG("menu") %>
                </div>
                <ul class="dropdown-menu shadow rounded accountListDropdown" style="padding: 0;">
                    <%if (MonnitSession.CustomerCan("Account_View"))
                        {     %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item" style="height: inherit; width: 100%; display: flex;" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account", "View Account")%>">View Account
                        <span>
                            <%=Html.GetThemedSVG("login") %>
                        </span>
                        </a>
                    </li>
                    <li>
                        <a class="dropdown-item menu_dropdown_item"
                            href="/Account/ProxySubAccount/" onclick="viewRulesQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts", "Account Alerts")%>">Rules
                        <span>
                            <%=Html.GetThemedSVG("actions") %>
                        </span>
                        </a>
                    </li>
                    <%} %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item"
                            href="/Settings/AccountEdit/<%=item.AccountID %>">Settings
                        <span>
                               <%=Html.GetThemedSVG("user-settings") %>

                        </span>
                        </a>
                    </li>
                    <li>
                        <a class="dropdown-item menu_dropdown_item"
                            href="/Settings/AccountUserList/<%=item.AccountID %>">Users
                        <span>
                            <%=Html.GetThemedSVG("recipients") %>
                        </span>
                        </a>
                    </li>
                    <%if (MonnitSession.CustomerCan("Account_View"))
                        {     %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item"
                            href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%>">Sensors
                        <span>
                            <%=Html.GetThemedSVG("sensor") %>
                        </span>
                        </a>

                    </li>
                    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                        { %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item"
                            href="/Settings/AdminSubscriptionDetails/<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions", "Account Subscriptions")%>">Subscriptions
                            <span>
                                <%=Html.GetThemedSVG("subscription") %>
                            </span>
                        </a>
                    </li>
                    <% }
                        } %>
                </ul>
            </div>
        </div>
        <!-- Proxy Login -->
    </div>

</div>
<hr />
<%}%>
<style>
    .accountSub #svg_edit {
        height:15px;
        width:15px;
    }

</style>
