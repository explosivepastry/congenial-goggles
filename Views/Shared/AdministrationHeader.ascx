<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="header">
	<div id="logo">
		&nbsp;</div>
	<!-- logo -->
	<div id="logindisplay">
		<% Html.RenderPartial("LogOnUserControl"); %>
	</div>
	<!-- logindisplay -->

	<div id="menucontainer">
		<div id="top_nav">
			<ul class="sf-menu sf-js-enabled sf-shadow">
				
				<li><a id="MenuAccounts" class="sf-with-ul" href="/Account/Index">Accounts</a></li>
				<%Html.RenderPartial("CustomNavigationAdmin"); %>
				<li><a id="MenuReports" class="sf-with-ul" href="/Report/AdminOverview">Reports</a></li>
				                
				<li><a id="MenuNetwork" class="sf-with-ul" href="/">My Network</a></li>
				
			</ul>
		</div> <!-- top_nav --> 
	</div>
	<!-- menucontainer -->
</div>
<!-- header -->

