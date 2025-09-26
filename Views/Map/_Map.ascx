<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<%--<%VisualMap map = VisualMap.Load(Model.VisualMapID);%>--%>
<%
    bool isFavorite = MonnitSession.IsVisualMapFavorite(Model.VisualMapID);
    string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
    string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
%>
<div class="two-thirdSection">
    <div class="blockSectionTitle">
        <div class="card_container__top mobile-top">
            <div class="card_container__top__title">
                <%=Html.GetThemedSVG("static-map") %>&nbsp;
                <%=Model.Name %>
            </div>
            <div class="ruleSelectOptionsTop" style="cursor:pointer">                
                <span title="<%:isFavorite ? "Unfavorite Rule" : "Favorite Rule" %>" id="favoriteItem" data-id="<%=Model.VisualMapID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                    onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'visualmap')">
                    <%:Html.GetThemedSVG("heart-beat") %>                
                </span>

                <a title="Delete" onclick="event.preventDefault();removeMap('<%=Model.VisualMapID %>')">
                    <span>
                        <%=Html.GetThemedSVG("delete") %>
                    </span>
                </a>
				<a class="docsBtn btn btn-primary btn-sm text-nowrap" href="/Map/LoadSnapshot/<%:Model.VisualMapID%>" onclick="loadSnapshot(this); return false;">
		            <%: Html.TranslateTag("Map/_Map|Load Data Snapshot","Load Data Snapshot")%>
				</a>
            </div>
        </div>
    </div>
    <div class="x_content" id="snapshotHolder"  style="margin-top:-25px;margin-bottom:-25px;width:100%""></div>
    <div class="clearfix"></div>
    <div style="margin-top: 25px; margin-bottom: 5px;">
        <div id="MainRefresh"></div>
        <div style="color: Black; font-size: 10pt; text-align: right; padding-right: 55px; margin-top: -17px;"></div>             
    </div>
    <!-- Set by the VisualAddonOns, but I'm not sure what this does yet? Possibly holds overlay images? -->
       
</div>
<!-- This is the container that holds the map -->

<div class="two-thirdSectionInside" >
    <div id="container" width="100%" style="z-index: -1;border:none;border-radius:8px;"/>
    <div id="overlayTooltip" style="display: none;"/>        
</div>
<div class="clearfix"/>

<style type="text/css">
    #container {
        height: 700px;
        border: 1px solid black;
        color: white; /* for error messages, etc. */
    }

    #output {
        width: 500px;
        border: none;
        margin: 1em 0em;
    }

        #output td {
            width: 33%;
        }

        #output .outputLabel {
            font-weight: bold;
        }

    .crossHair {
        cursor: crosshair;
    }

    #tiptip_holder {
        z-index: 100000000;
    }

    .tip {
        opacity: 1 !important;
        font-family: Arial;
        padding: 4px 8px;
        border: 1px solid rgba(255,255,255,0.25);
        background-color: rgb(238,238,238);
        background-image: -webkit-linear-gradient(top, #eeeeee 0%,#eeeeee 100%);
        -webkit-box-shadow: 0 0 3px #555;
        -moz-box-shadow: 0 0 3px #555;
        z-index: 10000;
    }
</style>

<script src="../../Scripts/OpenSeaDragon/openseadragon.js" type="text/javascript"></script>

<script type="text/javascript">      

    function ClearAddOns() {
        if (snapshotLoaded) {
            loadSnapshot();
        }
    }

    function loadSnapshot(anchor) {
        if (snapshotLoaded) {
            snapshotLoaded = false;
            $('#overlayTooltip').children().each(function () {
                $(this).hide();
            });
        }
        else {
            snapshotLoaded = true
            $('.mapIcon').each(function () {
                loadDeviceData(this);
            });
            //$.get($(anchor).attr("href"), function (data) {
            //    $('#overlayTooltip').html(data);            
            //    $('#overlayTooltip').children().each(function () {                      
            //        var sid = $(this).attr("id");              
            //        var icon = $('#' + sid);                

            //        if (icon.length > 0) {                    
            //            var tipx = $(this).position().left,
            //                tipy = $(this).position().top;
            //        }
            //    });
            //});
        }
    }
    var vmid = <%:Model.VisualMapID %>;
    var sd_init_width = <%:Model.Width %>;
    var sd_init_height = <%:Model.Height %>;

    function addOverlays() {
        var surface = viewer.element;
        OpenSeadragon.addEvent(surface, "mousedown", onMouseDown);
        OpenSeadragon.addEvent(surface, "mouseup", onMouseUp);
        OpenSeadragon.addEvent(surface, "mousewheel", onMouseWheel);
        if (!snapshotLoaded) {
            <%Html.RenderPartial("_MapOverlay", Model); %>
        }
        if (typeof setCSSSize !== "function") setTimeout('if(typeof setCSSSize === "function") setCSSSize();', 10);
    }


    $(function () {
        isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
    });

    function removeMap(map) {
        let values = {};
        values.url = `/Map/DeleteMap/${map}`;
        values.text = 'Are you sure you want to remove this map?';
        values.redirect = "/Map/Index";
        openConfirm(values);
    }

</script>
