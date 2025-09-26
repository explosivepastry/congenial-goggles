<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%=""%>


<% 
    bool isProxied = MonnitSession.UserIsCustomerProxied;
    bool isUserProxied = MonnitSession.OldCustomer != null && MonnitSession.CurrentCustomer.CustomerID != MonnitSession.OldCustomer.CustomerID;
    bool isSubAccountProxied = MonnitSession.OldCustomer != null && MonnitSession.OldCustomer.AccountID != MonnitSession.CurrentCustomer.AccountID;
    bool isAccLinked = MonnitSession.UserIsAccountProxied;


    if (isProxied)
    {
        if (MonnitSession.CurrentCustomer.Can("Account_View"))
        {
            Tuple<bool, Stack<AccountLocationListModel>> tuple = null; // = AccountLocationListModel.LocationList(MonnitSession.CurrentCustomer.AccountID, MonnitSession.OldCustomer.CustomerID);
            if (isUserProxied)
            {
                tuple = AccountLocationListModel.LocationList(MonnitSession.CurrentCustomer.AccountID, MonnitSession.CurrentCustomer.CustomerID);
            }
            else if (isSubAccountProxied)
            {
                tuple = AccountLocationListModel.LocationList(MonnitSession.CurrentCustomer.AccountID, MonnitSession.OldCustomer.CustomerID);
            }

            if (tuple == null || tuple.Item2.Count <= 1)
            {
                return;
            }

            bool lastIsLeaf = tuple.Item1;
            Stack<AccountLocationListModel> locationStack = tuple.Item2;

            //if (MonnitSession.OldCustomer != null)
            //{
            //    if (locationStack.Where(m => { return m.AccountID == MonnitSession.OldCustomer.AccountID; }).Count() == 0)
            //    {
            //        locationStack.Clear();
            //         locationStack.Push(new AccountLocationListModel() { AccountID = MonnitSession.CurrentCustomer.AccountID, AccountNumber = MonnitSession.CurrentCustomer.Account.AccountNumber, RowID = 1 });
            //        locationStack.Push(new AccountLocationListModel() { AccountID = MonnitSession.OldCustomer.AccountID, AccountNumber = MonnitSession.OldCustomer.Account.AccountNumber, RowID = 2 });


            //    }
            //}

%>
<div class="lbc-top-bar">
    <div class="lbc-shadow">
        <div class="location-icon">
            <%:Html.GetThemedSVG("location") %>
        </div>

        <%if (isAccLinked == true)
            {%>
        <div class=" locate-breadcrumbs">

            <div class="locate-name locate-proxy" data-accountid="<%=MonnitSession.OldCustomer.AccountID %>" title="<%: Html.TranslateTag("Shared/LocationsBar|Go Home", "Go Home")%>">
                <%=MonnitSession.OldCustomer.Account.AccountNumber %>
            </div>
            <div class="lbc-arrow">
                <svg class="arrow-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512" style="fill: var(--primary-color)">
                    <path d="M246.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L178.7 256 41.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z" />
                </svg>
            </div>

            <div class="locate-name locate-proxy" data-accountid="<%=MonnitSession.CurrentCustomer.AccountID %>" data-accnumb="<%=MonnitSession.CurrentCustomer.AccountID %>" title="<%: Html.TranslateTag("Shared/LocationsBar|View Account", "View Account")%>:    <%=MonnitSession.CurrentCustomer.Account.AccountNumber %> ">
                <%=MonnitSession.CurrentCustomer.Account.AccountNumber %>
            </div>
        </div>
        <div id="crumb" class="crumb-end"></div>

        <%}
            else
            {%>

        <div class=" locate-breadcrumbs">
            <%
                AccountLocationListModel location = locationStack.Pop();
            %>

            <div class="locate-name locate-proxy" data-accountid="<%=location.AccountID %>" title="<%: Html.TranslateTag("Shared/LocationsBar|Go Home", "Go Home")%>">
                <%=location.AccountNumber %>
            </div>

            <%


                while (locationStack.Count > 0)
                {
                    location = locationStack.Pop();

            %>
            <%--                  <%
                if (locationStack.Count != 0)
                {
                    break;
                }
            %>--%>

            <div class="lbc-arrow">
                <svg class="arrow-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512" style="fill: var(--primary-color)">
                    <path d="M246.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L178.7 256 41.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z" />
                </svg>
            </div>

            <div class="locate-name locate-proxy" data-accountid="<%=location.AccountID %>" data-accnumb="<%=location.AccountID %>" title="<%: Html.TranslateTag("Shared/LocationsBar|View Account", "View Account")%>:    <%=location.AccountNumber %> ">
                <%=location.AccountNumber %>
            </div>

            <%
                }
            %>
        </div>

        <%--<%: subAccounts == 0 ? "end" : "continue"%>--%>
        <div id="crumb" class="crumb-<%: lastIsLeaf ? "end" : "continue"%>"></div>
        <% }%>
    </div>
</div>

<script>


    var currentPageURL = window.location.href;
    if (currentPageURL.includes('/Settings/LocationOverview/<%:MonnitSession.CurrentCustomer.AccountID %>')) {
        $('.add-Locations-btn').show();

    }



    <%= ExtensionMethods.LabelPartialIfDebug("LocationsBar.ascx")  %>




    function truncateBreadName() {
        var accId = document.querySelectorAll("[data-accnumb]");
        Array.from(accId).forEach(function (a) {
            var name = a.innerHTML.trim();
            if (name.length > 20) {
                a.textContent = name.slice(0, 20) + "...";
            }

        });
    }

    window.onload = function () {
        truncateBreadName();
    };




    //function viewLocationQuick(lnk) {
    //    var anchor = $(lnk);
    //    var acctID = anchor.data('accountid');
    //    var href = anchor.attr('href');
    //    $.post(href, { id: acctID }, function (data) {
    //        if (data == "Success")
    //            window.location.href = "/Settings/LocationOverview/" + acctID;
    //        else
    //            /*$('#proxyMessage_' + acctID).html('Proxy Failed');*/
    //            showSimpleMessageModal(data, true);

    //    });

    $('.locate-home').click(
        function (e) {
            //e.preventDefault();
            //e.stopPropagation();

            acctID = $(this).data('accountid');

            unProxy()
                .then(
                    function (data) {
                        if (data == "Success") {
                            window.location.href = "/Settings/LocationOverview/" + acctID;
                        } else {
                            proxyError(data);
                        }
                    }
                );

            //$.post("Account/UnProxyAndRedirect",
            //    { redirectUrl: "/Settings/LocationOverview/" + $(this).data('accountid') },
            //    function () {
            //        window.
            //    }
        }
    )

    $('.locate-proxy').click(
        function (e) {
            //e.preventDefault();
            //e.stopPropagation();

            var anchor = $(this);
            var newAcctID = anchor.data('accountid');
            var isUserProxied = <%= isUserProxied.ToString().ToLower() %>;

            // UnProxy will undo both a Subaccount proxy and a User proxy
            var xhr = unProxy();

            if (isUserProxied) {
                // if they were User proxied, re-do the User proxy before attempting the new Subaccount proxy
                xhr = xhr.then(proxyUser);
            }

            xhr.then(
                function (result) {
                    proxyAccount(result, newAcctID);
                }
            );
        }
    );

    function unProxy() {
        return $.post(
            "/Account/UnProxy/",
            function (data) {
                if (data != "Success") {
                    proxyError(data);
                }
            }
        );
    }

    function proxyUser(result) {

        if (result == "Success") {
            return $.post(
                "/Account/ProxyCustomer/",
                { id: <%= MonnitSession.CurrentCustomer.CustomerID  %>},
                function (data) {
                    if (data != "Success") {
                        proxyError(data);
                    }
                }
            );
        }
    }

    function proxyAccount(result, newAcctID) {
        if (result == "Success") {
            $.post(
                "/Account/ProxySubAccount",
                { id: newAcctID },
                function (data) {

                    if (data == "Success") {
                        window.location.href = "/Settings/LocationOverview/" + newAcctID;
                    }
                    else {
                        proxyError(data);
                    }
                }
            );
        }
    }

    function proxyError(message, timeout = 5000) {
        //showSimpleMessageModal(message, true);
        toastBuilder(message);
        setTimeout(() => { window.location.href = "/Overview/Index"; }, timeout);
    }

</script>


<%  }
    else
    {
%>
<div class="lbc-top-bar">
    <div class="lbc-shadow">
        <div class="location-icon">
            <%:Html.GetThemedSVG("location") %>
        </div>

        <div class=" locate-breadcrumbs">

            <div id="locate-home" class="locate-name locate-proxy" data-accountid="<%=MonnitSession.CurrentCustomer.DefaultAccount.AccountID %>" title="<%: Html.TranslateTag("Shared/LocationsBar|Go Home", "Go Home")%>">
                <%=MonnitSession.CurrentCustomer.DefaultAccount.AccountNumber %>
            </div>
        </div>
        <div id="crumb" class="crumb-end"></div>
    </div>
</div>
<script>
    $('#locate-home').click(
        function (e) {
            //e.preventDefault();
            //e.stopPropagation();

            acctID = $(this).data('accountid');

            unProxy()
                .then(
                    function () {
                        window.location.href = "/Overview/Index/";
                    }
                );

            //$.post("Account/UnProxyAndRedirect",
            //    { redirectUrl: "/Settings/LocationOverview/" + $(this).data('accountid') },
            //    function () {
            //        window.
            //    }
        }
    )

    function unProxy() {
        return $.post(
            "/Account/UnProxy/",
            function (data) {
                if (data != "Success") {
                    showSimpleMessageModal(data);
                }
            }
        );
    }
</script>

<%
        }
    } // END: isProxied
%>







