<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<InboundPacketModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DataPacketIn
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <form action="/Settings/DataPacketIn/" method="post" id="dataPacketInForm">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
            <div class="col-12 x_panel card_container shadow-sm rounded mt-4">
                <div class="ms-2">
                    <div class="x_title">
                        <div class="card_container__top__title"><%=Html.GetThemedSVG("list") %> &nbsp;<%: Html.TranslateTag("Settings/DataPacketIn|Inbound Data Packet","Inbound Data Packet")%></div>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-12">
                            <div class="">
                                <div class="clearfix"></div>
                                <div class="form-group col-12">
                                    <div class="col-12">
                                        <div id="datePickMobi"></div>

                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("From","From")%>: 
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <input type="text" class="form-control" id="Mobi_startDate" name="FromDateStr" />
                                            </div>
                                        </div>

                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("To","To")%>: 
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <input type="text" class="form-control" id="Mobi_endDate" name="ToDateStr" />
                                            </div>
                                        </div>
                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Time Offset","Time Offset")%>:
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <select class="form-select" id="TimeOffset" name="TimeOffset">
                                                    <option value="0" <%:  Model.TimeOffset == 0?"selected":""%>>UTC </option>
                                                    <option value="-1" <%:  Model.TimeOffset == -1?"selected":""%>>UTC -1 </option>
                                                    <option value="-2" <%:  Model.TimeOffset == -2?"selected":""%>>UTC -2 </option>
                                                    <option value="-3" <%:  Model.TimeOffset == -3?"selected":""%>>UTC -3 </option>
                                                    <option value="-4" <%:  Model.TimeOffset == -4?"selected":""%>>UTC -4 </option>
                                                    <option value="-5" <%:  Model.TimeOffset == -5?"selected":""%>>UTC -5 </option>
                                                    <option value="-6" <%:  Model.TimeOffset == -6?"selected":""%>>UTC -6 </option>
                                                    <option value="-7" <%:  Model.TimeOffset == -7?"selected":""%>>UTC -7 </option>
                                                    <option value="-8" <%:  Model.TimeOffset == -8?"selected":""%>>UTC -8 </option>
                                                    <option value="-9" <%:  Model.TimeOffset == -9?"selected":""%>>UTC -9 </option>
                                                    <option value="-10" <%:  Model.TimeOffset == -10?"selected":""%>>UTC -10 </option>
                                                    <option value="-11" <%:  Model.TimeOffset == -11?"selected":""%>>UTC -11 </option>
                                                    <option value="-12" <%:  Model.TimeOffset == -12?"selected":""%>>UTC -12 </option>
                                                    <option value="1" <%:  Model.TimeOffset == 1?"selected":""%>>UTC +1 </option>
                                                    <option value="2" <%:  Model.TimeOffset == 2?"selected":""%>>UTC +2 </option>
                                                    <option value="3" <%:  Model.TimeOffset == 3?"selected":""%>>UTC +3 </option>
                                                    <option value="4" <%:  Model.TimeOffset == 4?"selected":""%>>UTC +4 </option>
                                                    <option value="5" <%:  Model.TimeOffset == 5?"selected":""%>>UTC +5 </option>
                                                    <option value="6" <%:  Model.TimeOffset == 6?"selected":""%>>UTC +6 </option>
                                                    <option value="7" <%:  Model.TimeOffset == 7?"selected":""%>>UTC +7 </option>
                                                    <option value="8" <%:  Model.TimeOffset == 8?"selected":""%>>UTC +8 </option>
                                                    <option value="9" <%:  Model.TimeOffset == 9?"selected":""%>>UTC +9 </option>
                                                    <option value="10" <%:  Model.TimeOffset == 10?"selected":""%>>UTC +10 </option>
                                                    <option value="11" <%:  Model.TimeOffset == 11?"selected":""%>>UTC +11 </option>
                                                    <option value="12" <%:  Model.TimeOffset == 12?"selected":""%>>UTC +12 </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3" ondblclick="$('.sensor-input').show();">
                                        <%: Html.TranslateTag("Gateway ID","Gateway ID")%>:
                                    </div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" id="GatewayID" name="GatewayID" type="text" value="<%=Session["PacketGatewayID"]%>">
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3">
                                        <%: Html.TranslateTag("Response","Response")%>:
                                    </div>
                                    <div class="col sensorEditFormInput">
                                        <select class="form-select" id="Response" name="Response">
                                            <option value="<%: int.MinValue %>" <%: Convert.ToInt32(Model.Response) == int.MinValue ? "selected":""%>><%: Html.TranslateTag("Settings/DataPacketIn|All Responses","All Responses")%></option>
                                            <option value="0" <%:   Convert.ToInt32(Model.Response) == 0?"selected":""%>>Data Message</option>
                                            <option value="1" <%:   Convert.ToInt32(Model.Response) == 1?"selected":""%>>V1 Gateway Message</option>
                                            <option value="2" <%:   Convert.ToInt32(Model.Response) == 2?"selected":""%>>Send Sensor List</option>
                                            <option value="3" <%:   Convert.ToInt32(Model.Response) == 3?"selected":""%>>Update Network Config</option>
                                            <option value="4" <%:   Convert.ToInt32(Model.Response) == 4?"selected":""%>>Reform Network</option>
                                            <option value="5" <%:   Convert.ToInt32(Model.Response) == 5?"selected":""%>>Reset to Factory Defaults</option>
                                            <option value="6" <%:   Convert.ToInt32(Model.Response) == 6?"selected":""%>>Location Data Received</option>
                                            <option value="7" <%:   Convert.ToInt32(Model.Response) == 7?"selected":""%>>WiFi Sensor Lookup</option>
                                            <option value="8" <%:   Convert.ToInt32(Model.Response) == 8?"selected":""%>>Gateway Config Match</option>
                                            <option value="9" <%:   Convert.ToInt32(Model.Response) == 9?"selected":""%>>File Upload</option>
                                            <option value="10" <%:  Convert.ToInt32(Model.Response) == 10?"selected":""%>>Recorded Status</option>
                                            <option value="11" <%:  Convert.ToInt32(Model.Response) == 11?"selected":""%>>Startup Message</option>
                                            <option value="14" <%:  Convert.ToInt32(Model.Response) == 14?"selected":""%>>OTA Ack</option>
                                            <option value="250" <%: Convert.ToInt32(Model.Response) == 250?"selected":""%>>Gateway BootLoader</option>
                                            <option value="251" <%: Convert.ToInt32(Model.Response) == 251?"selected":""%>>Sensor Bootloader</option>
                                            <option value="252" <%: Convert.ToInt32(Model.Response) == 252?"selected":""%>>Interpretor Load</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="row sensorEditForm" style='display: <%: Model.SensorID > long.MinValue ? "block" : "none" %>'>
                                    <div class="col-12 col-md-3" ondblclick="$('.sensor-input').hide();">
                                        <%: Html.TranslateTag("Sensor ID","Sensor ID")%>:
                                    </div>
                                    <div class="col sensorEditFormInput">
                                        <input class="form-control" id="SensorID" name="SensorID" type="text" value="<%=Model.SensorID > long.MinValue ? Model.SensorID.ToString() : ""%>">
                                    </div>
                                </div>


                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3">
                                        <%: Html.TranslateTag("Count","Count")%>:
                                    </div>
                                    <div class="col sensorEditFormInput">
                                        <select class="form-select" id="Count" name="Count">
                                            <option value="10" <%: Convert.ToInt32(Model.Count) == 10?"selected":""%>>10</option>
                                            <option value="25" <%: Convert.ToInt32(Model.Count)  == 25?"selected":""%>>25</option>
                                            <option value="50" <%: Convert.ToInt32(Model.Count)  == 50?"selected":""%>>50</option>
                                            <option value="75" <%: Convert.ToInt32(Model.Count)  == 75?"selected":""%>>75</option>
                                            <option value="100" <%: Convert.ToInt32(Model.Count)  == 100?"selected":""%>>100</option>
                                            <option value="500" <%: Convert.ToInt32(Model.Count)  == 500?"selected":""%>>500</option>
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <div class="form-group d-flex">
                                    <button type="submit" id="toFromSub" name="toFromSub" class="btn btn-primary me-2" value="<%: Html.TranslateTag("Settings/DataPacketIn|Filter","Filter")%>">
                                        <%: Html.TranslateTag("Settings/DataPacketIn|Filter","Filter")%>
                                    </button>
                                    <button class="btn btn-primary parsedMessage" onclick="$('.rawMessage').show(); $('.parsedMessage').hide(); return false;"><%: Html.TranslateTag("Settings/DataPacketIn|Show Raw","Show Raw")%></button>
                                    <button class="btn btn-primary rawMessage" style="display: none;" onclick="$('.parsedMessage').show(); $('.rawMessage').hide(); return false;"><%: Html.TranslateTag("Settings/DataPacketIn|Show Parsed","Show Parsed")%></button>

                                    <a href="/Settings/DataPacketInCSV/" title="<%: Html.TranslateTag("Export","Export")%>" class="ms-2 helpIco" id="exportInboundDataPackets" style="cursor: pointer; float: right;">
                                        <%=Html.GetThemedSVG("download-file") %>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-12" style="padding: 10px;">
                            <div class="">
                                <div class="clearfix"></div>
                                <div class="inboundPacket">
                                    <div class="row">
                                        <div class="bold col-3">
                                            <%: Html.TranslateTag("Settings/DataPacketIn|ReceivedDate","ReceivedDate")%>
                                        </div>
                                        <div class="bold col-3">
                                            <%: Html.TranslateTag("Gateway ID","Gateway ID")%>
                                        </div>
                                        <div class="bold col-1">
                                            <%: Html.TranslateTag("Type","Type")%>
                                        </div>
                                        <div class="bold col-1">
                                            <%: Html.TranslateTag("Count","Count")%>
                                        </div>
                                        <div class="bold col-1">
                                            <%: Html.TranslateTag("Power","Power")%>
                                        </div>
                                        <div class="bold col-2">
                                            <%: Html.TranslateTag("Settings/DataPacketIn|Security","Security")%>
                                        </div>
                                        <div class="bold col-1 extra" style="text-align: right;">
                                            <%: Html.TranslateTag("More","More")%>
                                        </div>
                                    </div>
                                    <div>
                                        <%if (Model.InboundPackets != null)
                                            {
                                                Dictionary<int, string> gatewayMessageTypes = ExtensionMethods.EnumerationToDictionary<BaseApplication.eGatewayMessageType>();
                                                string gatewayMessageTypeName = string.Empty;
                                        %>
                                        <%foreach (InboundPacket ibp in Model.InboundPackets)
                                            {
                                                if (!gatewayMessageTypes.TryGetValue(ibp.Response, out gatewayMessageTypeName))
                                                {
                                                    gatewayMessageTypeName = "Other";
                                                }
                                                DateTime receiveDateTime = ibp.ReceivedDate.AddHours(Model.TimeOffset);
                                        %>
                                        <div class="row inboundDataPacketDetails" title="Inbound Packet GUID: <%: ibp.InboundPacketGUID %>">
                                            <div class="col-xs-3 col-md-3 col-lg-3 dataPacketField">
                                                <%:receiveDateTime.ToDateFormatted(MonnitSession.CurrentCustomer.Preferences["Date Format"]) 
                                                + " " + receiveDateTime.ToTimeFormatted(MonnitSession.CurrentCustomer.Preferences["Time Format"]) %>
                                            </div>
                                            <div class="col-3 dataPacketField">
                                                <%: ibp.APNID%>
                                            </div>
                                            <div class="col-1 dataPacketField" title="<%: gatewayMessageTypeName.ToStringSafe() %>">
                                                <%: ibp.Response %>
                                            </div>
                                            <div class="col-1 dataPacketField">
                                                <%: ibp.MessageCount%>
                                            </div>
                                            <div class="col-1 dataPacketField">
                                                <%: ibp.Power %>
                                            </div>
                                            <div class="col-2 dataPacketField" title="<%:ibp.Security %>">
                                                <%: ibp.Security >1 ? "True" : "False" %>
                                            </div>
                                            <div class="col-1 dataPacketField" style="text-align: right;">
                                                <%: ibp.More %>
                                            </div>
                                        </div>
                                        <div class="holdInboundPacketDetails" style="display: none; border-top-width: 0px; width: 100%;">
                                            <input class="rawMessage" type="text" value="<%:ibp.Message.ToHex()%>" style="width: 100%;" />
                                            <%if (ibp.Response == 0)
                                                { %>
                                            <table class="table table-striped" style="width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th scope="col"></th>
                                                        <th scope="col">Date</th>
                                                        <th scope="col">ID</th>
                                                        <th scope="col">Type</th>
                                                        <th scope="col"></th>
                                                        <th scope="col">RSSI</th>
                                                        <th scope="col">Voltage</th>
                                                        <th scope="col">ProfileID</th>
                                                        <th scope="col">State</th>
                                                        <th scope="col">Message</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%
                                                        int index = 0;
                                                        while (ibp.Payload.Length > index + 5)
                                                        {
                                                            int MessageLength = ibp.Payload[index + 5].ToInt();
                                                            if (MessageLength <= 0)
                                                                break;
                                                            byte[] messagebuffer = new byte[MessageLength + 7];
                                                            if (ibp.Payload.Length < (index + MessageLength + 7))
                                                                break;
                                                            Array.Copy(ibp.Payload, index, messagebuffer, 0, MessageLength + 7);
                                                            index += MessageLength + 7;
                                                            long MessageSensorID = 0;
                                                            if (messagebuffer.Length >= 12)
                                                                MessageSensorID = BitConverter.ToUInt32(messagebuffer, 8);

                                                            if (Model.SensorID > long.MinValue && Model.SensorID != MessageSensorID)
                                                                continue;

                                                    %>
                                                    <tr class="rawMessage">
                                                        <td></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%> : <%:MessageSensorID %>" colspan="5"><%:messagebuffer.ToHex() %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                        if (MessageLength < 8)
                                                            continue;

                                                        DateTime time = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(messagebuffer, 0));
                                                        switch (messagebuffer[7])
                                                        {
                                                            case 0x55:// DataMessage
                                                                byte[] messagepayload = new byte[MessageLength - 12];
                                                                Array.Copy(messagebuffer, 18, messagepayload, 0, MessageLength - 12);
                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.AddHours(Model.TimeOffset).ToShortDateString() %> <%:time.AddHours(Model.TimeOffset).ToLongTimeString() %> UTC</td>
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Data Message</td>
                                                        <td><%:(messagebuffer[6] & 0x01) == 0x01 ? "Urgent" : "" %></td>
                                                        <td><%:messagebuffer[12] - 256 %>/<%:messagebuffer[13] - 256 %></td>
                                                        <td><%:(messagebuffer[14] + 150) / 100.0 %></td>
                                                        <td><%:BitConverter.ToUInt16(messagebuffer,15) %></td>
                                                        <td><%:messagebuffer[17] %></td>
                                                        <td><%:messagepayload.ToHex() %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;
                                                        case 0x56://DataMessage DL
                                                            byte[] messagepayload2 = new byte[MessageLength - 16];
                                                            Array.Copy(messagebuffer, 22, messagepayload2, 0, MessageLength - 16);
                                                            time = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(messagebuffer, 12));
                                                            time = time.AddHours(Model.TimeOffset);
                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.ToShortDateString() %> <%:time.ToLongTimeString() %></td>
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Data Message DL</td>
                                                        <td><%:(messagebuffer[6] & 0x01) == 0x01 ? "Urgent" : "" %></td>
                                                        <td><%:messagebuffer[16] == 0 ? 0 : messagebuffer[16] - 256 %>/<%:messagebuffer[17] - 256 %></td>
                                                        <td><%:(messagebuffer[18] + 150) / 100.0 %></td>
                                                        <td><%:BitConverter.ToUInt16(messagebuffer,19) %></td>
                                                        <td><%:messagebuffer[21] %></td>
                                                        <td><%:messagepayload2.ToHex() %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;
                                                        case 0x57://Authenticated Data Message (Sensor Prints)
                                                            byte[] messagepayload3 = new byte[MessageLength - 16];
                                                            Array.Copy(messagebuffer, 22, messagepayload3, 0, MessageLength - 16);
                                                            time = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(messagebuffer, 12));
                                                            time = time.AddHours(Model.TimeOffset);
                                                    %>

                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.ToShortDateString() %> <%:time.ToLongTimeString() %></td>
                                                        <!-- Date -->
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <!-- ID -->
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Data Message ADL</td>
                                                        <td><%:(messagebuffer[6] & 0x01) == 0x01 ? "Urgent" : "" %></td>
                                                        <!-- Option -->
                                                        <td><%:messagebuffer[20] == 0 ? 0 : messagebuffer[20] - 256 %></td>
                                                        <!-- RSSI -->
                                                        <td><%:(messagebuffer[21] + 150) / 100.0 %></td>
                                                        <!-- Power -->
                                                        <td><%:BitConverter.ToUInt16(messagebuffer,22) %></td>
                                                        <!-- ProfileID -->
                                                        <td><%:messagebuffer[24] %></td>
                                                        <!-- State -->
                                                        <td title="AuthToken: <%: BitConverter.ToUInt32(messagebuffer, 16)%>"><%:messagepayload3.ToHex() %></td>
                                                        <!-- Message -->
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;
                                                        case 0x25://Join Message
                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.AddHours(Model.TimeOffset).ToShortDateString() %> <%:time.AddHours(Model.TimeOffset).ToLongTimeString() %></td>
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>" colspan="7">Join <%:messagebuffer[14] == 0 ? "Allowed":"Prohibited" %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;
                                                        case 0x52:// Parent Message
                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.AddHours(Model.TimeOffset).ToShortDateString() %> <%:time.AddHours(Model.TimeOffset).ToLongTimeString() %></td>
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Parent Device: </td>
                                                        <td colspan="5"><%:BitConverter.ToUInt32(messagebuffer, 12)%></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;
                                                        case 0x53:// Status Message
                                                            byte[] StatusPayload = new byte[messagebuffer.Length - 12];
                                                            Array.Copy(messagebuffer, 12, StatusPayload, 0, messagebuffer.Length - 12);
                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.AddHours(Model.TimeOffset).ToShortDateString() %> <%:time.AddHours(Model.TimeOffset).ToLongTimeString() %></td>
                                                        <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Sensor Status</td>
                                                        <td colspan="5"></td>
                                                        <td><%:StatusPayload.ToHex() %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                            break;

                                                        default:
                                                            byte[] DefaultPayload = null;
                                                            if (messagebuffer.Length - 12 > 0)
                                                            {
                                                                DefaultPayload = new byte[messagebuffer.Length - 12];
                                                                Array.Copy(messagebuffer, 12, DefaultPayload, 0, messagebuffer.Length - 12);
                                                            }
                                                            else
                                                            {
                                                                DefaultPayload = new byte[0];
                                                            }


                                                    %>
                                                    <tr class="parsedMessage">
                                                        <td></td>
                                                        <td><%:time.AddHours(Model.TimeOffset).ToShortDateString() %> <%:time.AddHours(Model.TimeOffset).ToLongTimeString() %></td>
                                                        <td><%:MessageSensorID%></td>
                                                        <td title="0x<%:messagebuffer[7].ToHex()%>">Other</td>
                                                        <td colspan="5"></td>
                                                        <td><%:DefaultPayload.ToHex() %></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                                break;

                                                            }
                                                        }%>
                                                </tbody>
                                            </table>
                                            <%}
                                                else
                                                { %>
                                            <div>Only 0 messages show parsed data</div>
                                            <%}%>
                                        </div>
                                        <%}
                                            }%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <div class="buttons"></div>
    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {
            <%--var startDate = '<%=Model.FromDateStr.ToString("yyyy-MM-dd-HH-mm")%>';
            var endDate = '<%=Model.ToDateStr.ToString("yyyy-MM-dd-HH-mm")%>';
            var startArr = startDate.split('-');
            var endArr = endDate.split('-');--%>

            //$('#datePickMobi').mobiscroll().datepicker({
            //    theme: 'ios',
            //    display: popLocation,
            //    controls: ['calendar', 'time'],
            //    select: 'range',
            //    defaultSelection: [new Date(startArr[0], startArr[1] - 1, startArr[2], startArr[3], startArr[4]), new Date(endArr[0], endArr[1] - 1, endArr[2], endArr[3], endArr[4])],
            //    startInput: "#Mobi_startDate",
            //    endInput: "#Mobi_endDate"
            //});

            let fromDt = new Date("<%= Model.FromDateOffset %>");
            let toDt = new Date("<%= Model.ToDateOffset %>");
            let mobiDateFmt = '<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>'.toUpperCase();
            let mobiTimeFmt = '<%=MonnitSession.CurrentCustomer.Preferences["Time Format"]%>'.replace('tt', 'A');

            $('#Mobi_startDate').mobiscroll().datepicker({
                theme: 'ios',
                controls: ['calendar', 'time'],
                select: 'date',
                display: popLocation,
                touchUI: true,
                dateFormat: mobiDateFmt,
                timeFormat: mobiTimeFmt,
                onInit: function (event, inst) {
                    inst.setVal(fromDt);
                }
            });

            $('#Mobi_endDate').mobiscroll().datepicker({
                theme: 'ios',
                controls: ['calendar', 'time'],
                select: 'date',
                display: popLocation,
                touchUI: true,
                dateFormat: mobiDateFmt,
                timeFormat: mobiTimeFmt,
                onInit: function (event, inst) {
                    inst.setVal(toDt);
                }
            });

            $('input[type=text]').click(function () {
                $(this).select();
            });

            $('.inboundDataPacketDetails').click(function () {
                var tr = $(this).next();
                var hide = tr.is(":visible");
                $('.inboundDataPacketDetails').css('border-bottom-width', '1px');
                $('.holdInboundPacketDetails').hide();
                if (!hide) {
                    $(this).css('border-bottom-width', '0px');
                    tr.show();
                }
            }).css('cursor', 'pointer');
            $('.rawMessage').hide();

            $('#exportInboundDataPackets').click(function (e) {
                e.preventDefault();

                $('#dataPacketInForm').attr('action', '/Settings/DataPacketInCSV/');
                $('#dataPacketInForm').submit();
                setTimeout(function () {
                    $('#dataPacketInForm').attr('action', '/Settings/DataPacketIn/');
                }, 500); // half second
            });

        });
    </script>
</asp:Content>
