<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cable Log
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
//string RecentData = string.Empty;
//if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
//{
//    if (Model.LastDataMessage != null)
//    {
//        RecentData = Model.LastDataMessage.DisplayData;
//        if (RecentData.Length > 30)
//            RecentData = RecentData.Substring(0, 30);
//    }
//    else
//    {
//        RecentData = "None";
//    }
//}
//else
//{
//    RecentData = "No current logs";
//}
//string imagePath = "";
//switch (Model.Status)
//{
//    case Monnit.eSensorStatus.OK:
//        imagePath = Html.GetThemedContent("/images/good.png");
//        break;
//    case Monnit.eSensorStatus.Warning:
//        imagePath = Html.GetThemedContent("/images/Alert.png");
//        break;
//    case Monnit.eSensorStatus.Alert:
//        imagePath = Html.GetThemedContent("/images/alarm.png");
//        break;
//    case Monnit.eSensorStatus.Offline:
//        imagePath = Html.GetThemedContent("/images/sleeping.png");
//        break;
//}
//string dirtyIcon = "";
//if (Model.IsDirty)
//{
//    if (imagePath == "/images/inactive.png")
//        dirtyIcon = Html.GetThemedContent("/images/inactive-dirty.png");
//    else if (imagePath == "/images/good.png")
//        dirtyIcon = Html.GetThemedContent("/images/good-dirty.png");
//    else if (imagePath == "/images/sleeping.png")
//        dirtyIcon = Html.GetThemedContent("/images/sleeping-dirty.png");
//    else if (imagePath == "/images/Alert.png")
//        dirtyIcon = Html.GetThemedContent("/images/Alert-dirty.png");
//}
    %>
    <style type="text/css">
        .sensorStatusBox {
            width: 45px;
        }

        .chartSelectedText {
            font-size: 0.9em;
            text-decoration: underline;
            color: #0067ab;
        }

        .chartUnSelectedText {
            font-size: 0.8em;
            text-decoration: none;
            color: #0067ab;
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
                                <%: Html.TranslateTag("Overview/CableLog|Cable Log","Cable Log")%>
                            </h2>
                            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                                { %>
                            <span id="cableLabel" style="cursor: pointer;" onclick="showCableLog();"></span>
                            <span style="vertical-align: central;"> </span>
                            <%} %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>

                <div class="x_content hasScroll-rule pe-0" id="cableHistory">
                </div>
                <div class="text-center" id="loading" style="display: none;">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        <%= ExtensionMethods.LabelPartialIfDebug("CableLog.aspx") %>

        var isWorking = false;
        $(function () {
            loadCableHistory();
        });

        function loadCableHistory() {

            var CableLog = $('.historyReading').last().attr('data-guid');
            isWorking = true;
            $('#loading').show();

            $.post("/Overview/CableHistoryData/", { sensorID: '<%=Model.SensorID%>', cableLog: CableLog }, function (data) {
                isWorking = false;
                $('#loading').hide();

                //if (data == "No Data") {
                //    return;
                //}
                $("#cableHistory").append(data);
            });
        }

        function showCableLog() {
            $('#cableLabel').removeClass("chartUnSelectedText");
            $('#cableLabel').addClass("chartSelectedText");

            $.post("/Overview/CableHistoryData/", { sensorID: '<%=Model.SensorID%>' }, function (data) {
                isWorking = false;
                $('#loading').hide();

                alert(data);

                if (data == "No Data") {
                    showAlertModal(data);
                    return;
                }
                $("#cableHistory").html(data);
            });
        }

    </script>

    <style>
        .right_col {
            padding-bottom: 10px !important;
        }
    </style>

</asp:Content>
