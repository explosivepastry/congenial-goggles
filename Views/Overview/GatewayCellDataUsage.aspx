<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GatewaySensorList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">

        <%Html.RenderPartial("GatewayLink", Model); %>
        <div class="col-12 device_detailsRow__card">

            <%if (Model.GatewayTypeID == 30 || Model.GatewayTypeID == 32 || Model.GatewayTypeID == 24 || Model.GatewayTypeID == 26)
                { %>

            <div class="x_panel card_container shadow-sm rounded mb-4">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <div>
                            <%: Html.TranslateTag("Overview/GatewayHome|Cellular Data Use","Cellular Data Use")%>:  
                            <span class="dataUseChartSelector Current" onclick="LoadDataUseChart('Current');"><%: Html.TranslateTag("Overview/GatewayHome|Current Month","Current Month")%></span>
                            <span style="vertical-align: central;">| </span>
                            <span class="dataUseChartSelector Last" onclick="LoadDataUseChart('Last');"><%: Html.TranslateTag("Overview/GatewayHome|Last Month","Last Month")%></span>
                            <span style="vertical-align: central;">| </span>
                            <span class="dataUseChartSelector Year" onclick="LoadDataUseChart('Year');"><%: Html.TranslateTag("Overview/GatewayHome|Past 12 Months","Past 12 Months")%></span>
                        </div>
                    </div>
                </div>

                <div class="card_container__content" style="margin-top: 20px;">
                    <div class="text-center" id="dataUseLoading">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                        </div>
                    </div>

                    <div id="dataUseChartDiv">
                        <div class="clearfix"></div>
                    </div>
                </div>
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
    <%} %>

</asp:Content>
