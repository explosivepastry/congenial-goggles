<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<AccountSubscription>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminSubscriptionDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Account account = Account.Load(Model[0].AccountID);

        long currentSubscriptionID = account.CurrentSubscription.AccountSubscriptionID;

        List<AccountSubscriptionChangeLog> changeLogs = AccountSubscriptionChangeLog.LoadBySubscriptionID(currentSubscriptionID);

    %>

    <div class="container-fluid">
        <div class="dfac" style="width: 100%; margin-top: 20px;">
        </div>
        <div class="col-md-6 col-12 mt-2 pe-md-2">
            <div class="shadow-sm rounded gridPanel w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title ms-2">
                        <%=Html.GetThemedSVG("subscription") %>
                        &nbsp;
                            <%: Html.TranslateTag("Settings/AdminSubscriptionEdit|Edit Subscription","Edit Subscription")%>
                    </div>
                </div>
                <div class="card_container__body">
                    <div class="card_container__body__content" style="padding: 10px;">
                        <% Html.RenderPartial("_AdminSubscriptionEdit", account.CurrentSubscription); %>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-12 mt-2 ps-md-2">
            <div class="shadow-sm rounded gridPanel" style="width: 100%;">
                <div class="card_container__top">
                    <div class="card_container__top__title ms-2">
                        <%=Html.GetThemedSVG("subscription") %>
                        &nbsp;
                            <%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Account Subscriptions","Account Subscriptions")%>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="card_container__body" style="padding-top: 10px;">
                    <div class="card_container__body__content" style="padding: 10px;">

                        <table class="table table-hover align-middle">
                            <thead>
                                <tr>
                                    <th scope="col"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Type","Type")%></th>
                                    <th scope="col" class="text-end"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Expiration Date","Expiration Date")%></th>
                                    <th scope="col" class="text-end"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Max Sensors","Max Sensors")%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%
                                    foreach (AccountSubscription subscription in Model)
                                    {
                                %>
                                <tr>
                                    <th>
                                        <div class="d-flex">
                                            <%=subscription.AccountSubscriptionType.Name %>
                                            <%if (subscription.AccountSubscriptionID == currentSubscriptionID)
                                                { %>
                                            <span title="Primary account contact" class="badge bg-green ms-2"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Current","Current")%></span>
                                            <%} %>
                                        </div>
                                    </th>
                                    <td>
                                        <div class="text-end">
                                            <div class="editor-error">
                                                <%=subscription.ExpirationDate.ToShortDateString() %>
                                                <div class="fa fa-circle" style="color: <%= subscription.ExpirationDate > DateTime.UtcNow ? "#39e654" : "#e63939" %>"></div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="text-end">
                                            <%=subscription.AccountSubscriptionType.AllowedSensors %>
                                        </div>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <%--        Account Subsription History--%>

        <div class="col-md-6 col-12 mt-2 ps-md-2">
            <div class="shadow-sm rounded gridPanel" style="width: 100%;">
                <div class="card_container__top">
                    <div class="card_container__top__title ms-2">
                        <%=Html.GetThemedSVG("subscription") %>
                        &nbsp;
                            <%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Subscription History","Subscription History")%>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="card_container__body" style="padding-top: 10px;">
                    <div class="card_container__body__content" style="padding: 10px;">

                        <table class="table table-hover align-middle">
                            <thead>
                                <tr>
                                    <th scope="col"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Change Date","Change Date")%></th>
                                    <th scope="col" class="text-end"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|User","User")%></th>
                                    <th scope="col" class="text-end"><%: Html.TranslateTag("Settings/AdminSubscriptionDetails|Key Type","Key Type")%></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%
                                    var sortedLogs = changeLogs.OrderByDescending(log => log.ChangeDate);
                                    foreach (AccountSubscriptionChangeLog log in sortedLogs)
                                    {
                                %>
                                <tr>
                                    <th>
                                        <div class="d-flex">
                                            <%=log.ChangeDate%>
                                        </div>
                                    </th>
                                    <td>
                                        <div class="text-end">
                                            <%=log.Customer.FirstName%> <%=log.Customer.LastName %>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="text-end">
                                            <%=log.ChangeNote %>
                                        </div>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>



    </div>

    <script type="text/javascript">
        $(document).ready(function () {
<%--                $('#submessage').html('<%=ViewBag.subMessage%>');--%>
            toastBuilder('<%=ViewBag.subMessage%>');
        });
        function goback() {
            window.history.back();
        }
    </script>

</asp:Content>
