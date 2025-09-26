<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_UseCaseOptions.ascx", Model);
%>

<div id="HiddenUntilUseCaseSelected">
	<div id="ValuesThatGetRefreshed">
	<%	
	string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\_SimpleEdit_Refresh", Model.ApplicationID.ToString("D3"));
	if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
	{
		ViewBag.returnConfirmationURL = ViewToFind;
		Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
		ViewBag.canEdit = true; //used for logic in the save _saveButtons.ascx
	}
	else
	{
		Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default_Simple\\_SimpleEdit_Refresh.ascx", Model);
		ViewBag.canEdit = false; //used for logic in the save _saveButtons.ascx

	}
	%>
	</div>
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_AllInOne_DevicePages.ascx", Model);%><!-- These views are for PoE, LTE, and MOWI fields -->
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_SaveButtons.ascx", Model);%>
</div>
