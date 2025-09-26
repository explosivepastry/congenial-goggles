<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string EventDetectionTypeDescription = string.Empty;
    string valueForZero = string.Empty;
    string valueForOne = string.Empty;
    MonnitApplicationBase.ProfileSettingsForTriggeredUI(Model, out EventDetectionTypeDescription, out valueForZero, out valueForOne);

    SelectList select = null;
    string SelectedValue = "";
    switch (Model.EventDetectionType)
    {
        case 2:
            SelectedValue = "State Change";
            break;
        case 1:
            SelectedValue = valueForOne;
            break;
        case 0:
            SelectedValue = valueForZero;
            break;
    }
    if (Model.SensorTypeID != 4 && new Version(Model.FirmwareVersion) >= new Version("2.3.0.0") || Model.SensorTypeID == 8)
    {
        select = new SelectList(new string[3] { valueForZero, valueForOne, "State Change" }, SelectedValue);
    }
    else if (Model.SensorTypeID == 4 || new Version(Model.FirmwareVersion) < new Version("2.3.0.0"))
    {
        select = new SelectList(new string[2] { valueForZero, valueForOne }, SelectedValue);
    }
%>
<div class=" row sensorEditForm">
    <div class="col-12 " style="width:200px;">
        <%: EventDetectionTypeDescription %>
    </div>
    <div class="col sensorEditFormInput" style="min-width:250px;">
        <%: Html.DropDownList("EventDetectionType_Manual", select as IEnumerable<SelectListItem>, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.EventDetectionType)%>
        <img alt="help" class="helpIcon" title="Determines which reading of the sensor will trigger the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" />
    </div>
</div>
<script>
    $('#EventDetectionType_Manual').addClass("form-select");
</script>
