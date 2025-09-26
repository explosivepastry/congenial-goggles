let MowiGattServicesDic = [
    {"uuid": "00001800-0000-1000-8000-00805f9b34fb", "name": "Generic Access"},
    
    {"uuid": "c7027f35-d65d-4c6b-a66f-86109e1d13b5", "name": "MOWI2 BLE Service"},
]

let MowiGattCharacteristicsDic = [
    {
        "name": "Device Name",
        "uuid": "00002a00-0000-1000-8000-00805f9b34fb"
    },
    {
        "name": "MOWI2 Status",
        "uuid": "c7028035-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Authorization Type",
        "uuid": "c7028335-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Password",
        "uuid": "c7028235-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 SSID",
        "uuid": "c7028135-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Scan Network",
        "uuid": "c7028435-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 IP Config",
        "uuid": "c7028535-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Server Address",
        "uuid": "c7028635-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Server Port",
        "uuid": "c7028735-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Primary Server Comm Status",
        "uuid": "c7028835-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Secondary Server Comm Status",
        "uuid": "c7028935-d65d-4c6b-a66f-86109e1d13b5"
    },
    {
        "name": "MOWI2 Unlock Status",
        "uuid": "c7028a35-d65d-4c6b-a66f-86109e1d13b5"
    },
    
]

$(MowiGattServicesDic).each((i, s) => {
	//console.log(s);
	let option = `<option value="${s.uuid}"> ${s.name} </option>`
	$('#service_select').append(option)
})

$(MowiGattCharacteristicsDic).each((i, c) => {
	//console.log(c);
	let option = `<option value="${c.uuid}"> ${c.name} </option>`
	$('#characteristic_select').append(option)
})