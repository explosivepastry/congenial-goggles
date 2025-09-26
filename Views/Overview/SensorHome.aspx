<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        string RecentData = string.Empty;
        if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
        {
            if (Model.LastDataMessage != null)
            {
                RecentData = Model.LastDataMessage.DisplayData;
                if (RecentData.Length > 30)
                    RecentData = RecentData.Substring(0, 30);
            }
            else
            {
                RecentData = "None";
            }
        }
        else
        {
            RecentData = "No current reading";
        }
        string imagePath = "";
        switch (Model.Status)
        {
            case Monnit.eSensorStatus.OK:
                imagePath = Html.GetThemedContent("/images/good.png");
                break;
            case Monnit.eSensorStatus.Warning:
                imagePath = Html.GetThemedContent("/images/Alert.png");
                break;
            case Monnit.eSensorStatus.Alert:
                imagePath = Html.GetThemedContent("/images/alarm.png");
                break;
            //case Monnit.eSensorStatus.Inactive:
            //    imagePath = Html.GetThemedContent("/images/inactive.png");
            //    break;
            //case Monnit.eSensorStatus.Sleeping:
            //    imagePath = Html.GetThemedContent("/images/sleeping.png");
            //    break;
            case Monnit.eSensorStatus.Offline:
                imagePath = Html.GetThemedContent("/images/sleeping.png");
                break;
        }
        string dirtyIcon = "";
        if (Model.IsDirty)
        {
            if (imagePath == "/images/inactive.png")
                dirtyIcon = Html.GetThemedContent("/images/inactive-dirty.png");
            else if (imagePath == "/images/good.png")
                dirtyIcon = Html.GetThemedContent("/images/good-dirty.png");
            else if (imagePath == "/images/sleeping.png")
                dirtyIcon = Html.GetThemedContent("/images/sleeping-dirty.png");
            else if (imagePath == "/images/Alert.png")
                dirtyIcon = Html.GetThemedContent("/images/Alert-dirty.png");
        }
    %>

    <style type="text/css">
        .sensorStatusBox {
            width: 45px;
        }

        .chartSelectedText {
            font-size:0.9em;
            text-decoration:underline;
            color:#0067ab;
        }

        .chartUnSelectedText {
            font-size:0.8em;
            text-decoration:none;
            color:#0067ab;
        }

        @media screen and (min-width:571px) {
            r-break {
                display: none;
            }
        }
    </style>

    <div class="container-fluid" id="fullScroll">
        <%Html.RenderPartial("SensorLink", Model); %>
        <div class="col-12 px-0" id="outsideScroll">
            <div class="rule-card_container mt-lg-2 powertour-hook w-100" id="hook-seven">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <div class="col-md-6 col-12" style="width: 50%;">
                                <h2 style="max-width: 90%; overflow: unset; font-weight: bold">
                                    <%: Html.TranslateTag("Overview/SensorHome|Sensor Readings","Sensor Readings")%>
                                </h2>
                            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                                { %>
                                <span id="readingsLabel" style="cursor:pointer;" onclick="showReadingsChart();"><%: Html.TranslateTag("Overview/SensorHome|Readings","Readings")%></span>
                                <span style="vertical-align:central;"> | </span>
                                <%--<span style="vertical-align:central;"><%: Html.TranslateTag(" | "," | ")%></span>--%>
                                <span id="missedCommsLabel" style="cursor:pointer;" onclick="showMissedComms();"><%: Html.TranslateTag("Overview/SensorHome|Missed Communications","Missed Communications")%></span>
                            <%} %>
                            </div>

                            <div class="col-6 d-flex justify-content-end">
                                <%Html.RenderPartial("MobiDateRange");%>
                                <%if (MonnitSession.CustomerCan("Sensor_Export_Data") && !Request.IsSensorCertMobile())
                                    { %>
                                <!-- export button -->
                                <a href="/Export/ExportSensorData/<%=Model.SensorID%>" target="_blank" title="<%: Html.TranslateTag("Export","Export")%>" class="ms-2 helpIco" style="cursor: pointer; float: right;">
                                    <%=Html.GetThemedSVG("download-file") %>
                                </a>
                                <% } %>
                            </div>
                        </div>
                    </div>

                <div class="clearfix"></div>
                
                <div class="x_content hasScroll-rule pe-0" id="sensorHistory">
                </div>
                <div class="text-center" id="loading" style="display:none;">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        <%= ExtensionMethods.LabelPartialIfDebug("SensorHome.aspx") %>
        var isWorking = false;
        $(function () {
            //refreshHistory();

            //$(window).scroll(function () {
            //    if (($(window).scrollTop() + $(window).height() > $(document).height() - 100) && isWorking == false) {
            //        var DataMessageGUID = $('.historyReading').last().attr('data-guid');
            //        getNextData(DataMessageGUID);

            //    }
            //});

            $('#sensorHistory').scroll(function () {
                /*console.log(`$(this).scrollTop() = ${$(this).scrollTop()} $(this).innerHeight() = ${$(this).innerHeight()} $(this).scrollTop() + $(this).innerHeight() = ${$(this).scrollTop() + $(this).innerHeight()}  $(this)[0].scrollHeight = ${$('#sensorHistory')[0].scrollHeight}`);*/
                if ($(this).scrollTop() + $(this).innerHeight() + 1 >= $(this)[0].scrollHeight) {
                    if (isWorking == false) {
                        getNextData();
                    }
                }
            });

            $('#readingsLabel').addClass("chartSelectedText");
            $('#missedCommsLabel').addClass("chartUnSelectedText");

        });

        function getNextData() {
            var DataMessageGUID = $('.historyReading1').last().attr('data-guid');
            isWorking = true;
            $('#loading').show();

            $.post("/Overview/SensorHistoryData/", { sensorID: '<%=Model.SensorID%>', dataMsg: DataMessageGUID }, function (data) {
                isWorking = false;
                $('#loading').hide();

                if (data == "No Data") {
                    return;
                }
                $("#sensorHistory").append(data);
                
                //$(".note").on("mouseenter", function () {
                //    var cell = $(this);
                //    cell.find('.myhover').height(cell.height()).width(cell.width()).fadeIn(700);
                //}).on("mouseleave", function () {
                //    $(this).find('.myhover').stop().fadeOut(100);
                //});
            });
        }



        let mobiDataDestElem = '#sensorHistory';
        let mobiDataPayload = { sensorID: '<%=Model.SensorID%>', dataMsg: '' };
        let mobiDataController = '/Overview/SensorHistoryData';

        function exportHistory() {
            disableUnsavedChangesAlert();
            window.location = '/Export/ExportSensorData/' + '<%=Model.SensorID%>';
        }

		function showMissedComms() {
            $('#readingsLabel').removeClass("chartSelectedText");
            $('#readingsLabel').addClass("chartUnSelectedText");
            $('#missedCommsLabel').removeClass("chartUnSelectedText");
            $('#missedCommsLabel').addClass("chartSelectedText");
			mobiDataController = '/Sensor/HistoryMissedOV/<%=Model.SensorID%>';
			mobiDataPayload = { id: '<%=Model.SensorID%>' };

            var sensorID = <%=Model.SensorID%>;
            $.get("/Sensor/HistoryMissedOV/"+ sensorID, function (data) {
                $('#loading').hide();
                $('#sensorHistory').html(data);
            });
        }

		function showReadingsChart() {
            $('#missedCommsLabel').removeClass("chartSelectedText");
            $('#missedCommsLabel').addClass("chartUnSelectedText");
            $('#readingsLabel').removeClass("chartUnSelectedText");
            $('#readingsLabel').addClass("chartSelectedText");
			mobiDataController = '/Overview/SensorHistoryData/<%=Model.SensorID%>';
			mobiDataPayload = { sensorID: '<%=Model.SensorID%>', dataMsg: '' };

            $.post("/Overview/SensorHistoryData/", { sensorID: '<%=Model.SensorID%>' }, function (data) {
                isWorking = false;
                $('#loading').hide();

                if (data == "No Data") {
                    return;
                }
                $("#sensorHistory").html(data);
            });
        }

    </script>

    <style type="text/css">
        .right_col {
            padding-bottom: 10px!important;
        }
    </style>
        
</asp:Content>
