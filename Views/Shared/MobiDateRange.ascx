<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<!-- MobiDateRange START -->
<%--<div id="MobiDateRange">--%>
<%--
******************************************************************************************************************************************************
    Controller: Mobiscroll Datepicker Range

    Usage: Choose a period between two DateTime values and use to render a Partial View retrieved from a Controller

    Variables must be placed on the page (aspx) where you are loading this partial (ascx)

    REQUIRED variables:
        mobiDataDestElem                String
        mobiDataPayload                 Struct
        mobiDataController              String

    OPTIONAL variables, default values shown below
        mobiDataInitialLoad             Boolean
        mobiDataFromDate                JS Date
        mobiDataToDate                  JS Date
        mobiMinRangeDays                Number
        mobiMaxRangeDays                Number
        mobiMinDate                     Date
        mobiMaxDate                     Date

    Note 1: Declare variables in your page to initialize and customize MobiDateRange.
            Example configuration:
                        let mobiDataDestElem = '#ChartDiv';
                        let mobiDataPayload = { id: '<%=Sensor.SensorID%>', isBatteryChart: false };
                        let mobiDataController = '/Chart/MultiChartChart';
                        let mobiMaxRangeDays = 7;
                        let mobiDataInitialLoad = false;
                        let mobiDataFromDate = new Date("<%= DateTime.Parse("2023-03-17")%>");
                        let mobiDataToDate = new Date("<%=DateTime.Parse("2023-04-12")%>");
                        let mobiMinRangeDays = 3;
                        let mobiMaxRangeDays = 10;
                        let mobiMinDate = '2023-01-01'
                        let mobiMaxDate = new Date((new Date()).setDate((new Date()).getDate() - 1)); // yesterday

    Note 2: This controller will update the `HistoryFromDate` and `HistoryToDate` in the `MonnitSession` and will the call `mobiDataRefresh()` 
            to populate the partial.

    Note 3: 'MonnitSession' 'HistoryFromDate' and 'HistoryToDate' are localized to the user. For display you only need to format them 
    (e.g. DateTime.OVToDateTimeShort()). To use them as data inputs for a Controller, they must be converted to UTC. This will ideally be done 
    in the Controller (as we generally don't pass them as arguments, rather the Controller accesses them directly). Similarly, data received from 
    a Controller will have dates in UTC and must be adjusted for the user's (Account's) timezone 
    as well as formatted according to the user's preferences (e.g. DateTime.OVToLocalDateTimeShort()).

    Note 4: Loading this user control partial will call `mobiDataRefresh()` as the last line of its $(document).ready(function () {}) for 
    initial population of the data partial. You can override the behavior here by declaring the function in the parent page like so:
        function mobiDataRefresh() {}
    or

        function mobiDataRefresh() {
        var paramForController = <%=Model.SomethingID%>;
        $.post("/Rule/RuleHistoryRefresh", { id: paramForController }, function (data) {
            $('#divContainingPartial').html("waiting...")
            setTimeout($('#divContainingPartial').html(data), 1000);
        })
            .fail(function (response) {
                alert('Error response from mobiDataRefresh()');
            });
        }
    
******************************************************************************************************************************************************

    Revision 1: 'mobiDataRefresh()' is included here. There are 3 variables that must be defined in the page including this controller:
        mobiDataDestElem        Element (most likely a <div>) to be updated with data retrieved by changing date range
        mobiDataPayload         Parameters required by back-end for the Model/Controller being used 
        mobiDataController      URL for GET (or POST?) call to Controller & method to retrieve data (typical pattern: `Controller/Method`)
        
        Example:
        let mobiDataDestElem = '#sensorHistory';
        let mobiDataPayload = { sensorID: '<%=Model.SensorID%>', dataMsg: '' };
        let mobiDataController = '/Overview/SensorHistoryData';
        
    Revision 2:
        Edits made to facilitate use of Google Maps API    

        // Makes call to `mobiDataRefresh()` optional on init
        let mobiDataInitialLoad = true;
    
        // If instantiated from the View calling MobiDateRange.ascx, will override the JS values 'fromDt' and 'toDt' used below.
        let mobiDataFromDate = new Date("<%=MonnitSession.HistoryFromDate%>");
        let mobiDataToDate = new Date("<%=MonnitSession.HistoryToDate%>");

    Revision 3:
        Set a minimum and/or maximum range of days using `mobiMinRangeDays` and `mobiMaxRangeDays`
        Set a minimum and/or maximum date using `mobiMinDate` and `mobiMaxDate`

--%>

<style>
    .mobiDate_container__start, .mobiDate_container__end {
        width: 165px;
        color: #000000;
        margin-left: 0px;
        margin-right: 0px;
        font-size: 15px;
        padding:3px;
        border-radius:5px;
    }

    .mobiDate_container__text {
        color: #707070;
        font-size: 15px;
        font-weight: bold;
    }

    .mobiDate-icon {
        cursor: pointer;
    }
    #datePickMobi, #Mobi_startDate, #Mobi_endDate {
        cursor:pointer;
    }
</style>
<svg id="datePickMobi" class="svg_icon" viewBox="0 0 18.047 18.047">
    <path d="M0,0H18.047V18.047H0Z" fill="none" />
    <path id="Path_50" data-name="Path 50" d="M15.032,2.5H14.28V1h-1.5V2.5H6.76V1h-1.5V2.5H4.5A1.508,1.508,0,0,0,3,4.008V14.536a1.508,1.508,0,0,0,1.5,1.5H15.032a1.508,1.508,0,0,0,1.5-1.5V4.008A1.508,1.508,0,0,0,15.032,2.5Zm0,12.032H4.5V7.016H15.032ZM4.5,5.512v-1.5H15.032v1.5Zm1.5,3.008h7.52v1.5H6.008Zm0,3.008h5.264v1.5H6.008Z" transform="translate(-0.744 -0.248)" />
</svg>
<input id="Mobi_startDate" class="mobiDate_container__start media_desktop" placeholder="<%:Html.TranslateTag("Start", "Start")%>" readonly>
<span class="mobiDate_container__text media_desktop">- </span>
<input id="Mobi_endDate" class="mobiDate_container__end media_desktop" placeholder="<%:Html.TranslateTag("End", "End")%>">
<%--<%=Html.GetThemedSVG("calendar") %>--%>



<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("MobiDateRange.ascx") %>

    let popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    let mobiDateFmt = '<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>'.toUpperCase();        // existing occurences in code had toLower() in inline C# then once it was JS, used .toUppserCase() before finally using
    let mobiTimeFmt = '<%=MonnitSession.CurrentCustomer.Preferences["Time Format"]%>'.replace('tt', 'A');   // existing occurences in code also had: `replace("mm", "ii")`
    let fromDt = new Date("<%=MonnitSession.HistoryFromDate%>");
    let toDt = new Date("<%=MonnitSession.HistoryToDate%>");
    let str = $('#Mobi_startDate');
    let end = $('#Mobi_endDate');

    //let datePickR;
    let datePickDiv;
    let datePickMobi;           // = $('#datePickMobi').mobiscroll().datepicker({...});
    let datePickInst;       // = $('#datePickMobi').mobiscroll('getInst');
    //                                                                                      !!! datePickMobi != datePickMobiInst    !!!                     //


    $(document).ready(function () {

        if (typeof mobiDataFromDate !== 'undefined' && mobiDataFromDate != null) {
            fromDt = new Date(mobiDataFromDate)
        }
        if (typeof mobiDataToDate !== 'undefined' && mobiDataToDate != null) {
            toDt = new Date(mobiDataToDate);
        }
        datePickDiv = document.getElementById('datePickMobi');
        datePickMobi =
            $(datePickDiv).mobiscroll().datepicker({
                theme: 'ios',
                controls: ['calendar', 'time'],
                select: 'range',
                rangeHighlight: true,
                showRangeLabels: true,
                /*minRange: 3,*/
                /*maxRange: 8.64e+7,*/
                display: popLocation,
                dateFormat: mobiDateFmt,
                timeFormat: mobiTimeFmt,
                //defaultSelection: [fromDt, toDt], // set tempVal and Val, instead just set tempVal in onInit
                startInput: '#Mobi_startDate',
                endInput: '#Mobi_endDate',
                //value: [fromtDt, toDt], // permanenetly sets values
                //inRangeInvalid = true, // causes problem
                onInit: function (event, inst) {
                    inst.setTempVal([fromDt, toDt]);
                    str.attr({ 'placeholder': inst._tempStartText });
                    end.attr({ 'placeholder': inst._tempEndText });
                    //console.log('\n onInit');
                },
                onOpen: function (event, inst) {
                    //console.log('\n onOpen');

                    if (typeof mobiMinRangeDays !== 'undefined' && isANumber(mobiMinRangeDays)) {
                        //console.log('mobiMinRangeDays = ' + mobiMinRangeDays);
                        inst.setOptions({ minRange: mobiMinRangeDays * 8.64e+7 });
                    }
                    if (typeof mobiMaxRangeDays !== 'undefined' && isANumber(mobiMaxRangeDays)) {
                        //console.log('mobiMaxRangeDays = ' + mobiMaxRangeDays);
                        inst.setOptions({ maxRange: mobiMaxRangeDays * 8.64e+7 });
                    }
                    if (typeof mobiMinDate !== 'undefined') {
                        //console.log('mobiMinDate = ' + mobiMinDate);
                        let mindt = Date.parse(mobiMinDate);
                        if (!isNaN(mindt)) {
                            inst.setOptions({ min: mindt });
                        }
                    }
                    if (typeof mobiMaxDate !== 'undefined') {
                        //console.log('mobiMaxDate = ' + mobiMaxDate);
                        let maxdt = Date.parse(mobiMaxDate);
                        if (!isNaN(maxdt)) {
                            inst.setOptions({ max: maxdt });
                        }
                    }
                },
                onChange: function (event, inst) {
                    var Dates = inst.getVal();
                    var startDate = Dates[0];
                    var endDate = Dates[1];
                    setSessionDates(startDate, endDate);
                },
                onCellClick: function (event, inst) {
                    //console.log("onCellClick");
                }
            }) //.mobiscroll('getInst');

        if ((typeof mobiDataInitialLoad === 'undefined' || mobiDataInitialLoad == null)
            || typeof mobiDataInitialLoad !== 'undefined' && mobiDataInitialLoad != null && mobiDataInitialLoad) {
            mobiDataRefresh();
        }
        //datePickMobi = $('#datePickMobi');
        //datePickDiv = datePickMobi[0];
        //datePickMobi = $('#datePickMobi').mobiscroll.datepicker()  //!!! DON'T!!! overwrites the previous datepicker attached to $('#datePickMobi')
        datePickInst = datePickMobi.mobiscroll('getInst');
        // mobiscroll.Dateickper('#datePickMobi') === mobiscroll.Datepicker(), so I dunno what that does
        // ALSO!!! $('#datePickMobi') != $('#datePickMobi') !!! Each is a separate JQuery object, although they point to the same DOM element
    });



    function setSessionDates(fromdate, todate) {
		if (todate != null && fromdate != null) {
			// round to nearest minute as setting seconds not offered in ui
			let toDate = new Date(Math.round(new Date(todate).getTime() / (1000 * 60)) * (1000 * 60));
			let fromDate = new Date(Math.round(new Date(fromdate).getTime() / (1000 * 60)) * (1000 * 60));
            var url = '/Overview/SetMobiDates?toDate=' + toDate + '&fromDate=' + fromDate;
            $.get(url, function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else {
                    if (typeof mobiDataRefresh === "function") {
                        mobiDataRefresh();
                    } else {
                        showSimpleMessageModal("<%=Html.TranslateTag("mobiDataRefresh() not defined")%>");
                        window.location.reload();
                    }
                }

            });
        }
    }

    // update dates in calendar view (useful if controller changes session dates)
    function getSessionDates() {
        //console.log("getSessionDates")
        return $.get('/Overview/GetMobiDates', function (data) {
            //let fromDtNewInt = parseInt(data.HistoryFromDate.replaceAll('/Date(', '').replace(')/'));
            //let toDtNewInt = parseInt(data.HistoryToDate.replaceAll('/Date(', '').replace(')/'));
            //if (isANumber(fromDtNewInt) && isANumber(toDtNewInt)) {
            //    let fromDtNew = new Date(fromDtNewInt);
            //    let toDtNew = new Date(toDtNewInt);
            //    let userTimeOffsetMinutes = new Date().getTimezoneOffset();
            //    let offset = Number(data.Offset);

            //    fromDtNew = fromDtNew.setMinutes(fromDtNew.getMinutes() + offset + userTimeOffsetMinutes);
            //    toDtNew = toDtNew.setMinutes(toDtNew.getMinutes() + offset + userTimeOffsetMinutes);

            //    datePickInst.setVal([fromDtNew, toDtNew]);
            //    //str.attr({ 'placeholder': fromDtNew });
            //    //end.attr({ 'placeholder': toDtNew });
            //}

            let fromDtNew = new Date(data.HistoryFromDate);
            let toDtNew = new Date(data.HistoryToDate);
            datePickInst.setVal([fromDtNew, toDtNew]);
        })
    }
    const mobiLoadingDiv = `
            <div class="text-center" id="mobiLoading"  >
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>
        `;

    function mobiDataRefresh() {
        var mobiDataRefreshAlertMsg = '';
        if (typeof mobiDataDestElem === 'undefined') { mobiDataRefreshAlertMsg += 'mobiDataDestElem not defined!\n'; }
        if (typeof mobiDataPayload === 'undefined') { mobiDataRefreshAlertMsg += 'mobiDataPayload not defined!\n'; }
        if (typeof mobiDataController === 'undefined') { mobiDataRefreshAlertMsg += 'mobiDataController not defined!\n'; }
        if (mobiDataRefreshAlertMsg.length > 0) { alert(mobiDataRefreshAlertMsg); }
        $(mobiDataDestElem).html(mobiLoadingDiv); // todo:jfk replace with spinner
        $.post(mobiDataController, mobiDataPayload, function (data) {
            //$(mobiDataDestElem).html("...Waiting"); // todo:jfk replace with spinner
            setTimeout(function () { $(mobiDataDestElem).html(data); }, 1000);
        }).fail(function (xhr, status, error) {
            let statusCode = xhr.status;
            let statusText = xhr.statusText;
            let errMsgTitle = xhr.responseText.match(/<title>.*<\/title>/g)[0];
            let errMsgTitleText = $(errMsgTitle)[0].innerHTML;

            $('#mobiLoading').hide();
            errMsg = `
<div>
Error response from mobiDataRefresh()
<br/>
${xhr.statusText} (${xhr.status})
<br>
${errMsgTitleText}
</div>
`;
            $(mobiDataDestElem).html(errMsg);
        });
    }

</script>

<!-- MobiDateRange END -->
