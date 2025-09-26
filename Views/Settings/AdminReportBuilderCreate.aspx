<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminReportBuilderCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="x_panel shadow-sm rounded mt-4">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Settings/AdminReportBuilderCreate|Create Report","Create Report")%></h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <form id="prefForm" class="form-horizontal form-label-left" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% Html.RenderPartial("_AdminReportBuilderForm", new ReportQuery()); %>

                    <div class="clearfix"></div>
                    <div class="text-end">
                        <a href="/Settings/AdminReportBuilder/" class="btn btn-default"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                        <input id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
