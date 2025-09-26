<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<eDatumStruct>>" %>

<%if(Model.Count > 1){ %>
<div class="col-12 EventTypeChange" id="ApplicationDatum">
    <label class="card_container__body__content"><%: Html.TranslateTag("Events/CheckAppDatum|This Sensor has more than One Reading Type!")%></label>
    <label class="card_container__body__content"><%: Html.TranslateTag("Events/CheckAppDatum|Select Desired Reading Type")%></label>
    <select class="form-select" id="Datum" name="Datum">
        <%foreach(eDatumStruct obj in Model){
                if (obj.val.StartsWith("15&")) continue;// this removes Mode datum from ddl  %>
        <option value="<%:obj.val %>"><%=obj.name %></option>
        <%} %>
    </select>
</div>
<div class="clearfix"></div>
<%}else if(Model.Count ==1){ %>
<input type="hidden" name="Datum" id="Datum" value="<%Response.Write(Model[0].val);%>" />
<%}else{ %>
<br />
<div class="col-12 EventTypeChange" id="Div1">
    <label class="card_container__body__content"><% Response.Write(Html.TranslateTag("Events/CheckAppDatum|Unable to locate sensor definition"));%></label>
    <input type="hidden" name="Datum" id="Datum" value="0&0" />
</div>
<div class="clearfix"></div>
<%} %>
<script type="text/javascript">
    $(function () {
        loadAppSettings();
        $('select#Datum').change(function () {
            loadAppSettings();
        });
    });
</script>
