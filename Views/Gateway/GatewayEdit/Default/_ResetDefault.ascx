<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_ResetDefault|Reset Gateway to Factory Defaults","Reset Gateway to Factory Defaults")%>
    </div>
    <div class="col sensorEditFormInput">
        <a href="#" id="Reset" class="btn btn-secondary btn-sm" style="width:63.2px;" ><%: Html.TranslateTag("Gateway/_ResetDefault|Reset","Reset")%></a>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        var defaultConfirm = "<%: Html.TranslateTag("Gateway/_ResetDefault|Are you sure you want to reset this gateway to defaults?","Are you sure you want to reset this gateway to defaults?")%>";

        $('#Reset').click(function (e) {
            let values = {};

            e.preventDefault();
            var GatewayID = <%: Model.GatewayID%>;
            var returnUrl = $('#returns').val();

            values.partialTag = $('#gatewayEdit_<%:Model.GatewayID %>').parent();
            values.url = `/Overview/GatewayReset?id=${GatewayID}&url=${returnUrl}`;
            values.text = `${defaultConfirm}`;
            openConfirm(values);

        });
    });


</script>

