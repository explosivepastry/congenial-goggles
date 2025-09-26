<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.AccountSearchModel>>" %>
<% 

    Account account = ViewBag.TargetAccount ?? new Account();
%>



<%
    if (Model.Count() == 0)
    {

%>
<div class="x_title col-12 d-flex align-items-center" style="margin-bottom: 20px; width: 100%!important;">
    <div class="col-12">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("No Results")%></h2>
    </div>
</div>
<%
    }
    else
    {
%>
<div class="x_title col-12 d-flex align-items-center" style="margin-bottom: 20px; width: 100%!important;">
    <div class="col-6">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Account Tree", "Account Tree")%></h2>
    </div>
    <div class="col-3">
        <h2></h2>
    </div>
    <div class="col-3">
        <h2 style="width: 100%!important;"><%: Html.TranslateTag("Primary Contact", "Primary Contact")%></h2>
    </div>


</div>

<div class="row hasScroll">
    <%foreach (AccountSearchModel item in Model)
        {%>
    <div class="col-12 d-flex align-items-center" id="listrow_<%: item.AccountID%>" style="max-width: 100%;">
        <div class="col-6" style="overflow: hidden;" title="Company : <%=item.CompanyName %>&#13;Account ID: <%=item.AccountID %>&#13;Account Number: <%=item.AccountNumber %>">
            <div class=" locate-breadcrumbs">
            <%if (item.AccountTree.Count() > 0)
                {
                    for (int i = 0; i < item.AccountTree.Length; i++)
                    {
                        string accountName = HttpUtility.HtmlDecode(item.AccountTree[i]);%>
            <div class="locate-name" style="cursor:default;"><%=accountName %></div>
                <%
                    if (i < (item.AccountTree.Length - 1))
                    {
                %>
                <div class="lbc-arrow">
                    <svg class="arrow-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512" style="fill: var(--primary-color)">
                        <path d="M246.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-160 160c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L178.7 256 41.4 118.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l160 160z" />
                    </svg>
                </div>
                <%
                    }
                %>
            <%}
                }
                else
                {%>
		            N/A
                <%} %>
        </div></div>
        <div class="col-3">
            <!-- Assign Parent button-->
            <button type="button" class="btn btn-primary assignParentBtn" data-id="<%=item.AccountID %>">Assign as Parent</button>
        </div>
        <div class="col-3">
            <%= item.FirstName + " " + item.LastName%>
        </div>
    </div>
    <hr />
    <%}%>
</div>


<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("AssignParentList.ascx") %>

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    var alreadyparent = '<%: Html.TranslateTag("Settings/AccountEdit|This account already set as parent", "This account already set as parent") %>';

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

    $('.assignParentBtn').click(function () {
        var acctnumber = $(this).attr('data-id');

        if (acctnumber == '<%=account.RetailAccountID%>') {
            showSimpleMessageModal(alreadyparent);
        } else {
            var url = "/Account/UpdateAccountParent?accountID=" + '<%=account.AccountID%>' + "&parentID=" + acctnumber;
            $.post(url, function (data) {
                if (data == "Success") {
                    window.location.href = '/Settings/LocationOverview/' + acctnumber;
                }
                else {
                    //alert(data);
                    showSimpleMessageModal(data);
                }
            });
        }
    });
</script>
<%
    }
%>