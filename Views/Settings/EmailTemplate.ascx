<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

<div class="x_panel">
    <div class="x_title">
        <h2><%= Model.Name %></h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="form-group">
                <label class="col-xs-3" for="subject">
                    <%: Html.TranslateTag("Settings/EmailTemplate|Subject","Subject")%>
                </label>
                <div class="col-xs-9">
                    <%= Model.Subject%>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="form-group">
                <label class="col-xs-3" for="subject">
                    <%: Html.TranslateTag("Settings/EmailTemplate|Flags","Flags")%>
                </label>
                <div class="col-xs-9">
                    <%= Model.Flags%>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="form-group" style="margin-top: 1em; width: inherit; overflow: auto;">
                <div class="col-xs-12">
                    <% string temp = System.Net.WebUtility.HtmlDecode(Model.Template);  %>
                    <%:  Html.Raw(temp)%>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="form-group" style="padding-top: 10px;">
                <div class="col-lg-9 col-xs-12">
                </div>
                <div class="col-lg-3 col-xs-12">
                    <% if (MonnitSession.IsCurrentCustomerMonnitAdmin)
                        { %>
                    <a onclick="deleteTemplate(<%=Model.EmailTemplateID %>);" class="btn btn-dark"><%: Html.TranslateTag("Delete","Delete")%></a>
                    <% } %>
                    <a href="/Settings/TemplateEdit/<%:Model.EmailTemplateID%>" class="btn btn-primary"><%: Html.TranslateTag("Edit","Edit")%></a>
                    <div style="clear: both;"></div>
                </div>
            </div>
        </div>



    </div>
</div>

<script type="text/javascript">

    var AreYouSure = "<%: Html.TranslateTag("Settings/EmailTemplate|Are you sure you want to delete this template?")%>";

    function deleteTemplate(templateID) {

        if (confirm(AreYouSure)) {
            $.post("/Settings/TemplateDelete", { id: templateID }, function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else {

                    window.location.href = window.location.href;
                }
            });
        }

    }


</script>

