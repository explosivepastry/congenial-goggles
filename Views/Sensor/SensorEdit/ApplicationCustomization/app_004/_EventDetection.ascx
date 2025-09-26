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
            SelectedValue = Html.TranslateTag("Sensor/ApplicationCustomization/Default|State Change", "State Change");
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

<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: EventDetectionTypeDescription %>
	</div>
	<div class="col sensorEditFormInput">
		<%: Html.DropDownList("EventDetectionType_Manual", select as IEnumerable<SelectListItem>, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.EventDetectionType)%>
	</div>
</div>

<script>
    
          <% if (Model.CanUpdate)
             { %>
    //MobiScroll
    //$(function () {
    //    $('#EventDetectionType_Manual').mobiscroll().select({
    //        theme: 'ios',
    //        display: popLocation,
    //        minWidth: 200
    //    });
    //});

  <%}%>
    $('#EventDetectionType_Manual').addClass("form-select mw185");

</script>
