<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>


<div class="x_title">
	<h2 style="overflow: unset;"><%: Html.TranslateTag("Overview/SensorHud|Average Signal","Average Signal")%></h2>
	<div class="nav navbar-right panel_toolbox"></div>
	<div class="clearfix"></div>
</div>
<div class="x_content">
	<div class="col-12 col-lg-12" style="text-align: center;">
		<%
			if (Model.PreAggregateList.Count > 0)
			{
				if (Model.sensor.IsPoESensor)
				{%>
		<div class="icon icon-ethernet-b"></div>
		<%}
			else
			{

				double signalStrength = Model.PreAggregateList.Average(m => m.Avg_SignalStrength);
				int Percent = DataMessage.GetSignalStrengthPercent(Model.sensor.GenerationType, Model.sensor.SensorTypeID, signalStrength.ToInt());
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
		<div title="<%=Percent %>" style="margin-top: -20px; margin-bottom: -15px; font-size: 4.0em;" class="icon iconSignal icon-signal<%:signal %>"></div>
		<%}
			}
			else
			{ %>
		<span style="font-size: 2.0em;"><%: Html.TranslateTag("Overview/SensorHud|Offline", "Offline")%></span>
		<%} %>
		<br />
		<span style="font-size: 0.8em; margin-top: -10px;"><%: Html.TranslateTag("Overview/SensorHud|7 Day Average","7 Day Average")%></span>
	</div>
</div>

