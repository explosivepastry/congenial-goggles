<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_RemoteNetworkReset|Reform Network","Reform Network")%>
    </div>
    <div class="col sensorEditFormInput">
        <a href="#" id="Reform" class="btn btn-secondary btn-sm">Reform</a>
    </div>
</div>


<script type="text/javascript">

    $(document).ready(function () {

        var reformConfirm = "<%: Html.TranslateTag("Gateway/_RemoteNetworkReset|Are you sure you want to reform this gateway?","Are you sure you want to reform this gateway?")%>";
        
        $('#Reform').click(function (e) {

            e.preventDefault();

            let values = {};
            let GatewayID = <%: Model.GatewayID%>;
            let returnUrl = $('#returns').val();
            values.partialTag = $('#gatewayEdit_<%:Model.GatewayID %>').parent();
            values.url = `/Overview/GatewayReform?id=${GatewayID}&url=${returnUrl}`;
            values.text = `${reformConfirm}`;

            openConfirm(values);
        });
    });


</script>

