<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%="" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
    ushort Time1 = 0;

    LatchedDualControl Application = new LatchedDualControl();
    Application.SetSensorAttributes(Model.SensorID);

    LatchedDualControl ControlMessage = null;
    if (Model.LastDataMessage != null)
    {
        ControlMessage = (LatchedDualControl)MonnitApplicationBase.LoadMonnitApplication(Model.FirmwareVersion, Model.LastDataMessage.Data, Model.ApplicationID, Model.SensorID);
    }
%>
<style>
    .green {
        background-image: url("/Content/images/notification/relay-on.png");
        background-size: 40px 30px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .gray {
        background-image: url("/Content/images/notification/relay-off.png");
        background-size: 40px 30px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .greengray {
        background-image: url("/Content/images/notification/relay-toggle.png");
        background-size: 40px 30px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .greentime {
        background-image: url("/Content/images/notification/timer-on.png");
        background-size: 40px 30px;
        background-repeat: no-repeat;
        width: 76px;
        margin-left: 1px;
        height: 57px;
    }

    .graytime {
        background-image: url("/Content/images/notification/timer-off.png");
        background-size: 40px 30px;
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
                <select class="form-select" name="Relay" id="Relay" style="margin-left: 11px; max-width: 250px;">
                    <option value="1">Set <%: Model.GetDatumName(0)== "RelayState1" ? Application.Relay1NameAttribute.Value : Model.GetDatumName(0) %></option>
                    <option value="2">Set <%: Model.GetDatumName(1)== "RelayState2" ? Application.Relay2NameAttribute.Value : Model.GetDatumName(1) %></option>
                </select>
            </div>
        </div>
        <div class="col-md-2 col-4"style="padding-left:30px;">
            <div id="relaymessage" class="gray">
                <input id="State" type="hidden" class="form-control" name="State" value="1" />
            </div>
        </div>
        <div class="col-md-5 col-sm-7 col-8">
            <div id="timerelay" style="float: left" class="greentime">
            </div>
            <div id="timing" style="margin-top: 10px; float: left;">
                <div class="col-12 mdbox d-flex align-items-center">
                    <input class="form-control" id="minutes" type="text" style="text-align: center; width: 50px;" onchange="AddMinutes(<%:Model.SensorID%>); return false;" value="" />
                    <input class="form-control" id="seconds" type="text" style="text-align: center; width: 50px;" onchange="AddMinutes(<%:Model.SensorID %>); return false;" value="" />
                    <input class="form-control" id="Time" type="hidden" style="width: 60px" name="Time" value="<%:Time1 %>" />
                    <div style="text-align: start;" class="timeSec"><%: Html.TranslateTag("Minutes","Minutes")%> &nbsp <%: Html.TranslateTag("Seconds","Seconds")%></div>
                </div>

            </div>

        </div>

        <div class="col-sm-3 col-12">
            <button type="submit" value="Send Control" class="btn btn-primary btn-sm dfac">
                <span>Send
                </span>
                &nbsp;
                <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 18 18">
                    <path id="paper-plane-regular" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" fill="#fff" />
                </svg>
            </button>
        </div>

        <div class="clearfix"></div>
    </div>




    <%}%>
    <div>
        <div class="formtitle" id="controlCommands">
            <div class="col-sm-10 col-8">Control Commands</div>
            <div class="col-sm-2 col-3">
                <a role="button" style="max-width: 120px;" class="btn btn-secondary btn-sm dfjcc" href="/Overview/ClearPendingControlHistory?sensorID=<%: Model.SensorID%>">Clear History
                </a>
            </div>
        </div>

        <!-- START -->

        <table id="controlTable" class="table table-hover">
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
                <% foreach (var item in NotificationRecorded.LoadGetMessageForLocalNotifier(Model.SensorID).OrderBy(stat => stat.NotificationDate))
                    {

            %>
                <tr>
                    <td><%: item.NotificationDate.OVToLocalDateTimeShort()  %></td>
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
                                if (countTemp == 2)
                                    isRelayOne = false;
                                if (!RelayWritten)
                                {
                                    if (isRelayOne)
                                    {
                                        RelayWritten = true;
            %>
                    <td><%:LatchedDualControl.Relay1Name(Model.SensorID)%></td>
                    <%}
                        else
                        {
                            RelayWritten = true; %>
                    <td><%:LatchedDualControl.Relay2Name(Model.SensorID)%></td>
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
                    <td><%: s%></td>
                    <%}
                        if (ser[2].ToInt() == 0 && ser[3].ToInt() == 0)
                        { %>
                    <td>&nbsp;</td>
                    <%}
                            }
                        }%>
                    <td><%: item.Status %></td>
                </tr>
                <% } %>
            </tbody>
        </table>


        <!-- END -->

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

