<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<%=""%>
<%
    bool isProxied = MonnitSession.UserIsCustomerProxied;
    bool isUserProxied = MonnitSession.OldCustomer != null && MonnitSession.CurrentCustomer.CustomerID != MonnitSession.OldCustomer.CustomerID;
%>
<!-- top navigation -->
<div class="top_nav">
    <div class="nav_menu shadow-sm">
        <nav class="top-nav py-2" role="navigation">

            <div class="media_mobile" id="mobileMenuIcon" onclick="openNav()">
                <%=Html.GetThemedSVG("mobile-menu")%>
            </div>

            <% string url = (MonnitSession.CurrentCustomer != null && !string.IsNullOrEmpty(MonnitSession.CurrentCustomer.HomepageLink) ? MonnitSession.CurrentCustomer.HomepageLink : "/Overview/Index"); %>
            <a class="siteLogo_container" href="<%= url %>">
                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0)
                    {%>
                <img class="siteLogo2" src="/Overview/Logo" />
                <%}
                else
                {%>
                <img class="siteLogo2" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>
                <div id="siteLogo2"></div>
            </a>
            <%if (MonnitSession.CurrentCustomer != null)
                { %>
            <div class="notifications-wrapper">
                <div role="presentation" class="dropdown">
                    <a id="mailDrop" href="javascript:;" class="dropdown-toggles info-number" data-bs-toggle="dropdown" aria-expanded="false">
                        <%=Html.GetThemedSVG("notifications") %>
                        <% List<NotificationTriggered> Notis = MonnitSession.CurrentCustomer != null ? NotificationTriggered.LoadActiveByAccountID(MonnitSession.CurrentCustomer.AccountID) : new List<NotificationTriggered>();
                            List<NotificationTriggered> AlertingNotis = Notis.Where(c => c.AcknowledgedTime == DateTime.MinValue).ToList();
                            List<NotificationTriggered> PendingNotis = Notis.Where(c => c.AcknowledgedTime != DateTime.MinValue && c.resetTime == DateTime.MinValue).ToList();
                        %>
                        <span id="ActiveEventAlert" style="<%=AlertingNotis.Count() == 0 ?  "display:none": "display:block"%>; padding:3px;" title="<%: Html.TranslateTag("Shared/TopBar|Events: Alerting", "Events: Alerting")%> " class="badge bg-red">
                            <span style="font-size:10px;" id="activeOnlyNotiCount"><%:AlertingNotis.Count() + PendingNotis.Count()%> </span>
                        </span>
<%--                        <span id="EventAlert" style="<%= PendingNotis.Count() > 0 ?  "display:block": "display:none"%>; position: absolute; top: -2px;" title="<%: Html.TranslateTag("Shared/TopBar|Events: (Alerting / Pending)", "Events: (Alerting / Pending)")%> " class="badge bg-red">
                            <span id="activeNotiCount"><%:AlertingNotis.Count%></span>/<span id="pendingNotiCount"><%:PendingNotis.Count%></span>
                        </span>--%>
                    </a>
                    <ul id="menu1" class="dropdown-menu py-0" role="menu">
                        <li>
                            <a href="/Rule/Index">
                                <strong><%: Html.TranslateTag("Shared/TopBar|See All Rules", "See All Rules")%></strong>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- start          topbar-->
            <div class="ms-auto media_desktop" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" style="cursor: pointer; white-space: nowrap;">
                <%
                    //if(MonnitSession.CustomerIDLoggedInAsProxy > 0)

                    if (isUserProxied)
                    {
                        // only for ProxyUser, not for ProxySubAccount or ProxyCustomerAccountLink
                %>
                <span class="stopProxyGoHome">
                    <span style="padding-right: 0.25rem">
                        <%=  Html.GetThemedSVG("logout") %>
                    </span>
                    <span style="font-weight: bold; text-align: right;">
                        <%= MonnitSession.CurrentCustomer.UserName %>
                    </span>
                </span>
                <%                
                    }
                %>
                <span style="font-weight: bold; text-align: right;">
                    <%=Html.GetThemedSVG("profile") %>
                </span>
            </div>

            <ul class="dropdown-menu shadow-sm rounded" style="padding: 0;">
                <li>
                    <a class="dropdown-item menu_dropdown_item p-3 dropdown-profile-icon" href="/Settings/UserDetail/<%:MonnitSession.CurrentCustomer.CustomerID %>">
                            <%=Html.GetThemedSVG("profile") %>

                        <!--User Settings-->
                        <span><%= MonnitSession.CurrentCustomer.FirstName %> <%= MonnitSession.CurrentCustomer.LastName%></span>
                    </a>
                </li>

                <%if (Request.IsSensorCertMobile() && MonnitSession.IsCurrentCustomerMonnitAdmin)// monnit admin part added until testing is complete
                    { %>
                <li>
                    <a class="dropdown-item menu_dropdown_item p-3" href="/ConfigurePush?customerID=<%:MonnitSession.CurrentCustomer.CustomerID%>">
                        <i class="fa fa-bullhorn pull-right"></i>
                        <span><%: Html.TranslateTag("Shared/TopBar|Configure Push","Configure Push") %></span>
                    </a>
                </li>
                <% }
                    if (MonnitSession.CurrentTheme.EnableClassic && !MonnitSession.IsEnterprise)
                    {
                        if (
                            !Request.IsSensorCertMobile() &&
                            (MonnitSession.CurrentCustomer.Account.CreateDate < ConfigData.AppSettings("NewAccountOVDate", "2019/01/01").ToDateTime())
                            || MonnitSession.CurrentCustomer.Preferences["Allow Classic Link"].ToBool())
                        { %>
                <li>
                    <a id="classicViewLink" class="dropdown-item menu_dropdown_item p-3" onclick="OneviewCustomerLog('Back to classic');">
                        <%=Html.GetThemedSVG("rotate-left") %>
                        <span><%: Html.TranslateTag("Shared/TopBar|Back to Classic View ", "Back to Classic View ")%></span>
                    </a>
                </li>
                <% }
                    }
                    List<Monnit.CustomerAccountLink> calList = Monnit.CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
                    int deletedCount = 0;
                    foreach (var item in calList)
                    {
                        if (item.CustomerDeleted)
                            deletedCount++;
                    }
                    if (calList.Count > 0 && calList.Count != deletedCount)
                    { %>
                <li>
                    <a class="dropdown-item menu_dropdown_item p-3" href="/Settings/AccountLinkList">
                        <%=Html.GetThemedSVG("link") %>

                        <span><%: Html.TranslateTag("Linked Accounts","Linked Accounts") %></span>
                    </a>
                </li>
                <% } %>

                <li>
                    <a style="justify-content: unset;" class="dropdown-item menu_dropdown_item p-3 home-icon" onclick="SetHomepage()">
                        <%=Html.GetThemedSVG("home") %>

                        <%: Html.TranslateTag("Shared/TopBar|Set as Landing Page", "Set as Landing Page")%>
                    </a>
                </li>
                <% Html.RenderPartial("CogList"); %>
                <li>
                    <a class="dropdown-item menu_dropdown_item p-3 " href="/Overview/Logoff">
                        <%=Html.GetThemedSVG("logout") %>

                        <span><%: Html.TranslateTag("Shared/TopBar|Log Out","Log Out") %></span>
                    </a>
                </li>
            </ul>
            <%} %>
        </nav>

    </div>
</div>

<%
    string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
%>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("TopBar.ascx") %>

    $('.stopProxyGoHome').click(function (e) {
        e.stopPropagation();

        // All 3 seem to produce the same (desired) effect in this situation

        // similar behavior as an HTTP redirect
        /*window.location.replace("/Account/StopProxyGoHome");*/

        // similar behavior as clicking on a link
        /*window.location.href = "/Account/StopProxyGoHome";*/
        window.location.href = "/Account/UnproxyUserAndGoToLocationOverview";

        // ???
        /*location.replace("/Account/StopProxyGoHome");*/
    });

    $(document).ready(function () {

        if (platform.isStandalone) {
            $('#classicViewLink').hide();
        }

        $('#mailDrop').click(function (e) {

            e.preventDefault();

            $.post("/Notification/TopBarNotiList", function (data) {
                $('#menu1').html(data)
            });
        });
        $(window).on("scroll", function () {
            if ($(window).scrollTop() > 70) {
                document.getElementById("mobileMenuIcon").classList.add("media-mobileScroll");

            } else {
                document.getElementById("mobileMenuIcon").classList.remove("media-mobileScroll");
            }
        });
		<%
	if(!System.Diagnostics.Debugger.IsAttached && !ConfigData.AppSettings("DisableTopBarNotiRefresh").ToBool())
			{
		%>
		window.setInterval('updateNoti()', 60000);// 60000 = 1 minute;
		<%
			}
		%>
        

    });

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    function OneviewCustomerLog(info) {
		<%if (!ConfigData.AppSettings("IsEnterprise").ToBool())
    {%>
        $.post("/Overview/OneviewTravelLog", { note: info }, function (data) {
            if (data == "Success") {
                setCookie("portaltype", "classic", 7)
                window.location.href = "/"
            }

        });
		<%}%>
    }

    function SetHomepage() {
        var my_url_var = (window.location != window.parent.location)
            ? document.referrer
            : document.location.href;

        $.get('/Overview/SetHomepage?customerid=' + <%: MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer.CustomerID : long.MinValue%> + '&link=' + encodeURIComponent(document.location.href), function (data) {
        });
    }

    function updateNoti() {
        $.get('/AutoRefresh/TopBarNotiRefresh', function (data) {
            data = JSON.parse(data);
            var showIcon = data.showIcon;
            $('#activeNotiCount').html(data.activeCount);
            $('#activeOnlyNotiCount').html(data.activeCount);
            $('#pendingNotiCount').html(data.pendingCount);
            if (showIcon == "1") {

                if (data.pendingCount == 0) {

                    $('#EventAlert').hide();
                    $('#ActiveEventAlert').show();

                } else {

                    $('#EventAlert').show();
                    $('#ActiveEventAlert').hide();
                }

            } else {

                $('#EventAlert').hide();
                $('#ActiveEventAlert').hide();
            }

        });
    }
    function openNav() {
        document.getElementById("sideNav").style.width = "100%";
        document.getElementById("sideNav").style.display = "block"
    }

    function closeNav() {
        document.getElementById("sideNav").style.width = "0";
        document.getElementById("sideNav").style.display = "none"
    }


</script>

<style scoped>
    .top_nav .menu_dropdown_item svg {
        margin-right: 10px !important;
        margin-left: 0 !important;
    }

    .top_nav #svg_notifications {
        height: 30px;
        width: 30px;
    }

    nav #svg_profile {
        height: 40px;
        width: 40px;
        fill: #666;
    }

    .top_nav .menu_dropdown_item span {
        text-align: left;
        margin-right: auto;
    }

    /*.stopProxyGoHome {
        display: inline;
    }*/

    .stopProxyGoHome:hover * {
        fill: #da8118;
        color: #da8118;
    }
</style>

<%--<style>
                    @keyframes changeColor {
                        0% {
                            fill: red;
                        }

                        25% {
                            fill: yellow;
                        }
  
                        50% {
                            fill: green;
                        }

                        75% {
                            fill: yellow;
                        }
  
                        100% {
                            fill: red;
                        }
                    }

                    .stopProxyGoHome > svg {
                        fill: blue;
                        animation: changeColor ease;
                        animation-iteration-count: infinite;
                        animation-duration: 4s;
                        animation-fill-mode: both;
                        cursor:pointer; 
                    }
                </style>--%>