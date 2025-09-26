<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Models.DatabaseStatistics>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatabaseStatistics
</asp:Content>
<%-- jfk:delme--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="text-end mt-4">
            <button type="button" class="btn btn-primary dropdown-toggle" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                <%: Html.TranslateTag("Report/DatabaseStatistics|Quick Pick","Quick Pick")%>
            </button>
            <div class="dropdown-menu p-3" id="quickPick" role="menu">
                <a onclick="quickPick(60);" class="btn btn-primary minuteOption" id="PastHour"><%: Html.TranslateTag("Report/DatabaseStatistics|Past Hour","Past Hour")%></a>
                <a onclick="quickPick(25);" class="btn btn-primary minuteOption" id="Past25min"><%: Html.TranslateTag("Report/DatabaseStatistics|Past 25 Min","Past 25 Min")%></a>
            </div>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top">
                    <div class="card_container__top__title dfac">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                            <path id="ic_equalizer_24px" d="M10,20h4V4H10ZM4,20H8V12H4ZM16,9V20h4V9Z" transform="translate(-4 -4)" class="main-page-icon-fill" />
                        </svg>

                        &nbsp;
                    <%: Html.TranslateTag("Events/Actions|DatabaseStatistics","DatabaseStatistics")%>
                    </div>
                    <div class="nav navbar-right panel_toolbox d-flex flex-nowrap">
                        <%--                    <%: Html.TranslateTag("Report/DatabaseStatistics|StartDate","StartDate")%>: --%>
                        <input style="font-size: 14px; cursor: pointer;" class="mobiDate_container__start" id="Mobi_startDate" placeholder="<%=MonnitSession.HistoryFromDate %>" />
                        -
                        <%--                    <%: Html.TranslateTag("Report/DatabaseStatistics|EndDate","EndDate")%>: --%>
                        <input style="font-size: 14px; cursor: pointer;" class="mobiDate_container__end" id="Mobi_endDate" placeholder="<%=MonnitSession.HistoryToDate %>" />
                        <a id="datePickMobi" style="cursor: pointer;">
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="18" height="18" viewBox="0 0 22 22">
                                <image id="NoPath_-_Copy_47_" data-name="NoPath - Copy (47)" width="22" height="22" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAAAAADH8yjkAAACJElEQVRo3mP4T2PAMGoByRYwoAGCBhBQP2oBeRZgY+OzAJ/6UQtGLRgKFoyCwQFoVtmMWjBqwRCyYBSMlkWjFoxaMGrBqAUELPi3M89ThUMtoHLTP0zJO4L4HUeEBYds4YosjqBLftZhoNSCdpRCNwXVE/+CGSi1YBlIkrd85ekNzfIgZieKbAsDpRacYQPKeTwHs7/mAtlMuxCSvwoYKLYgFiil8hHK+esC5DnB5Z5YMVBswWcuoNRuOPcUkMf9C8L+0szLQLkFC4Eywr/h3B+sQP5pMHOaOEiX4jIKLXi2uTOuDsH9CoqRzQiN8R/vU5yKUMB2kMobMI0yK///p64F/2KACjX+QDSqzv35n8oW/CsEKVwG4ayE2ENFC76vAiVShgrUrEwtC5Yaq4MTJV8/WnlHLQuyIIo41qGXp9SywB2mTHUObSxorupb2GcMVpj9mxYWQMBVB5DKUtpZ8P+9NlAl2wPaWfD/IagwyqehBf/NgUodaWkBKDNLUdOCn3f2vEfm9wGVClHPgn9GjAwMM9FrOG8q+iABKOOKLKABFGilogVrgDLM55HKJJDS/VS04CM3UErnB4x7jw/I1f9OzUheBZJzgWStf0vEgBzBu9TNyaUgSZ7sWYdW1YBbKYw7qFzY/U5GaTqC6mFql0WnrOGKeFq/0qI++Le9MUKfS8m7dP4LDDmqFxWjXahRC0YtGLWAphaMguE5bg2P6FELCAEAiX2+a4qCoeAAAAAASUVORK5CYII="></image>
                            </svg>
                        </a>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="card_container__body" style="padding: 10px;">
                    <style>
                        div.mobile {
                        }

                        @media screen and (min-width: 601px) {
                            div.mobile {
                                font-size: 15px;
                            }
                        }

                        @media screen and (max-width: 600px) {
                            div.mobile {
                                font-size: 10px;
                            }
                        }
                    </style>

                    <div class="text-center" id="loading" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>

                    <div id="dbStatList"></div>

                </div>

            </div>
        </div>
    </div>
    <%

        string prefDate = MonnitSession.CurrentCustomer.Preferences["Date Format"].ToLower();
        string prefTime = MonnitSession.CurrentCustomer.Preferences["Time Format"];

        if (prefTime.Contains("tt"))
            prefTime = prefTime.Replace("tt", "A");
        //if (prefTime.Contains("mm"))
        //    prefTime = prefTime.Replace("mm", "ii");
    %>
    <script type="text/javascript">
        let datePickMobiInst;
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $(document).ready(function () {

            var dFormat = '<%= prefDate %>';
                <%--var dFormat = prefDate.replace("yyyy", "yyyy"); // ??? purpose? If one exists then should be in a display expression (<%= ... %>)--%>
            var tFormat = '<%= prefTime %>';

            datePickMobiInst =
                $('#datePickMobi').mobiscroll().datepicker({
                    theme: 'ios',
                    controls: ['calendar', 'time'],
                    select: 'range',
                    display: popLocation,
                    dateFormat: dFormat.toUpperCase(),
                    timeFormat: tFormat,
                    defaultSelection: [new Date("<%=MonnitSession.HistoryFromDate%>"), new Date("<%=MonnitSession.HistoryToDate%>")],
                    startInput: '#Mobi_startDate',
                    endInput: '#Mobi_endDate',
                    onChange: function (event, inst) {
                        var Dates = inst.getVal();
                        var startDate = Dates[0];
                        var endDate = Dates[1];
                        setSessionDates(startDate, endDate);
                        //updateDataList(); // possible race condition where session dates not updated before new data requested
                    }
                }).mobiscroll('getInst');

            //datePickMobiInst = $('#datePickMobi').mobiscroll('getInst');
            updateDataList();
        });


        function setSessionDates(fromdate, todate) {
            if (todate != null && fromdate != null) {
                var url = '/Overview/SetMobiDates?toDate=' + todate + '&fromDate=' + fromdate;
                $.get(url, function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    } else {
                        //refreshHistory(); // fx DNE!
                        updateDataList();
                    }

                });
            }
        }
        function updateDataList() {
            $('#dbStatList').hide();
            $('#loading').show();
            $.get("/Report/DatabaseStats", function (data) {
                if (data != "Failed") {
                    $('#dbStatList').html(data);
                    $('#loading').hide();
                    $('#dbStatList').show();
                }
            });
        }
        const minToMilli = 60000;

        function quickPick(minutes) {
            let to = new Date();
            let from = new Date(to - (minutes * minToMilli));
            //datePickMobiInst.setVal([from, to]);                          // should trigger datepicker onchange but doesn't!
            //$('#datePickMobi').mobiscroll('getInst').setVal([from, to], true, true, false, 0);
            datePickMobiInst.setVal([from, to]);     // this does and yet datePickMobiInst == $('#datePickMobi').mobiscroll('getInst') => true
            setSessionDates(from, to);
        }
    </script>

    <style>
        .table > :not(:last-child) > :last-child > * {
            border-bottom-color: #ddd;
        }
    </style>
</asp:Content>
