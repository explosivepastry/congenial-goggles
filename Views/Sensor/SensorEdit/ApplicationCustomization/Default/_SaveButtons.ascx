 <%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group" id="hideMyDiv">
    <div class="bold col-sm-9 col-12" style="padding-bottom: 4px;">
    </div>

    <div class="d-flex w-100" style="align-items: center; justify-content: space-between">
        <div id="wrapperForSometimesSpan">
            <span <%: Model.CanUpdate ? "hidden" : "" %> style="align-items: center;">
                <span class="pendingEditIconLeft pendingsvg" style="padding-right: 0.25rem;"><%=Html.GetThemedSVG("Pending_Update") %></span>
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
            </span>
        </div>
        <button class="btn btn-primary" type="button" id="save" <%=Model.CanUpdate ? "" : "disabled" %> onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>

        <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            <%: Html.TranslateTag("Saving...","Saving...")%>
        </button>


    </div>
</div>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("SensorEdit_ApplicationCustomization_Default_SaveButtons.ascx") %>

<%
    if (MonnitSession.CustomerCan("Support_Advanced"))
    {
%>

    function clearDirtyFlags(sensorID) {
        $.post("/Overview/SetSensorActive/" + sensorID, function (data) {
            window.location.href = window.location.href;
        });
    }

    $('.pendingEditIconLeft.pendingsvg').click(function () {
        clearDirtyFlags(<%: Model.SensorID %>);
    });
<%
    }
%>

    // JavaScript code to handle Bluetooth scan
  <%--  document.addEventListener("DOMContentLoaded", function () {

        document.getElementById("scanButton")?.addEventListener("click", async function () {
            if ('bluetooth' in navigator) {
                try {
                    //const bleService = 'battery_service';
                    //const bleCharacteristic = 'battery_level';

                    const bleService = 'c7027f35-d65d-4c6b-a66f-86109e1d13b5';
                    const bleCharacteristic = 'Status';


                    //const device = await navigator.bluetooth.requestDevice({
                    //    filters: [{
                    //        services: [bleService]
                    //    }]

                    const device = await navigator.bluetooth.requestDevice({
                        acceptAllDevices: true,
                        optionalServices: [bleService]
                    });

                    device.addEventListener('gattserverdisconnected', handleDeviceDisconnect);

                    const server = await device.gatt.connect();
                    //const service = await server.getPrimaryService(bleService);
                    //const characteristic = await service.getCharacteristic(bleCharacteristic);
                    //await characteristic.startNotifications();

                    //characteristic.addEventListener('characteristicvaluechanged', e => {
                    //    const value = e.target.value.getUint8(0);
                    //    console.log(`${bleCharacteristic} changed`, value);
                    //    updateBatteryIndicator(value);
                    //});

                    //characteristic.readValue();
                    //const serviceTemp = await server.getPrimaryService();
                    //serviceTemp.
                    const service = await server.getPrimaryService(bleService);
                    const characteristic = await service.getCharacteristic(bleCharacteristic);
                    await characteristic.startNotifications();

                    characteristic.addEventListener('characteristicvaluechanged', e => {
                        const value = e.target.value.getUint8(0);
                        console.log(`${bleCharacteristic} changed`, value);
                    });

                    characteristic.readValue();

                } catch (error) {
                    showSimpleMessageModal(error.message);
                }
            } else {
                showSimpleMessageModal("<%:Html.TranslateTag("Web Bluetooth API is not supported in this browser.")%>");
            }
        });

        //function updateBatteryIndicator(value) {

        //    batteryIndicator.value = value;
        //}

        function handleDeviceDisconnect(event) {
            showSimpleMessageModal("<%:Html.TranslateTag("Device Disconnected")%>");
            if (characteristic) {
                characteristic.removeEventListener('characteristicvaluechanged', handleCharacteristicValueChanged);
                characteristic = null;
            }
        }
	});--%>
	


</script>
