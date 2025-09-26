<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Models.AccountThemeReleaseNoteModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAnnouncementView
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="x_panel shadow-sm rounded">
        <div class="x_title">
            <h2><%: Html.TranslateTag("Version","Version")%> <%:System.Reflection.Assembly.GetAssembly(typeof(iMonnit.Controllers.HomeController)).GetName().Version.ToString() %> <%: Html.TranslateTag("Settings/AdminAnnouncementView|Release Notes","Release Notes")%></h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">

            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>


            <%foreach (var item in Model)
              { %>
            <div class="x_content col-12">
                <div class="bold col-12 form-group">
                    <%= item.Description %>
                </div>
                <div class="clearfix"></div>
            </div>

            <div class="x_content col-12">
                <div class="col-12 form-group">
                    <%= string.IsNullOrWhiteSpace(item.OverriddenNote) ?  item.Note : item.OverriddenNote %>
                </div>
                <div class="clearfix"></div>
            </div>
            <% 
              } %>


            <div class="clearfix"></div>
            <hr />
            <div>
                <a href="/Settings/AdminAnnouncement/" class="gen-btn" style="padding:3px 20px;max-width:80px;"><%: Html.TranslateTag("Settings/AdminAnnouncementView|Done","Done")%></a>
            </div>

        </div>
    </div>


</asp:Content>
