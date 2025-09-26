<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ExternalDataSubscriptionProperty>>" %>

<%  
    //purgeme
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore(); %>
<%

    foreach (ExternalDataSubscriptionProperty cookie in Model)
    {
        string[] arr = cookie.StringValue.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
%>

<div class="row">
    <div class="col-md-5 col-xs-5">
        <%=cookie.StringValue %>
    </div>

    <div class="col-md-5 col-xs-5">
        <%=cookie.StringValue2%>
    </div>

    <div class="col-md-2 col-xs-2">
        <a style="cursor: pointer;" title="<%: Html.TranslateTag("Export/WebhookCookie|Delete","Delete")%>" onclick="removeProperty('<%=cookie.ExternalDataSubscriptionPropertyID %>','cookie');">
            <i style="font-size: 1.5em;" class="fa fa-trash"></i>
        </a>
    </div>
</div>

<div class="clearfix"></div>

<%}%>
<!-- Detail Row End -->


