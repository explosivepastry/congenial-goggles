<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Sensor Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="x_panel shadow-sm rounded my-4">
            <div class="x_title">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/SensorEdit|Admin Sensor Edit","Admin Sensor Edit")%>
                </div>
            </div>
            <div class="x_content col-12">
                <div class="col-md-2 col-sm-4 col-11 input-group" style="width: 250px;">
                    <input id="ID" type="text" class="form-control" name="ID" placeholder="<%: Html.TranslateTag("Sensor ID","Sensor ID")%>">
                    <a role="button" id="searchID" onclick="window.location.href = '/Settings/SensorEdit/' + $('#ID').val();" class="btn btn-primary">
                        <%:Html.GetThemedSVG("search") %>
                    </a>
                </div>

                <div class="clearfix"></div>
            </div>
        </div>

        <% if (Model != null)
            {
                Version FirmwareVersion = null;
                try
                {
                    FirmwareVersion = new Version(Model.FirmwareVersion);
                }
                catch
                {
                    FirmwareVersion = new Version(0, 0, 0, 0);
                }
        %>

        <% using (Html.BeginForm())
            { %>
        <%: Html.ValidationSummary(true) %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <div class="x_panel shadow-sm rounded">
            <div class="x_title">
                <h2 class="fw-bold"><%: Html.TranslateTag("Settings/SensorEdit|Configuration","Configuration")%></h2>
                <div class="clearfix"></div>
            </div>
            
            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Sensor ID","Sensor ID")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <h4>
                        <%: Model.SensorID %>
                        
                        <%if (MonnitSession.CustomerCan("Support_Advanced")) { 
                            if(Model.IsDeleted) {%>
                            <span>-Permanently Deleted-</span>
                          <%}
                            else {%>
                            <a href="/Settings/PermanentDeleteSensor/<%:Model.SensorID%>" class="permaDelete btn btn-danger">Permanently Disable/ Delete</a>
                          <%}
                          }%>
                    </h4>
                    <%: Html.HiddenFor(model => model.SensorID) %>
                    
                </div>
            </div>
            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Sensor Name","Sensor Name")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <input class="form-control" type="text" name="SensorName" value="<%= Model.SensorName %>" />
                </div>
            </div>
            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Sensor Type","Sensor Type")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.DropDownListFor(x => x.SensorTypeID, new SelectList(Monnit.SensorType.LoadAll(), "SensorTypeID", "Name"))%>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <a href="/Overview/SensorChart/<%:Model.SensorID %>" target="_blank" class="btn btn-primary ">View Sensor</a>
                </div>
                <br />
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Gateway ID","Gateway ID")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%if (Model.SensorTypeID == 4 || Model.SensorTypeID == 6 || Model.SensorTypeID == 7 || Model.SensorTypeID == 8)
                        {
                            Gateway sensgtw = Gateway.LoadBySensorID(Model.SensorID);
                            if (sensgtw != null)
                            {%>
                    <h4><%: sensgtw.GatewayID%>&nbsp; |&nbsp;  <%: sensgtw.Name %></h4>
                    <%}
                        else
                        { %>
                    <h5 class="text-secondary"><%: Html.TranslateTag("No GatewayID","No GatewayID")%></h5>
                    <%}%>
                    <%}%>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Network","Network")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <h4><%= Model.CSNetID != long.MinValue ? CSNet.Load(Model.CSNetID).Name : Model.SensorName.ToUpper() + Html.TranslateTag("Settings/SensorEdit|DELETED FROM NETWORK!!", "  |  DELETED FROM NETWORK!!") %></h4>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Account","Account")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <h4><%: Account.Load(Model.AccountID).CompanyName %></h4>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Last Check in","Last Check in")%> UTC
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <input type="text" disabled="disabled" value="<%= DateTime.Parse(Model.LastCommunicationDate.ToString("MM/dd/yyyy HH:mm:ss"))%>" id="LastCommunicationDate" name="LastCommunicationDate" class="datepicker aSettings__input_input">
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Resume Notification Date","Resume Notification Date")%> UTC
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <input <%= MonnitSession.CustomerCan("Notification_Pause") ? "" : "disabled=\"disabled\"" %> type="text" value="<%= DateTime.Parse(Model.resumeNotificationDate.ToString("MM/dd/yyyy HH:mm:ss"))%>" id="resumeNotificationDate" name="resumeNotificationDate" class="datepicker aSettings__input_input" <%= !MonnitSession.CurrentCustomer.IsAdmin ? "readonly='readonly'" : ""%> />
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Sensor Start Date","Sensor Start Date")%> UTC
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <input <%= MonnitSession.CustomerCan("Reset_Start_Date") ? "" : "disabled=\"disabled\"" %>  type="text" value="<%= DateTime.Parse(Model.StartDate.ToString("MM/dd/yyyy HH:mm:ss"))%>" id="StartDate" name="StartDate" class="datepicker aSettings__input_input" <%= !MonnitSession.CurrentCustomer.IsAdmin ? "readonly='readonly'" : ""%> />
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Firmware Version","Firmware Version")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.TextBoxFor(fv => fv.FirmwareVersion) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Radio Band","Radio Band")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%:  Html.TextBoxFor(rb => rb.RadioBand) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Power Source","Power Source")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.DropDownListFor(x => x.PowerSourceID, new SelectList(Monnit.PowerSource.LoadAll(), "PowerSourceID", "Name"))  %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Is Sleeping","Is Sleeping")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsSleeping) %>
                    <%: Html.ValidationMessageFor(model => model.IsSleeping) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Is New To Network","Is New To Network")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsNewToNetwork) %>
                    <%: Html.ValidationMessageFor(model => model.IsNewToNetwork) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Is Active","Is Active")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsActive) %>
                    <%: Html.ValidationMessageFor(model => model.IsActive) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Is Deleted","Is Deleted")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsDeleted) %>
                    <%: Html.ValidationMessageFor(model => model.IsDeleted) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Pending Action Control Command","Pending Action Control Command")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.PendingActionControlCommand) %>
                    <%: Html.ValidationMessageFor(model => model.PendingActionControlCommand) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Cable Enabled","Cable Enabled")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.IsCableEnabled) %>
                    <%: Html.ValidationMessageFor(model => model.IsCableEnabled) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Force Overwrite","Force Overwrite")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.ForceOverwrite) %>
                    <%: Html.ValidationMessageFor(model => model.ForceOverwrite) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Generation Type","Generation Type")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.GenerationType) %>
                    <%: Html.ValidationMessageFor(model => model.GenerationType) %>
                </div>
            </div>

            <%if (!string.IsNullOrWhiteSpace(Model.GenerationType) && Model.GenerationType.ToUpper().Contains("GEN2"))
                { %>
            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Settings/SensorEdit|Send Firmware Update","Send Firmware Update")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <% if (!Model.FirmwareUpdateInProgress)
                        { %>
                    <%: Html.EditorFor(model => model.SendFirmwareUpdate) %>
                    <%}
                        else
                        { %>
                    <%: Html.DisplayFor(model => model.SendFirmwareUpdate) %>
                    <%} %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("SensorFirmwareID","SensorFirmwareID")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <% if (!Model.FirmwareUpdateInProgress)
                        { %>
                    <%: Html.EditorFor(model => model.SensorFirmwareID) %>
                    <%}
                        else
                        { %>
                    <%: Html.DisplayFor(model => model.SensorFirmwareID) %>
                    <%} %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Send Sensor Print","Send Sensor Print")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <%: Html.EditorFor(model => model.SensorPrintDirty) %>
                </div>
            </div>

            <div class="x_content col-12">
                <div class="bold col-lg-4 col-sm-6 col-12">
                    <%: Html.TranslateTag("Sensor Print","Sensor Print")%>
                </div>
                <div class="col-lg-4 col-sm-6 col-12">
                    <input class="form-control" id="SensorPrintHex" name="SensorPrintHex" type="text" value="<%:Model.SensorPrint.ToHex() %>">
                </div>
            </div>
            <%} %>

            <div class="clearfix"></div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("General Config 1","General Config 1")%></h2>
                    <div class="clearfix"></div>
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

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ActiveStateInterval) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ActiveStateInterval) %>
                        <%: Html.ValidationMessageFor(model => model.ActiveStateInterval) %>
                    </div>
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
                        <%: Html.LabelFor(model => model.TransmitIntervalLink) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TransmitIntervalLink) %>
                        <%: Html.ValidationMessageFor(model => model.TransmitIntervalLink) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.TransmitIntervalTest) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TransmitIntervalTest) %>
                        <%: Html.ValidationMessageFor(model => model.TransmitIntervalTest) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.TestTransmitCount) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TestTransmitCount) %>
                        <%: Html.ValidationMessageFor(model => model.TestTransmitCount) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.StandardMessageDelay) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.StandardMessageDelay) %>
                        <%: Html.ValidationMessageFor(model => model.StandardMessageDelay) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <label for="MaximumNetworkHops">LED Behavior/Max Network Hops</label>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.MaximumNetworkHops) %>
                        <%: Html.ValidationMessageFor(model => model.MaximumNetworkHops) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.GeneralConfigDirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.GeneralConfigDirty) %>
                        <%: Html.ValidationMessageFor(model => model.GeneralConfigDirty) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ReadGeneralConfig1) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ReadGeneralConfig1) %>
                        <%: Html.ValidationMessageFor(model => model.ReadGeneralConfig1) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("General Config 2","General Config 2")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.RetryCount) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.RetryCount) %>
                        <%: Html.ValidationMessageFor(model => model.RetryCount) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Recovery) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Recovery) %>
                        <%: Html.ValidationMessageFor(model => model.Recovery) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.GeneralConfig2Dirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.GeneralConfig2Dirty) %>
                        <%: Html.ValidationMessageFor(model => model.GeneralConfig2Dirty) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ReadGeneralConfig2) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ReadGeneralConfig2) %>
                        <%: Html.ValidationMessageFor(model => model.ReadGeneralConfig2) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("General Config 3","General Config 3")%></h2>
                    <div class="clearfix"></div>
                </div>

                <%if (Model.GenerationType.ToUpper().Contains("GEN1"))
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Listen Before Talk Value","Listen Before Talk Value")%>
                    </div>

                    <div class="col-lg-4 col-sm-6 col-12">
                        <select class="tzSelect" name="ListenBeforeTalkValue">
                            <option value="255" <%: Model.ListenBeforeTalkValue == 255 ? "selected" : "" %>><%: Html.TranslateTag("Default", "Default")%></option>
                            <option value="8" <%: Model.ListenBeforeTalkValue == 8 ? "selected" : "" %>><%: Html.TranslateTag("Off", "Off")%></option>
                            <option value="7" <%: Model.ListenBeforeTalkValue == 7 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Least Selective", "Least Selective")%></option>
                            <option value="3" <%: Model.ListenBeforeTalkValue == 3 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Low Selective", "Low Selective")%></option>
                            <option value="0" <%: Model.ListenBeforeTalkValue == 0 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Medium Selective", "Medium Selective")%></option>
                            <option value="13" <%: Model.ListenBeforeTalkValue == 13 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|High Selective", "High Selective")%></option>
                            <option value="9" <%: Model.ListenBeforeTalkValue == 9 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Most Selective", "Most Selective")%></option>
                        </select>

                        <div style="float: right">
                            <%: Html.TranslateTag("Settings/SensorEdit|Least Selective will only prevent talking if RF is very high.","Least Selective will only prevent talking if RF is very high.")%>
                            <br />
                            <%: Html.TranslateTag("Settings/SensorEdit|Most Selective will prevent talking even if RF is very low.","Most Selective will prevent talking even if RF is very low.")%>
                        </div>
                        <%: Html.ValidationMessageFor(model => model.ListenBeforeTalkValue) %>
                    </div>
                </div>
                <%}
                    else if (Model.GenerationType.Contains("Gen2") && FirmwareVersion.Minor >= 24)
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Listen Before Talk Value","Listen Before Talk Value")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ListenBeforeTalkValue) %>
                        <%: Html.ValidationMessageFor(model => model.ListenBeforeTalkValue) %>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-8 col-xs-12">
                        <%: Html.TranslateTag("RSSI Threshold in dBm.  When signals are above this value, the channel is considered busy.  -50 to -120 values permitted for normal operation.  0 means this is disabled.","RSSI Threshold in dBm.  When signals are above this value, the channel is considered busy.  -50 to -120 values permitted for normal operation.  0 means this is disabled.")%>
                    </div>
                </div>
                <%} %>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Link Acceptance Value","Link Acceptance Value")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.LinkAcceptance) %>
                        <%: Html.ValidationMessageFor(model => model.LinkAcceptance) %>
                    </div>
                </div>

                <% if (!string.IsNullOrWhiteSpace(Model.GenerationType) && Model.GenerationType.ToUpper().Contains("GEN2"))
                    { %>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Crystal Start Time","Crystal Start Time")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.CrystalStartTime) %>
                        <%: Html.ValidationMessageFor(model => model.CrystalStartTime) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("DM Exchange Delay Multiple","DM Exchange Delay Multiple")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.DMExchangeDelayMultiple) %>
                        <%: Html.ValidationMessageFor(model => model.DMExchangeDelayMultiple) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("SHID1","SHID1")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SHID1) %>
                        <%: Html.ValidationMessageFor(model => model.SHID1) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("SHID2","SHID2")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.SHID2) %>
                        <%: Html.ValidationMessageFor(model => model.SHID2) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("CryptRequired","CryptRequired")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.CryptRequired) %>
                        <%: Html.ValidationMessageFor(model => model.CryptRequired) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Pingtime","Pingtime")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Pingtime) %>
                        <%: Html.ValidationMessageFor(model => model.Pingtime) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Time Offset","Time Offset")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TimeOffset) %>
                        <%: Html.ValidationMessageFor(model => model.TimeOffset) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Time of Day Control","Time of Day Control")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TimeOfDayControl) %>
                        <%: Html.ValidationMessageFor(model => model.TimeOfDayControl) %>
                    </div>
                </div>
                <%} %>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.GeneralConfig3Dirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.GeneralConfig3Dirty) %>
                        <%: Html.ValidationMessageFor(model => model.GeneralConfig3Dirty) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ReadGeneralConfig3) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ReadGeneralConfig3) %>
                        <%: Html.ValidationMessageFor(model => model.ReadGeneralConfig3) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <%if (Model.GenerationType.ToUpper().Contains("GEN1"))
                { %>
            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("RF Config 1","RF Config 1")%></h2>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Transmit Power","Transmit Power")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <select class="tzSelect" id="TransmitPower" name="TransmitPower">
                            <% if (Model.RadioBand.Contains("900"))
                                {
                            %><option value="196" <%: Model.TransmitPower == 196 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Max","Max")%></option>
                            <%
                                }
                                else if (Model.RadioBand.Contains("868"))
                                {
                            %><option value="192" <%: Model.TransmitPower == 192 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Max","Max")%></option>
                            <%
                                }
                                else if (Model.RadioBand.Contains("433"))
                                {
                            %><option value="201" <%: Model.TransmitPower == 201 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Max","Max")%></option>
                            <%
                                }
                                else if (Model.RadioBand.Contains("920"))
                                {
                            %><option value="192" <%: Model.TransmitPower == 192 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Max","Max")%></option>
                            <%
                                } %>
                            <option value="134" <%: Model.TransmitPower == 134 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|High","High")%></option>
                            <option value="97" <%: Model.TransmitPower == 97 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Med","Med")%></option>
                            <option value="26" <%: Model.TransmitPower == 26 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Low","Low")%></option>
                            <option value="0" <%: Model.TransmitPower == 0 ? "selected" : "" %>><%: Html.TranslateTag("Settings/SensorEdit|Min","Min")%></option>
                        </select>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.TransmitPower) %>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Receive Sensitivity","Receive Sensitivity")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <select class="tzSelect" id="ReceiveSensitivity" name="ReceiveSensitivity">
                            <option value="0" <%: Model.ReceiveSensitivity == 0 ? "selected" : "" %>><%: Html.TranslateTag("Default","Default")%></option>
                            <option value="1" <%: Model.ReceiveSensitivity == 1 ? "selected" : "" %>>1</option>
                            <option value="2" <%: Model.ReceiveSensitivity == 2 ? "selected" : "" %>>2</option>
                            <option value="3" <%: Model.ReceiveSensitivity == 3 ? "selected" : "" %>>3</option>
                        </select>
                        <%: Html.ValidationMessageFor(model => model.ReceiveSensitivity) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.RFConfig1Dirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.RFConfig1Dirty) %>
                        <%: Html.ValidationMessageFor(model => model.RFConfig1Dirty) %>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <br />
            <%}
                else if (Model.GenerationType.Contains("Gen2") && FirmwareVersion.Minor >= 24)
                {%>
            <div>
                <div class="x_title">
                    <h2><%: Html.TranslateTag("RF Config 1","RF Config 1")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Transmit Power","Transmit Power")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TransmitPower) %>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-8 col-xs-12">
                        <%: Html.TranslateTag("Invalid numbers are 0/0xFFFF.  All other numbers are valid between 1-65534","Invalid numbers are 0/0xFFFF.  All other numbers are valid between 1-65534")%>
                        <br />
                        <%: Html.TranslateTag("** it is assumed that if CUSTOM transmit power is used, the device will not use TXboost.  This means that maximum possible transmit power is not available for this setting.","** it is assumed that if CUSTOM transmit power is used, the device will not use TXboost.  This means that maximum possible transmit power is not available for this setting.")%>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.TransmitPower) %>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Transmit Power Options","Transmit Power Options")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <select class="form-select" id="TransmitPowerOptions" name="TransmitPowerOptions">
                            <option value="0" <%: Model.TransmitPowerOptions == 0 ? "selected" : "" %>><%: Html.TranslateTag("Optimized for battery power","Optimized for battery power")%></option>
                            <option value="1" <%: Model.TransmitPowerOptions == 1 ? "selected" : "" %>><%: Html.TranslateTag("Optimized for Range","Optimized for Range")%></option>
                            <option value="2" <%: Model.TransmitPowerOptions == 2 ? "selected" : "" %>><%: Html.TranslateTag("Use custom transmit power setting","Use custom transmit power setting")%></option>
                        </select>
                    </div>
                    <%: Html.ValidationMessageFor(model => model.TransmitPowerOptions) %>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Receive Sensitivity","Receive Sensitivity")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        0x&nbsp;<input class="aSettings__input_input" id="ReceiveSensitivityHex" name="ReceiveSensitivityHex" value="<%:Model.ReceiveSensitivity.ToString("X2") %>" style="width: 85%; display: inline-block;" />
                        <input type="hidden" id="ReceiveSensitivity" name="ReceiveSensitivity" value="<%:Model.ReceiveSensitivity %>" />
                        <%: Html.ValidationMessageFor(model => model.ReceiveSensitivity) %>

                        <script type="text/javascript">
                            $(document).ready(function () {
                                $('#ReceiveSensitivityHex').change(function () {
                                    var decimalVal = parseInt($('#ReceiveSensitivityHex').val(), 16);
                                    $('#ReceiveSensitivity').val(decimalVal);
                                });
                            });
                        </script>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.RFConfig1Dirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.RFConfig1Dirty) %>
                        <%: Html.ValidationMessageFor(model => model.RFConfig1Dirty) %>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <br />
            <%} %>
            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("Profile Config 1","Profile Config 1")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.MeasurementsPerTransmission) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.MeasurementsPerTransmission) %>
                        <%: Html.ValidationMessageFor(model => model.MeasurementsPerTransmission) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.TransmitOffset) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.TransmitOffset) %>
                        <%: Html.ValidationMessageFor(model => model.TransmitOffset) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Hysteresis) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Hysteresis) %>
                        <%: Html.ValidationMessageFor(model => model.Hysteresis) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.MinimumThreshold) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.MinimumThreshold) %>
                        <%: Html.ValidationMessageFor(model => model.MinimumThreshold) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.MaximumThreshold) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.MaximumThreshold) %>
                        <%: Html.ValidationMessageFor(model => model.MaximumThreshold) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.EventDetectionType) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.EventDetectionType) %>
                        <%: Html.ValidationMessageFor(model => model.EventDetectionType) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.EventDetectionPeriod) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.EventDetectionPeriod) %>
                        <%: Html.ValidationMessageFor(model => model.EventDetectionPeriod) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.EventDetectionCount) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.EventDetectionCount) %>
                        <%: Html.ValidationMessageFor(model => model.EventDetectionCount) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.RearmTime) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.RearmTime) %>
                        <%: Html.ValidationMessageFor(model => model.RearmTime) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.BiStable) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.BiStable) %>
                        <%: Html.ValidationMessageFor(model => model.BiStable) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ProfileConfigDirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ProfileConfigDirty) %>
                        <%: Html.ValidationMessageFor(model => model.ProfileConfigDirty) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ReadProfileConfig1) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ReadProfileConfig1) %>
                        <%: Html.ValidationMessageFor(model => model.ReadProfileConfig1) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("Profile Config 2","Profile Config 2")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Calibration1) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Calibration1) %>
                        <%: Html.ValidationMessageFor(model => model.Calibration1) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Calibration2) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Calibration2) %>
                        <%: Html.ValidationMessageFor(model => model.Calibration2) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Calibration3) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Calibration3) %>
                        <%: Html.ValidationMessageFor(model => model.Calibration3) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.Calibration4) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.Calibration4) %>
                        <%: Html.ValidationMessageFor(model => model.Calibration4) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ProfileConfig2Dirty) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ProfileConfig2Dirty) %>
                        <%: Html.ValidationMessageFor(model => model.ProfileConfig2Dirty) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ReadProfileConfig2) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ReadProfileConfig2) %>
                        <%: Html.ValidationMessageFor(model => model.ReadProfileConfig2) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("Settings/SensorEdit|Calibration Lock","Calibration Lock")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Calibration Facility","Calibration Facility")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.DropDownListFor(m => m.CalibrationFacilityID, CalibrationFacility.LoadAll(), "Name","No Certification")%>
                    </div>
                </div>

                    <%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();%>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.TranslateTag("Settings/SensorEdit|Calibration Valid Until","Calibration Valid Until")%>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                         <input disabled="disabled" type="text" value="<%= DateTime.Parse(CalibrationCertificationValidUntil.ToString("MM/dd/yyyy HH:mm:ss"))%>" id="CalibrationCertificationValidUntil" name="CalibrationCertificationValidUntil" class="datepicker aSettings__input_input"  />
                       
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.CalibrationCertification) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.CalibrationCertification) %>
                        <%: Html.ValidationMessageFor(model => model.CalibrationCertification) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <br />

            <div>
                <div class="x_title">
                    <h2 class="fw-bold"><%: Html.TranslateTag("Settings/SensorEdit|WiFi Specific","WiFi Specific")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.ForceToBootloader) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.ForceToBootloader) %>
                        <%: Html.ValidationMessageFor(model => model.ForceToBootloader) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.DataLog) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.DataLog) %>
                        <%: Html.ValidationMessageFor(model => model.DataLog) %>
                    </div>
                </div>

                <div class="x_content col-12">
                    <div class="bold col-lg-4 col-sm-6 col-12">
                        <%: Html.LabelFor(model => model.LocalConfigOptions) %>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <%: Html.EditorFor(model => model.LocalConfigOptions) %>
                        <%: Html.ValidationMessageFor(model => model.LocalConfigOptions) %>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <div class="x_content col-12 text-end">
                <a href="/Settings/SensorDefault/<%:Model.SensorID%>" class="btn btn-light"><%: Html.TranslateTag("Settings/SensorEdit|Default Settings","Default Settings")%></a>
                <input type="submit" class="btn btn-primary ms-2" style="width: 80px;" value="Save" />
            </div>
            <div class="clearfix"></div>
        </div>

    </div>
    <% } %>

    <div style="clear: both;"></div>

    <script type="text/javascript">

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $('select').addClass("form-control");
        $("input").addClass("form-control");
        $(":checkbox").removeClass("form-control");

        var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;
        $(document).ready(function () {
            $('#ID').bind('keypress', function (e) {
                var code = e.keyCode || e.which;
                if (code == 13) { //Enter keycode
                    loadSensor();
                }
            }).focus();

            $('.datepicker').mobiscroll().datepicker({
                theme: 'ios',
                display: popLocation,
                controls: ['calendar', 'time']
            });

            $("#ReportInterval").change(function () {
                if (isANumber($("#ReportInterval").val())) {
                    if ($("#ReportInterval").val() < minReportInterval) {
                        $("#ReportInterval").val(minReportInterval);
                    }
                    if ($("#ReportInterval").val() > 720) {
                        $("#ReportInterval").val(720);
                    }
                    if (Number($('#ReportInterval').val()) < Number($('#ActiveStateInterval').val())) {
                        $('#ActiveStateInterval').val(Number($('#ReportInterval').val()));
                    }
                }
                else {
                    $("#ReportInterval").val(<%: Model.ReportInterval%>);
                }
            });

            $("#ActiveStateInterval").change(function () {
                if (isANumber($("#ActiveStateInterval").val())) {
                    if ($("#ActiveStateInterval").val() < minReportInterval)
                        $("#ActiveStateInterval").val(minReportInterval);

                    if ($("#ActiveStateInterval").val() > 720) {
                        $("#ActiveStateInterval").val(720);
                    }

                    if (Number($('#ActiveStateInterval').val()) > Number($('#ReportInterval').val())) {
                        $('#ReportInterval').val(Number($('#ActiveStateInterval').val()));
                    }
                }
                else {
                    $("#ActiveStateInterval").val(<%: Model.ActiveStateInterval%>);
                }
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

        function loadSensor() {
            window.location.href = '/Settings/SensorEdit/' + $('#ID').val();
        }
    </script>
    <% } %>

    <style>
        .bold {
            font-size: 1rem;
            margin-top: 5px;
        }
    </style>
</asp:Content>
