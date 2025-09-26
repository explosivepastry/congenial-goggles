<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        Gateway gw = Model; // Gateway.Load(Url.RequestContext.RouteData.Values["id"].ToLong());
        bool IsProtocolVersionOne = gw.GatewayTypeID == 1 || gw.GatewayTypeID == 3 || gw.GatewayTypeID == 8 || gw.GatewayTypeID == 19 || gw.GatewayTypeID == 21;
    %>

    <div class="container-fluid">
        <% if (Request.Url.PathAndQuery.Contains("newDevice=true"))
            { %>
        <div class="col-12 alert alert-success sensor_chart_add_new" role="alert">
            <p style="font-size: 1rem;"><%: Html.TranslateTag("Overview/GatewayHome|Congrats on adding your new gateway","Congrats on adding your new gateway")%>.</p>
            <button class="btn btn-primary btn-sm" id="add_new_btn" style="background-color: #0067ab;">
                <%=Html.GetThemedSVG("add") %>
                <%: Html.TranslateTag("Overview/GatewayHome|Add Another Device","Add Another Device")%>
            </button>
        </div>
        <% } %>
        <%Html.RenderPartial("GatewayLink", gw); %>
        <div class="col-12 device_detailsRow__card">

     <%--cg moved to its own view 12/14/2022
    
     <%if (Model.GatewayTypeID == 30 || Model.GatewayTypeID == 32 || Model.GatewayTypeID == 24 || Model.GatewayTypeID == 26) { %>
            <div class="x_panel card_container shadow-sm rounded mb-4">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <div> <%: Html.TranslateTag("Overview/GatewayHome|Cellular Data Use")%>:  
                            <span class="dataUseChartSelector Current" onclick="LoadDataUseChart('Current');"><%: Html.TranslateTag("Overview/GatewayHome|Current Month")%></span>
                            <span style="vertical-align:central;"> | </span>
                            <span class="dataUseChartSelector Last" onclick="LoadDataUseChart('Last');"><%: Html.TranslateTag("Overview/GatewayHome|Last Month")%></span>
                            <span style="vertical-align:central;"> | </span>
                            <span class="dataUseChartSelector Year" onclick="LoadDataUseChart('Year');"><%: Html.TranslateTag("Overview/GatewayHome|Past 12 Months")%></span>
                        </div>
                    </div>
                </div>

                <div class="card_container__content" style="margin-top: 20px;">
                    <div class="text-center" id="dataUseLoading">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                    <div id="dataUseChartDiv">
                    <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                function LoadDataUseChart(range) {
                    $(".dataUseChartSelector").removeClass("dataUseChartSelectorActive");
                    $("." + range).addClass("dataUseChartSelectorActive");
                    
                    $('#dataUseLoading').show();
                    $('#dataUseChartDiv').hide();

                    $.post('/Overview/GatewayDataUseChart', { id: <%=Model.GatewayID%>, range: range }, function (data) {
                        $('#dataUseChartDiv').html(data);
                        $('#dataUseLoading').hide();
                        $('#dataUseChartDiv').show();
                    });
                }

                $(function () {
                    LoadDataUseChart("Current");
                });
                
            </script>

            <%} %>--%>

            <div class="rule-card_container w-100">
                <div class="x_title">
                    <div class="card_container__top">
                        <div class="card_container__top__title d-flex justify-content-between">
                            <div class="col-md-6 col-6" style="width: 50%;">
                                <%: Html.TranslateTag("Overview/GatewayHome|Gateway Messages","Gateway Messages")%>
                            </div>

                            <div class="col-6 d-flex justify-content-end">
                                <%Html.RenderPartial("MobiDateRange");%>
                                <%if (MonnitSession.CustomerCan("Sensor_Export_Data") && !Request.IsSensorCertMobile())
                                    { %>
                                <a onClick="exportHistory()" title="<%: Html.TranslateTag("Export","Export")%>" class="ms-2 helpIco" style="cursor: pointer; float: right;">
                                    <%=Html.GetThemedSVG("export") %>
                       
                                </a>
                                <% } %>
                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </div>

                <div class="x_content hasScroll" id="gatewayHistory" style="margin-top: 0px;">
                    <%--<%Html.RenderPartial("GatewayMessageList", Model);%>--%>
                </div>
            </div>
        </div>
    <%--    <%if (MonnitSession.CurrentTheme.Theme == "Default")
          {%>
        <div class="col-12 col-lg-6 pe-lg-3" style="display: flex; height: inherit;">
            <div class="rule-card_container w-100" style="height: inherit; margin-top: 0;">
                <div class="card_container__top ">
                    <div class="card_container__top__title docTitle">
                        <div style="display: flex; align-items: baseline;">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Support", "Support")%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="x_content">
                    <div class="card__container__body">
                        <div class="col-12 card_container__body__content">
                            <%Html.RenderPartial("_DeviceInfoSupport", Model.SKU);%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <% } %>--%>
    </div>

    <script type="text/javascript">

        let mobiDataDestElem = '#gatewayHistory';
        let mobiDataPayload = { id: <%=Model.GatewayID%> };
        let mobiDataController = '/Overview/GatewayMessageRefresh';

        function exportHistory() {
            disableUnsavedChangesAlert();
            window.location = '/Export/ExportGatewayData/' + '<%=Model.GatewayID%>';
        }

        $('#add_new_btn').on("click", function () {
            location.href = "/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID%>?networkid=<%=Model.CSNetID%>";
        })
    </script>
    
    <style type="text/css">
        .sensor_chart_add_new {
            display: flex !important;
            align-items: center;
            background: #d1e7dd;
            color: black;
            border: none;
            margin: 10px 0 0 0;
        }

            .sensor_chart_add_new button svg {
                margin-right: 5px;
            }

            .sensor_chart_add_new p {
                font-size: 1.6rem;
                margin: 0 20px;
            }
    
        .dataUseChartSelectorActive {
            font-size:0.9em;
            text-decoration:underline;
        
        }
        .dataUseChartSelector {
            cursor:pointer;
            font-size:0.8em;
            text-decoration:none;
            color:#0067ab;
        }
        .dataUseChartSelectorActive {
            font-size:0.9em;
            text-decoration:underline;
          
        }
    </style>

</asp:Content>
