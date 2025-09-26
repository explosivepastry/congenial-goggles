<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>



<%if(Model != null){ %>    
<div style="width:610px; overflow:auto;">
    <table width="100%">
        <tr>
            <th></th>
            <th>
                SensorID
            </th>
            <th>
                Name
            </th>
            <th>
                Account
            </th>
            <th>
                Network
            </th>
            <th>
                Status
            </th>
            <th>
                Last Check In
            </th>
        </tr>

    <% foreach (Sensor item in Model)
        {
            Account account = Account.Load(item.AccountID);
            string imagePath = "";
            switch (item.Status)
            {
                case eSensorStatus.OK:
                    imagePath = Html.GetThemedContent("/images/good.png");
                    break;
                case eSensorStatus.Warning:
                    imagePath = Html.GetThemedContent("/images/Alert.png");
                    break;
                case eSensorStatus.Alert:
                    imagePath = Html.GetThemedContent("/images/alarm.png");
                    break;
                //case eSensorStatus.Inactive:
                //    imagePath = Html.GetThemedContent("/images/inactive.png");
                //    break;
                //case eSensorStatus.Sleeping:
                //    imagePath = Html.GetThemedContent("/images/sleeping.png");
                //    break;
                case eSensorStatus.Offline:
                    imagePath = Html.GetThemedContent("/images/sleeping.png");
                    break;

            } %>
    <%if (item.CSNetID != long.MinValue)
      { %>
        <tr>
            <td>
                <a href="/Account/NetworkSettings/<%: item.AccountID%>?networkID=<%:item.CSNetID%>">View</a>
            </td>
            <td>
                <%:item.SensorID%>
            </td>
            <td>
                <%:item.SensorName%> 
            </td>
            <td>
                <%:account.CompanyName%> 
            </td>
            <td>
                
                <%:CSNet.Load(item.CSNetID).Name%> (<%:item.CSNetID%>)
            </td>
            <td>
                <% string Title = item.LastDataMessage == null ? "No Data" : item.LastDataMessage.DisplayData + "     (Signal Strength: " + DataMessage.GetSignalStrengthPercent(item.GenerationType, item.SensorTypeID, item.LastDataMessage.SignalStrength) + "%)"; %>
                <a href="/Sensor/Details/<%:item.SensorID%>?IsModal=true" onclick="loadFullSensor(this); return false;"><img src="<%: imagePath%>" alt="<%:Title%>" title="<%:Title%>" /></a>
            </td>
            <td>
                <% if (item.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
                   { %>
            <%: Monnit.TimeZone.GetLocalTimeById(item.LastCommunicationDate, account.TimeZoneID).ToShortDateString()%>
            <%: Monnit.TimeZone.GetLocalTimeById(item.LastCommunicationDate, account.TimeZoneID).ToShortTimeString()%>
            <% } %>
            </td>
        </tr>
    
    <% }
       } %>

    </table>

</div>

    <div class="formtitle" style="margin:0px -10px;">Search</div>
 <% } %>


<% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

            <div class="editor-label">
                Application
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("ID", new SelectList(MonnitApplication.LoadAll().OrderBy(ma=>{return ma.ApplicationName;}), "ApplicationID", "ApplicationName", ViewData["ApplicationID"].ToStringSafe()), new Dictionary<string, string>())%>
            </div>
        
    <div style="clear:both;"></div> 
    <div class="buttons" style="margin: 10px -10px -10px -10px;">
        <input type="button" onclick="postMain();" value="Search" class="bluebutton" />
        <div style="clear:both;"></div>
    </div>
    <% } %>

<script type="text/javascript">
    $(document).ready(function () {

        $('#ID').keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        //$(window).keydown(function (event) {
        //    if ($("*:focus").attr("id") != "savebtn") {
        //        if (event.keyCode == 13) {
        //            event.preventDefault();
        //            return false;
        //        }
        //    }
        //});

        $('form').submit(function (e) {
            e.preventDefault();
            postMain();
        });
    });

    function loadFullSensor(anchor) {
        newModal("Sensor Details", $(anchor).attr("href"), 450, 700);
    }
</script>