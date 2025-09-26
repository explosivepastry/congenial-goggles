<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<% 
    List<Sensor> SensorList = Sensor.LoadByCsNetID(Model);
    CSNet network = CSNet.Load(Model);

    string urlparameter = (string)ViewContext.RouteData.Values["id"];
    long parameterid = urlparameter.ToLong();

    if (SensorList.Count > 1)
    {%>
<div id="sidenav-secondary" class="secondary-sidenav secondary_sidenav">
    <div class="formtitle">
        <div id="MainTitle" style="display: none;"></div>
        <div style="height: 20px">
            <div class="sensorList-networkContainer" id="hook-one" style="padding-right: 2px !important;">

                <ul id="networkSelect" class="network-sidebar">
                    <div class="network-sidebar__title"><%:network.Name %></div>
                    <%foreach (Sensor s in SensorList)
                        { %>
                    <li><a style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; display: block;" class="networkList_link" href='/Overview/SensorChart/<%:s.SensorID%>' <%:s.SensorID == parameterid ? "selected=selected" : "" %>>

                        <%=s.SensorName.Length > 0 ? s.SensorName : s.SensorID.ToString() %>
                    </a></li>
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
    var sensorCount = <%=SensorList.Count()%>;

    if (window.location.href.indexOf("/Overview/SensorIndex") > -1) {
        if (sensorCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".inner_leftBar").addClass("sidenav-secondary50");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
        }
    }
    if (window.location.href.indexOf("/Overview/SensorChart") > -1) {
        if (sensorCount > 1) {
            $("#sideNav").addClass("main_leftBar__active");
            $(".secondary-sidenav").addClass("sidenav-secondary50");
            $(".inner_leftBar").addClass("sidenav-secondary50");
        }
    }

</script>
