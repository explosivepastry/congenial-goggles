<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%if(!string.IsNullOrEmpty(ViewData["Phone"].ToStringSafe())){%>
    <div>Click this link to send a test message to <a href="#" onclick="testSMS(); return false;"><span class="displayNotificationPhone"><%:ViewData["Phone"].ToStringSafe() %></span></a></div>
<%}%>