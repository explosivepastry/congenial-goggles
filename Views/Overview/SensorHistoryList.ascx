<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>


<%--<div style="font-size: 1.4em">
    <div class="hidden-xs col-md-1"></div>
    <div class="col-xs-6 col-md-5" style="font-size: 0.9em;color:#515356;"><%: Html.TranslateTag("Overview/SensorHome|Reading","Reading")%></div>
    <div class="col-xs-6 col-md-5" style="font-size: 0.9em;color:#515356;"><%: Html.TranslateTag("Date","Date")%></div>
    <div class="hidden-xs col-md-1" style="font-size: 0.8em"></div>
</div>--%>
<div class="clearfix"></div>
<%--<br />--%>
<div id="dataList" class="dataList">	
</div>
<%--<div id="dataHistoryLoad"></div>--%>
<script type="text/javascript">
	$(document).ready(function () {
		loadView();
    });
   	
	function loadView() {
				
		var sensorID = '<%= Model.SensorID %>';
		var dataMsg = $('.historyReading1').first().attr('data-guid');		
		var returndata = null;

		$.ajax({
			url: '/Overview/SensorHistoryData',
			type: 'get',			
			async: false,			
			data: {
				"sensorID": sensorID,
				"dataMsg": dataMsg
			},
			success: function (returndata) {				
				$(".dataList").append(returndata);				
			}
		});					
	}
</script>
