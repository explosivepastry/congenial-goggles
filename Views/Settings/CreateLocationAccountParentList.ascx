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
    
    <div class="col-3">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Parent","Parent")%></h2>
    </div>
    <div class="col-3">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Company","Company")%></h2>
    </div>
    <div class="col-3">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Primary Contact","Primary Contact")%></h2>
    </div>
    <div class="col-3">
        <h2></h2>
    </div>
</div>

<div class="row hasScroll">
    <%foreach (AccountSearchModel item in Model)
    {%>
        <div class="col-12 d-flex align-items-center" id="listrow_<%: item.AccountID%>" style="max-width:100%;">
            <div class="col-3" style="overflow: hidden;" title="Company : <%=item.CompanyName %>&#13;Account ID: <%=item.AccountID %>&#13;Account Number: <%=item.AccountNumber %>">
                <%string parentName = "";
                  if (item.AccountTree.Count() > 2)
                  {
                    parentName = HttpUtility.HtmlDecode(item.AccountTree[item.AccountTree.Length - 2]);
                  }%>
                <%=parentName %>
            </div>
            <div class="col-3" style="overflow-wrap:break-word;">
                <!-- Company -->
                <%= item.CompanyName%>
            </div>
            <div class="col-3">
                <!-- Primary Contact -->
                <%= item.FirstName + " " + item.LastName%>
            </div>
            <div class="col-3">
                <!-- Assign Parent button-->
                <button type="button" class="btn btn-primary assignParentBtn" data-id="<%=item.AccountID %>" data-name="<%=item.CompanyName %>">Assign as Parent</button>
            </div>
        </div>
        <hr />
    <%}%>
</div>


<script type="text/javascript">
    $('.assignParentBtn').click(function () {
        var id = $(this).attr('data-id');
        var name = $(this).attr('data-name');

        $('#ParentAccountID').val(id);
        $('#parentLocationName').html(name);

        $('#parentSearchResultsModal .btn-close').click();
        $('#companyName').focus().select();
    });
</script>