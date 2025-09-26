<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.ChartModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorChart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        //DateTime fromDate = MonnitSession.HistoryFromDate;
        //DateTime toDate = MonnitSession.HistoryToDate;
    %>
    <div class="container-fluid">
        <% if (Request.Url.PathAndQuery.Contains("newDevice=true"))
            { %>
        <div class="col-12 alert alert-success sensor_chart_add_new" role="alert">
            <p style="font-size: 1rem;">Congrats on adding your new sensor.</p>
            <button class="btn btn-primary btn-sm" id="add_new_btn">
                <%=Html.GetThemedSVG("add") %>
                Add Another Sensor
            </button>
        </div>
        <% } %>
        <%Html.RenderPartial("SensorLink", Model.Sensor); %>
        <div class="col-12 px-0">
            <%if (MonnitSession.IsCurrentCustomerMonnitAdmin && Model.Sensor.LastDataMessage != null && Model.Sensor.ApplicationID == 125)
                { %>
            <div id="easyThermDiv">
                <%Html.RenderPartial("_ThermostatEasy", Model.Sensor); %>
            </div>
            <%} %>
            <div class="card_container shadow-sm rounded mb-4">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <%: Html.TranslateTag("Overview/SensorChart|Sensor Chart Reading","Chart")%>: <%: Model.Sensor.SensorName%>

                        <%Html.RenderPartial("MobiDateRange");%>
                    </div>
                </div>

                <div class="card_container__content" id="ChartDiv" style="margin-top: 20px;">
                    <div class="text-center" id="loading">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
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
                <div class="x_panel shadow-sm rounded">
                    <div class="card_container__top ">
                        <div class="card_container__top__title">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Support", "Support")%>
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
        $(document).ready(function () {
            refreshChartdata();
      
            $('#applyTherm').click(function (e) {
                e.preventDefault();
                $.post("/Overview/EasyThermostatEdit/<%=Model.Sensor.SensorID%>", $("#thermForm").serialize(), function (data) {
                    if (data.includes('Failed:')) {
                        $('#errorMessage').html(data);
                        setTimeout(clearMessage, 6000);
                    } else {
                        $('#easyThermDiv').html(data);
                    }
                });
            });
        });
        // override default b/c we need 2
        function mobiDataRefresh() {
            refreshChartdata();
            refreshNotedata();
        }

        function refreshChartdata() {
            $('#ChartDiv').html("Loading...");
            $.post('/Overview/refreshChartdata', { id: <%=Model.Sensor.SensorID%>, isBatteryChart: false }, function (data) {
                $('#ChartDiv').html("...Waiting...");
                setTimeout(function () { $('#ChartDiv').html(data); }, 1000);
            }).fail(function (response) {
                showSimpleMessageModal("<%=Html.TranslateTag("Error response from mobiDataRefresh()")%>");
            });
        }

        function refreshNotedata() {
            $('#NoteDiv').html("Loading...");
            $.post('/Overview/refreshNotedata', { id: <%=Model.Sensor.SensorID%> }, function (data) {
                $('#NoteDiv').html("...Waiting...");
                setTimeout(function () { $('#NoteDiv').html(data); }, 1000);
            }).fail(function (response) {
                showSimpleMessageModal("<%=Html.TranslateTag("Error response from mobiDataRefresh()")%>");
            });
        }

        $('#add_new_btn').on("click", function () {
            location.href = "/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID%>?networkID=<%=Model.Sensor.CSNetID%>";
        })

    </script>

    <style>
        .sensor_chart_add_new {
            display: flex !important;
            align-items: center;
            margin: 10px 0 0 0;
        }

            .sensor_chart_add_new button svg {
                margin-right: 5px;
            }

            .sensor_chart_add_new p {
                font-size: 1.6rem;
                margin: 0 20px;
            }
    </style>
</asp:Content>
