<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<style>
    .turngreen {
        color: limegreen;
    }

    .turngrey {
        color: gray;
    }
</style>

<%
    bool ShowSensors = true;
    bool ShowGateways = true;
    switch (Model.NotificationClass)
    {
        case eNotificationClass.Application:
            ShowGateways = false;
            break;
        case eNotificationClass.Inactivity:
            break;
        case eNotificationClass.Low_Battery:
            break;
        case eNotificationClass.Advanced:
            AdvancedNotification adv = AdvancedNotification.Load(Model.AdvancedNotificationID);
            if (adv != null)
            {
                ShowSensors = adv.HasSensorList;
                ShowGateways = adv.HasGatewayList;
                //switch (adv.AdvancedNotificationType)
                //{
                //    case eAdvancedNotificationType.Everyone:
                //        break;
                //    case eAdvancedNotificationType.Premium:
                //        break;
                //    case eAdvancedNotificationType.AccountLevel:
                //        break;
                //    case eAdvancedNotificationType.Gateway:
                //        ShowSensors = false;
                //        break;
                //    case eAdvancedNotificationType.AutomatedSchedule:
                //        ShowSensors = false;
                //        ShowGateways = false;
                //        break;
                //    case eAdvancedNotificationType.SensorNonActivity:
                //        ShowGateways = false;
                //        break;
                //    default:
                //        break;
                //}
            }
            else
            {
                ShowSensors = false;
                ShowGateways = false;
            }
            break;
        case eNotificationClass.Credit:
            break;
        case eNotificationClass.Timed:
            ShowGateways = false;
            break;
        default:
            break;
    }
%>

<div class="formBody" style="margin-top: 20px;">
    <table width="100%">
        <%if (ShowSensors)
            { %>
        <tr>
            <td style="vertical-align: top;">
                <div style="border: 1px solid #ccc" class="notiTable">
                    <div class="blockSectionTitle">
                        <div class="blockTitle">Notification will be triggered by</div>
                        <div style="float: right; margin-right: 20px;" class="blockTitle">Filter by Network</div>
                        <div style="clear: both;"></div>
                        <div class="deviceSearch">
                            <div style="float: left;" class="searchInput">
                                <input id="sensorFilter" name="sensorFilter" type="text" /></div>
                            <div style="float: left;" class="searchButton">
                                <img src="../../Content/images/Notification/device-search.png" /></div>
                        </div>
                        <!-- deviceSearch -->


                        <%
                            List<CSNet> CSNetList = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
                            List<CSNet> list = (from n in CSNetList
                                                where (MonnitSession.CustomerCan("Network_View", n.CSNetID))
                                                orderby n.Name.Trim()
                                                select (n)).ToList();

                            if (list.Count > 1)
                            {%>
                        <div style="float: right; margin-right: 20px;">

                            <select id="networkSelectSensor" style="font-weight: normal;" value="All Networks">
                                <option value="-1" selected>All Networks</option>
                                <%foreach (CSNet Network in list)
                                    { %>
                                <option value='<%:Network.CSNetID%>'><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                                <% } %>
                            </select>
                        </div>
                        <% } %>

                        <div style="clear: both;"></div>
                    </div>
                    <div id="divSensorList" style="overflow-y: auto; padding: 10px;">
                    </div>
                </div>
            </td>



            <%} %>

            <%if (ShowSensors && ShowGateways)
                { %>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <%} %>

        <%if (ShowGateways)
            { %>
        <tr>
            <td style="vertical-align: top;">
                <div style="border: 1px solid #ccc" class="notiTable">
                    <div class="blockSectionTitle">
                        <div class="blockTitle">Notification will be triggered by</div>
                        <div style="float: right; margin-right: 20px;" class="blockTitle">Filter by Network</div>
                        <div style="clear: both;"></div>
                        <div class="deviceSearch">
                            <div style="float: left;" class="searchInput">
                                <input id="gatewayFilter" name="gatewayFilter" type="text" /></div>
                            <div style="float: left;" class="searchButton">
                                <img src="../../Content/images/Notification/device-search.png" /></div>
                        </div>
                        <!-- deviceSearch -->


                        <%
                            List<CSNet> CSNetList = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
                            List<CSNet> list = (from n in CSNetList
                                                where (MonnitSession.CustomerCan("Network_View", n.CSNetID))
                                                orderby n.Name.Trim()
                                                select (n)).ToList();

                            if (list.Count > 1)
                            {%>
                        <div style="float: right; margin-right: 20px;">

                            <select id="networkSelectGateway" style="font-weight: normal;" value="All Networks">
                                <option value="-1" selected>All Networks</option>
                                <%foreach (CSNet Network in list)
                                    { %>
                                <option value='<%:Network.CSNetID%>'><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                                <% } %>
                            </select>
                        </div>
                        <% } %>

                        <div style="clear: both;"></div>
                    </div>
                    <div id="divGatewayList" style="overflow-y: auto; padding: 10px;">
                    </div>
                </div>
            </td>

        </tr>


        <%} %>
    </table>
</div>

<script type="text/javascript">
    var sensorFilterTimeout = null;
    var gatewayFilterTimeout = null;
    $(document).ready(function () {
        loadSensors();
        loadGateways();


        $('#networkSelectSensor').change(function () {
            loadSensors();
        });

        $('#networkSelectGateway').change(function () {
            loadGateways();
        });

        $('#sensorFilter').watermark('Sensor Search', {
            left: 0,
            top: 0
        }).keyup(function () {
            if (sensorFilterTimeout != null)
                clearTimeout(sensorFilterTimeout);
            sensorFilterTimeout = setTimeout("loadSensors();", 1000);
        });

        $('#gatewayFilter').watermark('Gateway Search', {
            left: 0,
            top: 0
        }).keyup(function () {
            if (sensorFilterTimeout != null)
                clearTimeout(sensorFilterTimeout);
            sensorFilterTimeout = setTimeout("loadGateways();", 1000);
        });
    });



    function loadSensors() {
        $.get("/Notification/SentFromSensorList/<%:Model.NotificationID %>?q=" + $('#sensorFilter').val() + '&networkID=' + $('#networkSelectSensor').val(), function (data) {
            $('#divSensorList').html(data);
        });
    }

    function loadGateways() {
        $.get("/Notification/SentFromGatewayList/<%:Model.NotificationID %>?q=" + $('#gatewayFilter').val() + '&networkID=' + $('#networkSelectGateway').val(), function (data) {
            $('#divGatewayList').html(data);
        });
    }

    function toggleSensor(sensorID, datumindex) {
        var add = $('.notiSensor' + sensorID + '_' + datumindex).hasClass('inactive');
        var url = "/Notification/ToggleSensor/<%:Model.NotificationID %>";
        var params = "sensorID=" + sensorID;
        params += "&add=" + add;
        params += "&datumindex=" + datumindex;

        $.post(url, params, function (data) {
            if (data == "Success") {
                if (add)
                    $('.notiSensor' + sensorID + '_' + datumindex).removeClass('inactive').removeClass('icon-unchecked').addClass('active').addClass('icon-checked');
                else
                    $('.notiSensor' + sensorID + '_' + datumindex).removeClass('active').removeClass('icon-checked').addClass('inactive').addClass('icon-unchecked');
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }

    function toggleGateway(gatewayID) {
        var add = $('.notiGateway' + gatewayID).hasClass('inactive');
        var url = "/Notification/ToggleGateway/<%:Model.NotificationID %>";
        var params = "gatewayID=" + gatewayID;
        params += "&add=" + add;

        $.post(url, params, function (data) {
            if (data == "Success") {
                if (add)
                    $('.notiGateway' + gatewayID).removeClass('inactive').removeClass('icon-unchecked').addClass('active').addClass('icon-checked');
                else
                    $('.notiGateway' + gatewayID).removeClass('active').removeClass('icon-checked').addClass('inactive').addClass('icon-unchecked');
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }


</script>
