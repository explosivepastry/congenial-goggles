<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.SensorMessageAudit.SensorMessageAuditCountModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Message Audit look up

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <form action="SensorMessageAuditLookUp" method="post">
        <div id="fullForm" style="width: 100%">
            <div class="formtitle">
                Sensor Message Audit
            </div>
            <table class="DateTimeSensor" width="100%">

                <tr style="width: 100%">
                    <th width="20"></th>
                    <th colspan="2">From: Day and Time
                    </th>
                    <th colspan="2"></th>
                    <th colspan="3">To: Day and Time
                    </th>
                </tr>
                <tr class="activeTime">
                    <td></td>

                    <td colspan="2">
                        <%: Html.TextBox("FromMonths",(string)ViewBag.DatefromString,(Dictionary<string,object>)ViewData["HtmlAttributes"]) %>
                        <select class="tzSelect" id="FromHours" name="FromHours" style="width: 50px;">
                            <option value="0" <%:  ViewBag.fromHours == 0 ?"selected":""%>>0</option>
                            <option value="1" <%:  ViewBag.fromHours ==  1 ?"selected":""%>>1</option>
                            <option value="2" <%:  ViewBag.fromHours ==  2 ?"selected":""%>>2</option>
                            <option value="3" <%:  ViewBag.fromHours ==  3 ?"selected":""%>>3</option>
                            <option value="4" <%:  ViewBag.fromHours ==  4 ?"selected":""%>>4</option>
                            <option value="5" <%:  ViewBag.fromHours ==  5 ?"selected":""%>>5</option>
                            <option value="6" <%:  ViewBag.fromHours ==  6 ?"selected":""%>>6</option>
                            <option value="7" <%:  ViewBag.fromHours ==  7 ?"selected":""%>>7</option>
                            <option value="8" <%:  ViewBag.fromHours ==  8 ?"selected":""%>>8</option>
                            <option value="9" <%:  ViewBag.fromHours ==  9 ?"selected":""%>>9</option>
                            <option value="10" <%:  ViewBag.fromHours ==  10 ?"selected":""%>>10 </option>
                            <option value="11" <%:  ViewBag.fromHours ==  11 ?"selected":""%>>11 </option>
                            <option value="12" <%:  ViewBag.fromHours ==  12 ?"selected":""%>>12 </option>
                            <option value="13" <%:  ViewBag.fromHours == 13 ?"selected":""%>>13</option>
                            <option value="14" <%:  ViewBag.fromHours == 14 ?"selected":""%>>14</option>
                            <option value="15" <%:  ViewBag.fromHours == 15 ?"selected":""%>>15</option>
                            <option value="16" <%:  ViewBag.fromHours == 16 ?"selected":""%>>16</option>
                            <option value="17" <%:  ViewBag.fromHours == 17 ?"selected":""%>>17</option>
                            <option value="18" <%:  ViewBag.fromHours == 18 ?"selected":""%>>18</option>
                            <option value="19" <%:  ViewBag.fromHours == 19 ?"selected":""%>>19</option>
                            <option value="20" <%:  ViewBag.fromHours == 20 ?"selected":""%>>20</option>
                            <option value="21" <%:  ViewBag.fromHours == 21 ?"selected":""%>>21</option>
                            <option value="22" <%:  ViewBag.fromHours == 22 ?"selected":""%>>22 </option>
                            <option value="23" <%:  ViewBag.fromHours == 23 ?"selected":""%>>23 </option>
                        </select>: 
                          <select class="tzSelect" id="FromMinutes" name="FromMinutes" style="width: 50px;">
                               <option value="0" <%:  ViewBag.fromMinutes == 0 ?"selected":""%>>00</option>
                               <option value="5" <%:  ViewBag.fromMinutes ==  5 ?"selected":""%>>05</option>
                              <option value="10" <%:  ViewBag.fromMinutes == 10 ?"selected":""%>>10</option>
                              <option value="15" <%:  ViewBag.fromMinutes == 15 ?"selected":""%>>15</option>
                              <option value="20" <%:  ViewBag.fromMinutes == 20 ?"selected":""%>>20</option>
                              <option value="25" <%:  ViewBag.fromMinutes == 25 ?"selected":""%>>25</option>
                              <option value="30" <%:  ViewBag.fromMinutes == 30 ?"selected":""%>>30</option>
                              <option value="35" <%:  ViewBag.fromMinutes == 35 ?"selected":""%>>35</option>
                              <option value="40" <%:  ViewBag.fromMinutes == 40 ?"selected":""%>>40</option>
                              <option value="45" <%:  ViewBag.fromMinutes == 45 ?"selected":""%>>45</option>
                              <option value="50" <%:  ViewBag.fromMinutes == 50 ?"selected":""%>>50</option>
                              <option value="55" <%:  ViewBag.fromMinutes == 55 ?"selected":""%>>55</option>
                          </select>
                    </td>
                    <td colspan="2">

                    </td>
                    <td colspan="2">
                        <%: Html.TextBox("ToMonths",(string)ViewBag.DatetoString,(Dictionary<string,object>)ViewData["HtmlAttributes"]) %>
                        <select class="tzSelect" id="ToHours" name="ToHours" style="width: 50px;">
                            <option value="0" <%:  ViewBag.toHours == 0 ?"selected":""%>>0</option>
                            <option value="1" <%:  ViewBag.toHours ==  1 ?"selected":""%>>1</option>
                            <option value="2" <%:  ViewBag.toHours ==  2 ?"selected":""%>>2</option>
                            <option value="3" <%:  ViewBag.toHours ==  3 ?"selected":""%>>3</option>
                            <option value="4" <%:  ViewBag.toHours ==  4 ?"selected":""%>>4</option>
                            <option value="5" <%:  ViewBag.toHours ==  5 ?"selected":""%>>5</option>
                            <option value="6" <%:  ViewBag.toHours ==  6 ?"selected":""%>>6</option>
                            <option value="7" <%:  ViewBag.toHours ==  7 ?"selected":""%>>7</option>
                            <option value="8" <%:  ViewBag.toHours ==  8 ?"selected":""%>>8</option>
                            <option value="9" <%:  ViewBag.toHours ==  9 ?"selected":""%>>9</option>
                            <option value="10" <%:  ViewBag.toHours ==  10 ?"selected":""%>>10 </option>
                            <option value="11" <%:  ViewBag.toHours ==  11 ?"selected":""%>>11 </option>
                            <option value="12" <%:  ViewBag.toHours ==  12 ?"selected":""%>>12 </option>
                            <option value="13" <%:  ViewBag.toHours == 13 ?"selected":""%>>13</option>
                            <option value="14" <%:  ViewBag.toHours == 14 ?"selected":""%>>14</option>
                            <option value="15" <%:  ViewBag.toHours == 15 ?"selected":""%>>15</option>
                            <option value="16" <%:  ViewBag.toHours == 16 ?"selected":""%>>16</option>
                            <option value="17" <%:  ViewBag.toHours == 17 ?"selected":""%>>17</option>
                            <option value="18" <%:  ViewBag.toHours == 18 ?"selected":""%>>18</option>
                            <option value="19" <%:  ViewBag.toHours == 19 ?"selected":""%>>19</option>
                            <option value="20" <%:  ViewBag.toHours == 20 ?"selected":""%>>20</option>
                            <option value="21" <%:  ViewBag.toHours == 21 ?"selected":""%>>21</option>
                            <option value="22" <%:  ViewBag.toHours == 22 ?"selected":""%>>22 </option>
                            <option value="23" <%:  ViewBag.toHours == 23 ?"selected":""%>>23 </option>
                        </select>: 
                          <select class="tzSelect" id="ToMinutes" name="ToMinutes" style="width: 50px;">
                               <option value="0" <%:  ViewBag.toMinutes == 0 ?"selected":""%>>00</option>
                               <option value="5" <%:  ViewBag.toMinutes ==  5 ?"selected":""%>>05</option>
                              <option value="10" <%:  ViewBag.toMinutes == 10 ?"selected":""%>>10</option>
                              <option value="15" <%:  ViewBag.toMinutes == 15 ?"selected":""%>>15</option>
                              <option value="20" <%:  ViewBag.toMinutes == 20 ?"selected":""%>>20</option>
                              <option value="25" <%:  ViewBag.toMinutes == 25 ?"selected":""%>>25</option>
                              <option value="30" <%:  ViewBag.toMinutes == 30 ?"selected":""%>>30</option>
                              <option value="35" <%:  ViewBag.toMinutes == 35 ?"selected":""%>>35</option>
                              <option value="40" <%:  ViewBag.toMinutes == 40 ?"selected":""%>>40</option>
                              <option value="45" <%:  ViewBag.toMinutes == 45 ?"selected":""%>>45</option>
                              <option value="50" <%:  ViewBag.toMinutes == 50 ?"selected":""%>>50</option>
                              <option value="55" <%:  ViewBag.toMinutes == 55 ?"selected":""%>>55</option>
                          </select>
                    </td>

                </tr>



                <tr style="width: 100%">
                    <td width="20"></td>
                    <td colspan="2">SensorID:
                        <input type="text" class="editFieldMedium" name="SensorID" value="<%=ViewBag.SensorID %>" />
                    </td>
                    <td colspan="2">
                        <table>
                            <tr>
                                <th></th>
                                <th>Event</th>
                                <th>Success</th>
                                <th>Total</th>
                                <th></th>
                            </tr>
                            <%
                                if (Model != null && Model.Count > 0)
                                    foreach (SensorMessageAudit.SensorMessageAuditCountModel auditCount in Model)
                                    { %>

                            <tr>

                                <td colspan="2"><a class="getinfo" href="/Admin/AuditDetails?sensorID=<%=ViewBag.SensorID %>&fromDate=<%=ViewBag.from %>&toDate=<%=ViewBag.to%>&eventType=<%=auditCount.EventType %>"><%= auditCount.EventType %></a></td>
                                <td><%= auditCount.Success%></td>
                                <td><%= auditCount.Total %></td>
                                <td></td>
                            </tr>
                            <%} %>
                        </table>
                    </td>
                    <td colspan="3"></td>
                </tr>

                <tr>
                    <td></td>
                    <td>
                        <input type="submit" id="toFromSub" name="toFromSub" class="bluebutton" value="Filter" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>

            </table>
        </div>
    </form>


    <div id="CountReturn">
    </div>




    <script type="text/javascript">
        $(document).ready(function () {
            $('#FromMonths').datepicker().css({ "width": "95", "margin-bottom": "39px" });
            $('#ToMonths').datepicker().css({ "width": "95", "margin-bottom": "39px" });
            $('#SensorID').css({ "width": "95", "margin-bottom": "39px" });
            $('#FromHours').css({ "margin-bottom": "39px" });
            $('#FromMinutes').css({ "margin-bottom": "39px" });
            $('#ToHours').css({ "margin-bottom": "39px" });
            $('#ToMinutes').css({ "margin-bottom": "39px" });
            $('#toFromSub').css({ "margin-bottom": "39px" });

        });




        $('.getinfo').click(function () {
            var href = $(this).attr('href');
            $.get(href, function (data) {
                $('#CountReturn').html(data);
            });
            return false;
        });


    </script>

</asp:Content>
