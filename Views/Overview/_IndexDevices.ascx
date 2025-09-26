<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<!-- ==========================
            - Sensor List Container
        ============================-->

<div class="d-flex w-100 headerCard  ruleTabs" style="align-items: center; gap: 10px;">
	<div class="top-icon"><%:Html.GetThemedSVG("circle-sensor-blu") %></div>
	<span style="width: 100%">Sensor List</span>
</div>

<div id="sensorList" class="sensor-list-flow scroll-arrow containRule">
	<div title="Scroll to see more sensors" class="scroll-arrow-icon"><%=Html.GetThemedSVG("arrowRight") %></div>
	<%if (MonnitSession.OverviewHomeModel.Sensors.Count < 1)
		{%>
	<a href="/setup/QASteps">
		<div class="d-flex linex">
			<div class="redDot"></div>
			<div class="icon-update"><%:Html.GetThemedSVG("sensor") %></div>
			Add a<span style="color: var(--primary-color); font-weight: bold;">Device</span>for this account.
		</div>
	</a>
	<%}%>

	<%

		foreach (var item in MonnitSession.OverviewHomeModel.Sensors)
		{%>
	<a href="/Overview/SensorHome/<%=item.Sensor.SensorID%>" onmouseover="$(this).css('color', 'var(--primary-color)').css('font-weight', 'bold');"
		onmouseout="$(this).css('color', '').css('font-weight', '');;">

		<%
			if (item.Sensor.Status == eSensorStatus.OK)
			{
		%>
		<div class="d-flex linex">
			<div class="greenDot"></div>
			<span class="report_name" style="max-width: 288px;"><%=item.Sensor.SensorName %></span>
		</div>
		<%
			}

			if (item.Sensor.Status == eSensorStatus.Offline)
			{
		%>
		<div class="d-flex linex">
			<div class="greyDot"></div>
			<span class="report_name" style="max-width: 288px;"><%:item.Sensor.SensorName %></span>
		</div>
		<%
			}

			if (item.Sensor.Status == eSensorStatus.Alert)
			{
		%>

		<div class="d-flex linex">
			<div class="redDot"></div>
			<span class="report_name" style="max-width: 288px;"><%:item.Sensor.SensorName %></span>
		</div>
		<%
			}

			if (item.Sensor.Status == eSensorStatus.Warning)
			{
		%>
		<div class="d-flex linex">
			<div class="yellowDot"></div>
			<span class="report_name" style="max-width: 288px;"><%:item.Sensor.SensorName %></span>
		</div>
		<%}%>
	</a>
	<%}%>
</div>
<script>
	/*      Scroll arrow function*/


	// Get the container element
	var container = document.querySelector('.sensor-list-flow');

	// Get the scroll-arrow-icon element
	var scrollArrowIcon = document.querySelector('.scroll-arrow-icon');

	// Add scroll event listener to the container
	container.addEventListener('scroll', function () {
		// Check if the container is being scrolled
		if (container.scrollTop > 0) {
			// Hide the scroll-arrow-icon
			scrollArrowIcon.style.display = 'none';
		} else {
			// Show or hide the scroll-arrow-icon based on content height
			var containerHeight = container.clientHeight;
			var contentHeight = container.scrollHeight;

			if (contentHeight > containerHeight) {
				// Show the scroll-arrow-icon
				scrollArrowIcon.style.display = 'block';
			} else {
				// Hide the scroll-arrow-icon
				scrollArrowIcon.style.display = 'none';
			}
		}
	});

	// Check if the scroll-arrow-icon needs to be initially hidden
	var containerHeight = container.clientHeight;
	var contentHeight = container.scrollHeight;

	if (contentHeight <= containerHeight) {
		// Hide the scroll-arrow-icon initially
		scrollArrowIcon.style.display = 'none';
	}

	// Resize the sensor list to grown and shrink with the outer card.
	const sensorListCard = document.querySelector("#HomePageSensorCard"); 
	const sensorList = document.querySelector("#sensorList");

	const handleResizeRule = () => {
		let heightToCopy = sensorListCard.offsetHeight - 10;
		if (heightToCopy > 900) {
			heightToCopy = 900;
		}
        sensorList.style.maxHeight = heightToCopy + 'px'; 
    }

    handleResizeRule();
    window.addEventListener('resize', handleResizeRule)
</script>

