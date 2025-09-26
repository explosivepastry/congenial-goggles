$(document).ready(function () {

    $(window).scrollTop($('#bluetooth_testing').parent().position().top);
    //$('#serverCommStatus1MsgLong').val('')
    clearAll()

    function clearAll() {
        $('input').val('') // clear all inputs
        $('#networks_list').children('.ntwk_found').remove(); // clear network list
        $('#logBox').text(''); // clear logs
        $('#serverCommStatus1MsgLong').val(''); // clear server comm response
    }

    enable_standard_logs = true;
    enable_error_logs = true;

    $('#enable_standard_logs').prop('checked', enable_standard_logs);
    $('#enable_error_logs').prop('checked', enable_error_logs)

    $('#enable_error_logs').change(e => {
        enable_error_logs = e.target.checked
    })

    $('#enable_standard_logs').change(e => {
        enable_standard_logs = e.target.checked
    })

    function log(text) {
        if (enable_standard_logs) {
            logit(text, false);
        }
    }

    function logerr(text) {
        if (enable_error_logs) {
            logit(text, true);
        }
    }

    function logit(text, err, box = "#logBox") {
        if (typeof text == 'object') {
            let objToTextRes = objToString(text);
            if (objToTextRes != '') {
                text = objToTextRes
            }

        }
        $(box).append(`<p class="logLine ${err ? "error" : ""}">${text}</p>`);
        console.log(text);
    }

    function objToString(obj) {
        let res = '';
        try {
            $.each(obj, (i, x) => { res += `{DOMType: ${x.nodeName}, ID: ${x.id}, Classes: ${x.classList.value}, Value: ${x.value}}, ` });
        } catch {
            res = '';
        }
        return res;
    }

    function debugNow() {
        log("debug this!");
        debugger;
    }

    function clearOutput() {
        $('#logBox').text('');
        //$('#rememberedDevicesBox').text('');
        //$('#selectedDeviceBox').text('');
        //$('#serviceBox').text('');
        //$('#characteristicBox').text('');
        //$('#descriptorBox').text('');
        //$('#valueBox').text('');
    }

    $('#debug_Button').click(debugNow);

    $('#clear_Button').click(clearOutput);
    function onDisconnectButtonClick() {
        if (!bluetoothDevice) {
            return;
        }
        log('Disconnecting from Bluetooth Device...');
        if (bluetoothDevice.gatt.connected) {
            bluetoothDevice.gatt.disconnect();
        } else {
            log('> Bluetooth Device is already disconnected');
        }
    }

    function onDisconnected(event) {
        // Object event.target is Bluetooth Device getting disconnected.
        log('> Bluetooth Device disconnected');
    }

    function bluetoothDeviceIsNull() {
        if (!bluetoothDevice) {
            $('#btNull').css('background-color', 'red')
            return;
        }
        $('#btNull').css('background-color', 'green')

    }
    setInterval(bluetoothDeviceIsNull, 2000);

    function bluetoothDeviceIsConnected() {
        if (bluetoothDevice && bluetoothDevice.gatt.connected) {
            $('#btConnected').css('background-color', 'green')
            return;
        }
        $('#btConnected').css('background-color', 'red')

    }
    setInterval(bluetoothDeviceIsConnected, 2000);

    function onReconnectButtonClick() {
        if (!bluetoothDevice) {
            log('> Bluetooth Device is NULL');
            return;
        }
        if (bluetoothDevice.gatt.connected) {
            log('> Bluetooth Device is already connected');
            return;
        }
        try {
            connect();
        } catch (error) {
            logerr('onReconnectButtonClick Argh! ' + error);
        }
    }
    async function connect() {
        try {
            log('Connecting to Bluetooth Device...');
            await bluetoothDevice.gatt.connect();
            log('> Bluetooth Device connected');
            try {
                log('Connecting to GATT Server...');
                const server = await bluetoothDevice.gatt.connect();
                log('Getting Generic Access Service...');
                const service = await server.getPrimaryService(ServiceByName("Generic Access").uuid);
                log('Getting Device Name Characteristic...');
                const characteristic = await service.getCharacteristic(CharacteristicByName("Device Name").uuid)
                await readDeviceNameValue(characteristic);
            } catch (error) {
                logerr('Failed to retreive Device Name. Error: ', error)
            } finally {
                log('Device is connected. You can now READ the device.')
            }
        } catch (error) {
            logerr('Failed to connect to device. Error: ', error)
        }
    }

    /*
        // EXAMPLE
        let options = {
            filters: [
                { services: ["heart_rate"] },
                { services: [0x1802, 0x1803] },
                { services: ["c48e6067-5295-48d3-8d5c-0395f61792b1"] },
                { name: "ExampleName" },
                { namePrefix: "Prefix" },
            ],
            optionalServices: ["battery_service"],
        };
    */
    const bluetoothDeviceScanOptions = {
        filters: [
            /*{ services: [ServiceByName("Generic Access").uuid] },*/
            { services: [ServiceByName("MOWI2 BLE Service").uuid] },
            /*{ namePrefix: "MW" },*/
        ],
        optionalServices: [ServiceByName("Generic Access").uuid],
    };

    var bluetoothDevice;
    async function scanNow() {
        try {
            log('Looking for Bluetooth Devices...');
            bluetoothDevice = null;
            bluetoothDevice = await navigator.bluetooth.requestDevice(bluetoothDeviceScanOptions);
            // {
            // 	// filters: [...] <- Prefer filters to save energy & show relevant devices.
            // 	// acceptAllDevices: true,
            // 	// optionalServices: ['generic_access']
            // 	filters: [
            // 		{
            // 			namePrefix: 'MW',
            // 			optionalServices: ['00001800-0000-1000-8000-00805f9b34fb', 'c7027f35-d65d-4c6b-a66f-86109e1d13b5']
            // 		}
            // 	]

            // });
            log('Bluetooth Device selected...');
            clearAll();
            bluetoothDevice.addEventListener('gattserverdisconnected', onDisconnected);
            // await subscribeToStatusCharacteristic();
            /*navigator.bluetooth.setScreenDimEnabled(true);*/
            //navigator.bluetooth.addEventListener("backgroundstatechanged", e => { log("backgroundstatechanged"); /*$(window.document.children[0]).html('backgroundstatechanged')*/ })
            connect();

        } catch (error) {
            logerr('scanNow Argh! ' + error);
        }
    }

    async function readMowiVals() {
        try {

            $(window).scrollTop($('#settings_header').parent().position().top);
            log('Connecting to GATT Server...');
            const server = await bluetoothDevice.gatt.connect();

            log('Getting MOWI2 Service...');
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);

            log('Getting MOWI2 Characteristics...');
            const characteristics = await service.getCharacteristics();

            for (const characteristic of characteristics) {
                switch (characteristic.uuid.toLowerCase()) {

                    case CharacteristicByName('MOWI2 Status').uuid:
                        await readStatusValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Authorization Type').uuid:
                        await readAuthTypeValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Password').uuid:
                        break;

                    case CharacteristicByName('MOWI2 SSID').uuid:
                        await readSSIDValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Scan Network').uuid:
                        //await readScanNetworkValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 IP Config').uuid:
                        await readIPConfigValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Server Address').uuid:
                        await readServerAddressValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Server Port').uuid:
                        await readServerPortValue(characteristic);
                        break;

                    case CharacteristicByName('MOWI2 Primary Server Comm Status').uuid:
                        break;

                    case CharacteristicByName('MOWI2 Secondary Server Comm Status').uuid:
                        break;

                    case CharacteristicByName('MOWI2 Unlock Status').uuid:
                        await readUnlockStatus(characteristic);
                        break;

                    default: log('> Unknown Characteristic: ' + characteristic.uuid);
                }
            }
        } catch (error) {
            logerr('readMowiVals Argh! ' + error);
        }
    }

    /*async function readCharacteristicValue(characteristic) {
        const name = CharacteristicByUuid(characteristic.uuid).name
        const v = await characteristic.readValue();
        const value = (new TextDecoder()).decode(v);
        log('> Name: ' + name + ' Value: ' + value);
    }*/

    async function readSSIDValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 SSID").uuid);
        }

        if (characteristic.properties.read) {
            log('> SSID Characteristic is readable')
            const value = await characteristic.readValue();
            const val = (new TextDecoder()).decode(value);
            log('> SSID: ' + val);
            $('#ssid').val(val);
        } else {
            logerr('> SSID Characteristic is NOT readable')
        }
    }

    async function readAuthTypeValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Authorization Type").uuid);
        }

        if (characteristic.properties.read) {
            log('> Authorization Type Characteristic is readable')
            const value = await characteristic.readValue();
            const val = value.getUint8(0);
            log('> Auth Type: ' + val);
            $('#authtype option').filter((i, x) => { return x.value == val }).prop('selected', true);
        } else {
            logerr('> Authorization Type Characteristic is NOT readable')
        }
    }




    function u32toOctets(u) {
        const u0 = u.getUint8(0)
        const u1 = u.getUint8(1)
        const u2 = u.getUint8(2)
        const u3 = u.getUint8(3)

        return [u0, u1, u2, u3]
    }

    function readIPAddress(ipaddr) {
        $('#ipaddr0').val(ipaddr[0]);
        $('#ipaddr1').val(ipaddr[1]);
        $('#ipaddr2').val(ipaddr[2]);
        $('#ipaddr3').val(ipaddr[3]);

        log(`  > IP Address: ${ipaddr[0]}.${ipaddr[1]}.${ipaddr[2]}.${ipaddr[3]}.`);
    }

    function readSubnetMask(mask) {
        $('#mask0').val(mask[0]);
        $('#mask1').val(mask[1]);
        $('#mask2').val(mask[2]);
        $('#mask3').val(mask[3]);

        log(`  > Subnet Mask: ${mask}`);
        log(`  > Subnet Mask: ${mask[0]}.${mask[1]}.${mask[2]}.${mask[3]}.`);
    }

    function readGateway(gateway) {
        $('#gateway0').val(gateway[0]);
        $('#gateway1').val(gateway[1]);
        $('#gateway2').val(gateway[2]);
        $('#gateway3').val(gateway[3]);

        log(`  > Gateway: ${gateway[0]}.${gateway[1]}.${gateway[2]}.${gateway[3]}.`);
    }

    function readDNS(dns) {
        $('#dns0').val(dns[0]);
        $('#dns1').val(dns[1]);
        $('#dns2').val(dns[2]);
        $('#dns3').val(dns[3]);

        log(`  > DNS: ${dns[0]}.${dns[1]}.${dns[2]}.${dns[3]}.`);
    }

    async function readIPConfigValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 IP Config").uuid);
        }

        if (characteristic.properties.read) {
            log('> IP Config Characteristic is readable')

            const value = await characteristic.readValue();
            // const ipaddr = value.getUint32(0);

            log(`> IP Config: `);

            readIPAddress(u32toOctets(new DataView(value.buffer.slice(0, 4))));
            readSubnetMask(u32toOctets(new DataView(value.buffer.slice(4, 8))));
            readGateway(u32toOctets(new DataView(value.buffer.slice(8, 12))));
            readDNS(u32toOctets(new DataView(value.buffer.slice(12, 16))));
        } else {
            logerr('> IP Config Characteristic is NOT readable')
        }
    }

    async function readServerAddressValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Server Address").uuid);
        }

        if (characteristic.properties.read) {
            log('> Server Address Characteristic is readable')
            const value = await characteristic.readValue();
            $('#serveraddress_nbytes').text(value.byteLength);
            const val = (new TextDecoder()).decode(value);
            log('> Server Address: ' + val);
            $('#serveraddress').val(val);
        } else {
            logerr('> Server Address Characteristic is NOT readable')
        }
    }

    async function readServerPortValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Server Port").uuid);
        }

        if (characteristic.properties.read) {
            log('> Server Port Characteristic is readable')
            const value = await characteristic.readValue();
            const val = value.getUint16(0, true);
            log('> Server Port: ' + val);
            $('#serverport').val(val);
        } else {
            logerr('> Server Port Characteristic is NOT readable')
        }
    }

    async function readUnlockStatus(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Unlock Status").uuid);
        }

        if (characteristic.properties.read) {
            log('> Unlock Status Characteristic is readable')
            const value = await characteristic.readValue();
            const val = value.getUint8();
            log('> Unlock Status: ' + val);
            if (val > 0) {
                $('#routing').show();
            }
            return val;
        } else {
            logerr('> Unlock Status Characteristic is NOT readable')
        }
    }

    async function readAppearanceValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("Appearance").uuid);
        }

        if (characteristic.properties.read) {
            log('> Appearance Characteristic is readable')
            const value = await characteristic.readValue();
            log('> Appearance: ' +
                getDeviceType(value.getUint16(0, true /* Little Endian */)));
        } else {
            logerr('> Appearance Characteristic is NOT readable')
        }
    }

    async function readDeviceNameValue(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("Device Name").uuid);
        }

        if (characteristic.properties.read) {
            try {
                log('> Device Name Characteristic is readable')
                const v = await characteristic.readValue();
                const value = (new TextDecoder()).decode(v);
                log('> Device Name: ' + value);
                $('#device_name').val(value)
            } catch (error) {
                logerr(error)
            }
        } else {
            logerr('> Device Name Characteristic is NOT readable')
        }

    }

    async function readPPCPValue(characteristic) {
        const value = await characteristic.readValue();
        log('> Peripheral Preferred Connection Parameters: ');
        log('  > Minimum Connection Interval: ' +
            (value.getUint8(0) | value.getUint8(1) << 8) * 1.25 + 'ms');
        log('  > Maximum Connection Interval: ' +
            (value.getUint8(2) | value.getUint8(3) << 8) * 1.25 + 'ms');
        log('  > Latency: ' +
            (value.getUint8(4) | value.getUint8(5) << 8) + 'ms');
        log('  > Connection Supervision Timeout Multiplier: ' +
            (value.getUint8(6) | value.getUint8(7) << 8));
    }

    async function readCentralAddressResolutionSupportValue(characteristic) {
        const value = await characteristic.readValue();
        const supported = value.getUint8(0);
        if (supported === 0) {
            log('> Central Address Resolution: Not Supported');
        } else if (supported === 1) {
            log('> Central Address Resolution: Supported');
        } else {
            log('> Central Address Resolution: N/A');
        }
    }

    async function readPeripheralPrivacyFlagValue(characteristic) {
        const value = await characteristic.readValue();
        const flag = value.getUint8(0);
        if (flag === 1) {
            log('> Peripheral Privacy Flag: Enabled');
        } else {
            log('> Peripheral Privacy Flag: Disabled');
        }
    }

    /* Utils */

    function getDeviceType(value) {
        // Check out page source to see what valueToDeviceType object is.
        return value +
            (value in valueToDeviceType ? ' (' + valueToDeviceType[value] + ')' : '');
    }


    $('#linkBtDevicesButton').click(scanNow);
    $('#read_Button').click(readMowiVals);
    $('#disconnect_Button').click(onDisconnectButtonClick);
    $('#reconnect_Button').click(onReconnectButtonClick);
    $('#stopNtfc_Button').click(onStopNotificationsButtonClick);
    $('#startNtfc_Button').click(onStartNotificationsButtonClick);

    async function writeSSID(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 SSID").uuid);
        }
        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 SSID Characteristic is writeable')
            let encoder = new TextEncoder('utf-8');
            let value = $('#ssid').val();
            try {
                log('Setting SSID Characteristic...');
                await characteristic.writeValue(encoder.encode(value));
                await writeStatus();

                log('> Characteristic SSID changed to: ' + value);
            } catch (error) {
                logerr('writeSSID Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 SSID Characteristic is NOT writeable')
        }


    }

    $('#update_wifi').click(updateWiFi);

    async function updateWiFi() {
        await writeSSID();
        await writeAuthType();
        await writePassword();
    }

    $('#ssid_write').click(writeSSID);

    async function writeAuthType(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Authorization Type").uuid);
        }
        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Authorization Type Characteristic is writeable')
            const value = $('#authtype option:selected').val();
            const buffer = new Uint8Array(new ArrayBuffer(1));
            buffer.fill(value, 0, 1);

            try {
                log('Setting Authorization Type Characteristic...');
                await characteristic.writeValue(buffer);
                await writeStatus();

                log('> Characteristic Authorization Characteristic changed to: ' + value);
            } catch (error) {
                logerr('writeAuthType Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Authorization Type Characteristic is NOT writeable')
        }
    }

    $('#authtype_write').click(writeAuthType);

    async function writeServerAddress(characteristic) {
        let isUnlocked = await readUnlockStatus();
        if (!isUnlocked) {
            return;
        }

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Server Address").uuid);
        }
        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Server Address Characteristic is writeable')
            let encoder = new TextEncoder('utf-8');
            let value = $('#serveraddress').val();

            // check if is an ip address but not valid
            if (isANumber(value.replaceAll('.', '')) && !isIPv4(value)) {
                logerr('Server Address appears to be an invalid IPv4 address')
            }

            try {
                log('Setting Server Address Characteristic...');
                await characteristic.writeValue(encoder.encode(value));
                await writeStatus();

                log('> Characteristic Server Address changed to: ' + value);
            } catch (error) {
                logerr('writeServerAddress Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Server Address Characteristic is NOT writeable')
        }
    }

    $('#serveraddress_write').click(writeServerAddress);

    async function writeServerPort(characteristic) {
        let isUnlocked = await readUnlockStatus();
        if (!isUnlocked) {
            return;
        }

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Server Port").uuid);
        }
        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Server Port Characteristic is writeable')
            const value = $('#serverport').val();
            const buffer = new DataView(new ArrayBuffer(2));
            buffer.setUint16(0, value, true);

            try {
                log('Setting Server Port Characteristic...');
                await characteristic.writeValue(buffer.buffer);
                await writeStatus();

                log('> Characteristic Server Port changed to: ' + value);
            } catch (error) {
                logerr('writeServerPort Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Server Port Characteristic is NOT writeable')
        }
    }

    $('#serverport_write').click(writeServerPort);

    async function writeIPConfig(characteristic) {
        let isUnlocked = await readUnlockStatus();
        if (!isUnlocked) {
            return;
        }

        let ipaddr = `${$('#ipaddr0').val()}.${$('#ipaddr1').val()}.${$('#ipaddr2').val()}.${$('#ipaddr3').val()}`
        let mask = `${$('#mask0').val()}.${$('#mask1').val()}.${$('#mask2').val()}.${$('#mask3').val()}`
        let gateway = `${$('#gateway0').val()}.${$('#gateway1').val()}.${$('#gateway2').val()}.${$('#gateway3').val()}`
        let dns = `${$('#dns0').val()}.${$('#dns1').val()}.${$('#dns2').val()}.${$('#dns3').val()}`

        errString = '';
        errString += isIPv4(ipaddr) ? '' : 'IP Address is invalid\n';
        errString += isIPv4(mask) ? '' : 'Subnet Mask is invalid\n';
        errString += isIPv4(gateway) ? '' : 'Gateway Address is invalid\n';
        errString += isIPv4(dns) ? '' : 'DNS Address is invalid\n';

        if (errString.length > 0) {
            logerr(errString);
            return;
        }

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 IP Config").uuid);
        }

        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 IP Config Characteristic is writeable')
            //const value = await characteristic.readValue();
            // const ipaddr_buff = new DataView(new ArrayBuffer(4));
            // const ipaddrstr = $('#ipaddr0').val() + $('#ipaddr1').val() + $('#ipaddr2').val() + $('#ipaddr3').val();
            // const ipaddr0 = $('#ipaddr0').val()
            // const ipaddr1 = $('#ipaddr1').val()
            // const ipaddr2 = $('#ipaddr2').val()
            // const ipaddr3 = $('#ipaddr3').val()
            // 
            const ipaddr_buff = new DataView(new ArrayBuffer(4));
            ipaddr_buff.setUint8(0, +$('#ipaddr0').val());
            ipaddr_buff.setUint8(1, +$('#ipaddr1').val());
            ipaddr_buff.setUint8(2, +$('#ipaddr2').val());
            ipaddr_buff.setUint8(3, +$('#ipaddr3').val());
            //log(ipaddr_buff.getUint32(0, true))
            // log(ipaddr_buff.getUint32(0, false))

            const mask_buff = new DataView(new ArrayBuffer(4));
            mask_buff.setUint8(0, +$('#mask0').val());
            mask_buff.setUint8(1, +$('#mask1').val());
            mask_buff.setUint8(2, +$('#mask2').val());
            mask_buff.setUint8(3, +$('#mask3').val());
            //log(mask_buff.getUint32(0, true))

            const gateway_buff = new DataView(new ArrayBuffer(4));
            gateway_buff.setUint8(0, +$('#gateway0').val());
            gateway_buff.setUint8(1, +$('#gateway1').val());
            gateway_buff.setUint8(2, +$('#gateway2').val());
            gateway_buff.setUint8(3, +$('#gateway3').val());
            //log(gateway_buff.getUint32(0, true))

            const dns_buff = new DataView(new ArrayBuffer(4));
            dns_buff.setUint8(0, +$('#dns0').val());
            dns_buff.setUint8(1, +$('#dns1').val());
            dns_buff.setUint8(2, +$('#dns2').val());
            dns_buff.setUint8(3, +$('#dns3').val());
            //log(dns_buff.getUint32(0, true))

            const ipaddrstr = $('#ipaddr0').val() + $('#ipaddr1').val() + $('#ipaddr2').val() + $('#ipaddr3').val();
            const maskstr = $('#mask0').val() + $('#mask1').val() + $('#mask2').val() + $('#mask3').val();
            const gatewaystr = $('#gateway0').val() + $('#gateway1').val() + $('#gateway2').val() + $('#gateway3').val();
            const dnsstr = $('#dns0').val() + $('#dns1').val() + $('#dns2').val() + $('#dns3').val();
            const buffer = new DataView(new ArrayBuffer(16));

            buffer.setUint32(0, ipaddr_buff.getUint32(0));
            //log(buffer.getUint32(0, true))
            //log(buffer.getUint32(0, false))

            buffer.setUint32(4, mask_buff.getUint32(0));
            //log(buffer.getUint32(4, true))
            //log(buffer.getUint32(4, false))

            buffer.setUint32(8, gateway_buff.getUint32(0));
            //log(buffer.getUint32(8, true))
            //log(buffer.getUint32(8, false))

            buffer.setUint32(12, dns_buff.getUint32(0));
            //log(buffer.getUint32(12, true))
            //log(buffer.getUint32(12, false))



            try {
                log('Setting IP Config Characteristic...');
                await characteristic.writeValue(buffer.buffer);
                await writeStatus();

                log('> Characteristic IP Config changed to: ' + buffer.buffer);
            } catch (error) {
                logerr('writeIPConfig Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 IP Config Characteristic is NOT writeable')
        }





    }
    //$('#ipconfig_write').click(writeIPConfig);

    // -----------------------------------------------------
    // Find all the elements that have a tabindex set, 
    // I would keep this file scoped for performance sake
    // ------------------------------------------------------
    const tabElements = $('[tabindex]').filter((i, e) => e.tabIndex > -1)
    //log(tabElements)


    const event_keys = ['.', '.']
    const event_keyCodes = [110, 190]
    const event_codes = ['NumpadDecimal', 'Period']

    $('.octet').keydown(event => {

        if (event_keys.includes(event.key) || event_keyCodes.includes(event.keyCode) || event_codes.includes(event.code)) {
            //log('event.key = ' + event.key);
            //log('event.keyCode = ' + event.keyCode);
            //log('event.code = ' + event.code);
            //log(tabElements)
            //log(document.activeElement)
            //log(event.target)
            //log(event.target.tabIndex)
            // let xxx = tabElements.filter((i, e) => {e.tabIndex == (event.target.tabIndex + 1)})
            // 
            // xxx.focus()
            // document.activeElement.dispatchEvent(new KeyboardEvent("keypress", { 
            // 	key: "Tab" 
            // }));
            event.preventDefault()
            if (event.target.tabIndex < 16) {
                tabElements[event.target.tabIndex].focus();
                tabElements[event.target.tabIndex].select()
            } else {
                tabElements[0].focus();
                tabElements[0].select();
            }
        } else {
            //log('event.key = ' + event.key);
            //log('event.keyCode = ' + event.keyCode);
            //log('event.code = ' + event.code);
        }
    })


    async function writePassword(characteristic) {

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName('MOWI2 Password').uuid);
        }
        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Password Characteristic is writeable')
            let encoder = new TextEncoder('utf-8');
            let value = $('#password').val();
            try {
                log('Setting Password Characteristic...');
                await characteristic.writeValue(encoder.encode(value));
                await writeStatus();

                log('> Characteristic Password changed to: ' + value);
            } catch (error) {
                logerr('writePassword Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Password Characteristic is NOT writeable')
        }


    }

    $('#password_write').click(writePassword);





    /***
     * Status
     */
    async function writeStatus(characteristic) {

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Status").uuid);
        }

        await subscribeToStatusCharacteristic(characteristic);

        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Statis Characteristic is writeable')
            //const buffer = new Uint8Array(new ArrayBuffer(1));
            //buffer.fill(1, 0, 1);
            const value = true;
            const buffer = new DataView(new ArrayBuffer(1));
            buffer.setUint8(0, value, true);

            try {
                log('Setting Status Characteristic...');
                await characteristic.writeValue(buffer.buffer);
                setTimeout(readStatusValue, 5000);

                log('> Characteristic Status changed to: ' + value);
            } catch (error) {
                logerr('writeStatus Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Statis Characteristic is NOT writeable')
        }


    }


    $('#status_write').click(readStatusValue)

    async function stopNotificationsOnStatusCharacteristic(characteristic) {

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }
        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Status").uuid);
        }

        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Status Characteristic is subscribable')
            if (characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.stopNotifications();
                    log('> Status Characteristic notifications stopped');
                    // characteristic.removeEventListener('characteristicvaluechanged', handleStatusNotifications);
                    characteristic.characteristicvaluechanged = null;
                } catch (error) {
                    logerr('stopNotificationsOnStatusCharacteristic Argh! ' + error);
                }
            } else {
                log('Not subscribed to Status Characteristic Notifications')
            }
        } else {
            logerr('> MOWI2 Status Characteristic is NOT subscribable')
        }
    }

    function handleStatusNotifications(event) {
        let value = event.target.value;
        let a = [];
        // Convert raw data bytes to hex values just for the sake of showing something.
        // In the "real" world, you'd use data.getUint8, data.getUint16 or even
        // TextDecoder to process raw data bytes.

        for (let i = 0; i < value.byteLength; i++) {
            a.push('0x' + ('00' + value.getUint8(i).toString(16)).slice(-2));
        }
        log('> ' + a.join(' '));

        updateStatus(value.getUint8(0))
    }

    async function subscribeToStatusCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {

            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Status").uuid);
        }

        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Status Characteristic is subscribable')
            if (!characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.startNotifications();
                    log('> Status Characteristic notifications started');
                    characteristic.oncharacteristicvaluechanged = handleStatusNotifications;
                    //characteristic.addEventListener('characteristicvaluechanged', handleStatusNotifications);
                    characteristic.characteristicvaluechanged = null;
                } catch (error) {
                    logerr('subscribeToStatusCharacteristic Argh! ' + error);
                }
            } else {
                log('Already subscribed to Status Characteristic notifications')
            }
        } else {
            logerr('> MOWI2 Status Characteristic is NOT subscribable')
        }
    }

    async function readStatusValue(characteristic) {

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Status").uuid);
        }


        if (characteristic && characteristic.properties.read) {
            log('> MOWI2 Scan Network Characteristic is readable')
            const value = await characteristic.readValue();
            const val = value.getUint8(0);
            updateStatus(val);
        } else {
            logerr('> MOWI2 Scan Network Characteristic is NOT readable')
        }

    }

    function updateStatus(val) {

        //0: Awaiting configuration data
        //1: Configuration saved successfully
        //2: Configuration error
        let status;
        let bgcol;
        switch (val) {
            case 0:
                status = "Awaiting configuration data";
                bgcol = '';
                break;
            case 1:
                status = "Configuration saved successfully";
                bgcol = "#43be5f60";
                break;
            case 2:
                status = "Configuration error";
                bgcol = "#f5504e60";
                break;
            default:
                status = "Unknown";
                bgcol = "#f5f04e60";
                break;
        }
        log('> Status: ' + status);
        $('#status').val(status);
        $('#status').css('background-color', bgcol);
    }

    /***
     * END Status
     */

    /***
     * Scan Network
     */

    async function stopNotificationsOnScanNetworkCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Scan Network").uuid);
        }
        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Scan Network Characteristic is subscribable')
            if (characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.stopNotifications();
                    log('> Scan Network Characteristic Notifications stopped');
                    characteristic.characteristicvaluechanged = null;
                } catch (error) {
                    logerr('stopNotificationsOnScanNetworkCharacteristic Argh! ' + error);
                }
            } else {
                log('Not subscribed to Scan Network Characteristic Notifications')
            }
        } else {
            logerr('> MOWI2 Scan Network Characteristic is NOT subscribable')
        }
    }

    async function subscribeToScanNetworkCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Scan Network").uuid);
        }
        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Scan Network Characteristic is subscribable')
            if (!characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.startNotifications();
                    log('> Scan Network Characteristic notifications started');
                    characteristic.oncharacteristicvaluechanged = handleScanNetwork;
                } catch (error) {
                    logerr('subscribeToScanNetworkCharacteristic Argh! ' + error);
                }

            } else {
                log('Already subscribed to Scan Network Characteristic notifications')
            }
        } else {
            logerr('> MOWI2 Scan Network Characteristic is NOT subscribable')
        }
    }

    async function readScanNetworkValue(characteristic) {

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Scan Network").uuid);
        }

        if (characteristic && characteristic.properties.read) {
            log('> MOWI2 Scan Network Characteristic is readable')
            const value = await characteristic.readValue();
            if (value.buffer.byteLength > 2) {
                parseScanNetwork(value);
            }
        } else {
            logerr('> MOWI2 Scan Network Characteristic is NOT readable');
        }
    }

    $('#scannetwork_write').click(writeScanNetworkValue)

    async function writeScanNetworkValue(characteristic) {
        $('#networks_list').children('.ntwk_found').remove();

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }
        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Scan Network").uuid);
        }


        await subscribeToScanNetworkCharacteristic(characteristic)

        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Scan Network Characteristic is writeable')
            const buffer = new Uint8Array(new ArrayBuffer(1));
            buffer.fill(1, 0, 1);

            try {
                log('Setting Scan Network Characteristic...');
                await characteristic.writeValue(buffer);
                $('#networks_loading').show();
                $(window).scrollTop($('#networks_list').parent().position().top); // scroll to Network List
                //await writeStatus();

                log('> Characteristic Scan Network changed to: ' + buffer[0]);
            } catch (error) {
                logerr('writeScanNetworkValue Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Scan Network Characteristic is NOT writeable')
        }




    }

    /*$('#scannetwork_write').click(writeScanNetworkValue)*/

    function handleScanNetwork(event) {
        //$(window).scrollTop($('table#networks_list').parent().position().top);
        $('#networks_loading').hide();
        parseScanNetwork(event.target.value);
    }

    const authTypes = [
        { "id": 0, "name": "Open", "supported": true },
        { "id": 1, "name": "WEP", "supported": false },
        { "id": 2, "name": "WPA_PSK", "supported": true },
        { "id": 3, "name": "WPA2_PSK", "supported": true },
        { "id": 4, "name": "WPA_WPA2_PSK", "supported": true },
        { "id": 5, "name": "WPA2_ENTERPRISE", "supported": false },
        { "id": 6, "name": "WPA3_PSK", "supported": true },
        { "id": 7, "name": "WPA2_WPA3_PSK", "supported": true },
        { "id": 8, "name": "WAPI_PSK", "supported": false },
        { "id": 9, "name": "OWE", "supported": false },
    ]
    // populate auth dropdown
    $.each(authTypes, (i, x) => { x.supported ? $('#authtype').append($("<option />").val(x.id).text(x.name)) : ''; });

    function parseScanNetwork(ntwks) {
        networks = []
        let decoder = new TextDecoder();
        i = 0
        let nbytes = 0
        while (i < ntwks.byteLength) {
            try {
                nbytes = ntwks.getUint8(i)
                let record = new DataView(ntwks.buffer.slice(i, (i + nbytes)))
                let rssi = record.getUint8(1)
                let rssiPct = rssi > -70 ? ((rssi + 70) * 2) + 60 : ((rssi + 84) * 4.2857);
                rssiPct = rssiPct / 10;
                let authIndex = record.getUint8(2)
                //let authName = $('#authtype option').filter((i, x) => { return x.value == auth }).text();
                let auth = authTypes.find(x => x.id == authIndex)

                let ssid = decoder.decode(record.buffer.slice(3, nbytes))
                let ntwk_found = { 'rssi': rssi, 'auth': auth.name, 'supported': auth.supported, 'ssid': ssid }
                networks.push(ntwk_found)
                $('#networks_list').append(`<tr class='${auth.supported ? 'ntwk_found' : 'ntwk_found unsupported'}' data-ssid='${ssid}' data-auth='${authIndex}'><th scope="row">${ssid}</th><td>${rssiPct}%</td><td${auth.supported ? '' : " class='auth_unsupported'"}>${auth.name}</td></tr>`)

            } catch (error) {
                logerr('parseScanNetwork Argh! ' + error + '. Attempting to continue');
            } finally {
                i = i + nbytes
            }
        }


    }

    //   $('.ntwk_found').click(function(event) {
    // 	$('#ssid').val($(event.target).data('ssid'))
    //   })

    $(document).on("click", '.ntwk_found:not(.unsupported)', function (event) {
        $('#ssid').val($(this).data('ssid'));
        $('#authtype').val($(this).data('auth'));
        $(window).scrollTop($("label[for='ssid']").offset().top);
        $('#password').focus();
    });


    function parseIP(ip) {
        var octets = ip.split('.');
        return octets.map(function (octet) {
            return octet | 0;
        });
    }



    function isIPv4(ip) {
        var octets = parseIP(ip),
            len = octets.length,
            isValid = true;

        if (len !== 4) return false;

        octets.forEach(function (octet) {
            if (octet < 0 || octet > 255) {
                isValid = false;
            }
        });
        return isValid;
    }



    //function ipToLong (ip) {
    //	var octets = parseIP(ip);

    //	if (!isIPv4(ip)) {
    //		throw 'Invalid IPv4 address!';
    //	}
    //	return ((octets[0] << 24) >>> 0) +
    //		((octets[1] << 16) >>> 0) +
    //		((octets[2] << 8) >>> 0) +
    //		(octets[3]);
    //}



    //function ipStringToLong (ip) {
    //	var octets = ip.split('.');
    //	if (octets.length !== 4) {
    //		throw new Error("Invalid format -- expecting a.b.c.d");
    //	}
    //	var ip = 0;
    //	for (var i = 0; i < octets.length; ++i) {
    //		var octet = parseInt(octets[i], 10);
    //		if (Number.isNaN(octet) || octet < 0 || octet > 255) {
    //			throw new Error("Each octet must be between 0 and 255");
    //		}
    //		ip |= octet << ((octets.length - i) * 8);
    //	}
    //	return ip;
    //}




    $('#ipconfig_write').click(writeIPConfig);

    /***
     * END Scan Network
     */

    async function onStartNotificationsButtonClick() {
        subscribeToStatusCharacteristic();
        subscribeToScanNetworkCharacteristic();
    }
    async function onStopNotificationsButtonClick() {
        stopNotificationsOnStatusCharacteristic();
        stopNotificationsOnScanNetworkCharacteristic();
    }

    //Git...2

    /** Server Status
     * c7028835-d65d-4c6b-a66f-86109e1d13b5
     * c7028935-d65d-4c6b-a66f-86109e1d13b5
     * 
•	BT_SERVCOMM_SUCCESS = 0, //Comm test successful
•	BT_SERVCOMM_UNTESTED = 1, //Comm never tested since BT started
•	BT_SERVCOMM_RUNNING = 2, //Comm test currently running
•	BT_SERVCOMM_ERR_SCAN_RUNNING = 3, //Scan running, can't do server comm and scan at same time
•	BT_SERVCOMM_ERR_INVALID_SSID = 4, //SSID invalid characters, format, or length
•	BT_SERVCOMM_ERR_INVALID_SECURITY_TYPE = 5, //Security type not valid
•	BT_SERVCOMM_ERR_INVALID_PASSCODE = 6, //Passkey incorrect format or length
•	BT_SERVCOMM_ERR_WIFI_FAILED_TO_START = 7, //Wifi failed to start, internal error, this should generally never happen
•	BT_SERVCOMM_ERR_AP_NOT_FOUND = 8, //No AP found with matching SSID
•	BT_SERVCOMM_ERR_AP_NOT_FOUND_WITH_PASSKEY = 9, //AP found but not with correct PASSKEY
•	BT_SERVCOMM_ERR_AP_NOT_FOUND_WITH_SECURITY = 10, //AP found but not with correct security type
•	BT_SERVCOMM_ERR_WIFI_TIMEOUT = 11, //Took over 5000 ms to acquire IP address
•	BT_SERVCOMM_ERR_WIFI_DISCONNECT = 12, //WiFi disconnected after initial connection
•	BT_SERVCOMM_ERR_DNS = 13, //DNS error or bad server name, will typically have secondary value as well
•	BT_SERVCOMM_ERR_UDP_OPEN = 14, //Fail to open UDP socket, will typically have secondary value as well
•	BT_SERVCOMM_ERR_UDP_TX = 15, //Fail to transmit data over UDP socket, will typically have secondary value as well
•	BT_SERVCOMM_ERR_UDP_RX = 16, //Fail to receive data over UDP socket, will typically have secondary value as well
•	BT_SERVCOMM_ERR_MSVR = 17, //Data received contains error, refer to secondary server comm status for details
•	BT_SERVCOMM_ERR_WIFI_CONFIG = 18, //Error occurred while trying set wifi configs, refer to secondary server comm status for details
•	BT_SERVCOMM_ERR_CONNECT = 19, //Error occurred while trying to connect, refer to secondary server comm status for details
•	BT_SERVCOMM_ERR_FAIL_TO_START_TEST = 20, //Failed to start comm test for unknown reason
•	BT_SERVCOMM_ERR_MAX = 21 //Max error, report this value or any value greater than this as number directly

     */

    const PrimaryServerCommStatusResponses = Object.freeze({
        BT_SERVCOMM_SUCCESS: 0,
        BT_SERVCOMM_UNTESTED: 1,
        BT_SERVCOMM_RUNNING: 2,
        BT_SERVCOMM_ERR_SCAN_RUNNING: 3,
        BT_SERVCOMM_ERR_INVALID_SSID: 4,
        BT_SERVCOMM_ERR_INVALID_SECURITY_TYPE: 5,
        BT_SERVCOMM_ERR_INVALID_PASSCODE: 6,
        BT_SERVCOMM_ERR_WIFI_FAILED_TO_START: 7,
        BT_SERVCOMM_ERR_AP_NOT_FOUND: 8,
        BT_SERVCOMM_ERR_AP_NOT_FOUND_WITH_PASSKEY: 9,
        BT_SERVCOMM_ERR_AP_NOT_FOUND_WITH_SECURITY: 10,
        BT_SERVCOMM_ERR_WIFI_TIMEOUT: 11,
        BT_SERVCOMM_ERR_WIFI_DISCONNECT: 12,
        BT_SERVCOMM_ERR_DNS: 13,
        BT_SERVCOMM_ERR_UDP_OPEN: 14,
        BT_SERVCOMM_ERR_UDP_TX: 15,
        BT_SERVCOMM_ERR_UDP_RX: 16,
        BT_SERVCOMM_ERR_MSVR: 17,
        BT_SERVCOMM_ERR_WIFI_CONFIG: 18,
        BT_SERVCOMM_ERR_CONNECT: 19,
        BT_SERVCOMM_ERR_FAIL_TO_START_TEST: 20,
        BT_SERVCOMM_ERR_MAX: 21,
    });

    const PrimaryServerCommStatusResponseMessages = Object.freeze({
        0: "Comm test successful",
        1: "Comm never tested since BT started",
        2: "Comm test currently running",
        3: "Scan running, can't do server comm and scan at same time",
        4: "SSID invalid characters, format, or length",
        5: "Security type not valid",
        6: "Passkey incorrect format or length",
        7: "Wifi failed to start, internal error, this should generally never happen",
        8: "No AP found with matching SSID",
        9: "AP found but not with correct PASSKEY",
        10: "AP found but not with correct security type",
        11: "Took over 5000 ms to acquire IP address",
        12: "WiFi disconnected after initial connection",
        13: "DNS error or bad server name, will typically have secondary value as well",
        14: "Fail to open UDP socket, will typically have secondary value as well",
        15: "Fail to transmit data over UDP socket, will typically have secondary value as well",
        16: "Fail to receive data over UDP socket, will typically have secondary value as well",
        17: "Data received contains error, refer to secondary server comm status for details",
        18: "Error occurred while trying set wifi configs, refer to secondary server comm status for details",
        19: "Error occurred while trying to connect, refer to secondary server comm status for details",
        20: "Failed to start comm test for unknown reason",
        21: "Max error, report this value or any value greater than this as number directly"
    });

    function handleServerCommStatus(event) {
        const val = event.target.value;
        let v, r, m;
        try {
            v = val.getUint8();
            r = Object.keys(PrimaryServerCommStatusResponses).find(k => PrimaryServerCommStatusResponses[k] == v).replace('BT_SERVCOMM_', '');
        } catch (err) {
            logerr(err);
        }

        try {
            v = val.getUint8();
            m = PrimaryServerCommStatusResponseMessages[v];
        } catch (err) {
            logerr(err);
        }

        r ??= 'ERR_UNKNOWN';
        m ??= '';

        $('#servercommstatus1Msg').val(r);
        $('#servercommstatus1Hex').val('0x' + toHexString(new Uint8Array(val.buffer).slice(0, 1)).toUpperCase());
        $('#serverCommStatus1MsgLong').val(m);
    }

    function handleSecondaryServerCommStatus(event) {
        const val = event.target.value;
        $('#servercommstatus2').val('0x' + toHexString(new Uint8Array(val.buffer).reverse()).toUpperCase());
    }


    $('#servercommstatus_write').click(writeServerCommStatus)

    async function writeServerCommStatus(characteristic) {
        $('#servercommstatus1Msg').val('');
        $('#servercommstatus1Hex').val('');
        $('#serverCommStatus1MsgLong').val('');
        $('#servercommstatus2').val('');

        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }
        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Primary Server Comm Status").uuid);
        }


        await subscribeToServerCommStatusCharacteristic(characteristic)
        await subscribeToSecondaryServerCommStatusCharacteristic()

        if (characteristic && characteristic.properties.write) {
            log('> MOWI2 Primary Server Comm Status Characteristic is writeable')
            const buffer = new Uint8Array(new ArrayBuffer(1));
            buffer.fill(1, 0, 1);

            try {
                log('Setting MOWI2 Primary Server Comm Status Characteristic...');
                await characteristic.writeValue(buffer);
                //await writeStatus();

                log('> Characteristic MOWI2 Primary Server Comm Status changed to: ' + buffer[0]);
            } catch (error) {
                logerr('writeServerCommStatus Argh! ' + error);
            }
        } else {
            logerr('> MOWI2 Primary Server Comm Status Characteristic is NOT writeable')
        }
    }

    async function stopNotificationsOnServerCommStatusCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Primary Server Comm Status").uuid);
        }
        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Primary Server Comm Status Characteristic is subscribable')
            if (characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.stopNotifications();
                    log('> Primary Server Comm Status Characteristic Notifications stopped');
                    characteristic.characteristicvaluechanged = null;
                } catch (error) {
                    logerr('stopNotificationsOnServerCommStatusCharacteristic Argh! ' + error);
                }
            } else {
                log('Not subscribed to Primary Server Comm Status Characteristic Notifications')
            }
        } else {
            logerr('> MOWI2 Primary Server Comm Status Characteristic is NOT subscribable')
        }
    }

    async function subscribeToServerCommStatusCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Primary Server Comm Status").uuid);
        }
        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Primary Server Comm Status Characteristic is subscribable')
            if (!characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.startNotifications();
                    log('> Primary Server Comm Status Characteristic notifications started');
                    characteristic.oncharacteristicvaluechanged = handleServerCommStatus;
                } catch (error) {
                    logerr('subscribeToServerCommStatusCharacteristic Argh! ' + error);
                }

            } else {
                log('Already subscribed to Primary Server Comm Status Characteristic notifications')
            }
        } else {
            logerr('> MOWI2 Primary Server Comm Status Characteristic is NOT subscribable')
        }
    }

    async function subscribeToSecondaryServerCommStatusCharacteristic(characteristic) {
        if (!bluetoothDevice.gatt.connected) {
            await connect();
        }

        if (!characteristic || !(characteristic instanceof BluetoothRemoteGATTCharacteristic)) {
            const server = await bluetoothDevice.gatt.connect();
            const service = await server.getPrimaryService(ServiceByName("MOWI2 BLE Service").uuid);
            characteristic = await service.getCharacteristic(CharacteristicByName("MOWI2 Secondary Server Comm Status").uuid);
        }
        if (characteristic && characteristic.properties.notify) {
            log('> MOWI2 Secondary Server Comm Status Characteristic is subscribable')
            if (!characteristic.oncharacteristicvaluechanged) {
                try {
                    await characteristic.startNotifications();
                    log('> Secondary Server Comm Status Characteristic notifications started');
                    characteristic.oncharacteristicvaluechanged = handleSecondaryServerCommStatus;
                } catch (error) {
                    logerr('subscribeToSecondaryServerCommStatusCharacteristic Argh! ' + error);
                }

            } else {
                log('Already subscribed to Secondary Server Comm Status Characteristic notifications')
            }
        } else {
            logerr('> MOWI2 Secondary Server Comm Status Characteristic is NOT subscribable')
        }
    }

    function toHexString(byteArray) {
        return Array.from(byteArray, function (byte) {
            return ('0' + (byte & 0xFF).toString(16)).slice(-2);
        }).join('')
    }



})
/*
function toHex1(buffer) {
    return Array.prototype.map.call(buffer, x => ('00' + x.toString(16)).slice(-2)).join('');
}

function toHex2(buffer) {
    return Array.prototype.map.call(buffer, x => x.toString(16).padStart(2, '0')).join('');
}

// Pre-Init
//const LUT_HEX_4b = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'];
// End Pre-Init
function toHex3(buffer) {
    return Array.prototype.map.call(buffer, x => `${LUT_HEX_4b[(x >>> 4) & 0xF]}${LUT_HEX_4b[x & 0xF]}`).join('');
}

// Pre-Init
//const LUT_HEX_4b = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'];
// End Pre-Init
function toHex4(buffer) {
    return Array.prototype.map.call(buffer, x => (LUT_HEX_4b[(x >>> 4) & 0xF] + LUT_HEX_4b[x & 0xF])).join('');
}

// Pre-Init
const LUT_HEX_4b = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'];
const LUT_HEX_8b = new Array(0x100);
for (let n = 0; n < 0x100; n++) {
    LUT_HEX_8b[n] = `${LUT_HEX_4b[(n >>> 4) & 0xF]}${LUT_HEX_4b[n & 0xF]}`;
}
// End Pre-Init
function toHex5(buffer) {
    return Array.prototype.map.call(buffer, x => LUT_HEX_8b[x]).join('');
}
*/