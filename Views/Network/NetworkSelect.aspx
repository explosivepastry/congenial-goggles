<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NetworkSelect
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        CSNet acctNetwork = ViewBag.Network;
    %>
    <div class="container-fluid mt-4">
        <div class="col-md-6 col-12 px-0 pe-md-3">
            <div class="x_panel shadow-sm rounded mb-4">
                <div class="card_container__top">
                    <div class="card_container__top__title dfac">
                        <svg xmlns="http://www.w3.org/2000/svg" style="margin-bottom: 5px;" width="22.005" height="22.005" viewBox="0 0 22.005 22.005">
                            <g id="ic_rss_feed_24px" transform="translate(-5.968 10.691) rotate(-45)">
                                <circle id="Ellipse_9" data-name="Ellipse 9" cx="2.18" cy="2.18" r="2.18" transform="translate(4 15.64)" class="card_container__icon-fill-1"></circle>
                                <path id="Path_6" data-name="Path 6" d="M4,4.44V7.27A12.731,12.731,0,0,1,16.73,20h2.83A15.565,15.565,0,0,0,4,4.44ZM4,10.1v2.83A7.076,7.076,0,0,1,11.07,20H13.9A9.9,9.9,0,0,0,4,10.1Z" class="card_container__icon-fill-1"></path>
                            </g>
                        </svg>
                        <div class="dfac" style="margin-left: 5px;">
                            <%: Html.TranslateTag("Network/AssignDevice|Select Network","Select Network")%>
                        </div>
                    </div>
                </div>
                <div class="x_content">
                    <form action="/Network/NetworkSelect/">
                        <div class="form-group">
                            <div class="aSettings__title"><%: Html.TranslateTag("Current Network","Current Network")%></div>
                            <div class="col-sm-9 powertour-hook" id="hook-one">
                                <select id="netSelect" class="form-select" style="max-width: 300px;">
                                    <%foreach (CSNet net in Model)
                                        { %>
                                    <option <%=net.CSNetID == acctNetwork.CSNetID ? "selected" : "" %> value="<%=net.CSNetID %>"><%=net.Name %></option>
                                    <%} %>
                                </select>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3 col-12" for="messageBox">
                            </label>
                            <div class="col-sm-6 col-12" id="messageBox" style="color: red">
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="form-group">
                            <div class="">
                                <a id="addDeviceBtn">
                                    <input class="btn btn-primary btn-sm me-2" type="button" value="<%: Html.TranslateTag("Network/NetworkSelect|Add One Device","Add One Device")%>">
                                </a>
                                <a id="multiDeviceBtn">
                                    <input class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/NetworkSelect|Upload CSV","Upload CSV")%>">
                                </a>
                                <% if (MonnitSession.CustomerCan("Gateway_Create") && MonnitSession.CustomerCan("Sensor_Create"))
                                    { %>
                                <a id="xmlDeviceBtn">
                                    <input class="btn btn-secondary btn-sm" type="button" value="<%: Html.TranslateTag("Network/NetworkSelect|Upload XML","Upload XML")%>">
                                </a>
                                <% }%>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

        <%if (MonnitSession.CustomerCan("Support_Advanced"))
            { %>
        <input type="hidden" id="CSNetID" value="<%= acctNetwork.CSNetID %>" />
        <input type="hidden" id="AccountID" value="<%= ViewBag.AccountID %>" />
        <div class="col-md-6 col-12 px-0 ps-md-3">
            <div class="x_panel shadow-sm rounded shadow-sm">
                <div class="card_container__top">
                    <div class="card_container__top__title col-6" style="font-weight: bold;"><%: Html.TranslateTag("Administration","Administration")%></div>
                </div>
                <div class="x_content">
                    <div class="form-group">
                        <%if (MonnitSession.CustomerCan("Gateway_Can_Change_Network"))
                            { %>
                        <div class="">
                            <div style="font-size: 1.2em;"><%: Html.TranslateTag("Gateway ID","Gateway ID")%> :</div>
                            <div class="input-group">
                                <input class="form-control" style="max-width: 200px;" type="text" name="QuickGatewayID" id="QuickGatewayID" />
                                <button type="button" onclick="checkGatewayMove();" class="btn btn-primary" value="Assign">Assign</button>
                            </div>
                        </div>
                        <%}%>
                        <%if (MonnitSession.CustomerCan("Sensor_Can_Change_Network"))
                            { %>
                        <div class="">
                            <div style="font-size: 1.2em;"><%: Html.TranslateTag("Sensor ID","Sensor ID")%> :</div>
                            <div class="input-group">
                                <input class="form-control" style="max-width: 200px;" type="text" name="QuickSensorID" id="QuickSensorID" />
                                <button type="button" onclick="checkSensorMove();" class="btn btn-primary" value="Assign">Assign</button>
                            </div>
                        </div>
                        <%}%>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%} %>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#QuickSensorID").keypress(function (e) {
                if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
                    checkSensorMove();
                    return false;
                }
            });

            $("#QuickGatewayID").keypress(function (e) {
                if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
                    checkGatewayMove();
                    return false;
                }
            });

            if (queryString("result") == "SensorAdded") {
                $("#QuickSensorID").focus();
            }

            if (queryString("result") == "GatewayAdded") {
                $("#QuickGatewayID").focus();
            }

            $('#netSelect').change(function (e) {
                $('#CSNetID').val(Number($('#netSelect').val()));

            });

            $('#addDeviceBtn').click(function () {
                window.location.href = "/Setup/AssignDevice/<%= ViewBag.AccountID %>?networkID=" + $("#netSelect").val();
            });

            $('#multiDeviceBtn').click(function () {
                window.location.href = "/Network/AssignMultipleDevices/<%= ViewBag.AccountID %>?networkID=" + $("#netSelect").val();;
            });

            $('#xmlDeviceBtn').click(function () {
                window.location.href = "/Network/XmlDeviceAdd/<%=acctNetwork.AccountID%>?networkID=" + $("#netSelect").val();;
            });


        });

        function checkSensorMove() {

            if ($('#QuickSensorID').val().includes(":")) {
                var stringArr = $('#QuickSensorID').val().split(":");
                var sensorID = stringArr[0];
                $('#QuickSensorID').val(sensorID);
                $('#QuickSensorID').focus();
            }
            $.get('/Network/CheckSensorMove/' + $('#QuickSensorID').val(), function (data) {
                eval(data);
                $('#QuickSensorID').select();
                $('#QuickSensorID').focus();
            });
        }

        function checkGatewayMove() {

            if ($('#QuickGatewayID').val().includes(":")) {
                var stringArr = $('#QuickGatewayID').val().split(":");
                var gatewayID = stringArr[0];
                $('#QuickGatewayID').val(gatewayID);
                $('#QuickGatewayID').focus();
            }
            $.get('/Network/CheckGatewayMove/' + $('#QuickGatewayID').val(), function (data) {
                eval(data);
                $('#QuickGatewayID').select();
                $('#QuickGatewayID').focus();
            });
        }

        function selectNetwork() {

            netID = $('#netSelect').val()

            if (netID == -1) {
                $('#messageBox').html("Please Select a Network");
            } else {

                var accID = '<%=ViewBag.AccountID%>';
            }
        }
    </script>

</asp:Content>
