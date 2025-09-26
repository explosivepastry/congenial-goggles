<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Portal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <% Html.RenderPartial("_WhiteLabelLink", Model); %>
        <div class="x_panel shadow-sm rounded">
            <div class="x_title">
                <div class="card_container__top__title"><%: Html.TranslateTag("Settings/AdminAccountThemeEdit|Portal Edit","Portal Edit")%></div>
                <div class="clearfix"></div>
            </div>

            <div class="x_content">
                <form id="prefForm" class="form-horizontal form-label-left" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% Html.RenderPartial("_AdminAccountThemeForm", Model); %>
                </form>
         
                <br />

<%--                <div style="color: red; font-weight: bold; font-size: 1.1em;"><%=ViewBag.Result %></div>--%>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>

<script>
    toastBuilder("<%=ViewBag.Result %>");
</script>

</asp:Content>
