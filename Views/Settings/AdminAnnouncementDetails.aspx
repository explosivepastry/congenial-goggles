<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.AccountThemeReleaseNoteLink>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminAnnouncementDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%:Html.Partial("ReleaseNoteHeader", (Announcement) ViewBag.Announcement) %>

    <%foreach (var item in Model) {
			AccountTheme ItemTheme = Monnit.AccountTheme.Load(item.AccountThemeID);
			%>
    <div class="x_panel shadow-sm">
        <div class="x_title">
            <%: Html.Label(ItemTheme.Theme, new { @style = "font-size:20pt;" })%>
			&nbsp;
            <%: ItemTheme.Domain%>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <form action="/Settings/AdminAnnouncementDetails" class="form-horizontal form-label-left" method="post">
				<% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
				<% if ((MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
						|| ((MonnitSession.IsCurrentCustomerMonnitAdmin /*|| MonnitSession.IsCurrentCustomerReseller*/) && item.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID)) { %>
					<%--<%: Html.Hidden("releasenoteid",item.AnnouncementID)%>--%>
					<%: Html.Hidden("accountthemeid",item.AccountThemeID)%>

					<div class="x_content col-12" style="margin-left: -5px !important;">
						<div class="bold col-sm-2 col-12">
							<label for="IsHidden<%: item.AccountThemeID%>" style="margin-left: 34px;"><%: Html.TranslateTag("Settings/AdminAnnouncementDetails|Hide Note","Hide Note")%>:</label>
						</div>
						<div class="bold col-sm-2 col-12">
							<input name="ishidden" value="true" style="margin-left: 132px; margin-top: 14px;" type="checkbox" <%: item.IsHidden ? "checked='checked'" : ""%>>
							<input name="ishidden" value="false" type="hidden" />
						</div>
						<div class="clearfix"></div>
					</div>

					<div class="x_content col-12" style="margin-left: -5px !important;">
						<div class="bold col-12">
							<label for="OverriddenNote<%: item.AccountThemeID%>" style="margin-left: 34px; vertical-align: top"><%: Html.TranslateTag("Settings/AdminAnnouncementDetails|Override ReleaseNote","Override ReleaseNote")%>:</label>
						</div>
						<div class="col-sm-4 col-12">
							<textarea cols="20" id="OverriddenNote<%: item.AccountThemeID%>" name="overridennote" rows="2" style="height: 108px; width: 341px; resize: none; margin-left: 72px;"><%: item.OverriddenNote %></textarea>
						</div>
						<div class="clearfix"></div>    
					</div>

					<div class="clearfix"></div>
					<hr />
					<div>
						<input type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="gen-btn" style="padding:3px 20px;" />
					</div>
				<%} %>
            </form>
        </div>
    </div>
	<%} %>
</asp:Content>