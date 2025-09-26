<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%
    bool validTypeForUpdate = Model.GatewayType.SupportsOTASuite; //MinOTAFirmwareVersion only applies to BSN firmware capabilities..   && new Version(Model.GatewayFirmwareVersion) >= new Version(Model.GatewayType.MinOTAFirmwareVersion);

    if (!validTypeForUpdate)
    {
        validTypeForUpdate = (!string.IsNullOrWhiteSpace(Model.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(Model.GatewayType.LatestGatewayVersion));
    }

    if (validTypeForUpdate && !Model.ForceToBootloader)
    {
        bool requiresUpdate = false;

        if (!string.IsNullOrWhiteSpace(Model.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(Model.GatewayType.LatestGatewayVersion))//must at least support new firemware to flagged for it
        {
            if (Model.GatewayFirmwareVersion != Model.GatewayType.LatestGatewayVersion)
            {
                requiresUpdate = true;
            }
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(Model.SKU))
            {

                string latestVersion = null;
                if (MonnitSession.IsEnterprise)
                {
                    latestVersion= MonnitUtil.GetLatestEncryptedFirmwareVersion(Model.SKU, Model.GatewayType.IsGatewayBase);
                }
                else
                {
                    latestVersion= MonnitUtil.GetLatestFirmwareVersionFromMEA(Model.SKU, Model.GatewayType.IsGatewayBase);
                }

                if (!string.IsNullOrEmpty(latestVersion) && !latestVersion.Contains("Failed") && latestVersion != Model.GatewayFirmwareVersion)
                {
                    requiresUpdate = true;
                }

                if (Model.GenerationType.Contains("Gen2")
                        && Model.GatewayType.SupportsOTASuite
                        && new Version(Model.GatewayFirmwareVersion) >= new Version(Model.GatewayType.MinOTAFirmwareVersion))
                {
                    Version BSNVersion = new Version(Model.APNFirmwareVersion);// Must be greater than x.x.1.0
                    if (BSNVersion.Build > 1 || (BSNVersion.Build == 1 && BSNVersion.Revision > 0))
                    {
                        string latestRadioVersion = null;
                        if (MonnitSession.IsEnterprise)
                        {
                            latestRadioVersion= MonnitUtil.GetLatestEncryptedFirmwareVersion(Model.SKU, Model.GatewayType.IsGatewayBase);
                        }
                        else
                        {
                            latestRadioVersion = MonnitUtil.GetLatestFirmwareVersionFromMEA(Model.SKU, false);
                        }
                        if (!string.IsNullOrEmpty(latestRadioVersion) && !latestRadioVersion.Contains("Failed") && latestRadioVersion != Model.APNFirmwareVersion)
                        {
                            requiresUpdate = true;
                        }
                    }
                }
            }
        }

        if (requiresUpdate)
        { %>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Gateway/_UpdateFirmware|Update Gateway Firmware", "Update Gateway Firmware")%>
                </div>
                <div class="col sensorEditFormInput">
                    <a href="#" id="Update" class="btn btn-secondary btn-sm"><%: Html.TranslateTag("Gateway/_UpdateFirmware|Update", "Update")%></a>
                </div>
            </div>
        <%}
    }
%>

<script type="text/javascript">

    $(document).ready(function () {

        var updateConfirm = "<%: Html.TranslateTag("Gateway/_UpdateFirmware|Are you sure you want to update this gateway?","Are you sure you want to update this gateway?")%>";

        $('#Update').click(function (e) {
            e.preventDefault();

            let values = {};
            let GatewayID = <%: Model.GatewayID%>;
            let returnUrl = $('#returns').val();
            values.partialTag = $('#gatewayEdit_<%:Model.GatewayID %>').parent();
            values.url = `/Overview/GatewayFirmwareUpdate?id=${GatewayID}&url=${returnUrl}`;
            values.text = `${updateConfirm}`;

            openConfirm(values);

        });
    });
</script>