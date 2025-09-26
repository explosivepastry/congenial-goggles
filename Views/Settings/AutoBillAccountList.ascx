<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.AccountSearchModel>>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>


<div class="x_title col-12 d-flex align-items-center" style="margin-bottom:20px; width:100%!important;">

    <div class="col-4">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Settings/AdminAccountList|Account Tree","Account Tree")%></h2>
    </div>
    <div class="col-md-2 d-none d-md-block">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Subscription","Subscription")%></h2>
    </div>
    <div class="col-md-2 d-none d-md-block">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Expiration","Expiration")%></h2>
    </div>
    <div class="col-md-2 d-none d-md-block">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("AutoBill To","AutoBill To")%> </h2>
    </div>
</div>

<div class="row hasScroll">
    <%Html.RenderPartial("_AutoBillAccountListRow", Model);%>
</div>


<script type="text/javascript">
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(document).ready(function () {
        $('#partialList').show();
    });

    $('.scrollParentLarge').on('show.bs.dropdown', '.df', function (e) {
        
        var dropdown = $(this).find('.dropdown-menu');

        dropdown.appendTo('body');
        $(this).on('hidden.bs.dropdown', function () {
            dropdown.appendTo(this);
        })
    });

</script>
