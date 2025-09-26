<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<table width="100%">
    <tr>
        <td style="vertical-align:top;" width="46%">
            <div class="tableLeft" style="max-height:245px;">
                <div class="blockSectionTitle">
                    <div class="blockTitle"> User Created Notifications
                        <%
                          List<eDatumType> sensorDatumTypes = null;  
                          List<eDatumStruct> sensorDatumStructs = null;
                          if(ViewBag.Sensor !=null) {
                            Sensor sens = ViewBag.Sensor;
                            sensorDatumTypes = MonnitApplicationBase.GetDatumTypes(sens.ApplicationID);
                            sensorDatumStructs = new List<eDatumStruct>(from di in Enumerable.Range(0,sensorDatumTypes.Count) select new eDatumStruct(sens, di));
                            if(sensorDatumStructs.Count > 1) {
                            %>
                            <%: Html.DropDownList("datumindex", new SelectList(sensorDatumStructs, "datumindex", "customname"), new Dictionary<string, int>()) %>
                            <%} else { %>
                            <%: Html.Hidden("datumindex", sensorDatumStructs[0].datumindex) %>
                            <%} %>
                        <%} %>
                        <div class="notificationSearch" style="display: none;">
                        <div class="searchInput"><input id="notificationFilter" name="notificationFilter" type="text" /></div>
                        <div class="searchButton"><img src="../../Content/images/Notification/device-search.png" /></div>
                        </div> <!-- deviceSearch -->
					</div>
                    <div style="clear: both;"></div>
                </div>
                <div id="divNotificationList" style="max-height:200px; overflow-y:auto;">
                
                </div>
            </div>
        </td>
        <td style="vertical-align:top;" align="center" width="8%">&nbsp;
            
        </td>
        <td style="vertical-align:top;" width="46%">
            <div class="tableRight" style="max-height:245px;">
                <%if(ViewBag.Sensor != null){ %>
                <div class="blockSectionTitle">
                    <div class="blockTitle">
                        Default Notification Templates
					</div>
                    <div style="clear: both;"></div>
                </div>
                <div style="max-height:200px; overflow-y:auto;">
                    <table border="0" style="margin: 10px">
                    <%int index = 0;
                        foreach (Notification item in ViewBag.Sensor.DefaultNotifications()) { 
                          %>
                        <tr id="Tr1">
                            <td width="20">
                                <input type="checkbox" id="predefinedNotification_<%:index %>" />
                            </td>
                            <td>
                                <label for="predefinedNotification_<%:index %>"><img src="<%: Html.GetThemedContent(string.Format("/images/notification/class-{0}.png",item.NotificationClass))%>" class="typeIcon" alt="<%:item.NotificationClass %> Notification" /></label>
                            </td>
                            <td>
                                <label for="predefinedNotification_<%:index %>"><%: item.Name%></label>
                            </td>
                        </tr>
                    <% index++;
                    } %>
                    </table>
                </div>
                <%} %>
            </div>
        </td>
    </tr>
    
    <tr>
        <td style="vertical-align:top;" align="center">
            <div id="addNotifications">
                <a href="Add" onclick="addNotification(); return false;" class="colorbutton addbuttonDN"><img src="/content/images/notification/down-arrows.png" class="" /></a><br />
            </div>
        </td>
        <td>&nbsp;</td>
        <td style="vertical-align:top;" align="center">
            <div >
                <%if(ViewBag.Sensor != null){ %><a href="Add" onclick="addNotificationTemplate(); return false;" class="colorbutton addbuttonDN"><img src="/content/images/notification/down-arrows.png" class="" /></a><br /> <%} %>
            </div>
        </td>
    </tr>
   
</table>
    

<script type="text/javascript">
    var notificationFilterTimeout = null;
    $(document).ready(function () {
        loadNotifications();

        $('#datumindex').width(175);

        $('#notificationFilter').watermark('Notification Search', {
            left: 0,
            top: 0
        }).keyup(function () {
            if (notificationFilterTimeout != null)
                clearTimeout(notificationFilterTimeout);
            notificationFilterTimeout = setTimeout("loadNotifications();", 1000);
        });
    });

    $('#datumindex').change(function () {
        loadNotifications();
    });

    function loadNotifications() {
        var url = "/Notification/ExistingNotificationList/?q=" + $('#notificationFilter').val();
        <%if(ViewBag.Sensor != null){%>
        url += "&sensorID=<%:ViewBag.Sensor.SensorID %>";
        <%}%>
        <%if(ViewBag.Gateway != null){%>
        url += "&gatewayID=<%:ViewBag.Gateway.GatewayID %>";
        <%}%>
        url += "&datumindex=" + $('#datumindex').val();
        $.get(url, function (data) {
            $('#divNotificationList').html(data);
        });
    }

    function addNotification() {
        var url = "/Notification/AddExistingNotification";
        var params = "";
        <%if(ViewBag.Sensor != null){%>
        params += "sensorID=<%:ViewBag.Sensor.SensorID %>";
        <%}%>
        <%if(ViewBag.Gateway != null){%>
        params += "gatewayID=<%:ViewBag.Gateway.GatewayID %>";
        <%}%>
        var notiChecked = false;
        $("input:checked").each(function () {
            var id = $(this).attr("id");
            if (id && id.indexOf("notificationID_") == 0)//make sure it is from the correct list
            {
                id = id.replace("notificationID_", "");
                params += "&notificationIDs=" + id;
                if($('#datumindex').val() != null) {
                    //alert($('#datumindex').val());
                    params += "&datumindex=" + $('#datumindex').val();
                }
                notiChecked = true;
            }
        });

        if (notiChecked == false) {
            showSimpleMessageModal("<%=Html.TranslateTag("You must select at least one notification to add.")%>");
            return;
        }

        $.post(url, params, function (data) {
            $('#assignedNotificationTable').html(data);
            loadNotifications();
        });
    }

    function addNotificationTemplate() {
        var url = "/Notification/AddNotificationTemplate";
        var params = "";
        <%if(ViewBag.Sensor != null){%>
        params += "sensorID=<%:ViewBag.Sensor.SensorID %>";
        <%}%>
        <%if(ViewBag.Gateway != null){%>
        params += "gatewayID=<%:ViewBag.Gateway.GatewayID %>";
        <%}%>
        var templateChecked = false;
        $("input:checked").each(function () {
            var id = $(this).attr("id");
            if (id && id.indexOf("predefinedNotification_") == 0)//make sure it is from the correct list
            {
                params += "&notificationTemplateIndexes=" + id.replace("predefinedNotification_", "");
                templateChecked = true;
            }
        });

        if (templateChecked == false) {
            showSimpleMessageModal("<%=Html.TranslateTag("You must select at least one notification template to add.")%>");
            return;
        }

        $.post(url, params, function (data) {
            $('#assignedNotificationTable').html(data);
            //loadNotifications();
        });
    }

    //Moved to script on Add Page
    //function removeNotification(notificationID) 
   

</script>
