
var PRECISION = 3;      // number of decimal places
        
var viewer = null;
var aspectRatio = null;
        
function init() {
    //var tileSource = new OpenSeadragon.TileSource(sd_init_width, sd_init_height, 256, 1);
    //tileSource.getTileUrl = function (level, x, y) {
    //    return "/Visual/Image/" + vmid + "?level=" + level + "&x=" + x + "&y=" + y;
    //}

    //viewer.open(tileSource);

    viewer = OpenSeadragon({
        id: "container",
        //debugMode: true,
        prefixUrl: "/Scripts/OpenSeadragon/images/",
        tileSources: {
            width: sd_init_width,
            height: sd_init_height,
            tileSize: 256,
            minLevel: 1,
            getTileUrl: function (level, x, y) {
                return "/Visual/Image/" + vmid + "?level=" + level + "&x=" + x + "&y=" + y;
                //return "http://localhost:60542/Visual/Image/1?level=8&x=0&y=0";
            }
        }
    });
    
    viewer.addHandler("open", addOverlays);

    if (typeof setCSSSize === "function")
        viewer.addHandler("animation", setCSSSize);
}

OpenSeadragon.addEvent(window, "load", init);

//This function may be overridden by themed content (NER)
function addOverlaySensor(id, imageUrl, point, size, cssClass) {

    if (size == null)
        size = viewer.viewport.deltaPointsFromPixels(new OpenSeadragon.Point(48, 30));

    if (size.x > 1)
        size = viewer.viewport.deltaPointsFromPixels(size);

    if (cssClass == null || cssClass.length == 0)
        cssClass = "";

    aspectRatio = size.y / size.x;

    var div = document.createElement("div");
    div.style.height = size.y + 'px';
    div.style.width = size.x + 'px';
    div.style.cursor = 'pointer';
    div.id = id;

    //img = document.createElement("div");
    div.style.backgroundImage = 'url("' + imageUrl + '")';
    div.style.backgroundSize = '100%';
    div.style.backgroundRepeat = 'no-repeat';
    div.className = cssClass;
    //div.appendChild(img);
    
    var rect = new OpenSeadragon.Rect(
            point.x, point.y,
            size.x, size.y);
    viewer.addOverlay(div, rect);
    
    $(div).tipTip({ content: "<div class='tiptipDiv'></div>" });
    $(div).hover(loadSensorData, clearSensorData)
    draggable(div);
            
    var menu1 = visualContextMenu(id);//May be Theme overridden but function in visualContextMenu.js
    var menu = $(div).contextMenu(menu1, { theme: 'vista', direction: 'up', beforeShow: function () { setTimeout("$('.context-menu-shadow').prev().css('z-index', 100000000);", 50); } });
            
    return div;
}

function loadSensorData() {
    $('.tiptipDiv').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

    $.get("/Sensor/DetailsSmallOneView/" + $(this).attr("id"), function(data) {
        $('.tiptipDiv').html(data);
    });
}
function clearSensorData() {
    $('.tiptipDiv').html('');
}

function loadFullSensor(id) {
    newModal("Sensor Details", "/Sensor/Details/" + id + "?IsModal=true",450,700);
    modalDiv.parent().css('z-index', 100000002);
    modalDiv.parent().parent.css('z-index', 100000001);
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
    setTimeout('if (viewer.viewport.getZoom() != zoomPreClick) viewer.viewport.zoomTo(zoomPreClick);',1);
}
function onMouseWheel(event){
    if(ClearAddOns)
        ClearAddOns();
}

function startPlacing(id, iconUrl) {
    $('#SensorList' + id).hide();
    var startingPoint = viewer.viewport.getCenter();
    $.post("/Visual/Placement/", { "id": id, "vmid": vmid , "x": startingPoint.x, "y": startingPoint.y }, function(data) {
        addOverlaySensor(id, iconUrl, startingPoint);
    });
}

function remove(id) {
    $('#SensorList' + id).show();
    $.get("/Visual/Remove/" + id, function(data) {
        viewer.removeOverlay(document.getElementById(id));
    });
}
        
function newVisualMap(anchor) {
    newModal("New Visual Map", $(anchor).attr("href"));
}
        
function editVisualMap(anchor) {
    newModal("Edit Visual Map", $(anchor).attr("href"));
}
        
function showHelp(anchor){
    newModal("Visual Map Help", $(anchor).attr("href"));
}
        
function draggable(img) {
    var tracker = new OpenSeadragon.MouseTracker(img);
    var location = null;
    var originalLocation = null;
    var mouseDownTime = null;

    tracker.pressHandler = function (tracker, position) {
        $(img).removeShadow();
        $(img).dropShadow();
        $(img).css('cursor', 'move');
        mouseDownTime = new Date().getTime();
        viewer.setMouseNavEnabled(false);

        location = OpenSeadragon.getElementPosition(img).minus(OpenSeadragon.getElementPosition(viewer.element));  // remember where piece was originally drawn
        originalLocation = location;
    };

    tracker.dragHandler = function (event){
        
        location = location.plus(event.delta);

        img.style.left = location.x + "px";
        img.style.top = location.y + "px";

        var pixel = OpenSeadragon.getElementPosition(img).minus(OpenSeadragon.getElementPosition(viewer.element));
        var size = new OpenSeadragon.Point($(img).width(), $(img).height());

        var placingPoint = viewer.viewport.pointFromPixel(pixel);
        var placingSize = viewer.viewport.deltaPointsFromPixels(size);

        var rect = new OpenSeadragon.Rect(
                placingPoint.x, placingPoint.y,
                placingSize.x, placingSize.y);
        viewer.updateOverlay(img, rect);

        $(img).redrawShadow();

    };

    tracker.releaseHandler = function (event) {
        if (!event.insideElementPressed) {
            return;         // ignore presses from outside
        }
        $(img).removeShadow();
        $(img).css('cursor', 'pointer');
        viewer.setMouseNavEnabled(true);
        var deltaDist = location.distanceTo(originalLocation);
        location = null;


        var mouseUpTime = new Date().getTime();
        var deltaTime = mouseUpTime - mouseDownTime;
        mouseDownTime = null;
        
        // same logic as MouseTracker:
        var quick = deltaTime <= event.clickTimeThreshold && deltaDist <= event.clickDistThreshold;
        if (quick) {
            loadFullSensor(img.id);
        }
        else {
            var pixel = OpenSeadragon.getElementPosition(img).minus(OpenSeadragon.getElementPosition(viewer.element));
            var size = new OpenSeadragon.Point($(img).width(), $(img).height());

            var placingPoint = viewer.viewport.pointFromPixel(pixel);
            var placingSize = viewer.viewport.deltaPointsFromPixels(size);

            var container = viewer.viewport.getContainerSize();
            if (pixel.x + size.x < 0 || pixel.x > container.x || pixel.y + size.y < 0 || pixel.y > container.y) {
                placingPoint = viewer.viewport.pointFromPixel(originalLocation);
            }

            updateOverlay(img, placingPoint, placingSize);
        }

        originalLocation = null;
    };

    tracker.setTracking(true);  // begin tracking
}

function updateOverlay(img, point, size) {

    $.post("/Visual/Placement/", { "id": img.id, "vmid": vmid, "x": point.x, "y": point.y, "w": size.x, "h": size.y }, function (data) {
        var rect = new OpenSeadragon.Rect(
                point.x, point.y,
                size.x, size.y);

        viewer.updateOverlay(img, rect);
    });
}