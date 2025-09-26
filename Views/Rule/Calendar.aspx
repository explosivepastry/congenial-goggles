<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.NotificationScheduleDisableModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Calendar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        Dictionary<string, object> dic = new Dictionary<string, object>();
        AdvancedNotification Advanced = ViewData["AdvancedNotification"] as AdvancedNotification;
    %>

    <!-- page content -->
    <div class="container-fluid">
        <%:Html.Partial("Header",Model.notification) %>
        <div id="NotificationScheduleDisableEdit_<%:Model.notification.NotificationID %>">

            <script type="text/javascript">

                function Switch(value, day) {
                    $("." + day).hide();
                    if (value == 'Between') {
                        $('.' + value + '.' + day).show();
                    }
                    else if (value == 'Before_and_After') {
                        $('.' + value + '.' + day).show();
                    }
                    else if (value == 'Before') {
                        $('.' + value + '.' + day).show();
                    }
                    else if (value == 'After') {
                        $('.' + value + '.' + day).show();
                    }
                }

            </script>

            <style type="text/css">
                select.short {
                    width: 160px;
                }
            </style>

            <form action="/Rule/Calendar/<%:Model.notification.NotificationID %>?returnPage=<%:Request["returnPage"] %>" method="post">
                <%: Html.ValidationSummary(true)%>
                <%  
                    Dictionary<string, object> ClassShort = new Dictionary<string, object>();
                    ClassShort.Add("class", "short");

                    Dictionary<int, string> MonthName = new Dictionary<int, string>();
                    MonthName.Add(1, "Jan");
                    MonthName.Add(2, "Feb");
                    MonthName.Add(3, "Mar");
                    MonthName.Add(4, "Apr");
                    MonthName.Add(5, "May");
                    MonthName.Add(6, "June");
                    MonthName.Add(7, "July");
                    MonthName.Add(8, "Aug");
                    MonthName.Add(9, "Sept");
                    MonthName.Add(10, "Oct");
                    MonthName.Add(11, "Nov");
                    MonthName.Add(12, "Dec");

                %>

                <%: Html.Hidden("AdvancedNotificationID", Model.notification.AdvancedNotificationID)%>
                <%: Html.Hidden("MondayScheduleID", Model.notification.MondayScheduleID)%>
                <%: Html.Hidden("TuesdayScheduleID", Model.notification.TuesdayScheduleID)%>
                <%: Html.Hidden("WednesdayScheduleID", Model.notification.WednesdayScheduleID)%>
                <%: Html.Hidden("ThursdayScheduleID", Model.notification.ThursdayScheduleID)%>
                <%: Html.Hidden("FridayScheduleID", Model.notification.FridayScheduleID)%>
                <%: Html.Hidden("SaturdayScheduleID", Model.notification.SaturdayScheduleID)%>
                <%: Html.Hidden("SundayScheduleID", Model.notification.SundayScheduleID)%>

                <div class="col-12">
                    <div class="x_panel powertour-hook shadow-sm rounded" id="hook-five">
                        <div class="card_container__top">
                            <div class="card_container__top__title">
                                <span class="fa fa fa-calendar me-2"></span>
                                <%: Html.TranslateTag("Events/History|Month & Day Schedule","Month & Day Schedule")%>
                            </div>
                        </div>
                        <div class="x_content">
                            <div class="formbody">
                                <div style="padding: 10px;">
                                    <input type="hidden" value="<%: Model.notification.NotificationID%>" id="id" name="id" />
                                    <table style="margin-bottom: 20px;">
                                        <% Html.RenderPartial("~/Views/Rule/_TimeOfDay.ascx", Model.notification);  %>
                                    </table>
                                    <table width="100%">
                                        <tr class="dffdc">
                                            <td width="238px" style=""><%: Html.TranslateTag("Events/Calendar|Rule Schedule Months and days","Rule Schedule Months and days")%></td>
                                            <td width="268px" class="schedule-time-toggle">
                                                <div class="form-check form-switch d-flex ps-2">
                                                    <label class="form-check-label"><%: Html.TranslateTag("Schedule","Schedule")%></label>
                                                    <input class="form-check-input mx-2" type="checkbox" name="AlwaysSendAdvancedNotification" id="AlwaysSendAdvancedNotification" <%= ViewData["ShowAdvancedScheduleSchedule"].ToBool() ? "checked" : "" %>>
                                                    <label class="form-check-label"><%: Html.TranslateTag("Always","Always")%></label>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="calendar" width="100%" style="display: none" cellpadding="3px" cellspacing="2">

                                    <div style="float: right;">
                                        <div style="display: inline-block; background-clip: padding-box; -webkit-border-radius: 8px; -moz-border-radius: 8px; border-radius: 8px; border: 1.5px solid #fff; text-align: center; width: 125px; margin: 3px 2px 2px 2px; background: #707070; color: #ffffff;"><%: Html.TranslateTag("Highlighted","Highlighted")%></div>
                                        = <%: Html.TranslateTag("Events/Calendar|Rule Will Not Trigger","Rule Will Not Trigger")%>
                                    </div>
                                    <span class="d-flex ms-3">
                                        <button class="btn btn-primary btn-sm me-2" onclick="selectAll(); return false;"><%: Html.TranslateTag("All","All")%> </button>
                                        <button class="btn btn-secondary btn-sm" onclick="clearAll(); return false;"><%: Html.TranslateTag("Clear","Clear")%></button>
                                    </span>

                                    <table style="width: 100%;">
                                        <tr>
                                            <% for (int m = 1; m <= 12; m++)
                                                {%>
                                            <th data-ishighlighted="false" data-month="<%:m %>" onclick="addHighlightClass(this);"><%:Html.TranslateTag(MonthName[m])%></th>
                                            <%}%>
                                        </tr>
                                        <% for (int i = 1; i < 32; i++)
                                            {%>
                                        <tr>
                                            <% for (int m = 1; m <= 12; m++)
                                                {
                                                    if (i <= DateTime.DaysInMonth(2016, m))
                                                    {%>
                                            <td class="day<%: Model.dic.ContainsKey(m) && Model.dic[m].ContainsKey(i) && Model.dic[m][i]?" highlighted":""  %>" data-ishighlighted="false" data-month="<%:m%>" data-day="<%:i%>">
                                                <input type="hidden" name="<%:MonthName[m] %>" value="<%: i%>" <%: Model.dic.ContainsKey(m) && Model.dic[m].ContainsKey(i) && Model.dic[m][i]?"":"disabled"  %> />
                                                <%: i.ToString()  %>
                                            </td>
                                            <%}
                                                else
                                                {%>
                                            <td></td>
                                            <%}
                                                }%>
                                        </tr>
                                        <%}%>
                                    </table>
                                </div>
                                <div class="form-group">
<%--                                    <div class="bold " id="saveMessage" style="font-weight: bold; font-size: 18px;">
                                    </div>--%>
                                    <div class="bold " id="notiTimeValid" style="font-weight: bold; font-size: 18px; color: #37BC9B !important;">
                                    </div>
                                    <br />
                                    <div class="bold col-12 text-end">
                                        <button type="button" onclick="$(this).hide();saveNotiTime()" value="<%: Html.TranslateTag("Save","Save")%>" id="save" class="btn btn-primary">
                                            <%: Html.TranslateTag("Save","Save")%>
                                        </button>
                                        <button class="btn btn-primary" id="loading" type="button" style="display:none;" disabled>
                                          <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                          <%: Html.TranslateTag("Saving...","Saving...")%>
                                        </button>
                                        <label id="fnlTxt" hidden></label>
                                        <div style="clear: both;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <script type="text/javascript">

        (function ($) {
            $.fn.toggleDisabled = function () {
                return this.each(function () {
                    var $this = $(this);
                    if ($this.attr('disabled')) $this.removeAttr('disabled');
                    else $this.attr('disabled', 'disabled');
                });
            };
        })(jQuery);

        function saveNotiTime() {
            $('#loading').show();
            if ($("#AlwaysSendAdvancedNotification").prop("checked"))
                $('td.highlighted').removeClass("highlighted");
            if ($("#AlwaysSend").prop("checked")) {
                $('.activeDay select').each(function () {
                    $(this).val("All_Day");
                });
            }

            var form = $('form').serialize();
            $.ajax({
                type: 'POST',
                url: '/Rule/Calendar/',
                data: form,
                success: function (returnVal) {
                    if (returnVal == 'Event Schedule Edit Success') {
                        $('#loading').hide();
                        $('#save').show();
                        toastBuilder("Success");
/*                        $('#notiTimeValid').html("Success!");*/
                    }
                    else {
                        $('#loading').hide();
                        $('#save').show();
                        toastBuilder("Failed");
/*                        $('#saveMessage').html("Failed!").css("color", "red");*/
                    }
                }
            });
        }

        function selectAll() {
            var x = document.querySelectorAll(".day");
            $(x).each(function (i, val) {
                $(this).addClass('highlighted');
                $(this).data('ishighlighted', 'true');
                $(this).children('input').removeAttr('disabled');
                $(this).children('input').removeProp('disabled');
            });
        }

        function clearAll() {
            var x = document.querySelectorAll(".day");
            $(x).each(function (i, val) {
                $(this).removeClass('highlighted');
                $(this).data('ishighlighted', 'false');
                $(this).children('input').each(function () {
                    $(this).prop('disabled', 'disabled');
                });
            });
        }

        function addHighlightClass(th) {
            var month = $(th).data('month');
            $('.day[data-month="' + month + '"]').each(function (index) {
                var length = $(this).text().length;
                var isHighlighted = $(this).data('ishighlighted');
                if (length > 0) {
                    if (isHighlighted == false || isHighlighted == 'false') {
                        $(this).addClass('highlighted')
                        $(this).data('ishighlighted', 'true');
                        $(this).children('input').each(function () {
                            $(this).removeAttr('disabled');
                        });
                    }
                    else {
                        $(this).removeClass('highlighted')
                        $(this).data('ishighlighted', 'false');
                        $(this).children('input').each(function () {
                            $(this).attr('disabled', 'disabled');
                        });
                    }
                }
            });
        }

        $(function () {
            //setTimeout('$("#AlwaysSendAdvancedNotification").iButton({labelOn: "Always Send" ,labelOff: "Schedule" });', 500);
            var isAdvChk = '<%: ViewData["ShowAdvancedScheduleSchedule"]%>';
            if (isAdvChk == 'false')
                $("#calendar").show();
            else
                $("#calendar").hide();
            $("#AlwaysSendAdvancedNotification").change(function () {
                if ($("#AlwaysSendAdvancedNotification").prop("checked")) {
                    $("#selectionBtns").hide();
                    $("#calendar").hide();
                    $('#calendar td').children().prop('disabled', 'disabled');
                    $('#calendar td').removeClass('highlighted');
                }
                else {
                    $("#selectionBtns").show();
                    $("#calendar").show();
                }
            });

            var isMouseDown = false;

            $("#calendar td")
                .mousedown(function () {
                    isMouseDown = true;
                    var length = $(this).text().length;
                    if (length > 0) {
                        $(this).toggleClass("highlighted");
                        $(this).children('input').toggleDisabled();//.prop("disabled", function (_, val) { return !val; });
                    }
                    return false; // prevent text selection
                })

                .mouseover(function () {
                    if (isMouseDown) {
                        var length = $(this).text().length;
                        if (length > 0) {
                            $(this).toggleClass("highlighted");
                            $(this).children('input').toggleDisabled();//.prop("disabled", function (_, val) { return !val; });
                        }
                    }
                })

                .bind("selectstart", function () {
                    return false; // prevent text selection in IE
                });

            $(document)
                .mouseup(function () {
                    isMouseDown = false;
                });

            //setTimeout('$("#AlwaysSend").iButton({labelOn: "Always" ,labelOff: "Schedule" });', 500);
            $('#AlwaysSend').change(function () {
                if ($('#AlwaysSend').prop('checked')) {
                    $('.activeDay').hide();
                    $('.activeTime').hide();
                    $('.Between').hide();
                }
                else {
                    $('.activeDay').show();
                    $('#MondaySchedule\\.NotificationDaySchedule').change();
                    $('#TuesdaySchedule\\.NotificationDaySchedule').change();
                    $('#WendnesdaySchedule\\.NotificationDaySchedule').change();
                    $('#ThursdaySchedule\\.NotificationDaySchedule').change();
                    $('#FridaySchedule\\.NotificationDaySchedule').change();
                    $('#SaturdaySchedule\\.NotificationDaySchedule').change();
                    $('#SundaySchedule\\.NotificationDaySchedule').change();
                }
            });

            setTimeout('$("#AlwaysSend").change();', 600);
            $('.DateTime').datepicker();
            $('#ui-datepicker-div').css('display', 'none');

            if ($('#AlwaysSend').prop('checked')) {
                $('.activeDay').hide();
                $('.activeTime').hide();
                $('.Between').hide();
            }
            else {
                $('.activeDay').show();
                $('#MondaySchedule\\.NotificationDaySchedule').change();
                $('#TuesdaySchedule\\.NotificationDaySchedule').change();
                $('#WendnesdaySchedule\\.NotificationDaySchedule').change();
                $('#ThursdaySchedule\\.NotificationDaySchedule').change();
                $('#FridaySchedule\\.NotificationDaySchedule').change();
                $('#SaturdaySchedule\\.NotificationDaySchedule').change();
                $('#SundaySchedule\\.NotificationDaySchedule').change();
            }
        });

    </script>

</asp:Content>

















