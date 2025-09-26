<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<div class="rule-card_container w-100" style="max-height: 50vh!important;">
    <div class="trigger-device__top">
        <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px;">
            <div class="card_container__top__title">
                <span class="me-2"><%=Html.GetThemedSVG("gateway") %></span>
                <%: Html.TranslateTag("Map/_GatewaysListWithFilters|Gateways to Display on Map", "Gateways to Display on Map")%>
            </div>
            <div class="clearfix"></div>
        </div>

        <%--        <div id="filters_gateway">
            <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                <font color="gray">
                        <%: Html.TranslateTag("Click device to enable/disable","Click device to enable/disable")%>
                </font>
                <a style="cursor:pointer;" onclick="toggleGatewayFilters(this)">
                    <%=Html.GetThemedSVG("filter") %>
                </a>
            </div>
            <div id="gatewayFiltersPartial" style="display: none;">
                <%=Html.Action("GatewayFilters", "Map", Model.VisualMapID) %>
            </div>
        </div>--%>

        <div id="devices_gateway">
            <div class="trigger-device__top">
                <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                    <font color="gray">
                        <%: Html.TranslateTag("Map/_GatewaysListWithFilters|Click device to enable/disable","Click device to enable/disable")%>
                    </font>
                    <a id="gatewayFiltersToggle" style="cursor: pointer;" onclick="toggleGatewayFilters()">
                        <%=Html.GetThemedSVG("filter") %>
                    </a>
                </div>
            </div>
            <div id="gatewayFiltersPartial" style="display: none;">
                <%=Html.Action("GatewayFilters", "Map", Model.VisualMapID) %>
            </div>

            <div id="filteredGatewaysPartial" style="display: none;">
                <%=Html.Action("FilteredGateways", "Map", Model.VisualMapID) %>
            </div>
        </div>
    </div>
</div>
<script>
    // pointing up (inverted) is transform="scale(1, -1)" data('rotation')=1 so that 
    // oneRotation = 1 and scale(-1, --1)
    $(document).ready(function () {
        toggleGateways();
    })

    //let tglSnsrFltrs = -1;
    //let tglSnsrs = -1;

    function toggleGatewayFilters(el) {
        //$(el).children('#svg_filter').first().attr('transform', `scale(${-tglSnsrFltrs}, ${tglSnsrFltrs})`);
        //tglSnsrFltrs = -tglSnsrFltrs;
        $('#gatewayFiltersPartial').toggle();
        return false;
    }
    function toggleGateways(el) {
        //$(el).children('#svg_filter').first().attr('transform', `scale(${-tglSnsrs}, ${tglSnsrs})`);
        //tglSnsrs = -tglSnsrs;
        $('#filteredGatewaysPartial').toggle();
        return false;
    }
</script>
