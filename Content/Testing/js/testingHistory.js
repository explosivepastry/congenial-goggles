
$('.noDataMessagesRow').fadeOut(5000,
    function () {
        $(this).remove();
    }
);


/*
    Example Usage:
        var header = getCSSRule('#header');
        header.style.backgroundColor = 'red';
*/

function getCSSRule(ruleName) {
    ruleName = ruleName.toLowerCase();
    var result = null;
    var find = Array.prototype.find;

    find.call(document.styleSheets, styleSheet => {
        result = find.call(styleSheet.cssRules, cssRule => {
            return cssRule instanceof CSSStyleRule
                && cssRule.selectorText.toLowerCase() == ruleName;
        });
        return result != null;
    });
    return result;
}

/*
    newSize must be a valid css value for font-size:
        'x-small' or '16px'
*/

function changeFontSize(ruleName, newSize) {
    var rule = getCSSRule(ruleName);
    if (rule != null) {
        rule.style.fontSize = newSize
    }
}

var isWorkingGateway = false;

$('#gatewayHistory').scroll(function () {


    if ($(this).scrollTop() + $(this).innerHeight() + 1 >= $(this)[0].scrollHeight) {


        if (isWorkingGateway == false) {
            // TODO: load more records
            getNextDataGateway();
        }
    }
});

function getNextDataGateway() {

    //console.log('loading more records...');

    isWorkingGateway = true;
    $('#loading').show();
    $.get('/Testing/LoadMoreGatewayHistory/',
        {
            id: $('#testingGatewayHistoryTable').data('id'),
            timestamp: $('#testingGatewayHistoryTable .testingHistoryRow').last().attr('data-timestamp')
        },
        function (data) {
            isWorkingGateway = false;
            $('#loading').hide();
            $.each(data, (idx, row) => {
                var r = $(row);
                $('#testingGatewayHistoryTable').append(r);
                flash(r);
                $('#testingHistoryRecordCount').text(parseInt($('#testingHistoryRecordCount').text()) + 1)
            });
        }
    );

}

var isWorkingSensor = false;

$('#sensorHistory').scroll(function () {


    if ($(this).scrollTop() + $(this).innerHeight() + 1 >= $(this)[0].scrollHeight) {


        if (isWorkingSensor == false) {
            // TODO: load more records
            getNextDataSensor();
        }
    }
});

function getNextDataSensor() {

    //console.log('loading more records...');

    isWorkingSensor = true;
    $('#loading').show();
    $.get('/Testing/LoadMoreSensorHistory/',
        {
            id: $('#testingSensorHistoryTable').data('id'),
            timestamp: $('#testingSensorHistoryTable .testingHistoryRow').last().attr('data-timestamp')
        },
        function (data) {
            isWorkingSensor = false;
            $('#loading').hide();
            $.each(data, (idx, row) => {
                var r = $(row);
                $('#testingSensorHistoryTable').append(r);
                flash(r);
                $('#testingHistoryRecordCount').text(parseInt($('#testingHistoryRecordCount').text()) + 1)
            });
        }
    );
}

$('#textMinus,#textPlus').click(
    function (e) {
        e.stopPropagation();
        this.blur();

        var i = 0;
        if (this.id === 'textMinus')
            i = -1;
        if (this.id === 'textPlus')
            i = 1;

        var cur = parseInt($('.testingHistoryRow').css('font-size'));
        var nxt = cur + i;
        console.log(`${cur} => ${nxt}`);
        //$('.testingHistoryRow').css('font-size', nxt);
        //changeFontSize('.testingHistoryRow', nxt + 'px');

        $(this).toggleClass('btn-secondary btn-primary');

        setTimeout(
            () => {
                $(this).toggleClass('btn-secondary btn-primary');
            }
            , 500
        );

        $.post('/Testing/SetTestingToolFontSizePx/'
            , { fontSize: nxt }
            , (data) => {
                console.log(data);
                changeFontSize('.testingHistoryRow', data);
            }
        );
    }
)