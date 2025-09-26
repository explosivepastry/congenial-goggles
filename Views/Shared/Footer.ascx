<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; %>

<div id="footer">
    <div id="btm_nav">
        <ul>
            <li><a href="/Network/List">Network Overview</a></li>
            <li><a href="/Account/Overview">Account Information</a></li>
        </ul>
    </div>
    <!-- btm_nav -->
    <div id="brand">
        © <%=DateTime.UtcNow.Year %> All Rights Reserved. 
    </div>
    <!-- brand -->
</div>
<!-- footer -->


