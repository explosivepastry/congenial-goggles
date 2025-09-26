<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreditTypeLink
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div> <a  href="/Retail/NotificationCredit/<%=MonnitSession.CurrentCustomer.AccountID %>">Notification Credits</a> <a  href="/Retail/MessageCredit/<%=MonnitSession.CurrentCustomer.AccountID %>">DataMessage Credits</a></div>

</asp:Content>
