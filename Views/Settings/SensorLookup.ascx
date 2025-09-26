<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.SensorInformation>" %>

<% 
    string imagePath = "";
    if (Model.Sensor != null)
    {
        switch (Model.Sensor.Status)
        {
            case Monnit.eSensorStatus.OK:
                imagePath = Html.GetThemedContent("/images/good.png");
                break;
            case Monnit.eSensorStatus.Warning:
                imagePath = Html.GetThemedContent("/images/Alert.png");
                break;
            case Monnit.eSensorStatus.Alert:
                imagePath = Html.GetThemedContent("/images/alarm.png");
                break;
            //case Monnit.eSensorStatus.Inactive:
            //    imagePath = Html.GetThemedContent("/images/inactive.png");
            //    break;
            //case Monnit.eSensorStatus.Sleeping:
            //    imagePath = Html.GetThemedContent("/images/sleeping.png");
            //    break;
            case Monnit.eSensorStatus.Offline:
                imagePath = Html.GetThemedContent("/images/sleeping.png");
                break;
        }
    }
%>

<div class="dfac">
    <%=Html.GetThemedSVG("sensor") %>
    <h2 class="ms-2" style="margin-bottom: .2rem;"><%: Html.TranslateTag("Settings/SensorLookup|Sensor Information","Sensor Information")%></h2>
    <div class="nav navbar-right panel_toolbox">
    </div>
    <div class="clearfix"></div>
</div>

<%if (Model.Sensor != null)
    {%>
<div>
<div class="col-md-6 col-12 ps-0 pe-md-2">
    <div class="half-card w-100" style="min-height:285px;">
        <div class="dfjcsbac card_container__top">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Settings/SensorLookup|Meta Data","Meta Data")%>
            </div>

            <div class=" AlignTop" style="display: flex; flex-direction: column; align-items: flex-end; width: 0%;">
                <div style="height: 100%" class="dfjcfe">
                    <div style="padding: 5px 15px; cursor: pointer;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                        <%=Html.GetThemedSVG("menu") %>
                    </div>

                    <ul class="dropdown-menu ddm dropdownMobileMenu p-0" aria-labelledby="dropdownMenuButton">
                        <%if (MonnitSession.CustomerCan("Account_View"))
                            {     %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2" style="height: inherit; width: 100%; display: flex;" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account", "View Account")%>">
                                <%: Html.TranslateTag("Account/ProxySubAccount|View Account","View Account")%>
                                <span>
                                    <%=Html.GetThemedSVG("login") %>
                                </span>
                            </a>
                        </li>

                        <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                href="/Account/ProxySubAccount/" onclick="viewEventsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts", "Account Alerts")%>">
                                <span><%: Html.TranslateTag("Account/ProxySubAccount|Actions","Actions")%></span>
                                <span>
                                    <%=Html.GetThemedSVG("actions") %>
                                </span>
                            </a>
                        </li>
                        <%}
                            } %>

                        <%if (MonnitSession.CustomerCan("Account_Edit"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                href="/Settings/AccountEdit/<%:Model.Account.AccountID%>">
                                <span><%: Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%></span>
                                <span>
                                          <%=Html.GetThemedSVG("user-settings") %>
                            
                                </span>
                            </a>
                        </li>
                        <%} %>

                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                href="/Settings/AccountUserList/<%:Model.Account.AccountID%>">
                                <span><%: Html.TranslateTag("Settings/AccountUserList|Users","Users")%></span>
                                <span>
                                    <%=Html.GetThemedSVG("recipients") %>
                                </span>
                            </a>
                        </li>

                        <%if (MonnitSession.CustomerCan("Account_View"))
                            {     %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%>">
                                <span><%: Html.TranslateTag("Account/ProxySubAccount|Sensors","Sensors")%></span>
                                <span>
                                    <%=Html.GetThemedSVG("sensor") %>
                                </span>
                            </a>
                        </li>

                        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                href="/Account/ProxySubAccount" onclick="viewSubsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions", "Account Subscriptions")%>">
                                <span><%: Html.TranslateTag("Account/ProxySubAccount|Subscriptions","Subscriptions")%></span>
                                <span>
                                    <%=Html.GetThemedSVG("subscription") %>
                                </span>
                            </a>
                        </li>
                        <%}
                            } %>
                    </ul>
                </div>
            </div>
        </div>

        <div class="x_content" style="padding: 10px;">
            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Name (SensorID)","Name (SensorID)")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%= Model.Name %> (<%:Model.SensorID %>) <%if (Model.Sensor.IsDeleted || Model.Account.AccountID == null)
                                                                 {%>&nbsp;<span style="color: red; font-style: italic;"> <%: Html.TranslateTag("Settings/SensorLookup|Deleted","Deleted")%></span> <%} %>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Application","Application")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%:Model.Sensor.MonnitApplication.ApplicationName%>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Firmware Version","Firmware Version")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%:Model.Sensor.FirmwareVersion%>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Radio Band","Radio Band")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%:Model.Sensor.RadioBand%>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Power Source","Power Source")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%:Model.Sensor.PowerSource.Name%>
                </div>
            </div>

            <div class="text-end">
                <% if (MonnitSession.CustomerCan("Support_Advanced"))
                    { %>
                <a class="btn btn-primary" href="/Settings/SensorEdit/<%: Model.SensorID %>"><%: Html.TranslateTag("Settings/SensorLookup|Admin Edit","Admin Edit")%></a>
                <%} %>
            </div>
        </div>
    </div>
</div>

<% if (Model.Network != null)
    { %>

<div class="col-md-6 col-12 px-0 ps-md-2">
    <div class="half-card w-100" >
        <div class="dfjcsbac card_container__top">
            <div class="card_container__top__title"><%: Html.TranslateTag("Settings/SensorLookup|Activity Info","Activity Info")%></div>
            <div class=" AlignTop" style="display: flex; flex-direction: column; align-items: flex-end; width: 0%;">
                <div style="height: 100%" class="dfjcfe">
                    <div style="padding: 5px 15px; cursor: pointer;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                        <%=Html.GetThemedSVG("menu") %>
                    </div>

                    <div class="dropdown-menu ddm dropdownMobileMenu p-0" aria-labelledby="dropdownMenuButton">
                        <ul class="p-0 m-0">
                            <%if (MonnitSession.CustomerCan("Account_View"))
                                {     %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2" style="height: inherit; width: 100%; display: flex;" href="/Account/ProxySubAccount/" onclick="viewAccountQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account", "View Account")%>">
                                    <span><%: Html.TranslateTag("Account/ProxySubAccount|View Account","View Account")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("login") %>
                                    </span>
                                </a>
                            </li>

                            <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                                { %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                    href="/Account/ProxySubAccount/" onclick="viewEventsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Alerts", "Account Alerts")%>">
                                    <span><%: Html.TranslateTag("Account/ProxySubAccount|Actions","Actions")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("actions") %>
                                    </span>
                                </a>
                            </li>
                            <% }
                                }
                                if (MonnitSession.CustomerCan("Account_Edit"))
                                { %>

                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                    href="/Settings/AccountEdit/<%:Model.Account.AccountID%>">
                                    <span><%: Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("user-settings") %>
                                    </span>
                                </a>
                            </li>
                            <%} %>

                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                    href="/Settings/AccountUserList/<%:Model.Account.AccountID%>">
                                    <span><%: Html.TranslateTag("Settings/AccountUserList|Users","Users")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("recipients") %>
                                    </span>
                                </a>
                            </li>

                            <%if (MonnitSession.CustomerCan("Account_View"))
                                {     %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                    href="/Account/ProxySubAccount/" onclick="viewSensorsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Sensors", "Account Sensors")%>">
                                    <span><%: Html.TranslateTag("Settings/Account/ProxySubAccount|Sensors","Sensors")%></span>
                                    <span>
                                        <%=Html.GetThemedSVG("sensor") %>
                                    </span>
                                </a>
                            </li>

                            <%if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Account_Set_Premium"))
                                { %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item px-3 py-2"
                                    href="/Account/ProxySubAccount" onclick="viewSubsQuick(this); return false;" data-accountid="<%:Model.Account.AccountID%>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|Account Subscriptions", "Account Subscriptions")%>">
                                    <span><%: Html.TranslateTag("Settings/Account/ProxySubAccount|Subscriptions","Subscriptions")%></span>
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
            </div>
        </div>

        <div class="x_content" id="systemActionList" style="display:initial; padding: 5px;">

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Network","Network")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%= Model.Network.Name%> (<%:Model.Account.AccountID%>)
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Pending Transaction","Pending Transaction")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%:(!Model.Sensor.CanUpdate).ToString()%>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Status","Status")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <img src="<%: imagePath %>" />
                    <%:Model.Sensor.Status.ToString()%>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Last Check In","Last Check In")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <% if (Model.Sensor.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
                        { %>
                    <%: Monnit.TimeZone.GetLocalTimeById(Model.Sensor.LastCommunicationDate, Model.Account.TimeZoneID).ToShortDateString()%>
                    <%: Monnit.TimeZone.GetLocalTimeById(Model.Sensor.LastCommunicationDate, Model.Account.TimeZoneID).ToShortTimeString()%>
                    <% } %>
                </div>
            </div>

            <div class="form-group row">
                <div class="bold col-12 col-sm-6" style="padding-left: 20px;">
                    <%: Html.TranslateTag("Settings/SensorLookup|Parent","Parent")%>
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
                    <%: Html.TranslateTag("Settings/SensorLookup|Account","Account")%>
                </div>

                <div class="col-12 col-sm-6 mobileCardIndent">
                    <%=Model.Account.CompanyName%>
                </div>
            </div>

            <div class="form-group row">
                <div class="col-12 col-sm-6 input-group d-flex align-items-center" style="padding-left: 17px;">
                    <%if (Model.Network.AccountID != ConfigData.AppSettings("DeviceHolderAccountID").ToLong() && (MonnitSession.CustomerCan("Account_View") || MonnitSession.CustomerCan("Navigation_View_Administration")))
                        {%>
                    <input class="form-control" style="max-width:200px;" type="text" placeholder="Access Token" id="accessToken_<%=Model.Account.AccountID %>" />&nbsp;
                        <a style="font-size: 1.5em; vertical-align: middle;" class="viewAccount" href="/Account/ProxySubAccount/" onclick="viewAccount(this); return false;" data-accountid="<%=Model.Account.AccountID %>" title="<%: Html.TranslateTag("Settings/_AdminAccountListRow|View Account","View Account")%>">
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
<% }
    } %>

<script>
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
