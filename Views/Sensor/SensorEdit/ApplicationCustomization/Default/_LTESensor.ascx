<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (Model.IsLTESensor)
    {
        Gateway LTEGateway = Gateway.LoadBySensorID(Model.SensorID);

        if (LTEGateway != null && LTEGateway.GatewayType != null)
        {
            string errorParseStr = Html.TranslateTag("Error in parsing");

            string[] simstrings = LTEGateway.MacAddress.Split('|'); ;
            if (simstrings.Length < 3) simstrings = new string[] { "errorParseStr", "errorParseStr", "errorParseStr" };

            bool showOtaUpdate = false;

            if (!MonnitSession.IsEnterpriseAdmin && !MonnitSession.IsEnterprise)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(LTEGateway.SKU))
                    {
                        string latestVersion = MonnitUtil.GetLatestFirmwareVersionFromMEA(LTEGateway.SKU, LTEGateway.GatewayType.IsGatewayBase);

                        if (!latestVersion.Contains("Failed") && latestVersion != LTEGateway.GatewayFirmwareVersion)
                        {
                            showOtaUpdate = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Log("_LTESensor[.ascx][GetLatestFirmwareVersionFromMEA] ");
                }
            }

%>

<%--    <p class="useAwareState">Cellular Settings</p>--%>
<div class="clearfix"></div>
<br />


<script type="text/javascript">

    $(function () {
        $('#btnOtaUpdate').click(function (e) {
            e.preventDefault();

            var obj = $(this);
            var oldHtml = $(this).html();
            $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

            $.post('/Overview/OTAUpdate', { id: '<%:Model.SensorID%>' }, function (data) {
                if (data == 'Success') {
                    window.location.href = window.location.href;
                } else {
                    obj.html(oldHtml);
                }
            });
        });
    });

</script>
<%}
        }%>
