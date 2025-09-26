<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Models.AccountThemeReleaseNoteModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ReleaseNoteWindows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm())
       { %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div class="formtitle">
        Release Notes  
        <label>V <%:System.Reflection.Assembly.GetAssembly(typeof(iMonnit.Controllers.HomeController)).GetName().Version.ToString() %></label>
    </div>
    <div class="fullForm">
        <div class="table table-hover">
            <%  int count = 0;
                foreach (var item in Model)
                {
                    if (!item.IsHidden)
                    {
            %>
            <div id="<%: item.ReleaseNoteID %>">
                <div id="<%: item.Version %>">
                    <%: Html.Hidden("ReleaseNoteID"+count,item.ReleaseNoteID) %>
                </div>
                <div>
                    <%= string.IsNullOrWhiteSpace(item.OverriddenNote) ?  item.Note : item.OverriddenNote %>
                </div>
            </div>
            <% count++;
                    }
                }%>
        </div>
    </div>
    <div style="clear: both; margin-top: 20px; margin-left: 0px; float: right;">
        <input type="submit" value="<%: Html.TranslateTag("Overview/ReleaseNoteWindows|OK","OK")%>" class="btn btn-primary btn-large" />
    </div>
    <%} %>

</asp:Content>
