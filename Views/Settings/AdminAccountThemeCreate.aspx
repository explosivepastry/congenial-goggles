<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAccountThemeCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="x_panel shadow-sm rounded mt-4">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Settings/AdminAccountThemeCreate|Portal Create","Portal Create")%>
                <div class="clearfix"></div>
            </div>

            <div class="x_content">
                <form id="prefForm" class="form-horizontal form-label-left" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% Html.RenderPartial("_AdminAccountThemeForm", Model); %>
                </form>

                <br />

                <div style="color: red; font-weight: bold; font-size: 1.1em;"><%=ViewBag.Result %></div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>

</asp:Content>
