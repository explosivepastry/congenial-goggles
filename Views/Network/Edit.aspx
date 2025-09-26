<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.CSNet>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        
        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string response = "";
        List<CSNet> networks = ViewBag.networks;
        response = ViewBag.EditResponse == null ? "" : ViewBag.EditResponse;
    %>

    <div class="container-fluid">

        <!-- Event Info Panel -->
        <div class="d-flex">
            <div class="rule-card_container " style="flex-basis: 28rem; margin-bottom: 1rem;">
                <div class="card_container__top">
                    <div class="card_container__top__title " id="hook-one" style="gap: 1rem;">
                        <span style="font-weight: 800;" class="me-1"><%: Html.TranslateTag("Network","Network")%>:</span>
                        <%=Model != null ? Model.Name : "&nbsp;"%>

                        <% if (Model.Gateways.Count >= 0 && Model.Sensors.Count >= 0)
                            {%>
                        <a title="<%: Html.TranslateTag("Network/Edit|Delete","Delete")%>" onclick="deleteNetwork()" class="" style="margin-left: auto;">

                            <%=Html.GetThemedSVG("delete") %>
                        </a>
                        <%} %>
                    </div>
                </div>
                <div class="x_content">
                    <form action="/Network/Edit/<%:Model.CSNetID %>" method="post" id="networkEditForm">
                        <%=Html.ValidationSummary(true) %>
                        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                            <div class="form-group " style="align-items: center; display: flex; gap: 1rem;">
                                <label class="" for="networkName">
                                    <%: Html.TranslateTag("Name","Name")%>
                                </label>
                                <div class="">
                                    <input type="text" id="networkName" name="networkName" required="required" value="<%=Model.Name %>" class="form-control user-dets" style="width: 200px">
                                </div>
                            </div>
                            <% if (MonnitSession.CustomerCan("Notification_Disable_Network") && MonnitSession.CustomerCan("Notification_Edit"))
                                {%>

                            <div class="form-group d-flex justify-content-around" style="margin-top: 1em; width: 40%;">
                                <label class=" " style="width: 50%;" for="networkName">
                                    <%: Html.TranslateTag("Network/Edit|Rules Enabled","Rules Enabled")%>
                                </label>
                                <div class="">
                                    <input type="checkbox" id="sendNotification" name="sendNotification" <%= Model.SendNotifications ? "checked='checked'" : ""  %> value="true">
                                </div>
                            </div>
                        <%}%>
                        <div class="form-group d-flex justify-content-around" style="margin-top: 1em; width: 40%;">
                            <label class=" " style="width: 50%;" for="networkName">
                                <%: Html.TranslateTag("Network/Edit|Holding Enabled","Holding Enabled")%>
                            </label>
                            <div class="">
                                <input type="checkbox" id="holdingNetwork" name="holdingNetwork" <%= Model.HoldingOnlyNetwork ? "checked='checked'" : ""  %> value="true">
                            </div>
                        </div>
                        <div class="form-group  d-flex justify-content-around flex-column" style="margin-top: 1em;">
                            <label class="" style="margin-top: 10px; width: 80%;" for="externalAccess">
                                <%: Html.TranslateTag("Network/Edit|Install Tech Access Cut-off Date","Install Tech Access Cut-off Date")%>:
                            </label>

                            <%if (Model.ExternalAccessUntil == DateTime.MinValue)
                                {
                                    Model.ExternalAccessUntil = DateTime.UtcNow.AddDays(-1);
                                }%>

                            <div class="" style="margin-top: 10px; display: flex; align-items: center;">
                                <input id="externalAccess" name="externalAccess" value="<%=MonnitSession.CurrentCustomer.FormatLocalDateTime(Model.ExternalAccessUntil) %>" class="date-pick-noIcon" style="cursor: pointer;">
                            </div>
                        </div>

                        <div class="form-group">
                            <span class="dfac justify-content-end" style="margin-top: 10px;">
                                <button id="SaveBtn" onclick="$(this).hide();$('#saving').show();" type="submit" class="btn btn-primary me-2">
                                    <%: Html.TranslateTag("Save","Save")%>
                                </button>

                                <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    <%: Html.TranslateTag("Network/Edit|Saving...","Saving...")%>
                                </button>
                            </span>
                        </div>
                    </form>
                </div>
            </div>
        </div>

        <div class="d-flex" style="gap: 1rem; padding: 10px 0;">
            <% if (MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CustomerCan("Sensor_Edit") && MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                { %>
            <a class="btn btn-secondary user-dets" href="/LookUp/NetworkDevicesXmlData/<%=Model.CSNetID %>"><%: Html.TranslateTag("Network/Edit|Device Xml Download","Device Xml Download")%></a>
            <% }%>

            <a href="/Network/NetworkSelect?AccountID=<%:MonnitSession.CurrentCustomer.AccountID %>&networkID=<%:Model.CSNetID%>" class="btn btn-secondary user-dets">
                <span>
                    <%: Html.TranslateTag("Network/Edit|Add Device","Add Device to Network")%>
                </span>
                <span class="ms-1">
                    <%=Html.GetThemedSVG("add") %>
                </span>
            </a>
        </div>

        <div class="col-12">
            <div class="col-md-6 col-12 ps-0 pe-md-2 mb-4" id="hook-five">
                <div class=" detailed-card-list_container">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <%=Html.GetThemedSVG("sensor") %>
                            &nbsp;
                    <%: Html.TranslateTag("Sensors","Sensors")%> 
                    : 
                    <%List<Gateway> NonWiFiGateways = Model.Gateways.Where(g => { return g.SensorID == long.MinValue && g.RadioBand != "WIFI"; }).ToList(); %>
                    &nbsp;
                    <span style="font-weight: 500;">
                        <%: Model.Sensors.Count() %> 
                    </span>
                        </div>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="card_container__body  bsInset">
                        <div class="card_container__content" style="padding: 3px 10px; display: flex; flex-wrap: wrap;">
                            <%foreach (Sensor sens in Model.Sensors.OrderBy(m => m.ApplicationID))
                                { %>
                            <div class="card-w-data" style="padding: 0px 10px; flex-basis: 100%;" title="SKU: <%: !sens.SKU.IsEmpty() ? sens.SKU : "No SKU" %>">
                                <div class="card-contents" style="font-size: 1em; height: 100%;">
                                    <div class="details-n-card">
                                        <strong><%=sens.SensorName %></strong>
                                        <br />
                                        <% Html.RenderPartial("~/Views/Shared/DeviceIDAndCheckCode.ascx", sens.SensorID); %>
                                    </div>
                                    <div style="display: flex; flex-basis: 30%;">
                                        <%: Html.TranslateTag("Network/Edit|RadioBand", "RadioBand")%>: <%=sens.RadioBand %><br />
                                        <%: Html.TranslateTag("Network/Edit|Version", "Version")%>: <%=sens.FirmwareVersion %>
                                    </div>
                                    <div class="" style="display: flex; flex-basis: 30%;">
                                        <%=sens.MonnitApplication.ApplicationName %>
                                        <br />
                                    </div>
                                    <div class="d-flex" style="margin-left: auto">
                                        <div class="gatewayList_detail">
                                            <div class="dropleft">
                                                <div class="menu-hover" style="" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                                                    <%=Html.GetThemedSVG("menu") %>
                                                </div>
                                                <div class="dropdown-menu ddm" aria-labelledby="dropdownMenuButton">
                                                    <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                                                        { %>
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Overview/SensorChart/<%:sens.SensorID%>">
                                                        <span><%: Html.TranslateTag("View", "View")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("view") %>
                                                        </span>
                                                    </a>
                                                    <%} %>

                                                    <% if (MonnitSession.CustomerCan("Sensor_Edit"))
                                                        { %>
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Overview/SensorEdit/<%:sens.SensorID%>">
                                                        <span><%: Html.TranslateTag("Edit", "Edit")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("edit") %>
                                                        </span>
                                                    </a>
                                                    <%if (MonnitSession.CustomerCan("Network_Edit"))
                                                        {
                                                            if (networks.Count() > 1)
                                                            { %>
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Setup/AssignDevice/<%:Model.AccountID%>?networkID=<%=Model.CSNetID %>&DeviceToMove=<%=sens.SensorID %>:IM<%:MonnitUtil.CheckDigit(sens.SensorID)%>">
                                                        <span><%: Html.TranslateTag("Move", "Move")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("network") %>
                                                        </span>
                                                    </a>
                                                    <%} %>
                                                    <hr class="my-0">
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" onclick="removeSensor(<%=sens.SensorID %>); return false;" style="cursor: pointer;">
                                                        <span><%: Html.TranslateTag("Delete", "Delete")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("delete") %>
                                                        </span>
                                                    </a>
                                                    <%}
                                                        } %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <style>
                                ::-webkit-scrollbar {
                                    width: 0px;
                                }

                                ::-webkit-scrollbar-track-piece {
                                    background-color: transparent;
                                    -webkit-border-radius: 0px;
                                }
                            </style>
                            <%} %>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-12 ps-md-2">
                <div class="detailed-card-list_container">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <%=Html.GetThemedSVG("gateway") %>
                            &nbsp;
                    <%: Html.TranslateTag("Gateways","Gateways")%>:
                    &nbsp;
                    <span style="font-weight: 500;">
                        <%: NonWiFiGateways.Count()%>
                    </span>
                        </div>
                        <div class="nav navbar-right panel_toolbox"></div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="card_container__body bsInset">
                        <div class="card_container__content" style="display: flex; flex-wrap: wrap; padding: 3px 10px;">
                            <%foreach (Gateway gate in Model.Gateways)
                                {
                                    //Wifi = 11, PoE = 35, LTE = 36 Wifi2 = 38
                                    if (gate.GatewayTypeID.ToInt() != 11 && gate.GatewayTypeID.ToInt() != 35 && gate.GatewayTypeID.ToInt() != 36 && gate.GatewayTypeID.ToInt() != 38)
                                    { %>
                            <div class="card-w-data" style="padding: 0px 10px; flex-basis: 100%;" title="SKU: <%: gate.SKU %>">
                                <div class="card-contents" style="font-size: 1em; height: 100%;">
                                    <div class="details-n-card">
                                        <strong><%=gate.Name %></strong><br />
                                        <% Html.RenderPartial("~/Views/Shared/DeviceIDAndCheckCode.ascx", gate.GatewayID); %>
                                    </div>
                                    <div class="" style="display: flex; flex-basis: 30%">
                                        <%: Html.TranslateTag("Network/Edit|RadioBand", "RadioBand")%>: <%=gate.RadioBand %><br />
                                        <%: Html.TranslateTag("Network/Edit|APN", "APN")%>: <%=gate.APNFirmwareVersion %>
                                    </div>
                                    <div class="" style="display: flex; flex-basis: 30%">
                                        <%= gate.GatewayType.Name%><br />
                                        <%: Html.TranslateTag("Network/Edit|Firmware", "Firmware")%>: <%: gate.GatewayFirmwareVersion %>
                                    </div>
                                    <div style="display: flex; margin-left: auto;">
                                        <div class="gatewayList_detail" style="margin-left: auto">
                                            <div class="dropleft">
                                                <div class="menu-hover" style="" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                                                    <%=Html.GetThemedSVG("menu") %>
                                                </div>
                                                <div class="dropdown-menu ddm" aria-labelledby="dropdownMenuButton">
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Overview/GatewayHome/<%:gate.GatewayID%>">
                                                        <span><%: Html.TranslateTag("View", "View")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("view") %>
                                                        </span>
                                                    </a><%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                                                            { %>
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Overview/GatewayEdit/<%:gate.GatewayID%>">
                                                        <span><%: Html.TranslateTag("Edit", "Edit")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("edit") %>
                                                        </span>
                                                    </a>
                                                    <%} %>
                                                    <%if (MonnitSession.CustomerCan("Network_Edit"))
                                                        {
                                                            if (networks.Count() > 1)
                                                            { %>
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" href="/Setup/AssignDevice/<%:Model.AccountID%>?networkID=<%=Model.CSNetID %>&DeviceToMove=<%=gate.GatewayID %>:IM<%:MonnitUtil.CheckDigit(gate.GatewayID)%>">
                                                        <span><%: Html.TranslateTag("Move", "Move")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("network") %>
                                                        </span>
                                                    </a>
                                                    <%} %>
                                                    <hr class="my-0">
                                                    <a class="dropdown-item menu_dropdown_item ddm-space py-2" onclick="removeGateway('<%:gate.GatewayID %>'); return false;" style="cursor: pointer;">
                                                        <span><%: Html.TranslateTag("Delete", "Delete")%></span>
                                                        <span>
                                                            <%=Html.GetThemedSVG("delete") %>
                                                        </span>
                                                    </a>
                                                    <%} %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <style type="text/css">
                                ::-webkit-scrollbar {
                                    width: 0px;
                                }

                                ::-webkit-scrollbar-track-piece {
                                    background-color: transparent;
                                    -webkit-border-radius: 0px;
                                }
                            </style>

                            <div class="clearfix"></div>
                            <%}

                                }%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {
            $('#externalAccess').mobiscroll().datepicker({
                theme: 'ios',
                controls: ['date', 'time'],
                dateFormat: '<%:MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString().ToUpper()%>',
                touchUI: 'true',
                display: popLocation,
                headerText: 'Date & Time: {value}',
                display: 'anchored',
                /*controls: ['Date Format', 'Time Format'],*/
            });

            $('#SaveBtn').click(function (e) {
                if ($('#networkName').val().length < 1) {
                    $(SaveBtn).show(); $('#saving').hide();
                    return;
                }
                var netName = $('#networkName').val().replace(/[<>]/g, '');
                $('#networkName').val(netName);
            });

            toastBuilder("<%=response%>")

        });

        var confirmRemoveGateway = "<%:Html.TranslateTag("Network/DetailList|Are you sure you want to remove this gateway from the network","Are you sure you want to remove this gateway from the network?")%>";

        function removeGateway(item) {
            let values = {};
            values.url = `/Network/RemoveGateway/${item}`;
            values.text = confirmRemoveGateway;
            openConfirm(values);
        }
        var confirmRemoveSensor = "<%: Html.TranslateTag("Network/DetailList|Are you sure you want to remove this sensor from the network","Are you sure you want to remove this sensor from the network?")%>";

        function removeSensor(item) {
            let values = {};
            values.url = `/Network/RemoveSensor/${item}`;
            values.text = confirmRemoveSensor;
            openConfirm(values);
        }

        var confirmRemoveNetwork = "<%: Html.TranslateTag("Network/DetailList|Deleting your network will delete ALL of your Sensors and Gateways. Are you sure you want to continue?")%>";

        function deleteNetwork() {
            let values = {};
            values.url = "/Network/Delete/<%:Model.CSNetID %>";
            values.text = confirmRemoveNetwork;
            values.redirect = "/Network/List";
            openConfirm(values);
        }


    </script>

    <style type="text/css">
        #svg_menu {
            bottom: 15px;
            right: 15px;
        }

        #svg_cardList g {
            stroke: #FFF !important;
        }
    </style>
</asp:Content>

