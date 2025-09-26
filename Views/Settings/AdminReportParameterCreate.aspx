<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminReportParameterCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="x_panel shadow-sm">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Settings/AdminReportParameterCreate|Create Parameter","Create Parameter")%></h2>
            <div class="clearfix"></div>
        </div>
        <!-- Edit Form -->
        <form id="prefForm" class="form-horizontal form-label-left" method="post">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
              <%ReportParameter rp = new ReportParameter();
                rp.ReportQueryID = ViewBag.ReportQueryID;%>
            <% Html.RenderPartial("_AdminReportParameterForm", rp); %>
            <div class="clearfix"></div>
            <hr />
            <div>
                <a href="/Settings/AdminReportBuilder/" class="btn btn-default" style="width: 100px;"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                <input id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary" style="width: 100px; float: left;" />
            </div>
        </form>
    </div>

</asp:Content>
