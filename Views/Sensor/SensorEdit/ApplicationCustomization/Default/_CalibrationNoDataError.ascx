<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    
    if (Model.LastDataMessageGUID == null || Model.LastDataMessageGUID == Guid.Empty || Model.LastDataMessage == null)
    {
        TempData["CanCalibrate"] = false;%>

        <div class="form-inline">
            <div class="form-group">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration is not available until data is present.", "Calibration is not available until data is present.")%> 
            </div>
        </div>
<% } %>