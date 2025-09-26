<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<LocationMessage>>" %>

<%if (Model == null || Model.Count == 0)
  {%> 
        <h3>No results</h3>
<script>
    // Initialize and add the map
    function initMap() {
        $('#map').html('');
    }
</script>
  <%}
      else
      {
          string addPointHtml = "";
          long visualMapID = ViewData["VisualMapID"].ToLong();
          DateTime today = DateTime.UtcNow;
          string todayDateString = today.ToString("yyyy-MM-ddThh:mm");
          string eightDaysAgoDateString = today.AddDays(-8).ToString("yyyy-MM-ddThh:mm");

          string todayLinkString = "fromDate=" + today.AddDays(-1).ToString("yyyy-MM-ddTHH:mm") + "&toDate=" + today.ToString("yyyy-MM-ddTHH:mm");
          string twoDayLinkString = "fromDate=" + today.AddDays(-2).ToString("yyyy-MM-ddTHH:mm") + "&toDate=" + today.ToString("yyyy-MM-ddTHH:mm");
          string fiveDayLinkString = "fromDate=" + today.AddDays(-5).ToString("yyyy-MM-ddTHH:mm") + "&toDate=" + today.ToString("yyyy-MM-ddTHH:mm");
          string sevenDayLinkString = "fromDate=" + today.AddDays(-7).ToString("yyyy-MM-ddTHH:mm") + "&toDate=" + today.ToString("yyyy-MM-ddTHH:mm");
          foreach (LocationMessage locationMessage in Model)
          {
              Gateway gateway = Gateway.Load(locationMessage.DeviceID);//Expand to accept other device Types later

              string markerTitle = "<h4>" + gateway.Name + "</h4>"
                               + "<b>" + locationMessage.LocationDate.OVToLocalDateTimeShort() + "</b>"
                               + " <br/> " + Math.Round(locationMessage.Latitude,2) + "°/" + Math.Round(locationMessage.Longitude,2) + "°"
                               + " <div class=\"d-block d-lg-none\">"
                               + "       <b>Altitude:</b> " + locationMessage.Altitude + " m"
                               + " <br/> <b>Speed:</b> " + locationMessage.Speed + " km/h"
                               //+ " <br/> <b>Course Over Ground:</b> " + locationMessage.CourseOverGround + "°"
                               //+ " <br/> <b>Fix Time:</b> " + locationMessage.FixTime + " seconds"
                               //+ " <br/> <b>Number of Satellites:</b> " + locationMessage.NumberSatellites
                               + " <br/> <b>Accuracy:</b> " + locationMessage.Uncertainty + " m"
                                   + " <br/>"
                                   + " <a style=\"color: blue;\" href=\"/Overview/GatewayGPS/" + locationMessage.DeviceID + "/?" + todayLinkString + "\">Today</a>&nbsp;"
                                   + " <a style=\"color: blue;\" href=\"/Overview/GatewayGPS/" + locationMessage.DeviceID + "/?" + twoDayLinkString + "\">2 days</a>&nbsp;"
                                   + " <a style=\"color: blue;\" href=\"/Overview/GatewayGPS/" + locationMessage.DeviceID + "/?" + fiveDayLinkString + "\">5 days</a>&nbsp;"
                                   + " <a style=\"color: blue;\" href=\"/Overview/GatewayGPS/" + locationMessage.DeviceID + "/?" + sevenDayLinkString + "\">7 days</a>&nbsp;"
                               + "</div>";

              addPointHtml += "AddPoint(" + locationMessage.Latitude + ", " + locationMessage.Longitude + ", \'" + markerTitle + "\', \'" + locationMessage.LocationMessageGUID + "\');\n";

              //sensorStatusOK sensorStatusWarning sensorStatusAlert sensorStatusInactive
              string Status = "Inactive";
              if (locationMessage.isValid)
              {
                  if (gateway.LastLocationDate.AddMinutes(gateway.GPSReportInterval + gateway.ReportInterval) > DateTime.UtcNow)
                  {
                      Status = "OK";
                  }
                  else
                  {
                      Status = "Warning";
                  }
              }
              %>

        <div class="locationMsg shadow-sm rounded align-items-center m-1" style="cursor: pointer; padding:15px 0px"  data-id="<%:locationMessage.LocationMessageGUID %>">
            <a style="width: 100%;">
                <div class="d-flex align-items-center">
                    <div class="divCellCenter">
                        <div class="eventIcon_container">
                            <div class="eventIcon eventIconStatus sensorStatus<%:Status%>"></div>
                            <%=Html.GetThemedSVG("gps-pin") %>
                        </div>
                    </div>
                    <div class="glance-text">
                        <div class="glance-name" style="font-size: small;"><%:gateway.Name%></div>
                        <div class="glance-name" style="font-size: small;"><%:locationMessage.LocationDate.OVToLocalDateTimeShort()%></div>
                        <div class="glance-reading" style="font-size: x-small;" title="<%:locationMessage.Latitude + "° / " +locationMessage.Longitude + "°"%>">Location: <%:Math.Round(locationMessage.Latitude,2) + "° / " + Math.Round(locationMessage.Longitude,2) + "°"%></div>
                    </div>
                </div>
                <div class="locationMsgExtra" id="locationMsgExtra_<%:locationMessage.LocationMessageGUID %>" style="display: none;">
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Altitude:</span> <%:locationMessage.Altitude%> m</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Speed:</span> <%:locationMessage.Speed%> km/h</div>
                    <%--<div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Course Over Ground:</span> <%:locationMessage.CourseOverGround%>°</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Fix Time:</span> <%:locationMessage.FixTime%> seconds</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Number of Satellites:</span> <%:locationMessage.NumberSatellites%></div>--%>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Accuracy:</span> <%:locationMessage.Uncertainty%> m</div>  
                    
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">View History:</span><a style="color: #007FEB;" href="/Overview/GatewayGPS/<%:locationMessage.DeviceID%>/?returnUrl=/Map/ViewMap/<%:visualMapID%>/&<%:todayLinkString%>">Today</a></div>  
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;"></span> <a style="color: #007FEB;" href="/Overview/GatewayGPS/<%:locationMessage.DeviceID%>/?returnUrl=/Map/ViewMap/<%:visualMapID%>/&<%:twoDayLinkString%>">2 days</a></div>  
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;"></span> <a style="color: #007FEB;" href="/Overview/GatewayGPS/<%:locationMessage.DeviceID%>/?returnUrl=/Map/ViewMap/<%:visualMapID%>/&<%:fiveDayLinkString%>">5 days</a></div>  
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;"></span> <a style="color: #007FEB;" href="/Overview/GatewayGPS/<%:locationMessage.DeviceID%>/?returnUrl=/Map/ViewMap/<%:visualMapID%>/&<%:sevenDayLinkString%>">7 days</a></div>  
                    
                </div>

            </a>
        </div>
  <%} %>
    <script>
        // Initialize and add the map
        function initMap() {
            map = new google.maps.Map(document.getElementById("map"), {
                streetViewControl: false,
                mapTypeId: "roadmap",
                mapTypeControl: false,
                fullscreenControl: false,
            });
            bounds = new google.maps.LatLngBounds();
            pathCoordinates = [];
            msgMarkers = new Array();
            markers = [];
            infoWindow = new google.maps.InfoWindow();

            //AddPoint(41.060, -111.960);
            //AddPoint(40.344, -111.800);
            //AddPoint(40.7, -111.8);
            <%:Html.Raw(addPointHtml)%>

            map.fitBounds(bounds);

            new markerClusterer.MarkerClusterer({ map, markers });
        }

        //map must be initialized before this is called
        function AddPoint(latitude, longitude, label, msgGuid) {
            let Point = { lat: latitude, lng: longitude };

            let marker = new google.maps.Marker({
                position: Point,
                map: map
            });
            marker.addListener("click", (function (marker, content, msgGuid) {

                return function () {
                    if ($('#locationMsgExtra_' + msgGuid).css('display') != 'none') {
                        infoWindow.close();
                        $('#locationMsgExtra_' + msgGuid).css('display', 'none');
                    } else {
                        marker.setAnimation(google.maps.Animation.BOUNCE);
                        setTimeout('msgMarkers["' + msgGuid + '"].setAnimation(null);', 1000);

                        infoWindow.setContent(content);
                        infoWindow.open(map, marker);

                        $('.locationMsgExtra').css('display', 'none');
                        $('#locationMsgExtra_' + msgGuid).css('display', 'block');
                        
                        $('#resultsList').animate({
                            scrollTop: ($('#resultsList').scrollTop() + $('.locationMsg[data-id=' + msgGuid + ']').position().top - 150)
                        }, 250);
                    }
                }
            })(marker, label, msgGuid));

            bounds.extend(Point);
            pathCoordinates.push(Point);
            msgMarkers[msgGuid] = marker;
            markers.push(marker);
        }

        Date.prototype.addDays = function (days) {
            var date = new Date(this.valueOf());
            date.setDate(date.getDate() + days);
            return date;
        }

        $(function () {
            if (map != null && map != undefined) {
                initMap();
            }

            $('.locationMsg').click(function () {
                var msgGuid = $(this).attr('data-id');

                var _marker = msgMarkers[msgGuid];
                google.maps.event.trigger(_marker, 'click');
            });
        });
</script>
<%}%>