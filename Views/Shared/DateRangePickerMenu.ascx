<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%--
<div class="daterangePickerMenu" style="display: none;">
    <div style="float: right;">
        <%: Html.TranslateTag("Quick Pick") %>: 
        <select id="historySelect" style="width: 150px;">
            <option value="null">&nbsp;</option>
            <option value="0">Today</option>
            <option value="-7">Last 7 days</option>
            <option value="-30">Last 30 days</option>
        </select>
        <input type="button" class="bluebutton" value="Go" style="margin: -1px 0px 0px 10px;" />
    </div>
    <div style="clear: both; padding-top: 10px;">
        <div id="historyFromDatePicker" class="datepicker history"></div>
        <div style="float: left; width: 10px; height: 160px;"></div>
        <div id="historyToDatePicker" class="datepicker history"></div>
    </div>
</div>
<script>
    function fromDateChanged(date) {
        $("#historyToDatePicker").datepicker("option", "minDate", date);
        $("#historySelect").prop('selectedIndex', 0);
        var input = $(".historyFromDate:Visible");
        if (input.length > 0) {
            input.val(date);
            setServerDate(input);
        }
    }
    function toDateChanged(date) {
        $("#historyFromDatePicker").datepicker("option", "maxDate", date);
        $("#historySelect").prop('selectedIndex', 0);
        var input = $(".historyToDate:Visible");;
        if (input.length > 0) {
            input.val(date);
            setServerDate(input);
        }
    }
    function setHistoryRanges() {
        $("#historyToDatePicker").datepicker("option", "minDate", $("#historyFromDatePicker").datepicker("getDate"));
        $("#historyFromDatePicker").datepicker("option", "maxDate", $("#historyToDatePicker").datepicker("getDate"));
    }
    function setServerDate(input) {
        if (input != null) {
            $.get('/Overview/Set' + input.attr('name') + '?date=' + input.val(), function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    }
    function setDateRangePicker() {
        $('.historyFromDate, .historyToDate').click(function (e) {
            e.preventDefault();

            var pos = $('.historyToDate:Visible').offset();
            if (!$('.daterangePickerMenu').is(":Visible")) {//allow user to select text if already open
                $("#historyFromDatePicker").datepicker("setDate", $('.historyFromDate').val());
                $("#historyToDatePicker").datepicker("setDate", $('.historyToDate').val());
                setHistoryRanges();
            }

            $('.daterangePickerMenu').css({ top: pos.top + 35, left: pos.left - 273 }).show();

        });
        $('.historyFromDate').change(function () {
            var input = $(this);
            setServerDate(input);
            $("#historySelect").prop('selectedIndex', 0);
            $("#historyFromDatePicker").datepicker("setDate", input.val());
            setHistoryRanges();
        });
        $('.historyToDate').change(function () {
            var input = $(this);
            setServerDate(input);
            $("#historySelect").prop('selectedIndex', 0);
            $("#historyToDatePicker").datepicker("setDate", input.val());
            setHistoryRanges();
        });
    }

    $(function () {
        $('#historyFromDatePicker').datepicker({
            defaultDate: $('.historyFromDate').val(),
            altField: ".historyFromDate",
            altFormat: "mm/dd/yy",
            onSelect: fromDateChanged
            <%if (MonnitSession.CurrentCustomer != null && !MonnitSession.AccountCan("select_old_data"))
    {%>
            , minDate: "-3M"
            <%}%>
        });
        $('#historyToDatePicker').datepicker({
            defaultDate: $('.historyToDate').val(),
            altField: ".historyToDate",
            altFormat: "mm/dd/yy",
            onSelect: toDateChanged
        });

        $('#historySelect').change(function () {
            var days = $(this).val();
            if (days != "null") {
                $("#historyFromDatePicker").datepicker("setDate", days);
                $("#historyToDatePicker").datepicker("setDate", "-0d");
                setHistoryRanges();
                setServerDate($('.historyFromDate:Visible'));
                setServerDate($('.historyToDate:Visible'));
            }
        });

        $(document).mouseup(function (e) {
            var container = $(".daterangePickerMenu");
            if (container.is(":visible")) {
                var dateinputs = $('.historyFromDate:Visible, .historyToDate:Visible');

                if ((!container.is(e.target) // if the target of the click isn't the container...
                    && container.has(e.target).length === 0 // ... nor a descendant of the container
                    && !dateinputs.is(e.target)) // ... nor one of the date inputs
                    || $('.bluebutton').is(e.target))// or they did click the button
                {
                    container.hide();

                    var tabContainter = $('.tabContainer').tabs();
                    //var selected = tabContainter.tabs('option', 'selected');
                    var active = tabContainter.tabs('option', 'active');
                    if ($.isNumeric(active)) {

                        tabContainter.tabs('load', active);
                    }
                    else {
                        //multichart 
                        window.location.href = window.location.href;

                    }


                    //if (modalDiv != null && modalDiv.find('.datepicker.history').length > 0) { //Multi Chart is open
                    //    var ids = "";
                    //    $('#Main :checked').each(function () {
                    //        ids += $(this).val() + "|";
                    //    });
                    //    if (ids.length > 0) {
                    //        var href = '/Report/ChartMultiple?ids=' + ids;
                    //        jQuery.get(href, function (data) {
                    //            modalDiv.html(data);
                    //        });
                    //    }
                    //}
                }
            }
        });
    });
</script>
--%>