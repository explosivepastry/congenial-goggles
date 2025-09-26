<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MultiChart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-4">
        <div class="col-12">
            <div class="x_panel shadow-sm rounded">
                <div class="x_title dfjcsbac">
                    <a class="btn btn-primary" href="/Chart/ChartEdit/">&nbsp;<%: Html.TranslateTag("Add / Remove Sensors","Add / Remove Sensors")%></a>
                    <div class="nav navbar-right panel_toolbox">
                        <div style="display: flex;">
                            <% Html.RenderPartial("MobiDateRange"); %>
                            <div id="printChartButton" class="clickable setpause" style="padding-left: .5rem; padding-top: .25rem;">
                                <%:Html.GetThemedSVG("printer") %>
                            </div>
                        </div>

                        <%--<div style="text-align: right;">
                            <input style="width: 75px;" class="mobiDate_container__start" id="Mobi_startDate" placeholder="<%=fromDate.OVToLocalDateShort() %>" />
                            - 
                            <input style="width: 75px;" class="mobiDate_container__end" id="Mobi_endDate" placeholder="<%=toDate.OVToLocalDateShort() %>" />
                            <a id="datePickMobi" style="cursor: pointer;"><%=Html.GetThemedSVG("calendar") %></a>
                        </div>--%>
                    </div>
                </div>
                <div class="x_content" id="ChartDiv">
                </div>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>

    <script type="text/javascript">

        let mobiDataDestElem = '#ChartDiv';
        let mobiDataPayload = { id: null };
        let mobiDataController = '/Chart/MultiChartChart';
        let mobiMaxRangeDays = 7;



        let sensorName = "Test";
        let currentURL = window.location.href;

        function printAllCharts() {
            let charts = document.querySelectorAll('[data-zr-dom-id="zr_0"]');
            let chartTextElements = document.querySelectorAll('.chart_row__details');

            let chartTitles = [];
            chartTextElements.forEach(element => {
                chartTitles.push(element.childNodes[0]?.textContent.trim());
            })

            let chartDataUrl = [];
            charts.forEach(chart => {
                chartDataUrl.push(chart.toDataURL());
            });

            let windowContent = '<!DOCTYPE html>';
            windowContent += '<html>';
            windowContent += `<head><title>Sensor Data From: ${currentURL} </title></head>`;
            windowContent += '<body>';

            for (let i = 0; i < chartDataUrl.length; i++) {
                windowContent += `<h1 style="font-size:16px;">${chartTitles[i]}</h1>`;
                windowContent += '<img src="' + chartDataUrl[i] + '">';
            }

            windowContent += '</body>';
            windowContent += '</html>';

            let printWin = window.open('sensorData', 'Sensor Data', 'width=' + screen.availWidth + ',height=' + screen.availHeight);
            printWin.document.open();
            printWin.document.write(windowContent);

            printWin.document.addEventListener('load', function () {
                printWin.focus();
                printWin.print();
                printWin.document.close();
                printWin.close();
            }, true);
        }

        const printButton = document.querySelector("#printChartButton");
        printButton.addEventListener("click", printAllCharts)

        $(document).ready(function () {
            //    // 7 day maximum data time span enforced in backend by controller and frontend with `maxRange`
            //let sevenDaysInMilliSeconds = 6.048e+8; //wrong
            //    $('#datePickMobi').mobiscroll('setOptions', {
            //         todo:jfk we want to alert the user of the constraint but this doesn't work
            //        maxRange: sevenDaysInMilliSeconds,
            //        renderCalendarHeader: function () {
            //            return '<div class="mx-auto" style="color:red; font-weight:bold;">7 Days Max</div>';
            //        }
            //    });
        });

    </script>
</asp:Content>
