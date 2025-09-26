<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.CustomerAccountLink>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountLinkList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <% 

        Customer cust = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
    %>

    <%:Html.Partial("~/Views/Settings/AccountLink.ascx",cust.Account) %>

    <br>
    <div class="card_container col-lg-12 col-md-12 col-xs-12">
        <div class="card_container__top" style="padding-left: 5px;">
            <div class="card_container__top__title"><i style="margin-bottom: 3px;" class="fa fa-list-ul card_container__icon-color-1"></i>&nbsp;<%: Html.TranslateTag("Linked Accounts","Linked Accounts")%></div>
            <div class="nav navbar-right panel_toolbox">
            </div>

            <div class="clearfix"></div>
            
        </div>
        <br />
        <!-- User List View -->
        <div class="col-lg-12 col-md-12 col-xs-12">
            <div class="glanceView">
                <div  id="UserList">

                    <%            foreach (var item in Model)
                        {
                            if (!item.CustomerDeleted)
                            {

                                bool canLink = MonnitSession.AccountCan("link_users");
                    %>
                   
                        <div class="col-lg-12 col-md-12 col-xs-12" style="font-size: 1.4em;">

                            <div class="col-lg-3 col-md-3 col-xs-12 bold" style="font-size: 0.8em;">
                                <% string name = "";
                                    Account account = Account.Load(item.AccountID);
                                    if (account == null)
                                    {
                                        new Exception("Error on linked account page look at account on  CustAccLInk :" + item.CustomerAccountLinkID).Log();
                                    }
                                    else
                                    {
                                        name = account.CompanyName;
                                    }%>
                                <%:  System.Web.HttpUtility.HtmlDecode(name) %>
                                <% if (account != null && !account.IsPremium)
                                    {%>

                                <br />
                                <span style="color: red; font-size: 0.7em;"><%: Html.TranslateTag("Settings/AccountLinkList|Premiere Subscription Expired","Premiere Subscription Expired") %></span>
                                <%}%>
                            </div>
                            <div class=" col-lg-3 col-md-3 col-xs-3" style="font-size: 0.8em; width:">
                                <%if (!item.RequestConfirmed && canLink)
                                    { %>
                                <span style="padding-right: 10px;">
                                    
                                </span>
                                <a style="margin-right: 10px;" title="<%: Html.TranslateTag("Settings/AccountLinkList|Accept Invitation","Accept Invitation") %>" class="goto" href="/Account/RegisterToAccount?customerID=<%: item.CustomerID %>&guid=<%: item.GUID %>">
                                    <span style="font-size: 1.0em;"> <%: Html.TranslateTag("Settings/_AccountLinkList|Accept Link","Accept Link")%></span>
                                </a>|  &nbsp;
                                <a title="<%: Html.TranslateTag("Settings/AccountLinkList|Decline Invitation","Decline Invitation") %>" class="goto" href="/Account/DeclineAccountLink?customerID=<%: item.CustomerID %>&guid=<%: item.GUID %>">
                                  <span style="font-size: 1.0em;"> <%: Html.TranslateTag("Settings/_AccountLinkList|Decline Link","Decline Link")%></span>
                                </a>
                                <% }
                                    else if (!item.AccountDeleted && canLink)
                                    { %>
                             &nbsp;&nbsp;&nbsp;<span style="font-size: 1.0em;" title="<%: Html.TranslateTag("Settings/AccountLinkList|Account Linked","Account Linked") %>"><%=Html.GetThemedSVG("link") %></span><br />
                                <%: Html.TranslateTag("Linked","Linked")%>
                                <%}
                                    else
                                    {%>
                             &nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size: smaller" title="<%: Html.TranslateTag("Settings/AccountLinkList|Account is Not Linked","Account is Not Linked") %>" ><%=Html.GetThemedSVG("unlink") %></span><br />
                                <%: Html.TranslateTag("Settings/_AccountLinkList|Un-Linked","Un-Linked")%>
                                <% } %>
                            </div>

                            <div class="col-lg-3 col-md-3 col-xs-3" style="font-size: 0.8em; ">
                                <%if (!item.RequestConfirmed && canLink)
                                    { %>
                            &nbsp;
                              <%}
                                  else if (item.RequestConfirmed && !item.AccountDeleted && MonnitSession.CurrentCustomer.AccountID != item.AccountID && canLink)
                                  { %>
                                 &nbsp;<a class="goto" title="<%: Html.TranslateTag("Settings/AccountLinkList|View Linked Account","View Linked Account") %>" href="/Account/ProxyCustomerAccountLink?accountID=<%: item.AccountID %>">
                                     <%=Html.GetThemedSVG("login") %>
                                     <br />
                                     &nbsp;<%: Html.TranslateTag("View","View")%>
                                 </a>

                                <%}
                                    else
                                    { %>
                            &nbsp;
                            <%} %>
                            </div>
                            <div class="col-lg-3 col-md-3 col-xs-3" style="font-size: 0.8em;">
                                <%if (!item.RequestConfirmed && canLink)
                                    { %>
                            &nbsp;
                           <%}
                               else
                               {
                                   if (!item.CustomerDeleted && MonnitSession.CurrentCustomer.AccountID != item.AccountID && canLink)
                                   {%>
                                <a class="Deleted" title="<%: Html.TranslateTag("Settings/AccountLinkList|Remove Account Link","Remove Account Link") %>" style="width: 107px;" data-custid="<%: item.CustomerID%>" data-acctid="<%: item.AccountID%>" href="#">&nbsp;&nbsp;&nbsp;&nbsp;<%=Html.GetThemedSVG("delete") %>
                                    <br />
                                    <%: Html.TranslateTag("Remove","Remove")%></a>
                                <%}
                                    else
                                    {%>
                            &nbsp;
                              <%}
                                  }%>
                            </div>

                        </div>
                        <div class="clearfix"></div>

                        <%}%>
                    <hr />
                            <%} %>
                    

                </div>
            </div>

        </div>
    </div>

    <script>
        $(function () {


            var failAccept = "<%: Html.TranslateTag("Settings/AccountLinkList|Failed To Accept Invitation","Failed To Accept Invitation") %>";
            var failDecline = "<%: Html.TranslateTag("Settings/AccountLinkList|Failed To Decline Invitation","Failed To Decline Invitation") %>";
            var failtogo = "<%: Html.TranslateTag("Settings/AccountLinkList|Failed to go to account","Failed to go to account") %>";
            var delFail = "<%: Html.TranslateTag("Settings/AccountLinkList|Delete Failed","Delete Failed") %>";
            var aresure = "<%: Html.TranslateTag("Settings/AccountLinkList|Are you sure you want to remove this link?","Are you sure you want to remove this link?") %>";


            var listCount = <%: Model.Count%>;

            if (listCount == 0)
                location.href = "/";

            $('.regaccept').click(function (e) {

                e.preventDefault();
                var href = $(this).attr('href');

                $.post(href, function (data) {
                    if (data == "Success")
                        location.href = "/Overview";
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to Accept Invitation")%>");
                });
            });

            $('.regdecline').click(function (e) {

                e.preventDefault();
                var href = $(this).attr('href');

                $.get(href, function (data) {
                    if (data == "Success")
                        location.reload();
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to Decline Invitation")%>");
                });
            });


            $('.goto').click(function (e) {
                e.preventDefault();
                var href = $(this).attr('href');

                $.post(href, function (data) {
                    if (data == "Success")
                        location.href = "/Overview";
                    else
                        showSimpleMessageModal(data);
                });
            });

            $('.Deleted').click(function (e) {
                e.preventDefault();
                var id = $(this).data('custid');
                var acctid = $(this).data('acctid');
                if (confirm(aresure)) {
                    $.post("/Account/DeleteAccountLink?customerID=" + id + "&accountID=" + acctid, function (data) {
                        if (data == "Success") {
                            if (listCount - 1 < 1)
                                location.href = "/Overview";
                            else
                                location.reload();
                        }
                        else {
                            showSimpleMessageModal("<%=Html.TranslateTag("Delete Failed")%>");
                        }
                    });
                }
            });
        });
    </script>



</asp:Content>
