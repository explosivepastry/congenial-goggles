<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<p>
Your paid sensor portal subscription <%:MonnitSession.CurrentCustomer.Account.IsPremium ? "will expire on" : "expired on"%> <b><%:MonnitSession.CurrentCustomer.Account.PremiumValidUntil.ToShortDateString() %></b><br />
To extend your subscription please call.<%-- (xxx)xxx-xxxx--%>
</p>