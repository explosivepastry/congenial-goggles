let AllGattServicesDic = [
    {"uuid": "00001800-0000-1000-8000-00805f9b34fb", "name": "Generic Access"},
    {"uuid": "00001801-0000-1000-8000-00805f9b34fb", "name": "Generic Attribute"},
    {"uuid": "00001802-0000-1000-8000-00805f9b34fb", "name": "Immediate Alert"},
    {"uuid": "00001803-0000-1000-8000-00805f9b34fb", "name": "Link Loss"},
    {"uuid": "00001804-0000-1000-8000-00805f9b34fb", "name": "Tx Power"},
    {"uuid": "00001805-0000-1000-8000-00805f9b34fb", "name": "Current Time Service"},
    {"uuid": "00001806-0000-1000-8000-00805f9b34fb", "name": "Reference Time Update Service"},
    {"uuid": "00001807-0000-1000-8000-00805f9b34fb", "name": "Next DST Change Service"},
    {"uuid": "00001808-0000-1000-8000-00805f9b34fb", "name": "Glucose"},
    {"uuid": "00001809-0000-1000-8000-00805f9b34fb", "name": "Health Thermometer"},
    {"uuid": "0000180a-0000-1000-8000-00805f9b34fb", "name": "Device Information"},
    {"uuid": "0000180d-0000-1000-8000-00805f9b34fb", "name": "Heart Rate"},
    {"uuid": "0000180e-0000-1000-8000-00805f9b34fb", "name": "Phone Alert Status Service"},
    {"uuid": "0000180f-0000-1000-8000-00805f9b34fb", "name": "Battery Service"},
    {"uuid": "00001810-0000-1000-8000-00805f9b34fb", "name": "Blood Pressure"},
    {"uuid": "00001811-0000-1000-8000-00805f9b34fb", "name": "Alert Notification Service"},
    {"uuid": "00001812-0000-1000-8000-00805f9b34fb", "name": "Human Interface Device"},
    {"uuid": "00001813-0000-1000-8000-00805f9b34fb", "name": "Scan Parameters"},
    {"uuid": "00001814-0000-1000-8000-00805f9b34fb", "name": "Running Speed and Cadence"},
    {"uuid": "00001815-0000-1000-8000-00805f9b34fb", "name": "Automation IO"},
    {"uuid": "00001816-0000-1000-8000-00805f9b34fb", "name": "Cycling Speed and Cadence"},
    {"uuid": "00001818-0000-1000-8000-00805f9b34fb", "name": "Cycling Power"},
    {"uuid": "00001819-0000-1000-8000-00805f9b34fb", "name": "Location and Navigation"},
    {"uuid": "0000181a-0000-1000-8000-00805f9b34fb", "name": "Environmental Sensing"},
    {"uuid": "0000181b-0000-1000-8000-00805f9b34fb", "name": "Body Composition"},
    {"uuid": "0000181c-0000-1000-8000-00805f9b34fb", "name": "User Data"},
    {"uuid": "0000181d-0000-1000-8000-00805f9b34fb", "name": "Weight Scale"},
    {"uuid": "0000181e-0000-1000-8000-00805f9b34fb", "name": "Bond Management Service"},
    {"uuid": "0000181f-0000-1000-8000-00805f9b34fb", "name": "Continuous Glucose Monitoring"},
    {"uuid": "00001820-0000-1000-8000-00805f9b34fb", "name": "Internet Protocol Support Service"},
    {"uuid": "00001821-0000-1000-8000-00805f9b34fb", "name": "Indoor Positioning"},
    {"uuid": "00001822-0000-1000-8000-00805f9b34fb", "name": "Pulse Oximeter Service"},
    {"uuid": "00001823-0000-1000-8000-00805f9b34fb", "name": "HTTP Proxy"},
    {"uuid": "00001824-0000-1000-8000-00805f9b34fb", "name": "Transport Discovery"},
    {"uuid": "00001825-0000-1000-8000-00805f9b34fb", "name": "Object Transfer Service"},
    {"uuid": "00001826-0000-1000-8000-00805f9b34fb", "name": "Fitness Machine"},
    {"uuid": "00001827-0000-1000-8000-00805f9b34fb", "name": "Mesh Provisioning Service"},
    {"uuid": "00001828-0000-1000-8000-00805f9b34fb", "name": "Mesh Proxy Service"},
    {"uuid": "00001829-0000-1000-8000-00805f9b34fb", "name": "Reconnection Configuration"},
    {"uuid": "c7027f35-d65d-4c6b-a66f-86109e1d13b5", "name": "MOWI2 BLE Service"},
]

// console.log("AllGattServicessDic	=	", AllGattServicessDic);
// console.log();
// console.log('AllGattServicessDic[0]	=	', AllGattServicessDic[0]);
// console.log();

function ServiceByUuid(uuid) {
	return AllGattServicesDic.filter((obj) => { return obj.uuid == uuid })[0]
}

function ServiceByName(name) {
	return AllGattServicesDic.filter((obj) => { return obj.name == name })[0]
}

// function execute_param(func) {
    // func();
// }

// execute_param(function() { console.log(this); console.log(this.uuid); console.log(this.name); }.bind(GattByUuid("00002a00-0000-1000-8000-00805f9b34fb")))

// execute_param(function() { console.log(this); console.log(this.uuid); console.log(this.name); }.bind(GattByName("Device Name")))

