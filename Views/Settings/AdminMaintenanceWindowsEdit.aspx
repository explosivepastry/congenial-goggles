<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.MaintenanceWindow>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminCreateMaintenanceWindows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display:flex; justify-content:center">
        <div style="margin-top: 1rem; max-width: 1500px;" class="x_panel shadow-sm rounded">
            <h2 style="font-size: 2rem; text-align: center;"><%: Html.TranslateTag("Settings/AdminMaintenanceWindowsEdit|Edit Maintenance Windows","Edit Maintenance Windows")%></h2>
            <form id="prefForm" class="form-horizontal form-label-left" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <% Html.RenderPartial("_AdminMaintenanceWindowsForm", Model); %>
            </form>
        </div>
    </div>
</asp:Content>