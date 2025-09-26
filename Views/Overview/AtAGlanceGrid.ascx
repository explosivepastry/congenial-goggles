<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.SensorGroupSensorModel>>" %>
<%="" %>
<div class="container">
	<div class="row display-flex">

		<%if (Model.Count == 0)
			{ %>
		<div class="col-md-12 col-xs-12">
			<div class="gridPanel">
				<h2>No Sensors found. To add a sensor click <a style="text-decoration: underline;" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>">Here</a>.</h2>

			</div>
		</div>


		<%
			}
			else
			{
				foreach (var item in Model)
				{

					DataMessage message = item.DataMessage;
		%>

		<div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
			<div class="gridPanel">
				<!----------------- Link for Sensor Home ----------------->
				<a href="/Overview/SensorChart/<%=item.Sensor.SensorID %>">
					<table width="100%">
						<tr class="chipHover viewSensorDetails <%:item.Sensor.SensorID %>" id="SensorHome<%=item.Sensor.SensorID %>">
							<td width="8.5%">
								<div class="divCellCenter holder holder<%:item.Sensor.Status.ToString() %>">

									<div class="sensor sensorIcon sensorStatus<%:item.Sensor.Status.ToString() %>">
										<%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
									</div>

								</div>
							</td>

							<td class="" style="padding: 0px; vertical-align: middle;" width="74.5%">
								<div class="glance-text">
									<div class="glance-name"><%= item.Sensor.SensorName%></div>
									<%if (message != null)
										{ %>
									<div class="glance-reading"><%: message.DisplayData%></div>
									<%} %>
								</div>
							</td>

							<td width="16.5%" style="padding: 0px!important; float: right; margin-right: 2px;">
								<!--<img src="../../Content/images/iconmonstr-battery-7-240.png" width="20px" height="auto" style="margin-right: 5px;">-->
								<div class="col-md-1 col-xs-2 AlignTop" style="margin-top: 5px;">
									<div class="gatewaySignal sigIcon" style="float: right; font-size: 0.6em!important;">

										<%if (message != null)
											{
												if (item.Sensor.IsPoESensor)
												{ %>
										<div class="icon icon-ethernet-b"></div>
										<%}
											else
											{
												int Percent = DataMessage.GetSignalStrengthPercent(item.Sensor.GenerationType, item.Sensor.SensorTypeID, message.SignalStrength);
												string signal = "";

												if (Percent <= 0)
													signal = "-0";
												else if (Percent <= 10)
													signal = "-1";
												else if (Percent <= 25)
													signal = "-2";
												else if (Percent <= 50)
													signal = "-3";
												else if (Percent <= 75)
													signal = "-4";
												else
													signal = "-5";
										%>
										<div style="float: right; font-size: 0.7em!important;" class="icon iconSignal icon-signal<%:signal %>"></div>
										<%}
										}%>
									</div>
								</div>
								<br>
								<!--<img src="../../Content/images/iconmonstr-battery-7-240.png" width="20px" height="auto" style="margin-right: 5px;">-->
								<div class="col-md-1 col-xs-2 AlignTop" style="margin-top: 8px;">
									<%string battLevel = "";
										string battType = "";
										string battModifier = "";
										if (message != null)
										{
											if (message.Battery <= 0)
											{
												battLevel = "-0";
												battModifier = " batteryCritical batteryLow";
											}
											else if (message.Battery <= 10)
											{
												battLevel = "-1";
												battModifier = " batteryCritical batteryLow";
											}
											else if (message.Battery <= 25)
											{
												battLevel = "-2";
											}
											else if (message.Battery <= 50)
												battLevel = "-3";
											else if (message.Battery <= 75)
												battLevel = "-4";
											else
												battLevel = "-5";

											if (item.Sensor.PowerSourceID == 3 || message.Voltage == 0)
											{
												battType = "-line";
												battLevel = "";
											}
											else if (item.Sensor.PowerSourceID == 4)
											{
												battType = "-volt";
												battLevel = "";
											}
											else if (item.Sensor.PowerSourceID == 1 || item.Sensor.PowerSourceID == 14)
												battType = "-cc";
											else if (item.Sensor.PowerSourceID == 2 || item.Sensor.PowerSourceID == 8 || item.Sensor.PowerSourceID == 10 || item.Sensor.PowerSourceID == 13 || item.Sensor.PowerSourceID == 15)
												battType = "-aa";
											else if (item.Sensor.PowerSourceID == 6 || item.Sensor.PowerSourceID == 7 || item.Sensor.PowerSourceID == 9 || item.Sensor.PowerSourceID == 16 || item.Sensor.PowerSourceID == 18 || item.Sensor.PowerSourceID == 17)
												battType = "-ss";
											else
												battType = "-gateway";

										}%>
									<div style="float: right; font-size: 0.6em!important;" class="battIcon  icon icon-battery<%:battType %><%:battLevel %><%:battModifier %>">
										<%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "" %>
									</div>
								</div>
								<br>
								<div class="col-md-1 col-xs-2" style="margin-top: 12px;">
									<span style="margin-left: -17px; <%= item.Sensor.CanUpdate ?  "display:none;" : ""%>" title="Settings Update Pending" class="showPendingIcon pendingEditList pendingsvg"><%=Html.GetThemedSVG("Pending_Update")%></span>
								</div>
							</td>
						</tr>
					</table>
				</a>
			</div>
		</div>
		<% }
			} %>
	</div>
</div>



<script type="text/javascript">
    <% if (MonnitSession.SensorListFilters.CSNetID > long.MinValue ||
MonnitSession.SensorListFilters.ApplicationID > long.MinValue ||
MonnitSession.SensorListFilters.Status > int.MinValue ||
MonnitSession.SensorListFilters.Name != "")
		Response.Write("$('#filterApplied').show();");
	else
		Response.Write("$('#filterApplied').hide();");
    %>

	$(document).ready(function () {
		$('#filterdSensors').html('<%:Model.Count%>');
		$('#totalSensors').html('<%:ViewBag.TotalSensors%>');


		$('.sortable').click(function (e) {
			e.preventDefault();

			$.get('/Overview/SensorSortBy', { column: $(this).attr("href"), direction: $(this).attr("data-direction") }, function (data) {
				if (data == "Success")
					getMain();
				else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
			});
		}).css('font-weight', 'bold').css('text-decoration', 'none');
	});

	var tabToShow = '';
</script>
