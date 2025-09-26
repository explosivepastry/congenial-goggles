<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>


<div class="x_title">
	<h2 style="overflow: unset; text-overflow: unset;"><%: Html.TranslateTag("Overview/SensorHud|Recent Readings","Recent Readings")%></h2>
	<div class="clearfix"></div>
</div>
<div class="x_content" style="padding-bottom: 20px; max-height: 220px; margin-top: -10px; cursor: pointer; text-align: center; overflow-y: scroll !important;" title="View History" onclick="location.href='/Overview/SensorHome/<%=Model.sensor.SensorID %>';">

	<%  if (Model.DataMessageList.Count > 0)
		{
			Sensor sensor = Model.sensor;
			foreach (var item in Model.DataMessageList.Take(4))
			{%>

	<div class="col-12" id="dataInfo" style="font-size: 16px; height: auto; cursor: pointer;" title="View History">
		<div class="col-xs-4 col-sm-4 col-md-4 historyReading" style="padding-top: 10px;"><%:item.DisplayData %></div>

		<div class="col-xs-4 col-sm-4 col-md-4 historyDate" style="text-align: center; padding-top: 10px;"><%:item.MessageDate.OVToLocalDateTimeShort() %></div>
		<div class="col-xs-4 col-sm-4 col-md-4" style="font-size: 0.7em; text-align: center;">
			<div class="col-xs-6 col-sm-6 col-md-6 gatewaySignal sigIcon" style="font-size: 9px!important;">
				<%if (item != null)
					{
						if (sensor.IsPoESensor)
						{%>
				<div class="icon icon-ethernet-b"></div>
				<%}
					else
					{
						int Percent = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, item.SignalStrength);
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
				<div title="<%=Percent %>" style="font-size: 0.85em; padding-top: 5px; margin-right: -18px;" class="icon iconSignal icon-signal<%:signal %>"></div>
				<%}
					}%>
			</div>

			<%
				string battLevel = "";
				string battType = "";
				string battModifier = "";
				if (item != null)
				{
					if (item.Battery <= 0)
					{
						battLevel = "-0";
						battModifier = " batteryCritical batteryLow";
					}
					else if (item.Battery <= 10)
					{
						battLevel = "-1";
						battModifier = " batteryCritical batteryLow";
					}
					else if (item.Battery <= 25)
					{
						battLevel = "-2";
					}
					else if (item.Battery <= 50)
						battLevel = "-3";
					else if (item.Battery <= 75)
						battLevel = "-4";
					else
						battLevel = "-5";

					if (sensor.PowerSourceID == 3 || item.Voltage == 0)
					{
						battType = "-line";
						battLevel = "";
					}
					else if (sensor.PowerSourceID == 4)
					{
						battType = "-volt";
						battLevel = "";
					}
					else if (sensor.PowerSourceID == 1 || sensor.PowerSourceID == 14)
						battType = "-cc";
					else if (sensor.PowerSourceID == 2 || sensor.PowerSourceID == 8 || sensor.PowerSourceID == 10 || sensor.PowerSourceID == 13 || sensor.PowerSourceID == 15 || sensor.PowerSourceID == 17 || sensor.PowerSourceID == 19)
						battType = "-aa";
					else if (sensor.PowerSourceID == 6 || sensor.PowerSourceID == 7 || sensor.PowerSourceID == 9 || sensor.PowerSourceID == 16 || sensor.PowerSourceID == 18)
						battType = "-ss";
					else
						battType = "-gateway";

				}%>

			<div style="font-size: 0.7em; padding-top: 5px;" class="col-xs-6 col-sm-6 col-md-6 battIcon  icon icon-battery<%:battType %><%:battLevel %><%:battModifier %>" title="<%=item.Battery %>">
				<%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", item.Voltage) : "" %>
			</div>

		</div>
	</div>
	<%} %>
	<%}
        else
        { %>
	<br />
	<span style="font-size: 2.0em; text-align: center !important;"><%: Html.TranslateTag("Overview/SensorHud|No Readings","No Readings")%></span>
	<%} %>
	<%--<div class="clearfix"></div>--%>
	<!-- History-->
	<%--<hr style="margin: 0px;" />--%>
</div>

