<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--<%if (!Model.IsWiFiSensor)
	{%>
<div style="display: none;">
	<input type="checkbox" name="TransmitOffsetChk" id="TransmitOffsetChk" <%=Model.TransmitOffset > 0 ? "checked" : "" %>"/>
</div>

<script type="text/javascript">
	$(document).ready(function () {
		if ($('#TransmitOffsetChk').prop('checked')) {
			$('#TransmitOffset').val(5);
		} else {
			$('#TransmitOffset').val(0);
		}
	});
</script>
<%}%>--%>