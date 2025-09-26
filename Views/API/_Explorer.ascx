<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

 <div class="rule-card_container" style="min-height: 360px;">
	<div class="card_container__top">
		<div id="explorer_top" class="card_container__top__title dfjcsb">
			<span><%: Html.TranslateTag("API/_Explorer|API Explorer","API Explorer")%></span>
		</div>
	</div>
	<div class="x_content">
		<div class="card__container__body">
			<div class="col-12 card_container__body__content">

				<div class="card_container__body">
					<h2><%: Html.TranslateTag("EndPoint","EndPoint")%></h2>
					<div class="col-lg-12" style="padding-bottom: 10px; display: flex;">
						<label id="endpoint"><%: Html.TranslateTag("API/_Explorer|Choose an API from the dropdown","Choose an API from the dropdown")%></label>
					</div>
				</div>
				<div class="clearfix"></div>
				<br />

				<div class="card_container__body">
					<h2><%: Html.TranslateTag("API/_Explorer|Response Format","Response Format")%></h2>
					<div class="form-check form-switch d-flex ps-2 align-items-center">
                        <label class="form-check-label" ><%: Html.TranslateTag("JSON","JSON")%></label>
                        <input id="requestFormat" class="form-check-input mx-2 my-0s" type="checkbox">
                        <label class="form-check-label" ><%: Html.TranslateTag("XML","XML")%></label>
                    </div>
				</div>
				<div class="clearfix"></div>
				<br />

				<div class="card_container__body">
					<h2><%: Html.TranslateTag("API/_Explorer|Request URL","Request URL")%></h2>
					<div class="col-12 input-group">
						<input class="form-control" disabled placeholder="" type="text" id="requestURL">
						<input id="post_btn" type="button" class="btn btn-primary" value="POST" style="font-weight: 600;" disabled>
					</div>
				</div>
				<div class="clearfix"></div>
				<br />

				<form id="post_request_parameters">
					<div class="card_container__body" id="HeaderSection">
						<h2><%: Html.TranslateTag("API/_Explorer|Headers (Authentication)","Headers (Authentication)")%></h2>

						<div class="col-lg-12" style="padding-bottom: 10px; display: flex;">
							<table id="HeaderTable" class="table table-hover" style="margin-bottom: 0px;">
								<thead>
									<tr>
										<th style="max-width: 12px;"></th>
										<th scope="col"><%: Html.TranslateTag("KEY","KEY")%></th>
										<th scope="col"><%: Html.TranslateTag("VALUE","VALUE")%></th>
									</tr>
								</thead>
								<tbody>
									<tr>
									</tr>
								</tbody>
							</table>
						</div>
						<div class="clearfix"></div>
						<br />
					</div>

					<div class="card_container__body" id="ParamSection">
						<h2><%: Html.TranslateTag("Parameters","Parameters")%></h2>
						<div class="col-lg-12" style="padding-bottom: 10px; display: flex;">
							<table id="ParamTable" class="table table-hover" style="margin-bottom: 0px;">
								<thead>
									<tr>
										<th style="max-width: 12px;"></th>
										<th scope="col"><%: Html.TranslateTag("KEY","KEY")%></th>
										<th scope="col"><%: Html.TranslateTag("VALUE","VALUE")%></th>
									</tr>
								</thead>
								<tbody>
									<tr>
									</tr>
								</tbody>
							</table>
						</div>
						<div class="clearfix"></div>
					</div>
				</form>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	$(document).ready(function () {

		var emptyrowwithdescription = '<tr><td><input class="form-control tableTextBox" type="text" style="width: 100%;" value=""></td><td><input class="form-control tableTextBox" type="text" style="width: 100%;" value=""></td><td><input class="form-control tableTextBox" type="text" style="width: 100%;" value=""></td><td class="col-xs-1 text-center"><a href="#" onClick="deleteRow(this)"><i class="fa fa-times" aria-hidden="true"></a></td></tr>';
		var emptyrow = '<tr><td><input class="form-control tableTextBox" type="text" style="width: 100%;" value=""></td><td><input class="form-control tableTextBox" type="text" style="width: 100%;" value=""></td><td class="col-xs-1 text-center"><a href="#" onClick="deleteRow(this)"><i class="fa fa-times" aria-hidden="true"></a></td></tr>';

		$('#addParamRow').click(function (e) {
			$('#ParamTable > tbody:last-child').append(emptyrow);
		});

		$('#addHeaderRow').click(function (e) {
			$('#HeaderTable > tbody:last-child').append(emptyrow);
		});
	});

	function deleteRow(trash) {
		$(trash).closest('tr').remove();
	};

	function requestURLBuilder() {
		var url = "/api/testurl/"
		$('#requestURL').text(url);
	}

</script>

<style type="text/css">
	.tableTextBox {
		border: 1px solid #e3e3e3;
		border-radius: 2px 2px 2px 2px;
		height: 30px !important;
		padding-left: 10px;
		-webkit-box-shadow: none;
	}

	.tableTextBox {
		border-color: #FFFFFF;
	}

	.tableTextBox:hover {
		border-color: #EEEEEE;
	}

	.tableTextBox:focus {
		border-color: #EEEEEE;
	}

	tr:focus-within {
		background: #F8F8F8;
	}

	table {
		border-bottom: 1px solid #e3e3e3;
	}

	i {
		margin-top: 8px;
		color: darkblue;
	}

	th {
		font-weight: 800;
	}

	#endpoint {
		font-weight: 800;
		font-size: 16px;
		color: purple;
	}

</style>
