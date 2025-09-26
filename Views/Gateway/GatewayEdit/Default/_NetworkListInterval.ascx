<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_NetworkListInterval|Refresh Network List Minutes (default","Refresh Network List Minutes (default")%>: <%:Model.GatewayType.DefaultNetworkListInterval%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" id="NetworkListInterval" name="NetworkListInterval" value="<%=  Model.NetworkListInterval%>" />
        <a id="netListNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.NetworkListInterval)%>
    </div>
</div>


<script type="text/javascript">

    var mobiLabel = '<%: Html.TranslateTag("Minutes","Minutes")%>';
    var listInterval_array = [1, 5, 10, 20, 30, 60, 120, 360, 720];

    $(document).ready(function () {

        $('#NetworkListInterval').change(function () {
            if ($('#NetworkListInterval').val() < 1)
                $('#NetworkListInterval').val(1);

            if ($('#NetworkListInterval').val() > 720)
                $('#NetworkListInterval').val(720);
        });

        createSpinnerModal("netListNum", mobiLabel, "NetworkListInterval", listInterval_array);
    });

</script>
