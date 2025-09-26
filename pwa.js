//remember to increment the version # when you update the service worker
const version = "0.01",
    preCache = "PRECACHE-" + version,
    cacheList = ["/Account/LogonOV", "/Setup/NoNetwork" ];

/*
create a list (array) of urls to pre-cache for your application
*/


/*  Service Worker Event Handlers */

self.addEventListener("install", function (event) {

    //console.log("Installing the service worker!");

    event.waitUntil(
        caches.open(preCache)
            .then(function (cache) {
                return cache.addAll(cacheList);
            })
    );

    //console.log("Install Cache finished");
    
    // Force the waiting service worker to become the active service worker.
    self.skipWaiting();    
} );

self.addEventListener( "activate", function ( event ) {

    //console.log("service worker activated");

    event.waitUntil(
        //wholesale purge of previous version caches
        caches.keys().then( cacheNames => {
            cacheNames.forEach( value => {
                if ( value.indexOf( version ) < 0 ) {
                    caches.delete( value );
                }
            } );
            return;
        })
    );

    //-----------------------------------NoNetwork----------------------------//
    event.waitUntil(
        (async () => {
            // Enable navigation preload if it's supported.
            // See https://developers.google.com/web/updates/2017/02/navigation-preload
            if ("navigationPreload" in self.registration) {
                await self.registration.navigationPreload.enable();
            }
        })()
    );

    // Tell the active service worker to take control of the page immediately.
    self.clients.claim();
    //-----------------------------------NoNetwork----------------------------//

} );

self.addEventListener( "fetch", function ( event ) {

    //event.respondWith(

    //    fetch( event.request )

    //    /* check the cache first, then hit the network */
    //    /*
    //            caches.match( event.request )
    //            .then( function ( response ) {

    //                if ( response ) {
    //                    return response;
    //                }

    //                return fetch( event.request );
    //            } )
    //    */
    //);

    //-----------------------------------NoNetwork----------------------------//
    // We only want to call event.respondWith() if this is a navigation request for an HTML page.
    if (event.request.mode === "navigate") {
        event.respondWith(
            (async () => {
                try {
                    //// if logout, delete push message subscription
                    //if (event.request.url.includes('Overview/Logoff')) {
                    //    self.registration.pushManager.getSubscription().then(sub => {
                    //        var subJson = JSON.stringify(sub);
                    //        const data = new URLSearchParams();
                    //        data.append('subJson', subJson);

                    //        fetch("/Setup/DeletePushMessageSubscription/", { method: 'post', body: data })
                    //            .then(async response => {
                    //                var value = await response.text();

                    //                if (value == "Success") {
                    //                    sub.unsubscribe();
                    //                }
                    //            });

                    //        //const response = await fetch("/Setup/DeletePushMessageSubscription/", { method: 'post', body: data });
                    //        //if (response.ok) {
                    //        //    var responseValue = await response.text();

                    //        //    if (responseValue == "Success") {
                    //        //        sub.unsubscribe();
                    //        //    }
                    //        //}

                    //    });
                    //}

                    //console.log("fetch-Navigate: " + event.request);
                    // First, try to use the navigation preload response if it's supported.
                    const preloadResponse = await event.preloadResponse;
                    if (preloadResponse) {
                        return preloadResponse;
                    }

                    // Always try the network first.
                    const networkResponse = await fetch(event.request);
                    return networkResponse;
                } catch (error) {
                    // catch is only triggered if an exception is thrown, which is likely due to a network error.
                    // If fetch() returns a valid HTTP response with a response code in the 4xx or 5xx range, the catch() will NOT be called.
                    //console.log("Fetch failed; returning offline page instead.", error);

                    const cache = await caches.open(preCache);
                    const cachedResponse = await cache.match("/Setup/NoNetwork");
                    return cachedResponse;
                }
            })()
        );
    }

  // If our if() condition is false, then this fetch handler won't intercept the request.
  // If there are any other fetch handlers registered, they will get a chance to call event.respondWith(). 
  // If no fetch handlers call event.respondWith(), the request will be handled by the browser as if there were no service worker involvement.
    //-----------------------------------NoNetwork----------------------------//

} );

self.addEventListener('push', event => {
    //console.log('pwa push - ' + event.data.text());
    event.stopImmediatePropagation();
    var jsonBody = JSON.parse(event.data.text());
    var title = jsonBody["title"];

    // Reference: https://developer.mozilla.org/en-US/docs/Web/API/notification
    // Property: 'body', 'close', 'data', 'icon', 'lang', 'permission', 'requestPermission', 'tag', 'title'
    // Events: 'click', 'close', 'error', 'show'
    var options = new Object();

    for (var prop in jsonBody) {
        var propValue = jsonBody[prop];
        if (propValue.length > 0 || propValue != null)
            options[prop] = jsonBody[prop];

        //if (prop in Notification.prototype) {
        //    // Supported.
        //} else {
        //    // NOT Supported.
        //}
    }
    //console.log('--options--\r\n' + JSON.stringify(options));

    event.waitUntil(
        self.registration.showNotification(title, options)
    );
});

self.addEventListener('notificationclick', event => {
    //console.log('pwa notification click');
    var notiUrl = event.notification.data["clickUrl"];
    event.notification.close();

    // -UPDATED CODE- This looks to see if the PWA app is already open and focuses if it is
    //https://developer.mozilla.org/en-US/docs/Web/API/Clients/openWindow
    event.waitUntil(
        clients.matchAll({ type: "window" }).then((clientsArr) => {
            // If a Window tab matching the targeted URL already exists, focus that;
            const hadWindowToFocus = clientsArr.some((windowClient) =>
                windowClient.url === notiUrl
                    ? (windowClient.focus(), true)
                    : false,
            );
            // Otherwise, open a new tab to the applicable URL and focus it.
            if (!hadWindowToFocus)
                clients.openWindow(notiUrl).then((windowClient) => (windowClient ? windowClient.focus() : null));
        }),
    );

    //// This looks to see if the PWA app is already open and focuses if it is
    ////https://developer.mozilla.org/en-US/docs/Web/API/WindowClient
    //event.waitUntil(
    //    clients.matchAll({ type: "window", })
    //        .then((clientList) => {
    //            for (const client of clientList) {
    //                if (client.url === "/" && "focus" in client)
    //                    return client.focus();
    //            }
    //            if (clients.openWindow) {
    //                if (notiUrl !== "/")
    //                    return clients.openWindow(notiUrl);
    //                else
    //                    return clients.openWindow("/");
    //            }
    //        }),
    //);
});

self.addEventListener('periodicsync', event => {
    if (event.tag === 'pushsub-sync') {
        //console.log('periodicsync found');

        // Update db with latest pushsubscription
        event.waitUntil(
            self.registration.pushManager.getSubscription().then(sub => {
                //console.log('periodicsync sub start\n' + new Date());

                if (sub != undefined) {
                    var subJson = JSON.stringify(sub);

                    var jsonObj = JSON.parse(subJson);
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
                        deviceName = deviceName.substring(parenthesisIndex + 1, semicolonIndex);
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
                    subJson = JSON.stringify(jsonObj);

                    // localStorage not available in ServiceWorkers
                    //var subGuid = localStorage != null ? localStorage.getItem("pushMsgGuid") : null;
                    //if (subGuid != null && subGuid != undefined && subGuid.length > 0) {
                    //    subscriptionJson["guid"] = subGuid;
                    //}

                    //console.log('periodicsync sub found\n' + subJson + "\n----------------");
                    const data = new URLSearchParams();
                    data.append('subJson', subJson);

                    try {
                        fetch("/Setup/EnablePushMessageSubscription/", { method: 'post', body: data }).then(async response => {
                            var responseData = await response.text();

                            if (responseData.includes("Success")) {
                                //console.log("pwa-periodicsync: subscription updated");

                                var splitData = responseData.split("|");
                                var guid = splitData[1];

                                //if (guid.length > 0 && localStorage != null) {
                                //    localStorage.setItem("pushMsgGuid", guid);
                                //}
                            } else {
                                //console.log("pwa-periodicsync: else - " + responseData);
                            }
                        });
                    } catch (e) {
                        //console.log("pwa-periodicsync-catch:\n" + e);
                    }

                }
                //console.log('periodicsync sub end');
            })
        );
        //console.log('periodicsync end');
    }
});


self.addEventListener('pushsubscriptionchange', event => {

    var subJson = JSON.stringify(event.newSubscription);

    var jsonObj = JSON.parse(subJson);
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
        deviceName = deviceName.substring(parenthesisIndex + 1, semicolonIndex);
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
    subJson = JSON.stringify(jsonObj);

    //var subGuid = localStorage != null ? localStorage.getItem("pushMsgGuid") : null;
    //if (subGuid != null && subGuid != undefined && subGuid.length > 0) {
    //    subscriptionJson["guid"] = subGuid;
    //}

    const data = new URLSearchParams();
    data.append('subJson', subJson);

    fetch("/Setup/EnablePushMessageSubscription/", { method: 'post', body: data }).then(async response => {
        var responseData = await response.text();

        if (responseData.includes("Success")) {
            //console.log("pwa-pushsubscriptionchange: subscribed");

            var splitData = responseData.split("|");
            var guid = splitData[1];

            //if (guid.length > 0 && localStorage != null) {
            //    localStorage.setItem("pushMsgGuid", guid);
            //}
        } else {
            //console.log("pwa-pushsubscriptionchange: EnablePushMessageSubscription - else - " + data);
        }
    });

    //// https://developer.mozilla.org/en-US/docs/Web/API/ServiceWorkerGlobalScope/pushsubscriptionchange_event
    //// "event.oldSubscription" + "event.newSubscription" only supported in Firefox as of 11/18/2022
    //if (event.oldSubscription != undefined || event.oldSubscription != null) {
    //    var oldSubJson = JSON.stringify(event.oldSubscription);

    //    fetch("/Setup/DeletePushMessageSubscription/", { method: 'post', body: { subJson: oldSubJson } }).then(response => {

    //        if (response == "Success") {
    //            console.log("pwa-pushsubscriptionchange: unsubscribed");
    //            event.oldSubscription.unsucribe();
    //        } else {
    //            console.log("pwa-pushsubscriptionchange: DeletePushMessageSubscription - else - " + data);
    //        }
    //    });
    //}
    //if (event.newSubscription != undefined || event.newSubscription != null) {
    //    var newSubJson = JSON.stringify(event.newSubscription);

    //    fetch("/Setup/EnablePushMessageSubscription/", { method: 'post', body: { subJson: newSubJson } }).then(response => {

    //        if (response == "Success") {
    //            console.log("pwa-pushsubscriptionchange: subscribed");
    //        } else {
    //            console.log("pwa-pushsubscriptionchange: EnablePushMessageSubscription - else - " + data);
    //        }
    //    });
    //}
});