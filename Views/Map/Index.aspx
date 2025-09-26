<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<VisualMap>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix"></div>
    <style>
        .mapName {
            cursor: pointer;
        }
    </style>

    <div class="container-fluid">
        <div class="formtitle">
            <div id="MainTitle" style="display: none;"></div>
            <div class="top-add-btn-row media_desktop d-flex" id="hook-one" style="margin-top: 5px; justify-content: flex-end;">
                <a href="/Map/NewMap/" id="list" class="btn btn-primary">
                    <%=Html.GetThemedSVG("add") %>
                    &nbsp; 
                    <%: Html.TranslateTag("Map/NewMap|Add Map","Add Map")%>                
                </a>
            </div>
            <div class="bottom-add-btn-row media_mobile">
                <a class="btn add-btn-mobile" href="/Map/NewMap/">
                    <%=Html.GetThemedSVG("add") %>
                </a>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title dfac" style="height: 35px;">
                    <%=Html.GetThemedSVG("map") %>
                    &nbsp;
                    <%: Html.TranslateTag("Map/_MapLink|Maps List","Maps List")%>
                </div>
            </div>
            <div class="card_container__body">
                <div class="small-card_container">
                    <%//List<VisualMap> maps = (ViewData["VisualMapList"] as List<Monnit.VisualMap>);
                        //if (maps.Count > 0)
                        if (Model.Count > 0)
                        {%>
                    <%bool hasMapsAPIKey = !string.IsNullOrEmpty(MonnitSession.CurrentTheme.PropertyValue("Maps_API_Key"));
                        foreach (VisualMap map in Model)
                        {
                            bool isFavorite = MonnitSession.IsVisualMapFavorite(map.VisualMapID);
                    %>
                    <div class="small-list-card">
                        <div class="sensor corp-status sensorStatusOK "></div>
                        <a style="width: 100%; padding: 5px;" href="/Map/ViewMap/<%:map.VisualMapID %>">
                            <div class="d-flex align-items-center">
                                <div class="eventIcon_container col-2 align-items-center">
                                    <div class="icon-color" style="width: 100%; display: flex; justify-content: flex-end;">
                                        <%=Html.GetThemedSVG("static-map") %>
                                    </div>
                                </div>
                                <div class="glance-text d-flex col-8 align-items-center">
                                    <span class="glance-name" style="font-size: .9rem;"><%=map.Name%></span>
                                </div>
                                <div class="gatewayList_detail ">
                                    <div class="menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" style="margin-left: -10px;">
                                        <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex": "none" %>; align-items: start; justify-content: center;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
                                            <div class="listOfFav"><%= Html.GetThemedSVG("heart-beat")  %></div>
                                        </div>
                                        <%=Html.GetThemedSVG("menu") %>
                                    </div>
                                    <div class="dropdown-menu ddm" style="padding: 0;">
                                        <ul class="ps-0 mb-0">
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/ViewMap/<%:map.VisualMapID%>'">
                                                    <%: Html.TranslateTag("View","View")%>
                                                    <%=Html.GetThemedSVG("view") %>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/EditMap/<%:map.VisualMapID %>'">
                                                    <%: Html.TranslateTag("Edit","Edit")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("edit") %>
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/DevicesToShow/<%:map.VisualMapID %>'">
                                                    <%: Html.TranslateTag("Devices","Devices")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("gps-pin") %>
                                                    </span>
                                                </div>
                                            </li>
                                            <hr class="my-0">
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault();removeMap('<%=map.VisualMapID %>')">
                                                    <%: Html.TranslateTag("Delete","Delete")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("delete") %>
                                                    </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>


                    <%if (map.MapType == eMapType.GpsMap && hasMapsAPIKey)
                        {%>
                    <div class="small-list-card">
                        <div class="sensor corp-status sensorStatusOK "></div>
                        <a style="width: 100%; padding: 5px;" href="/Map/ViewMap/<%:map.VisualMapID %>">
                            <div class="d-flex align-items-center">
                                <div class="eventIcon_container col-2 align-items-center">
                                    <div class="icon-color" style="width: 100%; display: flex; justify-content: flex-end;">
                                        <%=Html.GetThemedSVG("gps-pin") %>
                                    </div>
                                </div>
                                <div class="glance-text d-flex col-8 align-items-center">
                                    <span class="glance-name" style="font-size: .9rem;"><%=map.Name%></span>
                                </div>
                                <div class="gatewayList_detail ">
                                    <div class="menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" style="margin-left: -10px;">
                                        <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex": "none" %>; align-items: start; justify-content: center;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
                                            <div class="listOfFav"><%= Html.GetThemedSVG("heart-beat")  %></div>
                                        </div>
                                        <%=Html.GetThemedSVG("menu") %>
                                    </div>
                                    <div class="dropdown-menu ddm" style="padding: 0;">
                                        <ul class="ps-0 mb-0">
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/ViewMap/<%:map.VisualMapID%>'">
                                                    <%: Html.TranslateTag("View", "View")%>
                                                    <%=Html.GetThemedSVG("view") %>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/EditMap/<%:map.VisualMapID %>'">
                                                    <%: Html.TranslateTag("Edit", "Edit")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("edit") %>
                                                    </span>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/DevicesToShow/<%:map.VisualMapID %>'">
                                                    <%: Html.TranslateTag("Devices", "Devices")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("gps-pin") %>
                                                    </span>
                                                </div>
                                            </li>
                                            <hr class="my-0">
                                            <li>
                                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault();removeMap('<%=map.VisualMapID %>')">
                                                    <%: Html.TranslateTag("Delete", "Delete")%>
                                                    <span>
                                                        <%=Html.GetThemedSVG("delete") %>
                                                    </span>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <% }%> 


                    <%}%>
                    <%}%>
                    <%else
                        { %>
                    <h2><%: Html.TranslateTag("Map/Index|No maps currently created. Click \"Add Map \" to get started.","No maps currently created. Click \"Add Map\" to get started.")%></h2>
                    <%} %>
                </div>
            </div>
        </div>
    </div>

    <script>
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        function removeMap(map) {
            let values = {};
            values.url = `/Map/DeleteMap/${map}`;
            values.text = 'Are you sure you want to remove this map?';
            openConfirm(values);
        }

        $(document).ready(function () {
            $(".listOfFav svg").addClass("liked");
        });
    </script>
</asp:Content>
