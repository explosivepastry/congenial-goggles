<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.VisualMap>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevicesToShowGPS 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="clearfix"></div>
        <div class="my-4">
            <%Html.RenderPartial("_MapLink", Model); %>
        </div>

        <div class="col-12">
            <div class="x_panel shadow-sm rounded mb-4" id="divSensorList">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Map/EditMap|Devices To Show on Map","Devices To Show on Map")%>
                    </div>
                </div>
                <div class="x_content mapEdit scrollParentSmall">
                    <div class="form-group">
                        <div class="col-sm-4 col-12">
                            <%
                                List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
                                if (CSNetList.Count > 1)
                                {%>
                            <select id="netID" onchange="loadDevices();" class="form-select" style="width: 350px; max-width: 100%;">
                                <option <%:MonnitSession.SensorListFilters.CSNetID == long.MinValue ? "selected=selected" : "" %> value="null"><%: Html.TranslateTag("All Networks","All Networks")%></option>
                                <%foreach (CSNet Network in CSNetList)
                                    { %>
                                <option value='<%:Network.CSNetID%>' <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected" : "" %>><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                                <% } %>
                            </select>

                            <% } %>
                        </div>
                        <div class="col-sm-4 col-12">
                            <input value="<%=ViewData["filterQuery"].ToStringSafe() %>" type="text" name="searchQuery" id="searchQuery"  style="width: 350px; max-width: 100%;height :37.97px;" placeholder="Search...">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="card_container__body hasScroll-sm pt-3" id="deviceList">
                    </div>
                </div>
            </div>
        </div>

        <!-- End Device Info Panel -->



    </div>
    <!-- End Map View -->

    <script>
        var failedErrMessage = '<%: Html.TranslateTag("Map/EditMap|Failed: Could not load device list.")%>';
        var searchTimeout = null;
        var searchRequest = null;

        $(document).ready(function () {
            loadDevices();

            $('#searchQuery').keyup(function () {
                if (searchTimeout != null)
                    clearTimeout(searchTimeout);
                if (searchRequest != null) {
                    searchRequest.abort();
                    searchRequest = null;
                }
                searchTimeout = setTimeout("loadDevices()", 1000);
            });

        });

        function loadDevices() {
            var netID = $('#netID').val();
            var acctID = '<%=Model.AccountID%>';
            var mapID = '<%=Model.VisualMapID%>';
            var searchQuery = encodeURIComponent($('#searchQuery').val())

            $.post("/Map/GpsDeviceList/", { accountID: acctID, visualMapID: mapID, csNetID: netID, filterQuery: searchQuery }, function (data) {
                if (data.includes('Failed:')) 
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed: Could not load device list.")%>");
                else
                    $('#deviceList').html(data);
            });
        }

    </script>

</asp:Content>
