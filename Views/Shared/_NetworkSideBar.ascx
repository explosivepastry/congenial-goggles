<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
    string selection = ViewBag.LeftNavSelection;

    if (CSNetList.Count > 1)
    {%>
<div id="sidenav-secondary" class="secondary-sidenav secondary_sidenav">
    <div class="formtitle">
        <div id="MainTitle" style="display: none;"></div>
        <div style="height: 20px">
            <div class="sensorList-networkContainer verticalScroll" id="secondaryBar" style="padding-right: 2px !important;">
                <ul id="networkSelect" class="network-sidebar">
                    <div class="network-sidebar__title dfjcsbac">
                        <span>Networks</span>
                    </div>

                    <%if (selection == "Sensor")
                        { %>
                    <li class="dfac networkListItem"><a style="width: 90%;" class="networkList_link" href='/Overview/SensorIndex/?id=-1&view=List'><%: Html.TranslateTag("All Networks","All Networks")%>
                        <%}
                            else if (selection == "Gateway")
                            { %>
                    <li class="dfac networkListItem"><a style="width: 90%;" class="networkList_link" href='/Overview/GatewayIndex/?id=-1&view=List'><%: Html.TranslateTag("All Networks","All Networks")%>
                        <%}%>
                    </a>
                    </li>
                    <%foreach (CSNet Network in CSNetList)
                        { %>

                    <%if (selection == "Sensor")
                        { %>
                    <li class="dfac networkListItem"><a id="net_<%:Network.CSNetID%>" style="width: 90%;" class="networkList_link" href='/Overview/SensorIndex/?id=<%:Network.CSNetID%>&view=List' <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected autofocus" : "" %>><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></a></li>
                        <%}
                            else if (selection == "Gateway")
                            { %>
                    <li class="dfac networkListItem"><a id="net_<%:Network.CSNetID%>" style="width: 90%;" class="networkList_link" href='/Overview/GatewayIndex/?id=<%:Network.CSNetID%>&view=List' <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected autofocus" : "" %>><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></a></li>
                        <%}%>
                    <% } %>
                </ul>
            </div>
        </div>
    </div>
</div>
<% } %>

<script>
    var noText = document.getElementsByClassName("mobileMenuTitle");
    var navWidth = document.getElementById("sidenav-secondary");
    var networkCount = <%=CSNetList.Count()%>;
    var networkID = Number('<%=MonnitSession.SensorListFilters.CSNetID%>');

    $(document).ready(function () {
       
        //if (networkID > 0) {
            
        //    $('#net_' + networkID).focus();
        //}
    });
    if (window.location.href.indexOf("/Overview/SensorIndex") > -1) {
        if (networkCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".inner_leftBar").addClass("sidenav-secondary50");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
        }
    }

    if (window.location.href.indexOf("/Overview/GatewayIndex") > -1) {
        if (networkCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
            $(".inner_leftBar").addClass("sidenav-secondary50");
        }
    }
    //if (window.location.href.indexOf("/Overview/GatewayHome") > -1) {
    //    if (gatewayCount > 1) {
    //        $("#sideNav").addClass("main_leftBar__active");
    //        $(".secondary-sidenav").addClass("sidenav-secondary50");
    //        $(".inner_leftBar").addClass("sidenav-secondary50");
    //    }
    //}
</script>
