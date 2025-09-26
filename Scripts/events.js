function toggleEventStatus(anchor) {    
    var div = $(anchor).children('div.sensor');

    if (div.hasClass("sensorStatusOK")) {
        $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": false }, function (data) {
            if (data == "Success") {
                div.addClass("sensorStatusInactive");
                div.removeClass("sensorStatusOK");
                $("#toggleText_" + $(anchor).data("id")).html("Enable");
            }
        });
    }
    else {
        $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": true }, function (data) {
            if (data == "Success") {
                div.addClass("sensorStatusOK");
                div.removeClass("sensorStatusInactive");
                $("#toggleText_" + $(anchor).data("id")).html("Disable");
            }
        });
    }
}

