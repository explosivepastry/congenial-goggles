
function updatePushMessageSub(subscriptionObj, responseMessageElementId, refreshPage = false) {

    var subscriptionJson = JSON.stringify(subscriptionObj);

    var subGuid = localStorage != null ? localStorage.getItem("pushMsgGuid") : null;
    if (subGuid != null && subGuid != undefined && subGuid.length > 0) {
        var jsonObj = JSON.parse(subscriptionJson);
        jsonObj["guid"] = subGuid;

        subscriptionJson = JSON.stringify(jsonObj);
    }

    var jsonObj = JSON.parse(subscriptionJson);

    var deviceName = navigator.userAgent;
    if (deviceName.includes('Android')) {
        deviceName = navigator.userAgent.split(';')[1];
        deviceName = deviceName.split(';')[0];
    } else if (deviceName.includes('iPhone')) {
        var names = navigator.userAgent.split(';')[1].split(' ');
        deviceName = names[2] + ' ' + names[4];
    } else {
        var parenthesisIndex = deviceName.indexOf('(');
        var semicolonIndex = deviceName.indexOf(';');
        deviceName = deviceName.substring(parenthesisIndex+1, semicolonIndex);
    }

    if (navigator.userAgent.includes('Edg')) {
        deviceName += " - Edge";
    } else if (navigator.userAgent.includes('Chrome')) {
        deviceName += " - Chrome";
    } else if (navigator.userAgent.includes('Firefox')) {
        deviceName += " - Firefox";
    } else if (navigator.userAgent.includes('Safari')) {
        deviceName += " - Safari";
    } else {
        deviceName = navigator.userAgent;
    }
    jsonObj["name"] = deviceName;

    subscriptionJson = JSON.stringify(jsonObj);

    $.post('/Setup/EnablePushMessageSubscription/', { subJson: subscriptionJson }, function (data) {

        if (responseMessageElementId.length > 0) {

            if (data.includes("Success")) {
                //console.log("Push notification subscribed");
                $('#' + responseMessageElementId)[0].checked = true;

                var splitData = data.split("|");
                var guid = splitData[1];

                if (guid.length > 0 && localStorage != null) {
                    localStorage.setItem("pushMsgGuid", guid);
                }
                if (refreshPage) {
                    location.reload();
                }
            } else {
                //console.log("AllowPushNotification - else - " + data);
                $('#' + responseMessageElementId)[0].checked = false;
            }
        }
    });
}
function deletePushMessageSub(subscriptionObj, responseMessageElementId, refreshPage = false) {

    var subscriptionJson = JSON.stringify(subscriptionObj);
    $.post('/Setup/DeletePushMessageSubscription/', { subJson: subscriptionJson }, function (data) {
        if (responseMessageElementId.length > 0) {
            if (data == "Success") {
                //console.log("Push notification unsubscribed");
                $('#' + responseMessageElementId)[0].checked = false;

                if (refreshPage) {
                    location.reload();
                }
            } else {
                //console.log("DeletePushMessageSubscription - else - " + data);
                $('#' + responseMessageElementId)[0].checked = true;
            }
        }
    });
}
function urlB64ToUint8Array(base64String) {
    var padding = '='.repeat((4 - (base64String.length % 4)) % 4);
    var base64 = (base64String + padding).replace(/\-/g, '+').replace(/_/g, '/');
    var rawData = atob(base64);
    var outputArray = new Uint8Array(rawData.length);
    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}