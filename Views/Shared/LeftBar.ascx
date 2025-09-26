<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<style>
    body {
        margin: 0;
        padding: 0;
        /* make it look decent enough */
        background: transparent;
        color: white;
        font-family: "Avenir Next", "Avenir", sans-serif;
    }

    a {
        text-decoration: none;
        transition: color 0.3s ease;
    }

    @media (max-width: 991px) {
        #menuToggle {
            left: 20px;
        }
    }

    #menuToggle input {
        display: block;
        width: 40px;
        height: 40px;
        position: absolute;
        top: -7px;
        left: -5px;
        cursor: pointer;
        opacity: 0; /* hide this */
        z-index: 2; /* and place it over the hamburger */
        -webkit-touch-callout: none;
    }

    @media (min-width:991px) {
        #menuToggle input {
            visibility: hidden;
        }
    }

    /*
 * Just a quick hamburger
 */
    #menuToggle span {
        display: block;
        width: 33px;
        height: 4px;
        margin-bottom: 5px;
        position: relative;
        background: #515356;
        border-radius: 3px;
        z-index: 1;
        transform-origin: 4px 0px;
        transition: transform 0.4s cubic-bezier(0.77,0.2,0.05,1.0), background 0.4s cubic-bezier(0.77,0.2,0.05,1.0), opacity 0.55s ease;
    }

    #menuToggle span:first-child {
        transform-origin: 0% 0%;
    }

    #menuToggle span:nth-last-child(2) {
        transform-origin: 0% 100%;
    }

    /* 
 * Transform all the slices of hamburger
 * into a crossmark.
 */
    #menuToggle input:checked ~ span {
        opacity: 1;
        transform: rotate(45deg) translate(-2px, -1px);
        background: #c80202;
    }

        /*
 * But let's hide the middle one.
 */
    #menuToggle input:checked ~ span:nth-last-child(3) {
        opacity: 0;
        transform: rotate(0deg) scale(0.2, 0.2);
    }

        /*
 * Ohyeah and the last one should go the other direction
 */
    #menuToggle input:checked ~ span:nth-last-child(2) {
        transform: rotate(-45deg) translate(0, -1px);
    }

    /*
 * Make this absolute positioned
 * at the top left of the screen
 */
    #menu {
        position: absolute;
        width: 21em;
        min-height: 400px;
        list-style-type: none;
        -webkit-font-smoothing: antialiased;
        /* to stop flickering of text in safari */
        transform-origin: 0% 0%;
        transform: translate(-200%, 0);
        transition: transform 0.5s cubic-bezier(0.77,0.2,0.05,1.0);
    }

    #menu li {
        padding: 6px 0;
        font-size: 22px;
        color: white;
    }

    /*
 * And let's slide it in from the left
 */
    #menuToggle input:checked ~ ul {
        transform: none;
        box-shadow: 5px 0px 0px 200em rgba(0,0,0,.5);
        transition: box-shadow 0.4s ease-in-out;
    }

    @media (min-width: 991px) {


        /*.siteLogo {
			margin-left: 180px !important;
		}*/

        #menu {
            transform: translate(0, 0);
        }

        @media (min-width: 991px) {
            #menuToggle input {
                box-shadow: none;
                transition: none;
            }
        }
    }

    @media (min-width: 991px) {
        #menuToggle span {
            opacity: 0;
        }

        input < #menuToggle {
            visibility: hidden;
        }
    }

    @media (max-width: 991px) {
        #menu {
            min-height: 967px;
        }
    }
</style>

<%=""%>

<%
    bool isProxied = MonnitSession.UserIsCustomerProxied;
    //bool isUserProxied = MonnitSession.OldCustomer != null && MonnitSession.CurrentCustomer.CustomerID != MonnitSession.OldCustomer.CustomerID;

    List<Sensor> SensorList = new List<Sensor>();
    List<Gateway> GatewayList = new List<Gateway>();
    List<CSNet> CSNetList = new List<CSNet>();

    if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentTheme.EnableDashboard)
    {
        SensorList = Sensor.LoadByCsNetID(Model);
        GatewayList = Gateway.LoadByCSNetID(Model);
        GatewayList = GatewayList.Where(g => g.GatewayTypeID != 11).ToList();
        CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
        CSNet network = CSNet.Load(Model);
%>
<div class="formtitle media_mobile <%:Request.Path.StartsWith("~/Overview/SensorIndex") || Request.Path.StartsWith("/~Overview/GatewayIndex") ? " " : "disp-none" %>" id="sensorNetwork_mobile">
    <div id="MainTitle" style="display: none;"></div>
    <div style="height: 20px">
        <div id="network-sidenav">
            <%Html.RenderPartial("~/Views/Shared/_NetworkSideBar.ascx"); %>
        </div>
    </div>
    <!-- End Main Refresh -->
</div>

<div class="split_mobile">
    <div class="leftBar_profile media_mobile">
        <a id="userName" 
            class="menu-box-tab dfac leftBar_profile__wrapper mobileProfileWrapper profile-icon-nav <%= isProxied ? "stopProxyGoHome" : "" %>" 
            href="<%= isProxied ? "/Account/StopProxyGoHome" : "/Settings/UserDetail/" + MonnitSession.CurrentCustomer.CustomerID %>">
					<%= isProxied ? Html.GetThemedSVG("logout") : Html.GetThemedSVG("profile") %>
            <div class="leftBar_profile__wrapper__name mobileMenuTitle">
                        
                <%: MonnitSession.CurrentCustomer.FirstName %> <%: MonnitSession.CurrentCustomer.LastName%>
            </div>
        </a>
	</div>

    <div class="inner_leftBar_container">
        <div class="inner_leftBar_title ps-2" style="color: white; font-weight: 500; font-size: 18px;">
            <%: Html.TranslateTag("Shared/TopBar|Overview", "Overview") %>
        </div>
        <% if (isProxied)
            {
                Monnit.Customer cust = Monnit.Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
                if (cust == null)
                {
                    Response.Redirect("/Account/Logoff");
                } %>
        <%}%>
        <%--<a href="/Account/StopProxyGoHome">
            <div class="menuHover rounded-start">
					<%=Html.GetThemedSVG("admin")%>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Shared/TopBar|Admin", "Admin") %>
                </div>
            </div>
        </a>
        <%}%>
        <%else--%>
        <% if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Navigation_View_Administration"))
            { %>
        <a href="/Settings/AdminSearch/<%:MonnitSession.CurrentCustomer.AccountID%>">
            <div class="menuHover rounded-start">
                	<%=Html.GetThemedSVG("admin")%>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Admin", "Admin") %>
                </div>
            </div>
        </a>
        <%} %>

        <a href="/Overview/Index">
            <div class="menuHover rounded-start icon-nav <%:Request.Path.EndsWith("/Overview") || Request.Path.EndsWith("/Overview/Index") || Request.Path == "/" ? "menuActive" : " " %> ">
                	<%=Html.GetThemedSVG("home")%>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Home", "Home") %>
                </div>
            </div>
        </a>

        <a style="color: white;" href="/Overview/SensorIndex">
            <div class="menuHover rounded-start <%:((Request.Path.StartsWith("/Overview/Sensor") && CSNetList.Count > 1) ? "menuActive activeSensorNav" : " ") %> <%: Request.Path.StartsWith("/Overview/Sensor") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("sensor") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Sensors", "Sensors") %>
                </div>
                <%if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware") && MonnitSession.HasOTASuiteGateways(MonnitSession.CurrentCustomer.AccountID) && MonnitSession.HasOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID))
                    { %>
                <div id="updateSensors" style="margin-left: auto;">
                    <%=Html.GetThemedSVG("downloadFirmware") %>
                </div>
                <%} %>
            </div>
        </a>

        <a style="color: white;" href="/Overview/GatewayIndex">
            <div class="menuHover rounded-start <%:((Request.Path.StartsWith("/Overview/Gateway") && CSNetList.Count > 1) ? "menuActive activeSensorNav" : " ") %> <%: Request.Path.StartsWith("/Overview/Gateway") ? "menuActive " : " " %>">
                <%=Html.GetThemedSVG("gateway") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Gateways", "Gateways") %>
                </div>
                <%if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware") && MonnitSession.HasUpdateableGateways(MonnitSession.CurrentCustomer.AccountID))
                    { %>
                <div id="updateGateways" style="margin-left: auto;">
                    <%=Html.GetThemedSVG("downloadFirmware") %>
                </div>
                <%} %>
            </div>
        </a>



        <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
            { %>

        <a style="color: white;" href="/Rule/Index">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Rule") || Request.Path.StartsWith("/Events") ? "menuActive" : " " %>  tempSVGparentDiv">
                <%=Html.GetThemedSVG("rules") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Rules", "Rules") %>
                </div>
            </div>
        </a>

        <%}%>



        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Navigation_View_Maps")) && MonnitSession.AccountCan("view_maps"))
            { %>
        <a style="color: white;" href="/Map/Index">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Map") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("map") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Maps", "Maps") %>
                </div>
            </div>
        </a>
        <%} %>


        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_View_Chart")) && MonnitSession.AccountCan("view_multichart") || MonnitSession.IsCurrentCustomerMonnitAdmin)
            { %>
        <a style="color: white;" href="/Chart/ChartEdit">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Chart/ChartEdit") || Request.Path.StartsWith("/Chart/MultiChart") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("details") %>
                <div class="menuHover-title">
                    <%: Html.TranslateTag("Charts", "Charts") %>
                </div>
            </div>
        </a>
        <%} %>
        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Navigation_View_Reports")))
            { %>
        <a style="color: white;" href="/Export/ReportIndex">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Export/ReportIndex") || Request.Path.StartsWith("/Export/Report") || Request.Path.StartsWith("/Export/CreateNewReport") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("book") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Reports", "Reports") %>
                </div>
            </div>
        </a>
        <%} %>

        <div class="admin-spacer">
        </div>


        <div class="inner_leftBar_title ps-2" style="color: white; font-weight: 500; font-size: 18px;">
            <%: Html.TranslateTag("Account", "Account") %>
        </div>
        <%if (MonnitSession.CustomerCan("Customer_Edit_Other"))
          { %>
        <a class="menu-box-tab" href="/Settings/AccountUserList">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Settings/AccountUserList") || Request.Path.StartsWith("/Settings/User") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("recipients") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Shared/TopBar|Users", "Users") %>
                </div>
            </div>
        </a>
        <% }
           if (CSNetList.Count > 1 || MonnitSession.IsCurrentCustomerMonnitAdmin)
           { %>
        <a class="menu-box-tab" href="/Network/List">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Network") && !Request.Path.StartsWith("/Network/SensorsToUpdate") ? "menuActive" : "" %>">
                <%=Html.GetThemedSVG("network") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Shared/LeftBar|Networks", "Networks") %>
                </div>
            </div>
        </a>
         <%} %>

        <!--<a style="color: white;" href="/Network/SensorsToUpdate">
			<div class="menuHover rounded-start <%//:Request.Path.StartsWith("/Network/SensorsToUpdate") ? "menuActive" : " " %>">
				 <%//=Html.GetThemedSVG("pending") %> 
				<div class="menuHover-title">
					<%//: Html.TranslateTag("Sensor Updates", "Sensor Updates") %>
				</div>
			</div>
		</a>-->
        <%if (MonnitSession.CustomerCan("Account_View")) // View Sub Accounts
          {%>
        <a class="menu-box-tab" href="/Settings/LocationOverview/<%:MonnitSession.CurrentCustomer.AccountID %>">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Settings/LocationOverview") ? "menuActive" : "" %>">

                <%=Html.GetThemedSVG("location") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Shared/LeftBar|Locations", "Locations") %>
                </div>
            </div>
        </a>
        <%}
            var submitAction = "";

            if (MonnitSession.CurrentTheme.Theme == "Default")
            {
                submitAction = "/Retail/PremiereCredit/" + MonnitSession.CurrentCustomer.AccountID;
            }
            else
                submitAction = "/Retail/NotificationCredit/" + MonnitSession.CurrentCustomer.AccountID.ToString();
        %>
        <%if (!MonnitSession.IsEnterprise && MonnitSession.CustomerCan("Can_Access_Billing") && MonnitSession.CustomerCan("Navigation_View_My_Account") && !string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
            { %>
        <%--<a class="menu-box-tab" href="<%=submitAction %>">--%>
        <a class="menu-box-tab" href="/Retail/RetailIndex">
            <div class="menuHover rounded-start icon-nav <%:Request.Path.StartsWith("/Retail") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("credits") %>



                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Shared/TopBar|Credits", "Credits") %>
                </div>
            </div>
        </a>
        <% }%>
        <%if (MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CustomerCan("Navigation_View_API"))
            { %>
        <%--<a class="menu-box-tab" href="/API/RestAPI/">--%>
        <a class="menu-box-tab" href="/API/">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/API") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("code") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("API", "API") %>
                </div>
            </div>
        </a>
        <% }%>

        <%if (MonnitSession.CustomerCan("Can_Access_Testing"))
            { %>
        <a class="menu-box-tab" href="/Testing">
            <div class="menuHover rounded-start <%:Request.Path.StartsWith("/Testing") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("calibrate") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Testing", "Testing") %>
                </div>
            </div>
        </a>
        <%}%>

        <%if (MonnitSession.CustomerCan("Navigation_View_My_Account"))
            { %>
        <a class="menu-box-tab" href="/Settings/AccountEdit/<%:MonnitSession.CurrentCustomer.AccountID %>">
            <div class="menuHover rounded-start icon-nav <%:Request.Path.StartsWith("/Settings/AccountEdit") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("user-settings") %>
                <div class="menuHover-title mobileMenuTitle">
                    <%: Html.TranslateTag("Settings", "Settings") %>
                </div>
            </div>
        </a>
        <% }%>
        <div class="media_mobile">
            <% List<Monnit.CustomerAccountLink> calList = Monnit.CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
                int deletedCount = 0;
                foreach (var item in calList)
                {
                    if (item.CustomerDeleted)
                        deletedCount++;
                }

                if (calList.Count > 0 && calList.Count != deletedCount)
                { %>
            <div>
                <a class="menu-box-tab" href="/Settings/AccountLinkList">
                    <div class="menuHover rounded-start icon-nav <%:Request.Path.StartsWith("/Settings/AccountLinkList") ? "menuActive" : " " %>">
                <%=Html.GetThemedSVG("link") %>


                        <div class="menuHover-title mobileMenuTitle">
                            <%: Html.TranslateTag("Accounts", "Accounts") %>
                        </div>
                    </div>
                </a>
            </div>
            <% }%>
        </div>

        <%if (MonnitSession.CurrentTheme.AllowPWA && MonnitSession.CurrentStyle("MobileAppName").Length > 0 && MonnitSession.CurrentStyles["MobileAppLogo"].BinaryValue.Length > 0)
        {%>
            <a id="installAppLink" class="menu-box-tab" href="/Setup/InstallApp/" style="display: none;">
                <div class="menuHover rounded-start">
                    <%=Html.GetThemedSVG("download-file") %>

                    <span class="menuHover-title mobileMenuTitle">
                        <%: Html.TranslateTag("Shared/TopBar|Install App", "Install App") %>
                    </span>
                </div>
            </a>

            <script>
            //var platform = {};

            //function checkPlatform() {
            //    // browser info and capability
            //    var _ua = window.navigator.userAgent;

            //    //platform.isIDevice = (/iphone|ipod|ipad/i).test(_ua);
            //    platform.isIDevice = /iphone|ipod|ipad/i.test(_ua);
            //    platform.isSamsung = /Samsung/i.test(_ua);
            //    platform.isFireFox = /Firefox/i.test(_ua);
            //    platform.isOpera = /opr/i.test(_ua);
            //    platform.isEdge = /edg/i.test(_ua);

            //    // Opera & FireFox only Trigger on Android
            //    if (platform.isFireFox) {
            //        platform.isFireFox = /android/i.test(_ua);
            //    }

            //    if (platform.isOpera) {
            //        platform.isOpera = /android/i.test(_ua);
            //    }

            //    platform.isChromium = ("onbeforeinstallprompt" in window);
            //    platform.isInWebAppiOS = (window.navigator.standalone === true);
            //    platform.isInWebAppChrome = (window.matchMedia('(display-mode: standalone)').matches);
            //    platform.isMobileSafari = platform.isIDevice && _ua.indexOf('Safari') > -1 && _ua.indexOf('CriOS') < 0;
            //    platform.isStandalone = platform.isInWebAppiOS || platform.isInWebAppChrome;
            //    platform.isiPad = (platform.isMobileSafari && _ua.indexOf('iPad') > -1);
            //    platform.isiPhone = (platform.isMobileSafari && _ua.indexOf('iPad') === -1);
            //    platform.isCompatible = (platform.isChromium || platform.isMobileSafari ||
            //        platform.isSamsung || platform.isFireFox || platform.isOpera);
            //}
            //checkPlatform();

            //const searchApp = async () => {
            //    //If Browser supports getInstalledRelatedApps then itterate through them so see if the PWA is already installed
            //    await navigator.getInstalledRelatedApps().then(relatedApps => {
            //        relatedApps.forEach((app) => {
            //            console.log(app.id, app.platform, app.url);

            //            //if(app.xxxx == Myapp.xxx)
            //            //{
            //                //If the PWA is already installed Open it instead of continuwing in the browser
            //                //Can't make this work at this time

            //                //$('#installAppLink').hide();
            //            //}
            //        });

            //    });
            //}

                $(function () {

                if (platform.isStandalone) {
                    //User is running the PWA App so don't show them the link to install it (already installed if it is running)
                    $('#installAppLink').attr('disabled', 'disabled');
                    $('#installAppLink').hide();
                    return;

                } else if (platform.isCompatible) {
                    //Device can install PWA's show them the link
                        $('#installAppLink').show();
                    return;

                    } else {
                    //Check if IOS running in "Desktop" mode  
                    let isIOSinDesktopMode = (/iPad|iPhone|iPod/.test(navigator.platform) ||
                        (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1)) &&
                        !window.MSStream

                    if (isIOSinDesktopMode) {
                        //They can't install as is, but we hav instructions on the install page that may allow them to
                        //So allow them to get to the install page so they can see those instructions.
                        $('#installAppLink').show();
                        return;
                    }
                }

                //If nothing else indicates they can use PWA disable it
                $('#installAppLink').attr('disabled', 'disabled');
                $('#installAppLink').hide();

                });
            </script>
        <%} %>

        <%Html.RenderPartial("LeftNavCustom"); %>

        <a class="menu-box-tab" href="/Overview/Logoff">
            <div class="menuHover rounded-start icon-nav">
                    <%=Html.GetThemedSVG("logout") %>

                <span class="menuHover-title mobileMenuTitle" id="mobile-logout">
                    <%: Html.TranslateTag("Shared/TopBar|Log Out", "Log Out") %>
                </span>
            </div>
        </a>
    </div>
    <div id="copyWriteDiv"></div>
</div>
<% } %>
<%else if (MonnitSession.CurrentCustomer != null && !MonnitSession.CurrentTheme.EnableDashboard)
    { %>
<a href="/Overview/Index">
    <div class="menuHover icon-nav <% Response.Write(Request.Path.StartsWith("/Overview/Index") || Request.Path.EndsWith("/Overview") ? "menuActive" : "");%> ">

                    <%=Html.GetThemedSVG("home") %>

        <div class="menuHover-title mobileMenuTitle">
            <% Response.Write(Html.TranslateTag("Home", "Home")); %>
        </div>
    </div>
</a>
<%} %>
<%else
    {%>
<a class="menu-box-tab" href="/Account/LogonOV">
    <div class="menuLogin menuHover rounded-start icon-nav">
                    <%=Html.GetThemedSVG("sign-in-nav") %>
        <span class="menuHover_title mobileMenuTitle" id="mobile-logout">
            <% Response.Write(Html.TranslateTag("Shared/TopBar|Login", "Login")); %>
        </span>
    </div>
</a>
<%}%>

<style>
    .fa-sign-in {
        color: #fff;
        padding-bottom: 10px;
        padding-right: 10px;
    }

    .menuLogin:hover > i {
        color: #0067ab;
    }
</style>

<script>
    var noText = document.getElementsByClassName("mobileMenuTitle");
    var navWidth = document.getElementById("sidenav-secondary");
    var sensorCount =  <% Response.Write(SensorList.Count());%>;
    var gatewayCount = <% Response.Write(GatewayList.Count());%>;
    var networkCount = <% Response.Write(CSNetList.Count());%>;

    if (window.location.href.indexOf("/Overview/SensorIndex") > -1) {
        if (networkCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".inner_leftBar").addClass("sidenav-secondary50");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
        }
    }

    if (window.location.href.indexOf("/Overview/GatewayIndex") > -1) {
        if (networkCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
            $(".inner_leftBar").addClass("sidenav-secondary50");
        }
    }
    if (window.location.href.indexOf("/Overview/SensorChart") > -1) {
        if (sensorCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
            $(".inner_leftBar").addClass("sidenav-secondary50");
        }
    }
    if (window.location.href.indexOf("/Overview/GatewayHome") > -1) {
        if (gatewayCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
            $(".inner_leftBar").addClass("sidenav-secondary50");
        }
    }

    var gatewayBar = document.getElementsByClassName('gatewaySideList');
    var sensorBar = document.getElementsByClassName('sensorSideList');

    if (window.location.href.indexOf("/Overview/GatewayIndex") > -1) {
        $(".gatewaySideList").show();
    } else {
        $(".gatewaySideList").hide();
    }
    if (window.location.href.indexOf("/Overview/SensorIndex") > -1) {
        $(".sensorSideList").show();
    } else {
        $(".sensorSideList").hide();

    }

    var sideHref_all = document.getElementById('s-gHref_all');
    var sideHref = document.getElementById('s-gHref');

    if (window.location.href.indexOf("/Overview/GatewayIndex") > -1) {
        $(sideHref_all).attr("href", "/Overview/GatewayIndex/?id=-1&view=List");
    };

    /* Loop through all dropdown buttons to toggle between hiding and showing its dropdown content - This allows the user to have multiple dropdowns without any conflict */

    var dropdown = document.getElementsByClassName("menuHover_dropdown");
    var i;

    for (i = 0; i < dropdown.length; i++) {
        dropdown[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }

    function SetTheme(id) {
        $.post("/Overview/UpdateUserTheme?themeid=" + id, function (data) {
            if (data == 'Success') {
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }

            window.location.reload();
            return id;
        });
    }

    var header = document.getElementsByClassName("inner_leftBar_container");

    var activeSideBar = document.getElementById('UserList');
    var activeSideIcon = document.getElementById('side_user_icon');
    var activeSideTitle = document.getElementById('side_sensor_title');
    if (window.location.href.indexOf('index') > -1 && window.location.href.indexOf('testing') == 0) {
        activeSideBar.style.background = '#FFFFFF';
        activeSideBar.style.color = '#2C3745';
        activeSideIcon.innerHTML = "<svg xmlns='http://www.w3.org/2000/svg' width='22' height='14' viewBox='0 0 22 14'><path id='ic_people_outline_24px' d='M16.5,13A11.987,11.987,0,0,0,12,14a11.8,11.8,0,0,0-4.5-1C5.33,13,1,14.08,1,16.25V19H23V16.25C23,14.08,18.67,13,16.5,13Zm-4,4.5H2.5V16.25c0-.54,2.56-1.75,5-1.75s5,1.21,5,1.75Zm9,0H14V16.25a1.819,1.819,0,0,0-.52-1.22,9.647,9.647,0,0,1,3.02-.53c2.44,0,5,1.21,5,1.75ZM7.5,12A3.5,3.5,0,1,0,4,8.5,3.5,3.5,0,0,0,7.5,12Zm0-5.5a2,2,0,1,1-2,2A2.006,2.006,0,0,1,7.5,6.5Zm9,5.5A3.5,3.5,0,1,0,13,8.5,3.5,3.5,0,0,0,16.5,12Zm0-5.5a2,2,0,1,1-2,2A2.006,2.006,0,0,1,16.5,6.5Z' transform='translate(-1 -5)' fill='#2c3745'/></svg>"
    }

    $("li a").each(function () {
        if (this.href == window.location.href) {
            $(this).addClass("active");
        }
    });

    $('#updateSensors').click(function (event) {
        event.stopPropagation();
        window.location.href = '/Network/SensorsToUpdate';
        return false;
    });

    $('#updateGateways').click(function (event) {
        event.stopPropagation();
        window.location.href = '/Network/GatewaysToUpdate';
        return false;
    });

</script>

