<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%int ReportImmediatelyOn = AltaPIRBase.GetReportImmediatelyOn(Model);%>

<%if (!Model.IsWiFiSensor)
  {%>

   <%-- <input hidden type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ReportImmediatelyOnChk" id="ReportImmediatelyOnChk" <%=AltaPIRBase.GetReportImmediatelyOn(Model) < 1 ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware Reading","Aware Reading")%>" data-off="<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|All State Changes","All State Changes")%>" />

    <div style="display: none;"><%: Html.TextBoxFor(model => ReportImmediatelyOn, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>--%>

<div class="form-check form-switch d-flex align-items-center ps-0" style="margin-left:5px;">
            <label class="form-check-label">All State Changes</label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox"  <%=Model.CanUpdate ? "" : "disabled" %> name="ReportImmediatelyOnChk" id="ReportImmediatelyOnChk" style="cursor:pointer">
            <label class="form-check-label">Aware Reading</label>
        </div>
 <div style="display: none;"><%: Html.TextBoxFor(model => ReportImmediatelyOn, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>

<%}%>