<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%int ReportImmediatelyOn = AltaPIRBase.GetReportImmediatelyOn(Model);%>

<%if (!Model.IsWiFiSensor)
  {%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Report Immediately On:","Report Immediately On:")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("All State Changes","All State Changes")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ReportImmediatelyOnChk" id="ReportImmediatelyOnChk" <%=AltaPIRBase.GetReportImmediatelyOn(Model) < 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("","Aware Reading")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => ReportImmediatelyOn, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<script type="text/javascript">


    $('#ReportImmediatelyOnChk').change(function () {
        if ($('#ReportImmediatelyOnChk').prop('checked')) {
            $('#ReportImmediatelyOn').val(0);
        } else {
            $('#ReportImmediatelyOn').val(1);
        }
    });

</script>
<%}%>