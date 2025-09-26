<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<string,string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Icons
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container-fluid">
		<div class="container-fluid card_container shadow-sm rounded mobile_mgntp20">
			<div class="card_container__top">
				<div class="card_container__top__title d-flex justify-content-between">
					<div class="col-12 dfac" style="justify-content: space-between">

						<div class="card_container__top__title__text ms-2">
							System Status
						</div>
						<div>
							<input type="text" id="keyWord" />
							<button id="keyWordSearch">Search</button>
							<button id="resetSearch">Reset</button>
						</div>
					</div>
				</div>
			</div>
			<div class="card_container__body">
				<div class="glanceView">
					<div class="col-12 scrollParentLarge">
						<div class="hasScroll">

							<% string value = string.Empty;
								foreach (string configDataKey in Model.Keys)
								{

									Model.TryGetValue(configDataKey, out value);
									if (string.IsNullOrEmpty(value))
									{
										value = string.Empty;
									}
							%>
							<div class="gridPanel col-12 d-flex px-3">
								<div class="col-3">
									<%: configDataKey %>
								</div>
								<div class="col-3">
									<%: value %>
								</div>
							</div>
							<% } %>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<script type="text/javascript">



</script>

	<style>
		.modal-backdrop {
			display: none;
		}

		.btn-active-fill .svg_icon, .btn .svg_icon {
			fill: #666 !important;
		}

		.btn-primary .svg_icon {
			fill: white !important;
		}
	</style>
	<script>
		$(document).ready(function () {
			
			var keyWord = window.location.href.split('=')[1];
			if (keyWord != undefined) {
				$('#keyWord').val(keyWord);
			}
		})
		$('#keyWordSearch').click(function () {

			window.location.assign(window.location.href.split('?')[0] + '?keyWord=' + $('#keyWord').val());
		});
		$('#resetSearch').click(function () {

			window.location.assign(window.location.href.split('?')[0]);
		});
	</script>

</asp:Content>
