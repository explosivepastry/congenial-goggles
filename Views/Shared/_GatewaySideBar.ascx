<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% 
    List<Gateway> GatewayList = Gateway.LoadByCSNetID(Model);
    GatewayList = GatewayList.Where(g => g.GatewayTypeID != 11).ToList();
    CSNet network = CSNet.Load(Model);

    string urlparameter = (string)ViewContext.RouteData.Values["id"];
    long parameterid = urlparameter.ToLong();

    if (GatewayList.Count > 1)
    {%>
<div id="sidenav-secondary" class="secondary-sidenav secondary_sidenav">
    <div class="formtitle">
        <div id="MainTitle" style="display: none;"></div>
        <div style="height: 20px">
            <div class="sensorList-networkContainer" id="hook-one" style="padding-right: 2px !important;">
                <ul id="networkSelect" class="network-sidebar">
                    <div class="network-sidebar__title"><%:network.Name %></div>

                    <%foreach (Gateway g in GatewayList)
                        { %>
                    <%--                    <li><a class="networkList_link" href='/Overview/GatewayHome/?id=<%:g.GatewayID%>&view=List' <%:g.GatewayID == parameterid ? "selected=selected" : "" %>><%=g.Name.Length > 0 ? g.Name : g.GatewayID.ToString() %>--%>
                    <li>
                        <a style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; display: block;" class="networkList_link" href='/Overview/GatewayHome/<%:g.GatewayID%>' <%:g.GatewayID == parameterid ? "selected=selected" : "" %>><%=g.Name.Length > 0 ? g.Name : g.GatewayID.ToString() %>
                    </a>

                    </li>
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
<%--    var sensorCount = <%=SensorList.Count()%>;--%>
    var gatewayCount = <%=GatewayList.Count()%>;
<%--    var networkCount = <%=CSNetList.Count()%>;--%>

    $(document).ready(function () {
        if (window.location.href.indexOf("/Overview/SensorIndex") > -1) {
            if (networkCount > 1) {
                $("#sideNav").addClass("main_leftBar__active");
                $(".inner_leftBar").addClass("sidenav-secondary50");
                $(".secondary-sidenav").addClass("sidenav-secondary50");
            }
        }


        if (window.location.href.indexOf("/Overview/GatewayHome") > -1) {
            if (gatewayCount > 1) {
                $("#sideNav").addClass("main_leftBar__active");
                $(".secondary-sidenav").addClass("sidenav-secondary50");
                $(".inner_leftBar").addClass("sidenav-secondary50");
            }
        }
    });
</script>
