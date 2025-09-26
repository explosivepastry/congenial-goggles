let AllGattDescriptorsDic = [
    {"uuid": "0000290e-0000-1000-8000-00805f9b34fb", "name": "Time Trigger Setting"},
    {"uuid": "0000290d-0000-1000-8000-00805f9b34fb", "name": "Environmental Sensing Trigger Setting"},
    {"uuid": "0000290c-0000-1000-8000-00805f9b34fb", "name": "Environmental Sensing Measurement"},
    {"uuid": "0000290b-0000-1000-8000-00805f9b34fb", "name": "Environmental Sensing Configuration"},
    {"uuid": "0000290a-0000-1000-8000-00805f9b34fb", "name": "Value Trigger Setting"},
    {"uuid": "00002909-0000-1000-8000-00805f9b34fb", "name": "Number of Digitals"},
    {"uuid": "00002908-0000-1000-8000-00805f9b34fb", "name": "Report Reference"},
    {"uuid": "00002907-0000-1000-8000-00805f9b34fb", "name": "External Report Reference"},
    {"uuid": "00002906-0000-1000-8000-00805f9b34fb", "name": "Valid Range"},
    {"uuid": "00002905-0000-1000-8000-00805f9b34fb", "name": "Characteristic Aggregate Format"},
    {"uuid": "00002904-0000-1000-8000-00805f9b34fb", "name": "Characteristic Presentation Format"},
    {"uuid": "00002903-0000-1000-8000-00805f9b34fb", "name": "Server Characteristic Configuration"},
    {"uuid": "00002902-0000-1000-8000-00805f9b34fb", "name": "Client Characteristic Configuration"},
    {"uuid": "00002901-0000-1000-8000-00805f9b34fb", "name": "Characteristic User Description"},
    {"uuid": "00002900-0000-1000-8000-00805f9b34fb", "name": "Characteristic Extended Properties"}
]

// console.log("AllGattDescriptorsDic	=	", AllGattDescriptorsDic);
// console.log();
// console.log('AllGattDescriptorsDic[0]	=	', AllGattDescriptorsDic[0]);
// console.log();

function DescriptorByUuid(uuid) {
	return AllGattDescriptorsDic.filter((obj) => { return obj.uuid == uuid })[0]
}

function DescriptorByName(name) {
	return AllGattDescriptorsDic.filter((obj) => { return obj.name == name })[0]
}

// function execute_param(func) {
    // func();
// }

// execute_param(function() { console.log(this); console.log(this.uuid); console.log(this.name); }.bind(GattByUuid("00002a00-0000-1000-8000-00805f9b34fb")))

// execute_param(function() { console.log(this); console.log(this.uuid); console.log(this.name); }.bind(GattByName("Device Name")))
