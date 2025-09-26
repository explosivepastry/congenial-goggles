<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col-md-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="col-md-6 col-xs-6" style="width: 50%;">
                        <h2 style="max-width: 90%; overflow: unset; font-weight: bold"><%= Model.SensorName%></h2>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="col-md-4 col-xs-4" style="text-align: right; width: 80%;">
                            <%Html.RenderPartial("MobiDateRange");%>
                        </div>
                        <div class="col-md-2 col-xs-2" style="float: right; width: 20%;">
                            <%if (MonnitSession.CustomerCan("Sensor_Export_Data") && !Request.IsSensorCertMobile())
                              { %>
                            <!-- export button -->
                            <a href="/Export/ExportSensorData/<%=Model.SensorID%>" target="_blank" title="<%: Html.TranslateTag("Export","Export")%>" class="helpIco" style="padding-left: 25px; cursor: pointer; float: right;"><i class="fa fa-cloud-download" style="font-size: 1.5em;"></i></a>
                            <% } %>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content" id="sensorHistory">
                    <%--<%=Html.Partial("~/Views/Overview/SensorHistoryList.ascx")%>--%>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        let mobiDataDestElem = '#sensorHistory';
        let mobiDataPayload = { sensorID: '<%=Model.SensorID%>', dataMsg: '' };
        let mobiDataController = '/Overview/SensorHistoryData';

        function exportHistory() {
            disableUnsavedChangesAlert();
            window.location = '/Export/ExportSensorData/' + '<%=Model.SensorID%>';
        }

    </script>

</asp:Content>