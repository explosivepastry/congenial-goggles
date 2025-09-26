<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<LocationMessage>>" %>

<%
string addPointHtml = "";
if (Model == null || Model.Count == 0)
  {%> 
        <h3>No results</h3>
  <%}
      else
      {

            List<HistoryPinModel> Pins = new List<HistoryPinModel>();

            HistoryPinModel CurrentPin = null;
            foreach (LocationMessage locationMessage in Model)
            {
                //locationMessage at least 50 meters away from the last pin  OR  more than an hour betwen readings
                if (CurrentPin != null && (LocationMessage.Distance(CurrentPin.LastMessage, locationMessage) > 0.05 || CurrentPin.LastMessage.LocationDate.Subtract(locationMessage.LocationDate).TotalMinutes > 59))
                {
                    Pins.Add(CurrentPin);
                    CurrentPin = null;
                }

                if (CurrentPin == null)
                {
                    CurrentPin = new HistoryPinModel();
                    CurrentPin.Status = locationMessage.isValid ? "OK" : "Inactive";
                    CurrentPin.LastMessage = locationMessage;
                    CurrentPin.Reports = 0;
                }
                CurrentPin.FirstMessage = locationMessage;
                CurrentPin.Reports++;

            }

            if (CurrentPin != null)
            {
                //Add the last if it wasn't already added
                Pins.Add(CurrentPin);
                CurrentPin = null;
            }



          foreach (HistoryPinModel pinModel in Pins)
          {
              string timeString = pinModel.FirstMessage.LocationDate.OVToLocalDateTimeShort();
              if (pinModel.FirstMessage.LocationDate != pinModel.LastMessage.LocationDate)
              {
                  timeString += " - " + pinModel.LastMessage.LocationDate.OVToLocalDateTimeShort();
              }
              if(pinModel.LastMessage.isValid)//only show valid pins on Map
              {


                  string markerTitle = "<b>" + timeString + "</b>"
                                     + " <br/> " + Math.Round(pinModel.LastMessage.Latitude,2) + "°/" + Math.Round(pinModel.LastMessage.Longitude,2) + "°"
                                     + " <div class=\"d-block d-lg-none\">"
                                     + "       <b>Altitude:</b> " + pinModel.LastMessage.Altitude + " m"
                                     + " <br/> <b>Speed:</b> " + pinModel.LastMessage.Speed + " km/h"
                                     //+ " <br/> <b>Course Over Ground:</b> " + pinModel.LastMessage.CourseOverGround + "°"
                                     //+ " <br/> <b>Fix Time:</b> " + pinModel.LastMessage.FixTime + " seconds"
                                     //+ " <br/> <b>Number of Satellites:</b> " + pinModel.LastMessage.NumberSatellites
                                     + " <br/> <b>Accuracy:</b> " + pinModel.LastMessage.Uncertainty + " m"
                                     + " <br/> <b>Reports:</b> " + pinModel.Reports
                                     + "</div>";
                  addPointHtml += "AddPoint(" + pinModel.LastMessage.Latitude + ", " + pinModel.LastMessage.Longitude + ", \'" + markerTitle + "\', \'" + pinModel.LastMessage.LocationMessageGUID + "\');\n";

              }
              %>
              
               
        <div class="locationMsg shadow-sm rounded d-flex align-items-center m-1" style="cursor: pointer; padding:15px 0px"  data-id="<%:pinModel.LastMessage.LocationMessageGUID %>">
            <a style="width: 100%;">
                <div class="d-flex align-items-center">
                    <div class="divCellCenter">
                        <div class="eventIcon_container">
                            <div class="eventIcon eventIconStatus sensorStatus<%:pinModel.Status %>"></div><%-- sensorStatusOK sensorStatusWarning sensorStatusAlert sensorStatusInactive --%>
                            <%=Html.GetThemedSVG("gps-pin") %>
                        </div>
                    </div>
                    <div class="glance-text">
                        <div class="glance-name" style="font-size: small;"><%:timeString%></div>
                        <div class="glance-reading" style="font-size: x-small;" title="<%:pinModel.LastMessage.Latitude + "° / " + pinModel.LastMessage.Longitude + "°"%>">Location: <%:Math.Round(pinModel.LastMessage.Latitude,2) + "° / " + Math.Round(pinModel.LastMessage.Longitude,2) + "°"%></div>
                    </div>
                </div>
                <div class="locationMsgExtra" id="locationMsgExtra_<%:pinModel.LastMessage.LocationMessageGUID %>" style="display: none;">
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Altitude:</span> <%:pinModel.LastMessage.Altitude%> m</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Speed:</span> <%:pinModel.LastMessage.Speed%> km/h</div>
                    <%--<div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Course Over Ground:</span> <%:pinModel.LastMessage.CourseOverGround%>°</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Fix Time:</span> <%:pinModel.LastMessage.FixTime%> seconds</div>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Number of Satellites:</span> <%:pinModel.LastMessage.NumberSatellites%></div>--%>
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Accuracy:</span> <%:pinModel.LastMessage.Uncertainty%> m</div>                    
                    <div class="ms-3" style="font-size: small;"><span style="display: inline-block; width:125px;">Reports:</span> <%:pinModel.Reports%></div>                    
                </div>

            </a>
        </div>
  <%} %>
<%}%>
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

            <%:Html.Raw(addPointHtml)%>

            map.fitBounds(bounds);

            let polyline = new google.maps.Polyline({
                map: map,
                path: pathCoordinates,
                strokeColor: "#00FFFF",
                strokeOpacity: 1.0,
                strokeWeight: 3
            });

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

        $(function () {

            if (map != null && map != undefined) {
                initMap();
            }

            $('.locationMsg').click(function () {
                var msgGuid = $(this).attr('data-id');

                var _marker = msgMarkers[msgGuid];
                google.maps.event.trigger(_marker, 'click');
            })

            <%Gateway gateway = ViewData["Gateway"] as Gateway;
            if (gateway != null && Model.Count > 0 && gateway.LastLocationDate == Model[0].LocationDate) { %>
            //Auto activate top message if it is the "current location"
            $('.locationMsg').first().click();
            <%}%>
            
        });

    </script>