var PRECISION = 3;     // number of decimal places
var snapshotLoaded = false;
var viewer = null;
var aspectRatio = null;
        
function init() {

    viewer = OpenSeadragon({
        id: "container",
        //debugMode: true,
        prefixUrl: "/Scripts/OpenSeadragon/images/",
        tileSources: {
            minLevel: 8,
            width: sd_init_width,
            height: sd_init_height,
            tileSize: 256,            
            tileOverlap: 1,           
            getTileUrl: function (level, x, y) {
                return "/Map/Image/" + vmid + "?level=" + level + "&x=" + x + "&y=" + y;                
            }   
        }
    });
    
    viewer.addHandler("open", addOverlays);

    if (typeof setCSSSize === "function")
        viewer.addHandler("animation", setCSSSize);

    document.getElementById('container').dispatchEvent(viewerCreatedEvent);
}

const viewerCreatedEvent = new Event('viewer-created');

OpenSeadragon.addEvent(window, "load", init);

//function addOverlaySensor(id, imageUrl, point, size, cssClass) {
//    addOverlayDevice(id, imageUrl, point, size, cssClass, "sensor");
//}
//This function may be overridden by themed content (NER)
function addOverlayDevice(deviceID, imageUrl, point, size, cssClass, deviceType) {
    //if (deviceType == null) {
    //    deviceType = "sensor";
    //}

    if (size == null)
        size = viewer.viewport.deltaPointsFromPixels(new OpenSeadragon.Point(40, 40));

    if (size.x > 1)
        size = viewer.viewport.deltaPointsFromPixels(size);
    // cssClass = 'mapIcon' ?
    if (cssClass == null || cssClass.length == 0)
        cssClass = "mapIcon dropdown-toggle";



    aspectRatio = size.y / size.x;

    var div = document.createElement("div");
    
    div.style.cursor = 'pointer';
    if (deviceType == "sensor") {
        div.id = 'visMapSensor_' + deviceID;
    }
    else if (deviceType == "gateway") {
        div.id = 'visMapGateway_' + deviceID;
    }
    else {
        div.id = deviceID;
    }

    div.setAttribute('data-deviceid', deviceID); // data attributes are stored (and thus only retrieved in) lower case
    div.setAttribute('data-devicetype', deviceType);
    
    var pos = (size.x - (size.x + (size.x * 200) )) + 'px center';
    //div.style.backgroundImage = 'url("' + imageUrl + '")';
    div.innerHTML = imageUrl;
    div.style.backgroundPosition = pos; 
    div.style.boxShadow = '1pt 1pt 3pt 1pt #000000'; //#c72129'; //used inplace of border. Border breaks the OpenSeaDragon map
    div.style.borderRadius = '50%';

    //var statusColor;
    if (deviceType == "sensor") {
        $.get("/Overview/GetSensorStatus/" + deviceID, function (data) {
            // Remove all possible sensorStatus{Status}
            $(div).removeClass('sensorStatusOK sensorStatusWarning sensorStatusAlert sensorStatusOffline');
            
            //Add desired class
            $(div).addClass(deviceStatusColor(data));
        });
    }
    else if (deviceType == "gateway") {
        $.get("/Overview/GetGatewayStatus/" + deviceID, function (data) {
            // Remove all possible sensorStatus{Status}
            $(div).removeClass('sensorStatusOK sensorStatusWarning sensorStatusAlert sensorStatusOffline');

            //Add desired class
            $(div).addClass(deviceStatusColor(data));
        });
    }
    //div.style.backgroundColor = statusColor;

    //sensorBackgroundColor(div,id);
    
    div.style.backgroundSize = '100%';  
    div.style.backgroundRepeat = 'no-repeat';
    div.className = cssClass;
       
    var rect = new OpenSeadragon.Rect(point.x, point.y, size.x, size.x);
    
    viewer.addOverlay(div, rect);   
            
    //var menu1 = visualContextMenu(id);//May be Theme overridden but function in visualContextMenu.js
    //var menu = $(div).contextMenu(function () { menu1, { theme: 'vista', direction: 'up', beforeShow: function () { setTimeout("$('.context-menu-shadow').prev().css('z-index', 100000000);", 50); } } }); //2147483648
          
    MapFeatures(div, deviceID);

    //draggable(div);

    return div;
}

function loadFullDevice(deviceID, deviceType) {
    if (deviceType == "sensor") {
        window.location.replace("/Overview/SensorHome/" + deviceID);
    }
    else if (deviceType == "gateway") {
        window.location.replace("/Overview/GatewayHome/" + deviceID);
    }
}
            
var zoomPreClick;
         
function onMouseDown(event) {
    zoomPreClick = viewer.viewport.getZoom();
    if(ClearAddOns)
        ClearAddOns();
}
function onMouseUp(event) {
    // negate any zoom change that may have happened:
    if (viewer.viewport.getZoom() != zoomPreClick)
        viewer.viewport.zoomTo(zoomPreClick);
    if (ClearAddOns)
        ClearAddOns();
    setTimeout('if (viewer.viewport.getZoom() != zoomPreClick) viewer.viewport.zoomTo(zoomPreClick);',1);
}
function onMouseWheel(event){
    if(ClearAddOns)
        ClearAddOns();
}

function startPlacing(deviceID, iconUrl, deviceType) {
    var startingPoint = viewer.viewport.getCenter();
    //if (deviceType == null) {
    //    $.post("/Map/Placement/", { "id": deviceID, "vmid": vmid, "x": startingPoint.x, "y": startingPoint.y }, function (data) {
    //        addOverlaySensor(deviceID, iconUrl, startingPoint);
    //    });
    //} else {
    $.post("/Map/PlacementStatic/", { "deviceType": deviceType, "deviceID": deviceID, "visualMapID": vmid, "x": startingPoint.x, "y": startingPoint.y }, function () {
        addOverlayDevice(deviceID, iconUrl, startingPoint, null, null, deviceType);
    });
    //}
}

//function startPlacingStatic(deviceType, deviceID, iconUrl) {
//    $('#SensorList' + id).hide();
//    var startingPoint = viewer.viewport.getCenter();
//    $.post("/Map/PlacementStatic/", { "deviceType": deviceType, "deviceID": deviceID, "visualMapID": vmid, "x": startingPoint.x, "y": startingPoint.y }, function () {
//        addOverlayDevice(deviceID, iconUrl, startingPoint, null, null, deviceType);
//    })
//}

function remove(deviceID, deviceType) {
    var divID;
    if (deviceType == "sensor")
        divID = 'visMapSensor_' + deviceID;
    else if (deviceType == "gateway")
        divID = 'visMapGateway_' + deviceID;

        $.post("/Map/RemoveStatic/", { "deviceType": deviceType, "deviceID": deviceID, "visualMapID": vmid }, function (data) {
            viewer.removeOverlay(document.getElementById(divID));
        });
}

//function removeStatic(deviceType, deviceID) {
//    $('#SensorList' + id).show();
//    $.post("/Map/RemoveStatic/", { "deviceType": deviceType, "deviceID": deviceID, "visualMapID": vmid },  function (data) {
//        viewer.removeOverlay(document.getElementById(deviceID));
//    });
//}
        
function newVisualMap(anchor) {
    newModal("New Visual Map", $(anchor).attr("href"));
}
        
function editVisualMap(anchor) {
    newModal("Edit Visual Map", $(anchor).attr("href"));
}
        
function showHelp(anchor){
    newModal("Visual Map Help", $(anchor).attr("href"));
}

// Both Sensors and Gateways use the enum Monnit.eSensorStatus
function deviceStatusColor(status) {

    switch (status.toLowerCase()) {
        case "ok":
            return 'sensorStatusOK';
        case "warning":
            return 'sensorStatusWarning';
        case "alert":
            return 'sensorStatusAlert';
        case "inactive":
        case "sleeping":
        case "offline":
        default:
            return 'sensorStatusOffline';

    }
}

var blahhblahh = true;