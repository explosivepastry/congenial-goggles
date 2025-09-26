<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--Hysteresis--%>

<%	
	if (!Monnit.VersionHelper.IsVersion_1_0(Model))
	{
		string Hyst = "";
		MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);%>

		<input type="hidden" name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
		<%: Html.ValidationMessageFor(model => model.Hysteresis)%>
<%} %>
