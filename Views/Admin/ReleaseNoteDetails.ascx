<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.AccountThemeReleaseNoteLink>>" %>
<%foreach (var item in Model) {
        AccountTheme ItemTheme = Monnit.AccountTheme.Load(item.AccountThemeID);
        %>
	<form action="/Admin/ReleaseNoteDetails" method="post">
        <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin) { %>
        <div style="border: 1px solid black;">

            <div class="formtitle">
                <%: Html.Label(ItemTheme.Theme, new { @style = "font-size:20pt;" })%>
                <%: ItemTheme.Domain%>
            </div>
            <div class="formbody">
                <%: Html.Hidden("releasenoteid",item.ReleaseNoteID)%>
                <%: Html.Hidden("accountthemeid",item.AccountThemeID)%>
                <div>
                    <label for="IsHidden<%: item.AccountThemeID%>" style="margin-left: 34px;">Hide Note:</label>
                    <input name="ishidden" value="true" style="margin-left: 132px; margin-top: 14px;" type="checkbox" <%: item.IsHidden ? "checked='checked'" : ""%>>
                    <input name="ishidden" value="false" type="hidden" />
                </div>
                <div style="height: 108px; margin-top: 11px;">
                    <label for="OverriddenNote<%: item.AccountThemeID%>" style="margin-left: 34px; vertical-align: top">Override ReleaseNote:</label>

                    <textarea cols="20" id="OverriddenNote<%: item.AccountThemeID%>" name="overridennote" rows="2" style="height: 108px; width: 341px; resize: none; margin-left: 72px;"><%: item.OverriddenNote %></textarea>
                </div>
            </div>
            <div class="button">
                <input type="button" value="Save" style="margin-right: 333px;" onclick="postForm($(this).closest('form'))" title="Save" class="bluebutton submitted " />
        
                <div style="clear: both"></div>
            </div>
        </div>
        <%} %>
        <% if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && (MonnitSession.IsCurrentCustomerMonnitAdmin /*|| MonnitSession.IsCurrentCustomerReseller*/)) { %>
            <% if (item.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID) { %>
            <div style="border: 1px solid black;">

                <div class="formtitle">
                    <%: Html.Label(ItemTheme.Theme, new { @style = "font-size:20pt;" })%>
                    <%: ItemTheme.Domain%>
                </div>
                <div class="formbody">
                    <%: Html.Hidden("releasenoteid",item.ReleaseNoteID)%>
                    <%: Html.Hidden("accountthemeid",item.AccountThemeID)%>
                    <div>
                        <label for="IsHidden<%: item.AccountThemeID%>" style="margin-left: 34px;">Hide Note:</label>
                        <input name="ishidden" value="true" style="margin-left: 132px; margin-top: 14px;" type="checkbox" <%: item.IsHidden ? "checked='checked'" : ""%>>
                        <input name="ishidden" value="false" type="hidden" />
                    </div>
                    <div style="height: 108px; margin-top: 11px;">
                        <label for="OverriddenNote<%: item.AccountThemeID%>" style="margin-left: 34px; vertical-align: top">Override ReleaseNote:</label>

                        <textarea cols="20" id="Textarea1" name="overridennote" rows="2" style="height: 108px; width: 341px; resize: none; margin-left: 72px;"><%: item.OverriddenNote %></textarea>
                    </div>
                </div>
                <div class="button">
                    <input type="button" value="Save" style="margin-right: 333px;" onclick="postForm($(this).closest('form'))" title="Save" class="bluebutton submitted " />
                    <div style="clear: both"></div>
                </div>
            </div>
            <%} %>
        <%} %>
	</form>
<%} %>

<div class="buttons">
</div>