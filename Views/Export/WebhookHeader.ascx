<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ExternalDataSubscriptionProperty>>" %>

<%  
    //purgeme
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore(); %>
<%

    foreach (ExternalDataSubscriptionProperty header in Model)
    {
        //string[] arr = header.StringValue.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
%>

<div class="row">
    <div class="col-md-5 col-xs-5">
        <%=header.StringValue %>
    </div>

    <div class="col-md-5 col-xs-5">
        <%=header.StringValue2 %>
    </div>

    <div class="col-md-2 col-xs-2">
        <a style="cursor: pointer;" id="property" title="<%: Html.TranslateTag("Export/WebhookHeader|Delete","Delete")%>" onclick="removeProperty('<%=header.ExternalDataSubscriptionPropertyID %>','header');">
            <i style="font-size: 1.5em;" class="fa fa-trash"></i>
        </a>
    </div>
</div>

<div class="clearfix"></div>
<%}%>
<!-- Detail Row End -->



