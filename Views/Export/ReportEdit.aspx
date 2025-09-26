<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="clearfix"></div>
    <div class="container-fluid">
        <span ><%:Html.Partial("_ReportHeader") %></span>
        <%:Html.Partial("_BuildReport") %>
    </div>

</asp:Content>
