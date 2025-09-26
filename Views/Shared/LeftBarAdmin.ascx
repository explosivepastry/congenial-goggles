<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<style>
	body {
		margin: 0;
		padding: 0;
		background: transparent;
		color: white;
		font-family: "Avenir Next", "Avenir", sans-serif;
	}

	a {
		text-decoration: none;
		transition: color 0.3s ease;
	}

	.inner_leftBar {
		display: flex;
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
		opacity: 0;
		z-index: 2;
		-webkit-touch-callout: none;
	}

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

	#menu {
		list-style-type: none;
		-webkit-font-smoothing: antialiased;
		transform-origin: 0% 0%;
		transform: translate(-200%, 0);
		transition: transform 0.5s cubic-bezier(0.77,0.2,0.05,1.0);
	}

		#menu li {
			padding: 6px 0;
			font-size: 16px;
			color: white;
		}

	@media (min-width: 991px) {
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

		.inner_leftBar {
			display: flex;
			flex-direction: row;
		}
	}
</style>
<%="" %>

<%
	bool isEnterprise = ConfigData.AppSettings("IsEnterprise").ToBool(); 
	bool isUserProxied = MonnitSession.OldCustomer != null && MonnitSession.CurrentCustomer.CustomerID != MonnitSession.OldCustomer.CustomerID;
%>

<%if (MonnitSession.CurrentCustomer != null)
	{
		AccountTheme acctTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.AccountID);
%>
<div class="formtitle " style="min-width: fit-content;">
	<%if (MonnitSession.IsCurrentCustomerMonnitAdmin)// || MonnitSession.IsCurrentCustomerReseller)
		{ %>
	<% if (MonnitSession.UserIsCustomerProxied)
		{
			Monnit.Customer cust = Monnit.Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
			if (cust == null)
			{
				Response.Redirect("/Account/Logoff");
			} %>

	<%--<div class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<a href="/Account/StopProxyGoHome">
			<div class="menuLogout menuHover rounded-start ">
				<span class="media_desktop-nav home-icon-nav ">
					 <%=Html.GetThemedSVG("home")%>	
				</span>
				<span class="menuHover-title"><%= MonnitSession.CurrentCustomer.Account.AccountNumber%></span>
			</div>
		</a>
	</div>--%>
	<%}%>
	<% if (MonnitSession.CurrentCustomer.IsMonnitAdmin() && !isEnterprise) { %>
	<%-- START Temp BLE --%>

    <div class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; width: 143%;">
		<a class="leftBar_profile__wrapper" style="color: white; padding: 0px!important; max-height: 151px;" href="/Settings/BluetoothTesting">
			<div class="menuHover rounded-start">
				<span class="media_desktop-nav icon-nav" style="margin-bottom: 5px;">
					 <%=Html.GetThemedSVG("bluetooth")%>	

				

				</span>
				<span class="menuHover-title"><%= Html.TranslateTag("Bluetooth Testing") %></span>
			</div>
		</a>
	</div>

	<%-- END Temp BLE --%>
	 <% } %>
    <div class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; width: 143%;">
		<a class="leftBar_profile__wrapper" style="color: white; padding: 0px!important; max-height: 151px;" href="/Settings/AdminSearch/<%=MonnitSession.CurrentCustomer.AccountID %>">
			<div class="menuHover rounded-start">
				<span class="media_desktop-nav icon-nav" style="margin-bottom: 5px;">
					 <%=Html.GetThemedSVG("search")%>	

				

				</span>
				<span class="menuHover-title"><%= Html.TranslateTag("Search") %></span>
			</div>
		</a>
	</div>
	<%} %>
	<div id="adminHomeBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class="media_desktop-nav home-icon-nav">

				<%=Html.GetThemedSVG("home")%>
		

			</span>
			<span class="menuHover-title"><%= MonnitSession.CurrentCustomer.Account.AccountNumber%></span>
		</div>
	</div>
	<%if ((MonnitSession.CustomerCan("Navigation_View_Administration") && MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin /*|| MonnitSession.IsCurrentCustomerReseller*/ || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin))
        { %>
	<div id="adminPortalBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0px; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class="media_desktop-nav 11">
					<%=Html.GetThemedSVG("desktop")%>
			
				&nbsp;&nbsp;
			</span>
			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/LeftBarAdmin|Portal", "Portal")%>
			</span>
		</div>
	</div>
	<%} %>
	<%if (!ConfigData.AppSettings("IsEnterprise").ToBool())
		{%>
	<div id="adminNotificationsBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class=" media_desktop-nav">
					<%=Html.GetThemedSVG("notifications")%>

				&nbsp;&nbsp;
			</span>
			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/LeftBarAdmin|Notifications","Notifications")%>
			</span>
		</div>
	</div>
	<%} %>
	<%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced") || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin))
		{ %>
	<div id="adminDevicesBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class="media_desktop-nav devices-icon">

					<%=Html.GetThemedSVG("devices")%>
					
				&nbsp;&nbsp;
			</span>
			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/LeftBarAdmin|Devices","Devices")%>
			</span>

		</div>
	</div>
	<%} %>
	<%if (MonnitSession.IsCurrentCustomerMonnitAdmin || (isEnterprise && MonnitSession.CurrentCustomer.IsAdmin))
		{ %>
	<div id="adminServerBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class="media_desktop-nav server-icon">
					<%=Html.GetThemedSVG("server")%>

		
				&nbsp;&nbsp;
			</span>
			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/LeftBarAdmin|Server","Server")%>
			</span>

		</div>
	</div>
	<%} %>
	<%if ((/*MonnitSession.IsCurrentCustomerReseller &&*/ MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID) || MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
        {%>
	<% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin)
		{%>
	<div class="" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<a style="color: white;" href="/Settings/AdminAccountTheme">
			<div class="menuHover rounded-start">
				<span class="media_desktop-nav icon-nav">
					<%=Html.GetThemedSVG("color-fan")%>

	

					&nbsp;&nbsp;
				</span>
				<span class="menuHover-title">
					<%: Html.TranslateTag("Shared/LeftBarAdmin|Themes","Themes") %>
				</span>
			</div>
		</a>
	</div>
	<%}
		else if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
		{ %>
	<div class="" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<a style="color: white;" href="/Settings/AdminAccountThemeEdit/<%:acctTheme.AccountThemeID %>">
			<div class="menuHover rounded-start">
				<span class="media_desktop-nav icon-nav">
					<%=Html.GetThemedSVG("sellsy")%>

			&nbsp;&nbsp;
				</span>
				<span class="menuHover-title">
					<%: Html.TranslateTag("Shared/LeftBarAdmin|Themes Settings","Themes Settings") %>
				</span>
			</div>
		</a>
	</div>
	<%}%>
	<%} %>

	<%if (!ConfigData.AppSettings("IsEnterprise").ToBool())
		{
			if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")))
			{%>
	<div id="translationBtn" class="dropdown-toggles" style="background: transparent; border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important;">
		<div class="menuHover rounded-start">
			<span class="media_desktop-nav icon-nav">
					<%=Html.GetThemedSVG("pencil-square")%>

			&nbsp;&nbsp;
			</span>
			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/LeftBarAdmin|Translation", "Translation")%>
			</span>

		</div>
	</div>

	<%}
		}%>

	<%if (MonnitSession.CustomerCan("Database_User"))
		{ %>
	<div class="" style='border: none; color: white; text-decoration: none; display: block; padding-left: 0; margin: 0px; margin-bottom: 10px!important; height: 30px!important; background: transparent;'>
		<a style="color: white;" href="/Report/DatabaseStatistics">
			<div class="menuHover rounded-start">
				<span class="media_desktop-nav icon-nav">
					<%=Html.GetThemedSVG("chart")%>

		

					&nbsp;&nbsp;
				</span>
				<span class="menuHover-title">
					<%: Html.TranslateTag("Shared/LeftBarAdmin|Stats","Stats") %>
				</span>
			</div>
		</a>
	</div>

	<% if (MonnitSession.UserIsCustomerProxied)
		{
			Monnit.Customer cust = Monnit.Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
			if (cust == null)
			{
				Response.Redirect("/Account/Logoff");
			} %>


	<%}
		else
		{%>
	<a href="/Overview/Logoff">
		<div class="menuHover rounded-start dfac icon-nav ">
					<%=Html.GetThemedSVG("logout")%>

			<span class="menuHover-title">
				<%: Html.TranslateTag("Shared/TopBar|Log Out","Log Out") %>
			</span>
		</div>
	</a>

	<%} %>
	<%} %>
</div>
<%} %>

<script>
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


	$(document).ready(function () {
		$("#adminHomeBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);
			event.stopPropagation();
			$("#adminHomeContent").toggle(350);
		});
		$(document).on("click", function () {
			$("#adminHomeContent").hide(350);
		});
		$("#adminNotificationsBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);
			event.stopPropagation();
			$("#adminNotificationsContent").toggle(350);
		});
		$("#adminNotificationsBtn").on("click", function () {
			event.cancelBubble = true
		});
		$(document).on("click", function () {
			$("#adminNotificationsContent").hide(350);
		});
		$("#adminPortalBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);
			event.stopPropagation();
			$("#adminPortalContent").toggle(350);
		});
		$("#adminPortalBtn").on("click", function () {
			event.cancelBubble = true
		});
		$(document).on("click", function () {
			$("#adminPortalContent").hide(350);
		});
		$("#adminDevicesBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);

			event.stopPropagation();
			$("#adminDevicesContent").toggle(350);
		});
		$("#adminDevicesBtn").on("click", function () {
			event.cancelBubble = true
		});
		$(document).on("click", function () {
			$("#adminDevicesContent").hide(350);
		});
		$("#adminServerBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);
			event.stopPropagation();
			$("#adminServerContent").toggle(350);
		});
		$("#adminServerBtn").on("click", function () {
			event.cancelBubble = true
		});
		$(document).on("click", function () {
			$("#adminServerContent").hide(350);
		});
		$("#translationBtn").click(function (event) {
			$(".main_leftBar__sideBar").hide(350);
			event.stopPropagation();
			$("#translationContent").toggle(350);
		});
		$("#translationBtn").on("click", function () {
			event.cancelBubble = true
		});
		$(document).on("click", function () {
			$("#translationContent").hide(350);
		});
	});



</script>
