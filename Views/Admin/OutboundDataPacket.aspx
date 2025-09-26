<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.OutboundPacket>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Outbound Data Packet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="fullForm" style="width: 100%">

        <% using (Html.BeginForm())
           { %>
        <% string temp = ""; %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <div class="formtitle">
            Outbound Data Packet Information
            <label style="padding-left: 479px; font-size: xx-small;">Records to Show: </label>
            <select id="Count" name="Count" style="width: 72px;">
                <option value="10" <%: Convert.ToInt32(ViewBag.Count) == 10?"selected":""%>>10</option>
                <option value="25" <%: Convert.ToInt32(ViewBag.Count) == 25?"selected":""%>>25</option>
                <option value="50" <%: Convert.ToInt32(ViewBag.Count) == 50?"selected":""%>>50</option>
                <option value="75" <%: Convert.ToInt32(ViewBag.Count) == 75?"selected":""%>>75</option>
                <option value="100" <%: Convert.ToInt32(ViewBag.Count) == 100?"selected":""%>>100</option>
                <option value="500" <%: Convert.ToInt32(ViewBag.Count)  == 500?"selected":""%>>500</option>
            </select>
        </div>

        <div>
            <table class="DateTimeGateway" width="100%">

                <tr style="width: 100%">
                    <th width="20">
                    </th>
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
                        <%: Html.DropDownList("FromHours", (SelectList)ViewData["FromHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
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
                    <th>Response
                    </th>
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
                            <option value="0" <%:   Convert.ToInt32(ViewBag.Response) == 0?"selected":""%>>Data Ack</option>
                            <option value="1" <%:   Convert.ToInt32(ViewBag.Response) == 1?"selected":""%>>Sensor Message</option>
                            <option value="2" <%:   Convert.ToInt32(ViewBag.Response) == 2?"selected":""%>>Send Sensor List</option>
                            <option value="3" <%:   Convert.ToInt32(ViewBag.Response) == 3?"selected":""%>>Update Gateway Config</option>
                            <option value="4" <%:   Convert.ToInt32(ViewBag.Response) == 4?"selected":""%>>Reform Network</option>
                            <option value="5" <%:   Convert.ToInt32(ViewBag.Response) == 5?"selected":""%>>Reset Ack</option>
                            <option value="6" <%:   Convert.ToInt32(ViewBag.Response) == 6?"selected":""%>>Location Ack</option>
                            <option value="7" <%:   Convert.ToInt32(ViewBag.Response) == 7?"selected":""%>>WiFi Sensor Lookup</option>
                            <option value="8" <%:   Convert.ToInt32(ViewBag.Response) == 8?"selected":""%>>Gateway Config Ack</option>
                            <option value="9" <%:   Convert.ToInt32(ViewBag.Response) == 9?"selected":""%>>File Upload</option>
                            <option value="10" <%:  Convert.ToInt32(ViewBag.Response) == 10?"selected":""%>>Recorded Status</option>
                            <option value="11" <%:  Convert.ToInt32(ViewBag.Response) == 11?"selected":""%>>Startup Ack</option>
                            <option value="250" <%: Convert.ToInt32(ViewBag.Response) == 250?"selected":""%>>Gateway BootLoader</option>
                            <option value="251" <%: Convert.ToInt32(ViewBag.Response) == 251?"selected":""%>>Sensor Bootloader</option>
                            <option value="252" <%: Convert.ToInt32(ViewBag.Response) == 252?"selected":""%>>Interpretor Load</option>
                        </select></td>

                    <td id="gid">
                        <%: Html.TextBox("GatewayID",(string)ViewBag.GatewayID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                    </td>

                    <td id="srch">
                        <%: Html.TextBox("Search", (string)ViewBag.Search,(Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                    </td>
                    <td>
                        <input type="submit" id="toFromSub" class="bluebutton" value="Filter" />
                    </td>
                    <td><a href="/Admin/InboundDataPacket">Inbound Data Packet Viewer</a></td>
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
                        <th>Sent Date
                        </th>

                        <th>Gateway ID
                        </th>

                        <th>Type
                        </th>

                        <th>Count
                        </th>

                        <th>PayLoad
                        </th>

                        <th>Power
                        </th>

                        <th>More
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach (var ibp in Model)
                      {  %>
                    <tr class="OutboundPacket">
                        <td width="20"></td>
                        <td title="OutboundPacketID: <%:ibp.OutboundPacketID %>">
                            <%:  ibp.SentDate.AddHours(ViewBag.TimeFormat) %>
                        </td>
                        <td>
                            <%: ibp.APNID%>
                        </td>
                        <td>
                            <%: ibp.Response %>
                        </td>

                        <td>
                            <%: ibp.MessageCount %>
                        </td>

                        <%
                      int count = 0;
                      foreach (var mess in ibp.Payload)
                      {
                          count++;
                          if (count <=2)
                              continue;
                          
                          temp += mess.ToString("X2").Replace("-", "");
                          
                      } %>
                        <td>
                            <input type="text" value="<%:temp %>" />
                            <% temp = ""; %>
                        </td>

                        <td>
                            <%: ibp.Power %>
                        </td>

                        <td>
                            <%: ibp.More %>
                        </td>






                    </tr>

                    <%--<tr class="holdInboundPacketDetails" style="display: none; border-top-width: 0px;">

                        <td colspan="8" style="padding: 30px;">
                            <%
                               int index = 0;
                               
                              
                               while (ibp.Payload.Length > index + 1)
                               {
                                   temp = string.Empty;

                                   int MessageLength = ibp.Payload[index + 1].ToInt() + 3;

                                    byte[] messagebuffer  = new byte[MessageLength];
                                    if (ibp.Payload.Length < (index + MessageLength))
                                        break;
                                       
                                   Array.Copy(ibp.Payload, index, messagebuffer, 0, MessageLength);

                                   //UInt32 time = BitConverter.ToUInt32(messagebuffer, 0);
                                   //msg.MessageDate = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(time); breaks out the date and time of the message
                                   index += MessageLength;
                                   
                                   foreach (var mess in messagebuffer)
                                    {
                                         temp += mess.ToString("X2").Replace("-", "");
                                    }
                                   
                            %>

                            <span><%:temp %></span><br />

                            <%}
                              %>  
                      
                              
                        </td>






                    </tr>--%>
                    <!-- Detail Row End -->
                    <%} %>
                </tbody>
            </table>
            <%} %>
        </div>
        <div class="buttons"></div>
    </div>
    <%--<script type="text/javascript">
         $(document).ready(function () {

             $('.OutboundPacket').click(function () {
                 alert("hello");
                 var tr = $(this).next();
                 var hide = tr.is(":visible");

                 $('.OutboundPacket').css('border-bottom-width', '1px');
                 $('.holdInboundPacketDetails').hide();

                 if (!hide) {
                     $(this).css('border-bottom-width', '0px');
                     tr.show();
                 }

             }).css('cursor', 'pointer');
         });

    </script>--%>
</asp:Content>

