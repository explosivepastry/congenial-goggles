<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChooseSensorTemplate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <%=Html.Partial((string)ViewBag.PartialName.ToString(), MonnitSession.NotificationInProgress) %>

    <%=Html.Partial((string)ViewBag.ExistingRulePartialName.ToString(), (List<Notification>)ViewBag.ExistingRulesList) %>


</asp:Content>
