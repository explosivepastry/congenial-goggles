<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	string sku = Model.SKU;
	if (string.IsNullOrEmpty(sku))	
		sku = "MNS-9-W2-TS-ST";	
%>

<div id="DeviceInfoApplications"></div>

<script type="text/javascript">
	$(document).ready(function () {
		DeviceInfoApplications();
	});

	function DeviceInfoApplications() {
		var sku = '<%=sku%>';

		$.ajax({
			url: 'http://staging.monnit.com/ProductManagement/DeviceInfoApplications',
			type: 'get',
			async: false,
			data: {
				"sku": sku
			},
			success: function (returndata) {
				alert(returndata);
				$("#DeviceInfoApplications").html(returndata);
			}
		});
		SetTargetToBlank();
	}

	function SetTargetToBlank() {
		//$('a[href^="https://"]').not('a[href*=imonnit]').attr('target', '_blank');

		$('a').each(function () {
			var a = new RegExp('/' + window.location.host + '/');
			if (!a.test(this.href)) {
				$(this).attr("target", "_blank");
			}
		});
	}
</script>
