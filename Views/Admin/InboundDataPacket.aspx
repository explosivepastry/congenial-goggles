<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<InboundPacket>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inbound Data Packet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="fullForm" style="width: 100%">

        <% using (Html.BeginForm())
           {
               bool alt = true; %>
        <% string temp = ""; %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <div class="formtitle">
            Inbound Data Packet Information
            <label style="padding-left: 491px; font-size: xx-small;">Records to Show: </label>
            <select id="Count" name="Count" style="width: 72px;">
                <option value="10" <%: Convert.ToInt32(ViewBag.Count) == 10?"selected":""%>>10</option>
                <option value="25" <%: Convert.ToInt32(ViewBag.Count)  == 25?"selected":""%>>25</option>
                <option value="50" <%: Convert.ToInt32(ViewBag.Count)  == 50?"selected":""%>>50</option>
                <option value="75" <%: Convert.ToInt32(ViewBag.Count)  == 75?"selected":""%>>75</option>
                <option value="100" <%: Convert.ToInt32(ViewBag.Count)  == 100?"selected":""%>>100</option>
                <option value="500" <%: Convert.ToInt32(ViewBag.Count)  == 500?"selected":""%>>500</option>
            </select>
        </div>

        <div>
            <table class="DateTimeGateway" width="100%">

                <tr style="width: 100%">
                    <th width="20"></th>
                    <th colspan="2">From: Day and Time
                    </th>
                    <th colspan="2">Time Format</th>
                    <th colspan="3">To: Day and Time
                    </th>
                </tr>
                <tr class="activeTime">
                    <td></td>
                    
                    <td colspan="2">
                        <%: Html.TextBox("FromMonths",(string)ViewBag.from,(Dictionary<string,object>)ViewData["HtmlAttributes"]) %>
                        <%: Html.DropDownList("FromHours", ((SelectList)ViewData["FromHours"]), (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
                        <%: Html.DropDownList("FromMinutes", (SelectList)ViewData["FromMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>    
                    </td>
                     <td colspan="2">
                       <select id="TimeFormat" name="TimeFormat" style="width: 120px;">
                          <option value="0" <%:  ViewBag.TimeFormat == 0?"selected":""%>>UTC </option>
                          <option value="-1" <%:  ViewBag.TimeFormat == -1?"selected":""%>>UTC -1 </option>
                          <option value="-2" <%:  ViewBag.TimeFormat == -2?"selected":""%>>UTC -2 </option>
                           <option value="-3" <%:  ViewBag.TimeFormat == -3?"selected":""%>>UTC -3 </option>
                           <option value="-4" <%:  ViewBag.TimeFormat == -4?"selected":""%>>UTC -4 </option>
                           <option value="-5" <%:  ViewBag.TimeFormat == -5?"selected":""%>>UTC -5 </option>
                           <option value="-6" <%:  ViewBag.TimeFormat == -6?"selected":""%>>UTC -6 </option>
                           <option value="-7" <%:  ViewBag.TimeFormat == -7?"selected":""%>>UTC -7 </option>
                           <option value="-8" <%:  ViewBag.TimeFormat == -8?"selected":""%>>UTC -8 </option>
                           <option value="-9" <%:  ViewBag.TimeFormat == -9?"selected":""%>>UTC -9 </option>
                           <option value="-10" <%:  ViewBag.TimeFormat == -10?"selected":""%>>UTC -10 </option>
                           <option value="-11" <%:  ViewBag.TimeFormat == -11?"selected":""%>>UTC -11 </option>
                           <option value="-12" <%:  ViewBag.TimeFormat == -12?"selected":""%>>UTC -12 </option>
                           <option value="1" <%:  ViewBag.TimeFormat == 1?"selected":""%>>UTC +1 </option>
                           <option value="2" <%:  ViewBag.TimeFormat == 2?"selected":""%>>UTC +2 </option>
                           <option value="3" <%:  ViewBag.TimeFormat == 3?"selected":""%>>UTC +3 </option>
                           <option value="4" <%:  ViewBag.TimeFormat == 4?"selected":""%>>UTC +4 </option>
                           <option value="5" <%:  ViewBag.TimeFormat == 5?"selected":""%>>UTC +5 </option>
                           <option value="6" <%:  ViewBag.TimeFormat == 6?"selected":""%>>UTC +6 </option>
                           <option value="7" <%:  ViewBag.TimeFormat == 7?"selected":""%>>UTC +7 </option>
                           <option value="8" <%:  ViewBag.TimeFormat == 8?"selected":""%>>UTC +8 </option>
                           <option value="9" <%:  ViewBag.TimeFormat == 9?"selected":""%>>UTC +9 </option>
                           <option value="10" <%:  ViewBag.TimeFormat == 10?"selected":""%>>UTC +10 </option>
                           <option value="11" <%:  ViewBag.TimeFormat == 11?"selected":""%>>UTC +11 </option>
                           <option value="12" <%:  ViewBag.TimeFormat == 12?"selected":""%>>UTC +12 </option>
                           
                       </select> 
                    </td>
                    <td colspan="2">
                        <%: Html.TextBox("ToMonths",(string)ViewBag.to,(Dictionary<string,object>)ViewData["HtmlAttributes"]) %>
                        <span><%: Html.DropDownList("ToHours", (SelectList)ViewData["ToHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:</span>
                        <span><%: Html.DropDownList("ToMinutes", (SelectList)ViewData["ToMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>  </span>
                    </td>

                </tr>
                <tr>
                    <th width="20"></th>
                    <th>Response</th>
                    <th>Gateaway ID
                    </th>
                    <th>Search
                    </th>
                    <th colspan="4"></th>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <select id="Response" name="Response" style="margin-bottom: 37px; width: 209px;">
                            <option value="<%: int.MinValue %>" <%: Convert.ToInt32(ViewBag.Response) == int.MinValue ?"selected":""%>>All Responses</option>
                            <option value="0" <%:   Convert.ToInt32(ViewBag.Response) == 0?"selected":""%>>Data Message</option>
                            <option value="1" <%:   Convert.ToInt32(ViewBag.Response) == 1?"selected":""%>>V1 Gateway Message</option>
                            <option value="2" <%:   Convert.ToInt32(ViewBag.Response) == 2?"selected":""%>>Send Sensor List</option>
                            <option value="3" <%:   Convert.ToInt32(ViewBag.Response) == 3?"selected":""%>>Update Network Config</option>
                            <option value="4" <%:   Convert.ToInt32(ViewBag.Response) == 4?"selected":""%>>Reform Network</option>
                            <option value="5" <%:   Convert.ToInt32(ViewBag.Response) == 5?"selected":""%>>Reset to Factory Defaults</option>
                            <option value="6" <%:   Convert.ToInt32(ViewBag.Response) == 6?"selected":""%>>Location Data Received</option>
                            <option value="7" <%:   Convert.ToInt32(ViewBag.Response) == 7?"selected":""%>>WiFi Sensor Lookup</option>
                            <option value="8" <%:   Convert.ToInt32(ViewBag.Response) == 8?"selected":""%>>Gateway Config Match</option>
                            <option value="9" <%:   Convert.ToInt32(ViewBag.Response) == 9?"selected":""%>>File Upload</option>
                            <option value="10" <%:  Convert.ToInt32(ViewBag.Response) == 10?"selected":""%>>Recorded Status</option>
                            <option value="11" <%:  Convert.ToInt32(ViewBag.Response) == 11?"selected":""%>>Startup Message</option>
                            <option value="250" <%: Convert.ToInt32(ViewBag.Response) == 250?"selected":""%>>Gateway BootLoader</option>
                            <option value="251" <%: Convert.ToInt32(ViewBag.Response) == 251?"selected":""%>>Sensor Bootloader</option>
                            <option value="252" <%: Convert.ToInt32(ViewBag.Response) == 252?"selected":""%>>Interpretor Load</option>
                        </select>
                        <br />
                        <a href="#" onclick="$('.rawMessage').show(); $('.parsedMessage').hide();">Show Raw</a>
                        <a href="#" onclick="$('.parsedMessage').show(); $('.rawMessage').hide(); ">Show Parsed</a>
                    </td>

                    <td id="gid">
                        <%: Html.TextBox("GatewayID", (string)ViewBag.GatewayID,(Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                    </td>

                    <td id="srch">
                        <%: Html.TextBox("Search", (string)ViewBag.Search,(Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                    </td>
                    <td>

                    </td>
                    <td>
                        <input type="submit" id="toFromSub" name="toFromSub" class="bluebutton" value="Filter" />
                    </td>
                    <td><a href="/Admin/OutboundDataPacket">Outbound Data Viewer</a></td>
                </tr>

            </table>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#FromMonths').datepicker().css({ "width": "95", "margin-bottom": "39px" });
                    $('#ToMonths').datepicker().css({ "width": "95", "margin-bottom": "39px" });
                    $('#gid #GatewayID').css({ "width": "95", "margin-bottom": "39px" });
                    $('#FromHours').css({ "margin-bottom": "39px" });
                    $('#FromMinutes').css({ "margin-bottom": "39px" });
                    $('#ToHours').css({ "margin-bottom": "39px" });
                    $('#ToMinutes').css({ "margin-bottom": "39px" });
                    $('#toFromSub').css({ "margin-bottom": "39px" });
                    $('#srch #Search').css({ "width": "187px", "margin-bottom": "39px" });
                    $('input[type=text]').click(function () {
                        $(this).select();
                    });
                });


            </script>
            <table class="inboundPacket" width="100%">
                <thead>
                    <tr>
                        <th width="20"></th>

                        <th>ReceivedDate
                        </th>

                        <th>Gateway ID
                        </th>

                        <th>Type
                        </th>

                        <th>Count
                        </th>

                        <th>Power
                        </th>

                        <th>Security
                        </th>

                        <th style="text-align:right;">More
                        </th>
                        <th width="20"></th>
                    </tr>
                </thead>
                <tbody>
                    <%if (Model != null)
                      { %>
                    <%foreach (var ibp in Model)
                      {
                          alt = !alt; %>
                    <tr class="inboundDataPacketDetails <%:alt ? "alt":"" %>">
                        <td width="20"></td>

                        <td title="inboundPacketID: <%: ibp.InboundPacketID %>">
                          <%:  ibp.ReceivedDate.AddHours(ViewBag.TimeFormat) %>
                        </td>
                        
                        <td><%: ibp.APNID%>
                        </td>

                        <td><%: ibp.Response %>
                        </td>

                        <td><%: ibp.MessageCount%>
                        </td>

                        <td><%: ibp.Power %>
                        </td>

                        <td title="<%:ibp.Security %>"><%: ibp.Security >1 ? "True" : "False" %>                       
                        </td>

                        <td style="text-align:right;"><%: ibp.More %>
                        </td>

                         <td width="20"></td>
                    </tr>


                    <!-- Detail Row Insert -->
                    
                    <tr class="holdInboundPacketDetails <%:alt ? "alt":"" %>"  style="display: none; border-top-width: 0px; width:100%;" >
                        <td></td>
                        <td colspan="7"  >
                            <input class="rawMessage" type="text" value="<%:ibp.Message.ToHex()%>" style="width: 100%;" />
                            <%if(ibp.Response == 0){ %>
                            <table style="width: 100%;">
                                <tr>
                                    <th style="width: 20px;" ></th>
                                    <th>Date</th>
                                    <th>ID</th>
                                    <th colspan="7">Type</th>
                                    <th></th>
                                    <th>RSSI</th>
                                    <th>Voltage</th>
                                    <th>ProfileID</th>
                                    <th>State</th>
                                    <th>Message</th>
                                    <th style="width: 20px;"></th>
                                </tr>
                                <%
                          int index = 0;
                          while (ibp.Payload.Length > index + 5)
                          {
                             
                              int MessageLength = ibp.Payload[index + 5].ToInt();

                              byte[] messagebuffer = new byte[MessageLength + 7];

                              if (ibp.Payload.Length < (index + MessageLength + 7))
                                  break;
                              
                              Array.Copy(ibp.Payload, index, messagebuffer, 0, MessageLength + 7);
                              index += MessageLength + 7;
                                %>

                                <tr class="rawMessage">
                                    <td></td>
                                    <td colspan="5"><%:messagebuffer.ToHex() %></td>
                                    <td></td>
                                </tr>
                                <%
                              if (MessageLength < 8)
                                  continue;



                              DateTime time = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(messagebuffer, 0));

                              switch (messagebuffer[7])
                              {
                                   
                                  case 0x55:
                                      byte[] messagepayload = new byte[MessageLength - 12];
                                      Array.Copy(messagebuffer, 18, messagepayload, 0, MessageLength - 12);
                                %>
                                <tr class="parsedMessage">
                                    <td></td>
                                    <td><%:time.AddHours(ViewBag.TimeFormat).ToShortDateString() %> <%:time.AddHours(ViewBag.TimeFormat).ToLongTimeString() %> UTC</td>
                                    <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                    <td>Data Message</td>
                                    <td colspan="7"><%:(messagebuffer[6] & 0x01) == 0x01 ? "Urgent" : "" %></td>
                                    <td><%:messagebuffer[12] - 256 %>/<%:messagebuffer[13] - 256 %></td>
                                    <td><%:(messagebuffer[14] + 150) / 100.0 %></td>
                                    <td><%:BitConverter.ToUInt16(messagebuffer,15) %></td>
                                    <td><%:messagebuffer[17] %></td>
                                    <td><%:messagepayload.ToHex() %></td>
                                    <td></td>
                                </tr>
                                <%
                                    break;
                                  case 0x56:
                                      byte[] messagepayload2 = new byte[MessageLength - 16];
                                      Array.Copy(messagebuffer, 22, messagepayload2, 0, MessageLength - 16);
                                      time = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(BitConverter.ToUInt32(messagebuffer, 12));
                                      time = time.AddHours(ViewBag.TimeFormat); 
                                %>
                                <tr class="parsedMessage">
                                    <td></td>
                                    <td><%:time.ToShortDateString() %> <%:time.ToLongTimeString() %></td>
                                    <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                    <td>Data Message DL</td>
                                    <td colspan="7"><%:(messagebuffer[6] & 0x01) == 0x01 ? "Urgent" : "" %></td>
                                    <td><%:messagebuffer[16] == 0 ? 0 : messagebuffer[16] - 256 %>/<%:messagebuffer[17] - 256 %></td>
                                    <td><%:(messagebuffer[18] + 150) / 100.0 %></td>
                                    <td><%:BitConverter.ToUInt16(messagebuffer,19) %></td>
                                    <td><%:messagebuffer[21] %></td>
                                    <td><%:messagepayload2.ToHex() %></td>
                                    <td></td>
                                </tr>
                                <%
                                    break;
                                  case 0x25:
                                %>
                                <tr class="parsedMessage">
                                    <td></td>
                                    <td><%:time.AddHours(ViewBag.TimeFormat).ToShortDateString() %> <%:time.AddHours(ViewBag.TimeFormat).ToLongTimeString() %></td>
                                    <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                    <td colspan="7">Join <%:messagebuffer[14] == 0 ? "Allowed":"Prohibited" %></td>
                                    <td></td>
                                </tr>
                                <%
                                      break;
                                  case 0x52:
                                %>
                                <tr class="parsedMessage">
                                    <td></td>
                                    <td><%:time.AddHours(ViewBag.TimeFormat).ToShortDateString() %> <%:time.AddHours(ViewBag.TimeFormat).ToLongTimeString() %></td>
                                    <td><%:BitConverter.ToUInt32(messagebuffer, 8)%></td>
                                    <td colspan="7">Parent Device: <%:BitConverter.ToUInt32(messagebuffer, 12)%></td>                                 
                                    <td></td>
                                </tr>
                                <%
                                      break;
                                      
                                  default:
                                      break;

                              }
                          }%>
                            </table>
                            <%}else{ %>
                                <div>Only 0 messages show parsed data</div>
                            <%}%>
                        </td>
                        <td></td>





                    </tr>
                    <!-- Detail Row End -->





                    <%}
                      }%>
                </tbody>
            </table>
            <%} %>
        </div>
        <div class="buttons"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

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
        });

    </script>
</asp:Content>
