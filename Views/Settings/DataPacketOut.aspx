<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.OutboundPacket>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DataPacketOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <form action="/Settings/DataPacketOut/" method="post" id="dataPacketOutForm">
            <% string temp = ""; %>
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

            <div class="col-12">
                <div class="x_panel shadow-sm rounded mt-4">
                    <div class="x_title">
                        <div class="card_container__top__title"><%=Html.GetThemedSVG("list") %> &nbsp;<%: Html.TranslateTag("Settings/DataPacketOut|Outbound Data Packet","Outbound Data Packet")%></div>
                        <div class="nav navbar-right panel_toolbox">
                        </div>

                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">

                        <div class="col-12">
                            <div class="x_panel">
                                <div class="clearfix"></div>

                                <div class="form-group">

                                    <div class="bold col-12">

                                        <div id="datePickMobi"></div>

                                        <div class="row sensorEditForm">

                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("From","From")%>: 
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <input class="form-control" id="Mobi_startDate" name="startDate" />
                                                <%--value="<%=ViewBag.Displayfrom %>"--%>
                                            </div>
                                        </div>


                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("To","To")%>: 
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <input class="form-control" id="Mobi_endDate" name="endDate" />
                                                <%--value="<%=ViewBag.Displayto %>" --%>
                                            </div>
                                        </div>

                                        <div class="row sensorEditForm">
                                            <div class="col-12 col-md-3">
                                                <%: Html.TranslateTag("Time Offset","Time Offset")%>:
                                            </div>
                                            <div class="col sensorEditFormInput">
                                                <select id="TimeFormat" name="TimeFormat" class="form-select">
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
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3">
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
                                        <select id="Response" name="Response" class="form-select">
                                            <option value="<%: int.MinValue %>" <%: Convert.ToInt32(ViewBag.Response) == int.MinValue ? "selected":""%>><%: Html.TranslateTag("Settings/DataPacketOut|All Responses","All Responses")%></option>
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
                                        </select>
                                    </div>
                                </div>

                                <div class="row sensorEditForm">
                                    <div class="col-12 col-md-3">
                                        <%: Html.TranslateTag("Count","Count")%>:
                                    </div>
                                    <div class="col sensorEditFormInput">
                                        <select id="Count" name="Count" class="form-select">
                                            <option value="10" <%: Convert.ToInt32(ViewBag.Count) == 10?"selected":""%>>10</option>
                                            <option value="25" <%: Convert.ToInt32(ViewBag.Count) == 25?"selected":""%>>25</option>
                                            <option value="50" <%: Convert.ToInt32(ViewBag.Count) == 50?"selected":""%>>50</option>
                                            <option value="75" <%: Convert.ToInt32(ViewBag.Count) == 75?"selected":""%>>75</option>
                                            <option value="100" <%: Convert.ToInt32(ViewBag.Count) == 100?"selected":""%>>100</option>
                                            <option value="500" <%: Convert.ToInt32(ViewBag.Count)  == 500?"selected":""%>>500</option>
                                        </select>
                                    </div>
                                </div>

                                <button type="submit" id="toFromSub" name="toFromSub" class="btn btn-primary mt-1" value="Filter">
                                    Filter
                                </button>

                                <a href="/Settings/DataPacketOutCSV/" title="<%: Html.TranslateTag("Export","Export")%>" class="ms-2 helpIco" id="exportOutboundDataPackets" style="cursor: pointer;">
                                    <%=Html.GetThemedSVG("download-file") %>
                                </a>
                            </div>
                            <div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="x_panel">
                                <div class="clearfix"></div>
                                <table class="inboundPacket" width="100%">
                                    <thead>
                                        <tr>
                                            <th width="20"></th>
                                            <th><%: Html.TranslateTag("Settings/DataPacketOut|Sent Date","Sent Date")%>
                                            </th>

                                            <th class="extra"><%: Html.TranslateTag("Gateway ID","Gateway ID")%>
                                            </th>

                                            <th><%: Html.TranslateTag("Type","Type")%>
                                            </th>

                                            <th><%: Html.TranslateTag("Count","Count")%>
                                            </th>

                                            <th><%: Html.TranslateTag("Settings/DataPacketOut|PayLoad","PayLoad")%>
                                            </th>

                                            <th><%: Html.TranslateTag("Power","Power")%>
                                            </th>

                                            <th class="extra"><%: Html.TranslateTag("More","More")%>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%foreach (var ibp in Model)
                                            {
                                                DateTime sentDate = ibp.SentDate.AddHours(ViewBag.TimeFormat);
                                        %>
                                        <tr class="OutboundPacket" title="Outbound Packet GUID: <%:ibp.OutboundPacketGUID %>">
                                            <td width="20"></td>
                                            <td class="dataPacketField">
                                                <%:sentDate.ToDateFormatted(MonnitSession.CurrentCustomer.Preferences["Date Format"]) 
                                                    + " " + sentDate.ToTimeFormatted(MonnitSession.CurrentCustomer.Preferences["Time Format"]) %>
                                            </td>
                                            <td class="extra dataPacketField">
                                                <%: ibp.APNID%>
                                            </td>
                                            <td class="dataPacketField">
                                                <%: ibp.Response %>
                                            </td>

                                            <td class="dataPacketField">
                                                <%: ibp.MessageCount %>
                                            </td>

                                            <%
                                                int count = 0;
                                                foreach (var mess in ibp.Payload)
                                                {
                                                    count++;
                                                    if (count <= 2)
                                                        continue;

                                                    temp += mess.ToString("X2").Replace("-", "");

                                                } %>
                                            <td class="dataPacketField">
                                                <input type="text" value="<%:temp %>" />
                                                <% temp = ""; %>
                                            </td>

                                            <td class="dataPacketField">
                                                <%: ibp.Power %>
                                            </td>

                                            <td class="extra dataPacketField">
                                                <%: ibp.More %>
                                            </td>

                                        </tr>

                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </form>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {
            var startDate = '<%=ViewBag.from%>';
            var endDate = '<%=ViewBag.to%>';
            var startArr = startDate.split('-');
            var endArr = endDate.split('-');

            //$('#datePickMobi').mobiscroll().datepicker({
            //	theme: 'ios',
            //	display: popLocation,
            //	controls: ['calendar', 'time'],
            //	select: 'range',
            //	defaultSelection: [new Date(startArr[0], startArr[1] - 1, startArr[2], startArr[3], startArr[4]), new Date(endArr[0], endArr[1] - 1, endArr[2], endArr[3], endArr[4])],
            //	startInput: "#Mobi_startDate",
            //	endInput: "#Mobi_endDate",
            //});

            //$('#Mobi_startDate').mobiscroll().datepicker({
            //	theme: 'ios',
            //	display: popLocation,
            //	controls: ['calendar', 'time'],
            //});

            //$('#Mobi_endDate').mobiscroll().datepicker({
            //	theme: 'ios',
            //	display: popLocation,
            //	controls: ['calendar', 'time'],
            //});

            let fromDt = new Date("<%= ViewBag.Displayfrom %>");
            let toDt = new Date("<%= ViewBag.Displayto %>");
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

            //$('.outboundDataPacketDetails').click(function () {
            //    var tr = $(this).next();
            //    var hide = tr.is(":visible");
            //    $('.outboundDataPacketDetails').css('border-bottom-width', '1px');
            //    $('.holdOutboundPacketDetails').hide();
            //    if (!hide) {
            //        $(this).css('border-bottom-width', '0px');
            //        tr.show();
            //    }
            //}).css('cursor', 'pointer');
            //$('.rawMessage').hide();

            $('#exportOutboundDataPackets').click(function (e) {
                e.preventDefault();

                $('#dataPacketOutForm').attr('action', '/Settings/DataPacketOutCSV/');
                $('#dataPacketOutForm').submit();
                setTimeout(function () {
                    $('#dataPacketOutForm').attr('action', '/Settings/DataPacketOut/');
                }, 500); // half second
            });

        });
    </script>
</asp:Content>
