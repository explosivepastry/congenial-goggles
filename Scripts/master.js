$(document).ready(function () {
    $("ul.sf-menu").supersubs({
        minWidth: 12,
        maxWidth: 30,
        extraWidth: 0
    }).superfish();

    $(".helpIcon").tipTip();

    SetLeftNavHandlers();

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

    var platform = checkPlatform();

    if (platform.isStandalone) {
        //console.log("master.js -- isStandalone -- going to '/Overview'");
        window.location.href = "/Overview";
    }
});
function checkPlatform() {
    var platform = {};
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

    return platform;
}

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
    modalDiv = $('<div title="' + title + '">')
    modalDiv.html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");

    if (height == null) height = 450;
    if (width == null) width = 550;
    modalDiv.dialog({
        height: height,
        width: width,
        modal: true
    });

    if (href.length > 0) {
        keepAlive();
        jQuery.get(href, function (data) {
            modalDiv.html(data);
        });
    }

}

function isANumber(value) {

    return !isNaN(+value);

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

    modalDiv.html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");
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

function resetCount(SensorID)
{
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
    //alert("checkForm");

    if (minHeartBeat == null) {
        minHeartBeat = 60;
    }
    var reportInterval = $('#simpleEdit_' + sensorId + ' input[Name="ReportInterval"]');


    if (reportInterval.val().length > 0 && reportInterval.val() < 1 && reportInterval.val() < minHeartBeat && monnitApplicationID != 53) {
        if (confirm("Heartbeats of less than one minute will severely impact battery life and network performance. \r\nAnd additional charges may be incurred. \r\nPress OK to acknowledge.")) {
            jQuery.get('/Sensor/Acknowledge', { sensorID: sensorId, heartbeat: reportInterval.val() }, function (data) {
                postForm($('#simpleEdit_' + sensorId));
            });
        }
        else {
            //alert("returned 1st if");
            return;
        }
    }
    else if (reportInterval.val().length > 0 && reportInterval.val() < minHeartBeat && monnitApplicationID != 53) {
        if (confirm("Heartbeats of less than one hour may severely impact battery life. \r\nPress OK to acknowledge.")) {
            jQuery.get('/Sensor/Acknowledge', { sensorID: sensorId, heartbeat: reportInterval.val() }, function (data) {
                postForm($('#simpleEdit_' + sensorId));
            });
        }
        else {
            //alert("returned 2nd if");
            return;
        }
    }
    else {

        postForm($('#simpleEdit_' + sensorId));
    }
}

function checkGatewayForm(gatewayId, gatewayTypeID) {

    var reportInterval = $('#gatewayEdit_' + gatewayId + ' input[Name="ReportInterval"]');

    if (gatewayTypeID == 17 || gatewayTypeID == 18 || gatewayTypeID == 22 || gatewayTypeID == 23) {
    
        if ( reportInterval.val()  <= 5) {
            if (confirm("Heartbeats of less then 5 minutes will severely impact data consumption\r\nand additional charges may be incurred. \r\nPress OK to acknowledge.")) {
                jQuery.get('/CSNet/Acknowledge', { gatewayID: gatewayId, heartbeat: reportInterval.val() }, function (data) {
                    postForm($('#gatewayEdit_' + gatewayId));
                });
            }
            else {
                return;
            }
        }
        else if (reportInterval.val()  <= 10) {
            if (confirm("Heartbeats of ten minutes or less severely impact data usage.\r\nThis can cause additional charges to be incurred.\r\nPress OK to acknowledge.")) {
                jQuery.get('/CSNet/Acknowledge', { gatewayID: gatewayId, heartbeat: reportInterval.val() }, function (data) {
                    postForm($('#gatewayEdit_' + gatewayId));
                });
            }
            else {
                return;
            }
        }
    }
    
    postForm($('#gatewayEdit_' + gatewayId));
}

function postForm(bgForm, callback) {
    keepAlive();
    
    var parent = bgForm.parent();
    var postData = bgForm.serialize();

    var xhr = $.post(bgForm.attr("action"), postData, function (data) {
        parent.html(data);

        if (typeof callback == "string") {
            //alert('callback isString');
            eval(callback);
        }
        if (jQuery.isFunction(callback)) {
            //alert('callback isFunction');
            callback(data);
        }
    });

    return xhr;
}

function postForm2(bgForm, callback) {
    keepAlive();

    var parent = bgForm.parent();
    var postData = bgForm.serialize();
    alert("postform 2");

    //var xhr = $.post(bgForm.attr("action"), postData, function (data) {
       
        window.location.href = bgForm.attr("action");
   
    //});

    //return xhr;
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
                window.location.href = '/';
            }
        }
        else {
            alert(data);
        }
    });
}

function viewAccountQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success")
            window.location.href = "/Overview";
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
    });
}
function viewEventsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success")
            window.location.href = "/Events";
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
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
        if (data == "Success")
            window.location.href = "/Overview/SensorIndex";
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
    });
}
function viewSubsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success")
            window.location.href = "/Settings/AdminSubscriptionDetails/" + acctID;
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
    });
}

function viewLocationsQuick(lnk) {
    var anchor = $(lnk);
    var acctID = anchor.data('accountid');
    var href = anchor.attr('href');
    $.post(href, { id: acctID }, function (data) {
        if (data == "Success")
            window.location.href = "/Settings/LocationOverview/" + acctID;
        else
            $('#proxyMessage_' + acctID).html('Proxy Failed');
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
       
        div.html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");
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
    div.html("<img alt='Loading...' src='/content/images/ajax-loader.gif'/>");
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