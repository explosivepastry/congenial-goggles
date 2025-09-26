<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.VisualMap>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewMap
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Content/jquery.contextmenu.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/OpenSeaDragon/openseadragon.js" type="text/javascript"></script>

    <%--    <%VisualMap map = VisualMap.Load(Model.VisualMapID);%>--%>
    <div class="container-fluid">
        <div class="clearfix"></div>

        <div class="my-4">
            <%Html.RenderPartial("_MapLink", Model); %>
        </div>


        <!-- Map View -->
        <div class="col-12">
            <div class="x_panel shadow-sm rounded px-0">
                <div class="x_content" id="mapLoad">
                    <%:Html.Partial("_Map", Model) %>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <!-- Sensor Details View -->

        <div id="iconMenu">
            <div class="dropdown-menu shadow dropdown-menu-sm py-0" id="context-menu">
                <a class="dropdown-item py-2" id="viewDetails" href="#">View Details</a>
            </div>
        </div>

        <%--        <div class="col-12" hidden="hidden">
            <div class="x_panel shadow-sm">
                <div class="x_content">
                    <%:Html.Partial("~/Views/Map/_Map.ascx", Model) %>
                </div>
            </div>
        </div>--%>
        <div class="clearfix"></div>

    </div>

    <script src="../../Scripts/OpenSeaDragon/VisualOverview.js" type="text/javascript"></script>
    <script src="../../Scripts/OpenSeaDragon/MapView.js" type="text/javascript"></script>

    <script>

        $('#mapSelect').change(function () {
            var v = document.getElementById("mapSelect");
            window.location.href = "/Map/ViewMap/" + v.value;
        });

    </script>

    <style>
        #svg_cardList g {
            stroke: #FFF !important;
        }

        .mapIcon > #tower1 {
            width: 50%;
        }

        .mapIcon {
            display: flex !important;
            justify-content: center !important;
        }

            .mapIcon > svg {
                fill: #515356;
                width: 60%;
                height: auto;
            }

                .mapIcon > svg path {
                    fill: #515356;
                }

            .mapIcon > #ether-icon {
                width: 50% !important;
            }

            .mapIcon > .usb-icon-main {
                width: 59% !important;
            }

            .mapIcon > .thermo-icon-main {
                width: 50% !important
            }
    </style>

</asp:Content>
