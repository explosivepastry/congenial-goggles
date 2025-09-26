<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AccountLocationSearchModel>" %>
<!-- CorporateCards.ascx BEGIN  -->

<% 
    string sensorStatus = "sensorStatus";
    if (Model.DevicesAlerting > 0)
        sensorStatus += "Alert";
    else if (Model.DevicesWarning > 0)
        sensorStatus += "Warning";
    else if (Model.DevicesOffline > 0)
        sensorStatus += "Offline";
    else
        sensorStatus += "OK";

%>
<div class="searchCardDiv corp-card" style="width: 18rem;" data-search="<%=Model.AccountNumber %>">
    <a style="width: 100%; height: 100%; display: flex;" href="/Account/ProxySubAccount/<%=Model.AccountID %>" onclick="viewLocationsQuick(this); return false;" data-accountid="<%=Model.AccountID %>" title="<%: Html.TranslateTag("Settings/LocationOverviewDetails|View Account", "View Account")%>">
        <div class="corp-status <%= sensorStatus %>"></div>

        <div class="corp-top-title" title="">
            <%=Model.AccountNumber %>
            <div class="corp-grid-boxes">
                <div title="Locations" class="corp-box corp-alert"><%=Html.GetThemedSVG("db-location") %><div class="db-number"><%=Model.SubAccounts %></div>
                </div>
                <div title="Alerting" class="corp-box corp-alert"><%=Html.GetThemedSVG("db-alert") %><div class="db-number"><%=Model.DevicesAlerting %></div>
                </div>
                <div title="Warning" class="corp-box corp-alert"><%=Html.GetThemedSVG("db-low-battery") %><div class="db-number"><%=Model.DevicesWarning %></div>
                </div>
                <div title="Offline" class="corp-box corp-alert"><%=Html.GetThemedSVG("db-wifi-off") %><div class="db-number"><%=Model.DevicesOffline %></div>
                </div>
            </div>
        </div>

    </a>
    <%: Html.Partial("_LocationCardMenu") %>
</div>

<!-- CorporateCards.ascx END  -->