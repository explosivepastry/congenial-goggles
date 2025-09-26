<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
	bool isEnterprise = ConfigData.AppSettings("IsEnterprise").ToBool();
%>


<div class="main_leftBar__sideBar" id="adminHomeContent" style="display: none;">
    <div class="dropdown-container">
        <div style="list-style-type: none;">
            <a class="menuHover" style="color: black; font-size: small;" href="/Overview"><%: Html.TranslateTag("Shared/LeftBarAdmin|Home","Home")%></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Overview/SensorIndex"><%: Html.TranslateTag("Shared/LeftBarAdmin|Sensors","Sensors") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Overview/GatewayIndex"><%: Html.TranslateTag("Shared/LeftBarAdmin|Gateways","Gateways") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Network/List"><%: Html.TranslateTag("Shared/LeftBarAdmin|Networks","Networks") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Rule/Index"><%: Html.TranslateTag("Shared/LeftBarAdmin|Rules","Rules") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Export/ReportIndex"><%: Html.TranslateTag("Shared/LeftBarAdmin|Reports","Reports") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Map/Index"><%: Html.TranslateTag("Shared/LeftBarAdmin|Maps","Maps") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AccountUserList"><%: Html.TranslateTag("Shared/LeftBarAdmin|Users","Users") %></a>
            <a class="menuHover" style="color: black; font-size: small;" href="/Settings/LocationOverview"><%: Html.TranslateTag("Shared/LocationOverview|Locations","Locations") %></a>
        </div>
    </div>
</div>
<%AccountTheme acctTheme = MonnitSession.CurrentTheme;%>
<div id="adminPortalContent" class="main_leftBar__sideBar adminSubNav" style="display: none;">
    <div style="list-style-type: none;">

        <%if (MonnitSession.CustomerCan("Can_Create_Locations"))
            {%>
        <a class="menuHover" style="color: black; font-size: small;" href="/Overview/CreateAccountOV"><%: Html.TranslateTag("Shared/LeftBarAdmin|Create Sub-Account","Create Sub-Account")%></a>
        <%} %>
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || isEnterprise || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminAccountThemeEdit/<%:acctTheme.AccountThemeID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Portal Settings","Portal Settings") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminPreferences/<%:acctTheme.AccountThemeID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Preferences","Preferences") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminEmailTemplate/<%:acctTheme.AccountThemeID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Template","Template") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminThemeEdit/<%:acctTheme.AccountThemeID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Theme Edit","Theme Edit") %></a>
        <%} %>
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
            { %>
        <a class="menuHover" style="font-size: small; color: black;" href="/Settings/AdminSMSCarriersList"><%: Html.TranslateTag("Shared/LeftBarAdmin|SMS Carriers List","SMS Carriers List") %></a>
        <%if ((Request.Url.Host.Contains("staging") || Request.Url.Host.Contains("localhost")) && MonnitSession.CustomerCan("SVG_Icon_Admin"))
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Admin/SystemIcons/<%:acctTheme.AccountThemeID%>"><%: Html.TranslateTag("Shared/LeftBarAdmin|System Icons","System Icons") %></a>
        <%} %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminContacts/<%:MonnitSession.CurrentCustomer.AccountID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Account Contacts","Account Contacts") %></a>
        <%} %>
    </div>
</div>
<div id="adminNotificationsContent" class="main_leftBar__sideBar adminSubNav" style="display: none;">
    <div style="list-style-type: none;">
        <% if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminAnnouncementIndex"><%: Html.TranslateTag("Shared/LeftBarAdmin|Announcement","Announcement")%></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminMaintenanceWindows"><%: Html.TranslateTag("Shared/LeftBarAdmin|Maintenance","Maintenance") %></a>
        <%}
            if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
            {%>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminMassEmail"><%: Html.TranslateTag("Shared/LeftBarAdmin|Mass Email","Mass Email") %></a>
        <%}
            if (MonnitSession.CurrentTheme.Theme == "Default")
            {%>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/ReleaseNotesIndex"><%: Html.TranslateTag("Shared/LeftBarAdmin|Release Notes","Release Notes") %></a>
        <%} %>
    </div>
</div>
<div id="adminDevicesContent" class="main_leftBar__sideBar adminSubNav" style="display: none;">
    <div style="list-style-type: none;">
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced") || (isEnterprise))
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/SensorEdit"><%: Html.TranslateTag("Shared/LeftBarAdmin|Admin Edit Sensor","Admin Edit Sensor")%></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/GatewayEdit"><%: Html.TranslateTag("Shared/LeftBarAdmin|Admin Edit Gateway","Admin Edit Gateway") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/DataPacketIn"><%: Html.TranslateTag("Shared/LeftBarAdmin|Inbound Packets","Inbound Packets") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/DataPacketOut"><%: Html.TranslateTag("Shared/LeftBarAdmin|Outbound Packets","Outbound Packets") %></a>
        <%}
            if (MonnitSession.CustomerCan("Sensor_Create") || (isEnterprise && MonnitSession.IsCurrentCustomerMonnitAdmin))
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Network/ManualAddSensor"><%: Html.TranslateTag("Shared/LeftBarAdmin|Add Sensor","Add Sensor") %></a>
        <%}
            if (MonnitSession.CustomerCan("Gateway_Create") || (isEnterprise && MonnitSession.IsCurrentCustomerMonnitAdmin))
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Network/ManualAddGateway"><%: Html.TranslateTag("Shared/LeftBarAdmin|Add Gateway","Add Gateway") %></a>
        <%} %>
    </div>
</div>
<div id="adminServerContent" class="main_leftBar__sideBar adminSubNav" style="display: none;">
    <div style="list-style-type: none;">
        <%if (!isEnterprise)
            { %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminSettings/<%:MonnitSession.CurrentCustomer.AccountID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Administrative Settings", "Administrative Settings")%></a>
        <%} %>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminAutomaticEmailSettings/<%:MonnitSession.CurrentCustomer.AccountID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Automated Email Settings","Automated Email Settings") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminExceptionLogging/<%:MonnitSession.CurrentCustomer.AccountID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Exception Logging","Exception Logging") %></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AdminReportBuilder/<%:MonnitSession.CurrentCustomer.AccountID %>"><%: Html.TranslateTag("Shared/LeftBarAdmin|Report Builder","Report Builder") %></a>
        <a class="menuHover" style="color: black; font-size: small;" onclick="$.get('/Generic/ClearTimedCache', function (data) { alert(data); });"><%: Html.TranslateTag("Shared/LeftBarAdmin|Clear Server Cache","Clear Server Cache") %></a>
    </div>
</div>

<div id="translationContent" class="main_leftBar__sideBar adminSubNav" style="display: none;">
    <div style="list-style-type: none;">
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/TranslateHome"><%: Html.TranslateTag("Shared/LeftBarAdmin|Translate Search","Translate Search")%></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/TranslatePick"><%: Html.TranslateTag("Shared/LeftBarAdmin|Translation Entry","Translation Entry")%></a>
        <a class="menuHover" style="color: black; font-size: small;" href="/Settings/AutoTranslation"><%: Html.TranslateTag("Shared/LeftBarAdmin|Auto Translation","Auto Translation")%></a>
    </div>
</div>

