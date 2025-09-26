
$(document).ready(function () {
    window.setInterval('serverSessionKeepAlive()', 300000); //milliseconds 60,000 = 60 sec // 300,000 = 5 min

    /* set variables locally for increased performance */
    var scroll_timer;
    var displayed = false;
    var $scrollToTop = $('#scrollToTop a');
    var $window = $(window);
    var top = $(document.body).children(0).position().top;

    /* react to scroll event on window */
    $window.scroll(function () {
        window.clearTimeout(scroll_timer);
        scroll_timer = window.setTimeout(function () { // use a timer for performance
            if ($window.scrollTop() <= top) // hide if at the top of the page
            {
                displayed = false;
                $scrollToTop.fadeOut(500);
            }
            else if (displayed == false) // show if scrolling down
            {
                displayed = true;
                $scrollToTop.stop(true, true).show().click(function () { $scrollToTop.fadeOut(500); });
            }
        }, 100);
    });
});

//var platform = {};
var platform = {
    isCompatible: false,
    nativePrompt: "onbeforeinstallprompt" in window
};

function checkPlatform() {
    // browser info and capability
    var _ua = window.navigator.userAgent;

    //platform.NotSet = true;
    platform.isIDevice = /iphone|ipod|ipad/i.test(_ua);
    platform.isSamsung = /Samsung/i.test(_ua);
    platform.isFireFox = /Firefox/i.test(_ua);
    platform.isOpera = /opr/i.test(_ua);
    platform.isEdge = /edg/i.test(_ua);
    platform.isAndroid = /android/i.test(_ua);

    // Opera & FireFox only Trigger on Android
    if (platform.isFireFox) {
        platform.isFireFox = platform.isAndroid;
    }

    if (platform.isOpera) {
        platform.isOpera = platform.isAndroid;
    }

    platform.isChromium = "onbeforeinstallprompt" in window;
    platform.isInWebAppiOS = window.navigator.standalone === true;
    platform.isInWebAppChrome =
        window.matchMedia("(display-mode: fullscreen)").matches ||
        window.matchMedia("(display-mode: standalone)").matches ||
        window.matchMedia("(display-mode: minimal-ui)").matches;
    platform.isMobileSafari =
        platform.isIDevice &&
        _ua.indexOf("Safari") > -1 &&
        _ua.indexOf("CriOS") < 0;
    platform.isStandalone = platform.isInWebAppiOS || platform.isInWebAppChrome;
    platform.isiPad = platform.isMobileSafari && _ua.indexOf("iPad") > -1;
    platform.isiPhone = platform.isMobileSafari && _ua.indexOf("iPad") === -1;
    platform.isCompatible =
        platform.isChromium ||
        platform.isMobileSafari ||
        platform.isSamsung ||
        platform.isFireFox ||
        platform.isOpera ||
        platform.isIDevice;

    ////var count = 0;
    //for (let value of Object.values(platform)) {
    //    //if (count == 0) continue;
    //    if (value != undefined && value != null) {
    //        if (value && platform.NotSet) {
    //            platform.NotSet = false;
    //            //return;
    //        }
    //    }
    //    //count++;
    //}
}
checkPlatform();

function serverSessionKeepAlive() {
    $.get('/AutoRefresh/ServerSessionKeepAlive', function (data) {
        eval(data);
    });
}

function keepAlive() {
    //this method part of autoLogout.js if included on page
    if (typeof resetSessionTimeout === "function")
        resetSessionTimeout();
}

var modalDiv = null;

function newModal(title, href, height, width) {
    if (modalDiv != null) {
        modalDiv.remove();
        modalDiv = null;
    }
    if (!window.location.href.toLowerCase().includes("retail/premierecredit")) {
        modalDiv = $('<div title="' + title + '">')
        modalDiv.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

        if (height == null) height = 450;
        if (width == null) width = 550;
        modalDiv.dialog({
            height: height,
            width: width,
            modal: true
        });

        if (href && href.length > 0) {
            keepAlive();
            jQuery.get(href, function (data) {
                modalDiv.html(data);
            });
        }
    }


}

// ex: range(60, 0) will produce the minutes in an hour (0..59)
function range(size, startAt = 0) {
    return [...Array(size).keys()].map(i => i + startAt);
}

const arrayBuilder = (minValue, maxValue, incrementBy) => {
    let array = [];
    for (let i = minValue; i <= maxValue; i += incrementBy) {
        array.push(i);
    }
    return array;
}

const arrayBuilderForDecimals = (minValue, maxValue, incrementBy) => {
    let array = [];
    for (let i = minValue; i <= maxValue; i += incrementBy) {
        array.push(Number(i.toFixed(10)));
    }
    return array;
}

function findClosestNumberInArray(array, target) {
    if (array.length === 0) {
        throw new Error("Array is empty");
    }

    let closest = array[0];
    let smallestDiff = Math.abs(array[0] - target);

    for (let i = 1; i < array.length; i++) {
        let currentDiff = Math.abs(array[i] - target);
        if (currentDiff < smallestDiff) {
            closest = array[i];
            smallestDiff = currentDiff;
        }
    }

    return closest;
}

function isANumber(value) {
    // jQuery isNumeric: !isNaN( parseFloat(obj) ) && isFinite( obj )
    // this seems to work equivalently
    return !isNaN(+value) && typeof (+value) == 'number';

    // Unnecessary
    //if (typeof (value) == 'number')
    //    return true;

    // NO! This will match numbers followed by letters

    //var RegExpressn = /(?:\-\d*\.)?\d+/;

    //if (value.search(RegExpressn) == -1) //if match failed
    //    return false;
    //else
    //    return true;
}

function isNullOrWhiteSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
}

function hideModal() {
    modalDiv.dialog('close');
    modalDiv.remove();
    modalDiv = null;
}

function postModal() {
    keepAlive();
    var form = modalDiv.children('form');
    $.post(form.attr("action"), form.serialize(), function (data) {
        modalDiv.html(data);
    }, "text");

    modalDiv.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
}

function SetLeftNavHandlers() {
    $(".leftNav").children().click(function (e) {
        getMain($(this).attr("href"), $(this).html(), true);
        return false;
    });

    $(".leftNavEdit").children().click(function (e) {
        getMain($(this).attr("href"), $(this).attr("title"));
        return false;
    });
}

var returnUrl;
var returnTitle;

function getMain(url, title, setReturn) {
    keepAlive();
    if (!url) {
        url = returnUrl;
        title = returnTitle;
    }
    if (!title) {
        title = "Information";
    }

    if (setReturn) {
        returnUrl = url;
        returnTitle = title;
    }

    if ($(window).scrollTop() > $('#MainTitle').offset().top) {
        $(window).scrollTop($('#MainTitle').offset().top);
    }
    $('#Main').hide();
    $('#MainTitle').html(title);
    $('#MainLoading').fadeIn('fast');
    var xhr = $.get(url, function (data) {
        $('#Main').html(data).fadeIn();
        $('#MainLoading').hide();
    });

    return xhr;
}

function postMain(callback) {
    keepAlive();
    var form = $('form');
    $('#Main').hide('fast');
    $('#MainLoading').show('fast');
    var xhr = $.post(form.attr("action"), form.serialize(), function (data) {
        $('#Main').html(data);
        $('#MainLoading').hide('fast', function () { $('#Main').show('slow'); });


        if (typeof callback == "string") {
            eval(callback);
        }
        if (jQuery.isFunction(callback)) {
            callback(data);
        }
    });

    return xhr;
}

function resetCount(SensorID) {
    alert('unable to reset counter');
    //Checking if this method is still called,  should be added to correct page rather than master.js
    //12/30/2014 Brandon Young

    //$.ajax({
    //    type: "Post",
    //    url: "/Sensor/resetCounter",
    //    done: "Success",
    //    fail: "Failed",
    //    data: { id: SensorID}
    //});

}

function checkForm(sensorId, minHeartBeat, monnitApplicationID) {
    if (minHeartBeat == null) {
        minHeartBeat = 60;
    }
    var reportInterval = $('#simpleEdit_' + sensorId + ' input[Name="ReportInterval"]').val();

    let bodyMessage = "";
    if (reportInterval < 1 && reportInterval < minHeartBeat && monnitApplicationID != 53) {
        bodyMessage = "Heartbeats of less than one minute will severely impact battery life and network performance. \r\nAnd additional charges may be incurred. \r\nPress OK to acknowledge.";
    }
    else if (reportInterval < minHeartBeat && monnitApplicationID != 53) {
        bodyMessage = "Low heartbeats may severely impact battery life. \r\nPress OK to acknowledge.";
    }


    if (bodyMessage.length > 0) {
        let values = {};
        values.callback = function () { modalCheckFormCallback(sensorId, reportInterval) };
        values.text = bodyMessage;
        openConfirm(values);
    }
    else {
        $('#save').hide(); $('#saving').show();
        if (monnitApplicationID == '143') {
            postForm2($('#simpleEdit_' + sensorId));
        }
        else {
            postForm($('#simpleEdit_' + sensorId));
        }
    }
}


function modalCheckFormCallback(sensorId, reportInterval) {
    $('#save').hide(); $('#saving').show();
    jQuery.get('/Sensor/Acknowledge', { sensorID: sensorId, heartbeat: reportInterval }, function (data) {
        postForm($('#simpleEdit_' + sensorId));
    });
}// end function checkform

//function checkGatewayForm(gatewayId, gatewayTypeID, minHeartBeat) {

//    var reportInterval = $('#gatewayEdit_' + gatewayId + ' input[Name="ReportInterval"]');

//    if (gatewayTypeID == 17 || gatewayTypeID == 18 || gatewayTypeID == 22 || gatewayTypeID == 23 || gatewayTypeID == 30 || gatewayTypeID == 32) {

//        if (reportInterval.val() <= 5) {
//            if (confirm("Heartbeats of less then 5 minutes will severely impact data consumption\r\nand additional charges may be incurred. \r\nPress OK to acknowledge.")) {
//                jQuery.get('/Overview/Acknowledge', { gatewayID: gatewayId, heartbeat: reportInterval.val() }, function (data) {
//                    postForm($('#gatewayEdit_' + gatewayId));
//                });
//            }
//        }
//        else if (reportInterval.val() <= 10) {
//            if (confirm("Heartbeats of ten minutes or less will impact data usage.\r\nThis can cause additional charges to be incurred.\r\nPress OK to acknowledge.")) {
//                jQuery.get('/Overview/Acknowledge', { gatewayID: gatewayId, heartbeat: reportInterval.val() }, function (data) {
//                    postForm($('#gatewayEdit_' + gatewayId));
//                });
//            }
//        } else {

//            postForm($('#gatewayEdit_' + gatewayId));
//        }

//    } else {

//        postForm($('#gatewayEdit_' + gatewayId));
//    }
//}

function postForm(bgForm, callback) {
    keepAlive();

    var parent = bgForm.parent();
    var postData = bgForm.serialize();

    var xhr = $.post(bgForm.attr("action"), postData)
        .done(function (data) {
            parent.html(data);

            if (typeof callback == "string") {
                //alert('callback isString');
                eval(callback);
            }
            if (jQuery.isFunction(callback)) {
                //alert('callback isFunction');
                callback(data);
            }
        })
        .fail(function (data) {
            $('#saving').hide(); $('#save').show();
            $('#confirmLoad').hide(); $('#modalSubmit').show();

            showAlertModal(data.statusText);
        });

    return xhr;
}

function postForm2(bgForm) {

    keepAlive();

    var parent = bgForm.parent();
    var postData = bgForm.serialize();

    var xhr = $.post(bgForm.attr("action"), postData, function (data) {
        parent.html(data);


    });

    return xhr;

}

$(document).ready(function () {
    var securityToken = $('[name=__RequestVerificationToken]').val();
    $(document).ajaxSend(function (event, request, opt) {
        if (opt.hasContent && securityToken) {   // handle all verbs with content
            var tokenParam = "__RequestVerificationToken=" + encodeURIComponent(securityToken);
            opt.data = opt.data ? [opt.data, tokenParam].join("&") : tokenParam;
            // ensure Content-Type header is present!
            if (opt.contentType !== false || event.contentType) {
                request.setRequestHeader("Content-Type", opt.contentType);
            }
        }
    });
});


// #region Suneditor functions

function createSunEditor(editorID, enableEditSource = false) { //, saveToInputID) {
    var returnValue = null;
    var editorObj = $('#' + editorID);
    if (editorObj != undefined && editorObj.length > 0) {
        //var toolbarDiv = $('#toolbar_container');
        //if (toolbarDiv == undefined) {
        //    editorObj.before('<div id="toolbar_container"></div>');
        //}

        var buttonListObj = [['bold', 'underline', 'italic', 'font', 'fontSize', 'horizontalRule', 'list', 'link', 'removeFormat']];

        if (enableEditSource)
            buttonListObj = [['bold', 'underline', 'italic', 'font', 'fontSize', 'horizontalRule', 'list', 'link', 'removeFormat', 'codeView']];

        returnValue = SUNEDITOR.create(editorID, {
            //toolbarContainer: 'toolbar_container',
            //showPathLabel: false,
            //charCounter: true,
            //maxCharCount: 720,
            width: 'auto',
            minWidth: '275px',
            maxWidth: '700px',
            //height: 'auto',
            minHeight: '200px',
            //maxHeight: '250px',
            //katex: katex,
            buttonList: buttonListObj
            //buttonList: [
            //    //['undo', 'redo', 'font', 'fontSize', 'formatBlock'],
            //    //Advanced Only//['fullScreen', 'showBlocks', 'codeView', 'subscript', 'superscript'],
            //    //'/',
            //    ['bold', 'underline', 'italic', 'font', 'fontSize', 'horizontalRule', 'list', 'link', 'removeFormat'],
            //    //'/', // Line break
            //    //['fontColor', 'textStyle', 'hiliteColor', 'outdent', 'indent', 'align', 'lineHeight', 'table'],
            //    //['link', 'image', 'video', 'audio'],//, 'math'],
            //    //// (min-width: 992)
            //    //,['%992', [
            //    //    ['undo', 'redo'],
            //    //    [':p-More Paragraph-default.more_paragraph', 'font', 'fontSize', 'formatBlock', 'paragraphStyle', 'blockquote'],
            //    //    ['bold', 'underline', 'italic', 'strike'],
            //    //    [':t-More Text-default.more_text', 'subscript', 'superscript', 'fontColor', 'hiliteColor', 'textStyle'],
            //    //    ['removeFormat'],
            //    //    ['outdent', 'indent'],
            //    //    ['align', 'horizontalRule', 'list', 'lineHeight'],
            //    //    ['-right', ':i-More Misc-default.more_vertical', 'fullScreen', 'showBlocks', 'codeView', 'preview', 'print', 'save', 'template'],
            //    //    ['-right', ':r-More Rich-default.more_plus', 'table', 'link', 'image', 'video', 'audio', 'math', 'imageGallery']
            //    //]],
            //    //// (min-width: 767)
            //    //['%767', [
            //    //    ['undo', 'redo'],
            //    //    [':p-More Paragraph-default.more_paragraph', 'font', 'fontSize', 'formatBlock', 'paragraphStyle', 'blockquote'],
            //    //    [':t-More Text-default.more_text', 'bold', 'underline', 'italic', 'strike', 'subscript', 'superscript', 'fontColor', 'hiliteColor', 'textStyle'],
            //    //    ['removeFormat'],
            //    //    ['outdent', 'indent'],
            //    //    [':e-More Line-default.more_horizontal', 'align', 'horizontalRule', 'list', 'lineHeight'],
            //    //    [':r-More Rich-default.more_plus', 'table', 'link', 'image', 'video', 'audio', 'math', 'imageGallery'],
            //    //    ['-right', ':i-More Misc-default.more_vertical', 'fullScreen', 'showBlocks', 'codeView', 'preview', 'print', 'save', 'template']
            //    //]],
            //    //,'template']
            //],
            //templates: [
            //    {
            //        name: 'Template-1',
            //        html: '<p>HTML source1</p>'
            //    },
            //    {
            //        name: 'Template-2',
            //        html: '<p>HTML source2</p>'
            //    }
            //],
            //callBackSave: function (contents, isChanged) {
            //    if (isChanged) {
            //        $('#' + saveToInputID).val(contents);
            //    }
            //}
        });
    }

    return returnValue;
}

// #endregion

function setAproxTime() {
    if ($('#MeasurementsPerTransmission').length > 0) {
        var aprox = $("#ReportInterval").val() / $('#MeasurementsPerTransmission').val();
        if (aprox >= 1)
            $('#AproxAssessmentTime').html("Assessment frequency about " + aprox + " minutes.");
        else
            $('#AproxAssessmentTime').html("Assessment frequency less than 1 minute.");
    }
}


function refreshDatePicker(isPremium) {
    var minDays = -90;
    if (isPremium) {
        minDays = -365;
    }

    $('.daterange').daterangepicker({
        presets: { specificDate: 'Pick a date', dateRange: 'Date Range' },
        presetRanges: [
            { text: 'Today', dateStart: 'today', dateEnd: 'today' },
            { text: 'Last 7 days', dateStart: 'Today-7days', dateEnd: 'Today' },
            { text: 'Last 30 days', dateStart: 'today-30days', dateEnd: 'today' },
            { text: 'Month to date', dateStart: function () { return Date.parse('Today').moveToFirstDayOfMonth(); }, dateEnd: 'Today' },
        ],

        posX: '0px',
        posY: '0px',

        appendTo: '.daterangeDiv',
        onOpen: function () { $('.daterangeDiv').show().center(); },
        onClose: function () { $('.daterangeDiv').hide(); setTimeout('setDates();', 10); },
        datepickerOptions: { minDate: minDays, maxDate: 0 }
    });

}

function setDates() {
    keepAlive();
    $.get('/Sensor/SetDateRange', 'range=' + $('.daterange').val(), function (data) {
        var tabContainter = $('.tabContainer').tabs();
        //var selected = tabContainter.tabs('option', 'selected');
        var active = tabContainter.tabs('option', 'active');
        tabContainter.tabs('load', active);

        if (modalDiv != null && modalDiv.find('.daterange').length > 0) { //Multi Chart is open
            var ids = "";
            $('#Main :checked').each(function () {
                ids += $(this).val() + "|";
            });
            if (ids.length > 0) {
                var href = '/Report/ChartMultiple?ids=' + ids;
                jQuery.get(href, function (data) {
                    modalDiv.html(data);
                });
            }
        }
    });

}

function refreshAllTabs() {

    $('ul.refreshTab li img').bind('click', function () {
        //jQuery(this).addClass('ui.refreshTab li img-state-active')
        //.siblings().removeClass('ui.refreshTab li img-state-active');
    });
}

function proxyCustomer(anchor, callback) {
    $.get($(anchor).attr("href"), function (data) {
        if (data == "Success") {
            if (typeof callback == "string") {
                window.location.href = callback;
            }
            else if (jQuery.isFunction(callback)) {
                callback();
            }
            else {
                window.location.href = '/Overview';
            }
        }
        else {
            showAlertModal(data);
        }
    });
}

function viewAccountQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    if (href == undefined || href == null || href.length == 0)
        href = anchor.attr('data-href');

    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = "/Overview";
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}
function viewEventsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = "/Events";
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
        }
    });
}
function viewRulesQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success")
            window.location.href = "/Rule";
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
    });
}
function viewSensorsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = "/Overview/SensorIndex";
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}
function viewSubsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = "/Settings/AdminSubscriptionDetails/" + acctID;
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}

function viewLocationsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    if (href == undefined || href == null || href.length == 0)
        href = anchor.attr('data-href');

    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = "/Settings/LocationOverview/" + acctID;
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}

function viewDestinationQuick(lnk) {
    var anchor = $(lnk);
    var dest = anchor.data('destination');
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');

    $.post(href, { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = dest + acctID;
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}

function proxySubAccountAndRedirect(lnk) {
    var anchor = $(lnk);
    var dest = anchor.data('destination');
    var acctID = anchor.data('accountid');

    $.post("/Account/ProxySubAccount/", { id: acctID }, function (data) {
        if (data == "Success") {
            window.location.href = dest + acctID;
        }
        else {
            $('#proxyMessage_' + acctID).html('Proxy Failed');
            showAlertModal(data);
        }
    });
}


//===============================================================================================
//  Ajax Shortcuts
//===============================================================================================
function ajaxDiv(divID, url, callback) {
    keepAlive();
    var div = $('#' + divID);
    var xhr = null;

    div.slideUp('slow', function () {

        div.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
        div.slideDown('fast');

        xhr = $.get(url, function (data) {
            div.slideUp('fast', function () {
                div.html(data);
                if (typeof callback == "string") {
                    eval(callback);
                }
                if (jQuery.isFunction(callback)) {
                    callback();
                }
                div.slideDown('fast');
            });
        });
    });

    return xhr;
}

function ajaxPostDiv(divID, replace) {
    keepAlive();
    var div = $('#' + divID);
    var xhr = null;
    var loadingDiv = getLoadingDiv(divID);
    var form = div.children('form');
    div.hide('fast', function () {
        loadingDiv.show('fast');
        xhr = $.post(form.attr("action"), form.serialize(), function (data) {
            if (replace)
                div.replaceWith(data);
            else
                div.html(data);
            loadingDiv.hide('fast', function () { div.show('slow'); });
        });
    });

    return xhr;
}

function getLoadingDiv(divID) {
    var div = $('#Loading_' + divID);
    if (div.length == 0) {
        div = $('<div/>', {
            id: '#Loading_' + divID,
            //text: 'Loading...',
            title: 'Loading...'
        });
        div.insertAfter('#' + divID);
    }
    div.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
    return div;
}

var lookupOrigVal = null;
function createLookup(elemID, autoCompleteURL, extraParams, hdnElemID, idLookupURL, extraIDParams) {

    if (extraParams == null)
        extraParams = "";
    if (extraIDParams == null)
        extraIDParams = "";


    $("#" + elemID).autocomplete(autoCompleteURL, extraParams);

    $("#" + elemID).result(function (e, data, formatted) {
        //$("input[type='submit']").attr("disabled", "disabled");
        $("#" + hdnElemID)[0].value = data;
        jQuery.get(idLookupURL, 'name=' + data + extraIDParams, function (id) {
            if (parseInt(id) == id - 0) {
                $("#" + hdnElemID)[0].value = id;
            }
            //$("input[type='submit']").attr("disabled", "");
        });
    });

    $("#" + elemID).focus(function (e) {
        lookupOrigVal = $(this)[0].value;
    });

    $("#" + elemID).blur(function (e) {
        if (lookupOrigVal != $(this)[0].value) {
            //$("input[type='submit']").attr("disabled", "disabled");
            $("#" + hdnElemID)[0].value = $(this)[0].value;
            jQuery.get(idLookupURL, 'name=' + $(this)[0].value + extraIDParams, function (id) {
                if (parseInt(id) == id - 0) {
                    $("#" + hdnElemID)[0].value = id;
                }
                //$("input[type='submit']").attr("disabled", "");
            });
        }
    });
}


function checkLookup() {

}

function queryString(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

//Adds a horizontal center function to jquery
jQuery.fn.center = function () {
    this.css("position", "fixed");
    //this.css("top", ( $(window).height() - this.height() ) / 2+$(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
}

//Troubleshooting helpers
function simpleObjInspect(oObj, key, tabLvl) {

    var s = "";
    if (typeof oObj == "object" && oObj !== null) {
        s += typeof oObj + "\n";
        for (var k in oObj) {
            if (oObj.hasOwnProperty(k)) {
                s += k + " : " + oObj[k] + "\n";
            }
        }
    } else {
        s += "" + oObj + " (" + typeof oObj + ") \n";
    }
    return s;
}


var Mobiscroll_numpad_Char_decimal = '.';

var Mobiscroll_numpad_Char_thousands = ',';

function Mobiscroll_decimal_parseValue(value) {
    if (value) {
        return value.toString().replace(Mobiscroll_numpad_Char_thousands, '').split('');
    }
    return [];
}


function Mobiscroll_decimal_formatValue(numbers, vars, inst) {
    var ret = '',
        l = numbers.length,
        decimals,
        i;

    // Add leading zeroes if necessary
    if (numbers[0] == '.') {
        numbers.unshift(0);
        l++;
    }

    ret = numbers.join('').split(Mobiscroll_numpad_Char_decimal);

    ret[0] = ret[0].replace(/\B(?=(\d{3})+(?!\d))/g, Mobiscroll_numpad_Char_thousands);

    return ret.join(Mobiscroll_numpad_Char_decimal);
}



function Mobiscroll_decimal_validate(event, inst) {


    var s = inst.settings,
        values = event.values,
        vars = event.variables,
        disabledButtons = [],
        invalid = false;

    if (values.length >= s.maxLength || values.indexOf(Mobiscroll_numpad_Char_decimal) !== -1) {

        disabledButtons.push(Mobiscroll_numpad_Char_decimal);
    }

    if (values.length == 1 && values[0] === 0) {
        for (var i = 1; i <= 9; i++) {
            disabledButtons.push(i);
        }
    }

    if (!values.length || values[values.length - 1] == Mobiscroll_numpad_Char_decimal) {
        invalid = true;
    }

    // Only allow max maxScale decimal values
    if (values.length > (s.maxScale + 1) && values[values.length - s.maxScale - 1] == Mobiscroll_numpad_Char_decimal) {
        for (var i = 0; i <= 9; i++) {
            disabledButtons.push(i);
        }
    }

    // Display the formatted value
    if (inst.isVisible()) {
        $('.mbsc-np-dsp', inst._markup).html(inst.settings.formatValue(values, vars, inst) || '&nbsp;');
    }

    return {
        invalid: invalid,
        disabled: disabledButtons
    };

}

// foramtters for 1view chart customer preference dates
// Date needs to be converted to local time before calling these methods
function TimePreferenceFormat(dateVal, timeformat) {
    var date = new Date(dateVal);
    var hours = date.getUTCHours();
    var minutes = ('0' + date.getUTCMinutes()).slice(-2);
    var seconds = ('0' + date.getUTCSeconds()).slice(-2);
    var output = '';

    switch (timeformat) {
        case "H:mm":
        case 'HH:mm':
            output = hours + ':' + minutes;
            break;
        case 'H:mm:ss':
        case 'HH:mm:ss':
        case 'HH:mm.ss':
            output = hours + ':' + minutes + ':' + seconds;
            break;
        case 'h:mm':
        case 'h:mm tt':
        default:
            var suffix = "AM";
            if (hours >= 12) {
                suffix = "PM";
                hours = hours - 12;
            }
            if (hours === 0) {
                hours = 12;
            }
            output = hours + ':' + minutes + " " + suffix;

            break;
    }

    return output;
}


function DatePreferenceFormat(dateVal, timeformat) {
    var date = new Date(dateVal);
    var day = ('0' + date.getUTCDate()).slice(-2);
    var month = ('0' + (date.getUTCMonth() + 1)).slice(-2);
    var year = date.getUTCFullYear();
    var output = '';

    switch (timeformat) {

        case "MM/dd/yy":
        case "M/dd/yy":
        case "MM/d/yy":
        case "M/d/yy":
        case "MM-dd-yy":
        case "M-dd-yy":
        case "MM-d-yy":
        case "M-d-yy":
        case "MM.dd.yy":
        case "M.dd.yy":
        case "MM.d.yy":
        case "M.d.yy":
            output = month + '/' + day + '/' + (year.toString()).slice(-2);;
            break;
        case "dd/MM/yyyy":
        case "d/MM/yyyy":
        case "dd/M/yyyy":
        case "d/M/yyyy":
        case "dd-MM-yyyy":
        case "d-MM-yyyy":
        case "dd-M-yyyy":
        case "d-M-yyyy":
        case "dd.MM.yyyy":
        case "d.MM.yyyy":
        case "dd.M.yyyy":
        case "d.M.yyyy":
            output = day + '/' + month + '/' + year;
            break;
        case "yyyy-MM-dd":
        case "yyyy-M-dd":
        case "yyyy-MM-d":
        case "yyyy-M-d":
        case "yyyy/MM/dd":
        case "yyyy/M/dd":
        case "yyyy/MM/d":
        case "yyyy/M/d":
        case "yyyy.MM.dd":
        case "yyyy.M.dd":
        case "yyyy.MM.d":
        case "yyyy.M.d":
            output = year + '/' + month + '/' + day;
            break;
        case "yy-MM-dd":
        case "yy-M-dd":
        case "yy-MM-d":
        case "yy-M-d":
        case "yy/MM/dd":
        case "yy/M/dd":
        case "yy/MM/d":
        case "yy/M/d":
        case "yy.MM.dd":
        case "yy.M.dd":
        case "yy.MM.d":
        case "yy.M.d":
            output = (year.toString()).slice(-2) + '/' + month + '/' + day;
            break;
        case "dd-MM-yy":
        case "d-MM-yy":
        case "dd-M-yy":
        case "d-M-yy":
        case "dd/MM/yy":
        case "d/MM/yy":
        case "dd/M/yy":
        case "d/M/yy":
        case "dd.MM.yy":
        case "d.MM.yy":
        case "dd.M.yy":
        case "d.M.yy":
            output = day + '/' + month + '/' + (year.toString()).slice(-2);
            break;
        case "yy-MM-dd":
        case "yy-M-dd":
        case "yy-MM-d":
        case "yy-M-d":
        case "yy/MM/dd":
        case "yy/M/dd":
        case "yy/MM/d":
        case "yy/M/d":
        case "yy.MM.dd":
        case "yy.M.dd":
        case "yy.MM.d":
        case "yy.M.d":
            output = (year.toString()).slice(-2) + '/' + month + '/' + day;
            break;
        case "MM/dd":
        case "M/dd":
        case "MM/d":
        case "MM-dd":
        case "M-dd":
        case "MM-d":
        case "MM.dd":
        case "M.dd":
        case "MM.d":
            output = month + '/' + day;
            break;
        case "MM/dd/yyyy":
        case "M/dd/yyyy":
        case "MM/d/yyyy":
        case "M/d/yyyy":
        case "MM-dd-yyyy":
        case "M-dd-yyyy":
        case "MM-d-yyyy":
        case "M-d-yyyy":
        case "MM.dd.yyyy":
        case "M.dd.yyyy":
        case "MM.d.yyyy":
        case "M.d.yyyy":
        default:
            output = month + '/' + day + '/' + year;
            break;


    }

    return output;
}

function showAlertModal(data) {
    //$('#alertModal').modal('show');
    //$('#alertModal #alertModalMessage').text(data);

    showSimpleMessageModal(data, true);
}

function showInfoModal(data) {
    showSimpleMessageModal(data, false);
}

function showSimpleMessageModal(message, isError) {
    if (message.toLowerCase() == 'logoff') {
        window.location.href = "/Account/LogOff";
    }

    $('#messageModal .modal-body .message').text(message);

    if (isError) {
        $('#messageModal .modal-body').css({ "background-color": "#f8d7da", "color": "#842029", "border-color": "#f5c2c7" });
    } else {
        $('#messageModal .modal-body').css({ "background-color": "", "color": "", "border-color": "" });
    }

    $('#messageModal').modal('show');
}

// Bootstrap 3.5 modal -- START

function modalConfirm(gatewayId, gatewayTypeID) {

    var reportInterval = $('#gatewayEdit_' + gatewayId + ' input[Name="ReportInterval"]').val();

    //Cell GatewayTypes  (30 = IOT Gateway..  Included For now??)
    if ([8, 9, 13, 14, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 30, 32].indexOf(gatewayTypeID) > -1) {
        let bodyMessage = "";

        if (reportInterval < 5) {
            bodyMessage = "Heartbeats of less then 5 minutes will severely impact data consumption and additional charges may be incurred. Press OK to acknowledge."
        }

        else if (reportInterval < 10) {
            bodyMessage = "Heartbeats of ten minutes or less impact data usage. This can cause additional charges to be incurred. Press OK to acknowledge."
        }


        if (bodyMessage.length > 0 && reportInterval > 0) {
            let values = {};
            values.callback = function () { modalConfirmCallback(gatewayId, reportInterval) };
            values.text = bodyMessage;
            openConfirm(values);
        }
        else {
            postForm($('#gatewayEdit_' + gatewayId));
        }
    }
    else {
        postForm($('#gatewayEdit_' + gatewayId));
    }
}

function modalConfirmCallback(gatewayId, reportInterval) {
    jQuery.get('/CSNet/Acknowledge', { gatewayID: gatewayId, heartbeat: reportInterval }, function (data) {
        postForm($('#gatewayEdit_' + gatewayId));
    });
}


// Boostrap 3.5 modal -- END

//confirm modal -- start

let openConfirmLink = null;
let confirmRedirect = false;
let partialTag = null;
let params = null;
let callback = null;
let cancelCallback = null;

let openConfirm = (values) => {
    $('#confirmModal').modal('show');
    $('#confirmModal .modal-body .message').text(`${values.text}`);
    $('#confirmModal .modal-body').html(values.html); // passing 'html' will overwrite 'text'
    //$('#confirmModal .submit').text(`${btnText}`);
    openConfirmLink = values.url;
    values.redirect != null ? confirmRedirect = values.redirect : "";

    if (values.partialTag != undefined) {
        partialTag = values.partialTag;
    }

    if (values.params != undefined) {
        params = values.params;
    }

    if (values.callback != undefined) {
        callback = values.callback;
    }

    if (values.cancelCallback != undefined) {
        cancelCallback = values.cancelCallback;
    }
}

let cancelPost = () => {

    if (typeof cancelCallback === 'function') {
        cancelCallback();
        //$('#confirmModal').modal('hide');
    }
}

let confirmPost = url => {
    if (openConfirmLink == null) {

        //Redirect
        if (confirmRedirect != false) {
            if (confirmRedirect == "#") {
                $('#modalCancel').click();
            }
            window.location.href = confirmRedirect;
        }
        else {
            //call registered callback method
            if (typeof callback === 'function') {
                $('#confirmModal').modal('hide');
                $('#confirmModal #confirmLoad').hide();
                $('#confirmModal #modalSubmit').show();
                callback();
            }
        }
    } else {
        $.post(`${openConfirmLink}`, params, function (data) {
            if (typeof callback === 'function') {
                $('#confirmModal').modal('hide');
                $('#confirmModal #confirmLoad').hide();
                $('#confirmModal #modalSubmit').show();
                callback(data);
            }
            else if (data == "Success") {
                confirmRedirect == false ? location.reload() : window.location.href = `${confirmRedirect}`;
            }
            else if (partialTag != null) {
                partialTag.html(data);
                $('#confirmModal').modal('hide');
                $('#confirmModal #confirmLoad').hide();
                $('#confirmModal #modalSubmit').show();
            }
            else if (data == "Logout") {
                window.location.href = '/overview';
            }
            else {
                $('#confirmModal .alert').show();
                $('#confirmModal .modal-body .error').text(`${data}`);
                $('#confirmLoad').hide();
                $('#confirmModal #modalSubmit').show();
                setTimeout(function () {
                    $('#confirmModal .alert').fadeOut("slow");
                }, 3000);
                setTimeout(function () {
                    $('#confirmModal .modal-body .error').text("");
                }, 3200);
            }
        });
    }
}

//confirm modal -- end

// Custom JS confirm modal (to work for mobile)
// Modal message is that text that displays in the modal
// must use callback to block page and wait for user input
// implimentation examples on Overview/_GatewayDetails and Setup/StatusVerification
function confirmCustom(modalMessage, callback, okText, cancelText) {

    $("#confirmCustom").dialog({
        resizable: false,
        width: "auto",
        modal: false,
        draggable: false,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
                $('#confirmCustomOverlay').hide();
                callback(true);
            },
            CANCEL: function () {
                $(this).dialog("close");
                $('#confirmCustomOverlay').hide();
                callback(false);
            }
        }
    })

    $('#confirmCustomOverlay').show();

    $('.ui-dialog button:first-of-type').addClass('confirmCustomOK');
    $('#confirmCustomMessage').html(`<h4>${modalMessage}</h4>`);
    //okText ? $('confirmCustomOK').text(okText) : "OK";

}

// Custom JS confirm modal (to work for mobile)
// Modal message is that text that displays in the modal
function alertCustom(modalMessage) {

    $("#confirmCustom").dialog({
        resizable: false,
        width: "auto",
        modal: false,
        draggable: false,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
                $('#confirmCustomOverlay').hide();
            }
        }
    })

    $('#confirmCustomOverlay').show();

    $('.ui-dialog button:first-of-type').addClass('confirmCustomOK');
    $('#confirmCustomMessage').html(`<h4>${modalMessage}</h4>`);

}

function numberToPercent(x, minDec = 0, maxDec = 2) {
    return new Intl.NumberFormat(
        'default',
        {
            style: 'percent',
            minimumFractionDigits: minDec,
            maximumFractionDigits: maxDec,
        }
    ).format(x / 100)
}

// #region Favorites
function favoriteItemClickEvent(clickedObj, removeFavoriteAlertText, addFavoriteAlertText, favoriteType) {
    var obj = $(clickedObj);
    var id = obj.attr('data-id');
    var isFav = obj.attr('data-fav') == "true";

    let values = {};
    values.text = isFav ? removeFavoriteAlertText : addFavoriteAlertText;
    values.url = '/Overview/FavoritesToggle/';
    values.params = { id: id, isFav: isFav, type: favoriteType };

    values.callback = function (data) {

        if (data == "Success") {
            if (isFav) {
                //$('#favoriteItem').attr('data-fav', 'false');
                //$('#favoriteItem svg').removeClass('liked');
                //for pages with 2 favorite icons (e.g. LocationsOverview 1 on card & 1 in 3 dot menu)
                let favs = obj.closest('.searchCardDiv.corp-card').find('.favoriteItem');
                // for pages with 1 favorite icon
                favs = favs.add($('#favoriteItem'));
                favs.attr('data-fav', 'false');
                favs.find('svg').removeClass('liked');
                //favs.filter('.listOfFav').hide();
                favs.filter('.listOfFav').css('visibility', 'hidden');
            } else {
                //$('#favoriteItem').attr('data-fav', 'true');
                //$('#favoriteItem svg').addClass('liked');
                let favs = obj.closest('.searchCardDiv.corp-card').find('.favoriteItem');
                favs = favs.add($('#favoriteItem'));
                favs.attr('data-fav', 'true');
                favs.find('svg').addClass('liked');
                favs.filter('.listOfFav').show();
                favs.filter('.listOfFav').css('visibility', 'visible');
            }

        } else {
            // CurrentCustomerFavorites cache was out of sync with view
            // FavoritesToggle should have invalidated the cache
            // Show error message then reload to refresh cache and view
            //let values = {}
            //values.callback = function () { window.location.reload(); };
            //values.text = data;
            //openConfirm(values);

            // Don't alert them something is wrong. Just reload the window.
            //window.location.reload();

            //Show them they have exceeded favorite limit
            toastBuilder(data);
        }
    }

    openConfirm(values);
}

function removeFavorite(clickedObj, removeFavoriteAlertText, favoriteType) {
    event.stopPropagation();
    var obj = $(clickedObj);
    var id = obj.attr('data-id');
    var isFav = true;

    let values = {};
    values.text = removeFavoriteAlertText;
    values.url = '/Overview/FavoritesToggle/';
    values.params = { id: id, isFav: isFav, type: favoriteType };

    values.callback = function (data) {
        if (data == "Success") {
            obj.closest('.small-list-card').remove();
        } else {
            window.location.reload();
        }

    }

    openConfirm(values);
}


function isFavoriteItemCheck(isFavorite) {
    if (isFavorite) {
        $('#favoriteItem svg').addClass('liked');
    } else {
        $('#favoriteItem svg').removeClass('liked');
    }
}

$('.menu-hover, .dropdown-menu.ddm, .svg_icon.svg_menu, .menuIcon').click(function (e) {
    e.stopPropagation();
});
// #endregion

// Form Unsaved Changes Alert
function disableUnsavedChangesAlert() {
    //DO Nothing  Remove after we are confident all references have been removed.
}


// #region Vanilla JS functions

function addEventListenerForClass(eventName, className, functionParam) {
    var items = document.getElementsByClassName(className);
    for (i = 0; i < items.length; i++) {
        var item = items[i];
        item.addEventListener(eventName, functionParam);
    }
}

//---------------------------------------------------------------------------------------------------------------- Create Spinner Modal --------------------------------------------------------------------------------------------------------------

//Create Spinner Modal Documentation for Imonnit.
//The createSpinnerModal() function allows you to easily create a spinner modal within Imonnit. Follow the guidelines below to implement the spinner modal:

//Usage:

//Navigate to the view where you want to add the spinner modal.
//In the corresponding JavaScript file or script tag, add the createSpinnerModal() function.

//Examples:

//Used to select numbers:
//createSpinnerModal("openSpinnerNumberModal", "Test Modal", "SpinnerTestInput", [100, 200, 300, 400, 500, 600, 700, 800], 4);
//Used to select strings:
//createSpinnerModal("openSpinnerNumberModal2", "Test Modal2", "SpinnerTestInput2", ["happy", "sad", "nervous"], 1);
//Used to select a whole number and decimal:
//createSpinnerModal("openSpinnerNumberModal2", "Test Modal2", "SpinnerTestInput2", [1, 2, 3, 4, 5, 6, 7, 8, 9, 10], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

//Parameters:

//First parameter(string) is the ID of the element that you would like to open the modal onced clicked.
//Second parameter(string) is the Title that you would like to display in the modal.Titles with more than 47 characters will start to negatively effect the spinner. 47 characters example => Test Modal with really really long name this will
//Third parameter(string) is the input that will change once the user selects an item in the modal. Often times this is a hidden input.
//Fourth parameter(array) is an array of values that you would like the user to choose from in the modal.
//Fifth parameter(number)[OPTIONAL] is the INDEX of the starting value. EX: If value array is: ["happy", "sad", "nervous"]. If a 1 is passed in, the first value highlighted will be sad. By passing in a value it will always highlight that value even if the input value is something different.
//*If a fifth parameter is not passed in, it will default to the value of the input(third param).
//*If the input value is not a value passed into the param 4 array. The first highlighted value will pick the closest value that is in the param 4 array. EX: input value is 1.87, array passed into param 4: [0,1,2,3]. The two will be highlighted when the modal is opened.
//Sixth parameter(array)[OPTIONAL] If present in the function call it will create a second spinner wheel.

//Live Examples:

//The double spinner can be viewed here: https://staging.imonnit.com/Overview/SensorEdit/2221111189
//Click the icon that is next to the current shift aware amps input.

//Go to: https://staging.imonnit.com/Overview/GatewayEdit/123456
//Click on the icon that is next to the Heartbeat Minutes input field.
//The spinner modal should be displayed.

let isBodyScrollEnabled = false;
let scrollTimeout;

const createSpinnerModal = (openButtonSelectorId, modalTitle, inputToChangeId, valuesArray, initialIndex, valuesArraySecond, initialIndexSecond) => {
    //Generate a random and unique ID that will be concatenated to all of html IDs for this particular instance. This will help differentiate multiple number spinners on the same page.
    const generateRandomId = (length) => {
        const characters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
        let id = '';
        for (let i = 0; i < length; i++) {
            const randomIndex = Math.floor(Math.random() * characters.length);
            id += characters.charAt(randomIndex);
        }
        return id;
    }

    const generateUniqueId = (length = 36) => {
        let id;
        const existingIds = new Set(Array.from(document.querySelectorAll('[id]')).map(el => el.id));
        do {
            id = generateRandomId(length);
        } while (existingIds.has(id));
        return id;
    }

    //----------------------------------------------------Create the html-------------------------------------------------------------------------------------

    const uniqueId = generateUniqueId()

    // Create the overlay div if there isnt one already on the page.
    if (!document.querySelector(".AB-overlay-for-spinner")) {
        var overlayDiv = document.createElement('div');
        overlayDiv.classList.add('AB-overlay-for-spinner', 'no-display-for-spinner');
    } else {
        overlayDiv = document.querySelector(".AB-overlay-for-spinner")
    }

    // Create the section element
    const sectionElement = document.createElement('section');
    sectionElement.id = `userUpdateModal-${uniqueId}`;
    sectionElement.classList.add('userUpdateSpinnerModal', 'flex-column-for-spinner-ab', 'scale-in-ver-center', `${openButtonSelectorId}`);

    // Create the title 
    const titleDiv = document.createElement('div');
    titleDiv.classList.add('NumberSpinnerTitleStyle');
    titleDiv.textContent = `${modalTitle}`;

    // Create the number picker container
    const numberPickerContainer = document.createElement('div');
    numberPickerContainer.classList.add(`number-picker-container-${uniqueId}`, 'number-picker-container-ab');

    // Create the number picker div
    const numberPickerDiv = document.createElement('div');
    numberPickerDiv.classList.add('number-picker-ab');

    // Create the display div
    const displayDiv = document.createElement('div');
    displayDiv.id = `display-${uniqueId}`;
    displayDiv.classList.add('moveOnTheY');
    numberPickerDiv.classList.add('displayForNumberSpinner');

    numberPickerDiv.appendChild(displayDiv);
    numberPickerContainer.appendChild(numberPickerDiv);

    // Create the button wrapper div
    const buttonWrapperDiv = document.createElement('div');
    buttonWrapperDiv.classList.add('button-wrapper-ab');

    // Create the cancel button
    const cancelButton = document.createElement('button');
    cancelButton.id = `closeModal-${uniqueId}`;
    cancelButton.type = 'button';
    cancelButton.classList.add('blue-secondary-btn-spinner-ab', 'clickable');
    cancelButton.textContent = 'Cancel';

    // Create the set button
    const setButton = document.createElement('button');
    setButton.id = `closeModalOnRedirect-${uniqueId}`;
    setButton.type = 'button';
    setButton.classList.add('setButtonForNumberSpinner', 'blue-secondary-btn-spinner-ab', 'clickable');
    setButton.textContent = 'Set';

    buttonWrapperDiv.appendChild(cancelButton);
    buttonWrapperDiv.appendChild(setButton);
    sectionElement.appendChild(titleDiv);
    sectionElement.appendChild(numberPickerContainer);
    sectionElement.appendChild(buttonWrapperDiv);
    overlayDiv.appendChild(sectionElement);
    document.body.appendChild(overlayDiv);

    // Create the options in the spinner
    for (let i = 0; i < 3; i++) {
        valuesArray.forEach((value) => {
            const div = document.createElement('div');
            div.innerHTML = value;
            div.classList.add('valueForNumberSpinner');
            if (valuesArraySecond) { div.classList.add("borderRadiusOverwriteLeft"); }
            div.setAttribute("data-value", value);
            const displayElement = document.getElementById(`display-${uniqueId}`);
            displayElement.appendChild(div);
        });
    }

    //Create the second spinner
    if (valuesArraySecond) {

        // Create the number picker container
        const numberPickerContainer2 = document.createElement('div');
        numberPickerContainer2.classList.add(`number-picker-container-${uniqueId}-2`, 'number-picker-container-ab');

        // Create the number picker div
        const numberPickerDiv2 = document.createElement('div');
        numberPickerDiv2.classList.add('number-picker-ab');

        // Create the display div
        const displayDiv2 = document.createElement('div');
        displayDiv2.id = `display-${uniqueId}-2`;
        displayDiv2.classList.add('moveOnTheY');
        numberPickerDiv2.classList.add('displayForNumberSpinner2');
        numberPickerDiv2.appendChild(displayDiv2);
        numberPickerContainer2.appendChild(numberPickerDiv2);

        // Create a wrapper for both spinners. 
        const mainBodyOfModal = document.createElement('div');
        mainBodyOfModal.classList.add('boxOfBoxes');
        mainBodyOfModal.appendChild(numberPickerContainer);
        mainBodyOfModal.appendChild(numberPickerContainer2);

        //Place the second spinner next to the other one
        const parent = document.querySelector(`#userUpdateModal-${uniqueId}`);
        parent.insertBefore(mainBodyOfModal, parent.children[0].nextSibling);

        // Create the second set of options for the second spinner
        for (let i = 0; i < 3; i++) {
            valuesArraySecond.forEach((value) => {
                const div = document.createElement('div');
                div.innerHTML = value;
                div.classList.add('valueForNumberSpinner2');
                div.classList.add('borderRadiusOverwriteRight');
                div.setAttribute("data-value", value);
                displayDiv2.appendChild(div);
            });
        }
    }


    //----------------------------------------------------Basic modal functionality such as open, close, set input value, set initial highlighted modal value, add/remove overlay.---------------------------------------
    let startingIndex = initialIndex !== undefined && initialIndex !== null ? initialIndex : 0;
    let startingIndex2 = initialIndexSecond !== undefined && initialIndexSecond !== null ? initialIndexSecond : 0;

    if (startingIndex < 0 || startingIndex > valuesArray.length) {
        console.warn(`Spinner Param 5 (incorrect param:${startingIndex}) out of range. Range: 0 to ${valuesArray.length}.  Intial index was set to 0`)
        startingIndex = 0;
    };

    if (openButtonSelectorId.length > 0) {

        const inputToUpdate = document.querySelector(`#${inputToChangeId}`)
        const openButton = document.querySelector(`#${openButtonSelectorId}`);
        const closeModalButton = document.querySelector(`#closeModal-${uniqueId}`);
        const overlayElement = document.querySelector(".AB-overlay-for-spinner");
        const modal = document.querySelector(`#userUpdateModal-${uniqueId}`);
        const confirmButton = document.querySelector(`#closeModalOnRedirect-${uniqueId}`)

        //Sets the index that will highlight one or both values for each spinner present.
        const setStartingIndex = (valueToHightLight, spinnerToCreateHighlightFor) => {

            if (spinnerToCreateHighlightFor == 2 && valuesArraySecond) {

                const foundInputValueInTheArray = valuesArraySecond.find(n => Number(n) === Number(valueToHightLight));
                if (foundInputValueInTheArray) {
                    startingIndex2 = valuesArraySecond.indexOf(foundInputValueInTheArray)
                } else {
                    startingIndex2 = valuesArraySecond.indexOf(findClosestNumberInArray(valuesArraySecond, Number(valueToHightLight)));
                }
            } else {
                const foundInputValueInTheArray = valuesArray.find(n => n === valueToHightLight);
                if (foundInputValueInTheArray) {
                    startingIndex = valuesArray.indexOf(foundInputValueInTheArray)
                } else {
                    startingIndex = valuesArray.indexOf(findClosestNumberInArray(valuesArray, valueToHightLight));
                }
            }
        }

        if (valuesArraySecond) {
            const inputValue = inputToUpdate.value;

            if (inputValue.includes(".")) {

                const [whole, decimal] = inputValue.split(".");
                wholeNumber = parseInt(whole, 10);
                decimalNumber = "." + decimal;
                setStartingIndex(decimalNumber, 2);
                setStartingIndex(wholeNumber, 1);

            } else {
                wholeNumber = parseInt(inputValue, 10);
                decimalNumber = ".00";

                setStartingIndex(decimalNumber, 2);
                setStartingIndex(wholeNumber, 1);
            }
        } else if (inputToUpdate && inputToUpdate.value && !initialIndex) {
            const inputValue = Number(inputToUpdate.value);
            setStartingIndex(inputValue, 1);
        }

        const onOpenAnimationEnd = () => {
            modal.addEventListener("animationend", onCloseAnimationEnd);
            modal.removeEventListener("animationend", onOpenAnimationEnd);
        };

        const onCloseAnimationEnd = () => {
            modal.removeEventListener("animationend", onCloseAnimationEnd);
            overlayElement.classList.add("no-display-for-spinner");
            modal.style.display = "none";
            document.body.style.overflow = "";
        }

        const openModal = () => {
            overlayElement.classList.remove("no-display-for-spinner")
            modal.style.display = "flex";
            modal.classList.remove("scale-out-vertical");
            document.body.style.overflow = "hidden";
            modal.addEventListener("animationend", onOpenAnimationEnd);
            disableBodyScroll();
        };

        const closeModal = (setValue) => {
            enableBodyScroll();
            if (setValue) {
                const spinnerElement = document.querySelector(`#display-${uniqueId}`);
                const selectedElement = spinnerElement.querySelector(".selectedElementForNumberSpinner");
                let selectedValue2 = null;

                if (valuesArraySecond) {
                    const spinnerElement2 = document.querySelector(`#display-${uniqueId}-2`);
                    const selectedElement2 = spinnerElement2.querySelector(".selectedElementForNumberSpinner2");
                    selectedValue2 = selectedElement2.getAttribute("data-value");
                }

                const selectedValue = selectedValue2 ? selectedElement.getAttribute("data-value") + selectedValue2 : selectedElement.getAttribute("data-value");
                inputToUpdate.value = selectedValue;
            }
            modal.classList.add("scale-out-vertical");
        };

        if (openButton) {
            openButton.addEventListener("click", openModal);
        }

        closeModalButton.addEventListener("click", () => closeModal(null));

        const event = new Event("change", { bubbles: true });
        confirmButton.addEventListener("click", () => {
            closeModal(true)
            inputToUpdate.dispatchEvent(event);
        }
        );

        modal.addEventListener("click", (e) => {
            e.stopPropagation();
        });

        overlayElement.addEventListener("click", () => closeModal(null));
    }

    function disableBodyScroll() {
        const scrollY = window.scrollY;
        document.body.style.position = 'fixed';
        document.body.style.top = `-${scrollY}px`;
    }

    function enableBodyScroll() {
        if (!isBodyScrollEnabled) {
            isBodyScrollEnabled = true;

            const scrollY = parseInt(document.body.style.top || '0', 10);
            document.body.style.position = '';
            document.body.style.top = '';
            window.scrollTo({
                top: -scrollY,
                left: 0,
                behavior: "instant",
            });

            //This timeout is to prevent the issue when the page has multiple overlay click handlers.
            clearTimeout(scrollTimeout);
            scrollTimeout = setTimeout(() => {
                isBodyScrollEnabled = false;
            }, 100);
        }
    }

    const currentDisplayElement = document.querySelector(`#display-${uniqueId}`);

    //----------------------------------------------------Functionality for the number spinner within the modal. ------------------------------------------------------------------- 
    const getLatestValuesArray = () => {
        return currentDisplayElement.querySelectorAll('.valueForNumberSpinner');
    }

    const updateWheelStyles = () => {
        for (let i = 1; i <= 7; i++) {
            const className = `.number${i}`;
            const element = currentDisplayElement.querySelector(className);

            if (element) {
                element.classList.remove(`number${i}`);
            }
        }

        const numberElements = currentDisplayElement.querySelectorAll('.number');
        numberElements.forEach((element, index) => {
            element.classList.add(`number${index + 1}`);
        });
    }

    let values = getLatestValuesArray();
    let selectedIndex = valuesArray.length + Number(startingIndex);
    let valuesToDisplay = [values[selectedIndex - 3], values[selectedIndex - 2], values[selectedIndex - 1], values[selectedIndex], values[selectedIndex + 1], values[selectedIndex + 2], values[selectedIndex + 3]];

    const updateDisplay = () => {
        mostRecentArrayOfValues = getLatestValuesArray()
        mostRecentArrayOfValues.forEach((value, index) => {
            if (valuesToDisplay.includes(value)) {
                value.style.display = "block";
                value.classList.add("number");
                value.addEventListener('click', handleClick);
                if (index === selectedIndex) {
                    value.classList.add("selectedElementForNumberSpinner");
                } else {
                    value.classList.remove("selectedElementForNumberSpinner");
                }
            } else {
                value.style.display = "none";
                value.classList.remove("number");
                value.addEventListener('click', handleClick);
            }
        });
        updateWheelStyles();
    };

    let isScrolling;
    function handleScroll(event) {
        const elementsToRemoveHoverEffects = document.querySelectorAll(".number");
        elementsToRemoveHoverEffects.forEach(element => {
            element.classList.add('remove-hover-effects');
        })

        clearTimeout(isScrolling);

        isScrolling = setTimeout(function () {
            //scrolling has stopped;
            const elementsToAddHoverEffectsTo = document.querySelectorAll('.remove-hover-effects');
            elementsToAddHoverEffectsTo.forEach(element => {
                element.classList.remove('remove-hover-effects');
            })
        }, 100);

        const delta = Math.sign(event.deltaY);
        selectedIndex += delta;

        if (selectedIndex <= getLatestValuesArray().length / 3) {

            for (let i = valuesArray.length - 1; i >= 0; i--) {

                const div = document.createElement('div');
                div.innerHTML = valuesArray[i];
                div.classList.add('valueForNumberSpinner');
                if (valuesArraySecond) { div.classList.add("borderRadiusOverwriteLeft"); }
                div.setAttribute("data-value", valuesArray[i]);
                const displayElement = document.getElementById(`display-${uniqueId}`);
                displayElement.prepend(div);
            }
            selectedIndex += valuesArray.length
            updateDisplay();
        }

        if (selectedIndex >= getLatestValuesArray().length - (valuesArray.length + 1)) {

            for (let i = 0; i < valuesArray.length; i++) {

                const div = document.createElement('div');
                div.innerHTML = valuesArray[i];
                div.classList.add('valueForNumberSpinner');
                if (valuesArraySecond) { div.classList.add("borderRadiusOverwriteLeft"); }
                div.setAttribute("data-value", valuesArray[i]);
                const displayElement = document.getElementById(`display-${uniqueId}`);
                displayElement.appendChild(div);
            }
            selectedIndex -= valuesArray.length;
            updateDisplay();
        }

        // Calculate the indices for the values to display
        const indices = [
            selectedIndex - 3,
            selectedIndex - 2,
            selectedIndex - 1,
            selectedIndex,
            selectedIndex + 1,
            selectedIndex + 2,
            selectedIndex + 3
        ];

        // Update the values to display based on the new selected index and calculated indices
        valuesToDisplay = indices.map(index => getLatestValuesArray()[index]);
        updateDisplay();
    }

    function handleClick(event) {
        const clickedIndex = Array.from(getLatestValuesArray()).indexOf(event.currentTarget);
        selectedIndex = clickedIndex;

        const indices = [
            selectedIndex - 3,
            selectedIndex - 2,
            selectedIndex - 1,
            selectedIndex,
            selectedIndex + 1,
            selectedIndex + 2,
            selectedIndex + 3
        ];

        valuesToDisplay = indices.map(index => getLatestValuesArray()[index]);
        updateDisplay();
    }

    function handleMouseMove(deltaY) {
        const elementsToRemoveHoverEffects = document.querySelectorAll(".number");
        elementsToRemoveHoverEffects.forEach(element => {
            element.classList.add('remove-hover-effects');
        })

        clearTimeout(isDragging);

        isDragging = setTimeout(function () {
            //scrolling has stopped;
            const elementsToAddHoverEffectsTo = document.querySelectorAll('.remove-hover-effects');
            elementsToAddHoverEffectsTo.forEach(element => {
                element.classList.remove('remove-hover-effects');
            })
        }, 100);

        const delta = Math.sign(deltaY);
        selectedIndex += delta;

        if (selectedIndex <= getLatestValuesArray().length / 3) {

            for (let i = valuesArray.length - 1; i >= 0; i--) {

                const div = document.createElement('div');
                div.innerHTML = valuesArray[i];
                div.classList.add('valueForNumberSpinner');
                if (valuesArraySecond) { div.classList.add("borderRadiusOverwriteLeft"); }
                div.setAttribute("data-value", valuesArray[i]);
                const displayElement = document.getElementById(`display-${uniqueId}`);
                displayElement.prepend(div);
            }
            selectedIndex += valuesArray.length
            updateDisplay();
        }

        if (selectedIndex >= getLatestValuesArray().length - (valuesArray.length + 1)) {

            for (let i = 0; i < valuesArray.length; i++) {

                const div = document.createElement('div');
                div.innerHTML = valuesArray[i];
                div.classList.add('valueForNumberSpinner');
                if (valuesArraySecond) { div.classList.add("borderRadiusOverwriteLeft"); }
                div.setAttribute("data-value", valuesArray[i]);
                const displayElement = document.getElementById(`display-${uniqueId}`);
                displayElement.appendChild(div);
            }
            selectedIndex -= valuesArray.length;
            updateDisplay();
        }

        // Calculate the indices for the values to display
        const indices = [
            selectedIndex - 3,
            selectedIndex - 2,
            selectedIndex - 1,
            selectedIndex,
            selectedIndex + 1,
            selectedIndex + 2,
            selectedIndex + 3
        ];

        // Update the values to display based on the new selected index and calculated indices
        valuesToDisplay = indices.map(index => getLatestValuesArray()[index]);
        updateDisplay();
    }

    const container = document.querySelector(`.number-picker-container-${uniqueId}`);
    container.addEventListener('wheel', handleScroll);

    let isDragging = false;
    let startY = 0;
    let startScrollTop = 0;

    container.addEventListener('mousedown', (e) => {
        isDragging = true;
        startY = e.clientY;
    })

    let prevY = null;
    container.addEventListener('mousemove', (e) => {
        if (!isDragging) return;

        const currentY = e.clientY;
        const dy = currentY - startY;
        let moveDirectionNum = null;

        if (prevY !== null) {
            const deltaY = currentY - prevY;

            if (deltaY > 0) {
                moveDirectionNum = 1;

            } else if (deltaY < 0) {
                moveDirectionNum = -1;
            }
        }

        prevY = currentY;

        if ((startScrollTop - dy) % 5 === 0) { //slows down the move speed. Increase the number to the right of the modulo to slow it more. 
            handleMouseMove(moveDirectionNum)
        }
    });

    container.addEventListener('mouseup', (e) => {
        isDragging = false;
        startY = undefined;
    })

    //MOBILE
    container.addEventListener('touchstart', (e) => {
        e.stopPropagation();
        isDragging = true;
        startY = e.touches[0].clientY;
    })

    let prevYMobile = null;
    container.addEventListener('touchmove', (e) => {
        if (!isDragging) return;

        const currentY = e.touches[0].clientY;
        e.stopPropagation();
        const dy = currentY - startY;
        let moveDirectionNum = null;

        if (prevYMobile !== null) {
            const deltaY = currentY - prevYMobile;

            if (deltaY > 0) {
                moveDirectionNum = 1;

            } else if (deltaY < 0) {
                moveDirectionNum = -1;
            }
        }

        prevYMobile = currentY;

        if ((startScrollTop - dy) % 10 === 0) { //slows down the move speed. Increase the num just to the right of the % to slow it more. 
            handleMouseMove(moveDirectionNum)
        }
    });

    container.addEventListener('touchend', (e) => {
        isDragging = false;
        startY = undefined;
    })

    updateDisplay();

    //----------------------------------------------------Functionality for the  2nd number spinner within the modal. -------------------------------------------------------------------
    const currentDisplayElement2 = document.querySelector(`#display-${uniqueId}-2`);

    if (valuesArraySecond) {
        const getLatestValuesArray2 = () => {
            return currentDisplayElement2.querySelectorAll('.valueForNumberSpinner2');
        }

        const updateWheelStyles = () => {
            for (let i = 1; i <= 7; i++) {
                const className = `.number2-${i}`;
                const element = currentDisplayElement2.querySelector(className);

                if (element) {
                    element.classList.remove(`number2-${i}`);
                }
            }

            const numberElements2 = currentDisplayElement2.querySelectorAll('.number--2');
            numberElements2.forEach((element, index) => {
                element.classList.add(`number2-${index + 1}`);
            });
        }

        let values2 = getLatestValuesArray2();
        let selectedIndex2 = valuesArraySecond.length + Number(startingIndex2);
        let valuesToDisplay2 = [values2[selectedIndex2 - 3], values2[selectedIndex2 - 2], values2[selectedIndex2 - 1], values2[selectedIndex2], values2[selectedIndex2 + 1], values2[selectedIndex2 + 2], values2[selectedIndex2 + 3]];

        const updateDisplay2 = () => {
            mostRecentArrayOfValues = getLatestValuesArray2()
            mostRecentArrayOfValues.forEach((value, index) => {
                if (valuesToDisplay2.includes(value)) {
                    value.style.display = "block";
                    value.classList.add("number--2");
                    value.addEventListener('click', handleClick2);
                    if (index === selectedIndex2) {
                        value.classList.add("selectedElementForNumberSpinner2");
                    } else {
                        value.classList.remove("selectedElementForNumberSpinner2");
                    }
                } else {
                    value.style.display = "none";
                    value.classList.remove("number--2");
                    value.addEventListener('click', handleClick2);
                }
            });
            updateWheelStyles();
        };

        let isScrolling;
        function handleScroll(event) {
            const elementsToRemoveHoverEffects = document.querySelectorAll(".number--2");
            elementsToRemoveHoverEffects.forEach(element => {
                element.classList.add('remove-hover-effects');
            })

            clearTimeout(isScrolling);

            isScrolling = setTimeout(function () {
                //scrolling has stopped;
                const elementsToAddHoverEffectsTo = document.querySelectorAll('.remove-hover-effects');
                elementsToAddHoverEffectsTo.forEach(element => {
                    element.classList.remove('remove-hover-effects');
                })
            }, 100);

            const delta = Math.sign(event.deltaY);
            selectedIndex2 += delta;

            if (selectedIndex2 <= getLatestValuesArray2().length / 3) {

                for (let i = valuesArraySecond.length - 1; i >= 0; i--) {

                    const div = document.createElement('div');
                    div.innerHTML = valuesArraySecond[i];
                    div.classList.add('valueForNumberSpinner2');
                    div.classList.add('borderRadiusOverwriteRight');
                    div.setAttribute("data-value", valuesArraySecond[i]);
                    const displayElement = document.getElementById(`display-${uniqueId}-2`);
                    displayElement.prepend(div);
                }
                selectedIndex2 += valuesArraySecond.length
                updateDisplay2();
            }

            if (selectedIndex2 >= getLatestValuesArray2().length - (valuesArraySecond.length + 1)) {

                for (let i = 0; i < valuesArraySecond.length; i++) {

                    const div = document.createElement('div');
                    div.innerHTML = valuesArraySecond[i];
                    div.classList.add('valueForNumberSpinner2');
                    div.classList.add('borderRadiusOverwriteRight');
                    div.setAttribute("data-value", valuesArraySecond[i]);
                    const displayElement = document.getElementById(`display-${uniqueId}-2`);
                    displayElement.appendChild(div);
                }
                selectedIndex2 -= valuesArraySecond.length;
                updateDisplay2();
            }

            // Calculate the indices for the values to display
            const indices = [
                selectedIndex2 - 3,
                selectedIndex2 - 2,
                selectedIndex2 - 1,
                selectedIndex2,
                selectedIndex2 + 1,
                selectedIndex2 + 2,
                selectedIndex2 + 3
            ];

            // Update the values to display based on the new selected index and calculated indices
            valuesToDisplay2 = indices.map(index => getLatestValuesArray2()[index]);
            updateDisplay2();
        }

        function handleClick2(event) {
            const clickedIndex = Array.from(getLatestValuesArray2()).indexOf(event.currentTarget);
            selectedIndex2 = clickedIndex;

            const indices = [
                selectedIndex2 - 3,
                selectedIndex2 - 2,
                selectedIndex2 - 1,
                selectedIndex2,
                selectedIndex2 + 1,
                selectedIndex2 + 2,
                selectedIndex2 + 3
            ];

            valuesToDisplay2 = indices.map(index => getLatestValuesArray2()[index]);
            updateDisplay2();
        }

        function handleMouseMove2(deltaY) {
            const elementsToRemoveHoverEffects = document.querySelectorAll(".number--2");
            elementsToRemoveHoverEffects.forEach(element => {
                element.classList.add('remove-hover-effects');
            })

            clearTimeout(isDragging);

            isDragging = setTimeout(function () {
                //scrolling has stopped;
                const elementsToAddHoverEffectsTo = document.querySelectorAll('.remove-hover-effects');
                elementsToAddHoverEffectsTo.forEach(element => {
                    element.classList.remove('remove-hover-effects');
                })
            }, 100);

            const delta = Math.sign(deltaY);
            selectedIndex2 += delta;

            if (selectedIndex2 <= getLatestValuesArray2().length / 3) {

                for (let i = valuesArraySecond.length - 1; i >= 0; i--) {

                    const div = document.createElement('div');
                    div.innerHTML = valuesArraySecond[i];
                    div.classList.add('valueForNumberSpinner2');
                    div.classList.add('borderRadiusOverwriteRight');
                    div.setAttribute("data-value", valuesArraySecond[i]);
                    const displayElement = document.getElementById(`display-${uniqueId}-2`);
                    displayElement.prepend(div);
                }
                selectedIndex2 += valuesArraySecond.length
                updateDisplay2();
            }

            if (selectedIndex2 >= getLatestValuesArray2().length - (valuesArraySecond.length + 1)) {

                for (let i = 0; i < valuesArraySecond.length; i++) {

                    const div = document.createElement('div');
                    div.innerHTML = valuesArraySecond[i];
                    div.classList.add('valueForNumberSpinner2');
                    div.classList.add('borderRadiusOverwriteRight');
                    div.setAttribute("data-value", valuesArraySecond[i]);
                    const displayElement = document.getElementById(`display-${uniqueId}-2`);
                    displayElement.appendChild(div);
                }
                selectedIndex2 -= valuesArraySecond.length;
                updateDisplay2();
            }

            // Calculate the indices for the values to display
            const indices = [
                selectedIndex2 - 3,
                selectedIndex2 - 2,
                selectedIndex2 - 1,
                selectedIndex2,
                selectedIndex2 + 1,
                selectedIndex2 + 2,
                selectedIndex2 + 3
            ];

            // Update the values to display based on the new selected index and calculated indices
            valuesToDisplay2 = indices.map(index => getLatestValuesArray2()[index]);
            updateDisplay2();
        }

        const container2 = document.querySelector(`.number-picker-container-${uniqueId}-2`);
        container2.addEventListener('wheel', handleScroll);

        let isDragging2 = false;
        let startY2 = 0;
        let startScrollTop2 = 0;

        container2.addEventListener('mousedown', (e) => {
            isDragging2 = true;
            startY2 = e.clientY;
        })

        let prevY2 = null;
        container2.addEventListener('mousemove', (e) => {
            if (!isDragging2) return;

            const currentY2 = e.clientY;
            const dy2 = currentY2 - startY2;
            let moveDirectionNum = null;

            if (prevY2 !== null) {
                const deltaY = currentY2 - prevY2;

                if (deltaY > 0) {
                    moveDirectionNum = 1;

                } else if (deltaY < 0) {
                    moveDirectionNum = -1;
                }
            }

            prevY2 = currentY2;

            if ((startScrollTop2 - dy2) % 5 === 0) { //slows down the move speed. Increase the number to the right of the modulo to slow it more. 
                handleMouseMove2(moveDirectionNum)
            }
        });

        container2.addEventListener('mouseup', (e) => {
            isDragging2 = false;
            startY2 = undefined;
        })

        //MOBILE
        container2.addEventListener('touchstart', (e) => {
            e.stopPropagation();
            isDragging2 = true;
            startY2 = e.touches[0].clientY;
        })

        let prevYMobile2 = null;
        container2.addEventListener('touchmove', (e) => {
            if (!isDragging2) return;

            const currentY2 = e.touches[0].clientY;
            e.stopPropagation();
            const dy2 = currentY2 - startY2;
            let moveDirectionNum = null;

            if (prevYMobile2 !== null) {
                const deltaY = currentY2 - prevYMobile2;

                if (deltaY > 0) {
                    moveDirectionNum = 1;

                } else if (deltaY < 0) {
                    moveDirectionNum = -1;
                }
            }

            prevYMobile2 = currentY2;

            if ((startScrollTop - dy2) % 10 === 0) { //slows down the move speed. Increase the num just to the right of the % to slow it more. 
                handleMouseMove2(moveDirectionNum)
            }
        });

        container2.addEventListener('touchend', (e) => {
            isDragging = false;
            startY2 = undefined;
        })

        updateDisplay2();
    }
}

//---------------------------------------------------------------------------------------------End Spinner Modal-------------------------------------------------------------------------------------------------------------------


//---------------------------------------------------------------------------------------------Start Dynamic form Message for Rules---------------------------------------------------------------------------------------------------------------


//Example use:
//<script>
// <% List < UnitConversion > list = MonnitApplicationBase.GetScales(Sensor.Load(Model.SensorID)); %>

//    const ConversionDictionaryArray = [
//    <% foreach(UnitConversion x in list)
//    { %>
//    {
//        "Slope": <%: x.Slope %>,
//        "Intercept": <%: x.Intercept %>,
//        "UnitFrom": "<%: x.FromUnits %>",
//        "UnitTo": "<%: x.ToUnits %>",
//        "UnitLabel": "<%: x.UnitLabel %>"
//    },
//    <% } %>
//    ];

//showBaseUnitsMessage("CompareValue", "scale", ConversionDictionaryArray)

//</script>

const showBaseUnitsMessage = (compareValueId, unitOfMeasureId, ConversionDictionaryArray) => {
    let baseUnit;
    ConversionDictionaryArray.forEach(conversionObj => {
        if (conversionObj.UnitFrom === conversionObj.UnitTo) {
            baseUnit = conversionObj.UnitFrom;
        }
    });

    const compareValue = document.querySelector(`#${compareValueId}`);
    const unitOfMeasure = document.querySelector(`#${unitOfMeasureId}`);

    if (compareValue === null || unitOfMeasure === null) return;

    let baseConversionMessage = [compareValue.value, baseUnit];
    let paragraphHTML = `<p style="font-weight:600; margin-bottom:10px"> <span id="dynamicText">${unitOfMeasure.value}</span> Converted to ${baseUnit}:</p> <p id="previewMessage" data-to-post="">${baseConversionMessage.join(" ")}</p>`;

    if (!document.querySelector("#previewMessage")) {
        unitOfMeasure.insertAdjacentHTML('afterend', paragraphHTML);
    }

    const previewMessage = document.querySelector("#previewMessage");
    const dynamicText = document.querySelector("#dynamicText");

    const inputChange = (selector, indexOfBaseConversionMessageArrayToUpdate) => {

        previewMessage.classList.remove("fade-in-ani-ab");
        previewMessage.classList.add("fade-out-ani-ab");

        let conversionObject = ConversionDictionaryArray.filter(obj => {

            if (obj.UnitLabel === unitOfMeasure.value) {
                return obj
            }
        })

        let conversionMessage = ""
        let numberToPost;

        if (isANumber(selector.value) && selector.value.length > 0) {
            dynamicText.textContent = conversionObject[0].UnitFrom;
            numberToPost = selector.value * conversionObject[0]["Slope"] + conversionObject[0]["Intercept"];
            baseConversionMessage[indexOfBaseConversionMessageArrayToUpdate] = Math.round((selector.value * conversionObject[0]["Slope"] + conversionObject[0]["Intercept"]) * 100) / 100;
            conversionMessage = `${baseConversionMessage.join(" ")}`;
        }

        setTimeout(function () {
            previewMessage.textContent = conversionMessage;
            previewMessage.setAttribute('data-to-post', numberToPost);
            previewMessage.classList.add("fade-in-ani-ab");
            previewMessage.classList.remove("fade-out-ani-ab");
        }, 300);
    }

    compareValue.addEventListener("input", function () {
        inputChange(compareValue, 0);
    });

    unitOfMeasure.addEventListener("input", function () {
        inputChange(compareValue, 0);
    });

    inputChange(compareValue, 0);
}

//---------------------------------------------------------------------------------------------End Dynamic form Message for Rules---------------------------------------------------------------------------------------------------------------


// #endregion


//---------------------------------------------------------------------------------------------Start Tooltip---------------------------------------------------------------------------------------------------------------
//const addStyledToolTips = () => {
//    document.addEventListener('DOMContentLoaded', function () {


//        if ($(".custom-tooltip").length === 0) {
//            var tooltip = document.createElement('div');
//            tooltip.className = 'custom-tooltip';
//            document.body.appendChild(tooltip);
//        }


//        function showTooltip(event) {
//            const title = event.target.getAttribute('title');
//            if (title) {
//                event.target.removeAttribute('title'); // Temporarily remove the title to prevent default browser tooltip
//                tooltip.textContent = title;

//                // Get the position object of the event.target
//                const rect = event.target.getBoundingClientRect();

//                //Determine the page scroll
//                const scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;
//                const scrollTop = window.pageYOffset || document.documentElement.scrollTop;

//                // Calculate the exact position
//                const exactLeft = rect.left + scrollLeft;
//                const exactTop = rect.top + scrollTop;

//                // Get the tooltip dimensions
//                tooltip.style.display = 'block';
//                const tooltipHeight = tooltip.offsetHeight;
//                const tooltipWidth = tooltip.offsetWidth;

//                // Position the tooltip centered horizontally above the element
//                tooltip.classList.add('show');
//                let positionOnX = exactLeft + (rect.width / 2) - (tooltipWidth / 2);
//                let positionOnY = tooltip.style.top = exactTop + tooltipHeight + 5;

//                if (positionOnX < 0) {
//                    positionOnX = Math.abs(positionOnX);
//                }

//                if (positionOnY < 0) {
//                    positionOnY = Math.abs(positionOnY);
//                }

//                tooltip.style.left = positionOnX + 'px';
//                tooltip.style.top = positionOnY + 'px';
//            }
//        }

//        function hideTooltip(event) {
//            const title = tooltip.textContent;
//            if (title) {
//                event.target.setAttribute('title', title); // Restore the title attribute
//                tooltip.classList.remove('show');
//            }
//        }

//        let toolTipsAddedToPage = 0;

//        document.querySelectorAll('[title]').forEach(element => {
//            element.addEventListener('mouseenter', showTooltip);
//            element.addEventListener('mouseleave', hideTooltip);
//            toolTipsAddedToPage++;
//        });

//        console.log("Added tooltips:",toolTipsAddedToPage)
//    });
//}


//addStyledToolTips();
//---------------------------------------------------------------------------------------------End Tooltip---------------------------------------------------------------------------------------------------------------