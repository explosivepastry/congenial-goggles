<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; %>

<div id="footer">
    <div id="btm_nav">
        <ul>
            <li><a href="/Network/List">Network Overview</a></li>
            <li><a href="/Account/Overview">Account Information</a></li>
            <li><a href="http://www.monnit.com">www.monnit.com</a></li>
        </ul>
    </div>
    <div class="clear"></div>
    <!-- btm_nav -->
    <div id="brand">
        Unless otherwise noted, all trademarks are property of Monnit.<br />
        © 2020 Monnit, Corp. All Rights Reserved.
        <br />
        Software Version<%: System.Reflection.Assembly.GetAssembly(typeof(iMonnit.Controllers.HomeController)).GetName().Version %>
    </div>
    <!-- brand -->
</div>
<!-- footer -->


