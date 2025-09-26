<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AccountLocationOverviewHeaderModel>" %>

<%
//AccountLocationOverviewCustomModel overviewModel = new AccountLocationOverviewCustomModel();
//List<AccountLocationOverviewCustomModel> alom = AccountLocationOverviewCustomModel.LocationOverview(Request.RequestContext.RouteData.Values["id"].ToLong());
//var first = alom.FindAll(m => m.AccountID == Request.RequestContext.RouteData.Values["id"].ToLong()).FirstOrDefault();
//AccountLocationOverviewCustomModel overviewModel = alom.Find(m => m.AccountID == Request.RequestContext.RouteData.Values["id"].ToLong());

//if (Model != null && Model.Count > 0)
//    overviewModel = Model.FirstOrDefault();
%>
<style>
	.top-location-card {
		cursor: auto;
	}

		.top-location-card:hover {
			box-shadow: unset;
			transform: unset;
		}
</style>
<div class="formtitle col-12 " style="margin-top: 28px;">

	<div id="MainTitle" class="col-12 col-lg-3 dashboardCard_row">
		<div class="top-location-card db-1" onclick="filterAccounts('OK');">
			<div class=" dashboardCard__icon ">
				<%=Html.GetThemedSVG("db-white-location") %>
			</div>
			<div class="dbCard_title">
				<div class="dashboardCard__title__type">

					<%: Html.TranslateTag("Locations","Locations")%>
				</div>
				<div class="dashboardCard__title__number">
					<%--<%=overviewModel.SubAccounts%>--%>
					<%=Model.LocationCount%>
				</div>
			</div>
		</div>

		<div class="top-location-card db-2" onclick="filterAccounts('Alerting');">
			<div class=" dashboardCard__icon ">
				<%=Html.GetThemedSVG("db-white-alert") %>
			</div>
			<div class="dbCard_title">
				<div class="dashboardCard__title__type">
					<%: Html.TranslateTag("Alerting","Alerting")%>
				</div>
				<div class="dashboardCard__title__number">
					<%=Model.AlertCount  %>
				</div>
			</div>
		</div>

		<div class="top-location-card db-3" onclick="filterAccounts('Warning');">
			<div class="dashboardCard__icon">
				<%=Html.GetThemedSVG("db-white-lowbat") %>
			</div>
			<div class="dbCard_title">
				<div class="dashboardCard__title__type">
					<%: Html.TranslateTag("Warning")%>
				</div>
				<div class="dashboardCard__title__number">
					<%=Model.WarningCount  %>
				</div>
			</div>
		</div>

		<div class="top-location-card db-4" onclick="filterAccounts('Offline');">
			<div class="dashboardCard__icon">
				<%=Html.GetThemedSVG("db-white-wifioff") %>
			</div>
			<div class="dbCard_title">
				<div class="dashboardCard__title__type">
					<%: Html.TranslateTag("Offline")%>
				</div>
				<div class="dashboardCard__title__number">
					<%=Model.OfflineCount  %>
				</div>
			</div>
		</div>
	</div>

</div>
<style>
	.top-location-card {
		cursor: pointer;
	}
</style>
<script>
	let currentFilter = null;
	function filterAccounts(type) {
		console.log(type);
		if (currentFilter) {
			$('.searchCardDiv.corp-card').show()
			currentFilter = null;
		} else {
			$('.searchCardDiv.corp-card').hide()
			currentFilter = 'sensorStatus' + type;
			$('.searchCardDiv.corp-card').filter((i, x) => { return $(x).find('.corp-status.' + currentFilter).length == 1; }).show();
		}		
	}

</script>
