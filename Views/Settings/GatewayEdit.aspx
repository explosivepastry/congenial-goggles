<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Gateway Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="x_panel shadow-sm rounded my-4">
            <div class="x_title">
                <div class="card_container__top__title"><%: Html.TranslateTag("Settings/GatewayEdit|Admin Gateway Edit","Admin Gateway Edit")%></div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content col-12">
                <div class="col-lg-2 col-sm-6 col-11 input-group" style="width: 250px;">
                    <input id="ID" type="text" class="form-control" name="loadGateway" placeholder="Gateway ID">
                    <span class="btn btn-primary" onclick="loadGateway();">
                        <%:Html.GetThemedSVG("search") %>
                    </span>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
        <% if (Model != null)
            { %>
        <% using (Html.BeginForm())
            { %>
        <%: Html.ValidationSummary(true) %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

        <div class="x_panel shadow-sm rounded">
            <form id="prefForm" class="form-horizontal form-label-left" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <div class="x_title">
                    <h2><%: Html.TranslateTag("Settings/GatewayEdit|Configuration","Configuration")%></h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Gateway ID","Gateway ID")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <h4>
                            <%: Model.GatewayID %>

                            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                                {
                                    if (Model.IsDeleted)
                                    {%>
                            <span>-Permanently Deleted-</span>
                            <%}
                                else
                                {%>
                            <a href="/Settings/PermanentDeleteGateway/<%:Model.GatewayID%>" class="permaDelete btn btn-danger">Permanently Disable/ Delete</a>
                            <%}
                                }%>
                        </h4>
                        <%: Html.HiddenFor(model => model.GatewayID) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Gateway Name","Gateway Name")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input id="Name" name="Name" type="text" value="<%=Model.Name %>" class="form-control">
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Type","Type")%>
                    </div>

                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.DropDownListFor(gtn=> gtn.GatewayTypeID, new SelectList(Monnit.GatewayType.LoadAll(),"GatewayTypeID","Name")) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                    </div>

                    <div class="col-lg-4 col-sm-6 col-12">
                        <a href="/Overview/GatewayHome/<%:Model.GatewayID %>" target="_blank" class="btn btn-primary">View Gateway</a>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Network","Network")%>
                    </div>

                    <div class="col-lg-4 col-sm-6 col-12">
                        <%CSNet Network = CSNet.Load(Model.CSNetID); %>
                        <h4><%= Network != null ? Network.Name : Model.Name.ToUpper() + Html.TranslateTag("Settings/GatewayEdit| DELETED FROM NETWORK"," DELETED FROM NETWORK") %></h4>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Account","Account")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <h4><%: Account.Load(Network.AccountID).CompanyName %></h4>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Last Check In","Last Check In")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input id="LastCommunicationDate" disabled="disabled" name="LastCommunicationDate" type="text" value="<%= DateTime.Parse(Model.LastCommunicationDate.ToString("MM/dd/yyyy HH:mm:ss"))%>" class="form-control">
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Resume Notification Date","Resume Notification Date")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input <%= MonnitSession.CustomerCan("Notification_Pause") ? "" : "disabled=\"disabled\"" %> type="text" value="<%= DateTime.Parse(Model.resumeNotificationDate.ToString("MM/dd/yyyy HH:mm:ss"))%>" id="resumeNotificationDate" name="resumeNotificationDate" class="datepicker aSettings__input_input" <%= !MonnitSession.CurrentCustomer.IsAdmin ? "readonly='readonly'" : ""%> />
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|APN Firmware","APN Firmware")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.TextBoxFor(af=>af.APNFirmwareVersion)%>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Gateway Firmware","Gateway Firmware")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.TextBoxFor(gf=>gf.GatewayFirmwareVersion) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|RadioBand","RadioBand")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.TextBoxFor(rb=>rb.RadioBand) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Power Source","Power Source")%>
                    </div>
                    <%--<div class="col-lg-4 col-sm-6 col-12">
                    <%:  Model.PowerSource == null ? "" : Model.PowerSource.Name %>
                </div>--%>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.DropDownList("PowerSourceID", new SelectList(PowerSource.LoadAll(), "PowerSourceID", "Name"), null, null)%>
                        <%: Html.ValidationMessageFor(model => model.PowerSourceID)%>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        AutoConfig Time
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.TextBoxFor(rb=>rb.AutoConfigTime) %>
                        <%: Html.ValidationMessageFor(model => model.AutoConfigTime)%>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        AutoConfigActionCommandTime
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.TextBoxFor(rb=>rb.AutoConfigActionCommandTime) %>
                        <%: Html.ValidationMessageFor(model => model.AutoConfigActionCommandTime)%>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Security Key","Security Key")%>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <%: Html.TextBoxFor(rb=>rb.SecurityKeyString) %>
                        <%: Html.ValidationMessageFor(model => model.SecurityKeyString)%>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Is Enterprise Host","Is Enterprise Host")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.isEnterpriseHost) %>
                        <%: Html.ValidationMessageFor(model => model.isEnterpriseHost) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        Send AutoConfigStart
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.hasActionControlCommand) %>
                        <%: Html.ValidationMessageFor(model => model.hasActionControlCommand) %>
                    </div>
                </div>
                <%--<div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.LabelFor(model => model.IsDeleted) %>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsDeleted) %>
                    <%: Html.ValidationMessageFor(model => model.IsDeleted) %>
                </div>
            </div>--%>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ForceToBootloader) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ForceToBootloader) %>
                        <%: Html.ValidationMessageFor(model => model.ForceToBootloader) %>
                    </div>
                </div>
                <%if (Model.GatewayType.SupportsSensorInterpretor)
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Sensor Interpretor Version","Sensor Interpretor Version")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Model.SensorInterpretorVersion %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Update Sensor Interpretor","Update Sensor Interpretor")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SendSensorInterpretor) %>
                        <%: Html.ValidationMessageFor(model => model.SendSensorInterpretor) %>
                    </div>
                </div>
                <%} %>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.IgnoreData) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.IgnoreData) %>
                        <%: Html.ValidationMessageFor(model => model.IgnoreData) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Test Watchdog","Test Watchdog")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SendWatchDogTest) %>
                        <%: Html.ValidationMessageFor(model => model.SendWatchDogTest) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Cell Module Update","Cell Module Update")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SendUpdateModuleCommand) %>
                        <%: Html.ValidationMessageFor(model => model.SendUpdateModuleCommand) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Last Inbound IPAddress","Last Inbound IPAddress")%>
                    </div>
                    <div class="col-lg-3 col-md-4 col-sm-5 col-xs-12">
                        <h4><%: (string.IsNullOrEmpty(Model.LastInboundIPAddress)) ? Html.TranslateTag("Settings/GatewayEdit|Not Recorded","Not Recorded") : Model.LastInboundIPAddress%></h4>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/GatewayEdit|Last Sequence Number (UDP)","Last Sequence Number (UDP)")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <h4><%: Model.LastSequence %></h4>
                    </div>
                </div>
                <%if (Model.GatewayType.SupportsModbusInterface || Model.GatewayType.SupportsSNMPInterface || (Model.GatewayTypeID == 32 && new Version(Model.GatewayFirmwareVersion) > new Version("1.0.2.0")))
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.SingleQueueExpiration) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SingleQueueExpiration) %>
                        <%: Html.ValidationMessageFor(model => model.SingleQueueExpiration) %>
                    </div>
                </div>
                <%} %>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.GenerationType) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.GenerationType) %>
                        <%: Html.ValidationMessageFor(model => model.GenerationType) %>
                    </div>
                </div>
                <%if (!string.IsNullOrWhiteSpace(Model.GenerationType) && Model.GenerationType.ToUpper().Contains("GEN2") || Model.GenerationType.ToUpper().Contains("GEN4"))
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.UpdateRadioFirmware) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <% if (!Model.RadioFirmwareUpdateInProgress)
                            { %>
                        <%: Html.EditorFor(model => model.UpdateRadioFirmware) %>
                        <%}
                            else
                            { %>
                        <%: Html.DisplayFor(model => model.UpdateRadioFirmware) %>

                        <%} %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.RadioFirmwareUpdateID) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <% if (!Model.RadioFirmwareUpdateInProgress)
                            { %>
                        <%: Html.EditorFor(model => model.RadioFirmwareUpdateID) %>
                        <%}
                            else
                            { %>
                        <%: Html.DisplayFor(model => model.RadioFirmwareUpdateID) %>
                        <%} %>
                    </div>
                </div>
                <%} %>


                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.BacnetInterfaceActive) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.BacnetInterfaceActive) %>
                        <%: Html.ValidationMessageFor(model => model.BacnetInterfaceActive) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.DisableNetworkOnServerError) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.DisableNetworkOnServerError) %>
                        <%: Html.ValidationMessageFor(model => model.DisableNetworkOnServerError) %>
                    </div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ResetInterval) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ResetInterval) %>
                        <%: Html.ValidationMessageFor(model => model.ResetInterval) %>
                    </div>
                </div>

                <div class="clearfix"></div>
                <br />

                <!-- Wireless Sensor Network-->
                <div>
                    <div class="x_title">
                        <h2><%: Html.TranslateTag("Settings/GatewayEdit|Wireless Sensor Network","Wireless Sensor Network")%> </h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ChannelMask) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ChannelMask) %>
                            <%: Html.ValidationMessageFor(model => model.ChannelMask) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.NetworkIDFilter) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.NetworkIDFilter) %>
                            <%: Html.ValidationMessageFor(model => model.NetworkIDFilter) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.TranslateTag("Settings/GatewayEdit|Last Known Channel","Last Known Channel")%>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <h4><%: Model.LastKnownChannel %></h4>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.TranslateTag("Settings/GatewayEdit|Last Known NetworkID","Last Known NetworkID")%>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <h4><%: Model.LastKnownNetworkID %></h4>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.TranslateTag("Settings/GatewayEdit|Last Known Device Count","Last Known Device Count")%>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <h4><%: Model.LastKnownDeviceCount %></h4>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.TranslateTag("Settings/GatewayEdit|Request Network Information","Request Network Information")%>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RetreiveNetworkInfo) %>
                            <%: Html.ValidationMessageFor(model => model.RetreiveNetworkInfo) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.TranslateTag("Settings/GatewayEdit|Reform Network","Reform Network")%>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SendResetNetworkRequest) %>
                            <%: Html.ValidationMessageFor(model => model.SendResetNetworkRequest) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <!-- LAN Settings-->
                <div>
                    <div class="x_title">
                        <h2><%: Html.TranslateTag("Settings/GatewayEdit|LAN Settings","LAN Settings")%></h2>
                        <div class="clearfix"></div>
                    </div>
                    <%if (Model.GatewayTypeID == 24 || Model.GatewayTypeID == 25)
                        {
                            string[] simstrings = Model.MacAddress.Split('|');
                            if (simstrings.Length < 3) simstrings = new string[] { Html.TranslateTag("Settings/GatewayEdit|Not yet set", "Not yet set"), Html.TranslateTag("Settings/GatewayEdit|Not yet set", "Not yet set"), Html.TranslateTag("Settings/GatewayEdit|Not yet set", "Not yet set") };
                    %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            IMSI
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[0] %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            ICCID
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[1] %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            IMEI
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[2] %>
                        </div>
                    </div>
                    <%}
                        else if (Model.GatewayTypeID == 30 || Model.GatewayTypeID == 32)
                        {
                            string[] simstrings = Model.MacAddress.Split('|');
                            if (simstrings.Length > 3)
                            {
                    %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            IMSI
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[3] %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            ICCID
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[2] %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            IMEI
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[1] %>
                        </div>
                    </div>
                    <%
                        }
                        else
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Mac Address
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[0] %>
                        </div>
                    </div>
                    <%} %>
                    <%}
                        else if (Model.GatewayTypeID == 17 || Model.GatewayTypeID == 18 || Model.GatewayTypeID == 23 || Model.GatewayTypeID == 22)
                        {
                            string[] simstrings = Model.MacAddress.Split('|');
                            if (simstrings.Length < 2) simstrings = new string[] { Html.TranslateTag("Settings/GatewayEdit|Not yet set", "Not yet set"), Html.TranslateTag("Settings/GatewayEdit|Has your gateway checked in?", "Has your gateway checked in?") };
                    %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            MEID
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[0] %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Phone
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: simstrings[1] %>
                        </div>
                    </div>
                    <%}
                        else
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            MacAddress
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <input type="text" class="form-control" value="<%= Model.MacAddress%>" id="MacAddress" name="MacAddress" <%= !MonnitSession.CurrentCustomer.IsAdmin ? "readonly='readonly'":""%> />

                        </div>
                    </div>
                    <%} %>
                    <%if (Model.GatewayType.SupportsGatewayIP)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.GatewayIP) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.GatewayIP) %>
                            <%: Html.ValidationMessageFor(model => model.GatewayIP) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.GatewaySubnet) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.GatewaySubnet) %>
                            <%: Html.ValidationMessageFor(model => model.GatewaySubnet) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.DefaultRouterIP) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.DefaultRouterIP) %>
                            <%: Html.ValidationMessageFor(model => model.DefaultRouterIP) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.GatewayDNS) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.GatewayDNS) %>
                            <%: Html.ValidationMessageFor(model => model.GatewayDNS) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SecondaryDNS) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SecondaryDNS) %>
                            <%: Html.ValidationMessageFor(model => model.SecondaryDNS) %>
                        </div>
                    </div>
                    <%} %>
                </div>
                <div class="clearfix"></div>
                <br />


                <!-- Cellular Settings-->
                <div>
                    <div class="x_title">
                        <h2>Cellular Settings</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.CellAPNName) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.CellAPNName) %>
                            <%: Html.ValidationMessageFor(model => model.CellAPNName) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.Username) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.Username) %>
                            <%: Html.ValidationMessageFor(model => model.Username) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.Password) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.Password) %>
                            <%: Html.ValidationMessageFor(model => model.Password) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />

                <!-- Server Interface-->
                <div>
                    <div class="x_title">
                        <h2>Server Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ServerInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ServerInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.ServerInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ReportInterval) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ReportInterval) %>
                            <%: Html.ValidationMessageFor(model => model.ReportInterval) %>
                        </div>
                    </div>
                    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || Model.IsUnlocked)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ServerHostAddress) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ServerHostAddress) %>
                            <%: Html.ValidationMessageFor(model => model.ServerHostAddress) %>
                        </div>
                    </div>
                    <%if (Model.GatewayType.SupportsHostAddress2)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ServerHostAddress2) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ServerHostAddress2) %>
                            <%: Html.ValidationMessageFor(model => model.ServerHostAddress2) %>
                        </div>
                    </div>
                    <%} %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.Port) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.Port) %>
                            <%: Html.ValidationMessageFor(model => model.Port) %>
                        </div>
                    </div>
                    <%if (Model.GatewayType.SupportsHostAddress2)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.Port2) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.Port2) %>
                            <%: Html.ValidationMessageFor(model => model.Port2) %>
                        </div>
                    </div>
                    <%} %>
                    <%}
                        else
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            ServerHostAddress
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.ServerHostAddress %>
                        </div>
                    </div>
                    <%if (Model.GatewayType.SupportsHostAddress2)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            ServerHostAddress2
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.ServerHostAddress2 %>
                        </div>
                    </div>
                    <%} %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Port
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.Port %>
                        </div>
                    </div>
                    <%if (Model.GatewayType.SupportsHostAddress2)
                        { %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Port2
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.Port2 %>
                        </div>
                    </div>
                    <%} %>
                    <%} %>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ObserveAware) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ObserveAware) %>
                            <%: Html.ValidationMessageFor(model => model.ObserveAware) %>
                        </div>
                    </div>

                    <div class="x_content col-12 d-flex">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Communication Preference
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.GatewayCommunicationPreference %>
                        </div>
                        <div class="editor-error">
                            <%: Html.ValidationMessageFor(model => model.GatewayCommunicationPreference)%>
                        </div>
                    </div>
                    <div class="x_content col-12 d-flex">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Gateway Power Mode
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.DropDownList<eGatewayPowerMode>("GatewayPowerMode", Model.GatewayPowerMode)%>
                        </div>
                        <div class="editor-error">
                            <%: Html.ValidationMessageFor(model => model.GatewayPowerMode)%>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.NetworkListInterval) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.NetworkListInterval) %>
                            <%: Html.ValidationMessageFor(model => model.NetworkListInterval) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.PollInterval) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.PollInterval) %>
                            <%: Html.ValidationMessageFor(model => model.PollInterval) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            GPS Report Interval(default: <%:Model.GatewayType.DefaultGPSReportInterval %>)
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.TextBoxFor(model => model.GPSReportInterval)%>
                        </div>
                        <div class="editor-error">
                            <%: Html.ValidationMessageFor(model => model.GPSReportInterval)%>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />

                <%if (Model.GatewayType.SupportsRealTimeInterface)
                    { %>
                <!-- Real Time Interface-->
                <div>
                    <div class="x_title">
                        <h2>Real Time Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.RealTimeInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RealTimeInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.RealTimeInterfaceTimeout) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RealTimeInterfaceTimeout) %>
                            <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceTimeout) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.RealTimeInterfacePort) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RealTimeInterfacePort) %>
                            <%: Html.ValidationMessageFor(model => model.RealTimeInterfacePort) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>
                <!-- MQTT Interface-->
                <%if (Model.GatewayTypeID == 31)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>MQTT Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTPort) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTPort) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTPort) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTBrokerAddress) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTBrokerAddress) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTBrokerAddress) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTUser) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTUser) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTUser) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTPassword) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTPassword) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTPassword) %>
                        </div>
                    </div>

                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTClientID) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTClientID) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTClientID) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTClientTopic) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTClientTopic) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTClientTopic) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTPublicationInterval) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTPublicationInterval) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTPublicationInterval) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTKeepAlive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTKeepAlive) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTKeepAlive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTAckTimeout) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTAckTimeout) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTAckTimeout) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTQueueFlushLimit) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTQueueFlushLimit) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTQueueFlushLimit) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.MQTTBehaviorFlags) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.MQTTBehaviorFlags) %>
                            <%: Html.ValidationMessageFor(model => model.MQTTBehaviorFlags) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>

                <!-- NTP Interface-->
                <%if (Model.GatewayTypeID == 33)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>NTP Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.NTPInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.NTPInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.NTPInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.NTPServerIP) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.NTPServerIP) %>
                            <%: Html.ValidationMessageFor(model => model.NTPServerIP) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.NTPMinSampleRate) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.NTPMinSampleRate) %>
                            <%: Html.ValidationMessageFor(model => model.NTPMinSampleRate) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>

                <!-- HTTP Interface-->
                <%if (Model.GatewayTypeID == 33)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>HTTP Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.HTTPInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.HTTPInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.HTTPInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.HTTPServiceTimeout) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.HTTPServiceTimeout) %>
                            <%: Html.ValidationMessageFor(model => model.HTTPServiceTimeout) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>

                <!-- Modbus Interface-->
                <%if (Model.GatewayType.SupportsModbusInterface)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>Modbus Interface</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ModbusInterfaceActive) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ModbusInterfaceActive) %>
                            <%: Html.ValidationMessageFor(model => model.ModbusInterfaceActive) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ModbusInterfaceTimeout) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ModbusInterfaceTimeout) %>
                            <%: Html.ValidationMessageFor(model => model.ModbusInterfaceTimeout) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ModbusInterfacePort) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ModbusInterfacePort) %>
                            <%: Html.ValidationMessageFor(model => model.ModbusInterfacePort) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>
                <!-- SNMP Interface 1 -->
                <%if (Model.GatewayType.SupportsSNMPInterface)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>SNMP Interface 1</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterface1Active) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterface1Active) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterface1Active) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfaceAddress1) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfaceAddress1) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress1) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfacePort1) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfacePort1) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort1) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPTrapPort1) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPTrapPort1) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPTrapPort1) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />

                <!-- SNMP Interface 2 -->
                <div>
                    <div class="x_title">
                        <h2>SNMP Interface 2</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterface2Active) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterface2Active) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterface2Active) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfaceAddress2) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfaceAddress2) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress2) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfacePort2) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfacePort2) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort2) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPTrapPort2) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPTrapPort2) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPTrapPort2) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />

                <!-- SNMP Interface 3 -->
                <div>
                    <div class="x_title">
                        <h2>SNMP Interface 3</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterface3Active) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterface3Active) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterface3Active) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfaceAddress3) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfaceAddress3) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress3) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfacePort3) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfacePort3) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort3) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPTrapPort3) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPTrapPort3) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPTrapPort3) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />

                <!-- SNMP Interface 4 -->
                <div>
                    <div class="x_title">
                        <h2>SNMP Interface 4</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterface4Active) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterface4Active) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterface4Active) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfaceAddress4) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfaceAddress4) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfaceAddress4) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPInterfacePort4) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPInterfacePort4) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPInterfacePort4) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.SNMPTrapPort4) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.SNMPTrapPort4) %>
                            <%: Html.ValidationMessageFor(model => model.SNMPTrapPort4) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>

                <!-- Wi-Fi Settings -->
                <%if (Model.GatewayTypeID == 10 || Model.GatewayTypeID == 11 || Model.GatewayTypeID == 38)
                    { %>
                <div>
                    <div class="x_title">
                        <h2>Wi-Fi Settings</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            Wi-Fi SensorID
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Model.SensorID %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.WiFiNetworkCount) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.WiFiNetworkCount) %>
                            <%: Html.ValidationMessageFor(model => model.WiFiNetworkCount) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.ErrorHeartbeat) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.ErrorHeartbeat) %>
                            <%: Html.ValidationMessageFor(model => model.ErrorHeartbeat) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.RealTimeInterfaceTimeout) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RealTimeInterfaceTimeout) %>
                            <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceTimeout) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.RealTimeInterfacePort) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.RealTimeInterfacePort) %>
                            <%: Html.ValidationMessageFor(model => model.RealTimeInterfacePort) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.TransmitPower) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.TransmitPower) %>
                            <%: Html.ValidationMessageFor(model => model.TransmitPower) %>
                        </div>
                    </div>
                    <div class="x_content col-12">
                        <div class="bold col-lg-4 col-sm-6 col-12">
                            <%: Html.LabelFor(model => model.LedActiveTime) %>
                        </div>
                        <div class="col-lg-4 col-sm-6 col-12">
                            <%: Html.EditorFor(model => model.LedActiveTime) %>
                            <%: Html.ValidationMessageFor(model => model.LedActiveTime) %>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <% } %>

                <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                    { %>
                <hr />
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <b>Unlock GPS</b>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input type="checkbox" name="IsGPSUnlockedchk" id="IsGPSUnlockedchk" <%: Model.IsGPSUnlocked ?"checked":"" %> />
                        <input type="hidden" name="IsGPSUnlocked" id="IsGPSUnlocked" value="<%: Model.IsGPSUnlocked %>" />
                    </div>
                    <div style="clear: both;"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <b>Send Unlock GPS</b>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input type="checkbox" name="SendGPSUnlockedchk" id="SendGPSUnlockedchk" <%: Model.SendGPSUnlockRequest ?"checked":"" %> />
                        <input type="hidden" name="SendGPSUnlockRequest" id="SendGPSUnlockRequest" value="<%: Model.SendGPSUnlockRequest %>" />
                    </div>
                    <div style="clear: both;"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <b>Send GPS Ping</b>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input type="checkbox" name="SendGPSPingchk" id="SendGPSPingchk" <%: Model.GPSPing ?"checked":"" %> />
                        <input type="hidden" name="GPSPing" id="GPSPing" value="<%: Model.GPSPing %>" />
                    </div>
                    <div style="clear: both;"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <b>Unlock Gateway</b>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input type="checkbox" name="IsUnlockedchk" id="IsUnlockedchk" <%: Model.IsUnlocked ?"checked":"" %> />
                        <input type="hidden" name="IsUnlocked" id="IsUnlocked" value="<%: Model.IsUnlocked %>" />
                    </div>
                    <div style="clear: both;"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <b>Send Unlock Gateway</b>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <input type="checkbox" name="SendUnlockedchk" id="SendUnlockedchk" <%: Model.SendUnlockRequest ?"checked":"" %> />
                        <input type="hidden" name="SendUnlockRequest" id="SendUnlockRequest" value="<%: Model.SendUnlockRequest %>" />
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <%} %>


                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.IsDirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.IsDirty) %>
                        <%: Html.ValidationMessageFor(model => model.IsDirty) %>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div class="clearfix"></div>
                <div class="formtitle text-end">
                    <input type="submit" value="Save" class="btn btn-primary" style="width: 100px;" />
                    <div style="clear: both;"></div>
                </div>
            </form>
        </div>
        <div class="clearfix"></div>
        <% } %>
        <% } %>
    </div>
    <div style="clear: both;"></div>
    <script>

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $('select').addClass("form-control");
        $("input").addClass("form-control");
        $(":checkbox").removeClass("form-control");
        $('#GatewayPowerMode').addClass('form-select');
        $('#GatewayPowerMode').removeClass('form-control');
        $('#SingleQueueExpiration').removeClass('text-box single-line');

        $(function () {
            $('#ID').bind('keypress', function (e) {
                var code = e.keyCode || e.which;
                if (code == 13) { //Enter keycode
                    loadGateway();
                }
            }).focus();

            $('#IsGPSUnlockedchk').change(function () {
                var checked = $('#IsGPSUnlockedchk').is(':checked');
                if (checked)
                    $('#IsGPSUnlocked').val("True");
                else
                    $('#IsGPSUnlocked').val("False");
            });

            $('#SendGPSUnlockedchk').change(function () {
                var checked = $('#SendGPSUnlockedchk').is(':checked');
                if (checked)
                    $('#SendGPSUnlockRequest').val("True");
                else
                    $('#SendGPSUnlockRequest').val("False");
            });

            $('#SendGPSPingchk').change(function () {
                var checked = $('#SendGPSPingchk').is(':checked');
                if (checked)
                    $('#GPSPing').val("True");
                else
                    $('#GPSPing').val("False");
            });

            $('#IsUnlockedchk').change(function () {
                var checked = $('#IsUnlockedchk').is(':checked');
                if (checked)
                    $('#IsUnlocked').val("True");
                else
                    $('#IsUnlocked').val("False");
            });

            $('#SendUnlockedchk').change(function () {
                var checked = $('#SendUnlockedchk').is(':checked');
                if (checked)
                    $('#SendUnlockRequest').val("True");
                else
                    $('#SendUnlockRequest').val("False");
            });

            $('.datepicker').mobiscroll().datepicker({
                theme: 'ios',
                display: popLocation,
                controls: ['calendar', 'time']
            });

            $('.permaDelete').click(function (e) {
                e.preventDefault();

                let values = {};
                values.url = this.href;
                values.text = "Are you sure you want to mark this as permanently deleted?";
                values.callback = function (data) {
                    showSimpleMessageModal(data);
                };
                openConfirm(values);
            });
        });

        function loadGateway() {
            window.location.href = '/Settings/GatewayEdit/' + $('#ID').val();
        }

        $('.stopRefr').click(function (e) {
            e.stopPropagation();
        });

    </script>

    <style>
        .bold {
            margin-top: 5px;
            font-size: 1rem;
        }
    </style>
</asp:Content>
