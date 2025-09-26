<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.UserLookUpModel>>" %>

<%if (Model.Count == 0)
    { %>  
<%: Html.TranslateTag("Settings/UserLookup|No Users Found","No Users Found")%>
<%}
    else
    {
%>

<div class="card_container__top">
    <div class="card_container__top__title">
        <%:Html.TranslateTag("Users","Users")%>
    </div>
</div>

<div class="w-100" style="overflow-y: scroll;">
    <%foreach (UserLookUpModel item in Model)
        { %>

    <div class="long-det-card">
        <div style="padding: 5px;" class="d-flex align-items-center">
            <%if (MonnitSession.CustomerCan("Account_View"))
                {%>

            <div class="col-5">
                <div class="col-12">
                    <a style="cursor: pointer;" href="/Account/ProxySubAccount/" onclick="viewAccountList(this); return false;" data-accountid="<%=item.AccountID %>">
                        <div class="">
                            <span style="font-weight: bold; font-size: 1.2em"><%= item.FirstName + " " + item.LastName%></span><% if (!item.IsActive)
                                   { %><span style="font-weight: bold; font-style: italic; font-size: 1.0em; color: red"> <%: Html.TranslateTag("(Inactive)","(Inactive)")%></span><%} %><br />
                            <span style="font-size: 0.9em"><%= item.UserName %></span>
                        </div>
                    </a>
                </div>
            </div>
            <%}
                else
                { %>

            <div class="col-5">
                <div class="col-12 col-md-6">
                    <span style="font-weight: bold; font-size: 1.2em"><%= item.FirstName + " " + item.LastName%></span><% if (!item.IsActive)
                                                                                                                           { %><span style="font-weight: bold; font-style: italic; font-size: 1.0em; color: red"> <%: Html.TranslateTag("(Inactive)","(Inactive)")%></span><%} %><br />
                    <span style="font-size: 0.9em"><%= item.UserName %></span>
                </div>

                <div class="col-12 col-md-4">
                    <%if (MonnitSession.CustomerCan("Navigation_View_Administration"))
                        {%>
                    <input style="width: 85px;" type="text" placeholder="Access Token" id="accessToken_<%=item.AccountID %>" />&nbsp;
                        <a style="font-size: 1.5em; vertical-align: middle;" class="fa fa-sign-in viewAccount" href="/Account/ProxySubAccount/" onclick="viewAccount(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>"></a>
                    <span id="proxyMessage_<%=item.AccountID %>" class="bold" style="color: red;"></span>
                    <%}%>
                </div>
            </div>
            <%}%>

            <div class="col-6">
                <div class="hidden-xs col-md-6">
                    <b><%: Html.TranslateTag("Email","Email")%>:</b> <%= item.NotificationEmail %><br />
                    <b><%: Html.TranslateTag("Text","Text")%>:</b> <%: string.IsNullOrEmpty(item.NotificationPhone) ? Html.TranslateTag("None","None") :  item.NotificationPhone %>
                </div>

                <div class="col-12 col-md-6" title="Parent Account: <%: item.Retail %>">
                    <b><%: Html.TranslateTag("Account","Account")%>:</b> <%= item.AccountNumber %><br />
                    <b><%: Html.TranslateTag("Domain","Domain")%>:</b> <%= string.IsNullOrEmpty(item.Domain) ? Html.TranslateTag("None","None") : item.Domain %>
                </div>
            </div>

            <div class="col-1 text-end">
                <div class="AlignTop">
                    <div>
                        <div style="padding: 5px 15px;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                            <%=Html.GetThemedSVG("menu") %>
                        </div>

                        <ul class="dropdown-menu shadow rounded p-0">
                            <%if (MonnitSession.CustomerCan("Account_View"))
                                {     %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>">View Account
                                    <span>
                                        <%=Html.GetThemedSVG("login") %>
                                    </span>
                                </a>
                            </li>

                            <li>
                                <a class="dropdown-item menu_dropdown_item" href="/Account/ProxySubAccount/" onclick="viewRulesQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts","Account Alerts")%>">Rules
                                    <span>
                                        <%=Html.GetThemedSVG("actions") %>
                                    </span>
                                </a>
                            </li>
                            <%} %>

                            <li>
                                <a class="dropdown-item menu_dropdown_item" href="/Settings/AccountEdit/<%=item.AccountID %>">Settings
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
                                    href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors","Account Sensors")%>">Sensors
                                    <span>
                                        <%=Html.GetThemedSVG("sensor") %>
                                    </span>
                                </a>
                            </li>

                            <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                                { %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Account/ProxySubAccount" onclick="viewSubsQuick(this); return false;" data-accountid="<%=item.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions","Account Subscriptions")%>">Subscriptions
                                    <span>
                                        <%=Html.GetThemedSVG("subscription") %>
                                    </span>
                                </a>
                            </li>
                            <%} %>
                            <%} %>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%} %>
</div>

<%} %>


<script type="text/javascript">
    var viewFailed = "<%: Html.TranslateTag("Settings/AdminSearch|view account failed","view account failed")%>.";

    function viewAccountList(lnk) {
        var anchor = $(lnk);
        var acctID = anchor.data('accountid');
        var href = anchor.attr('href');

        $.post(href, { id: acctID }, function (data) {
            if (data == "Success")
                window.location.href = "/Settings/AccountUserList";
            else
           showSimpleMessageModal("<%=Html.TranslateTag("View account failed")%>");
        });
    }

    $(document).ready(function () {
        $('form').submit(function (e) {
            e.preventDefault();
            postMain();
        });

        $("#id").focus().select();
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 1000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 2000);
        setTimeout("if(document.activeElement != $('#id')[0]) {$('#id').focus().select();}", 3000);
    });
</script>
