<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Schedule  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div id="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>
        <div class="x_panel shadow-sm rounded">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Overview/SensorSchedule|Sensor On & Off Schedule","Sensor On & Off Schedule")%> - <%=Model.SensorID %>
                </div>
            </div>

            <div id="scheduleSlider" class="col-lg-12" style="width: 100%;">
                <table style="width: 100%; vertical-align: middle!important; min-height: 40px; margin-top: auto; margin-bottom: auto; padding-left: 10px; color: #515356;">
                    <colgroup>
                        <col width="14%">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                        <col width="3.44%;">
                    </colgroup>

                    <tr style="background: white; border-bottom: thin solid black">
                        <th colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; text-align: center; background: white;">
                            <span style="background: limegreen">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<%: Html.TranslateTag("Overview/SensorSchedule|On","On")%>
                            <br>
                            <span style="background-color: lightgrey; background-image: repeating-linear-gradient(-45deg, transparent, transparent 10px, rgba(255,255,255,.5) 10px, rgba(255,255,255,.5) 20px);">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            &nbsp;<%: Html.TranslateTag("Overview/SensorSchedule|Off","Off")%></th>
                        <th class="vertical-text" colspan="1">12AM</th>
                        <th class="hide-text" colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">2</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">4</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">6</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">8</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">10</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="vertical-text" colspan="1">12PM</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">2</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">4</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">6</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">8</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="hide-text" colspan="1">10</th>
                        <th colspan="1">&nbsp;</th>
                        <th class="vertical-text" colspan="1" style="padding-right: 10px;">11:59PM</th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Sunday","Sunday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Sun","Sun")%></span></th>
                        <td colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-sunday" type="text" data-slider-enabled="false" />
                        </td>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Monday","Monday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Mon","Mon")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-monday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Tuesday","Tuesday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Tue","Tue")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-tuesday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Wednesday","Wednesday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Wed","Wed")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-wednesday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Thursday","Thursday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Thu","Thu")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-thursday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Friday","Friday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Fri","Fri")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-friday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>

                    <tr style="padding-top: 5px; padding-bottom: 5px; border-bottom: thin solid #7B7D7D;">
                        <th id="align" colspan="1" style="vertical-align: middle!important; margin-top: auto; margin-bottom: auto; line-height: 40px; background: #F0F3F4; text-align: center;"><span class="fullDayWeek"><%: Html.TranslateTag("Saturday","Saturday")%></span> <span class="shortDayWeek"><%: Html.TranslateTag("Sat","Sat")%></span></th>
                        <th colspan="26" style="background: #F0F3F4; line-height: 40px;">
                            <input id="slider-saturday" type="text" data-slider-enabled="false" />
                        </th>
                    </tr>
                </table>
            </div>

            <div class="clearfix"></div>
            <br />
            <form action="/Overview/SaveSchedule/<%:Model.SensorID%>" method="post">
                <%                    
                    int sensorTimeOffset = Model.TimeOffset;
                    Account account = Account.Load(Model.AccountID);
                    int accountTimeOffset = (Monnit.TimeZone.GetCurrentLocalOffsetById(account.TimeZoneID).TotalHours * 4).ToInt();

                    bool sameOffset = (sensorTimeOffset == accountTimeOffset) ? true : false;
                %>
                <div style="color: #515356">&nbsp;&nbsp;&nbsp;<%: Html.TranslateTag("Overview/SensorSchedule|Account Time Zone","Account Time Zone")%>: <b><%: account.TimeZoneDisplayName %></b></div>
                <br>
                <div style="color: #515356">&nbsp;&nbsp;&nbsp;<%: Html.TranslateTag("Overview/SensorSchedule|Sensor Time Offset","Sensor Time Offset")%>: <b><%:Model.TimeOffset / 4%></b></div>
                <br>
                <%if (!sameOffset)
                    { %>
                <div style="color: #FFFFFF; background-color: #D98880; padding-top: 3px; padding-bottom: 3px;">
                    &nbsp;&nbsp;&nbsp;<%: Html.TranslateTag("Overview/SensorSchedule|The sensor time offset does not match the account time zone offset","The sensor time offset does not match the account time zone offset")%>.
                    <input id="updateTimeZone" class="btn btn-grey btn-sm" type="button" value="<%: Html.TranslateTag("Overview/SensorSchedule|Click Here","Click Here")%>" />
                    <%: Html.TranslateTag("Overview/SensorSchedule|to update the sensor time offset","to update the sensor time offset")%>.
                </div>
                <br>
                <%}%>

                <div class="buttons fadeIn" id="cancelsave" style="float: right; display: none;">
                    <span class="fadeIn" style="color: red;"><%:(string.IsNullOrEmpty(ViewBag.Message)? "" : ViewBag.Message)%></span>
                    <a href="" class="btn btn-grey buildReportCancel fadeIn" style="width: 100px"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                    <input type="submit" value="<%: Html.TranslateTag("Save","Save") %>" class="btn btn-primary fadeIn" id="saveReportButton" style="width: 100px" />
                    <div style="clear: both;"></div>
                </div>

                <div id="addSchedule" style="display: flex; justify-content: flex-start;">
                    <div class="btn btn-secondary btn-sm" onclick="displayScheduleFields()"><i class="fa fa-edit" style="color: #FFFFFF"></i>&nbsp; <%: Html.TranslateTag("Overview/SensorSchedule|Edit Schedule","Edit Schedule")%></div>
                </div>

                <br>
                <br>
                <br>
                <br>

                <div id="scheduleFields">
                    <div class="x_title">
                        <br>
                        <h2 class="head" style="color: #515356; text-overflow: unset; overflow: unset; white-space: unset; width: 100%!important;"><b><%: Html.TranslateTag("FIRST","FIRST")%>:</b> <%: Html.TranslateTag("Overview/SensorSchedule|Select which day(s) to set a schedule","Select which day(s) to set a schedule")%>...</h2>
                    </div>

                    <div class="col-12 view-btns" style="border-bottom: thin solid #515356 !important; padding: unset;">
                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Sun","Sun")%>
                            <input hidden name="sunday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Mon","Mon")%>
                            <input hidden name="monday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Tue","Tue")%>
                            <input hidden name="tuesday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Wed","Wed")%>
                            <input hidden name="wednesday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Thu","Thu")%>
                            <input hidden name="thursday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Fri","Fri")%>
                            <input hidden name="friday" value="0" />
                        </div>

                        <div class="btn btn-grey toggle-btn" onclick="toggle(this)" style="border: unset; margin: unset;">
                            <%: Html.TranslateTag("Sat","Sat")%>
                            <input hidden name="saturday" value="0" />
                        </div>
                    </div>
                    <br>
                    <br>
                    <br>
                    <br>

                    <div class="x_title">
                        <h2 class="head" style="color: #515356; text-overflow: unset; overflow: unset; white-space: unset; width: 100%!important;"><b><%: Html.TranslateTag("NEXT","NEXT")%>:</b> <%: Html.TranslateTag("Overview/SensorSchedule|Set an hourly sensor schedule for the selected days","Set an hourly sensor schedule for the selected days")%>...</h2>
                    </div>

                    <div class="btn-group" style="width: 100%; border-bottom: thin solid black!important; overflow-x: unset; margin: 0px;">
                        <button type="button" id="allTab" class="active btn btn-grey hourly-schedule" style="width: 25%; font-size: smaller" name="btnAll" value="All" onclick="setSlider(this)"><%: Html.TranslateTag("Overview/SensorSchedule|All Day","All Day")%></button>
                        <button type="button" id="noneTab" class="btn btn-grey hourly-schedule" style="width: 25%; font-size: smaller" name="btnNone" value="None" onclick="setSlider(this)"><%: Html.TranslateTag("Overview/SensorSchedule|Off Completely","Off Completely")%></button>
                        <button type="button" id="betweenTab" class="btn btn-grey hourly-schedule" style="width: 25%; font-size: smaller" name="btnBetween" value="Between" onclick="setSlider(this)"><%: Html.TranslateTag("Overview/SensorSchedule|Between Chosen Times","Between Chosen Times")%></button>
                        <button type="button" id="beforeAndAfterTab" class="btn btn-grey hourly-schedule" style="width: 25%; font-size: smaller" name="btnEnds" value="Before_and_After" onclick="setSlider(this)"><%: Html.TranslateTag("Overview/SensorSchedule|Before and After","Before and After")%></button>
                    </div>

                    <input hidden name="selectedSchedule" id="selectedSchedule" value="<%: Html.TranslateTag("All","All")%>" />
                    <div id="datePickMobi" class="mbsc-form-group">
                        <div id="defaultValues" style="color: #515356;">
                            <%: Html.TranslateTag("Overview/SensorSchedule|Select Schedule Time","Select Schedule Time")%>
                        </div>
                        <br>

                        <div id="timeValues" style="color: #515356">
                            <span id="time1Values"></span>
                            <input id="time1" readonly class="form-control" style="width: 250px;" placeholder="1:00 AM" value="" />
                            <input hidden name="rawTime1" id="rawTime1" value="60" />
                            <span id="time2Values"></span>
                            <input id="time2" readonly class="form-control" style="width: 250px;" placeholder="11:00 PM" value="" />
                            <input hidden name="rawTime2" id="rawTime2" value="1380" />
                            <%--  <div id="mobiDateScroll"></div>--%>
                            <br />
                        </div>

                        <div id="all">
                            <input type="text" id="slider-all" data-slider-id="slider-all" data-provide="slider" data-slider-min="1439" data-slider-max="1439" data-slider-value="1439">
                        </div>

                        <div id="none">
                            <input type="text" id="slider-none" data-slider-id="slider-none" data-provide="slider" data-slider-min="0" data-slider-max="0" data-slider-value="0">
                        </div>

                        <div id="between">
                            <input type="text" id="slider-between" data-slider-id="slider-between" data-provide="slider" data-slider-min="0" data-slider-max="1439" data-slider-value="[60,1380]" data-slider-step="15">
                        </div>

                        <div id="ends">
                            <input type="text" id="slider-ends" data-slider-id="slider-ends" data-provide="slider" data-slider-min="0" data-slider-max="1439" data-slider-value="[60,1380]" data-slider-step="15">
                        </div>
                    </div>

                    <div id="timeOfDayControl">
                        <input name="TimeOfDayControl" value="1" hidden />
                    </div>
                </div>
                <br>
            </form>
        </div>
    </div>

    <%        
        string prefDate = MonnitSession.CurrentCustomer.Preferences["Date Format"].ToLower();
        string prefTime = MonnitSession.CurrentCustomer.Preferences["Time Format"];

        if (prefTime.Contains("tt"))
            prefTime = prefTime.Replace("tt", "A");
        if (prefTime.Contains("mm"))
            prefTime = prefTime.Replace("mm", "ii");
    %>

    <script src="../../Content/Overview/js/bootstrap-slider/bootstrap-slider.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Content/Overview/js/bootstrap-slider/bootstrap-slider.min.css">

    <script type="text/javascript">

        var scheduleType = "All";
        var defaultslider = "defaultslider";
        var inverted = "inverted";

        $('#all').show();
        $('#none').hide();
        $('#between').hide();
        $('#ends').hide();
        $('#timeValues').hide();
        $('#defaultValues').show();
        $('#scheduleFields').hide();

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        var tFormat = '<%= prefTime %>';
        var dFormat = '<%= prefDate %>';
        //prefDate.replace("yyyy", "yyyy");

        $(document).ready(function () {
            $('#addSchedule').click(function () {
                $('#addSchedule').css("transform", "translate(-5px,0)");
            });

            $('#time1').change(function () {
                $('#time1').val()
                rawTime1
            });

            $('#mobiDateScroll').mobiscroll().datepicker({
                theme: 'ios',
                controls: ['time'],
                touchUi: false,
                select: 'range',
                display: popLocation,
                //steps: {
                //    minute: 15, second: 5, zeroBased: true
                //},
                startInput: '#time1',
                endInput: '#time2',
                onInit: function (event, inst) {
                    console.log("here init");

                },
                onSet: function (event, inst) {
                    console.log("here on set");
                    //var Dates = inst.getVal();
                    //var startDate = Dates[0];
                    //var endDate = Dates[1];
                    //$('#rawTime1').val(startDate);
                    //$('#rawTime2').val(endDate);

                    //console.log("time1: " + $('#rawTime1').val());
                    //console.log("time2: " + $('#rawTime2').val());
                }
            });

            $('#updateTimeZone').click(function () {
                var accountOffset = <%:accountTimeOffset%>;
                var sensorID = <%:Model.SensorID%>;
                $.get('/Overview/SetSensorTimeOffset', { id: sensorID, offset: accountOffset }, function (data) {
                    if (data == "Success")
                        window.location.href = "/Overview/SensorSchedule/" + sensorID;
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            });

        });

        function toggle(div) {
            var d = $(div);
            var value = d.children().val();
            d.toggleClass('btn-primary btn-grey');
            value == 0 ? d.children().val(1) : d.children().val(0);
        };

        function setSlider(div) {
            $(".btn-group > .btn").removeClass("active");
            scheduleType = div.value;
            if (scheduleType == "All") {
                $('#all').show();
                $('#none').hide();
                $('#between').hide();
                $('#ends').hide();
                $('#timeValues').hide();
                $('#defaultValues').html('<%: Html.TranslateTag("The sensor will run at all hours of the selected day(s)","The sensor will run at all hours of the selected day(s)")%>');
                $('#selectedSchedule').val("All");
                $('#allTab').addClass("active");
            }
            else if (scheduleType == "None") {
                $('#all').hide();
                $('#none').show();
                $('#between').hide();
                $('#ends').hide();
                $('#timeValues').hide();
                $('#defaultValues').html('<%: Html.TranslateTag("The sensor will not run the selected day(s)","The sensor will not run the selected day(s)")%>');
                $('#selectedSchedule').val("None");
                $('#noneTab').addClass("active");
            }
            else if (scheduleType == "Between") {
                $('#all').hide();
                $('#none').hide();
                $('#between').show();
                $('#ends').hide();
                $('#timeValues').show();
                $('#defaultValues').html('');
                $('#selectedSchedule').val("Between");
                $('#time1Values').html('<%: Html.TranslateTag("Start Time","Start Time")%>');
                $('#time2Values').html('<%: Html.TranslateTag("End Time","End Time")%>');
                $('#betweenTab').addClass("active");
            }

            else if (scheduleType == "Before_and_After") {
                $('#all').hide();
                $('#none').hide();
                $('#between').hide();
                $('#ends').show();
                $('#timeValues').show();
                $('#defaultValues').html('');
                $('#selectedSchedule').val("Before_and_After");
                $('#time1Values').html('<%: Html.TranslateTag("End Time","End Time")%>');
                $('#time2Values').html('<%: Html.TranslateTag("Start Time","Start Time")%>');
                $('#beforeAndAfterTab').addClass("active");
            }
        };
        $("#slider-all").slider({
            formatter: function (value) {
                return 'All';
            }
        });

        $("#slider-none").slider({
            formatter: function (value) {
                return 'None';
            }
        })
        $("#slider-ends").slider({
            selection: 'after',
            formatter: function (value) {
                return SetSliderValues(value);
            }
        });
        $("#slider-between").slider({
            formatter: function (value) {
                return SetSliderValues(value);
            }
        });

        var sundaySlider = $("#slider-sunday").bootstrapSlider({
            id: "sundaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[0]%>, <%:Model.TimeOfDayActive[1]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            },
        });
        sundaySlider.siblings().addClass(<%: Model.TimeOfDayActive[0] <= Model.TimeOfDayActive[1] ? "defaultslider" : "inverted"%>);

        var mondaySlider = $("#slider-monday").bootstrapSlider({
            id: "mondaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[2]%>, <%:Model.TimeOfDayActive[3]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        mondaySlider.siblings().addClass(<%: Model.TimeOfDayActive[2] <= Model.TimeOfDayActive[3] ? "defaultslider" : "inverted"%>);

        var tuesdaySlider = $("#slider-tuesday").bootstrapSlider({
            id: "tuesdaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[4]%>, <%:Model.TimeOfDayActive[5]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        tuesdaySlider.siblings().addClass(<%: Model.TimeOfDayActive[4] <= Model.TimeOfDayActive[5] ? "defaultslider" : "inverted"%>);

        var wednesdaySlider = $("#slider-wednesday").bootstrapSlider({
            id: "wednesdaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[6]%>, <%:Model.TimeOfDayActive[7]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        wednesdaySlider.siblings().addClass(<%: Model.TimeOfDayActive[6] <= Model.TimeOfDayActive[7] ? "defaultslider" : "inverted"%>);

        var thursdaySlider = $("#slider-thursday").bootstrapSlider({
            id: "thursdaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[8]%>, <%:Model.TimeOfDayActive[9]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        thursdaySlider.siblings().addClass(<%: Model.TimeOfDayActive[8] <= Model.TimeOfDayActive[9] ? "defaultslider" : "inverted"%>);

        var fridaySlider = $("#slider-friday").bootstrapSlider({
            id: "fridaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[10]%>, <%:Model.TimeOfDayActive[11]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        fridaySlider.siblings().addClass(<%: Model.TimeOfDayActive[10] <= Model.TimeOfDayActive[11] ? "defaultslider" : "inverted"%>);

        var saturdaySlider = $("#slider-saturday").bootstrapSlider({
            id: "saturdaySlider",
            min: 0,
            max: 96,
            value: [<%:Model.TimeOfDayActive[12]%>, <%:Model.TimeOfDayActive[13]%>],
            triggerSlideEvent: true,
            formatter: function (value) {
                return SetScheduleValues(value);
            }
        });
        saturdaySlider.siblings().addClass(<%: Model.TimeOfDayActive[12] <= Model.TimeOfDayActive[13] ? "defaultslider" : "inverted"%>);

        function SetSliderValues(value) {

            var time1 = TimeMinuteFormatter(value[0]);
            var time2 = TimeMinuteFormatter(value[1]);

            if (scheduleType == "Before_and_After") {
                SetTimeValues(time1, time2, value);
                return "stop " + time1 + " | start " + time2;
            }

            else if (scheduleType == "Between") {
                SetTimeValues(time1, time2, value);
                return "start " + time1 + " | stop " + time2;
            }
        }
        function SetScheduleValues(value) {

            var time1 = TimeFormatter(value[0] * 60);
            var time2 = TimeFormatter(value[1] * 60);

            if (value[0] == 0 && value[1] == 0) {
                return "Don't Run";
            }
            else if (value[0] == 0 && value[1] == 96) {
                return "All Day";
            }
            else if (value[0] < value[1]) {
                return "start " + time1 + " | stop " + time2;
            }
            else {
                return "stop " + time2 + " | start " + time1;
            }
        }

        function TimeMinuteFormatter(time) {
            var hours = Math.floor(time / 60);
            var minutes = time - (hours * 60);
            if (hours.length == 1) hours = '0' + hours;
            if (minutes > 0 && minutes < 10) minutes = '0' + minutes;
            if (minutes == 0) minutes = '00';
            if (hours >= 12) {
                if (hours == 12) {
                    hours = hours;
                    minutes = minutes + " PM";
                } else {
                    hours = hours - 12;
                    minutes = minutes + " PM";
                }
            } else {
                hours = hours;
                minutes = minutes + " AM";
            }
            if (hours == 0) {
                hours = 12;
                minutes = minutes;
            }
            return (hours + ":" + minutes);
        }

        function TimeFormatter(time) {
            time = (time / 4.0);
            var hours = Math.floor(time / 60);
            var minutes = time - (hours * 60);
            if (hours.length == 1) hours = '0' + hours;
            if (minutes > 0 && minutes < 10) minutes = '0' + minutes;
            if (minutes == 0) minutes = '00';
            if (hours >= 12) {
                if (hours == 12) {
                    hours = hours;
                    minutes = minutes + " PM";
                } else {
                    hours = hours - 12;
                    minutes = minutes + " PM";
                }
            } else {
                hours = hours;
                minutes = minutes + " AM";
            }
            if (hours == 0) {
                hours = 12;
                minutes = minutes;
            }
            return (hours + ":" + minutes);
        }

        function SetTimeValues(time1, time2, value) {
            if (value[0] >= 0 && value[1] >= 0 && scheduleType != "all" && scheduleType != "none") {
                $('#time1').val(time1);
                $('#rawTime1').val(value[0]);
                $('#time2').val(time2);
                $('#rawTime2').val(value[1]);
            }
        }

        function displayScheduleFields() {
            $('#addSchedule').hide();
            $('#scheduleFields').show();
            $('#cancelsave').show();
        }

        function setSessionTimes(t1, t2) {
            t1 = (t1.getHours() * 60) + t1.getMinutes();
            t2 = (t2.getHours() * 60) + t2.getMinutes();
            $('#rawTime1').val(t1);
            $('#rawTime2').val(t2);
            if (scheduleType == "Between") {
                betweenSlider.bootstrapSlider('setValue', [t1, t2]);
            }
            else if (scheduleType == "Before_and_After") {
                endsSlider.bootstrapSlider('setValue', [t1, t2]);
            }
        }

    </script>

    <style type="text/css">
        .vertical-text {
            transform: rotate(-45deg) !important;
        }

        .head {
            margin-top: -15px !important;
        }

        /*big screen   greater than */
        @media (min-width:991px) {
            .fullDayWeek {
                display: initial;
                color: black !important;
            }

            .shortDayWeek {
                display: none;
                color: black !important;
            }
        }

        /*small screen less than */
        @media (max-width:991px) {
            .fullDayWeek {
                display: none;
                color: black !important;
            }

            .shortDayWeek {
                display: initial;
                color: black !important;
            }

            .hide-text {
                display: none;
            }
        }

        .days {
            visibility: hidden;
        }

        #align {
            text-align: left !important;
            padding-left: 10px !important;
        }

        .head {
            /*        #endsSlider .slider-track-low, #endsSlider .slider-track-high, #betweenSlider .slider-selection, #allSlider .slider-track {
            background: limegreen;
        }

        #endsSlider .slider-selection, #betweenSlider .slider-track-low, #betweenSlider .slider-track-high, #noneSlider .slider-track {
            background-color: lightgrey;
            background-image: repeating-linear-gradient(-45deg, transparent, transparent 25px, rgba(255,255,255,.5) 25px, rgba(255,255,255,.5) 50px);
        }

        #allSlider .slider-handle, #noneSlider .slider-handle {
            background: transparent;
        }*/
            margin-top: -35px !important;
        }

        #betweenSlider .slider-handle, #endsSlider .slider-handle {
            background: #515356;
            top: 13px;
        }

        #sundaySlider .slider-handle, #mondaySlider .slider-handle, #tuesdaySlider .slider-handle, #wednesdaySlider .slider-handle, #thursdaySlider .slider-handle, #fridaySlider .slider-handle, #saturdaySlider .slider-handle {
            visibility: hidden;
        }

        .toggle-btn {
            width: 188px;
            border-bottom: thin solid #515356;
            padding: 6px 0px 6px 0px;
        }

        .inverted .slider-track-high, .inverted .slider-track-low, .defaultslider .slider-selection {
            background: limegreen;
            border-radius: 8px;
        }

        .defaultslider .slider-track-high, .defaultslider .slider-track-low, .inverted .slider-selection {
            background-color: lightgrey;
            background-image: repeating-linear-gradient(-45deg, transparent, transparent 25px, rgba(255,255,255,.5) 25px, rgba(255,255,255,.5) 50px);
            border-radius: 8px;
            height: 20px !important;
        }

        .slider.slider-horizontal {
            width: 100%;
            height: 20px !important;
            margin-bottom: 10px;
        }

        .slider-track {
            height: 20px !important;
        }

        .slider.slider-horizontal .slider-selection,
        .slider.slider-horizontal .slider-track-low,
        .slider.slider-horizontal .slider-track-high {
            height: 20px !important;
        }

        .btn, .btn-group {
            border: unset !important;
            border-bottom: thin solid black;
        }

        #addSchedule {
            transition: transform 1s ease;
        }

        .fadeIn {
            -webkit-animation-duration: 1s;
            animation-duration: 1s;
            -webkit-animation-fill-mode: both;
            animation-fill-mode: both;
        }

        @-webkit-keyframes fadeIn {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @keyframes fadeIn {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        .fadeIn {
            -webkit-animation-name: fadeIn;
            animation: fadeIn;
        }

        .slider-selection {
            background: limegreen;
        }

        .slider-handle {
            background: #515356;
        }

        #slider-all .slider-track {
            background: limegreen;
        }

        #slider-ends .slider-track {
            background: limegreen;
        }

        #slider-ends .slider-selection {
            background-color: #f7f7f7;
            background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#f5f5f5), to(#F9F9F9));
            background-repeat: repeat-x;
            -webkit-box-shadow: inset 0 1px 2px rgb(0 0 0 / 10%);
            -webkit-border-radius: 4px;
            border-radius: 0;
        }
    </style>


    <%--<script type="text/javascript">   cfa jQuery for between min & max width

        var windowSize = $(window).width();

        if (windowSize <= 991) {
            // change functionality for smaller screens
            $('.fullDayWeek').hide();
            $('.shortDayWeek').show();
            $('.hide-text').hidden();

        } else if (windowSize => 991) {
            // change functionality for larger screens
            $('.shortDayWeek').hide();
            $('.fullDayWeek').show();
            $('.hide-text').show();
        }

    </script>--%>
</asp:Content>
