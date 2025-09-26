<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Account>" %>

<%if (MonnitSession.CustomerCan("Navigation_View_My_Account") && Request.Url.AbsolutePath.IndexOf("Account/AccountUserList") == -1)
  { %>
    <a href="/Account/AccountUserList/<%:Model.AccountID %>" class="greybutton">User List</a>
<%}%>
<%if (MonnitSession.CustomerCan("Navigation_View_My_Account") && !string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone) && Request.Url.AbsolutePath.IndexOf("Account/NotificationCreditList") == -1)
  { %>
    <a href="/Account/NotificationCreditList/<%:Model.AccountID %>" class="greybutton">Notification Credits</a>
<%}%>
<%if (MonnitSession.CustomerCan("Navigation_View_My_Account") && Request.Url.AbsolutePath.IndexOf("Account/Overview") == -1)
  { %>
    <a href="/Account/Overview/<%:Model.AccountID %>" class="greybutton">Account Settings</a>
<%}%>