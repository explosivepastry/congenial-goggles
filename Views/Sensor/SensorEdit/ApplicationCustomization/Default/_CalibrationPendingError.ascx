<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Model.CanUpdate)
    {
        TempData["CanCalibrate"] = false;

%>

<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.", "Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>

<%}%>

<div class="formBody" style="color: red;"><%:!string.IsNullOrEmpty(ViewBag.ErrorMessage)?ViewBag.ErrorMessage:"" %></div>