<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AccountLocationSearchModel>>" %>

<%foreach (AccountLocationSearchModel location in Model)
  {
        if (location.SubAccounts > 1)
        {%>
            <%:Html.Partial("CorporateCards", location) %>
      <%}
        else
        { %>
            <%:Html.Partial("CorporateLeafCards", location) %>
      <%}
  }%>

<script>
	$('.searchCardDiv.corp-card').on('contextmenu', function (e) {
		e.preventDefault();
	})
    $(function () {
        $('.accountCard').click(function (e) {
            e.stopPropagation();
            e.preventDefault();

            var id = $(this).attr('data-id');
            $('#' + id).click();
        });

        $('.favoriteItem').each(function () {
            var obj = $(this);
            var svgObj = obj.find('svg');
            var isFavorite = obj.attr('data-fav') == 'true';

            if (isFavorite) {
                svgObj.addClass('liked');
            } else {
                svgObj.removeClass('liked');
            }
        });
    });
</script>
