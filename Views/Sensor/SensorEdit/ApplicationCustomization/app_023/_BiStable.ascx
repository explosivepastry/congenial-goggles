<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_023|Report Immediately On","Report Immediately On")%>:
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("State Change","State Change")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="BiStableChk" id="BiStableChk" <%= (Model.BiStable < 1) ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Aware Reading","Aware Reading")%></label>
        </div>
         <div style="display: none;"><%: Html.TextBoxFor(model => model.BiStable, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%: Html.ValidationMessageFor(model => model.BiStable)%>
    </div>
</div>

<script type="text/javascript">

  <% if(Model.CanUpdate){%>
    $('#BiStableChk').change(function () {
        if ($('#BiStableChk').prop('checked')) {
            $('#BiStable').val(0);
        } else {
            $('#BiStable').val(1);
        }
    });

    <%}%>

</script>