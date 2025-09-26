<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.DataMessage>>" %>
<% 
    
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
    
    Sensor viewSensor = (Sensor)ViewData["Sensor"];
%>

<div id="divHistory">


    <div style="width: 100%; margin-right: -10px">
        <div class="formtitle">
            <%if (MonnitSession.AccountCan("see_missed_communications"))
              { %>
            <a id="missed" href="/Sensor/HistoryMissed/<%:viewSensor.SensorID %>">Missed Communications</a>
            <%} %>
            <%Html.RenderPartial("HistoryDatePicker");%><br />
            <%--<input class="daterange" readonly="readonly" unselectable="on" value="<%: MonnitSession.CurrentDateRange %>"/>--%>
        </div>
        <table width="100%">
            <tr>
                <th style="width: 150px;">Date
                </th>
                <th style="width: 60px; text-align: center;">Signal
                </th>
                <th style="width: 60px; text-align: center;">Battery
                </th>
                <th style="text-align: right;">Sensor Reading
                </th>
            </tr>
        </table>
    </div>
    <div id="divHistTable" class="classicScrollBar" style="overflow: auto; width: 100%;  margin-right: -10px;">
        <table id="histTable" width="100%">
            <% foreach (var item in Model)
               {    %>

            <tr data-messageid="<%: item.DataMessageGUID%>">
                <td style="width: 150px; vertical-align: top;" title="Gateway: <%:item.GatewayID%>">
                    <%:  (String.Format("{0:g}", item.MessageDateLocalTime(MonnitSession.CurrentCustomer.Account.TimeZoneID)))%>
                </td>
                <td style="width: 60px; text-align: center; vertical-align: top;" title="RSSI: <%:item.SignalStrength%> Status: <%:item.State%>">
                    <%:  (DataMessage.GetSignalStrengthPercent(viewSensor.GenerationType, viewSensor.SensorTypeID, item.SignalStrength))%>
                </td>
                <td style="width: 60px; text-align: center; vertical-align: top;">
                    <%if ((ViewData["Sensor"] as Monnit.Sensor).PowerSourceID == 3 || item.Voltage == 0)
                          Response.Write("Line Power");
                      else if ((ViewData["Sensor"] as Monnit.Sensor).PowerSourceID == 4)
                          Response.Write(item.Voltage);
                      else
                          Response.Write(item.Battery);
                    %>
                </td>

                <td style="position: relative; vertical-align: top;" class="note">
                    <div style="float: left;">
                        <%if (item.HasNote)
                          { %>
                        <img alt="Reading Note" class="noteIcon" src="/Content/images/Notification/note2.png">
                        <%} %>
                    </div>
                    <div class="myhover">
                        <div class='addnote' data-messageid="<%: item.DataMessageGUID%>">+ Add Note </div>
                    </div>

                    <div style="float: right;"><%:  (item.DisplayData)%></div>
                </td>

            </tr>
            <%}%>
        </table>
    </div>
</div>
<div id="divNote" style="display:none;">   
</div>
<script>
    $(function () {
        $(".note").on("mouseenter", function () {
            var cell = $(this);
            cell.find('.myhover').height(cell.height()).width(cell.width()).fadeIn(700);
        }).on("mouseleave", function () {
            $(this).find('.myhover').stop().fadeOut(100);
        });

        $('#missed').click(function (e) {
            e.preventDefault();
            $.get($(this).attr("href"), function (data) {
                $('#divHistory').parent().html(data);
            });
        });

        $('.addnote').click(function () {
            var messageid = $(this).data('messageid');
                        
            $('#divNote').html('');
            $('#divHistory').hide();
            $('#divNote').show();

            $.get("/Sensor/DataMessageNoteLog/" + $(this).data("messageid"), function (data) {
                $('#divNote').html(data);
            });
        });

        var isWorking = false;
        $('#divHistTable').scroll(function () {
            if ($('#divHistTable').scrollTop() == $('#histTable').height() - $('#divHistTable').height() && isWorking == false) {
                isWorking = true;
                var DataMessageGUID = $('#histTable').find("tr").last().data('messageid');
                var sensorID = '<%:(ViewData["Sensor"] as Sensor).SensorID %>';

                var spinner = $(`<tr><td colspan=4><div id="loadingGIF" class="text-center" style="display: none;">
                    < div class= "spinner-border text-primary" style = "margin-top: 50px;" role = "status" >
                    <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div ></td></tr>`);

                $('#histTable tbody').append(spinner);
                $.get("/Sensor/History", { id: sensorID, dataMessageGUID: DataMessageGUID }, function (data) {
                    var items = $(data).find('#histTable tbody tr');
                    if (data == "No Data") {
                        spinner.remove();
                        isWorking = false;
                        return;
                    }
                    $('#histTable tbody').append(items);

                    spinner.remove();
                    isWorking = false;

                    $(".note").on("mouseenter", function () {
                        var cell = $(this);
                        cell.find('.myhover').height(cell.height()).width(cell.width()).fadeIn(700);
                    }).on("mouseleave", function () {
                        $(this).find('.myhover').stop().fadeOut(100);
                    });
                });
            }
        });
    });
</script>

