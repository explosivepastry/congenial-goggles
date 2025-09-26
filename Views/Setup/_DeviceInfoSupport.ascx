<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	string sku = Model.SKU;
	if (string.IsNullOrEmpty(sku))
		sku = "MNS-9-W2-TS-ST";
%>

<div id="DeviceInfoSupport"></div>

<script type="text/javascript">
	$(document).ready(function () {
		DeviceInfoSupport();
	});

	function DeviceInfoSupport() {
		var sku = '<%=sku%>';

		$.ajax({
            url: 'https://www.monnit.com/ProductManagement/DeviceInfoSupport',
			type: 'get',
			async: false,
			data: {
				"sku": sku
			},
			success: function (returndata) {
				$("#DeviceInfoSupport").html(returndata);
			}
		});
		$('a[href^="https://"]').not('a[href*=imonnit]').attr('target', '_blank');
	}
</script>
<style>
	a {
		color: #0B84D4;
	}

	.fa-question-circle {
		color: #08AFE6;
		width: 16px;
		margin-right: 5px;
	}

	.fa-book {
		color: #6F7378;
		width: 16px !important;
		height: 16px;
		margin-right: 5px;
	}
		
	.fa-file-pdf {
	color: #C72128;
	width: 12px;
	height: 16px;
	margin-left: 2px;
	margin-right: 5px;
	}
</style>
