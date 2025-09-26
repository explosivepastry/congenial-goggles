<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<long>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddDevice
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%

        Account Acnt = Account.Load(Model);

        CustomerPermissionType cpt = CustomerPermissionType.Find("Network_View");
        int SensorCount;


        List<CSNet> Networks = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(Model);// CSNet.LoadByAccountID(Model).Where(n => { return (MonnitSession.CurrentCustomer.Can(cpt, n.CSNetID) || (MonnitSession.IsCurrentCustomerReseller && MonnitSession.CurrentCustomer.AccountID != Model)); }).ToList();
        CSNet DefaultNetwork = Networks.Where(cs => { return cs.CSNetID == ViewBag.NetworkID; }).FirstOrDefault();
        if (DefaultNetwork == null && Networks.Count > 0)
            DefaultNetwork = Networks[0];

        List<SensorGroupSensorModel> sgsmList = iMonnit.ControllerBase.SensorControllerBase.GetSensorList(DefaultNetwork.AccountID, out SensorCount);

        MonnitSession.SensorListFilters.CSNetID = DefaultNetwork.CSNetID;
    %>
    <div id="addDeviceDiv" class="container-fluid">
        <div class="powertour-hook card_container col-12 shadow-sm rounded mb-4" id="hook-one" style="margin-top: 20px;">
            <h2 style="font-weight: bold;"><%: Html.TranslateTag("Network/DeviceList|Devices currently on Network - ","Devices currently on Network  ")%>-
                <span style="font-weight: normal; font-weight: normal;"><%=DefaultNetwork.Name %></span>
            </h2>
        </div>

<%--<div>
<h2><%: Html.TranslateTag("Network/DeviceList|Devices Added"," - Devices Added")%></h2>
</div>--%>

        <div class=" col-md-6 col-12 powertour-hook pe-md-2 mb-4" id="hook-two">

            <div class="x_panel shadow-sm rounded powertour-hook" id="hook-three">
                <div class="x_title">
                    <h2 style="font-weight: bold;"><%: Html.TranslateTag("Gateways","Gateways")%></h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="col-12">
                        <table class="table">
                            <tbody>
                                <%foreach (Gateway gate in DefaultNetwork.Gateways)
                                    {
                                        if (gate.GatewayTypeID == 10 || gate.GatewayTypeID == 11)//dont show wifi gateways here
                                            continue;

                                        string imagePath = Html.GetThemedContent("/images/good.png");
                                        if (gate.LastCommunicationDate == DateTime.MinValue)
                                            imagePath = Html.GetThemedContent("/images/sleeping.png");
                                        else if (gate.ReportInterval != double.MinValue && gate.LastCommunicationDate.AddMinutes(gate.ReportInterval * 2 + 1) < DateTime.UtcNow)//Missed more than one heartbeat + one minute to take drift into account
                                            imagePath = Html.GetThemedContent("/images/alert.png");
                        %>
                                <tr>
                                    <td>
                                        <img src="<%= imagePath %>" />
                                        <%= gate.Name %>
                                    </td>
                                    <td title="<%= gate.GatewayTypeID.ToString()%>">
                                        <%= gate.GatewayType.Name%><br />
                                    </td>
                                    <td>
                                        <%if (MonnitSession.CustomerCan("Network_Edit"))
                                            { %>
                                        <a href="/Network/RemoveGateway/<%:gate.GatewayID %>" onclick="removeGateway(this); return false;">
                                            <%=Html.GetThemedSVG("delete") %></a>
                                        <% } %>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-md-6 col-12 powertour-hook ps-md-2" id="hook-four">
            <div class="x_panel shadow-sm rounded">
                <div class="x_title">
                    <h2 style="font-weight: bold;"><%: Html.TranslateTag("Network/DeviceList|Wireless Sensors","Wireless Sensors")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content">
                    <div class="col-12">
                        <table class="table">
                            <tbody>
                                <%foreach (SensorGroupSensorModel sens in sgsmList)
                                    {
                                        string imagePath = "";
                                        switch (sens.Sensor.Status)
                                        {
                                            case Monnit.eSensorStatus.OK:
                                                imagePath = Html.GetThemedContent("/images/good.png");
                                                break;
                                            case Monnit.eSensorStatus.Warning:
                                                imagePath = Html.GetThemedContent("/images/alert.png");
                                                break;
                                            case Monnit.eSensorStatus.Alert:
                                                imagePath = Html.GetThemedContent("/images/alarm.png");
                                                break;
                                            //case Monnit.eSensorStatus.Inactive:
                                            //    imagePath = Html.GetThemedContent("/images/inactive.png");
                                            //    break;
                                            //case Monnit.eSensorStatus.Sleeping:
                                            //    imagePath = Html.GetThemedContent("/images/newicons/sleep.png");
                                            //    break;
                                            case Monnit.eSensorStatus.Offline:
                                                imagePath = Html.GetThemedContent("/images/sleeping.png");
                                                break;
                                        }
                        %>
                                <tr>
                                    <td>
                                        <img id="status_<%= sens.Sensor.SensorID %>" src="<%= imagePath %>" alt="<%: Title %>" />
                                        <%= sens.Sensor.SensorName %>
                                    </td>
                                    <td>
                                        <%=sens.Sensor.MonnitApplication.ApplicationName %>
                                        <br />
                                    </td>
                                    <td>
                                        <%if (MonnitSession.CustomerCan("Network_Edit"))
                                            {%>
                                        <a href="/Sensor/Remove/<%:sens.Sensor.SensorID%>" onclick="removeSensor(this); return false;" title="<%: Html.TranslateTag("Remove","Remove")%>">
                                            <%=Html.GetThemedSVG("delete") %></a>
                                        <%} %>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div style="text-align: center; float: right;" class="powertour-hook" id="hook-five">
        </div>

        <div class="clearfix"></div>
        <br>
        <div style="text-align: right;" class="powertour-hook" id="hook-six">

            <div class="col-12 dfac" style="justify-content: flex-end;">
                <span>
                    <a href="/Overview/Index" class="btn btn-primary"><%: Html.TranslateTag("Network/DeviceList|Continue","Continue")%></a>
                </span>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            <%if (DefaultNetwork != null)
        {%>
            if (!$('#Edit_<%: DefaultNetwork.CSNetID%>').is("visible"))
                $('#Edit_<%: DefaultNetwork.CSNetID%>').show();
            else {
                $('#Edit_<%: DefaultNetwork.CSNetID%>').hide();
                $('#addNetwork').hide();
            }

            $('#Edit_<%: DefaultNetwork.CSNetID%>').click(function () {
                if (!$('#Edit_<%: DefaultNetwork.CSNetID%>').is("visible")) {
                    $('#Edit_<%: DefaultNetwork.CSNetID%>').hide();
                    $('#addNetwork').hide();
                }
            });
            <%}%>


            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuAccount').addClass('currentPage');


            $('#networkSelect').change(function () {
                window.location.href = $(this).val();
            });
            var netID = <%: DefaultNetwork.CSNetID%>

                $('#Edit_network').click(function () {
                    $.get("/Network/Edit/" + netID, function (data) {

                        $('#networkDiv').html(data);

                    });
                });
        });

        var confirmRemoveGateway = "<%: Html.TranslateTag("Network/DetailList|Are you sure you want to remove this gateway from the network?","Are you sure you want to remove this gateway from the network?")%>";

        function removeGateway(anchor) {
            if (confirm(confirmRemoveGateway)) {
                $.get($(anchor).attr("href"), function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }

                    window.location.href = window.location.href;
                });
            }
        }

        var confirmRemoveSensor = "<%: Html.TranslateTag("Network/DetailList|Are you sure you want to remove this sensor from the network?","Are you sure you want to remove this sensor from the network?")%>";

        function removeSensor(anchor) {
            if (confirm(confirmRemoveSensor)) {
                $.get($(anchor).attr("href"), function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }

                    window.location.href = window.location.href;
                });
            }
        }

    </script>

</asp:Content>
