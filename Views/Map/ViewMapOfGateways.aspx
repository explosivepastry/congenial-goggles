<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<LocationMessage>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewMapOfGateways
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Set the size of the div element that contains the map */
        #map {
            height: 100%;
            /* The height is 400 pixels */
            width: 100%;
            /* The width is the width of the web page */
        }
    </style>

    <%
        long visualMapID = ViewData["VisualMapID"].ToLong();
        VisualMap tempMap = VisualMap.Load(visualMapID);

        List<Gateway> gatewayList = ViewData["GatewayList"] as List<Gateway>;
        string googleMapApiKey = MonnitSession.CurrentTheme.PropertyValue("Maps_API_Key");

        bool isFavorite = MonnitSession.IsVisualMapFavorite(visualMapID);
        string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
        string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
    %>
    <div class="container-fluid">
        <div class="my-4">
            <%Html.RenderPartial("_MapLink", tempMap); %>
        </div>
        <!-- Map View -->
        <div class="x_panel shadow-sm rounded px-0">
            <div class="x_content">
                <div class="two-thirdSection row">
                    <div class="blockSectionTitle col-12">
                        <div class="card_container__top">
                            <div class="card_container__top__title d-flex justify-content-between" style="overflow: unset;">

                                <div class="col-md-4 col-12">
                                    <%=Html.GetThemedSVG("gps-pin") %>
                                    <%:tempMap.Name %>
                                    <span style="padding: 5px 15px;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" class="help-hover d-none d-lg-inline-block">
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
                                                    Location Current
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="event.preventDefault();">
                                                    <div class="eventIcon_container">
                                                        <div class="eventIcon eventIconStatus sensorStatusWarning"></div>
                                                        <%=Html.GetThemedSVG("gps-pin") %>
                                                    </div>
                                                    Location Dated
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_item py-2 px-3" onclick="event.preventDefault();">
                                                    <div class="eventIcon_container">
                                                        <div class="eventIcon eventIconStatus sensorStatusInactive"></div>
                                                        <%=Html.GetThemedSVG("gps-pin") %>
                                                    </div>
                                                    Location Error
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

                                <div id="refreshBtn" class="col-md-8 col-12" style="text-align: right;">
                                    <%=Html.GetThemedSVG("refresh") %>

                                    <span title="<%:isFavorite ? "Unfavorite Rule" : "Favorite Rule" %>" id="favoriteItem" class="ps-2" data-id="<%=visualMapID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                                        onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'visualmap')">
                                        <%:Html.GetThemedSVG("heart-beat") %>                
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div id="resultsList" class="col-4 d-none d-lg-block" style="overflow-y: auto;">
                        <%: Html.Partial("~/Views/Map/_MapGatewaysResults.ascx", Model) %>
                    </div>
                    <div class="col-lg-8 col-12" style="height: 500px">
                        <!-- This is the container that holds the map -->
                        <div id="map">
                            <div id="loadingGIF" class="text-center" style="display: none;">
                                <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                    <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix" />
                </div>
            </div>
        </div>
    </div>
    <% %>
    <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=<%:googleMapApiKey %>&callback=initMap&v=weekly" defer></script>
    <script>
        let map = null;
        let bounds = null;
        let pathCoordinates = null;
        let msgMarkers = null;
        let markers = null;
        let activeMarker = null;
        let infoWindow = null;

        $(function () {
            $('#refreshBtn').click(function () {
                var obj = $('#resultsList');
                obj.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                $.get('/Map/MapGatewaysResults/<%:visualMapID%>', function (data) {
                    obj.html(data);
                });
            });

            isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
        });
    </script>
</asp:Content>
