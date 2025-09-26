<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.EmailTemplate>>" %>

<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<% foreach (var item in Model)
    {
%>
<div class="col-12" style="padding: 10px;">
    <div class="form-group d-flex">
        <div class="bold col-6 col-md-6">
            <%=item.Name%><br />
            <span class="grey-container" style="font-size: 0.8em;"><%=item.Subject %></span>
        </div>
        <div class="col-6 col-md-6">
            <div class="dfjcfe">
                <a href="/Settings/TemplateEdit/<%:item.EmailTemplateID %>" title="Edit: <%= item.Name %>" class="btn btn-secondary btn-sm">
                    <span class="me-1"><%: Html.TranslateTag("Edit Template","Edit Template")%></span>
                    <%=Html.GetThemedSVG("edit") %>
                </a>
            </div>
        </div>
    </div>
</div>
<% } %>
<script>
    function showTemplate(templateID) {
        $.post("/Settings/showTemplate", { id: templateID }, function (data) {
            if (data == "Failed") {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            } else {

                $('#templateDetail').html(data);
            }
        });
    }
</script>
