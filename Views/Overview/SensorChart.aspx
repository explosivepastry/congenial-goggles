<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.ChartModel>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        string name = Model.Sensor.Name;
    %>

    <%="" %>
    <div class="container-fluid">
        <% if (Request.Url.PathAndQuery.Contains("newDevice=true"))
            { %>
        <div class="col-12 alert alert-success sensor_chart_add_new" role="alert">
            <p style="font-size: 1rem;"><%: Html.TranslateTag("Overview/SensorChart|Congrats on adding your new sensor","Congrats on adding your new sensor")%>.</p>
            <button class="btn btn-primary btn-sm" id="add_new_btn">
                <%=Html.GetThemedSVG("add") %>
                <%: Html.TranslateTag("Overview/SensorChart|Add Another Sensor","Add Another Sensor")%>
            </button>
        </div>
        <% } %>
        <%Html.RenderPartial("SensorLink", Model.Sensor); %>
        <div class="col-12 px-0">
            <%if (Model.Sensor.LastDataMessage != null && (Model.Sensor.ApplicationID == 97 || Model.Sensor.ApplicationID == 125))
                { %>
            <div id="easyThermDiv">
                <%Html.RenderPartial(string.Format("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\app_{0}\\_ThermostatEasy.ascx", Model.Sensor.ApplicationID.ToString("D3")), Model.Sensor); %>
            </div>
            <%} %>
            <div class="rule-card_container" style="width: 100%;">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <div class="d-flex" style="gap: 6px; align-items: center;">
                            <div><%:Html.GetThemedSVG("details") %> </div>
                            <div style="margin-right: 20px;"><%: Html.TranslateTag("Chart","Chart")%> </div>

                            <span id="readingsLabel" style="cursor: pointer;" onclick="showReadingsChart();"><%: Html.TranslateTag("Overview/SensorChart|Data","Data")%></span><span style="vertical-align: central;"> | </span><span id="batteryLabel" style="cursor: pointer;" onclick="showBatteryChart();"><%: Html.TranslateTag("Overview/SensorChart|Battery","Battery")%></span>
                        </div>
                        <div style="display: flex;">
                            <%Html.RenderPartial("MobiDateRange");%>
                            <div id="printChartButton" class="clickable setpause" style="padding-left:.5rem; padding-top: .25rem;">
                                <%:Html.GetThemedSVG("printer") %>
                            </div>
                        </div>

                    </div>
                </div>


                <div class="card_container__content" id="ChartDiv" style="margin-top: 20px;">
                    <div class="text-center" id="loading">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden"><%: Html.TranslateTag("Overview/SensorChart|Loading","Loading")%>...</span>
                        </div>
                    </div>
                    <%  

                        string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\PreAggChart", Model.Sensor.ApplicationID.ToString("D3")); //PreAgg 
                        string RegChartViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Chart", Model.Sensor.ApplicationID.ToString("D3")); //Regular

                    %>
                    <div class="clearfix"></div>
                    <div class="col-12 device_detailsRow">
                    </div>
                </div>
            </div>
            <div id="NoteDiv">
                <%Html.RenderPartial("_SensorNoteList", Model);%>
            </div>



            <%if (MonnitSession.CurrentTheme.Theme == "Default")
                {%>
            <div id="supportDocs" class="col-lg-6 col-12 device_detailsRow__card pe-lg-3" style="min-height: 10px !important;">
                <div class="rule-card_container" style="width: 100%;">
                    <div class="card_container__top ">
                        <div class="card_container__top__title">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Support","Support")%>
                            &nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="x_content">
                        <div class="card__container__body">
                            <div class="col-md-12 col-xs-12 card_container__body__content">
                                <%Html.RenderPartial("_DeviceInfoSupport", Model.Sensor.SKU);%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <% } %>
        </div>
    </div>

    <script type="text/javascript">
        let sensorName = "<%=name%>";
        let currentURL = window.location.href;

        function printChart() {
            let dataUrl = document.querySelector('[data-zr-dom-id="zr_0"]').toDataURL();
            if (!dataUrl) {
                dataUrl = document.querySelector("canvas")?.toDataURL();
            }

            let windowContent = '<!DOCTYPE html>';
            windowContent += '<html>';
            windowContent += `<head><title>Sensor Data From: ${currentURL} </title></head>`;
            windowContent += '<body>';
            windowContent += `<h1 style="font-size:16px;">${sensorName}</h1>`;
            windowContent += '<img src="' + dataUrl + '">';
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
        printButton.addEventListener("click", printChart)

        let mobiMaxRangeDays = 365;
<%
        bool hasAdvancedSupport = MonnitSession.CustomerCan("Support_Advanced");

        if (!MonnitSession.IsEnterprise)
        {
            DateTime oneYearAgo = DateTime.UtcNow.AddDays(-366);

            //Allowing support to see data up to three years old
            if (hasAdvancedSupport)
            {
                oneYearAgo = DateTime.UtcNow.AddDays(-1095);
            }
%>
        let mobiMinDate = "<%= oneYearAgo.ToDateFormatted("yyyy/MM/dd")  %>";
<%
        }
%>
        $(document).ready(function () {
            //refreshChartdata();
            $('#readingsLabel').addClass("chartSelectedText");
            $('#batteryLabel').addClass("chartUnSelectedText");
        });

        // override default b/c we need 2
        function mobiDataRefresh() {
            //console.log("mobiDataRefresh")
            refreshChartdata()
                .then(refreshNotedata)
                .then(getSessionDates);
        }

        function refreshChartdata() {
            //console.log("refreshChartdata")
            $('#ChartDiv').html("Loading...");
            return $.post('/Overview/refreshChartdata', { id: <%=Model.Sensor.SensorID%>, isBatteryChart: false }, function (data) {
                $('#ChartDiv').html("...Waiting...");
                setTimeout(function () { $('#ChartDiv').html(data); }, 1000);
            }).fail(function (response) {
                showSimpleMessageModal("<%=Html.TranslateTag("Error response from mobiDataRefresh ()")%>");
            });
        }

        function refreshNotedata() {
            //console.log("refreshNotedata")
            $('#NoteDiv').html("Loading...");
            return $.post('/Overview/refreshNotedata', { id: <%=Model.Sensor.SensorID%> }, function (data) {
                $('#NoteDiv').html("...Waiting...");
                setTimeout(function () { $('#NoteDiv').html(data); }, 1000);
            }).fail(function (response) {
                showSimpleMessageModal("<%=Html.TranslateTag("Error response from mobiDataRefresh ()")%>");
            });
        }

        function showBatteryChart() {

            $('#readingsLabel').removeClass("chartSelectedText");
            $('#readingsLabel').addClass("chartUnSelectedText");
            $('#batteryLabel').removeClass("chartUnSelectedText");
            $('#batteryLabel').addClass("chartSelectedText");

            var sensorID = <%=Model.Sensor.SensorID%>;
            $.post("/Overview/refreshChartdata", { id: sensorID, isBatteryChart: true }, function (data) {
                $('#loading').hide();
                $('#ChartDiv').html(data);
            });
        }

        function showReadingsChart() {

            $('#batteryLabel').removeClass("chartSelectedText");
            $('#batteryLabel').addClass("chartUnSelectedText");
            $('#readingsLabel').removeClass("chartUnSelectedText");
            $('#readingsLabel').addClass("chartSelectedText");

            refreshChartdata();
        }

        $('#add_new_btn').on("click", function () {
            location.href = "/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID%>?networkID=<%=Model.Sensor.CSNetID%>";
        })

    </script>

</asp:Content>
