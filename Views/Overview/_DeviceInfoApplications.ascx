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
            url: 'https://www.monnit.com/ProductManagement/DeviceInfoApplications',
			type: 'get',
			async: false,
			data: {
				"sku": sku
			},
			success: function (returndata) {
				returndata = returndata.replace(/http:/g, "https:").replace(/stagingnew/g, "www");
				returndata == "" 
                    || returndata.includes("<%: Html.TranslateTag("Overview/_DeviceApplications|Product not found","Product not found")%>")
                    || returndata.includes("<%: Html.TranslateTag("Overview/_DeviceApplications|Looks like something went wrong","Looks like something went wrong")%>")
					? $("#applications").hide()
					: $("#DeviceInfoApplications").html(returndata)
            },
			error: function (returndata) {
                $("#supportDocs").hide();
            }
		});
	}
</script>
