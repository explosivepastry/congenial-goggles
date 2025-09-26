
document.getElementById('container').addEventListener('viewer-created',
    () => {
        viewer.addHandler('add-overlay', doDraggable);
    });

document.getElementById('container').addEventListener('viewer-created',
    () => {
        viewer.addHandler('remove-overlay', undoDraggable);
    });



//function MapFeatures(div, id) {

//    draggable(div);      
//}

let deviceTrackers = {};

//function doDraggable() {
//    $('.mapIcon').each(function () {
//        draggable(this);
//    });
//}

function doDraggable(event) {
    let div = event.element;
    draggable(div);
}

function undoDraggable(event) {
    let div = event.element;
    if (deviceTrackers[div.id]) {
        deviceTrackers[div.id].destroy();
        delete deviceTrackers[div.id];
    }
}

function draggable(div) {
    if (!deviceTrackers[div.id]) {
        var tracker = new OpenSeadragon.MouseTracker(div);
        var location = null;
        var originalLocation = null;
        var mouseDownTime = null;

        tracker.pressHandler = function (tracker, position) {
            $(div).css('cursor', 'move');
            mouseDownTime = new Date().getTime();
            viewer.setMouseNavEnabled(false);

            location = OpenSeadragon.getElementPosition(div).minus(OpenSeadragon.getElementPosition(viewer.element));  // remember where piece was originally drawn
            originalLocation = location;
        };

        tracker.dragHandler = function (event) {

            location = location.plus(event.delta);

            div.style.left = location.x + "px";
            div.style.top = location.y + "px";

            var pixel = OpenSeadragon.getElementPosition(div).minus(OpenSeadragon.getElementPosition(viewer.element));
            var size = new OpenSeadragon.Point($(div).width(), $(div).height());

            var placingPoint = viewer.viewport.pointFromPixel(pixel);
            var placingSize = viewer.viewport.deltaPointsFromPixels(size);

            var rect = new OpenSeadragon.Rect(
                placingPoint.x, placingPoint.y,
                placingSize.x, placingSize.y);
            viewer.updateOverlay(div, rect);
        };

        tracker.releaseHandler = function (event) {
            if (!event.insideElementPressed) {
                return;         // ignore presses from outside
            }
            $(div).css('cursor', 'pointer');
            viewer.setMouseNavEnabled(true);
            var deltaDist = location.distanceTo(originalLocation);
            location = null;


            var mouseUpTime = new Date().getTime();
            var deltaTime = mouseUpTime - mouseDownTime;
            mouseDownTime = null;

            // same logic as MouseTracker:
            var quick = deltaTime <= event.clickTimeThreshold && deltaDist <= event.clickDistThreshold;
            if (quick) {
                loadFullDevice(div.data('deviceid'), div.data('devicetype'));
            }
            else {
                var pixel = OpenSeadragon.getElementPosition(div).minus(OpenSeadragon.getElementPosition(viewer.element));
                var size = new OpenSeadragon.Point($(div).width(), $(div).height());

                var placingPoint = viewer.viewport.pointFromPixel(pixel);
                var placingSize = viewer.viewport.deltaPointsFromPixels(size);

                var container = viewer.viewport.getContainerSize();
                if (pixel.x + size.x < 0 || pixel.x > container.x || pixel.y + size.y < 0 || pixel.y > container.y) {
                    placingPoint = viewer.viewport.pointFromPixel(originalLocation);
                }

                updateOverlay(div, placingPoint, placingSize);
            }

            originalLocation = null;
        };

        tracker.setTracking(true);  // begin tracking
        deviceTrackers[div.id] = tracker;
    }
}

function updateOverlay(div, point, size) {
    $.post("/Map/PlacementStatic/", { "deviceType": $(div).data("devicetype"), "deviceID": $(div).data("deviceid"), "visualMapID": vmid, "x": point.x, "y": point.y, "w": size.x, "h": size.y }, function () {
        var rect = new OpenSeadragon.Rect(
            point.x, point.y,
            size.x, size.y);

        viewer.updateOverlay(div, rect);
    });
}


//added for tooltip functionality.
function MapFeatures(div, id) {
    $('#overlayTooltip').append("<div id='tooltipDiv_" + id + "' class='tip' style='display: none;' ></div>");
}

function loadDeviceData(div) {
    $('#overlayTooltip').show();
    var id;
    if ($(div).data('deviceid') == null) {
        id = $(div).attr("id");
    }
    else {
        id = $(div).data('deviceid');
    }
    var tip = $('#tooltipDiv_' + id);
    tip.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

    var tipx = $(div).position().left,
        tipy = $(div).position().top;

    tip.css({ top: tipy + 18, left: tipx + 6, position: 'absolute' });
    tip.show().css({ opacity: 1 });

    // todo:jfk find out Gateway equivlent of "/Sensor/DetailsSmallOneview/{id}"
    // No such thing as Gateways don't receive DataMessages, instead reproduce look with Status as the content
    if ($(div).data('devicetype') == 'sensor')
        $.get("/Sensor/DetailsSmallOneview/" + id, function (data) {
            tip.html(data);
        });
    else if ($(div).data('devicetype') == 'gateway')
        $.get("/Map/GatewayDetailsSmallOneView/" + id, function (data) {
            tip.html(data);
        });

    if (!deviceTrackers[div.id]) {
        draggable(div);
    }
}

function clearDeviceData(div) {
    //var id = $(div).attr("id");
    var deviceID = $(div).data('deviceid');
    $('#overlayTooltip').hide();
    $('#tooltipDiv_' + deviceID).hide();
}

$(document).on('mouseenter', '.mapIcon', function (e) {
    //id = $(this).attr('id');
    if (e.target == this) { // probably not necessary
        if (snapshotLoaded == false) {
            loadDeviceData(this);
        }
    }

});

$(document).on("mouseout", '.mapIcon', function () {
    if (snapshotLoaded == false) {
        clearDeviceData(this);
    }
});

$(document).on('contextmenu', function (e) {
    let item = $(e.target); //$(`#${id}`)
    item = item.closest('.mapIcon');
    if (item.hasClass('mapIcon')) {
        var top = item.position().top;
        var left = item.position().left;
        $('#iconMenu').show();
        $("#context-menu").css({
            display: "block",
            top: top + 75,
            left: left + 75,
        })
            .addClass("show")
        $('#bigger').off('click');
        $('#smaller').off('click');
        $('#bigger').on('click', function () { bigger(item[0]); })
        $('#smaller').on('click', function () { smaller(item[0]); })
        return false; //blocks default Webbrowser right click menu
    }
}).on("click", function () {
    $("#context-menu").hide();
});

$("#context-menu").on("click", function () {
    $(this).parent().removeClass("show").hide();
});

var biggerCount = 0;
function bigger(div) {
    console.log(div.id);
    biggerCount += 1;
    var width = $(div).width() + 10;
    //var i = div[0]; // unpackage DOM element from jQuery wrapper

    var pixel = OpenSeadragon.getElementPosition(div).minus(OpenSeadragon.getElementPosition(viewer.element));
    var size = new OpenSeadragon.Point(width, width * aspectRatio);

    var placingPoint = viewer.viewport.pointFromPixel(pixel);
    var placingSize = viewer.viewport.deltaPointsFromPixels(size);

    updateOverlay(div, placingPoint, placingSize);
    console.log('biggerCount = ' + biggerCount);
}

var smallerCount = 0;
function smaller(div) {
    smallerCount += 1;
    var width = $(div).width() - 10;
    //var i = div[0]; // unpackage DOM element from jQuery wrapper

    if (width < 10) return;

    var pixel = OpenSeadragon.getElementPosition(div).minus(OpenSeadragon.getElementPosition(viewer.element));
    var size = new OpenSeadragon.Point(width, width * aspectRatio);

    var placingPoint = viewer.viewport.pointFromPixel(pixel);
    var placingSize = viewer.viewport.deltaPointsFromPixels(size);

    updateOverlay(div, placingPoint, placingSize);
    console.log('smallerCount = ' + smallerCount);
}

$("#loadSnapshot").hide();