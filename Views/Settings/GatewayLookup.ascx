<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.GatewayInformation>" %>

<% 
    string imagePath = "";
    if (Model.Gateway != null)
    {
        if (Model.Gateway.LastCommunicationDate == DateTime.MinValue)
            imagePath = Html.GetThemedContent("/images/sleeping.png");
        else if (Model.Gateway.ReportInterval != double.MinValue && Model.Gateway.LastCommunicationDate.AddMinutes(Model.Gateway.ReportInterval * 2 + 1) < DateTime.UtcNow)//Missed more than one heartbeat + one minute to take drift into account
            imagePath = Html.GetThemedContent("/images/Alert.png");

        if (Model.Gateway.IsDirty)
            imagePath = imagePath.Replace(".png", "-dirty.png");
    }
%>

<div class=" dfac">
    <%=Html.GetThemedSVG("gateway") %>
    <h2 class="ms-2" style="margin-bottom: .2rem;"><%: Html.TranslateTag("Settings/GatewayLookup|Gateway Information","Gateway Information")%></h2>
    <div class="nav navbar-right panel_toolbox">
    </div>
    <div class="clearfix"></div>
</div>
<div class="x_content col-12">
    <%if (Model.Gateway != null)
        {%>

    <div class="col-md-6 col-12 px-0 pe-md-2">
        <div class="half-card w-100">
            <div class="dfjcsbac card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/GatewayLookup|Meta Data","Meta Data")%>
                </div>
                <div class=" AlignTop" style="display: flex; flex-direction: column; align-items: flex-end; width: 0%;">
                    <div style="height: 100%" class="dfjcfe">
                        <div style="padding: 5px 15px; cursor: pointer;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                            <%=Html.GetThemedSVG("menu") %>
                        </div>
                        <ul class="dropdown-menu shadow rounded" style="padding: 0;">
                            <%if (MonnitSession.CustomerCan("Account_View"))
                                {     %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item" style="height: inherit; width: 100%; display: flex;" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account", "View Account")%>">
                                    <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("login") %>
                                    </span>
                                </a>
                            </li>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Account/ProxySubAccount/" onclick="viewRulesQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts", "Account Alerts")%>">
                                    <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Rules","Rules")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("actions") %>
                                    </span>
                                </a>
                            </li>
                            <%} %>
                    
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Settings/AccountEdit/<%:Model.Account.AccountID%>">
                                    <span><%: Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("user-settings") %>
                                    </span>
                                </a>
                            </li>

                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Settings/AccountUserList/<%:Model.Account.AccountID%>">
                                    <span><%: Html.TranslateTag("Settings/AccountEdit|Users","Users")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("recipients") %>
                                    </span>
                                </a>
                            </li>

                            <%if (MonnitSession.CustomerCan("Account_View"))
                                {     %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%>">
                                    <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Sensors","Sensors")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("sensor") %>
                                    </span>
                                </a>
                            </li>
                            <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                                { %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Account/ProxySubAccount" onclick="viewSubsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions", "Account Subscriptions")%>">
                                    <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Subscriptions","Subscriptions")%></span>
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
            <div class="x_content" style="padding: 10px;">
                <div class="form-group row">

                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Name (GatewayID)","Name (GatewayID)")%>
                    </div>
                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%= Model.Name %> (<%:Model.GatewayID %>) <%if (Model.Gateway.IsDeleted || Model.Gateway.CSNetID == null)
                           {%>&nbsp;<span style="color: red; font-style: italic;"> <%: Html.TranslateTag("Settings/GatewayLookup|Deleted","Deleted")%></span> <%} %>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Type","Type")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%:Model.Gateway.GatewayType.Name.Replace("_", " ")%>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Gateway Firmware Version","Gateway Firmware Version")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%:Model.Gateway.GatewayFirmwareVersion%>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Radio Firmware Version","Radio Firmware Version")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%:Model.Gateway.APNFirmwareVersion%>
                    </div>
                </div>

                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Radio Band","Radio Band")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%:Model.Gateway.RadioBand%>
                    </div>
                </div>
                <% if (Model.Gateway.GatewayTypeID == 17 || Model.Gateway.GatewayTypeID == 18 || Model.Gateway.GatewayTypeID == 22 || Model.Gateway.GatewayTypeID == 23)
                    {%>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|MEID","MEID")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <% try
                            { %>
                        <%: Convert.ToInt64(Model.Gateway.MacAddress.Split('|')[0]).ToString("X")%>
                        <% }
                            catch { } %>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|Phone","Phone")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <% try
                            { %>
                        <%:Model.Gateway.MacAddress.Split('|')[1].Insert(6,"-").Insert(3,") ").Insert(0, "(") %>
                        <% }
                            catch { } %>
                    </div>
                </div>
                <%}
                    else if (string.IsNullOrWhiteSpace(Model.Gateway.MacAddress))
                    {  %>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Settings/GatewayLookup|MacAddress","MacAddress")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%: string.IsNullOrEmpty(Model.Gateway.MacAddress) ? Html.TranslateTag("None","None") : Model.Gateway.MacAddress %>
                    </div>
                </div>
                <%} %>

                <div class="form-group row">
                    <div class="bold col-12 col-sm-9" style="padding-left: 20px;">
                    </div>

                    <div class="text-end">
                        <% if (MonnitSession.CustomerCan("Support_Advanced"))
                            { %>
                        <a class="btn btn-primary" href="/Settings/GatewayEdit/<%: Model.GatewayID %>"><%: Html.TranslateTag("Settings/GatewayLookup|Admin Edit","Admin Edit")%></a>
                        <%} %>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <% if (Model.Network != null)
        { %>
    <div class="col-md-6 col-12 px-0 ps-md-2">
        <div class="half-card w-100" style="min-height:287px;">
            <div class="row">
                <div class="dfjcsbac card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/GatewayLookup|Activity Info","Activity Info")%>
                </div>

                    <div class=" AlignTop" style="display: flex; flex-direction: column; align-items: flex-end; width: 0%;">
                        <div style="height: 100%" class="dfjcfe">
                            <div style="padding: 5px 15px; cursor: pointer;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                                <%=Html.GetThemedSVG("menu") %>
                            </div>

                            <ul class="dropdown-menu ddm shadow rounded" style="padding: 0;">
                                <%if (MonnitSession.CustomerCan("Account_View"))
                                    {     %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item" style="height: inherit; width: 100%; display: flex;" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account", "View Account")%>">
                                        <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%></span>
                                        <span>
                                            <%=Html.GetThemedSVG("login") %>
                                        </span>
                                    </a>
                                </li>

                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Account/ProxySubAccount/" onclick="viewRulesQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts", "Account Alerts")%>">
                                        <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Rules","Rules")%></span>
                                        <span>
                                            <%=Html.GetThemedSVG("actions") %>
                                        </span>
                                    </a>
                                </li>
                                <%} %>

                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Settings/AccountEdit/<%:Model.Account.AccountID%>">
                                        <span><%: Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%></span>
                                        <span>
                                            <%=Html.GetThemedSVG("user-settings") %>
                  
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Settings/AccountUserList/<%:Model.Account.AccountID%>">
                                       <span><%: Html.TranslateTag("Settings/AccountEdit|Users","Users")%></span>
                                        <span>
                                           <%=Html.GetThemedSVG("recipients") %>
                                        </span>
                                    </a>
                                </li>

                                <%if (MonnitSession.CustomerCan("Account_View"))
                                    {     %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%>">
                                        <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%></span>
                                        <span>
                                            <%=Html.GetThemedSVG("sensor") %>
                                        </span>
                                    </a>
                                </li>
                                <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                                    { %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Account/ProxySubAccount" onclick="viewSubsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions", "Account Subscriptions")%>">
                                       <span><%: Html.TranslateTag("Settings/_AdminAccountListRow|Subscriptions","Subscriptions")%></span>
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
            <div class="x_content" id="systemActionList" style=" display:initial; padding:5px;">

                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Network","Network")%>
                    </div>
                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%= Model.Network.Name%> (<%:Model.Network.CSNetID%>)
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Status","Status")%>
                    </div>
                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <img src="<%: imagePath %>" />
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Last Check In","Last Check In")%>
                    </div>
                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <% if (Model.Gateway.LastCommunicationDate < DateTime.UtcNow)
                            { %>
                        <%: Monnit.TimeZone.GetLocalTimeById(Model.Gateway.LastCommunicationDate, Model.Account.TimeZoneID).ToShortDateString()%>
                        <%: Monnit.TimeZone.GetLocalTimeById(Model.Gateway.LastCommunicationDate, Model.Account.TimeZoneID).ToShortTimeString()%>
                        <% } %>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Parent","Parent")%>
                    </div>
                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <% Account Parent = Account.Load(Model.Account.RetailAccountID);
                            AccountTheme Theme = null;
                            if (Parent != null)
                                Theme = AccountTheme.Find(Parent);

                            if (Theme != null)
                            { %>
                        <a href="http://<%: Theme.Domain %>"><%= Parent != null ? Parent.CompanyName : "" %></a>
                        <%}
                            else
                            { %>
                        <%= Parent != null ? Parent.CompanyName : "N/A" %>
                        <%} %>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                        <%: Html.TranslateTag("Account","Account")%>
                    </div>

                    <div class="col-12 col-sm-6 mobileCardIndent">
                        <%=Model.Account.CompanyName%>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-12 col-sm-6 input-group d-flex align-items-center" style="padding-left: 17px;">
                        <%if (Model.Network.AccountID != ConfigData.AppSettings("DeviceHolderAccountID").ToLong() && (MonnitSession.CustomerCan("Account_View") || MonnitSession.CustomerCan("Navigation_View_Administration")))
                            {%>
                        <input class="form-control" style="max-width:200px;" type="text" placeholder="Access Token" id="accessToken_<%=Model.Account.AccountID %>" />
                        <a href="/Account/ProxySubAccount/" onclick="viewAccount(this); return false;" data-accountid="<%=Model.Account.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>">
                            <%=Html.GetThemedSVG("login") %>
                        </a>
                        <span id="proxyMessage_<%=Model.Account.AccountID %>" class="bold" style="color: red;"></span>
                        <%}%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%} %>
<%} %>

<script type="text/javascript">
    var ViewFailed = "<%: Html.TranslateTag("Settings/GatewayLookup|view account failed.","view account failed.")%>";

    function viewAccount(lnk) {

        var anchor = $(lnk);
        var acctID = anchor.data('accountid');
        var href = anchor.attr('href');
        $('#proxyMessage_' + acctID).html("");

        var accessToken = $('#accessToken_' + acctID).val().trim();

        if (tryCount < 10) {
            tryCount++;
            if (accessToken.length == 6) {
                $.post("/Settings/CheckAccessToken", { id: acctID, token: accessToken }, function (data) {

                    if (data == "Success") {
                        $.post(href, { id: acctID }, function (data) {
                            if (data == "Success") {
                                tryCount = 0;
                                window.location.href = "/Overview";
                            }
                            else {
                                $('#proxyMessage_' + acctID).html('Proxy Failed');
                            }
                        });
                    } else {
                        $('#proxyMessage_' + acctID).html('Token Not Accepted');
                    }
                });
            } else {
                $('#proxyMessage_' + acctID).html('Invalid Token');
            }
        } else {
            $('#proxyMessage_' + acctID).html('Try Later');
        }
    }


</script>
