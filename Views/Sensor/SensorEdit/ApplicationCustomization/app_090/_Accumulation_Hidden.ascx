<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {%>

<input hidden type="checkbox"<%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3Chk" id="Calibration3Chk" <%= Model.Calibration3 == 1 ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("On", "On")%>" data-off="<%: Html.TranslateTag("Off", "Off")%>" />
    <div style="display: none;"><%: Html.TextBoxFor(model => model.Calibration3, (Dictionary<string, object>)ViewData["HtmlAttributes"])%></div>
<%: Html.ValidationMessageFor(model => model.Calibration3)%>

<% } %>

<select hidden id="edge" name="edge" <%=Model.CanUpdate ? "" : "disabled" %>>
    <option value="0" <%:Model.Calibration1 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Positive Edge","Positive Edge")%></option>
    <option value="1" <%:Model.Calibration1 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Negative Edge","Negative Edge")%></option>
    <option value="2" <%:Model.Calibration1 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Both Edges","Both Edges")%></option>
</select>

<select hidden id="filter" name="filter" <%=Model.CanUpdate ? "" : "disabled" %>>
    <option value="2" <%:Model.Calibration4 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|No Filter","No Filter")%></option>
    <option value="<%: new Version(Model.FirmwareVersion) >= new Version("2.4.0.6")?"0":"1" %>" <%: new Version(Model.FirmwareVersion) >= new Version("2.4.0.6") && Model.Calibration4 == 0 ? "selected='selected'" :new Version(Model.FirmwareVersion) < new Version("2.4.0.6") && Model.Calibration4 == 1 ?"selected='selected'":"" %>>40 Hz <%: Html.TranslateTag("Filter","Filter")%></option>
    <option value="<%: new Version(Model.FirmwareVersion) >= new Version("2.4.0.6")?"1":"0" %>" <%:  new Version(Model.FirmwareVersion) >= new Version("2.4.0.6") && Model.Calibration4 == 1 ? "selected='selected'" : new Version(Model.FirmwareVersion) < new Version("2.4.0.6") && Model.Calibration4 == 0 ? "selected='selected'":"" %>>4 Hz <%: Html.TranslateTag("Filter","Filter")%></option>
</select>