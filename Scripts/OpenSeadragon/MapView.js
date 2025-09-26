function MapFeatures(div, id) {
    //$(div).tipTip({ content: "<div class='tiptipDiv'></div>" });
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
}

function clearDeviceData(div) {
    //var id = $(div).attr("id");
    let devicedID = $(div).data('deviceid');
    $('#overlayTooltip').hide();
    $('#tooltipDiv_' + devicedID).hide();
}

$(document).on('mouseover', '.mapIcon, .mapIcon svg', function (e) {
    //id = $(this).attr('id');
    e.stopImmediatePropagation();
    if (e.target == this) { // probably not necessary
        var divObj = this;
        if (!$(this).hasClass('mapIcon')) {
            divObj = $(this).parent()[0];
        }
        if (snapshotLoaded == false) {
            loadDeviceData(divObj);
        }
    }

});

$(document).on("mouseout", '.mapIcon', function () {

    if (snapshotLoaded == false) {
        clearDeviceData(this);
    }
});

//$(document).on("mouseout", id, function () {
//    id = null;
//});

$(document).on('contextmenu', function (e) {
    if ($(e.target).hasClass('mapIcon')) {
        let item = $(e.target); //$(`#${id}`)
        var top = item.position().top;
        var left = item.position().left;
        $('#iconMenu').show();
        $("#context-menu").css({
            display: "block",
            top: top + 75,
            left: left + 75,
        })
            .addClass("show")
        $('#viewDetails').attr("onclick", `loadFullDevice(${item.data('deviceid')}, '${item.data('devicetype')}')`)
        return false; //blocks default Webbrowser right click menu
    }
}).on("click", function () {
    $("#context-menu").hide();
});

$("#context-menu").on("click", function () {
    $(this).parent().removeClass("show").hide();
});
