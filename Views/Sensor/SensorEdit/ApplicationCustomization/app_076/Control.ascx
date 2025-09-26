<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
    ushort Time1 = 0;

    BasicControl Application = new BasicControl();
    Application.SetSensorAttributes(Model.SensorID);

    BasicControl ControlMessage = null;
    if (Model.LastDataMessage != null)
    {
        ControlMessage = (BasicControl)MonnitApplicationBase.LoadMonnitApplication(Model.FirmwareVersion, Model.LastDataMessage.Data, Model.ApplicationID, Model.SensorID);
    }
%>
<style>
    .green {
        background-image: url("/Content/images/notification/relay-on.png");
        background-size: 80px 60px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .gray {
        background-image: url("/Content/images/notification/relay-off.png");
        background-size: 80px 60px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .greengray {
        background-image: url("/Content/images/notification/relay-toggle.png");
        background-size: 80px 60px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .greentime {
        background-image: url("/Content/images/notification/timer-on.png");
        background-size: 80px 60px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .graytime {
        background-image: url("/Content/images/notification/timer-off.png");
        background-size: 80px 60px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }
</style>
<div id="container1">
    <%using (Html.BeginForm())
        {%>
    <div class="formtitle">

        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>


        <%: Html.ValidationSummary(false)%>
        <%: Html.Hidden("id", Model.SensorID)%>

        <%  if (Model.PendingActionControlCommand)
            { %>
        <div style="font-size: 12px">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_076|There is already a pending control message for this sensor, sending another may prevent the first from taking effect.","There is already a pending control message for this sensor, sending another may prevent the first from taking effect.")%>
        </div>
        <% } %>
    </div>


    <div class="row section">
        <div class="col-md-2 col-12 form-group">
            <div class="editor-label bold" style="vertical-align: central;">
                <%: Html.TranslateTag("Set Command","Set Command")%>:
            </div>
        </div>
        <div class="col-md-2 col-sm-4 col-4">
            <div id="relaymessage" class="gray">
                <input id="State" type="hidden" name="State" value="1" />
            </div>
        </div>
        <div class="col-md-5 col-sm-7 col-8">
            <div id="timerelay" style="float: left" class="greentime">
            </div>
            <div id="timing" style="margin-top: 10px; float: left;">
                <div class="col-md-12 col-12 mdbox d-flex align-items-center">
                    <input class="form-control" id="minutes" type="text" style="text-align: center; width: 50px;" onchange="AddMinutes(<%:Model.SensorID%>); return false;" value="" />
                    <input class="form-control" id="seconds" type="text" style="text-align: center; width: 50px;" onchange="AddMinutes(<%:Model.SensorID %>); return false;" value="" />
                    <input class="form-control" id="Time" type="hidden" style="width: 60px" name="Time" value="<%:Time1 %>" />
                    <div style="text-align: start;" class="timeSec"><%: Html.TranslateTag("Minutes","Minutes")%> &nbsp <%: Html.TranslateTag("Seconds","Seconds")%></div>
                </div>

            </div>

        </div>

        <div class="col-sm-3 col-12">
            <input type="submit" value="<%: Html.TranslateTag("Send Control","Send Control")%>" class="btn btn-primary btn-sm" />
        </div>

        <div class="clearfix"></div>
    </div>
    <%}%>
    <div>
        <div class="formtitle row" id="controlCommands">
            <div class="col-sm-10 col-8">Control Commands</div>

            <div class="col-sm-2 col-3">
                <a role="button" class="btn btn-secondary btn-sm" href="/Overview/ClearPendingControlHistory?sensorID=<%: Model.SensorID%>">
                    <%: Html.TranslateTag("Clear History","Clear History")%>
                </a>
            </div>
        </div>


        <table class="table table-hover">
            <thead>
                <tr>
                    <th scope="col">Date</th>
                    <th scope="col">Relay</th>
                    <th scope="col">State</th>
                    <th scope="col">Timer</th>
                    <th scope="col">Status</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var item in NotificationRecorded.LoadGetMessageForLocalNotifier(Model.SensorID).OrderBy(stat => stat.Status))
                    {

                %>
                <tr>
                    <td><%:  (Monnit.TimeZone.GetLocalTimeById(item.NotificationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).OVToLocalDateTimeShort())  %></td>
                    <% string[] ser = item.SerializedRecipientProperties.Split('|');
                        int status = 0;
                        int countTemp = 0;
                        string temp;
                        bool isRelayOne = true;
                        bool RelayWritten = false;
                        foreach (string s in ser)
                        {
                            countTemp++;
                            if (s != "0")
                            {
                                //if (countTemp == 2)
                                //    isRelayOne = false;
                                if (!RelayWritten)
                                {
                                    if (isRelayOne)
                                    {
                                        RelayWritten = true;
                    %>
                    <td><%:BasicControl.Relay1Name(Model.SensorID)%></td>
                    <%}
                        }
                        status++;
                        if (status == 1)
                        {  //2 on  //1 off // 3 toggle
                            switch (s)
                            {
                                case "1":
                                    temp = "Off";
                                    break;
                                case "2":
                                    temp = "On";
                                    break;
                                case "3":
                                    temp = "Toggle";
                                    break;
                                default:
                                    temp = "";
                                    break;
                            }%>
                    <td><%: temp%></td>
                    <%}
                        else
                        {%>
                    <td><%: s%>s</td>
                    <td>&nbsp;</td>
                    <%}
                            }
                        }%>
                    <td><%: item.Status %></td>

                </tr>
                <%
                    }%>
            </tbody>
        </table>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {


        if ($('#Time').val() == 0) {
            $('#timerelay').removeClass("greentime");
            $('#timerelay').addClass("graytime");
            $('#timing').hide();
            $('#Time').val(0);
        }

        $('#Time').blur(function () {
            var input = $('#Time').val();
            if (isANumber(input)) {
                if (input > 43200)
                    input = 43200;

                if (input < 0)
                    input = 0;

                $('#Time').val(input);
            }
            else {

                $('#timerelay').removeClass("greentime");
                $('#timerelay').addClass("graytime");
                $('#timing').hide();
                $('#Time').val(0);
            }
        });

        $('#relaymessage').click(function () {

            if ($('#relaymessage').hasClass("green")) {
                $('#relaymessage').removeClass("green");
                $('#relaymessage').addClass("greengray")
                $('#State').val(3);
                // 3 toggle
            }
            else if ($('#relaymessage').hasClass("greengray")) {
                $('#relaymessage').removeClass("greengray");
                $('#relaymessage').addClass("gray");
                $('#State').val(1);
                //1 off
            }
            else if ($('#relaymessage').hasClass("gray")) {
                $('#relaymessage').removeClass("gray");
                $('#relaymessage').addClass("green");
                $('#State').val(2);
                //2 on 
            }
        });

        $('#timerelay').click(function () {

            if ($('#timerelay').hasClass("greentime")) {
                $('#timerelay').removeClass("greentime");
                $('#timerelay').addClass("graytime");
                $('#timing').hide();
                $('#Time').val(0);
            }
            else if ($('#timerelay').hasClass("graytime")) {
                $('#timerelay').removeClass("graytime");
                $('#timerelay').addClass("greentime");
                $('#timing').show();
            }

        });



    });

    function AddMinutes(deviceID) {
        var minutes = ~~$('#minutes').val();
        var seconds = ~~$('#seconds').val();
        var total = (minutes * 60) + seconds;

        $('#minutes').val(~~(total / 60));
        $('#seconds').val(total % 60);
        $('#Time').val(~~total);



    }

    if (window.location.href.includes("Testing")) {
        document.getElementById("controlCommands").style.display = "none";
    }

</script>

