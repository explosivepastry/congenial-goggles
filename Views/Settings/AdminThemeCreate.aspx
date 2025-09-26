<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAccountThemeCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="x_panel shadow-sm">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Settings/AdminAccountThemeCreate|Portal Create","Portal Create")%></h2>
            <div class="clearfix"></div>
        </div>

        <div class="x_content">
            <form id="prefForm" class="form-horizontal form-label-left" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <% Html.RenderPartial("_AdminAccountThemeForm", Model); %>
            </form>

            <br />
             <div style="color:red;font-weight:bold;font-size:1.1em;"><%=ViewBag.Result %></div>
        </div>
        <div class="clearfix"></div>
    </div>

</asp:Content>
