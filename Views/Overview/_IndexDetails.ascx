

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CSNet>" %>

<%
	List<Sensor> senslist = Sensor.LoadByCsNetID(Model.CSNetID);
	DataMessage.CacheLastByNetwork(Model.CSNetID, new TimeSpan(0,0,30));
%>

<div id="recentnotifications">
	<%Html.RenderPartial("_RecentNotifications", senslist); %>
</div>

<div id="sensorlist">
	<%Html.RenderPartial("_SensorList", senslist); %>
</div>

