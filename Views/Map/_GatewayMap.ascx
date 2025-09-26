<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<LocationMessage>>" %>

<%
    Gateway gateway = ViewData["Gateway"] as Gateway;
    DateTime dateNow = DateTime.Now;
    string googleMapApiKey = MonnitSession.CurrentTheme.PropertyValue("Maps_API_Key");
%>

<div class="col-12 device_detailsRow__card">
    <div class="x_panel shadow-sm rounded">
        <div class="x_title">
          <div class="card_container__top">
             <div class="card_container__top__title d-flex justify-content-between" style="overflow:unset;">
                <div class="col-md-4 col-12"><%: Html.TranslateTag("Overview/GatewayGPS|Location Messages","Location Messages")%>
                   <span style="padding: 5px 15px;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" class=" d-none d-lg-inline-block">
                      <%=Html.GetThemedSVG("help") %>
                     </span>

            <div class="dropdown-menu shadow rounded" style="padding: 0;">
                <ul class="ps-0 mb-0">
                    <li>
                        <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="event.preventDefault();">
                            <div class="eventIcon_container">
                                <div class="eventIcon eventIconStatus sensorStatusOK"></div>
                                <%=Html.GetThemedSVG("gps-pin") %>
                            </div>
                            <%: Html.TranslateTag("Overview/GatewayGPS|Location Reported","Location Reported")%>
                        </div>
                        </li>

                    <li>
                        <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="event.preventDefault();">
                            <div class="eventIcon_container">
                                <div class="eventIcon eventIconStatus sensorStatusInactive"></div>
                                <%=Html.GetThemedSVG("gps-pin") %>
                            </div>
                            <%: Html.TranslateTag("Overview/GatewayGPS|Location Error","Location Error")%>
                        </div>
                    </li>

                <%--<hr class="my-0">
                <li>
                    <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="event.preventDefault();">
                        <%: Html.TranslateTag("Delete","Delete")%>
                        <span>
                            <%=Html.GetThemedSVG("delete") %>
                        </span>
                    </div>
                </li>--%>

                </ul>
            </div>
        </div>

            <div class="col-md-8 col-12" style="text-align: right;">
                <%Html.RenderPartial("MobiDateRange");%>
                <%if (MonnitSession.CustomerCan("Sensor_Export_Data") && !Request.IsSensorCertMobile())
                    { %>
                <!-- export button -->
                <a href="/Export/ExportLocationData/<%=gateway.GatewayID%>" target="_blank" title="<%: Html.TranslateTag("Export/ExportLocationData|Export","Export")%>" class="ms-2" style="float: right;">
                    <%=Html.GetThemedSVG("download-file") %>
                </a>
                <% } %>
            </div>
        </div>
    </div>
                   
 <div class="clearfix"></div>
 </div>

        <div class="x_content d-flex justify-content-between">
            <div id="resultsList" class="col-4 col-xl-3 d-none d-lg-block" style="overflow-y: auto; max-height: 500px">
                <%: Html.Partial("~/Views/Map/_GatewayMapResults.ascx", Model) %>
            </div>
            <div class="col-lg-8 col-xl-9 col-12">
                <!-- This is the container that holds the map -->
                <div id="map"></div>
            </div>

       <div class="clearfix"></div>
      </div>
    </div>
</div>
<% %>

<script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
<script src="https://maps.googleapis.com/maps/api/js?key=<%:googleMapApiKey %>&callback=initMap&v=weekly" defer></script>    

<script type="text/javascript">
    let map = null;
    let bounds = null;
    let pathCoordinates = null;
    let msgMarkers = null;
    let markers = null;
    let activeMarker = null;
    let infoWindow = null;
    //date inputs
    var gatewayID = '<%:gateway != null ? gateway.GatewayID : -1%>';
    let mobiDataDestElem = '#resultsList';
    let mobiDataPayload = { id: gatewayID};
    let mobiDataController = '/Map/GatewayMapResults';
    let mobiDataInitialLoad = false;

    Date.prototype.addDays = function (days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }

    $(function () {
        $('#Mobi_endDate').change(function () {
            var fromDateObj = $('#Mobi_startDate');
            var toDateObj = $('#Mobi_endDate');

            var fromDate = new Date(fromDateObj.val());
            var toDate = new Date(toDateObj.val());
            var timeDifference = toDate.getTime() - fromDate.getTime();
            var dayDifference = timeDifference / (1000 * 3600 * 24);

            var dayLimit = 7;
            if (Math.abs(dayDifference) > dayLimit) {
                fromDate = toDate.addDays(0 - dayLimit);
                var fromDateFormatted = fromDate.toISOString().split('.')[0];
                fromDateObj.val(fromDateFormatted);
            } else if (timeDifference < 0) {
                fromDate = toDate.addDays(-1);
                var fromDateFormatted = fromDate.toISOString().split('.')[0];
                fromDateObj.val(fromDateFormatted);
            }
        });

        $('#Mobi_startDate').change(function () {
            var fromDateObj = $('#Mobi_startDate');
            var toDateObj = $('#Mobi_endDate');
            var fromDate = new Date(fromDateObj.val());
            var toDate = new Date(toDateObj.val());
            var timeDifference = toDate.getTime() - fromDate.getTime();
            var dayDifference = timeDifference / (1000 * 3600 * 24);

            var dayLimit = 7;
            if (Math.abs(dayDifference) > dayLimit) {
                toDate = fromDate.addDays(dayLimit);
                var toDateFormatted = toDate.toISOString().split('.')[0];
                toDateObj.val(toDateFormatted);
            } else if (timeDifference < 0) {
                toDate = fromDate.addDays(1);
                var toDateFormatted = toDate.toISOString().split('.')[0];
                toDateObj.val(toDateFormatted);
            }
        });
    });

</script>