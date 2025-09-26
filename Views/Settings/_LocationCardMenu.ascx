<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountLocationSearchModel>" %>

<div class=" AlignTop">
    <%bool isFavorite = MonnitSession.IsLocationFavorite(Model.AccountID);
        string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
        string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?"); %>

    <div style="height: 100%" class="dfjcfe heart-top-head">
        <div class="listOfFav favoriteItem" style="<%=isFavorite ? "visibility:visible;": "visibility:hidden;" %>; pointer-events: none;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
            <%= Html.GetThemedSVG("heart-beat")  %>
        </div>
        <div class="dropleft dfjcac">
            <div class="menu-hover" id="accountCardGreen_<%:Model.AccountID %>" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                <%=Html.GetThemedSVG("menu") %>
            </div>

            <ul class="dropdown-menu ddm" style="padding: 0;">
                <%if (MonnitSession.CustomerCan("Account_View"))
                    {%>

                <%--
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%=account.AccountID %>" title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|View Account", "View Account")%>">
                                <span>View Account</span>
                                <span><%=Html.GetThemedSVG("profile") %></span>
                            </a>
                        </li>
                --%>

                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Overview/SensorIndex/"
                        title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Account Sensors", "Account Sensors")%>">
                        <span>Sensors</span>
                        <span>
                            <%=Html.GetThemedSVG("sensor") %>
                        </span>
                    </a>
                </li>
                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Rule/Index/"
                        title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Account AlertCount", "Account AlertCount")%>">
                        <span>Rules</span>
                        <span class="rules-menu-ico"><%=Html.GetThemedSVG("rules") %></span>
                    </a>
                </li>
                <%}%>

                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Settings/AccountUserList/">
                        <span>Users</span>
                        <span>
                            <%=Html.GetThemedSVG("recipients") %>
                        </span>
                    </a>
                </li>
                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Settings/AccountEdit/">
                        <span>Settings</span>
                        <span>
                            <%=Html.GetThemedSVG("user-settings") %>
                        </span>
                    </a>
                </li>

                <%if (Model.SubAccounts == 0 && MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Navigation_View_Administration") || MonnitSession.CustomerCan("Can_Create_Locations"))
                    { %>
                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Settings/CreateLocationAccount/"
                        title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Add Location", "Add Location")%>">
                        <span>Add Location</span>
                        <span><%=Html.GetThemedSVG("gps-pin") %></span>
                    </a>
                </li>
                <%}

                    if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CanAssignParent(Model.AccountID))
                    {%>
                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Settings/AssignParentSearch/"
                        title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Assign Parent", "Assign Parent")%>">
                        <span><%: Html.TranslateTag("Settings/LocationOverviewDetails|Assign Parent", "Assign Parent")%></span>
                        <span><%=Html.GetThemedSVG("add") %></span>
                    </a>
                </li>
                <%}

                    if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_View"))
                    {
                        if (MonnitSession.IsCurrentCustomerMonnitAdmin || (MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CustomerCan("Account_Set_Premium")))
                        { %>
                <li>
                    <a class="dropdown-item menu_dropdown_item"
                        onclick="proxySubAccountAndRedirect(this); return false;"
                        data-accountid="<%=Model.AccountID %>"
                        data-destination="/Settings/AdminSubscriptionDetails/"
                        title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|Account Subscriptions", "Account Subscriptions")%>">
                        <span><%: Html.TranslateTag("Settings/LocationOverviewDetails|Subscriptions", "Subscriptions")%></span>
                        <span>
                            <%=Html.GetThemedSVG("subscription") %>
                        </span>
                    </a>
                </li>
                <%}
                    } %>

                <li>
                    <%if (isFavorite == true)
                        {%>
                    <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=Model.AccountID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                        onclick="removeFavoriteLocation(this, '<%=removeFavoriteAlertText%>', 'location')">
                        <span>Unfavorite</span>
                        <span class="heart-n-menu">
                            <%=Html.GetThemedSVG("heart-beat") %>
                        </span>
                    </a>
                    <%}
                        else
                        {%>
                    <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=Model.AccountID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                        onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'location')">
                        <span>Favorite</span>
                        <span class="heart-n-menu">
                            <%=Html.GetThemedSVG("heart-beat") %>
                        </span>
                    </a>
                    <%}%>
                </li>
            </ul>
        </div>
    </div>
</div>
<%--</asp:Content>--%>

<script>

    function removeFavoriteLocation(clickedObj, removeFavoriteAlertText, favoriteType) {
        var obj = $(clickedObj);
        var id = obj.attr('data-id');
        var isFav = true;

        let values = {};
        values.text = removeFavoriteAlertText;
        values.url = '/Overview/FavoritesToggle/';
        values.params = { id: id, isFav: isFav, type: favoriteType };

        let favs = obj.closest('.searchCardDiv.corp-card').find('.favoriteItem');
        favs = favs.add($('#favoriteItem'));
        favs.attr('data-fav', 'true');
        favs.find('svg').addClass('liked');
        favs.filter('.listOfFav').show();
        favs.filter('.listOfFav').css('visibility', 'visible');
        openConfirm(values);
    }

</script>