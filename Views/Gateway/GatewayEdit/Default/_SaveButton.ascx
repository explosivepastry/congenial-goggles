<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<br>
<div class="row">
    <div class="col-12 text-end">
        <button type="button" onclick="modalConfirm(<%:Model.GatewayID%>,<%:Model.GatewayTypeID%>);return false;" value="<%: Html.TranslateTag("Save", "Save")%>" class="btn btn-primary">Save</button>
        <div style="clear: both;"></div>
    </div>
</div>
