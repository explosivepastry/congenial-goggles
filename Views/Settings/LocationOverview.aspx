<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LocationOverview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        long acctID = Request.RequestContext.RouteData.Values["id"].ToLong();
        if (acctID < 0)
        {
            if (ViewBag.AccountID != null)
            {
                acctID = ViewBag.AccountID;
            }

            if (acctID < 0)
            {
            acctID = MonnitSession.CurrentCustomer.AccountID;
            }
        }
        Account account = Account.Load(acctID);

        string locationSortPreferenceValue = "";

        PreferenceType preferenceType = PreferenceType.LoadAll().Where(pt => pt.Name.ToLower() == "location sorting").FirstOrDefault();
        Preference pref = Preference.LoadByPreferenceTypeIDandCustomerID(preferenceType.PreferenceTypeID, MonnitSession.CurrentCustomer.CustomerID);
        if (pref != null)
            locationSortPreferenceValue = pref.Value;
        else
        {
            //Check the account preference/default
            Preference secpref = Preference.LoadByPreferenceTypeIDandAccountID(preferenceType.PreferenceTypeID, MonnitSession.CurrentCustomer.AccountID);
            if (secpref != null)
                locationSortPreferenceValue = secpref.Value;
            else
            {
                //If the account is null  then check the Theme preference default
                AccountTheme theme = AccountTheme.Find(MonnitSession.CurrentCustomer.Account);
                AccountThemePreferenceTypeLink link = AccountThemePreferenceTypeLink.LoadByPreferenceTypeIDandAccountThemeID(preferenceType.PreferenceTypeID, theme.AccountThemeID);
                //if not link is nothing then locationSortPreferenceValue
                if (link != null)
                    locationSortPreferenceValue = link.DefaultValue;
            }
        }

        Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> locationSearchResult = AccountLocationSearchModel.LocationSearch(acctID, null);
        List<AccountLocationSearchModel> locations = locationSearchResult.Item1;
        if (locationSortPreferenceValue == "2")
            locations = locations.OrderBy(x => x.AccountNumber).ToList();
        else //if (locationSortPreferenceValue == "1")
            locations.Sort(new AccountLocationSearchModelComparer());

        AccountLocationOverviewHeaderModel headerVals = locationSearchResult.Item2;
    %>

    <div class="container-fluid">

        <div id="fullForm" class=" dffdc">
            <%:Html.Partial("LocationOverviewHeader", headerVals) %>
            <div id="indexdetails">
                <div class="rule-card_container" style="width: 100%">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <div class="location-top-header">
                                <div class="icon-header">
                                    <%=Html.GetThemedSVG("location") %>
                                    <span style="align-items: flex-end; display: flex;" id="networkname">
                                        
                                        <a href="javascript:" style="color: #707070;" onclick="navRedirect(<%:MonnitSession.CurrentCustomer.Account.AccountID%>)"><%=MonnitSession.CurrentCustomer.Account.AccountNumber %></a>
                                      
                                    </span>
                                </div>
                                
                                <div class="endline">
                                <%if (MonnitSession.CustomerCan("Account_View") && MonnitSession.CustomerCan("Can_Create_Locations")) // Reseller creating a sub account
                                    {%>
                                    <div class="add-location">
                                        <a class="btn btn-primary" href="/Settings/CreateLocationAccount/<%: acctID %>">Add Location</a>
                                    </div>
                                    <%} %>
                                    <div class="btn-group" style="height: 30px;">
                                        <input type="text" class="form-control user-dets" id="searchTerm" name="searchTerm" style="max-width: 175px;" placeholder="Filter">
                                    </div>
                                    <div class="menu-hover" id="filterSortDiv" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                                        <%=Html.GetThemedSVG("filter") %>
                                    </div>
                                    
                                    <ul class="dropdown-menu ddm" style="padding: 0;">
                                        
                                    <%foreach (PreferenceTypeOption opt in PreferenceTypeOption.LoadByPreferenceTypeID(preferenceType.PreferenceTypeID))
                                      {%>
                                        <li>
                                            <a class="dropdown-item menu_dropdown_item locationSortOption <%= opt.Value == locationSortPreferenceValue ? "border border-success" : "" %>" data-value="<%=opt.Value == null ? preferenceType.DefaultValue : opt.Value %>">
                                                <span><%=Html.TranslateTag("Settings/LocationOverview|" + opt.Name, opt.Name) %></span>
                                            </a>
                                        </li>
                                    <%} %>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%if (locations.Count == 0)
                        { %>

                    <%:Html.Partial("_WelcomeToLocations") %>

                    <%} %>
                    <div id="locationList" class="sensorList_main">
                        <%:Html.Partial("LocationOverviewDetails",locations) %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script>

        <%= ExtensionMethods.LabelPartialIfDebug("LocationOverview.aspx")  %>
        
        $('#searchTerm').change(
            function () {

                searchText = this.value.toLowerCase();

                $('.searchCardDiv').each(function () {
                    var obj = $(this);
                    var content = obj.attr('data-search').toLowerCase();

                    if (content.includes(searchText)) {
                        obj.removeClass('d-none');
                    } else {
                        obj.addClass('d-none');
                    }
                });
            }
        );

        $(function () {
            $('.locationSortOption').click(function () {

                var value = $(this).attr("data-value");

                $.post('/Settings/LocationSortPreferenceUpdate/', { value: value }, function (data) {
                    if (data == "Success")
                        location.href = '/Settings/LocationOverview/<%=MonnitSession.CurrentCustomer.AccountID%>';
                    else
                        showSimpleMessageModal(data);
                });
            });
        });
    </script>
</asp:Content>
