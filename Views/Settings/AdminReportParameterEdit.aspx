<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReportParameter>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminReportParameterEdit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="x_panel shadow-sm">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Settings/AdminReportParameterEdit|Edit Parameter","Edit Parameter")%></h2>
            <div class="clearfix"></div>
        </div>
        <!-- Edit Form -->
        <form id="prefForm" class="form-horizontal form-label-left" method="post">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>              
            <% Html.RenderPartial("_AdminReportParameterForm", Model); %>
            <div class="clearfix"></div>
            <hr />
            <div class="dfac">
                <a href="/Settings/AdminReportBuilder/" class="btn btn-secondary" style="margin-right: 5px;"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                <input id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="gen-btn" style="padding:3.5px 20px;" />
            </div>
        </form>
    </div>

</asp:Content>
