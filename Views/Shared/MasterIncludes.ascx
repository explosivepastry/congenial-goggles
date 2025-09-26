<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>   
<% System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; %>

<%
	string CacheControl = ConfigData.FindValue("CacheControl");
	if (string.IsNullOrEmpty(CacheControl))
		CacheControl = DateTime.UtcNow.Date.Ticks.ToString();//If nothing else force refresh once a day
%>

<%if (!MonnitSession.CurrentTheme.EnableClassic)
    { %>
<meta http-equiv="refresh" content="0; URL='/Overview'" />
<%} %>

<%--<link href="<%: Html.GetThemedContent("Site.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("jquery.autocomplete.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("jquery.daterangepicker.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("jquery-ui-base.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("Mobile/tabs.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("Mobile/fonts/fa/css/font-awesome.css")%>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("Skin.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("superfish.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("Forms.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />
<link href="<%: Html.GetThemedContent("my-account.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />--%>

<link href="<%: Html.GetThemedContent("tipTip.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />

<%--<link rel="stylesheet" type="text/css" href="/Scripts/jqueryPlugins/jqplot/jquery.jqplot.css?<%:CacheControl %>" />
<link rel="stylesheet" href="/Scripts/jqueryPlugins/jquery.ibutton/css/jquery.ibutton.css?<%:CacheControl %>" />--%>
<link rel="stylesheet" href="/Scripts/jqueryPlugins/internationalPhone/css/intlTelInput.css?<%:CacheControl %>" />

<%--<script src="/Scripts/jqueryPlugins/jquery-1.10.2.min.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jquery-ui-1.10.3.min.js?<%:CacheControl %>" type="text/javascript"></script>--%>

<script src="/Scripts/jquery-migrate-1.2.1.min.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jquery.ui.touch-punch.min.js?Release=4050" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jquery.autocomplete.js?<%:CacheControl %>" type="text/javascript"></script>

<!-- must be under jqueryui -->
<%--<script src="/Scripts/jqueryPlugins/jquery.watermark.js?<%:CacheControl %>" type="text/javascript"></script>--%>

<!-- must be under jqueryui -->
<%--<%if (ViewContext.RouteData.GetRequiredString("controller").ToLower() != "visual")
	{ %>
<script src="/Scripts/jqueryPlugins/jquery.daterangepicker.js?<%:CacheControl %>" type="text/javascript"></script>
<%}%>--%>

<!-- must be under jqueryui -->
<%--<script src="/Scripts/hoverIntent.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/superfish.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/supersubs.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jquery.tipTip.minified.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/master.js?<%:CacheControl %>" type="text/javascript"></script>--%>
<script src="/Scripts/jqueryPlugins/internationalPhone/js/intlTelInput.min.js?<%:CacheControl %>" type="text/javascript"></script>

<%--<script src="/Scripts/jqueryPlugins/jqplot/jquery.jqplot.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jqplot/plugins/jqplot.cursor.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jqplot/plugins/jqplot.dateAxisRenderer.min.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jqplot/plugins/jqplot.highlighter.min.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jquery.printElement.min.js?<%:CacheControl %>" type="text/javascript"></script>
<script src="/Scripts/jqueryPlugins/jquery.ibutton/lib/jquery.ibutton.js?<%:CacheControl %>"></script>
<script src="/Scripts/my-account.js?<%:CacheControl %>"></script>--%>

<!-- OLD API for Google Charts-->
<!--<script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1','packages':['annotationchart']}]}"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1.1', {packages: ['corechart', 'controls']});
     </script>-->

<!-- This calls the most current Google Charts APO -->
<%--<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
	//'current' replaces the need to call visualization', '1.1'. 
	//'current' also keeps the charts up-to-date with the most current version of the Google.Visualization API. 
	//'upcoming' can be used if you want to beta test the future versions.
	google.charts.load('current', { packages: ['corechart', 'controls'] });     
</script>--%>


<%HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["Preferences"];// Stay Logged in on this computer
	if (cookie == null)
	{ %>
<script src="/Scripts/autoLogout.js?<%:CacheControl %>" type="text/javascript"></script>
<script type="text/javascript">
	sessionLength = <%:(Session.Timeout - 1) * 60000 %>;//Logout one minute before session expires
</script>
<%} %>


<script type="text/javascript">
	function getCookie(cname) {
		var name = cname + "=";
		var decodedCookie = decodeURIComponent(document.cookie);
		var ca = decodedCookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1);
			}
			if (c.indexOf(name) == 0) {
				return c.substring(name.length, c.length);
			}
		}
		return "";
	}

    <%
	if (!ConfigData.AppSettings("IsEnterprise").ToBool() || Request.Url.AbsoluteUri.ToString().ToLower().Contains("/api/index"))
	{ %>
	var viewType = getCookie('portaltype'); // get cookie val

	if (window.location.pathname == "/") {
		if (viewType.length > 0) {

			var allowsOV = '<%=MonnitSession.CurrentTheme.EnableDashboard%>';
			if (viewType == 'oneview' && allowsOV == 'True') {

				window.location.href = "/Overview";

			}

		}
	}
    <%}
	else
	{
    %>
		window.location.href = "/Overview";
    <%}%>

</script>

<%--<link href="<%: Html.GetThemedContent("style-fix.css")%>?<%:CacheControl %>" rel="stylesheet" type="text/css" />--%>
