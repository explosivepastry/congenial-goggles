<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<SensorGroup>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChartIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        // try to use ViewData["Networks"] supplied by Chart controller which starts with CSNetController.GetNetworkList but adds further refinement
        // except can't b/c of type of 
        //List<CSNet> CSNetList = (List<CSNet>)(ViewData["Networks"] ?? iMonnit.Controllers.CSNetController.GetNetworkList(null));
        var CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);

    %>

    <div class="container-fluid mt-4">
        <div class="rule-card_container w-100" id="divSensorList" style="overflow-y: scroll; width: 100%">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Sensors To Chart","Sensors To Chart")%>
                </div>
            </div>
            <div class="x_content chartEdit  scrollParentLargeChart">
                <div>
                    <% Html.RenderPartial("MobiDateRange"); %>
                    <a role="button" href="/Chart/MultiChart/" class="btn btn-primary mx-2"><%=Html.TranslateTag("Chart Sensors","Chart Sensors") %></a>
                    <input type="button" id="clearBtn" class="btn btn-secondary" value="<%: Html.TranslateTag("Clear All","Clear All")%>" />
                </div>
                <div class="clearfix"></div>
                <hr />


                <div class="d-flex flex-wrap">
                    <div class="me-2">
                        <%: Html.TranslateTag("Name","Name")%>
                        <input class="form-control" style="width: 300px;" type="text" id="searchBox" placeholder="<%: Html.TranslateTag("Sensor Name","Sensor Name")%>..." />
                    </div>
                    <div>
                        <%: Html.TranslateTag("Network","Network")%>
                        <%
                            if (CSNetList.Count > 1)
                            {%>
                        <select id="netSelect" class="form-select" style="width: 300px;">
                            <option value="null"><%: Html.TranslateTag("All Networks","All Networks")%></option>
                            <%foreach (CSNet Network in CSNetList)
                                { %>
                            <option value='<%:Network.CSNetID%>' <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected" : "" %>><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                            <% } %>
                        </select>

                        <% } %>
                    </div>
                </div>


                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                    </div>
                    <div class="col sensorEditFormInput">
                    </div>
                </div>
                <div class="row sensorEditForm">
                    <div class="col-12 col-md-3">
                    </div>
                    <div class="col sensorEditFormInput">
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div style="padding-left: 25px;"><%: Html.TranslateTag("Sensors Selected","Sensors Selected")%>: <span style="font-size: 1.2em; font-weight: bold;" id="selectedCount"></span></div>
                </div>
                <hr />

                <div style="margin: 5px"><font color="gray"><%=Html.TranslateTag("Click to add/remove sensor","Click to add/remove sensor") %></font></div>

                <div class="glanceView hasScrollChart sensor_chart1" id="sensorList">
                    <%:Html.Partial("~/Views/Chart/SensorList.ascx", Model) %>
                </div>
                <div class="text-center" id="loading" style="display: none;">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var searchTimeout = null;
        var searchRequest = null;

        $('#netSelect').change(function () {
            $('#sensorList').html('');
            $('#loading').show();
            setNetwork();
        });

        // override b/c nothing to refresh on this page
        function mobiDataRefresh() { }

        let mobiMaxRangeDays = 7;

        $(document).ready(function () {
            // 7 day maximum data time span enforced in backend by controller and frontend with `maxRange`
            //let sevenDaysInMilliSeconds = 6.048e+8; //wrong
            //$('#datePickMobi').mobiscroll('setOptions', {
            // todo:jfk we want to alert the user of the constraint but this doesn't work
            //maxRange: sevenDaysInMilliSeconds,
            //renderCalendarHeader: function () {
            //    return '<div class="mx-auto" style="color:red; font-weight:bold;">7 Days Max</div>';
            //}
            //});

            $('#searchBox').on("input", function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                $('#sensorList').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
                searchTimeout = setTimeout(setNetwork(), 1000);
            });

            $('#clearBtn').click(function (e) {
                e.preventDefault();
                $('#sensorList').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
                $.post("/Chart/ChartClearAll/", { groupID: '<%=Model.SensorGroupID%>' }, function (data) {

                });
                setNetwork();
            });
        });

        function setNetwork() {
            if (searchRequest != null) {
                searchRequest.abort();
                searchRequest = null;
            }

            searchRequest = $.get("/Chart/SensorList/" + $('#netSelect').val() + "?groupID=<%=Model.SensorGroupID%>&query=" + encodeURIComponent($('#searchBox').val()), function (data) {
                $('#sensorList').html(data);
                $('#loading').hide();
                searchRequest = null;
            });
        }
    </script>

</asp:Content>
